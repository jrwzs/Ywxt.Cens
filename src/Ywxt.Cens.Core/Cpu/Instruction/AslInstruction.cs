using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class AslInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x0A, AddressingMode.AccumulatorAddressingMode},
                {0x06, AddressingMode.ZeroPageAddressingMode},
                {0x16, AddressingMode.ZeroPageXAddressingMode},
                {0x0E, AddressingMode.AbsoluteAddressingMode},
                {0x1E, AddressingMode.AbsoluteXAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            var old = data;
            byte @new = 0;
            switch (instruction)
            {
                case 0x0A:
                    cpu.Registers.A = (byte) (data << 1);
                    @new = cpu.Registers.A;
                    break;
                case 0x06:
                case 0x16:
                case 0x0E:
                case 0x1E:
                    cpu.Bus.WriteByte(address, (byte) (data << 1));
                    @new = (byte) (data << 1);
                    break;
            }

            cpu.Registers.SetC(old >> 7 == 1);
            cpu.Registers.SetZAndN(@new);

            return instruction switch
            {
                0x0A => 2,
                0x06 => 5,
                0x16 => 6,
                0x0E => 6,
                0x1E => 7,
                _ => 0
            };
        }
    }
}