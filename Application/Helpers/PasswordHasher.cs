using System.Security.Cryptography;
using System.Text;

public static class PasswordHasher
{
    public static string Hash(string password)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hashBytes);
    }

    public static bool Verify(string password, string storedHash)
    {
        return Hash(password) == storedHash;
    }
}
