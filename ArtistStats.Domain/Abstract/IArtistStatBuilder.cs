using ArtistStats.Domain.Model;
using System.Threading.Tasks;

namespace ArtistStats.Domain.Abstract
{
    public interface IArtistStatBuilder
    {
        IArtistStatBuilder GetArtist(string name);

        IArtistStatBuilder GetLyrics();

        IArtistStatBuilder GetLyrics(string name);

        Task<ArtistStat> Build();
    }
}
