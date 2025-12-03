namespace FoodStock.IAM.Application.Internal.OutboundServices;

public interface IHashingService
{
    string Hash(string input);
    bool Verify(string input, string hashed);
}