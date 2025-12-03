using FoodStock.IAM.Application.Internal.OutboundServices;
using FoodStock.IAM.Domain.Model.Aggregates;
using FoodStock.IAM.Domain.Model.Commands;
using FoodStock.IAM.Domain.Repositories;
using FoodStock.IAM.Domain.Services;

namespace FoodStock.IAM.Application.Internal.CommandServices;

public class UserCommandService : IUserCommandService
{
    private readonly IUserRepository _repo;
    private readonly IHashingService _hasher;

    public UserCommandService(IUserRepository repo, IHashingService hasher)
    {
        _repo = repo;
        _hasher = hasher;
    }

    public async Task<User> Handle(SignUpCommand command)
    {
        var existing = await _repo.GetByEmailAsync(command.Email);
        if (existing is not null)
            throw new InvalidOperationException("El correo ya está registrado.");

        var hashed = _hasher.Hash(command.Password);
        var user = new User(command.Name, command.Email, hashed, command.Phone, command.Segment, command.ProfilePicture);
        await _repo.AddAsync(user);
        return user;
    }

    public async Task<User> Handle(SelectPlanCommand command)
    {
        var user = await _repo.GetByIdAsync(command.UserId)
                   ?? throw new KeyNotFoundException("Usuario no encontrado.");

        user.SelectPlan(command.Plan);
        await _repo.UpdateAsync(user);
        return user;
    }
}