namespace Chip8.Business
{
    public class Instruction_Fx55_LD : Instruction
    {
        public Instruction_Fx55_LD()
            : base(0xF055, 0xF0FF)
        {
        }

        /// <summary>
        /// Fx55 - LD[I], Vx
        /// Store registers V0 through Vx in memory starting at location I.
        /// The interpreter copies the values of registers V0 through Vx into memory, starting at the address in I.
        /// </summary>
        public override void Execute(Interpreter c8, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            for (int i = 0; i <= x; i++)
            {
                c8.Memory[c8.I + i] = c8.V[i];
            }

            c8.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"LD[I], V{x:X1}";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"memory[i] := V0..V{x:X1}";
    }
}