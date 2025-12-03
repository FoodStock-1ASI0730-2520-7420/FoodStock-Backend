using FoodStock.IAM.Application.Internal.OutboundServices;
using FoodStock.IAM.Domain.Model.Aggregates;
using FoodStock.IAM.Domain.Model.Commands;
using FoodStock.IAM.Domain.Repositories;
using FoodStock.IAM.Domain.Services;

namespace FoodStock.IAM.Application.Internal.QueryServices;

public class UserQueryService : IUserQueryService
{
    private readonly IUserRepository _repo;
    private readonly IHashingService _hasher;
    private readonly ITokenService _token;

    public UserQueryService(IUserRepository repo, IHashingService hasher, ITokenService token)
    {
        _repo = repo;
        _hasher = hasher;
        _token = token;
    }

    public async Task<(User User, string Token)?> Handle(SignInCommand command)
    {
        var user = await _repo.GetByEmailAsync(command.Email);
        if (user is null) return null;

        var ok = _hasher.Verify(command.Password, user.Password);
        if (!ok) return null;

        var token = _token.Generate(user);
        return (user, token);
    }
}