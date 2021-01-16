namespace Chip8.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Logging;

    public class Interpreter
    {
        public const int MemorySize = 0x1000;

        public const int ProgrammOffset = 0x200;

        public const int SpriteOffset = 0x000;

        public const int DisplayHeight = 48;

        public const int DisplayWidth = 64;

        public const long Diff60Hz = 10_000 * 17;

        private const int AvailableProgrammMemory = MemorySize - ProgrammOffset;

        private readonly IDisplayDriver displayDriver;

        private readonly bool singleSteps;

        private readonly ILogger<Interpreter> logger;

        private int loadedProgrammSize = 0;

        private bool active = true;

        public Interpreter(IDisplayDriver displayDriver, bool singleSteps = false, ILogger<Interpreter> logger = null)
        {
            this.displayDriver = displayDriver;
            this.singleSteps = singleSteps;
            this.logger = logger;
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

        // 10.000 Ticks / Millisecond
        // 60 hz = 60 times / second = 60 times / (1000 milliseconds)
        public long LastTimerUpdate { get; set; } = DateTime.Now.Ticks;

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
            this.UpdateTimers();
            this.ProgrammCounter += 2 * words;
            return this.ProgrammCounter < (ProgrammOffset + this.loadedProgrammSize);
        }

        public void Start()
        {
            this.active = true;
            do
            {
                var instruction = this.CurrentInstruction;
                this.InstructionMap[this.ProgrammCounter] = instruction;
                ////Console.Write();
                ////Console.WriteLine(this.Status + " " +instruction.ToString(this, this.ProgrammCounter));
                ////Console.ReadLine();

                var instructionText = instruction.ToHumanString(this, this.ProgrammCounter);
                this.logger?.LogInformation(instructionText);
                Native.NativeConsole.Write(this.Status, instructionText);
                if (this.singleSteps)
                {
                    Console.ReadKey(true);
                }

                instruction.Execute(this, this.ProgrammCounter);
            }
            while (this.active);
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

        private void UpdateTimers()
        {
            var currectTicks = DateTime.Now.Ticks;
            var diff = this.LastTimerUpdate - currectTicks;
            //// Native.NativeConsole.Write("","",$"   diff={diff} expectedDiff={Diff60Hz}   ");
            var updateTimers = (currectTicks - this.LastTimerUpdate) > Diff60Hz;
            this.LastTimerUpdate = currectTicks;
            if (updateTimers && this.DelayTimer > 0)
            {
                this.DelayTimer--;
            }

            if (updateTimers && this.SoundTimer > 0)
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