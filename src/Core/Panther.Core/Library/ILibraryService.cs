using Panther.Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Panther.Core.Library
{
    public interface ILibraryService
    {
        Task<List<Album>> GetAlbumsAsync();
        Task<List<Artist>> GetArtistsAsync();
        Task<List<Playlist>> GetPlaylistsAsync();
        Task<List<Song>> GetSongsAsync();

        Task AddSongAsync(string fileName);
        Task AddSongAsync(string fileName, Stream songStream);

        Task ReinitializeAsync();

        Task RemoveAlbumAsync(long albumId);
        Task RemoveAlbumAsync(Album album);

        Task RemoveArtistAsync(long artistId);
        Task RemoveArtistAsync(Artist artist);

        Task RemovePlaylistAsync(long playlistId);
        Task RemovePlaylistAsync(Playlist playlist);

        Task RemoveSongAsync(long songId);
        Task RemoveSongAsync(Song song);
    }
}
