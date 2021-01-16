namespace Chip8.Business
{
    public abstract class Instruction : IInstruction
    {
        public Instruction(ushort id, ushort mask)
        {
            this.Id = id;
            this.Mask = mask;
        }

        public ushort Id { get; private set; }

        public ushort Mask { get; private set; }

        public string ToHumanString(Interpreter i, int pc)
            => $"{pc:X4}: {i.Memory[pc]:X2}{i.Memory[pc + 1]:X2} - {this.HumanReadable(i, pc)}";

        public string ToCompilerString(Interpreter i, int pc)
            => $"{pc:X4}: {i.Memory[pc]:X2}{i.Memory[pc + 1]:X2} - {this.CompilerReadable(i, pc)}";

        public void Execute(Interpreter i, int pc)
        {
            var (x, y, n, kk, nnn) = Extract(i, pc);
            this.Execute(i, pc, x, y, n, kk, nnn);
        }

        public string HumanReadable(Interpreter i, int pc)
        {
            var (x, y, n, kk, nnn) = Extract(i, pc);
            return this.HumanReadable(i, pc, x, y, n, kk, nnn);
        }

        public string CompilerReadable(Interpreter i, int pc)
        {
            var (x, y, n, kk, nnn) = Extract(i, pc);
            return this.CompilerReadable(pc, x, y, n, kk, nnn);
        }

        public abstract void Execute(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn);

        public abstract string HumanReadable(Interpreter i, int pc, byte x, byte y, byte n, byte kk, ushort nnn);

        public virtual string CompilerReadable(int pc, byte x, byte y, byte n, byte kk, ushort nnn) => string.Empty;

        private static (byte, byte, byte, byte, ushort) Extract(Interpreter i, int pc)
        {
            byte msb = i.Memory[pc];
            byte kk = i.Memory[pc + 1];

            // byte nibble0 = (byte)(msb >> 4);
            byte x = (byte)(msb & 0b1111);
            byte y = (byte)(kk >> 4);
            byte n = (byte)(kk & 0b1111);
            ushort nnn = (ushort)((x << 8) | kk);

            return (x, y, n, kk, nnn);
        }
    }
}