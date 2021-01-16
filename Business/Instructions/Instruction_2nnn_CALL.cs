namespace Chip8.Business.Instructions
{
    public class Instruction_2nnn_CALL : Instruction
    {
        public Instruction_2nnn_CALL()
            : base(0x2000, 0xF000)
        {
        }

        /// <summary>
        /// 2nnn - CALL addr
        /// Call subroutine at nnn.
        /// The interpreter increments the stack pointer, then puts the current PC on the top of the stack.The PC is then set to nnn.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            //// The interpreter increments the stack pointer, then puts the current PC on the top of the stack. The PC is then set to nnn.
            i.StackPointer += 1;
            i.Stack[i.StackPointer] = i.ProgrammCounter;
            i.ProgrammCounter = nnn;
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"CALL {nnn:X3}; Call Subroutine at {nnn:X3};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"call {nnn:X4}";
    }
}