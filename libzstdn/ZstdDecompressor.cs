using libzstdn.Common;

namespace libzstdn
{
    public class ZstdDecompressor
    {
        private readonly ZstdFrameDecompressor decompressor = new ZstdFrameDecompressor();

        public int Decompress(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int maxOutputLength)
        {
            long inputAddress = 0 + inputOffset;
            long inputLimit = inputAddress + inputLength;
            long outputAddress = 0 + outputOffset;
            long outputLimit = outputAddress + maxOutputLength;

            return decompressor.Decompress(new BinaryReader(new MemoryStream(input)), inputAddress, inputLimit, new BinaryWriter(new MemoryStream(output)), outputAddress, outputLimit);
        }

        public byte[] Decompress(byte[] input)
        {
            long inputAddress = 0;
            long inputLimit = input.Length;
            long outputAddress = 0;
            long outputLimit = long.MaxValue;
            var fmemstream = new FlexibleMemStream();
            decompressor.Decompress(new BinaryReader(new MemoryStream(input)), inputAddress, inputLimit, new BinaryWriter(fmemstream), outputAddress, outputLimit);
            long len = fmemstream.Length;
            byte[] output = new byte[len];
            for(int i = 0; i < len; i++)
            {
                output[i] = (byte)fmemstream.ReadByte();
            }
            return output;
        }
    }
}