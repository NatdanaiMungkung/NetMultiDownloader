using System;
using System.Collections.Generic;
using System.Text;

namespace NetMultiDownloader
{
    public class FileNameHelper
    {
        public static string GetDeDupFileName(Uri uri)
        {
            var filename = System.IO.Path.GetFileNameWithoutExtension(uri.LocalPath);
            var extension = System.IO.Path.GetExtension(uri.LocalPath);
            return $"{filename}-{CreateMD5(uri.ToString())}{extension}";
        }

        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().Substring(0,6);
            }
        }
    }
}
