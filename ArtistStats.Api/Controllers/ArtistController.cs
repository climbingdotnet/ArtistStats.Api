using System.Net;
using System.Threading.Tasks;
using ArtistStats.Domain.Abstract;
using ArtistStats.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace ArtistStats.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IRepository<Artist, string> artistRepository;

        public ArtistController(IRepository<Artist, string> artistRepository)
        {
            this.artistRepository = artistRepository;
        }

        [HttpGet("{identifier}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string identifier) 
        {
            return Ok(await this.artistRepository.Get(identifier));
        }
    }
}
