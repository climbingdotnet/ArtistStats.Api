namespace ArtistStats.Domain.Model
{
    public class LyricIdentifier
    {
        public LyricIdentifier(string artist, string song)
        {
            this.Artist = artist;
            this.Song = song;
        }

        public string Artist { get; set; }

        public string Song { get; set; }
    }
}
