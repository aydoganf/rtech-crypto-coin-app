using System.Security.Cryptography;
using System.Text;

namespace RTech.CryptoCoin.Data;

public static class StringEncryptor
{
    public static string Encrypt(string plainText, string passPhrase = null, byte[] salt = null)
    {
        if (plainText == null)
        {
            return null;
        }

        passPhrase ??= "gsKnGZ041HLL4IM8";
        salt ??= Encoding.ASCII.GetBytes("hgt!16kl");

        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        using var password = new Rfc2898DeriveBytes(passPhrase, salt);
        var keyBytes = password.GetBytes(32);
        using var symmetricKey = Aes.Create();
        symmetricKey.Mode = CipherMode.CBC;
        using var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes("jkE49230Tf093b42"));
        
        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();
        var cipherTextBytes = memoryStream.ToArray();
        return Convert.ToBase64String(cipherTextBytes);
    }
}
