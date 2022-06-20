using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Core.Security
{
    internal class AES32SymmetricCipher
    {
        private readonly byte[] _key;

        internal AES32SymmetricCipher(string key)
        {
            _key = Encoding.Default.GetBytes(key);
            _key = ExtendKey();
        }
        internal AES32SymmetricCipher(byte[] bytes)
        {
            _key = bytes;
            _key = ExtendKey();
        }

        private byte[] ExtendKey()
        {
            byte[] result = new byte[32];
            _key.CopyTo(result, 0);
            return result;
        }
        public byte[] Encode(string data)
        {
            byte[] encrypted;
            byte[] IV;

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = _key;

                aes.GenerateIV();
                IV = aes.IV;

                aes.Mode = CipherMode.CBC;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(data);
                        }
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            var combinedIvCt = new byte[IV.Length + encrypted.Length];
            Array.Copy(IV, 0, combinedIvCt, 0, IV.Length);
            Array.Copy(encrypted, 0, combinedIvCt, IV.Length, encrypted.Length);
            return combinedIvCt;
        }
        public string Decode(byte[] data)
        {
            string plaintext = null;
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = _key;

                byte[] IV = new byte[aes.BlockSize / 8];  // Генерируем СТАТИЧЕСКУЮ соль
                byte[] cipherText = new byte[data.Length - IV.Length];

                Array.Copy(data, IV, IV.Length);
                Array.Copy(data, IV.Length, cipherText, 0, cipherText.Length);

                aes.IV = IV;
                aes.Mode = CipherMode.CBC;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}
