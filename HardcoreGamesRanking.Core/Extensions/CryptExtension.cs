namespace HardcoreGamesRanking.Core.Extensions
{
    using System.Security.Cryptography;
    using System.Text;

    public static class CryptExtension
    {
        private const string Key = "jkzvXH2zVeZIszTUrgHoCLXFDLiscpGh";

        public static string EncryptString(this string baseText)
        {
            byte[] key = Encoding.UTF8.GetBytes(Key);
            using Aes aes = Aes.Create();
            using ICryptoTransform cryptoTransform = aes.CreateEncryptor(key, aes.IV);
            using MemoryStream memoryStream = new();
            using (CryptoStream cryptoStream = new(memoryStream, cryptoTransform, CryptoStreamMode.Write))
            using (StreamWriter streamWriter = new(cryptoStream))
            {
                streamWriter.Write(baseText);
            }
            byte[] iv = aes.IV;
            byte[] decryptedContent = memoryStream.ToArray();
            byte[] result = new byte[iv.Length + decryptedContent.Length];
            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
            Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);
            return Convert.ToBase64String(result);
        }

        public static string DecryptString(this string baseText)
        {
            byte[] fullCipher = Convert.FromBase64String(baseText);
            byte[] iv = new byte[16];
            byte[] cipher = new byte[fullCipher.Length - iv.Length];
            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);
            byte[] key = Encoding.UTF8.GetBytes(Key);
            using Aes aes = Aes.Create();
            using ICryptoTransform cryptoTransform = aes.CreateDecryptor(key, iv);
            string result;
            using (MemoryStream memoryStream = new(cipher))
            {
                using CryptoStream cryptoStream = new(memoryStream, cryptoTransform, CryptoStreamMode.Read);
                using StreamReader streamReader = new(cryptoStream);
                result = streamReader.ReadToEnd();
            }
            return result;
        }
    }
}