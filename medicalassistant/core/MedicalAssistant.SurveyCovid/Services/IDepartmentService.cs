using MedicalAssistant.SurveyCovid.Contracts.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.Services
{
    public interface IDepartmentService
    {
        Task AddDepartment(DepartmentDto departamentDto);
        Task DeleteDepartment(Guid id);
        Task UpdateDepartment(DepartmentDto departamentDto);
        Task<DepartmentDto> Get(Guid id);
        Task<IQueryable<DepartmentDto>> Get(DepartmentDtoFilter  departmentDtoFilter);
        Task<IQueryable<DepartmentDto>> GetActive(DepartmentDtoFilter departmentDtoFilter);
    }
}
