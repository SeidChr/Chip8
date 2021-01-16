namespace Chip8.Business.Instructions
{
    public class Instruction_00E0_CLS : Instruction
    {
        public Instruction_00E0_CLS()
            : base(0x00e0, 0xFFFF)
        {
        }

        /// <summary>
        /// 00E0 - CLS
        /// Clear the display.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            i.ClearScreen();
            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => "CLS; CLearScreen; Clear the display;;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => "clear";
    }
}