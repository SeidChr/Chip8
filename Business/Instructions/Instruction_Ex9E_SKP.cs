namespace Chip8.Business
{
    public class Instruction_Ex9E_SKP : Instruction
    {
        public Instruction_Ex9E_SKP()
            : base(0xE09E, 0xF0FF)
        {
        }

        /// <summary>
        /// Ex9E - SKP Vx
        /// Skip next instruction if key with the value of Vx is pressed.
        /// Checks the keyboard, and if the key corresponding to the value of Vx is currently in the down position, PC is increased by 2.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            if (Keyboard.IsKeyDown(i.V[x] & 0x0F))
            {
                i.Step();
            }

            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"SKP V{x:X1}; Skip if key {i.V[x]:X1} is pressed;;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"if not V{x:X1} key then";
    }
}