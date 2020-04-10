using NUnit.Framework;
using Piksel.Testing;
using Piksel.Testing.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Piksel.TestUtils.Tests
{
    public class DummyContentTests
    {

        MemoryStream ms;

        [SetUp]
        public void Setup()
        {
            ms = new MemoryStream();
        }

        [Test]
        public void EmptyDummyContent_WritesExpectedContent()
        {
            var uuc = DummyContent.Empty;
            uuc.WriteToStream(ms);
            Assert.Zero(ms.Length, "Content length was not empty");
        }

        [Test]
        public void AnyDummyContent_WritesExpectedContent()
        {
            var uuc = DummyContent.Any;
            uuc.WriteToStream(ms);

            Assert.NotZero(ms.Length, "Content stream was empty");
        }

        [Test]
        public void BytesDummyContent_WritesExpectedContent()
        {
            var byteCount = 12;
            var content = new byte[byteCount];

            var uuc = DummyContent.Bytes(content);
            uuc.WriteToStream(ms);

            Assert.AreEqual(byteCount, ms.Length, "Content stream was of incorrect size");
        }

        [Test]
        public void RandomBytesDummyContent_WritesExpectedContent()
        {
            var byteCount = 18;

            var uuc = DummyContent.RandomBytes(byteCount);
            uuc.WriteToStream(ms);
            Assert.AreEqual(byteCount, ms.Length, "Content stream was of incorrect size");
        }


        [Test]
        public void StringDummyContent_WritesExpectedContent()
        {
            var testString = "The ghosts of dead teenagers sing to me while I am dancing";

            var uuc = DummyContent.Utf8String(testString);
            uuc.WriteToStream(ms);

            ms.Seek(0, SeekOrigin.Begin);

            using var sr = new StreamReader(ms);
            var actualContent = sr.ReadToEnd();

            Assert.AreEqual(testString, actualContent, "File content did not match");
        }
    }
}
