namespace Chip8.Business
{
    using System.Collections.Generic;
    using System.Linq;
    using Chip8.Business.Instructions;

    public class InstructionFactory
    {
        private static readonly List<IInstruction> Instructions = new ()
        {
            new Instruction_00E0_CLS(),
            new Instruction_00EE_RET(),
            new Instruction_1nnn_JP(),
            new Instruction_2nnn_CALL(),
            new Instruction_3xkk_SE(),
            new Instruction_4xkk_SNE(),
            new Instruction_5xy0_SE(),
            new Instruction_6xkk_LD(),
            new Instruction_7xkk_ADD(),
            new Instruction_8xy0_LD(),
            new Instruction_8xy1_OR(),
            new Instruction_8xy2_AND(),
            new Instruction_8xy3_XOR(),
            new Instruction_8xy4_ADD(),
            new Instruction_8xy5_SUB(),
            new Instruction_8xy6_SHR(),
            new Instruction_8xy7_SUBN(),
            new Instruction_8xyE_SHL(),
            new Instruction_9xy0_SNE(),
            new Instruction_Annn_LD(),
            new Instruction_Bnnn_JP(),
            new Instruction_Cxkk_RND(),
            new Instruction_Dxyn_DRW(),
            new Instruction_Ex9E_SKP(),
            new Instruction_ExA1_SKNP(),
            new Instruction_Fx07_LD_DT(),
            new Instruction_Fx0A_LD(),
            new Instruction_Fx15_LD_DT(),
            new Instruction_Fx18_LD_ST(),
            new Instruction_Fx1E_ADD(),
            new Instruction_Fx29_LD(),
            new Instruction_Fx33_LD(),
            new Instruction_Fx55_LD(),
            new Instruction_Fx65_LD(),
        };

        public static IInstruction Create(Interpreter i, int pc)
        {
            ushort opcode = (ushort)(i.Memory[pc] << 8 | i.Memory[pc + 1]);
            var instruction = Instructions.FirstOrDefault(i => (opcode & i.Mask) == i.Id);
            if (instruction == null)
            {
                throw new System.Exception($"No instruction found for 0x{opcode:X4}");
            }

            return instruction;
        }
    }
}