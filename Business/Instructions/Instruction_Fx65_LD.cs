namespace Chip8.Business
{
    public class Instruction_Fx65_LD : Instruction
    {
        public Instruction_Fx65_LD()
            : base(0xF065, 0xF0FF)
        {
        }

        /// <summary>
        /// Fx65 - LD Vx, [I]
        /// Read registers V0 through Vx from memory starting at location I.
        /// The interpreter reads values from memory starting at location I into registers V0 through Vx.
        /// </summary>
        public override void Execute(Interpreter c8, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            for (int i = 0; i <= x; i++) 
            {
                c8.V[i] = c8.Memory[c8.I + i];
            }

            c8.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"LD V{x:X1}, [I]; Read Bytes at {i.I:X3} into V0..V{x:X1};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"V0..V{x:X1} := memory[i]";
    }
}