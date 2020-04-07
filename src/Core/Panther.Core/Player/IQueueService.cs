using Panther.Core.Models;
using System.Collections.Generic;

namespace Panther.Core.Player
{
    public interface IQueueService
    {
        Song Current { get; }
        int CurrentIndex { get; }
        bool Repeat { get; set; }
        bool Shuffle { get; set; }
        IReadOnlyList<Song> Songs { get; }

        void Add(Song song);
        void Add(IEnumerable<Song> songs);
        void Clear();
        void Insert(int index, Song song);
        Song Jump(int i);
        Song Next();
        Song Prev();
        void Remove(Song song);
        void RemoveAt(int index);
    }
}
