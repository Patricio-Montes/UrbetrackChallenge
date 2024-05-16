using NetChallenge.Domain;
using System.Collections.Generic;

namespace NetChallenge.Abstractions
{
    public interface IOfficeRepository : IRepository<Office>
    {
        List<Office> Get(string locationName);
    }
}