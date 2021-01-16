namespace Chip8.Business
{
    public class Instruction_Fx33_LD : Instruction
    {
        public Instruction_Fx33_LD()
            : base(0xF033, 0xF0FF)
        {
        }

        /// <summary>
        /// Fx33 - LD B, Vx
        /// Store BCD representation of Vx in memory locations I, I+1, and I+2.
        /// The interpreter takes the decimal value of Vx, and places the hundreds digit in memory
        /// at location in I, the tens digit at location I+1, and the ones digit at location I+2.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            // byte bcd_value = read_from_somewhere();      // lets say bcd_value = 55, or 0x37
            // byte tens = bcd_value >> 4;                  // tens = 3 (by shifting down the number four places)
            // byte units = bcd_value & 0x0F;               // units = 7 (the 0x0F filters-out the high digit)
            // byte final_value = (tens * 10) + units;      // final_value = 37

            // byte orig_value = 35;                 // original value we want to encode in BCD
            // byte tens = orig_value / 10;          // tens = 3 (by integer division)
            // byte units = orig_value % 10;         // units = 5 (by modulus)
            // byte bcd_value = (tens << 4) | units; // bcd_value = 53, or 0x35
            //                                          (by shifting the tens up four
            //                                          places and binary-OR'ing it
            //                                          with the units)
            i.Memory[i.I + 2] = (byte)(i.V[x] % 10);
            i.Memory[i.I + 1] = (byte)(i.Memory[i.I + 2] % 10);
            i.Memory[i.I] = (byte)(i.Memory[i.I + 1] % 10);
            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"LD B, V{x:X1}";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"memory[i] := bcd V{x:X1}";
    }
}