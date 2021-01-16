namespace Chip8.Business
{
    public class Instruction_Fx29_LD : Instruction
    {
        public Instruction_Fx29_LD()
            : base(0xF029, 0xF0FF)
        {
        }

        /// <summary>
        /// Fx29 - LD F, Vx
        /// Set I = location of sprite for digit Vx.
        /// The value of I is set to the location for the hexadecimal sprite corresponding to the value of Vx.
        /// See section 2.4, Display, for more information on the Chip-8 hexadecimal font.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            var digit = i.V[x] & 0x0F;
            i.I = Interpreter.SpriteOffset + (Sprites.CharSize * digit);
            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"LD F, V{x:X1}";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => "i := sprite V{x:X1}";
    }
}