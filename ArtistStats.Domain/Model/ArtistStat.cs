using System.Collections.Generic;

namespace ArtistStats.Domain.Model
{
    public class ArtistStat
    {
        public string ArtistName { get; set; }
        public IEnumerable<string> SongConsidered { get; set; }
        public IEnumerable<string> SongsNotConsidered { get; set; }
        public int? MinWordCount { get; set; }
        public int? MaxWordCount { get; set; }
        public double? AverageWordCount { get; set; }
    }
}
