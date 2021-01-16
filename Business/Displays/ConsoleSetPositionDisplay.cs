namespace Chip8.Business.Displays
{
    using System;

    public class ConsoleSetPositionDisplay : IDisplayDriver
    {
        public void Draw(Interpreter interpreter)
        {
            Console.CursorVisible = false;
            var line = new char[Interpreter.DisplayWidth];
            for (int y = 0; y < Interpreter.DisplayHeight; y++)
            {
                for (int x = 0; x < Interpreter.DisplayWidth; x++)
                {
                    line[x] = interpreter.Display[x, y] ? '#' : ' ';
                }

                Console.SetCursorPosition(0, y);
                Console.Write(line);
            }

            Console.CursorVisible = true;
        }
    }
}