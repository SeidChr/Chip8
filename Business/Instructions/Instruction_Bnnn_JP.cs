namespace Chip8.Business
{
    public class Instruction_Bnnn_JP : Instruction
    {
        public Instruction_Bnnn_JP()
            : base(0xB000, 0xF000)
        {
        }

        /// <summary>
        /// Bnnn - JP V0, addr
        /// Jump to location nnn + V0.
        /// The program counter is set to nnn plus the value of V0.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            i.ProgrammCounter = i.V[0] + nnn;
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"JP V0, 0x{nnn:X3}; Jump to 0x{i.V[0] + nnn:X3};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"goto 0xV0 + {nnn:X4}";
    }
}