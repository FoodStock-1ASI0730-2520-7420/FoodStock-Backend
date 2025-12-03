using BCrypt.Net;
using FoodStock.IAM.Application.Internal.OutboundServices;

namespace FoodStock.IAM.Infrastructure.Hashing;

public class HashingService : IHashingService
{
    public string Hash(string input) => BCrypt.Net.BCrypt.HashPassword(input);

    public bool Verify(string input, string hashed) => BCrypt.Net.BCrypt.Verify(input, hashed);
}