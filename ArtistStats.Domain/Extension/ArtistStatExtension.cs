using ArtistStats.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtistStats.Domain.Extension
{
    public static class ArtistStatExtension
    {
        public static ArtistComparison Compare(this ArtistStat a, ArtistStat b)
        {
            var comparison = new ArtistComparison()
            {
                HighestAverageWordCount = a.AverageWordCount > b.AverageWordCount ? a.ArtistName : a.AverageWordCount == b.AverageWordCount ? $"{a.ArtistName} & {b.ArtistName}" : b.ArtistName,
                LongestSong = a.MaxWordCount > b.MaxWordCount ? a.ArtistName : a.MaxWordCount == b.MaxWordCount ? $"{a.ArtistName} & {b.ArtistName}" : b.ArtistName,
                ShortestSong = a.MinWordCount > b.MinWordCount ? a.ArtistName : a.MinWordCount == b.MinWordCount ? $"{a.ArtistName} & {b.ArtistName}" : b.ArtistName,
                MostSongs = a.SongConsidered.Count() > b.SongConsidered.Count() ? a.ArtistName : a.SongConsidered.Count() == b.SongConsidered.Count() ? $"{a.ArtistName} & {b.ArtistName}" : b.ArtistName,
            };

            return comparison;
        }
    }
}
