using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class SedInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xF8, (AddressingMode.ImplicitAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            cpu.Registers.P |= PFlags.D;
            return instruction switch
            {
                0xF8 => 2,
                _ => 0
            };
        }
    }
}