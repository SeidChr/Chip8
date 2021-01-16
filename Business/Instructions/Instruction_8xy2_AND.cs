namespace Chip8.Business
{
    public class Instruction_8xy2_AND : Instruction
    {
        public Instruction_8xy2_AND()
            : base(0x8002, 0xF00F)
        {
        }

        /// <summary>
        /// 8xy4 - ADD Vx, Vy
        /// Set Vx = Vx + Vy, set VF = carry.
        /// The values of Vx and Vy are added together.If the result is greater than 8
        /// bits(i.e., > 255,) VF is set to 1, otherwise 0. Only the lowest 8 bits of
        /// the result are kept, and stored in Vx.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            int val = i.V[x];
            val &= i.V[y];
            i.V[0xF] = val > 255 ? 1 : 0;
            i.V[x] = (byte)val;
            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"AND V{x:X1}, V{y:X1}; V{x:X1} = {i.V[x]:X2} & {i.V[y]:X2} = {i.V[x] & i.V[y]:X2};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"V{x:X1} &= V{y:X1}";
    }
}