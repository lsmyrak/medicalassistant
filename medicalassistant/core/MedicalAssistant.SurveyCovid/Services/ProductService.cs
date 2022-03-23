using AutoMapper;
using MedicalAssistant.SurveyCovid.AccessData.Repositories;
using MedicalAssistant.SurveyCovid.Contracts.Dto;
using MedicalAssistant.SurveyCovid.Entitis;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task AddNewProduct(ProductDto productDto)
        {
            await _productRepository.AddAsync(_mapper.Map<Product>(productDto));
        }

        public async Task DeleteProduct(Guid id)
        {
            await _productRepository.Delete(id);
        }

        public async Task UpdateProduct(ProductDto productDto)
        {
            await _productRepository.UpdateAsync(_mapper.Map<Product>(productDto));
        }
        public async Task<ProductDto> Get(Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            return _mapper.Map<ProductDto>(product);
        }
        public async Task<IQueryable<ProductDto>> Get(ProductDtoFilter productDtoFilter)
        {
            var productList = await _productRepository.GetAsync();
            var products = productList.AsQueryable();
            if(!string.IsNullOrWhiteSpace(productDtoFilter.ProductCode))
            {
                products = products.Where(x => x.ProductCode != null);
                products = products.Where(x => x.ProductCode.Contains(productDtoFilter.ProductCode));
            }
            if (!string.IsNullOrWhiteSpace(productDtoFilter.ProductName))
            {
                products = products.Where(x => x.ProductName != null);
                products = products.Where(x => x.ProductName.Contains(productDtoFilter.ProductName));
            }

            return products.Select(_mapper.Map<ProductDto>).AsQueryable();
        }

        public async Task<IQueryable<ProductDto>> GetActive(ProductDtoFilter productDtoFilter)
        {
            var productList = await _productRepository.GetAsync();
            return productList.Where(x=>x.Status).Select(_mapper.Map<ProductDto>).AsQueryable();
        }
    }
}
