using System;
using System.IO;

namespace Piksel.Testing
{
	public static partial class FileUtils
	{
		private static readonly Random random = new Random();

		public static FileInfo WriteDummyData(FileInfo file, DummyContent content)
		{
			using var fs = file.OpenWrite();
			WriteDummyData(fs, content);
			return file;
		}

		public static void WriteDummyData(Stream stream, DummyContent content, bool autoClose = false)
		{
			content.WriteToStream(stream);

			if(autoClose)
			{
				stream.Dispose();
			}
		}

		public static FileInfo WriteDummyData(string fileName, DummyContent content)
			=> WriteDummyData(new FileInfo(fileName), content);

		public static TempFile GetDummyFile(DummyContent content) 
			=> new TempFile(content);

		public static string GetDummyFileName()
			=> $"{random.Next():x8}{random.Next():x8}{random.Next():x8}";

		public static string GetDummyFileName(string extension)
			=> $"{GetDummyFileName()}.{extension.TrimStart('.')}";

	}
}
