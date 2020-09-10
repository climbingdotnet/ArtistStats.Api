using ArtistStats.Domain.Abstract;
using ArtistStats.Domain.Extension;
using ArtistStats.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistStats.Domain.Concrete
{
    public class ArtistStatBuilder : IArtistStatBuilder
    {
        private readonly IRepository<Artist, string> artistRepository;
        private readonly IRepository<Lyric, LyricIdentifier> lyricRepository;

        private Task getArtistTask;
        private Task getLyricsTask;

        private Artist artist;
        private List<Lyric> lyrics;

        public ArtistStatBuilder(IRepository<Artist, string> artistRepository, IRepository<Lyric, LyricIdentifier> lyricRepository)
        {
            this.artistRepository = artistRepository;
            this.lyricRepository = lyricRepository;
        }

        public async Task<ArtistStat> Build()
        {
            if (getLyricsTask == null)
            {
                throw new Exception("Must get Lyrics before building stats");
            }

            await this.getLyricsTask;

            if (this.lyrics == null)
            {
                throw new Exception($"No Lyrics found");
            }

            var wordCounts = this.lyrics.WordCounts().Where(x => x.HasValue);

            return new ArtistStat()
            {
                ArtistName = this.artist.Name,
                SongConsidered = lyrics.Where(x => !string.IsNullOrWhiteSpace(x.Lyrics)).Select(x => x.Song),
                SongsNotConsidered = lyrics.Where(x => string.IsNullOrWhiteSpace(x.Lyrics)).Select(x => x.Song),
                AverageWordCount = wordCounts.Average(x => x),
                MaxWordCount = wordCounts.OrderByDescending(x => x).FirstOrDefault(),
                MinWordCount = wordCounts.OrderBy(x => x).FirstOrDefault()
            };
        }

        public IArtistStatBuilder GetArtist(string name)
        {
            this.getArtistTask = Task.Run(async () =>
            {
                this.artist = await this.artistRepository.Get(name);
            });
            return this;
        }

        public IArtistStatBuilder GetLyrics()
        {
            if (getArtistTask == null)
            {
                throw new Exception("Must get an Artist before getting Lyrics");
            }

            this.lyrics = new List<Lyric>();

            this.getLyricsTask = Task.Run(async () =>
            {
                await this.getArtistTask;

                foreach (var song in this.artist.Songs)
                {
                    this.lyrics.Add(await this.lyricRepository.Get(new LyricIdentifier(this.artist.Name, song)));
                }
            });
            return this;
        }

        public IArtistStatBuilder GetLyrics(string name)
        {
            return this.GetArtist(name).GetLyrics();
        }
    }
}
