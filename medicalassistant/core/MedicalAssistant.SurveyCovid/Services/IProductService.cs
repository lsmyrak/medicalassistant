using MedicalAssistant.SurveyCovid.Contracts.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.Services
{
    public interface IProductService
    {
        Task AddNewProduct(ProductDto productDto);
        Task DeleteProduct(Guid id);
        Task UpdateProduct(ProductDto productDto);
        Task<ProductDto> Get(Guid id);
        Task<IQueryable<ProductDto>> Get(ProductDtoFilter productDtoFilter);
        Task<IQueryable<ProductDto>> GetActive(ProductDtoFilter productDtoFilter);
    }
}