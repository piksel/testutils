using System.IO;
using NUnit.Framework;
using Piksel.Testing;
using static Piksel.Testing.FileUtils;

namespace Piksel.TestUtils.Tests
{
    [Category("FileUtils")]
    public class TempFileTests
    {
        [Test]
        public void TempFile_Fulfills_Lifetime()
        {
            string tempFilePath;

            using (var tempFile = TempFile.CreateEmpty())
            {
                Assert.IsTrue(tempFile.Exists, "Temporary file was not created");
                tempFilePath = tempFile.FullPath;
            }

            Assert.IsFalse(File.Exists(tempFilePath), "Temporary file was not deleted after Dispose");
        }

        [Test]
        public void TempFile_EmptyDummyContent()
        {
            using var tempFile = TempFile.CreateEmpty();

            Assert.Zero(tempFile.Size, "Temp file was not empty");
        }

        [Test]
        public void TempFile_AnyDummyContent()
        {
            using var tempFile = TempFile.CreateWith(DummyContent.Any);

            Assert.NotZero(tempFile.Size, "Temp file was empty");
        }

        [Test]
        public void TempFile_BytesDummyContent()
        {
            var expectedSize = 12;

            using var tempFile = TempFile.CreateWith(new byte[expectedSize]);

            Assert.AreEqual(expectedSize, tempFile.Size, "Temp file of incorrect size");
        }

        [Test]
        public void TempFile_RandomBytesDummyContent()
        {
            var expectedSize = 18;

            using var tempFile = TempFile.CreateWith(expectedSize);

            Assert.AreEqual(expectedSize, tempFile.Size, "Temp file of incorrect size");
        }


        [Test]
        public void TempFile_StringDummyContent()
        {
            var expectedSize = 10;

            using var tempFile = TempFile.CreateWith(new string('x', expectedSize));

            Assert.AreEqual(expectedSize, tempFile.Size, "File content did not match");
        }
    }
}
