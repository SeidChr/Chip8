namespace Chip8.Business
{
    public class Instruction_Cxkk_RND : Instruction
    {
        public Instruction_Cxkk_RND()
            : base(0xC000, 0xF000)
        {
        }

        /// <summary>
        /// Cxkk - RND Vx, byte
        /// Set Vx = random byte AND kk.
        /// The interpreter generates a random number from 0 to 255, which is then ANDed with the value kk.The results are stored in Vx.See instruction 8xy2 for more information on AND.
        /// </summary>
        public override void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
        {
            i.V[x] = (byte)(i.Random.Next(0xFF) & kk);
            i.Step();
        }

        public override string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"RND V{x:X1}, {kk:X2}; V{x:X1} = RND & 0x{kk:X2};;";

        public override string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn)
            => $"V{x:X1} := RND & {kk:X2};;";
    }
}