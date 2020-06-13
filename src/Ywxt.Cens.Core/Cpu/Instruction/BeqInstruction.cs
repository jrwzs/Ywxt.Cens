using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class BeqInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x90, AddressingMode.RelativeAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Address;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            var oldAddress = cpu.Registers.Pc;
            var jmpSuccess = !cpu.Registers.P.HasFlag(PFlags.C);
            if (jmpSuccess)
            {
                cpu.Registers.Pc = address;
            }

            return instruction switch
            {
                0x90 => 2 + BranchInstructionUtil.GetClockCycleIncrement(jmpSuccess, oldAddress, cpu.Registers.Pc),
                _ => 0
            };
        }
    }
}