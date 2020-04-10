using System;
using System.IO;

namespace Piksel.Testing.Content
{
    public class RandomBytesDummyContent : BytesDummyContent
    {
        private static readonly Random staticRandom = new Random();
        private readonly Random random = staticRandom;

        public RandomBytesDummyContent(int size)
            : base(new byte[size]) { }

        public RandomBytesDummyContent(int size, Random random)
            : this(size) => this.random = random;

        public override void WriteToStream(Stream stream)
        {
            random.NextBytes(bytes);
            base.WriteToStream(stream);
        }
    }
}
