﻿[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1028:no trailing whitespace", Justification = "autoformatted away")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:... should be documented", Justification = "maybe later in a stable state")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1602:... should be documented", Justification = "maybe later in a stable state")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:... should be documented", Justification = "maybe later in a stable state")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File must have header", Justification = "not required")]

namespace Chip8
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Chip8.Business;
    using Chip8.Business.Displays;
    using Chip8.Business.Native;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Main Programm Class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main Programm Entry Point.
        /// </summary>
        /// <param name="args">Commandline Arguments.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task Main(string[] args)
        {
            // // await InterpetAsync(args);
            ////await KeyboardDemoAsync(args);
            await ConsoleDisplayDemoAsync(args);
        }

        private static async Task KeyboardDemoAsync(string[] args)
        {
            do
            {
                Console.WriteLine(NativeKeyboard.IsKeyDown(ConsoleKey.A));
            }
            while (true);
        }

        private static async Task ConsoleDisplayDemoAsync(string[] args)
        {
            var display = new ConsoleDisplay();
            display.DrawCharFrame(0, 2, 4, 4);
            display.DrawCharFrame(5, 2, 8, 4);
            display.DrawPixelFrame(14, 2, 8, 8);
            display.Flush();
        }

        private static async Task InterpetAsync(string[] args) 
        {
            var programmData = await File.ReadAllBytesAsync("Assets/flightrunner.ch8");
            //// var programmData = await File.ReadAllBytesAsync("danm8ku.ch8");

            var logger = LoggerFactory
                .Create(logging => logging.AddFile(
                    "Logs/Interpreter-{Date}.txt",
                    outputTemplate: "{Message}{NewLine}"))
                .CreateLogger<Interpreter>();

            var interpreter = new Interpreter(
                new ConsoleHalfBlockNativeDisplay(3, 3),
                singleSteps: false,
                logger: logger);
            //// var interpreter = new Interpreter(new ConsoleHalfBlockDisplay());
            //// var interpreter = new Interpreter(new ConsoleSetPositionDisplay());

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

            // NativeConsole.Write(
            //     "123 GGG",
            //     "456 A B C D",
            //     "789 EFG");
        }
    }
}