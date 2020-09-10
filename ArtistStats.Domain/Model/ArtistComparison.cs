using System.Collections.Generic;

namespace ArtistStats.Domain.Model
{
    public class ArtistComparison
    {
        public string MostSongs { get; set; }
        public string ShortestSong { get; set; }
        public string LongestSong { get; set; }
        public string HighestAverageWordCount { get; set; }
    }
}
