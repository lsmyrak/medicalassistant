using AutoMapper;
using MedicalAssistant.SurveyCovid.AccessData.Repositories;
using MedicalAssistant.SurveyCovid.Contracts.Dto;
using MedicalAssistant.SurveyCovid.Entitis;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.Services
{
    public class DepartmentService : IDepartmentService
    {
        public readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task AddDepartment(DepartmentDto departmentDto)
        {
            await _departmentRepository.AddAsync(_mapper.Map<Department>(departmentDto));

        }

        public async Task DeleteDepartment(Guid id)
        {
            await _departmentRepository.Delete(id);
        }

        public async Task<DepartmentDto> Get(Guid id)
        {
            var department = await _departmentRepository.GetAsync(id);
            return _mapper.Map<DepartmentDto>(department);
        }
        public async Task<IQueryable<DepartmentDto>> GetActive(DepartmentDtoFilter departmentDtoFilter)
        {
            var departmentList = await _departmentRepository.GetAsync();

            var departments = departmentList.AsQueryable();

            if (!string.IsNullOrWhiteSpace(departmentDtoFilter.Name))
            {
                departments = departments.Where(x => x.Name.Contains(departmentDtoFilter.Name));
            }
            return departments.Select(_mapper.Map<DepartmentDto>).AsQueryable();
        }
        public async Task<IQueryable<DepartmentDto>> Get(DepartmentDtoFilter departmentDtoFilter)
        {
            var departmentList = await _departmentRepository.GetAsync();
      
            var departments = departmentList.AsQueryable();
        
            if(!string.IsNullOrWhiteSpace(departmentDtoFilter.Name))
            {
                departments = departments.Where(x => x.Name.Contains(departmentDtoFilter.Name));
            }
            return departments.Select(_mapper.Map<DepartmentDto>).AsQueryable();
        }  

        public async Task UpdateDepartment(DepartmentDto departamentDto)
        {
            await _departmentRepository.UpdateAsync(_mapper.Map<Department>(departamentDto));
        }
    }
}
