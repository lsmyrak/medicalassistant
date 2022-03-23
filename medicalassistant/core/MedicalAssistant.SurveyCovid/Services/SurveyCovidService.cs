using AutoMapper;
using MedicalAssistant.SurveyCovid.AccessData.Repositories;
using MedicalAssistant.SurveyCovid.Contracts.Dto;
using MedicalAssistant.SurveyCovid.Entitis;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.Services
{
    public class SurveyCovidService : ISurveyCovidService
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IMapper _mapper;

        public SurveyCovidService(ISurveyRepository surveyRepository, IMapper mapper)
        {
            _surveyRepository = surveyRepository;
            _mapper = mapper;
        }

        public async Task AddNewSurveyCovid(SurveyDto newSurveyCovidDto)
        {
            await _surveyRepository.AddAsync(_mapper.Map<Survey>(newSurveyCovidDto));
        }

        public async Task DeleteSurveyCovid(Guid id)
        {
            var survery = await _surveyRepository.GetAsync(id);
            survery.SetInactive();
            await _surveyRepository.UpdateAsync(survery);
        }

        public async Task<IQueryable<SurveyDto>> Get(SurveyDtoFilter surveyDtoFilter)
        {
            var surveyListAll = await _surveyRepository.GetAsync();

            var surveyList = surveyListAll.AsQueryable();

            if (!string.IsNullOrWhiteSpace(surveyDtoFilter.Pesel))
            {
                surveyList = surveyList.Where(x => x.Pesel != null);
                surveyList = surveyList.Where(x => x.Pesel.Contains(surveyDtoFilter.Pesel));
            }

            if (!string.IsNullOrWhiteSpace(surveyDtoFilter.AnotherDocument))
            {
                surveyList = surveyList.Where(x => x.AnotherDocument != null);
                surveyList = surveyList.Where(x => x.AnotherDocument.Contains(surveyDtoFilter.AnotherDocument));
            }

            if (!string.IsNullOrWhiteSpace(surveyDtoFilter.SeriesNumber))
            {
                surveyList = surveyList.Where(x => x.SeriesNumber != null);
                surveyList = surveyList.Where(x => x.SeriesNumber.Contains(surveyDtoFilter.SeriesNumber));
            }

            if (surveyDtoFilter.FromDate.HasValue && surveyDtoFilter.UntilDate.HasValue)
            {
                surveyList = surveyList.Where(x => x.FromDate >= surveyDtoFilter.FromDate.Value && x.UntilDate <= surveyDtoFilter.UntilDate.Value);
            }

            return surveyList.Select(_mapper.Map<SurveyDto>).AsQueryable();
        }

        public async Task<IQueryable<SurveyDto>> GetActive(SurveyDtoFilter surveyDtoFilter)
        {
            var surveyListAll = await _surveyRepository.GetAsync();

            var surveyList = surveyListAll.AsQueryable();


            if (!string.IsNullOrWhiteSpace(surveyDtoFilter.Pesel))
            {

                surveyList = surveyList.Where(x => x.Pesel.Contains(surveyDtoFilter.Pesel));
            }

            if (!string.IsNullOrWhiteSpace(surveyDtoFilter.AnotherDocument))
            {
                surveyList = surveyList.Where(x => x.AnotherDocument != null);
                surveyList = surveyList.Where(x => x.AnotherDocument.Contains(surveyDtoFilter.AnotherDocument));
            }

            if (!string.IsNullOrWhiteSpace(surveyDtoFilter.SeriesNumber))
            {
                surveyList = surveyList.Where(x => x.SeriesNumber != null);
                surveyList = surveyList.Where(x => x.SeriesNumber.Contains(surveyDtoFilter.SeriesNumber));
            }

            if (surveyDtoFilter.FromDate.HasValue && surveyDtoFilter.UntilDate.HasValue)
            {
                surveyList = surveyList.Where(x => x.FromDate >= surveyDtoFilter.FromDate.Value && x.UntilDate <= surveyDtoFilter.UntilDate.Value);
            }

            surveyList = surveyList.Where(x => x.Status);
            return surveyList.Select(_mapper.Map<SurveyDto>).AsQueryable();
        }

        public async Task<SurveyDto> Get(Guid id)
        {
            var survey = await _surveyRepository.GetAsync(id);
            return _mapper.Map<SurveyDto>(survey);
        }

        public async Task UpdateSurveyCovid(SurveyDto editSurveyCovidDto)
        {
            await _surveyRepository.UpdateAsync(_mapper.Map<Survey>(editSurveyCovidDto));
        }



    }
}
