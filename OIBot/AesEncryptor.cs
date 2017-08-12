using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace OIBot
{
    /// <summary>
    /// WARNING: THIS IS PROBABLY NOT SECURE. DO NOT USE THIS FOR SECURITY CRITICAL APPLICATIONS.
    /// </summary>
    public class AesEncryptor : IDisposable
    {
        private Aes _aes;

        public AesEncryptor(byte[] key = null, byte[] iv = null)
        {
            _aes = Aes.Create();
            if (key != null)
                _aes.Key = key;
            if (iv != null)
                _aes.IV = iv;
            _aes.Padding = PaddingMode.Zeros;
        }

        public AesEncryptor(string password, string salt)
        {
            _aes = Aes.Create();
            var key = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt));
            _aes.Key = key.GetBytes(_aes.KeySize / 8);
            _aes.IV = key.GetBytes(_aes.BlockSize / 8);

        }

        public string Encrypt(string str)
        {
            var arr = EncryptToBytes(str);
            return Convert.ToBase64String(arr);
        }

        public string Decrypt(string str)
        {
            var arr = Convert.FromBase64String(str);
            return DecryptFromBytes(arr);
        }

        public byte[] EncryptToBytes(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            var encryptor = _aes.CreateEncryptor(_aes.Key, _aes.IV);

            using (var ms = new MemoryStream())
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(str);
                sw.Flush();
                cs.Flush();
                cs.FlushFinalBlock();
                return ms.ToArray();
            }
        }

        public string DecryptFromBytes(byte[] data)
        {
            if (data?.Length == 0)
                return null;

            var decryptor = _aes.CreateDecryptor(_aes.Key, _aes.IV);

            using (var ms = new MemoryStream(data))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _aes?.Dispose();
        }
    }
}
