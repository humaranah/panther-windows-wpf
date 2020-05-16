using Panther.Core.Models;
using System;
using System.Threading.Tasks;

namespace Panther.Core.Data
{
    public class LibraryUnitOfWork : IDisposable, IAsyncDisposable
    {
        private readonly LibraryDbContext _context;

        private bool _isDisposed = false;

        public LibraryUnitOfWork(
            LibraryDbContext context,
            IRepository<Album> albumRepository,
            IRepository<Artist> artistRepository,
            IRepository<Playlist> playlistRepository,
            IRepository<Song> songRepository)
        {
            _context = context;
            AlbumRepository = albumRepository;
            ArtistRepository = artistRepository;
            PlaylistRepository = playlistRepository;
            SongRepository = songRepository;
        }

        public IRepository<Album> AlbumRepository { get; }
        public IRepository<Artist> ArtistRepository { get; }
        public IRepository<Playlist> PlaylistRepository { get; }
        public IRepository<Song> SongRepository { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}
