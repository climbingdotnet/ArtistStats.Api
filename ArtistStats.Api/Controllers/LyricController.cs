using System.Net;
using System.Threading.Tasks;
using ArtistStats.Domain.Abstract;
using ArtistStats.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace ArtistStats.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LyricController : ControllerBase
    {
        private readonly IRepository<Lyric, LyricIdentifier> lyricRepository;

        public LyricController(IRepository<Lyric, LyricIdentifier> lyricRepository)
        {
            this.lyricRepository = lyricRepository;
        }

        [HttpGet("{artist}/{song}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string artist, string song) 
        {
            return Ok(await this.lyricRepository.Get(new LyricIdentifier(artist, song)));
        }
    }
}
