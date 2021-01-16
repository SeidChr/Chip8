namespace Chip8.Business.Instructions
{
    public class Instruction_6xkk_LD : Instruction
    {
        public Instruction_6xkk_LD()
            : base(0x6000, 0xF000)
        {
        }

        /// <summary>
        /// 6xkk - LD Vx, byte
        /// Set Vx = kk.
        /// The interpreter puts the value kk into register Vx.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            i.V[x] = kk;
            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"LD V{x:X1}, 0x{kk:X2}; Load 0x{kk:X2} ({kk}) into V{x:X1};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"V{x:X1} := {kk}";
    }
}