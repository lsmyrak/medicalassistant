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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        [LightQuery]
        [ProducesResponseType(typeof(PaginationResult<DepartmentDto>), 200)]
        public async Task<ActionResult<IQueryable<DepartmentDto>>> GetAll([FromQuery] DepartmentDtoFilter departmentDtoFilter)
        {
            var departmentDtoList = await _departmentService.Get(departmentDtoFilter);
            return Ok(departmentDtoList);
        }

        [HttpGet("active")]
        [LightQuery]
        [ProducesResponseType(typeof(PaginationResult<DepartmentDto>), 200)]
        public async Task<ActionResult<IQueryable<DepartmentDto>>> GetActive([FromQuery] DepartmentDtoFilter departmentDtoFilter)
        {
            var departmentDtoList = await _departmentService.GetActive(departmentDtoFilter);
            return Ok(departmentDtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> Get(Guid id)
        {
            var depatmentDto = await _departmentService.Get(id);
            return Ok(depatmentDto);
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddDepartment([FromBody] DepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _departmentService.AddDepartment(departmentDto);
            return Ok();
        }

        [HttpPost("edit")]
        public async Task<ActionResult> EditDepartment([FromBody] DepartmentDto departmentDto)
        {
            await _departmentService.UpdateDepartment(departmentDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            await _departmentService.DeleteDepartment(id);
            return Ok();
        }
    }
}
