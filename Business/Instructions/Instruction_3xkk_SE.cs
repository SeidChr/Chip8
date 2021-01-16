namespace Chip8.Business
{
    public class Instruction_3xkk_SE : Instruction
    {
        public Instruction_3xkk_SE() 
            : base(0x3000, 0xF000)
        {
        }

        /// <summary>
        /// 3xkk - SE Vx, byte
        /// Skip next instruction if Vx = kk.
        /// The interpreter compares register Vx to kk, and if they are equal, increments the program counter by 2.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            if (i.V[x] == kk) 
            {
                i.Step();
            }

            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"SE V{x:X1}, {kk:X2}; Skip if {i.V[x]:X2} == {kk:X2} => {i.V[x] == kk};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"if V{x:X1} != {kk:X2} then"; // inverted because next statement is "then"
    }
}