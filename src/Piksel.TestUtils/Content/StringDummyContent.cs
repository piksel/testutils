using System.Text;

namespace Piksel.Testing.Content
{
    public class StringDummyContent : BytesDummyContent
    {
        public StringDummyContent(string content)
            : this(content, Encoding.UTF8) { }

        public StringDummyContent(string content, Encoding encoding)
            : base(encoding.GetBytes(content)) { }
    }
}
