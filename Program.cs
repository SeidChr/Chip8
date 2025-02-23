﻿[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA0001:no xml docs", Justification = "not required")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1028:no trailing whitespace", Justification = "autoformatted away")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:... should be documented", Justification = "maybe later in a stable state")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1602:... should be documented", Justification = "maybe later in a stable state")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:... should be documented", Justification = "maybe later in a stable state")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File must have header", Justification = "not required")]

namespace Chip8
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Chip8.Business;
    using Chip8.Business.Displays;
    using Chip8.Business.Native;
    using Microsoft.Extensions.Logging;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "debug") 
            {
                Attach();
            }

            await InterpetAsync(args);
            //// KeyboardDemo(args);
            //// await ConsoleDisplayDemoAsync(args);
        }

        private static void Attach() 
        {
            Console.WriteLine("Please attach debugger to " + AppDomain.CurrentDomain.FriendlyName);
            while (!Debugger.IsAttached) 
            {
            }
        }

        private static void KeyboardDemo(string[] args)
        {
            do
            {
                Console.WriteLine(NativeKeyboard.IsKeyDown(ConsoleKey.A));
            }
            while (true);
        }

        private static async Task ConsoleDisplayDemoAsync(string[] args)
        {
            using var display = new ConsoleDisplay();

            display.DrawFrame(0, 2, 4, 4);
            display.DrawFrame(5, 2, 8, 4);

            // display.Flush();
            await Task.Delay(TimeSpan.FromSeconds(5));
        }

        private static async Task NativeConsoleTest()
        {
            NativeConsole.Write(
                "123 GGG",
                "456 A B C D",
                "789 EFG");
        }

        private static async Task InterpetAsync(string[] args) 
        {
            //var programmData = await File.ReadAllBytesAsync("Assets/Games/flightrunner.ch8");
            // var programmData = await File.ReadAllBytesAsync("Assets/Games/Space Invaders [David Winter].ch8");
             var programmData = await File.ReadAllBytesAsync("Assets/Games/danm8ku.ch8");
            // var programmData = await File.ReadAllBytesAsync("Assets/Games/TETRIS.ch8");
            // var programmData = await File.ReadAllBytesAsync("Assets/Games/BLITZ.ch8");

            var logger = LoggerFactory
                .Create(logging => logging.AddFile(
                    "Logs/Interpreter-{Date}.txt",
                    outputTemplate: "{Message}{NewLine}"))
                .CreateLogger<Interpreter>();

            var displayDriver = new ConsoleHalfBlockNativeDisplay(3, 0);
            // var displayDriver = new ConsoleHalfBlockDisplay();
            var interpreter = new Interpreter(
                displayDriver,
                singleSteps: false,
                logger: logger);

            Console.CancelKeyPress += new ConsoleCancelEventHandler((sender, args) =>
            {
                var instructionMap = interpreter.InstructionMap
                    .Select(kvp => kvp)
                    .OrderBy(kvp => kvp.Key)
                    .Select(kvp => kvp.Value.ToCompilerString(interpreter, kvp.Key) + Environment.NewLine);

                logger.LogInformation("####################### Instructions" + Environment.NewLine + string.Concat(instructionMap));
                interpreter.Stop();
                args.Cancel = true;
            });

            interpreter.Load(programmData);
            interpreter.Start();

            if (displayDriver is IDisposable disposableDisplayDriver)
            {
                disposableDisplayDriver.Dispose();
            }
        }
    }
}
