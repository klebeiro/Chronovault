using System;
using System.Security.Cryptography;

public static class PasswordHasher
{
    private const int SaltSize = 128 / 8; 
    private const int KeySize = 256 / 8; 
    private const int Iterations = 10000;
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

    public static (byte[] hash, byte[] salt) Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            Iterations,
            Algorithm,
            KeySize);
        return (hash, salt);
    }

    public static bool Verify(string password, byte[] hash, byte[] salt)
    {
        byte[] computedHash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            Iterations,
            Algorithm,
            KeySize);
        return CryptographicOperations.FixedTimeEquals(computedHash, hash);
    }
}