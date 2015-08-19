using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Simon8029.EMPDemo.Utilities
{
    public static class StringExtension
    {
        public static bool IsSame(this string originalString, string compareString)
        {
            if (string.IsNullOrEmpty(originalString)||string.IsNullOrEmpty(compareString))
            {
                return false;
            }

            return string.Equals(originalString, compareString, StringComparison.CurrentCultureIgnoreCase);
        }

        public static string ToMD5(this string originalString)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(originalString));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte t in data)
            {
                stringBuilder.Append(t.ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public static string ToEncryptedString(this string originalString)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "encryptString", DateTime.Now,
                DateTime.Now.AddMinutes(60), true, originalString);
            return FormsAuthentication.Encrypt(ticket);
        }

        public static string ToDEcryptedString(this string originalString)
        {
            return FormsAuthentication.Decrypt(originalString).UserData;
            
        }

    }
}
