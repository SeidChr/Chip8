namespace Chip8.Business
{
    public class Sprites
    {
        public static readonly int CharSize = 5;

        public static byte[][] Chars { get; } = new byte[][]
        {
            new byte[]
            {
                0xF0,
                0x90,
                0x90,
                0x90,
                0xF0,
            },
            new byte[]
            {
                0x20,
                0x60,
                0x20,
                0x20,
                0x70,
            },
            new byte[]
            {
                0xF0,
                0x10,
                0xF0,
                0x80,
                0xF0,
            },
            new byte[]
            {
                0xF0,
                0x10,
                0xF0,
                0x10,
                0xF0,
            },
            new byte[]
            {
                0x90,
                0x90,
                0xF0,
                0x10,
                0x10,
            },
            new byte[]
            {
                0xF0,
                0x80,
                0xF0,
                0x10,
                0xF0,
            },
            new byte[]
            {
                0xF0,
                0x80,
                0xF0,
                0x90,
                0xF0,
            },
            new byte[]
            {
                0xF0,
                0x10,
                0x20,
                0x40,
                0x40,
            },
            new byte[]
            {
                0xF0,
                0x90,
                0xF0,
                0x90,
                0xF0,
            },
            new byte[]
            {
                0xF0,
                0x90,
                0xF0,
                0x10,
                0xF0,
            },
            new byte[]
            {
                0xF0,
                0x90,
                0xF0,
                0x90,
                0x90,
            },
            new byte[]
            {
                0xE0,
                0x90,
                0xE0,
                0x90,
                0xE0,
            },
            new byte[]
            {
                0xF0,
                0x80,
                0x80,
                0x80,
                0xF0,
            },
            new byte[]
            {
                0xE0,
                0x90,
                0x90,
                0x90,
                0xE0,
            },
            new byte[]
            {
                0xF0,
                0x80,
                0xF0,
                0x80,
                0xF0,
            },
            new byte[]
            {
                0xF0,
                0x80,
                0xF0,
                0x80,
                0x80,
            },
        };
    }
}