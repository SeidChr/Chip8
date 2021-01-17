using System;
using System.Runtime.InteropServices;
using static Chip8.Business.Native.NativeConsole;

namespace Chip8.Business.Displays
{
    public class ConsoleDisplay : IDisposable
    {
        private readonly int width;

        private readonly int height;

        private readonly int pixelHeight;

        private readonly CharInfo[] buffer;

        private readonly CharInfo Space = GetCharInfo(' ');

        public ConsoleDisplay()
            : this(Console.WindowWidth, Console.WindowHeight)
        {
        }

        public ConsoleDisplay(int width, int height) 
        {
            this.width = width;
            this.height = height;

            this.pixelHeight = height * 2;

            buffer = new CharInfo[height * width];

            Array.Fill(buffer, this.Space);
        }

        public static CharInfo GetCharInfo(char character) 
            => new CharInfo { UnicodeChar = character, Attributes = 0x0000 };

        public void Set(int x, int y, char character)
            => this.Set(x, y, GetCharInfo(character));

        public void Set(int x, int y, CharInfo character)
            => this.buffer[y * this.width + x] = character;

        public void DrawCharFrame(int startX, int startY, int width, int height)
        {
            // "█▀▄ ▛▜▙▟"
            var endX = startX + width;
            var endY = startY + height;

            for (int y = startY; y < endY ; y++)
            {
                this.Set(startX, y, '█');
                this.Set(endX - 1, y, '█');
            }

            for (int x = startX + 1; x < endX - 1; x++)
            {
                this.Set(x, startY, '█');
                this.Set(x, endY - 1, '█');
            }

            // Corners
            // this.Set(startX, startY, '▛');
            // this.Set(startX, endY, '▙');
            // this.Set(endX, startY, '▜');
            // this.Set(endX, endY, '▟');
        }

        public void DrawPixelFrame(int startX, int startY, int width, int height)
        {

            // "█▀▄ ▛▜▙▟"
            var endX = startX + width;
            var endY = startY + height;

            for (int y = startY; y < endY; y++)
            {
                this.Set(startX, y, '█');
                this.Set(endX - 1, y, '█');
            }

            for (int x = startX + 1; x < endX - 1; x++)
            {
                this.Set(x, startY, '▀');
                this.Set(x, endY - 1, '▄');
            }

            // Corners
            // this.Set(startX, startY, '▛');
            // this.Set(startX, endY, '▙');
            // this.Set(endX, startY, '▜');
            // this.Set(endX, endY, '▟');
        }

        public void Flush() 
        {
            var handle = GetStdHandle(Channel.StdOutput);
            var size = new Coordinate { X = (short)this.width, Y = (short)this.height };
            var location = new Coordinate { X = 0, Y = 0 };
            var region = new Rectangle 
            { 
                Top = 0,
                Left = 0,
                Bottom = (short)this.height,
                Right = (short)this.width,
            };

            ConsoleDisplay.WriteConsoleOutput(handle, this.buffer, size, location, ref region);
        }

        public void Dispose()
        {
            // restore original console stuff
        }

        // http://pinvoke.net/default.aspx/kernel32/WriteConsoleOutput.html
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool WriteConsoleOutput(
            IntPtr consoleOutput,
            CharInfo[] buffer,
            Coordinate bufferSize,
            Coordinate bufferCoord,
            ref Rectangle targetRegion);
    }
}