using MedicalAssistant.SurveyCovid.Entitis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.AccessData.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string login);

        Task<User> GetForRefreshToken(string refreshToken);
        Task<IReadOnlyCollection<User>> GetAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task Delete(Guid user);
    }
}
