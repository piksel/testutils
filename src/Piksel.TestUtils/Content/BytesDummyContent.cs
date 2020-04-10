using System.IO;

namespace Piksel.Testing.Content
{
    public class BytesDummyContent : DummyContent
    {
        public BytesDummyContent(byte[] bytes)
            => this.bytes = bytes;

        protected byte[] bytes;

        public override void WriteToStream(Stream stream)
        {
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}
