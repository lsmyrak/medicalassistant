using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MedicalAssistant.SurveyCovid.AccessData.Repositories;
using MedicalAssistant.SurveyCovid.Entitis;
using MedicalAssistant.SurveyCovid.Raports;

namespace MedicalAssistant.SurveyCovid.Services
{
    public class SurveyReportService : ISurveyReportService
    {
        private readonly ISurveyRepository _surveyRepository;

        public SurveyReportService(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository ?? throw new ArgumentNullException(nameof(surveyRepository));
        }

        public async Task<XlsxFile> ReportAsync(DateTime from, DateTime until)
        {
            var reportData = await FetchDataAsync(from, until);

            await using var memoryStream = new MemoryStream();
            using var spreadsheet = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook);
            var workbookPart = spreadsheet.CreateWorkbookPart();

            var reportPage = CreateReportPage(reportData, from, until);
            workbookPart.CreateWorksheetPart(reportPage);

            spreadsheet.Close();

            return new XlsxFile(memoryStream.ToArray(), "text/xlsx");
        }

        private static Cell CreateStringCell(UInt32Value styleIndex, string cellValue = null) =>
        new Cell
        {
            CellValue = new CellValue(cellValue ?? string.Empty),
            StyleIndex = styleIndex,
            DataType = CellValues.String
        };

        private static Cell CreateNumberCell(UInt32Value styleIndex, string cellValue) =>
        new Cell
        {
            CellValue = new CellValue(cellValue),
            StyleIndex = styleIndex,
            DataType = CellValues.Number
        };

        private async Task<IReadOnlyCollection<Survey>> FetchDataAsync(DateTime from, DateTime until)
        {
            var survey = await _surveyRepository.GetAsync();
            return survey.Where(s => s.EntryDate.Date >= from.Date).Where(s => s.EntryDate.Date <= until.Date).ToArray();
        }

        private ExcelReportPage CreateReportPage(IReadOnlyCollection<Survey> surveys, DateTime from, DateTime until)
        {
            var worksheetData = new SheetData();
            worksheetData.AppendChild(CreateHeaderRow());
            worksheetData.Append(CreateDataCells(surveys));

            return new ExcelReportPage
            {
                Name = $"Od {from:yyyy-MM-dd} do {until:yyyy-MM-dd}",
                Data = worksheetData,
                RowCounter = 100
            };
        }

        private Row CreateHeaderRow()
        {
            var cells = new List<Cell>
            {
                CreateStringCell(4, "Pesel"),
                CreateStringCell(4, "Inny Dokument"),
                CreateStringCell(4, "Numer/Seria"),
                CreateStringCell(4, "Kod Produktu"),
                CreateStringCell(4, "Nazwa Produktu"),
                CreateStringCell(4, "Data Od"),
                CreateStringCell(4, "Data Do"),
                CreateStringCell(4, "Liczba Jednostek Rozliczeniowych"),
                CreateStringCell(4, "Wartość Jednostki"),
                CreateStringCell(4, "Wartość"),
                CreateStringCell(4, "Miejsce")
            };

            return new Row(cells);
        }

        private Row CreateDataRow(Survey survey)
        {
            var cells = new List<Cell>
            {
                CreateStringCell(3, survey.Pesel),
                CreateStringCell(3, survey.AnotherDocument),
                CreateStringCell(3, survey.SeriesNumber),
                CreateStringCell(3, survey.Product.ProductCode),
                CreateStringCell(3, survey.Product.ProductName),
                CreateStringCell(3, survey.FromDate.ToString("yyyy-MM-dd")),
                CreateStringCell(3, survey.UntilDate.ToString("yyyy-MM-dd")),
                CreateNumberCell(3, survey.Product.UnitsCount.ToString()),
                CreateNumberCell(3, survey.Product.UnitValue.ToString()),
                CreateNumberCell(3, (survey.Product.UnitsCount*survey.Product.UnitValue).ToString()),
                CreateStringCell(3, survey.Place), 
            };

            return new Row(cells);
        }

        private IEnumerable<Row> CreateDataCells(IReadOnlyCollection<Survey> survey)
        {
            var cells = new List<Cell>();

            return survey.Select(CreateDataRow);
        }

    }
}
