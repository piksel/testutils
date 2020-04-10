using NUnit.Framework;
using Piksel.Testing;
using System.IO;
using System.Linq;
using static Piksel.Testing.FileUtils;
using static Piksel.Testing.Utils;

namespace Piksel.TestUtils.Tests
{
    [Category("FileUtils")]
    public class TempDirTests
    {
        
        [Test]
        public void TempDir_Fulfills_Lifetime()
        {
            string tempDirPath;

            using(var tempDir = new TempDir())
            {
                Assert.IsTrue(tempDir.Exists, "TempDir was not created");
                tempDirPath = tempDir.Fullpath;
            }

            Assert.IsFalse(Directory.Exists(tempDirPath), "TempDir was not deleted after Dispose");
        }

        [Test]
        public void TempDir_Clears_Contained_Files()
        {
            var dummyFileCount = 3;

            using var tempDir = new TempDir();
            if (!tempDir.Exists) Assert.Inconclusive("Failed to create test directory");

            Repeat(dummyFileCount, _ => tempDir.CreateDummyFile());

            var fileCount = tempDir.GetFiles().Count(f => f.Exists);
            if (fileCount != dummyFileCount) Assert.Inconclusive("Failed to create test files");

            tempDir.Clear();

            var remainingFileCount = tempDir.GetFiles("*", SearchOption.AllDirectories).Length;

            Assert.Zero(remainingFileCount, "All files were not delted upon clearing TempDir");
        }

        [Test]
        public void TempDir_Clears_SubDir_Files()
        {
            var dummyFileCount = 3;

            using var tempDir = new TempDir();
            if (!tempDir.Exists) Assert.Inconclusive("Failed to create test directory");

            Repeat(dummyFileCount, i => {
                var subDir = tempDir.CreateSubdirectory(i.ToString());
                var dummyFile = WriteDummyData(Path.Combine(subDir.FullName, GetDummyFileName()), DummyContent.Any);
                if (!dummyFile.Exists || dummyFile.Length == 0) Assert.Inconclusive("Failed to create test files");
            });

            tempDir.Clear();

            var remainingFileCount = tempDir.GetFiles("*", SearchOption.AllDirectories).Length;

            Assert.Zero(remainingFileCount, "All files were not delted upon clearing TempDir");
        }
    }
}