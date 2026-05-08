using System.Security.Cryptography;
using UniversityHistory.Application.Interfaces.Auth;

namespace UniversityHistory.Infrastructure.Identity;

public class PasswordGenerator : IPasswordGenerator
{
    private const string AllowedChars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789!@#$%";

    public string Generate(int length = 14)
    {
        if (length < 8)
            throw new ArgumentOutOfRangeException(nameof(length), "Password length must be at least 8.");

        var chars = new char[length];
        for (var i = 0; i < chars.Length; i++)
        {
            chars[i] = AllowedChars[RandomNumberGenerator.GetInt32(AllowedChars.Length)];
        }

        return new string(chars);
    }
}
