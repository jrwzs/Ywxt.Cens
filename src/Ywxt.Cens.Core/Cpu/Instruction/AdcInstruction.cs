using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class AdcInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x69, AddressingMode.ImmediateAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            var result = cpu.Registers.A + data + (byte) (cpu.Registers.P & PFlags.C);
            var af = cpu.Registers.A >> 7;
            var bf = data >> 7;
            var cf = (result >> 7) & 1;
            if (af == bf && af != cf)
            {
                cpu.Registers.P |= PFlags.V;
            }
            else
            {
                cpu.Registers.P &= ~PFlags.V;
            }

            if (((result >> 8) & 1) == 1)
            {
                cpu.Registers.P |= PFlags.C;
            }
            else
            {
                cpu.Registers.P &= ~PFlags.C;
            }

            cpu.Registers.A = unchecked((byte) result);
            cpu.Registers.SetZAndN(cpu.Registers.A);
            return instruction switch
            {
                0x69 => 2,
                _ => 0
            };
        }
    }
}