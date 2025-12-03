using FoodStock.IAM.Domain.Model.Aggregates;
using FoodStock.IAM.Domain.Model.Commands;

namespace FoodStock.IAM.Domain.Services;

public interface IUserQueryService
{
    Task<(User User, string Token)?> Handle(SignInCommand command);
}