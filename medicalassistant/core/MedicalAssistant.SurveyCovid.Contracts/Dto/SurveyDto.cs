using System;

namespace MedicalAssistant.SurveyCovid.Contracts.Dto
{
    public class SurveyDto
    {
        public Guid Id { get; set; }
        public string Pesel { get; set; }
        public string AnotherDocument { get; set; }
        public string SeriesNumber { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime UntilDate { get; set; }
        public string Place { get; set; }
        public DateTime EntryDate { get; set; }
        public bool Status { get; set; }
        public bool Export { get; set; }
        public ProductDto Product { get; set; }

    }
}