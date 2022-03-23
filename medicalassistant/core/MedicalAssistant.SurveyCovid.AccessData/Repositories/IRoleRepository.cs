using MedicalAssistant.SurveyCovid.Entitis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.AccessData.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> GetAsync(Guid id);
        Task<IReadOnlyCollection<Role>> GetAsync();
        Task AddAsync(Role role);
        Task UpdateAsync(Role role);
        Task Delete(Guid id);
    }
}
