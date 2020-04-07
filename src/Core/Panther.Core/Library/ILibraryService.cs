using Panther.Core.Models;
using System.Collections.Generic;

namespace Panther.Core.Library
{
    public interface ILibraryService
    {
        List<Album> Albums { get; }
        List<Artist> Artists { get; }
        List<Song> Songs { get; }
        List<Playlist> Playlists { get; }
    }
}
