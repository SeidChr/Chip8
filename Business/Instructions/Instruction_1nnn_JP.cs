namespace Chip8.Business.Instructions
{
    public class Instruction_1nnn_JP : Instruction
    {
        public Instruction_1nnn_JP() 
            : base(0x1000, 0xF000)
        {
        }

        /// <summary>
        /// 1nnn - JP addr
        /// Jump to location nnn.
        /// The interpreter sets the program counter to nnn.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            i.ProgrammCounter = nnn;
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"JMP {nnn:X3}; Jump to {nnn:X3};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"goto {nnn:X4}";
    }
}
