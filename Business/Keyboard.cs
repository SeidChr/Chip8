namespace Chip8.Business
{
    using System;

    public class Keyboard
    {
        public static ConsoleKey[] AvailableKeys { get; } = new[]
        {
            ConsoleKey.D0,
            ConsoleKey.D1,
            ConsoleKey.D2,
            ConsoleKey.D3,
            ConsoleKey.D4,
            ConsoleKey.D5,
            ConsoleKey.D6,
            ConsoleKey.D7,
            ConsoleKey.D8,
            ConsoleKey.D9,
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