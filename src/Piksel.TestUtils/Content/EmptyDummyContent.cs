using System.Collections.Generic;
using System.IO;

namespace Piksel.Testing.Content
{
    public class EmptyDummyContent : DummyContent
    {
        public override void WriteToStream(Stream stream) { }
    }
}
