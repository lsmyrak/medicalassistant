using MedicalAssistant.SurveyCovid.Context;
using MedicalAssistant.SurveyCovid.Entitis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.AccessData.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SurveyCovidContext _context;


        public RoleRepository(SurveyCovidContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Role role)
        {
            _context.Role.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var role = _context.Role.FirstOrDefault(x => x.Id == id);
            _context.Role.Remove(role);
            await _context.SaveChangesAsync();
        }

        public async Task<Role> GetAsync(Guid id)
        {
            return await _context.Role.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyCollection<Role>> GetAsync()
        {
            return await _context.Role.ToArrayAsync();
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Role.Update(role);
            await _context.SaveChangesAsync();
        }
    }
}
