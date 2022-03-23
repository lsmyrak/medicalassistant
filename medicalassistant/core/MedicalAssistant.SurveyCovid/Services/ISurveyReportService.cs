using MedicalAssistant.SurveyCovid.Contracts.Dto;
using MedicalAssistant.SurveyCovid.Raports;
using System;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.Services
{
    public interface ISurveyReportService
    {
        Task<XlsxFile> ReportAsync(DateTime from, DateTime until);
    }
}