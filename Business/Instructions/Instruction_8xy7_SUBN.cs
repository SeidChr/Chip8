namespace Chip8.Business
{
    public class Instruction_8xy7_SUBN : Instruction
    {
        public Instruction_8xy7_SUBN()
            : base(0x8007, 0xF00F)
        {
        }

        /// <summary>
        /// 8xy7 - SUBN Vx, Vy
        /// Set Vx = Vy - Vx, set VF = NOT borrow.
        /// If Vy > Vx, then VF is set to 1, otherwise 0. Then Vx is subtracted from Vy, and the results stored in Vx.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            var vx = i.V[x];
            var vy = i.V[y];
            i.V[0xF] = vy > vx ? 1 : 0;
            i.V[x] = (byte)(vy - vx);

            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"SUBN V{x:X1}, V{y:X1}; V{x:X1} = {i.V[x]:X2} - {i.V[y]:X2} = {i.V[x] - i.V[y]:X2};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"V{x:X1} -= V{y:X1} (SUBN)";
    }
}