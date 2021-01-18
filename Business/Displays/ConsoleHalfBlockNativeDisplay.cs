namespace Chip8.Business.Displays
{
    using System;
    using System.Linq;
    using static Chip8.Business.Native.NativeConsole;

    public class ConsoleHalfBlockNativeDisplay : IDisplayDriver, IDisposable
    {
        private readonly int width = Interpreter.DisplayWidth;

        private readonly int halfHeight = Interpreter.DisplayHeight / 2;

        private readonly short offsetTop;

        private readonly short offsetLeft;

        private readonly ConsoleDisplay consoleDisplay;

        // "█▀▄ "
        // "#^_ "
        // $"{this.drawIndicator} "
        // https://de.wikipedia.org/wiki/Unicodeblock_Blockelemente
        private readonly CharInfo[] chars = "█▀▄ ▛▜▙▟▌▐" //// "#^_ "
            .Select(c => new CharInfo { UnicodeChar = c, Attributes = 0x0000 })
            .ToArray();

        public ConsoleHalfBlockNativeDisplay(short offsetTop = 0, short offsetLeft = 0)
        {
            this.consoleDisplay = new ConsoleDisplay();

            this.consoleDisplay.DrawFrame(
                offsetLeft,
                offsetTop,
                this.width + 4,
                this.halfHeight + 2);

            // this.consoleDisplay.Flush();

            this.offsetTop = (short)(offsetTop + 1);
            this.offsetLeft = (short)(offsetLeft + 2);
        }

        public void Draw(Interpreter interpreter)
        {
            bool top;

            bool bottom;

            int x;

            int y;

            int srcLine;

            for (y = 0; y < this.halfHeight; y++)
            {
                for (x = 0; x < this.width; x++)
                {
                    srcLine = y * 2;
                    top = interpreter.Display[x, srcLine];
                    bottom = interpreter.Display[x, srcLine + 1];
                    var character = top
                        ? bottom
                            ? this.chars[0]
                            : this.chars[1]
                        : bottom
                            ? this.chars[2]
                            : this.chars[3];

                    this.Set(
                        y + this.offsetTop,
                        x + this.offsetLeft,
                        character);
                }
            }
            
            // this.consoleDisplay.Flush();
        }

        public void Dispose()
        {
            this.consoleDisplay.Dispose();
        }

        private void Set(int y, int x, CharInfo character)
        {
            this.consoleDisplay.Set(x, y, character);
        }
    }
}