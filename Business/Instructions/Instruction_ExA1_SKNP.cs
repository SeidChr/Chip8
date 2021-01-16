namespace Chip8.Business
{
    public class Instruction_ExA1_SKNP : Instruction
    {
        public Instruction_ExA1_SKNP()
            : base(0xE0A1, 0xF0FF)
        {
        }

        /// <summary>
        /// ExA1 - SKNP Vx
        /// Skip next instruction if key with the value of Vx is not pressed.
        /// Checks the keyboard, and if the key corresponding to the value of Vx is currently in the up position, PC is increased by 2.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            if (!Keyboard.IsKeyDown(i.V[x] & 0x0F))
            {
                i.Step();
            }

            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"SKNP V{x:X1}; Skip if key {i.V[x]:X1} not pressed;;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"if V{x:X1} key then";
    }
}