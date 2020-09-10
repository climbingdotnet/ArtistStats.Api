using ArtistStats.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace ArtistStats.Domain.Extension
{
    public static class LyricExtension
    {
        public static int? WordCount(this Lyric lyric) 
        {
            if (string.IsNullOrWhiteSpace(lyric.Lyrics)) return null;

            var count = 1; // Previous checks means there's at least one word.
            var text = lyric.Lyrics.Trim().Replace(@"\n", "");

            foreach (char c in text.ToCharArray()) 
            {
                if (char.IsWhiteSpace(c)) 
                {
                    count++;
                }
            }

            return count;

        }

        public static IEnumerable<int?> WordCounts(this IEnumerable<Lyric> lyric)
        {
            return lyric.Select(x => x.WordCount());

        }
    }
}
