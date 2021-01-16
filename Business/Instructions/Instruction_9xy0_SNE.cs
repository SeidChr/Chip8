namespace Chip8.Business
{
    public class Instruction_9xy0_SNE : Instruction
    {
        public Instruction_9xy0_SNE()
            : base(0x9000, 0xF00F)
        {
        }

        /// <summary>
        /// 9xy0 - SNE Vx, Vy
        /// Skip next instruction if Vx != Vy.
        /// The values of Vx and Vy are compared, and if they are not equal, the program counter is increased by 2.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            if (i.V[x] != i.V[y]) 
            {
                i.Step();
            }

            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"SNE V{x:X1}, V{y:X1}; Skip if {i.V[x]:X2} != V{i.V[y]:X2} => {i.V[x] != i.V[y]};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"if V{x:X1} == V{y:X1} then";
    }
}