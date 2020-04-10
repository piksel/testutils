using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Piksel.Testing
{
    public static partial class FileUtils
	{
        public class TempDir : IDisposable
		{
			public string Fullpath => Info.FullName;

			public bool Exists => Info.Exists;

			public DirectoryInfo Info { get; }

			public TempDir()
			{
				Info = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));
				Info.Create();
			}

			public FileInfo[] GetFiles() => Info.GetFiles();
			public FileInfo[] GetFiles(string searchPattern) => Info.GetFiles(searchPattern);
			public FileInfo[] GetFiles(string searchPattern, SearchOption searchOption) => Info.GetFiles(searchPattern, searchOption);

			/// <summary>
			/// Create a new empty file with a randomly generated name
			/// </summary>
			/// <returns></returns>
			public FileInfo CreateDummyFile()
				=> CreateDummyFile(GetDummyFileName(), DummyContent.Empty);

			/// <summary>
			/// Create a new empty file with the file name <paramref name="name"/>
			/// </summary>
			/// <returns></returns>
			public FileInfo CreateDummyFile(string name)
				=> CreateDummyFile(name, DummyContent.Empty);

			/// <summary>
			/// Create a new file containing <paramref name="content"/> with a randomly generated name
			/// </summary>
			/// <returns></returns>
			public FileInfo CreateDummyFile(DummyContent content)
				=> CreateDummyFile(GetDummyFileName(), content);

			/// <summary>
			/// Create a new file containing <paramref name="content"/> with the file name <paramref name="name"/>
			/// </summary>
			/// <returns></returns>
			public FileInfo CreateDummyFile(string name, DummyContent content)
			{
				var dummyFile = new FileInfo(Path.Combine(Info.FullName, name));
				WriteDummyData(dummyFile, content);
				return dummyFile;
			}

			public DirectoryInfo CreateSubdirectory(string path) => Info.CreateSubdirectory(path);

			public static string operator /(TempDir dir, string path)
				=> Path.Combine(dir.Info.FullName, path);


			public static implicit operator DirectoryInfo(TempDir tempDir)
				=> tempDir.Info;

			#region IDisposable Support

			private bool disposed = false; // To detect redundant calls

			[SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Should not throw in Dispose")]
			protected virtual void Dispose(bool disposing)
			{
				if (!disposed)
				{
					if (disposing && Directory.Exists(Fullpath))
					{
						try
						{
							Info.Delete(true);
						}
						catch { }
					}

					disposed = true;
				}
			}

			public void Clear()
			{
				foreach (var item in Info.GetFileSystemInfos())
				{
					if(item is DirectoryInfo di)
					{
						di.Delete(true);
					}
					else
					{
						item.Delete();
					}
				}
			}

			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			#endregion IDisposable Support

			
		}
	}
}
