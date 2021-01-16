namespace Chip8.Business
{
    public class Instruction_Fx07_LD_DT : Instruction
    {
        public Instruction_Fx07_LD_DT()
            : base(0xF007, 0xF0FF)
        {
        }

        /// <summary>
        /// Fx07 - LD Vx, DT
        /// Set Vx = delay timer value.
        /// The value of DT is placed into Vx.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            i.V[x] = i.DelayTimer;
            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"LD V{x:X1}, DT; V{x:X1} = {i.DelayTimer:X2};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"V{x:X1} := delay";
    }
}