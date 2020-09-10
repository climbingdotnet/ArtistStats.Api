using System.Net;
using System.Threading.Tasks;
using ArtistStats.Domain.Abstract;
using ArtistStats.Domain.Concrete;
using ArtistStats.Domain.Extension;
using ArtistStats.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace ArtistStats.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistComparisonController : ControllerBase
    {
        private readonly IRepository<Artist, string> artistRepository;
        private readonly IRepository<Lyric, LyricIdentifier> lyricRepository;

        public ArtistComparisonController(IRepository<Artist, string> artistRepository, IRepository<Lyric, LyricIdentifier> lyricRepository)
        {
            this.artistRepository = artistRepository;
            this.lyricRepository = lyricRepository;
        }

        [HttpGet("{artistA}/{artistB}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string artistA, string artistB) 
        {
            var builderA = Task.Run(() => new ArtistStatBuilder(this.artistRepository, this.lyricRepository).GetLyrics(artistA).Build());
            var builderB = Task.Run(() => new ArtistStatBuilder(this.artistRepository, this.lyricRepository).GetLyrics(artistB).Build());

            return Ok((await builderA).Compare(await builderB));
        }
    }
}
