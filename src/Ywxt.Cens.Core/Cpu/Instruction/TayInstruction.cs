using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class TayInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xA8, AddressingMode.ImplicitAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            cpu.Registers.Y = cpu.Registers.A;
            cpu.Registers.SetZAndNFlags(cpu.Registers.Y);
            return instruction switch
            {
                0xA8 => 2,
                _ => 0
            };
        }
    }
}