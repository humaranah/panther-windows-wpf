using Microsoft.EntityFrameworkCore;
using Panther.Core.Models;

namespace Panther.Core.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Playlist> Playlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>()
                .HasKey(song => song.Id);
            modelBuilder.Entity<Song>()
                .HasIndex(song => song.FileName)
                .IsUnique();
            modelBuilder.Entity<Song>()
                .HasOne(song => song.Album)
                .WithMany(album => album.Songs)
                .HasForeignKey(song => song.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Album>()
                .HasKey(album => album.Id);
            modelBuilder.Entity<Album>()
                .HasIndex(album => new { album.Name, album.ArtistId })
                .IsUnique();
            modelBuilder.Entity<Album>()
                .HasOne(album => album.Artist)
                .WithMany(artist => artist.Albums)
                .HasForeignKey(album => album.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Artist>()
                .HasKey(artist => artist.Id);
            modelBuilder.Entity<Artist>()
                .HasIndex(artist => artist.Name)
                .IsUnique();

            modelBuilder.Entity<Playlist>()
                .HasKey(playlist => playlist.Id);

            modelBuilder.Entity<SongPlaylist>()
                .HasKey(songPlaylist => new { songPlaylist.SongId, songPlaylist.PlaylistId });
            modelBuilder.Entity<SongPlaylist>()
                .HasOne(songPlaylist => songPlaylist.Song)
                .WithMany(song => song.SongPlaylists)
                .HasForeignKey(songPlaylist => songPlaylist.SongId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SongPlaylist>()
                .HasOne(songPlaylist => songPlaylist.Playlist)
                .WithMany(playlist => playlist.SongPlaylists)
                .HasForeignKey(songPlaylist => songPlaylist.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
