namespace Chip8.Business
{
    public class Instruction_8xyE_SHL : Instruction
    {
        public Instruction_8xyE_SHL()
            : base(0x800E, 0xF00F)
        {
        }

        /// <summary>
        /// 8xyE - SHL Vx {, Vy }
        /// Set Vx = Vx SHL 1.
        /// If the most-significant bit of Vx is 1, then VF is set to 1, otherwise to 0. Then Vx is multiplied by 2.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            i.V[0xF] = (i.V[x] & 0b1000000) > 0 ? 1 : 0;
            i.V[x] <<= 1;

            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"SHL V{x:X1}; V{x:X1} = 0x{x << 1:X2} ({x << 1});;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"V{x:X1} << 1";
    }
}