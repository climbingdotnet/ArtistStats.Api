using ArtistStats.Domain.Abstract;
using ArtistStats.Domain.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArtistStats.Domain.Concrete
{
    public class ArtistRepository : IRepository<Artist, string>
    {
        private readonly HttpClient client;
        public ArtistRepository(IHttpClientFactory clientFactory)
        {
            this.client = clientFactory.CreateClient("musicbrainz");
        }

        public async Task<Artist> Get(string identifier)
        {
            var artist = await GetArtist(identifier);
            artist.Songs = await GetSongs(artist.Id);

            return artist;
        }

        private async Task<Artist> GetArtist(string identifier) 
        {
            var response = await this.client.GetAsync($"artist?query=name={identifier}&fmt=json&inc");
            var raw = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            var artist = new Artist()
            {
                Id = new Guid((string)raw["artists"][0]["id"]),
                Name = (string)raw["artists"][0]["name"]
            };

            return artist;
        }

        private async Task<IEnumerable<string>> GetSongs(Guid id) 
        {
            var response = await this.client.GetAsync($"release?artist={id}&fmt=json");
            var raw = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            return raw["releases"].Select(x => (string)x["title"]).ToList().Distinct();
        }
    }
}
