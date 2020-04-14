using Microsoft.Extensions.Options;
using Panther.Core.Extensions;
using Panther.Core.Models;
using Panther.Core.Player.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Panther.Core.Player
{
    public class QueueService : IQueueService
    {
        private readonly List<int> _indexMap;
        private readonly List<Song> _queue;
        private bool _shuffle;

        public QueueService(IOptions<QueueSettings> settings)
        {
            _queue = new List<Song>();
            _indexMap = new List<int>();

            Repeat = settings.Value.Repeat;
            Shuffle = settings.Value.Shuffle;
        }

        public Song Current => _queue[_indexMap[CurrentIndex]];

        public int CurrentIndex { get; private set; }

        public bool Repeat { get; set; }

        public bool Shuffle
        {
            get => _shuffle;
            set
            {
                if (_shuffle == value)
                {
                    return;
                }

                _shuffle = value;
                if (_shuffle)
                {
                    _indexMap.Shuffle();
                    return;
                }

                _indexMap.Clear();
                _indexMap.AddRange(_queue.Select((song, i) => i));
            }
        }

        public IReadOnlyList<Song> Songs => _indexMap.Select(i => _queue[i]).ToList();

        public void Add(Song song)
        {
            _queue.Add(song);
            if (Shuffle)
            {
                _indexMap.RandomInsert(_indexMap.Count);
                return;
            }

            _indexMap.Add(_indexMap.Count);
        }

        public void Add(IEnumerable<Song> songs)
        {
            _queue.AddRange(songs);
            for (int i = _indexMap.Count; i < _queue.Count; i++)
            {
                if (Shuffle)
                {
                    _indexMap.RandomInsert(i);
                    continue;
                }

                _indexMap.Add(i);
            }
        }

        public void Clear()
        {
            _queue.Clear();
            _indexMap.Clear();
        }

        public void Insert(int index, Song song)
        {
            _queue.Insert(index, song);
            _indexMap.Insert(index, index);
        }

        public Song Jump(int toIndex)
        {
            if (toIndex < 0 || toIndex >= _queue.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(toIndex));
            }

            CurrentIndex = toIndex;
            return Current;
        }

        public Song Next()
        {
            CurrentIndex += 1;
            if (CurrentIndex < _queue.Count)
            {
                return Current;
            }

            if (Shuffle && Repeat)
            {
                _indexMap.Shuffle();
            }

            CurrentIndex = 0;
            return Current;
        }

        public Song Prev()
        {
            CurrentIndex -= 1;
            if (CurrentIndex >= 0)
            {
                return Current;
            }

            if (!Repeat)
            {
                CurrentIndex = 0;
                return Current;
            }

            CurrentIndex = _indexMap.Count - 1;
            if (Shuffle)
            {
                _indexMap.Shuffle();
            }

            return Current;
        }

        public void Remove(Song song)
        {
            if (!_queue.Contains(song))
            {
                return;
            }

            var index = _queue.FindIndex(s => s.Equals(song));
            _queue.Remove(song);
            _indexMap.Remove(index);
        }

        public void RemoveAt(int index)
        {
            _queue.RemoveAt(index);
            _indexMap.Remove(index);
        }
    }
}
