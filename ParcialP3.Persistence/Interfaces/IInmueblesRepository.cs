using ParcialP3.Domain.Entities;
using ParcialP3.Persistence.Base;

namespace ParcialP3.Persistence.Interfaces
{
    public interface IInmueblesRepository : IBaseRepository<Inmuebles>
    {
        Task<List<Inmuebles>> GetAllWitchIncludes();
    }
}

