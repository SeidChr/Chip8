namespace Chip8.Business.Instructions
{
    public class Instruction_Annn_LD : Instruction
    {
        public Instruction_Annn_LD()
            : base(0xA000, 0xF000)
        {
        }

        /// <summary>
        /// Annn - LD I, addr
        /// Set I = nnn.
        /// The value of register I is set to nnn.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            i.I = nnn;
            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"LD I, {nnn:X3}; I = {nnn:X3};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"i := {nnn}";
    }
}