using MedicalAssistant.SurveyCovid.Entitis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.AccessData.Repositories
{
    public interface ISurveyRepository
    {
        Task<Survey> GetAsync(Guid id);
        Task<IReadOnlyCollection<Survey>> GetAsync();
        Task AddAsync(Survey survey);
        Task UpdateAsync(Survey survey);
    }
}
