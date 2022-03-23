using MedicalAssistant.SurveyCovid.Context;
using MedicalAssistant.SurveyCovid.Entitis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.AccessData.Repositories
{
    class UserRepository : IUserRepository
    {

        private readonly SurveyCovidContext _context;


        public UserRepository(SurveyCovidContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

        }

        public async Task Delete(Guid id)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyCollection<User>> GetAsync()
        {
            return await _context.User.ToArrayAsync();
        }

        public async Task<User> GetAsync(string login)
        {
            return await _context.User
.Include(user => user.Role)
.Include(user => user.RefreshTokens)
.SingleOrDefaultAsync(user => user.Login == login);
        }

        public async Task<User> GetForRefreshToken(string refreshToken)
        {
            return await _context.User
.Include(user => user.Role)
.Include(user => user.RefreshTokens)
.SingleOrDefaultAsync(user => user.RefreshTokens.Any(rt => rt.Token == refreshToken));
        }

        public async Task UpdateAsync(User user)
        {
            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
