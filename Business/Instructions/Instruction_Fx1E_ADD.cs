namespace Chip8.Business
{
    public class Instruction_Fx1E_ADD : Instruction
    {
        public Instruction_Fx1E_ADD()
            : base(0xF01E, 0xF0FF)
        {
        }

        /// <summary>
        /// Fx1E - ADD I, Vx
        /// Set I = I + Vx.
        /// The values of I and Vx are added, and the results are stored in I.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            i.I += i.V[x];
            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"ADD I, V{x:X1}; I = 0x{i.I:X3} + 0x{i.V[x]:X2} = {i.I + i.V[x]:X2};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"i += V{x:X1}";
    }
}