namespace Chip8.Business.Instructions
{
    public class Instruction_8xy0_LD : Instruction
    {
        public Instruction_8xy0_LD()
            : base(0x8000, 0xF00F)
        {
        }

        /// <summary>
        /// 8xy0 - LD Vx, Vy
        /// Set Vx = Vy.
        /// Stores the value of register Vy in register Vx.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            i.V[x] = i.V[y];
            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"LD V{x:X1}, V{y:X1}; V{x:X1}={i.V[y]:X2};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"V{x:X1} := V{y:X1}";
    }
}