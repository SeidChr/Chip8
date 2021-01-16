namespace Chip8.Business
{
    public class Instruction_7xkk_ADD : Instruction
    {
        public Instruction_7xkk_ADD()
            : base(0x7000, 0xF000)
        {
        }

        /// <summary>
        /// 7xkk - ADD Vx, byte
        /// Set Vx = Vx + kk.
        /// Adds the value kk to the value of register Vx, then stores the result in Vx.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            i.V[x] += kk;
            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"ADD V{x:X1}, 0x{kk:X2} ({kk}); V{x:X1} = 0x{i.V[x]:X2} + 0x{kk:X2} = {i.V[x] + kk:X2} ({i.V[x] + kk})";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"V{x:X1} += {kk}";
    }
}