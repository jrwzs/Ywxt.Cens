using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class TaxInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xAA, (AddressingMode.ImplicitAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            cpu.Registers.X = cpu.Registers.A;
            cpu.Registers.SetZAndNFlags(cpu.Registers.X);
            return instruction switch
            {
                0xAA => 2,
                _ => 0
            };
        }
    }
}