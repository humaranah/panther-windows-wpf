using Panther.Core.Models;
using System.Collections.Generic;

namespace Panther.Core.Library.Tests.TestData
{
    public static class LibraryContextTestData
    {
        public static IEnumerable<object[]> TestData
        {
            get => new List<object[]>
            {
                new object[] { new Artist{ Id = 0, Name = "Dummy artist" } },
                new object[] { new Album { Id = 0, ArtistId = 0, Name = "Dummy album"} },
                new object[] { new Song { Id = 0, AlbumId = 0, FileName = "dummy.mp3"} }
            };
        }
    }
}
