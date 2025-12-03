using FoodStock.IAM.Domain.Model.Aggregates;

namespace FoodStock.IAM.Application.Internal.OutboundServices;

public interface ITokenService
{
    string Generate(User user);
}