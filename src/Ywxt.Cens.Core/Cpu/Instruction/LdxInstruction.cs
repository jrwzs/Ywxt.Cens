using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LdxInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xA2, AddressingMode.ImmediateAddressingMode},
                {0xAE, AddressingMode.AbsoluteAddressingMode},
                {0xA6, AddressingMode.ZeroPageAddressingMode},
                {0xB6, AddressingMode.ZeroPageYAddressingMode},
                {0xBE, AddressingMode.AbsoluteYAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var data = this.GetData(address, cpu, instruction);
            cpu.Registers.X = data;
            cpu.Registers.SetZAndN(cpu.Registers.X);

            return instruction switch
            {
                0xA2 => 2,
                0xA6 => 3,
                0xB6 => 4,
                0xAE => 4,
                0xBE => 4 + InstructionUtil.GetClockCyclesByCrossingPage(pageCrossed),
                _ => 0
            };
        }
    }
}