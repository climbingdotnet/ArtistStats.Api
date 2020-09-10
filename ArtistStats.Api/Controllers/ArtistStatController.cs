using System.Net;
using System.Threading.Tasks;
using ArtistStats.Domain.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ArtistStats.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistStatController : ControllerBase
    {
        private readonly IArtistStatBuilder artistStatBuilder;

        public ArtistStatController(IArtistStatBuilder artistStatBuilder)
        {
            this.artistStatBuilder = artistStatBuilder;
        }

        [HttpGet("{artist}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string artist) 
        {
            return Ok(await this.artistStatBuilder.GetArtist(artist).GetLyrics().Build());
        }
    }
}
