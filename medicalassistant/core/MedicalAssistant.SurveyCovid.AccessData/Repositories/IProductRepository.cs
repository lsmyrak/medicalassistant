using MedicalAssistant.SurveyCovid.Entitis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.AccessData.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(Guid Id);
        Task<IReadOnlyCollection<Product>> GetAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task UpdateRangeAsync(IEnumerable<Product> productList);
        Task Delete(Guid Id);
    }
}
