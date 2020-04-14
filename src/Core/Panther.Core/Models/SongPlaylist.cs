namespace Panther.Core.Models
{
    public class SongPlaylist
    {
        #region Primary and foreign keys
        /// <summary>
        /// Song Id
        /// </summary>
        public long SongId { get; set; }

        /// <summary>
        /// Playlist Id
        /// </summary>
        public long PlaylistId { get; set; }
        #endregion


        #region Navigation Properties
        /// <summary>
        /// Song
        /// </summary>
        public Song Song { get; set; }

        /// <summary>
        /// Playlist
        /// </summary>
        public Playlist Playlist { get; set; }
        #endregion
    }
}
