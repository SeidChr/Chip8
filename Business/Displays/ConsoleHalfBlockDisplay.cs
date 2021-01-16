namespace Chip8.Business.Displays
{
    using System;

    public class ConsoleHalfBlockDisplay : IDisplayDriver
    {
        public void Draw(Interpreter interpreter)
        {
            // https://de.wikipedia.org/wiki/Unicodeblock_Blockelemente
            // unicode
            // halbblock oben = 2580
            // halbblock unten = 2584
            var line = new char[Interpreter.DisplayWidth];
            var chars = "█▀▄ ".ToCharArray();
            bool top;
            bool bottom;
            int x;
            int y;
            int srcLine;

            Console.CursorVisible = false;

            for (y = 0; y < Interpreter.DisplayHeight / 2; y++)
            {
                for (x = 0; x < Interpreter.DisplayWidth; x++)
                {
                    srcLine = y * 2;
                    top = interpreter.Display[x, srcLine];
                    bottom = interpreter.Display[x, srcLine + 1];
                    line[x] = top
                        ? bottom
                            ? chars[0]
                            : chars[1]
                        : bottom
                            ? chars[2]
                            : chars[3];
                }

                Console.SetCursorPosition(0, y);
                Console.Write(line);
            }

            Console.ReadKey(true);
            Console.CursorVisible = true;
        }
    }
}