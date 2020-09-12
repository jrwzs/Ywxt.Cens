using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class SecInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x38, (AddressingMode.ImplicitAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            cpu.Registers.P |= PFlags.C;
            return instruction switch
            {
                0x38 => 2,
                _ => 0,
            };
        }
    }
}