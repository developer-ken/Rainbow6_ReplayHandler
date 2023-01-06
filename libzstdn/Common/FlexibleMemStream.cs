using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libzstdn.Common
{
    class FlexibleMemStream : Stream
    {
        private Queue<byte> data = new Queue<byte>();
        int currentPos;

        public override bool CanRead { get => true; }

        public override bool CanSeek { get => false; }

        public override bool CanWrite { get => true; }

        public override long Length => data.Count;

        public override long Position { get => 0; set { } }

        public override void Flush()
        {

        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int pos = 0;
            for (; pos < count; pos++)
            {
                if (pos >= data.Count) break;
                buffer[offset + pos] = data.Dequeue();
            }
            return pos;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return 0;
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            for (int i = 0; i < count; i++)
            {
                data.Enqueue(buffer[i + offset]);
            }
        }

        public override int ReadByte()
        {
            if (data.Count > 0)
                return data.Dequeue();
            else
                return -1;
        }
    }
}
