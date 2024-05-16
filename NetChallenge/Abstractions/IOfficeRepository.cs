using NetChallenge.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetChallenge.Abstractions
{
    public interface IOfficeRepository : IRepository<Office>
    {
        Task<List<Office>> Get(string locationName);
    }
}