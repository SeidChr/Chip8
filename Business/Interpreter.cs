namespace Chip8.Business
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class Interpreter : IDisposable
    {
        public const int MemorySize = 0x1000;

        public const int ProgrammOffset = 0x200;

        public const int SpriteOffset = 0x000;

        public const int DisplayHeight = 48;

        public const int DisplayWidth = 64;

        public const long Diff60Hz = 10_000 * 17;

        public const int ExecutionDelayTicks = 5000;

        private const int AvailableProgrammMemory = MemorySize - ProgrammOffset;

        private readonly IDisplayDriver displayDriver;

        private readonly bool singleSteps;

        private readonly ILogger<Interpreter> logger;

        private readonly Timer timerClock;

        private int loadedProgrammSize = 0;

        private bool active = true;

        private Stopwatch instructionExecutionStopwatch;

        public Interpreter(IDisplayDriver displayDriver, bool singleSteps = false, ILogger<Interpreter> logger = null)
        {
            this.displayDriver = displayDriver;
            this.singleSteps = singleSteps;
            this.logger = logger;

            this.timerClock = new Timer((state) => ((Interpreter)state).UpdateTimers(), this, 17, 17);
            this.instructionExecutionStopwatch = new Stopwatch();
        }

        ~Interpreter() 
        {
            this.Dispose();
        }

        public byte[] Memory { get; } = new byte[0x1000];

        public int[] Stack { get; } = new int[0x10];

        // 64 x 48 = 256 Dots.
        public bool[,] Display { get; } = new bool[DisplayWidth, DisplayHeight];

        public Dictionary<int, IInstruction> InstructionMap { get; } = new ();

        public byte[] V { get; } = new byte[0x10];

        public int I { get; set; } = 0;

        public Random Random { get; } = new ();

        public int StackPointer { get; set; } = -1;

        public int ProgrammCounter { get; set; } = ProgrammOffset;

        public byte SoundTimer { get; set; } = 0;

        public byte DelayTimer { get; set; } = 0;

        public string Status
            => $"V[{string.Join(",", this.V.Select(s => s.ToString("X2")))}] "
            + $"S[{string.Join(",", this.Stack.Select(s => s.ToString("X3")))}]@{this.StackPointer} I={this.I:X3} DT={this.DelayTimer:X2} ST={this.SoundTimer:X2}";

        public IInstruction CurrentInstruction => InstructionFactory.Create(this, this.ProgrammCounter);

        public void Load(byte[] programmData)
        {
            Array.Clear(this.Memory, 0, MemorySize);
            Array.Clear(this.Stack, 0, this.Stack.Length);
            Array.Clear(this.V, 0, this.V.Length);
            this.ClearScreen();

            this.ProgrammCounter = ProgrammOffset;

            this.LoadSprites();

            Array.Copy(programmData, 0, this.Memory, ProgrammOffset, Math.Min(programmData.Length, AvailableProgrammMemory));
            this.loadedProgrammSize = programmData.Length;
        }

        public bool Step(int words = 1)
        {
            this.ProgrammCounter += 2 * words;
            return this.ProgrammCounter < (ProgrammOffset + this.loadedProgrammSize);
        }

        public void Start()
        {
            this.active = true;
            this.instructionExecutionStopwatch.Start();

            do
            {
                var instruction = this.CurrentInstruction;
                this.InstructionMap[this.ProgrammCounter] = instruction;
                ////Console.Write();
                ////Console.WriteLine(this.Status + " " +instruction.ToString(this, this.ProgrammCounter));
                ////Console.ReadLine();

                // var instructionText = instruction.ToHumanString(this, this.ProgrammCounter);
                //// this.logger?.LogInformation(instructionText);
                //// Native.NativeConsole.Write(this.Status, instructionText);
                if (this.singleSteps)
                {
                    Console.ReadKey(true);
                }

                while (this.instructionExecutionStopwatch.ElapsedTicks < ExecutionDelayTicks) 
                {
                    // nop
                }

                this.instructionExecutionStopwatch.Restart();

                instruction.Execute(this, this.ProgrammCounter);
            }
            while (this.active);

            this.instructionExecutionStopwatch.Reset();
        }

        public void Draw()
        {
            this.displayDriver.Draw(this);
        }

        public void ClearScreen()
        {
            for (int a = 0; a < this.Display.GetLength(0); a++)
            {
                for (int b = 0; b < this.Display.GetLength(1); b++)
                {
                    this.Display[a, b] = false;
                }
            }
        }

        public void Stop() 
        {
            this.active = false;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            this.timerClock.Dispose();
        }

        private void UpdateTimers()
        {
            if (this.DelayTimer > 0)
            {
                this.DelayTimer--;
            }

            if (this.SoundTimer > 0)
            {
                this.SoundTimer--;
            }
        }

        private void LoadSprites()
        {
            var chars = Sprites.Chars;
            var len = chars.Length;
            var size = Sprites.CharSize;

            for (var i = 0; i < len; i++)
            {
                Array.Copy(chars[i], 0, this.Memory, i * size, size);
            }
        }

    }
}