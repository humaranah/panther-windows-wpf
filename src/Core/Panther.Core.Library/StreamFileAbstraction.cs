using System;
using System.IO;
using static TagLib.File;

namespace Panther.Core.Library
{
    public sealed class StreamFileAbstraction : IFileAbstraction, IDisposable
    {
        public StreamFileAbstraction(string name, Stream fileStream)
        {
            Name = name;
            ReadStream = fileStream;
        }

        public string Name { get; }

        public Stream ReadStream { get; }

        public Stream WriteStream => ReadStream;

        public void CloseStream(Stream stream)
        {
            stream.Close();
        }

        public void Dispose()
        {
            ReadStream.Dispose();
        }
    }
}
