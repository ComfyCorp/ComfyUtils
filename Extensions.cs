using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace ComfyUtils
{
    public static class Extensions
    {
        public static string Last(this string[] str, int extra = 0) { return str[str.Length - 1 - extra]; }
        public static char Last(this char[] chararray, int extra = 0) { return chararray[chararray.Length - 1 - extra]; }
        public static int Occur(this string str, string searchterm)
        {
            int i = 0;
            do
            {
                int index = str.IndexOf(searchterm);
                str = str.Remove(index, searchterm.Length);
                i++;
            }
            while (str.Contains(searchterm));
            return i;
        }
        public static string[] MultiSubstring(this string str, string searchterm, int length)
        {
            List<string> occurrences = new List<string>();
            do
            {
                int index = str.IndexOf(searchterm);
                occurrences.Add(str.Substring(index, length));
                str = str.Remove(index, length);
            }
            while (str.Contains(searchterm));
            return occurrences.ToArray();
        }
        public static string Reverse(this string str)
        {
            char[] chararray = str.ToCharArray();
            Array.Reverse(chararray);
            string reverse = string.Empty;
            foreach (char ch in chararray) { reverse += ch; }
            return reverse;
        }
        public static string Build(this char[] chararray)
        {
            string str = string.Empty;
            foreach (char ch in chararray) { str += ch; }
            return str;
        }
        public static string ToBase64(this string str) { return Convert.ToBase64String(Encoding.UTF8.GetBytes(str)); }
        public static string ToBase64(this byte[] bytearray) { return Convert.ToBase64String(bytearray); }
        public static string FromBase64(this string str) { return Encoding.UTF8.GetString(Convert.FromBase64String(str)); }
        public static byte[] FromBase64ToBytes(this string str) { return (Convert.FromBase64String(str)); }
        public static int ToInt32(this string str) { return Convert.ToInt32(str); }
        public static bool IsEmpty(this string str) { return string.IsNullOrEmpty(str); }
    }
}
