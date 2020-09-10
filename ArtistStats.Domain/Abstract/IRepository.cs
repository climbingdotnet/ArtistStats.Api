using System.Threading.Tasks;

namespace ArtistStats.Domain.Abstract
{
    public interface IRepository<TModel, TIdentifier>
    {
        Task<TModel> Get(TIdentifier identifier);
    }
}
