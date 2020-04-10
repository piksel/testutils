using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Piksel.Testing
{
    public static partial class FileUtils
	{
        public class TempFile : IDisposable
		{
			public FileInfo Info { get; }

			/// <summary>
			/// Returns the name of the file, including file extension
			/// </summary>
			public string FileName => Info.Name;

			/// <summary>
			/// Returns the full path of the file, including filename
			/// </summary>
			public string FullPath => Info.FullName;

			/// <summary>
			/// Returns the path of the file, excluding filename and path separator
			/// </summary>
			public string FilePath => Path.GetDirectoryName(Info.FullName);

			/// <summary>
			/// Returns whether the temporary file exists
			/// </summary>
			public bool Exists => Info.Exists;

			/// <summary>
			/// Returns size in bytes of the temporary file
			/// </summary>
			public long Size => Info.Length;

			private TempFile()
			{
				Info = new FileInfo(Path.Combine(Path.GetTempPath(), GetDummyFileName()));
			}

			/// <summary>
			/// Create a new temporary file containing <paramref name="content"/>
			/// </summary>
			public TempFile(DummyContent content): this()
			{
				using var fs = Info.Create();
				WriteDummyData(fs, content);
			}

			public static TempFile CreateEmpty()
				=> new TempFile(DummyContent.Empty);

			public static TempFile CreateWith(int size) 
				=> new TempFile(DummyContent.RandomBytes(size));

			public static TempFile CreateWith(string content)
				=> new TempFile(DummyContent.Utf8String(content));

			public static TempFile CreateWith(byte[] content)
				=> new TempFile(DummyContent.Bytes(content));

			public static TempFile CreateWith(DummyContent content)
				=> new TempFile(content);

			/// <summary>
			/// Return a new temporary file without creating it
			/// </summary>
			public static TempFile NewNonExisting()
				=> new TempFile();

			#region IDisposable Support

			private bool disposed = false; // To detect redundant calls

			[SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Should not throw in Dispose")]
			protected virtual void Dispose(bool disposing)
			{
				if (!disposed)
				{
					if (disposing && (Info?.Exists ?? false))
					{
						try
						{
							Info.Delete();
						}
						catch { }
					}

					disposed = true;
				}
			}

			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			#endregion IDisposable Support

			public static implicit operator FileInfo(TempFile tempFile)
				=> tempFile.Info;
		}
	}
}
