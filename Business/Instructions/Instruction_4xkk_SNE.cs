namespace Chip8.Business
{
    public class Instruction_4xkk_SNE : Instruction
    {
        public Instruction_4xkk_SNE()
            : base(0x4000, 0xF000)
        {
        }

        /// <summary>
        /// 4xkk - SNE Vx, byte
        /// Skip next instruction if Vx != kk.
        /// The interpreter compares register Vx to kk, and if they are not equal, increments the program counter by 2.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            if (i.V[x] != kk) 
            {
                i.Step();
            }

            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"SNE V{x:X1}, {kk:X2}; Skip if {i.V[x]:X2} != {kk:X2} => {i.V[x] != kk};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"if V{x:X1} == {kk} then"; // inverted because next statement is "then"
    }
}