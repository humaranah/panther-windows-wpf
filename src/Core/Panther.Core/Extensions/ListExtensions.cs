using System;
using System.Collections.Generic;

namespace Panther.Core.Extensions
{
    public static class ListExtensions
    {
        public static void Shuffle<TItem>(this List<TItem> list)
        {
            var random = new Random();
            var temp = new List<TItem>();
            while (list.Count > 0)
            {
                var index = random.Next(list.Count);
                temp.Add(list[index]);
                list.RemoveAt(index);
            }

            list.AddRange(temp);
        }

        public static void RandomInsert<TItem>(this List<TItem> list, TItem item)
        {
            var randomIndex = new Random().Next(list.Count);
            list.Insert(randomIndex, item);
        }
    }
}
