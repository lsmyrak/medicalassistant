using DocumentFormat.OpenXml.Spreadsheet;

namespace MedicalAssistant.SurveyCovid.Raports
{
    public class ExcelReportPage
    {
        public string Name { get; set; }

        public SheetData Data { get; set; }

        public int RowCounter { get; set; }
    }
}
