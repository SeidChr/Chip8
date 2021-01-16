namespace Chip8.Business
{
    public class Instruction_Fx18_LD_ST : Instruction
    {
        public Instruction_Fx18_LD_ST()
            : base(0xF018, 0xF0FF)
        {
        }

        /// <summary>
        /// Fx18 - LD ST, Vx
        /// Set sound timer = Vx.
        /// ST is set equal to the value of Vx.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            i.SoundTimer = i.V[x];
            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"LD ST, V{x:X1}; ST = 0x{i.V[x]:X2} ({i.V[x]});;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"sound := V{x:X1}";
    }
}