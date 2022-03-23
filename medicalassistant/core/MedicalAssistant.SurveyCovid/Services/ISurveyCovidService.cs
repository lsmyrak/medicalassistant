using MedicalAssistant.SurveyCovid.Contracts.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.Services
{
    public interface ISurveyCovidService
    {
        Task AddNewSurveyCovid(SurveyDto newSurveryCovidDto);

        Task UpdateSurveyCovid(SurveyDto editSurveryCovidDto);

        Task DeleteSurveyCovid(Guid id);

        Task<IQueryable<SurveyDto>> Get(SurveyDtoFilter surveyDtoFilter);
        Task<IQueryable<SurveyDto>> GetActive(SurveyDtoFilter surveyDtoFilter);
        Task<SurveyDto> Get(Guid id);

    }
}
