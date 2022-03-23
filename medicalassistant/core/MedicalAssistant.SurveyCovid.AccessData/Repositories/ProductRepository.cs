using MedicalAssistant.SurveyCovid.Context;
using MedicalAssistant.SurveyCovid.Entitis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.AccessData.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly SurveyCovidContext _context;

        public ProductRepository(SurveyCovidContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var product = _context.Product.FirstOrDefault(x => x.Id == id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetAsync(Guid Id)
        {
            return await _context.Product.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IReadOnlyCollection<Product>> GetAsync()
        {
            return await _context.Product.ToArrayAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Product.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<Product> productsList)
        {
            _context.Product.UpdateRange(productsList);
            await _context.SaveChangesAsync();
        }
    }
}
