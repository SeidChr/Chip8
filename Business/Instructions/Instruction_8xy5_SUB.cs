namespace Chip8.Business
{
    public class Instruction_8xy5_SUB : Instruction
    {
        public Instruction_8xy5_SUB()
            : base(0x8005, 0xF00F)
        {
        }

        /// <summary>
        /// 8xy5 - SUB Vx, Vy
        /// Set Vx = Vx - Vy, set VF = NOT borrow.
        /// If Vx > Vy, then VF is set to 1, otherwise 0. Then Vy is subtracted from Vx, and the results stored in Vx.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            var vx = i.V[x];
            var vy = i.V[y];
            i.V[0xF] = vx > vy ? 1 : 0;
            i.V[x] -= vy;
            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"SUB V{x:X1}, V{y:X1}; V{x:X1} = {i.V[x]:X2} - {i.V[y]:X2} = {i.V[x] - i.V[y]:X2};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"V{x:X1} -= V{y:X1}";
    }
}