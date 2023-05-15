using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Step.Utils.Cryptography;

public class Encryptor
{
    public static bool VerifyMd5Hash(string input, string hash)
    {
        // Hash the input.
        string hashOfInput = GetMd5Hash(input);

        // Create a StringComparer an compare the hashes.
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        switch (comparer.Compare(hashOfInput, hash))
        {
            case 0:
                return true;
            default:
                return false;
        }
    }

    public static string GetMd5Hash(string input)
    {
        using (MD5 md5Hash = MD5.Create())
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }

    public static string EncryptString(string text, string keyString)
    {
        var key = Encoding.UTF8.GetBytes(keyString);

        using (var aesAlg = Aes.Create())
        {
            using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
            {
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(text);
                    }

                    var iv = aesAlg.IV;

                    var decryptedContent = msEncrypt.ToArray();

                    var result = new byte[iv.Length + decryptedContent.Length];

                    Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                    Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                    return Convert.ToBase64String(result);
                }
            }
        }
    }

    public static string DecryptString(string cipherText, string keyString)
    {
        var iv = new byte[16];
        var cipher = new byte[16];

        var fullCipher = Convert.FromBase64String(cipherText);

        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
        var key = Encoding.UTF8.GetBytes(keyString);

        using (var aesAlg = Aes.Create())
        {
            using (var decryptor = aesAlg.CreateDecryptor(key, iv))
            {
                string result;
                using (var msDecrypt = new MemoryStream(cipher))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            result = srDecrypt.ReadToEnd();
                        }
                    }
                }

                return result;
            }
        }
    }

}

