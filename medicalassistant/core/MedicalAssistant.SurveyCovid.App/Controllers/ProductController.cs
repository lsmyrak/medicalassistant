using LightQuery;
using LightQuery.Client;
using MedicalAssistant.AspNetCommon.Attributes;
using MedicalAssistant.Common.Constants;
using MedicalAssistant.SurveyCovid.Contracts.Dto;
using MedicalAssistant.SurveyCovid.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.App.Controllers
{
    [Route("api/[controller]")]
    [AllowedRoles(UserRoles.Admin)]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [LightQuery]
        [ProducesResponseType(typeof(PaginationResult<ProductDto>), 200)]
        public async Task<ActionResult<IQueryable<ProductDto>>> GetAll([FromQuery] ProductDtoFilter productDtoFilter)
        {
            var productListDto = await _productService.Get(productDtoFilter);
            return Ok(productListDto);
        }

        [HttpGet("active")]
        [LightQuery]
        [ProducesResponseType(typeof(PaginationResult<ProductDto>), 200)]
        public async Task<ActionResult<IQueryable<ProductDto>>> GetActive([FromQuery] ProductDtoFilter productDtoFilter)
        {
            var productListDto = await _productService.Get(productDtoFilter);
            return Ok(productListDto);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(Guid id)
        {
            var productDto = await _productService.Get(id);
            return Ok(productDto);
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _productService.AddNewProduct(productDto);
            return Ok();
        }

        [HttpPost("edit")]
        public async Task<ActionResult> EditProduct([FromBody] ProductDto productDto)
        {
            await _productService.UpdateProduct(productDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteProduct(id);
            return Ok();
        }
    }
}
