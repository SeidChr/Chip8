namespace Chip8.Business.Instructions
{
    public class Instruction_00EE_RET : Instruction
    {
        public Instruction_00EE_RET()
            : base(0x00ee, 0xFFFF)
        {
        }

        /// <summary>
        /// 00EE - RET
        /// Return from a subroutine.
        /// The interpreter sets the program counter to the address at the top of the stack, then subtracts 1 from the stack pointer.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            // The interpreter sets the program counter to the address at the top of the stack, then subtracts 1 from the stack pointer.
            i.ProgrammCounter = i.Stack[i.StackPointer];
            i.Stack[i.StackPointer] = 0;
            i.StackPointer -= 1;
            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"RET; Return from subroutine to {i.Stack[i.StackPointer]:X3};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"return";
    }
}
