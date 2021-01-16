namespace Chip8.Business
{
    public class Instruction_5xy0_SE : Instruction
    {
        public Instruction_5xy0_SE()
            : base(0x5000, 0xF00F)
        {
        }

        /// <summary>
        /// 5xy0 - SE Vx, Vy
        /// Skip next instruction if Vx = Vy.
        /// The interpreter compares register Vx to register Vy, and if they are equal, increments the program counter by 2.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            if (i.V[x] == i.V[y])
            {
                i.Step();
            }

            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"SE V{x:X1}, V{y:X1}; Skip if {i.V[x]:X2} == {i.V[y]:X2} => {i.V[x] == i.V[y]};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"if V{x:X1} != V{y:X1} then";
    }
}