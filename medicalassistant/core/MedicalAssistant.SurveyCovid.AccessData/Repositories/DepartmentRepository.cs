using MedicalAssistant.SurveyCovid.Context;
using MedicalAssistant.SurveyCovid.Entitis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.AccessData.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly SurveyCovidContext _context;

        public DepartmentRepository(SurveyCovidContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Department department)
        {
            _context.Department.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var department = await _context.Department.FirstOrDefaultAsync(x => x.Id == id);
            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
        }

        public async Task<Department> GetAsync(Guid id)
        {
            return await _context.Department.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyCollection<Department>> GetAsync()
        {
            return await _context.Department.ToArrayAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            _context.Department.Update(department);
            await _context.SaveChangesAsync();
        }
    }
}
