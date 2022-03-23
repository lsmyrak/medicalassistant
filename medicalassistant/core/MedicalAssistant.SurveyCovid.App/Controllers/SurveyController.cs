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
    [AllowedRoles(UserRoles.Admin, UserRoles.User)]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyCovidService _surveryCovidService;
        public SurveyController(ISurveyCovidService surveryCovidService)
        {
            _surveryCovidService = surveryCovidService;
        }

        [HttpGet("{id}")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse(203)]

        public async Task<ActionResult<SurveyDto>> Get(Guid id)
        {
            var surveryDto = await _surveryCovidService.Get(id);
            if (surveryDto == null)
            {
                return NotFound();
            }
            return Ok(surveryDto);
        }

        [LightQuery(forcePagination: true)]
        [HttpGet]
        [LightQuery]
        [ProducesResponseType(typeof(PaginationResult<SurveyDto>), 200)]
        public async Task<ActionResult<IQueryable<SurveyDto>>> GetAll([FromQuery] SurveyDtoFilter surveyDtoFilter)
        {
            var surveyDtoList = await _surveryCovidService.Get(surveyDtoFilter);
            if(surveyDtoList!=null)
                return Ok(surveyDtoList);
            else
                return NoContent();
        }


        [LightQuery(forcePagination: true)]
        [HttpGet("active")]
        [LightQuery]
        [ProducesResponseType(typeof(PaginationResult<SurveyDto>), 200)]
        public async Task<ActionResult<IQueryable<SurveyDto>>> GetActive([FromQuery] SurveyDtoFilter surveyDtoFilter)
        {
            var surveyDtoList = await _surveryCovidService.GetActive(surveyDtoFilter);
            if(surveyDtoList != null)
                return Ok(surveyDtoList);
            else
                return NoContent();
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddSurveyCovid([FromBody] SurveyDto surveryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _surveryCovidService.AddNewSurveyCovid(surveryDto);
            return Ok();
        }


        [HttpPost("edit/{id}")]
        public async Task<ActionResult> EditSurveyCovid([FromBody] SurveyDto surveyDto)
        {
            await _surveryCovidService.UpdateSurveyCovid(surveyDto);
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteSurveyCovid(Guid id)
        {
            await _surveryCovidService.DeleteSurveyCovid(id);
            return NoContent();
        }
    }
}