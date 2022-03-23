using MedicalAssistant.AspNetCommon.Attributes;
using MedicalAssistant.Common.Constants;
using MedicalAssistant.SurveyCovid.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : Controller
    {
        private readonly ISurveyReportService _surveyReportService;

        public ReportsController(ISurveyReportService surveyReportService)
        {
            _surveyReportService = surveyReportService ?? throw new System.ArgumentNullException(nameof(surveyReportService));
        }

        [HttpGet("survey-report")]
        [AllowedRoles(UserRoles.Admin)]
        public async Task<FileContentResult> SurveyReport(DateTime from, DateTime until)
        {
            var xlsxFile = await _surveyReportService.ReportAsync(from, until);

            return new FileContentResult(xlsxFile.ByteContent, xlsxFile.ContentType)
            {
                FileDownloadName = $"SurveyReport.xlsx"
            };
        }
    }
}
