using System;
using System.IO;
using System.Text;

namespace Piksel.Testing.Content
{
    public class SomeDummyContent : BytesDummyContent
    {
        private const int ContentSize = 16;

        public SomeDummyContent()
            : base(new byte[ContentSize]) { }

        public override void WriteToStream(Stream stream)
        {
            Encoding.ASCII.GetBytes(DateTime.UtcNow.Ticks.ToString("x16"), 0, ContentSize, bytes, 0);
            base.WriteToStream(stream);
        }
    }
}
