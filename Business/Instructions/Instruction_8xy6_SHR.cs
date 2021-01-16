namespace Chip8.Business
{
    public class Instruction_8xy6_SHR : Instruction
    {
        public Instruction_8xy6_SHR()
            : base(0x8006, 0xF00F)
        {
        }

        /// <summary>
        /// 8xy6 - SHR Vx {, Vy }
        /// Set Vx = Vx SHR 1.
        /// If the least-significant bit of Vx is 1, then VF is set to 1, otherwise 0. Then Vx is divided by 2.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            i.V[0xF] = (byte)(i.V[x] & 1);
            i.V[x] >>= 1;

            //// i.V[0xF] = (byte)(i.V[y] & 1);
            //// i.V[y] >>= 1;

            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"SHR V{x:X1}; V{x:X1} = 0x{x >> 1:X2} ({x >> 1}) ;;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"V{x:X1} >> 1";
    }
}