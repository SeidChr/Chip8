namespace Chip8.Business
{
    public interface IInstruction
    {
        ushort Id { get; }

        ushort Mask { get; }

        string ToHumanString(Interpreter i, int pc);

        string ToCompilerString(Interpreter i, int pc);

        void Execute(Interpreter i, int pc);
    }
}