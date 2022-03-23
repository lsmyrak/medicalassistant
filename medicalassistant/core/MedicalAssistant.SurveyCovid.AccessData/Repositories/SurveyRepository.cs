using MedicalAssistant.SurveyCovid.Context;
using MedicalAssistant.SurveyCovid.Entitis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.AccessData.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {

        private readonly SurveyCovidContext _context;

        public SurveyRepository(SurveyCovidContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Survey survey)
        {
            _context.Survey.Add(survey);
            await _context.SaveChangesAsync();
        }

        public async Task<Survey> GetAsync(Guid id)
        {
            return await _context.Survey.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyCollection<Survey>> GetAsync()
        {
            return await _context.Survey.Include(survey => survey.Product).ToArrayAsync();
        }

        public async Task UpdateAsync(Survey survey)
        {
            _context.Survey.Update(survey);
            await _context.SaveChangesAsync();
        }
    }
}
