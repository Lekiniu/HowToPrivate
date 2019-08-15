using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;


namespace HowToWebApplication.Helpers
{
    public class PasswordHelper
    {
        public static class SHA
        {
            public static string GenerateSHA512String(string inputString)
            {
                SHA512 sha512 = SHA512.Create();
                byte[] bytes = Encoding.UTF8.GetBytes(inputString);
                byte[] hash = sha512.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}