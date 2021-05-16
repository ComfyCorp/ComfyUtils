using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace ComfyUtils
{
    public class Log
    {
        public static string Time() { return DateTime.Now.ToString("HH':'mm':'ss.fff"); }
        public static void Msg(string txt, bool newline = true, bool center = false)
        {
            Console.ResetColor();
            if (center) { Console.SetCursorPosition((Console.WindowWidth - txt.Length) / 2, Console.CursorTop); }
            if (newline) { Console.WriteLine(txt); }
            else Console.Write(txt);
        }
        public static void Msg(string txt, ConsoleColor color, bool newline = true, bool center = false)
        {
            Console.ForegroundColor = color;
            if (center) { Console.SetCursorPosition((Console.WindowWidth - txt.Length) / 2, Console.CursorTop); }
            if (newline) { Console.WriteLine(txt); }
            else Console.Write(txt);
            Console.ResetColor();
        }
        public static void Msg(string[] txt, bool center = false)
        {
            Console.ResetColor();
            if (center) { Console.SetCursorPosition((Console.WindowWidth - txt.Length) / 2, Console.CursorTop); }
            foreach (string str in txt) { Console.WriteLine(str); }
        }
        public static void Msg(string[] txt, ConsoleColor color, bool center = false)
        {
            Console.ForegroundColor = color;
            if (center) { Console.SetCursorPosition((Console.WindowWidth - txt.Length) / 2, Console.CursorTop); }
            foreach (string str in txt) { Console.WriteLine(str); }
            Console.ResetColor();
        }
        public static async void SlowType(string txt)
        {
            foreach (char ch in txt) { Console.Write(ch); await Task.Delay(100); }
            Console.WriteLine();
        }
        public static async void SlowType(string[] txt)
        {
            foreach (string str in txt) { foreach (char ch in str) { Console.Write(ch); await Task.Delay(100); } await Task.Delay(500); Console.WriteLine(); }
        }
        public static string Input(string txt = "", bool center = false)
        {
            Console.ResetColor();
            if (center) { Console.SetCursorPosition((Console.WindowWidth - (txt.Length + 2)) / 2, Console.CursorTop); }
            Console.Write($"{txt}> ");
            return Console.ReadLine();
        }
        public static string Input(string txt, ConsoleColor color, bool center = false)
        {
            Console.ForegroundColor = color;
            if (center) { Console.SetCursorPosition((Console.WindowWidth - (txt.Length + 2)) / 2, Console.CursorTop); }
            Console.Write($"{txt}> ");
            Console.ResetColor();
            return Console.ReadLine();
        }
        public static ConsoleKeyInfo KeyInput(string txt = "", bool silent = true, bool center = false)
        {
            Console.ResetColor();
            if (center) { Console.SetCursorPosition((Console.WindowWidth - txt.Length) / 2, Console.CursorTop); }
            Console.Write($"{txt}");
            ConsoleKeyInfo Response = Console.ReadKey(silent);
            Console.WriteLine();
            return Response;
        }
        public static string MaskedInput(string txt = "", char mask = '*', bool center = false)
        {
            if (center) { Console.SetCursorPosition((Console.WindowWidth - txt.Length) / 2, Console.CursorTop); }
            string output = string.Empty;
            ConsoleKeyInfo input;
            Console.Write($"{txt}>");
            do
            {
                input = Console.ReadKey(true);
                if (input.Key == ConsoleKey.Backspace && output.Length > 0)
                { Console.Write("\b \b"); output = output.Remove(output.Length - 1); }
                else if (!char.IsControl(input.KeyChar)) { Console.Write(mask); output += input.KeyChar; }
            }
            while (input.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return output;
        }
        public static void Pause() { Console.ReadKey(true); }
        public static void Clear() { Console.Clear(); Console.ResetColor(); }
        public static void Newline(int lines = 1) { for (int i = 0; i < lines; i++) { Console.WriteLine(); } }
        public static void Info(string txt, bool center = false)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (center) { Console.SetCursorPosition((Console.WindowWidth - txt.Length) / 2, Console.CursorTop); }
            Console.WriteLine($"[{DateTime.Now:HH':'mm':'ss.fff}] [Info] {txt}");
            Console.ResetColor();
        }
        public static void Debug(string txt, [CallerLineNumber] int line = 0, bool center = false)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            if (center) { Console.SetCursorPosition((Console.WindowWidth - txt.Length) / 2, Console.CursorTop); }
            Console.WriteLine($"[Ln|{line}] [Debug] {txt}");
            Console.ResetColor();
        }
        public static void Warning(string txt, bool center = false)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (center) { Console.SetCursorPosition((Console.WindowWidth - txt.Length) / 2, Console.CursorTop); }
            Console.WriteLine($"[{DateTime.Now:HH':'mm':'ss.fff}] [Warning] {txt}");
            Console.ResetColor();
        }
        public static void Error(string txt, bool center = false)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (center) { Console.SetCursorPosition((Console.WindowWidth - txt.Length) / 2, Console.CursorTop); }
            Console.WriteLine($"[{DateTime.Now:HH':'mm':'ss.fff}] [Error] {txt}");
            Console.ResetColor();
            Console.ReadKey(true);
        }
        public static void Error(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{DateTime.Now:HH':'mm':'ss.fff}] [Error] {ex.Message}");
            Console.ResetColor();
            Console.ReadKey(true);
        }
        public static void ErrorStackTrace(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{DateTime.Now:HH':'mm':'ss.fff}] [Error] {ex.Message}\r\n[StackTrace]\r\n{ex.StackTrace}");
            Console.ResetColor();
            Console.ReadKey(true);
        }
    }
}
