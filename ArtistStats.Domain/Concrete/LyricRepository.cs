using ArtistStats.Domain.Abstract;
using ArtistStats.Domain.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArtistStats.Domain.Concrete
{
    public class LyricRepository : IRepository<Lyric, LyricIdentifier>
    {
        private readonly HttpClient client;

        public LyricRepository(IHttpClientFactory clientFactory)
        {
            this.client = clientFactory.CreateClient("lyricsovh");
        }

        public async Task<Lyric> Get(LyricIdentifier identifier)
        {
            var lyric = new Lyric()
            {
                Artist = identifier.Artist,
                Song = identifier.Song
            };

            var response = await this.client.GetAsync($"{identifier.Artist}/{identifier.Song}");

            try
            {
                var raw = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

                lyric.Lyrics = (string)raw["lyrics"];
            }
            catch // ignore errors and leave as null
            {
            }

            return lyric;
        }
    }
}
