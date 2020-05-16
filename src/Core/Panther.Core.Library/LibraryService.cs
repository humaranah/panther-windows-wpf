using Microsoft.Extensions.Options;
using Panther.Core.Data;
using Panther.Core.Library.Settings;
using Panther.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Panther.Core.Library
{
    public class LibraryService : ILibraryService
    {
        private readonly IMusicExaminerService _examiner;
        private readonly IOptions<LibrarySettings> _settings;
        private readonly LibraryDbContext _dbContext;

        private readonly IRepository<Album> _albumRepository;
        private readonly IRepository<Artist> _artistRepository;
        private readonly IRepository<Playlist> _playlistRepository;
        private readonly IRepository<Song> _songRepository;

        public LibraryService(
            IMusicExaminerService examiner,
            IOptions<LibrarySettings> settings,
            LibraryDbContext dbContext,
            IRepository<Album> albumRepository,
            IRepository<Artist> artistRepository,
            IRepository<Playlist> playlistRepository,
            IRepository<Song> songRepository)
        {
            _examiner = examiner;
            _settings = settings;
            _dbContext = dbContext;

            _albumRepository = albumRepository;
            _artistRepository = artistRepository;
            _playlistRepository = playlistRepository;
            _songRepository = songRepository;
        }

        #region Accessor methods
        public Task<List<Album>> GetAlbumsAsync() => _albumRepository.GetAsync();
        public Task<List<Artist>> GetArtistsAsync() => _artistRepository.GetAsync();
        public Task<List<Playlist>> GetPlaylistsAsync() => _playlistRepository.GetAsync();
        public Task<List<Song>> GetSongsAsync() => _songRepository.GetAsync();
        #endregion


        #region Operations
        public Task AddSongAsync(string fileName)
        {
            var taglibFile = TagLib.File.Create(fileName);
            return AddSongAsync(taglibFile);
        }

        public Task AddSongAsync(string fileName, Stream fileStream)
        {
            var fileAbstraction = new StreamFileAbstraction(fileName, fileStream);
            var taglibFile = TagLib.File.Create(fileAbstraction);
            return AddSongAsync(taglibFile);
        }

        public async Task RemoveAlbumAsync(long albumId)
        {
            var album = await _albumRepository.GetByIdAsync(albumId);
            await RemoveAlbumAsync(album);
        }

        public async Task RemoveAlbumAsync(Album album)
        {
            var artistId = album.Id;
            _albumRepository.Remove(album);

            await RemoveArtistIfEmptyAsync(artistId);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveArtistAsync(long artistId)
        {
            _artistRepository.Remove(artistId);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveArtistAsync(Artist artist)
        {
            _artistRepository.Remove(artist);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemovePlaylistAsync(long playlistId)
        {
            _playlistRepository.Remove(playlistId);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemovePlaylistAsync(Playlist playlist)
        {
            _playlistRepository.Remove(playlist);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ReinitializeAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.Database.EnsureCreatedAsync();

            await Task.WhenAll(
                _examiner.ExamineAsync(
                    _settings.Value.SearchLocations, AddSongAsync, cancellationToken));
        }

        public async Task RemoveSongAsync(long songId)
        {
            var song = await _songRepository.GetByIdAsync(songId);
            await RemoveSongAsync(song);
        }

        public async Task RemoveSongAsync(Song song)
        {
            var albumId = song.AlbumId;
            _songRepository.Remove(song);

            await RemoveAlbumIfEmptyAsync(albumId);
            await _dbContext.SaveChangesAsync();
        }
        #endregion


        #region Private methods
        private async Task AddSongAsync(TagLib.File taglibFile)
        {
            var song = new Song
            {
                FileName = taglibFile.Name,
                Name = taglibFile.Tag?.Title ?? taglibFile.Name,
                Length = taglibFile.Length,
                TrackNumber = taglibFile.Tag?.Track ?? 0,

                Album = new Album
                {
                    Name = taglibFile.Tag?.Album ?? "No album",
                    EncodedImage = Convert.ToBase64String(taglibFile.Tag.Pictures[0].Data.Data),
                    Year = taglibFile.Tag?.Year ?? 0,

                    Artist = new Artist
                    {
                        Name = string.Join(", ", taglibFile.Tag?.Performers)
                    }
                }
            };

            await _songRepository.InsertAsync(song);
            await _dbContext.SaveChangesAsync();
        }

        private async Task RemoveAlbumIfEmptyAsync(long albumId)
        {
            var albumSongCount = await _songRepository
                .CountAsync(song => song.AlbumId == albumId);

            if (albumSongCount == 0)
            {
                var artistId = (await _albumRepository.GetByIdAsync(albumId)).ArtistId;
                _albumRepository.Remove(albumId);
                await RemoveArtistIfEmptyAsync(artistId);
            }

        }

        private async Task RemoveArtistIfEmptyAsync(long artistId)
        {
            var artistAlbumCount = await _albumRepository
                .CountAsync(album => album.ArtistId == artistId);

            if (artistAlbumCount == 0)
            {
                _artistRepository.Remove(artistId);
            }
        }
        #endregion
    }
}
