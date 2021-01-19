namespace Chip8.Business
{
    using System;

    public class Keyboard
    {
        public static ConsoleKey[] AvailableKeys { get; } = new[]
        {
            ConsoleKey.NumPad0,

            ConsoleKey.NumPad7,
            ConsoleKey.NumPad8,
            ConsoleKey.NumPad9,

            ConsoleKey.NumPad4,
            ConsoleKey.NumPad5,
            ConsoleKey.NumPad6,

            ConsoleKey.NumPad1,
            ConsoleKey.NumPad2,
            ConsoleKey.NumPad3,

            ConsoleKey.A,
            ConsoleKey.B,
            ConsoleKey.C,
            ConsoleKey.D,
            ConsoleKey.E,
            ConsoleKey.F,
        };

        public static ConsoleKey WaitForConsoleKey() 
            => AvailableKeys[WaitForKey()];

        public static byte WaitForKey() 
        {
            while (true)
            {
                var key = Console.ReadKey(true);
                var index = Array.IndexOf(AvailableKeys, key.Key);
                if (index >= 0) 
                {
                    return (byte)index;
                }
            }
        }

        public static bool IsKeyDown(ConsoleKey key)
        {
            return Native.NativeKeyboard.IsKeyDown(key);
        }

        public static bool IsKeyDown(int key)
        {
            return Native.NativeKeyboard.IsKeyDown(AvailableKeys[key]);
        }
    }
}