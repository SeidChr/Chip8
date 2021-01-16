namespace Chip8.Business.Instructions
{
    using System;

    public class Instruction_Dxyn_DRW : Instruction
    {
        public Instruction_Dxyn_DRW()
            : base(0xD000, 0xF000)
        {
        }

        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            // Dxyn - DRW Vx, Vy, nibble
            // Display n - byte sprite starting at memory location I at(Vx, Vy), set VF = collision.

            // The interpreter reads n bytes from memory, starting at the address stored in I.These bytes
            // are then displayed as sprites on screen at coordinates(Vx, Vy). Sprites are XORed onto the
            // existing screen.If this causes any pixels to be erased, VF is set to 1, otherwise it is set
            // to 0.If the sprite is positioned so part of it is outside the coordinates of the display,
            // it wraps around to the opposite side of the screen.See instruction 8xy3 for more information
            // on XOR, and section 2.4, Display, for more information on the Chip - 8 screen and sprites.

            // iterate all lines
            for (int seekY = 0; seekY < n; seekY++) 
            {
                var displayY = (i.V[y] + seekY) % Interpreter.DisplayHeight;
                var data = i.Memory[i.I + seekY];

                // iterate all columns
                for (int seekX = 0; seekX < 8; seekX++) 
                {
                    // data == single sprite byte in memory
                    // create a single-bit-active byte mask, then moving it right by seekX (1..8)
                    // seekX = 0: 10000000
                    // seekX = 6: 00000010
                    // AND it with the data from memory to get info about whether the single bit is active
                    if ((data & (0x80 >> seekX)) != 0) 
                    {
                        var displayX = (i.V[x] + seekX) % Interpreter.DisplayWidth;
                        var value = i.Display[displayX, displayY];
                        if (value) 
                        {
                            i.V[0xF] = 1;
                        }

                        i.Display[displayX, displayY] = !value;
                    }
                }
            }

            i.Draw();

            if (i.V[0xF] == 1) 
            {
                // Console.ReadKey(true);
            }

            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"DRW V{x:X1}, V{y:X1}; Draw {n} bytes at X = {i.V[x]}, Y = {i.V[y]};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"sprite V{x:X1} V{y:X1} {n}";
    }
}