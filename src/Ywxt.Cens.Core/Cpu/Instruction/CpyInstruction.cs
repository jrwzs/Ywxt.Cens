using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class CpyInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xC0, AddressingMode.ImmediateAddressingMode},
                {0xC4, AddressingMode.ZeroPageAddressingMode},
                {0xCC, AddressingMode.AbsoluteAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var data = this.GetData(address, cpu, instruction);
            var result = cpu.Registers.Y - data;
            cpu.Registers.SetZAndN(unchecked((byte) result));
            cpu.Registers.SetC(result >= 0);

            return instruction switch
            {
                0xC0 => 2,
                0xC4 => 3,
                0xCC => 4,
                _ => 0
            };
        }
    }
}