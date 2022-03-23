using MedicalAssistant.SurveyCovid.Entitis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.AccessData.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department> GetAsync(Guid id);
        Task<IReadOnlyCollection<Department>> GetAsync();
        Task AddAsync(Department department);
        Task UpdateAsync(Department department);
        Task Delete(Guid id);
    }
}
