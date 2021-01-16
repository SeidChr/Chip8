namespace Chip8.Business
{
    public class Instruction_Fx15_LD_DT : Instruction
    {
        public Instruction_Fx15_LD_DT()
            : base(0xF015, 0xF0FF)
        {
        }

        /// <summary>
        /// Fx15 - LD DT, Vx
        /// Set delay timer = Vx.
        /// DT is set equal to the value of Vx.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            i.DelayTimer = i.V[x];
            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
                => $"LD DT, V{x:X1}; DT = 0x{i.V[x]:X2} ({i.V[x]});;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"delay := V{x:X1}";
    }
}