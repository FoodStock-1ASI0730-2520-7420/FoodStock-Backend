using System.Text.Json.Serialization;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using FoodStock.IAM.Application.Internal.CommandServices;
using FoodStock.IAM.Application.Internal.OutboundServices;
using FoodStock.IAM.Application.Internal.QueryServices;
using FoodStock.IAM.Domain.Repositories;
using FoodStock.IAM.Domain.Services;
using FoodStock.IAM.Infrastructure.Hashing;
using FoodStock.IAM.Infrastructure.Persistence;
using FoodStock.IAM.Infrastructure.Tokens;
using FoodStock.Inventory.Application.Internal.CommandServices;
using FoodStock.Inventory.Application.Internal.QueryServices;
using FoodStock.Inventory.Domain.Repositories;
using FoodStock.Inventory.Domain.Services;
using FoodStock.Inventory.Infrastructure.Persistence.EFC.Repositories;
using FoodStock.Reservations.Application.Internal.CommandServices;
using FoodStock.Reservations.Application.Internal.QueryServices;
using FoodStock.Reservations.Domain.Repositories;
using FoodStock.Reservations.Domain.Services;
using FoodStock.Reservations.Infrastructure.Persistence.EFC.Repositories;
using FoodStock.Sales.Application.Internal.CommandServices;
using FoodStock.Sales.Application.Internal.QueryServices;
using FoodStock.Sales.Domain.Repositories;
using FoodStock.Sales.Domain.Services;
using FoodStock.Sales.Infrastructure.Persistence.EFC.Repositories;
using FoodStock.Shared.Domain.Repositories;
using FoodStock.Shared.Infrastructure.Interfaces.ASP.Configuration;
using FoodStock.Shared.Infrastructure.Mediator.Cortex.Configuration;
using FoodStock.Shared.Infrastructure.Persistence.EFC.Configuration;
using FoodStock.Shared.Infrastructure.Persistence.EFC.Repositories;
using FoodStock.Suppliers.Application.Internal.CommandServices;
using FoodStock.Suppliers.Application.Internal.QueryServices;
using FoodStock.Suppliers.Domain.Repositories;
using FoodStock.Suppliers.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention())).AddJsonOptions(options => {
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

if (connectionString == null) throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "FoodStock",
            Version = "v1",
            Description = "FoodStock API",
            Contact = new OpenApiContact
            {
                Name = "GestionPro",
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
    options.EnableAnnotations();
});

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Sales Bounded Context
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleItemRepository, SaleItemRepository>();
builder.Services.AddScoped<ISaleCommandService, SaleCommandService>();
builder.Services.AddScoped<ISaleQueryService, SaleQueryService>();
builder.Services.AddScoped<ISaleItemQueryService, SaleItemQueryService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDishRepository, DishRepository>();

builder.Services.AddScoped<IProductCommandService, ProductCommandService>();
builder.Services.AddScoped<IProductQueryService, ProductQueryService>();
builder.Services.AddScoped<IDishCommandService, DishCommandService>();
builder.Services.AddScoped<IDishQueryService, DishQueryService>();

// Suppliers Bounded Context
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();

builder.Services.AddScoped<CreateSupplierCommandService>();
builder.Services.AddScoped<UpdateSupplierCommandService>();
builder.Services.AddScoped<DeleteSupplierCommandService>();
builder.Services.AddScoped<GetAllSuppliersQueryService>();
builder.Services.AddScoped<GetSupplierByIdQueryService>();

// Reservations Bounded Context
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
builder.Services.AddScoped<TableCommandService>();
builder.Services.AddScoped<TableQueryService>();
builder.Services.AddScoped<ReservationCommandService>();
builder.Services.AddScoped<ReservationQueryService>();


// IAM Bounded Context
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();

//builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

// Mediator Configuration

// Add Mediator Injection Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

// Add Cortex Mediator for Event Handling
builder.Services.AddCortexMediator(
    configuration: builder.Configuration,
    handlerAssemblyMarkerTypes: [typeof(Program)], configure: options =>
    {
        options.AddOpenCommandPipelineBehavior(typeof(LoggingCommandBehavior<>));
        //options.AddDefaultBehaviors();
    });


var app = builder.Build();

// Verify if the database exists and create it if it doesn't
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply CORS Policy
app.UseCors("AllowAllPolicy");

// Add Authorization Middleware to Pipeline
//app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();