namespace Chip8.Business
{
    public class Instruction_Fx0A_LD : Instruction
    {
        public Instruction_Fx0A_LD()
            : base(0xF00A, 0xF0FF)
        {
        }

        /// <summary>
        /// Fx0A - LD Vx, K
        /// Wait for a key press, store the value of the key in Vx.
        /// All execution stops until a key is pressed, then the value of that key is stored in Vx.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            i.V[x] = Keyboard.WaitForKey();
            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"LD V{x:X1}, K; Load key into V{x:X1};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"V{x:X1} := key";
    }
}