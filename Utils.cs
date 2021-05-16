using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Reflection;

namespace ComfyUtils
{
    
    public class Utils
    {
        public class RegexList
        {
            public class Net
            {
                public static Regex IP = new Regex("[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}");
                public static Regex Port = new Regex("[0-9]{1,5}");
                public static Regex IP_Port = new Regex("[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\:[0-9]{1,5}");
            }
            public class VRChat
            {
                public static Regex Avatar = new Regex("avtr_[0-9a-fA-F]{8}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{12}");
                public static Regex World = new Regex("wrld_[0-9a-fA-F]{8}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{12}");
                public static Regex User = new Regex("usr_[0-9a-fA-F]{8}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{12}");
                public static Regex AuthCookie = new Regex("authcookie_[0-9a-fA-F]{8}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{12}");
            }
            public class Discord
            {
                public static Regex Token = new Regex("[\\w-]{24}\\.[\\w-]{6}\\.[\\w-]{27}");
                public static Regex Discriminator = new Regex("\\#[0-9]{4}");
            }
        }
        public static string AlphaLC = "abcdefghijklmnopqrstuvwxyz";
        public static string AlphaUC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string Nums = "0123456789";
        public static string CD() { return Environment.CurrentDirectory; }
        public static string AppData() { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); }
        public static int SecsToMs(int seconds) { return seconds * 1000; }
        public static int MinsToMs(int minutes) { return minutes * 1000 * 60; }
        public static int HoursToMs(int hours) { return hours * 1000 * 60 * 60; }
        public static bool Between(int number, int min, int max) { return number > min && number < max; }
        public static bool BetweenOrEqual(int number, int min, int max) { return number >= min && number <= max; }
        public static string ToBinary(string str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in str.ToCharArray())
            {
                stringBuilder.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return stringBuilder.ToString();
        }
        public static string FromBinary(string str)
        {
            List<Byte> byteList = new List<Byte>();
            for (int i = 0; i < str.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(str.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }
        public static string ToBase64(string str) { return Convert.ToBase64String(Encoding.UTF8.GetBytes(str)); }
        public static string ToBase64(byte[] bytearray) { return Convert.ToBase64String(bytearray); }
        public static string FromBase64(string str) { return Encoding.UTF8.GetString(Convert.FromBase64String(str)); }
        public static byte[] FromBase64ToBytes(string str) { return Convert.FromBase64String(str); }
        public static string GetSHA256(string str)
        {
            HashAlgorithm hashAlgorithm = new SHA256Managed();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(str)))
            {
                stringBuilder.Append(b.ToString("x2"));
            }
            return stringBuilder.ToString();
        }
        public static string GetSHA256(byte[] b)
        {
            HashAlgorithm hashAlgorithm = new SHA256Managed();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte by in hashAlgorithm.ComputeHash(b))
            {
                stringBuilder.Append(by.ToString("x2"));
            }
            return stringBuilder.ToString();
        }
        public static string RandomString(string chars, int length)
        {
            string result = string.Empty;
            Random rand = new Random();
            for (int i = 0; i < length; i++) { result += chars.ToCharArray()[rand.Next(chars.Length)]; }
            return result;
        }
        public static void DeleteDir(string directory)
        {
            if (Directory.Exists(directory))
            {
                foreach (string file in Directory.GetFiles(directory)) { File.Delete(file); }
                Directory.Delete(directory);
            }
        }
        public static void Restart()
        {
            Process.Start(Process.GetCurrentProcess().ProcessName);
            Process.GetCurrentProcess().Kill();
        }
        public static void AskRestart()
        {
        Start:
            Console.ResetColor();
            Console.WriteLine("Restart Program? [Y|N]");
            ConsoleKeyInfo Response = Console.ReadKey();
            if (Response.Key == ConsoleKey.Y)
            {
                Process.Start(Process.GetCurrentProcess().ProcessName);
                Process.GetCurrentProcess().Kill();
            }
            else if (Response.Key == ConsoleKey.N) { }
            else
            {
                Console.WriteLine("\nInvalid Key");
                goto Start;
            }
        }
        public static MethodInfo GetMethodInfo(string methodname)
        {
            Assembly assembly = Assembly.Load(AssemblyName.GetAssemblyName(Assembly.GetEntryAssembly().ManifestModule.Name));
            foreach (Type type in assembly.GetTypes())
            { foreach (MethodInfo method in type.GetMethods()) { if (method.Name == methodname) { return method; } } }
            return null;
        }
        public static void CallMethod(MethodInfo method)
        {
            object obj = Activator.CreateInstance(method.ReflectedType);
            method.Invoke(obj, null);
        }
    }
}