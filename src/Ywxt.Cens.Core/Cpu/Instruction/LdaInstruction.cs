using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LdaInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
            = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
            {
                {0xA9, (AddressingType.Data, AddressingMode.ImmediateAddressingMode)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort data)
        {
            cpu.Registers.A = (byte) data;
            cpu.Registers.SetZAndN(cpu.Registers.A);

            return instruction switch
            {
                0xA9 => 2,
                _ => 0
            };
        }
    }
}