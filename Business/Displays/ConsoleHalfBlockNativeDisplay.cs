namespace Chip8.Business.Displays
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.InteropServices;
    using static Chip8.Business.Native.NativeConsole;

    public class ConsoleHalfBlockNativeDisplay : IDisplayDriver
    {
        private readonly CharInfo[] buffer
            = new CharInfo[Interpreter.DisplayHeight + 2 * Interpreter.DisplayWidth + 2];

        private readonly Coordinate bufferSize = new ()
        {
            X = Interpreter.DisplayWidth + 2,
            Y = Interpreter.DisplayHeight + 2,
        };

        private readonly Coordinate bufferCoord = new ()
        {
            X = 0,
            Y = 0,
        };

        private readonly int halfHeight = Interpreter.DisplayHeight / 2;

        private readonly short offsetTop;

        private readonly short offsetLeft;

        // "█▀▄ "
        // "#^_ "
        // $"{this.drawIndicator} "
        // https://de.wikipedia.org/wiki/Unicodeblock_Blockelemente
        private readonly CharInfo[] chars = "█▀▄ ▛▜▙▟▌▐" //// "#^_ "
            .Select(c => new CharInfo { UnicodeChar = c, Attributes = 0x0000 })
            .ToArray();

        public ConsoleHalfBlockNativeDisplay(short offsetTop = 0, short offsetLeft = 0)
        {
            this.offsetTop = offsetTop;
            this.offsetLeft = offsetLeft;

            for (int y = 1; y < this.halfHeight + 1; y++)
            {
                this.Set(y, 0, this.chars[8]);
                this.Set(y, Interpreter.DisplayWidth + 1, this.chars[9]);
            }

            for (int x = 1; x < Interpreter.DisplayWidth + 1; x++)
            {
                this.Set(0, x, this.chars[1]);
                this.Set(this.halfHeight + 1, x, this.chars[2]);
            }

            this.Set(0, 0, this.chars[4]);
            this.Set(this.halfHeight + 1, 0, this.chars[6]);
            this.Set(0, Interpreter.DisplayWidth + 1, this.chars[5]);
            this.Set(this.halfHeight + 1, Interpreter.DisplayWidth + 1, this.chars[7]);
        }

        private void Set(int y, int x, CharInfo character) 
        {
            this.buffer[Interpreter.DisplayWidth * y + x] = character;
        }

        public void Draw(Interpreter interpreter)
        {
            var handle = GetStdHandle(Channel.StdOutput);

            Rectangle targetRegion = new ()
            {
                Top = this.offsetTop,
                Left = this.offsetLeft,
                Bottom = (short)(Interpreter.DisplayHeight + this.offsetTop + 2),
                Right = (short)(Interpreter.DisplayWidth + this.offsetLeft + 2),
            };

            bool top;

            bool bottom;

            int x;

            int y;

            int srcLine;

            for (y = 0; y < this.halfHeight; y++)
            {
                for (x = 0; x < Interpreter.DisplayWidth; x++)
                {
                    srcLine = y * 2;
                    top = interpreter.Display[x, srcLine];
                    bottom = interpreter.Display[x, srcLine + 1];
                    this.Set(y + 1, x + 1, top
                        ? bottom
                            ? this.chars[0]
                            : this.chars[1]
                        : bottom
                            ? this.chars[2]
                            : this.chars[3]);
                }
            }

            // for (y = 0; y < Interpreter.DisplayHeight; y++)
            // {
            //     for (x = 0; x < Interpreter.DisplayWidth; x++)
            //     {
            //         this.Set(y, x] = interpreter.Display[x, y]
            //             ? chars[0]
            //             : chars[1];
            //     }
            // }

            ////Console.ReadKey(true);
            var success = WriteConsoleOutput(
                handle,
                this.buffer,
                this.bufferSize,
                this.bufferCoord,
                ref targetRegion);

            ////Console.ReadKey(true);
            if (!success)
            {
                int errcode = Marshal.GetLastWin32Error();
                string errorMessage = new Win32Exception(errcode).Message;
                Console.WriteLine(errcode + ": " + errorMessage);
                ////Console.ReadLine();
            }
        }
    }
}