using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace MedicalAssistant.SurveyCovid.Raports
{
    public static class WorkbookPartExtensions
    {
        public static WorksheetPart CreateWorksheetPart(this WorkbookPart workbookPart, ExcelReportPage reportPage)
        {
            var currentSheetCount = workbookPart.Workbook.Sheets.Count();

            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

            workbookPart.Workbook.Sheets.InsertAt(
                new Sheet
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = (uint)currentSheetCount + 1,
                    Name = reportPage.Name
                },
                currentSheetCount);

            worksheetPart.Worksheet = new Worksheet(
                new SheetFormatProperties
                {
                    DefaultColumnWidth = DoubleValue.FromDouble(20),
                    DefaultRowHeight = DoubleValue.FromDouble(15),
                });

            //worksheetPart.Worksheet.AppendChild(CreateColumns());
            worksheetPart.Worksheet.AppendChild(reportPage.Data);
            worksheetPart.Worksheet.AppendChild(GenerateAutoFilter());
            //worksheetPart.Worksheet.AppendChild(CreateMergeCells());
            //worksheetPart.Worksheet.AppendChild(CreateConditionalFormatting(reportPage.RowCounter));

            return worksheetPart;
        }

        private static Columns CreateColumns()
        {
            var columns = new Columns();
            columns.AppendChild(new Column { Min = 1, Max = 1, Width = DoubleValue.FromDouble(21), CustomWidth = true });
            columns.AppendChild(new Column { Min = 2, Max = 2, Width = DoubleValue.FromDouble(9), CustomWidth = true });
            columns.AppendChild(new Column { Min = 3, Max = 33, Width = DoubleValue.FromDouble(3.5), CustomWidth = true });
            columns.AppendChild(new Column { Min = 34, Max = 34, Width = DoubleValue.FromDouble(7), CustomWidth = true });
            columns.AppendChild(new Column { Min = 35, Max = 37, Width = DoubleValue.FromDouble(11), CustomWidth = true });
            return columns;
        }

        private static AutoFilter GenerateAutoFilter()
        {
            AutoFilter autoFilter1 = new AutoFilter() { Reference = "A1:K499" };
            //autoFilter1.SetAttribute(new OpenXmlAttribute("xr", "uid", "http://schemas.microsoft.com/office/spreadsheetml/2014/revision", "{30FA0456-3C1D-4A16-BE38-2881FE213128}"));
            return autoFilter1;
        }

        private static MergeCells CreateMergeCells()
        {
            var mergeCells = new MergeCells();
            mergeCells.AppendChild(new MergeCell { Reference = "C1:AF1" });
            mergeCells.AppendChild(new MergeCell { Reference = "AI1:AK1" });
            mergeCells.Count = 2;
            return mergeCells;
        }

        private static ConditionalFormatting CreateConditionalFormatting(int rowCounter)
        {
            var conditionalFormatting = new ConditionalFormatting { SequenceOfReferences = new ListValue<StringValue> { InnerText = $"C3:AG{rowCounter}" } };

            var noTransitConditionalFormattingRule = new ConditionalFormattingRule
            { Type = ConditionalFormatValues.ContainsBlanks, FormatId = 3, Priority = 1 };
            var noTransitFormula = new Formula { Text = string.Empty };
            noTransitConditionalFormattingRule.AppendChild(noTransitFormula);

            var canceledConditionalFormattingRule = new ConditionalFormattingRule
            { Type = ConditionalFormatValues.CellIs, FormatId = 2, Priority = 2, Operator = ConditionalFormattingOperatorValues.Equal };
            var skippedFormula = new Formula { Text = $"\"Canceled\"" };
            canceledConditionalFormattingRule.AppendChild(skippedFormula);

            var notEnteredConditionalFormattingRule = new ConditionalFormattingRule
            { Type = ConditionalFormatValues.CellIs, FormatId = 1, Priority = 3, Operator = ConditionalFormattingOperatorValues.Equal };
            var notEnteredFormula = new Formula { Text = $"\"NotEntered\"" };
            notEnteredConditionalFormattingRule.AppendChild(notEnteredFormula);

            var enteredConditionalFormattingRule = new ConditionalFormattingRule
            { Type = ConditionalFormatValues.CellIs, FormatId = 0, Priority = 4, Operator = ConditionalFormattingOperatorValues.Equal };
            var enteredFormula = new Formula { Text = $"\"Entered\"" };
            enteredConditionalFormattingRule.AppendChild(enteredFormula);

            var bankHolidayConditionalFormattingRule = new ConditionalFormattingRule
            { Type = ConditionalFormatValues.CellIs, FormatId = 4, Priority = 1, Operator = ConditionalFormattingOperatorValues.Equal };
            var bankHolidayFormula = new Formula { Text = $"\"BankHoliday\"" };
            bankHolidayConditionalFormattingRule.AppendChild(bankHolidayFormula);

            var notAssignedConditionalFormattingRule = new ConditionalFormattingRule
            { Type = ConditionalFormatValues.CellIs, FormatId = 5, Priority = 5, Operator = ConditionalFormattingOperatorValues.Equal };
            var notAssignedFormula = new Formula { Text = $"\"NotAssigned\"" };
            notAssignedConditionalFormattingRule.AppendChild(notAssignedFormula);

            conditionalFormatting.AppendChild(noTransitConditionalFormattingRule);
            conditionalFormatting.AppendChild(canceledConditionalFormattingRule);
            conditionalFormatting.AppendChild(notEnteredConditionalFormattingRule);
            conditionalFormatting.AppendChild(enteredConditionalFormattingRule);
            conditionalFormatting.AppendChild(bankHolidayConditionalFormattingRule);
            conditionalFormatting.AppendChild(notAssignedConditionalFormattingRule);
            return conditionalFormatting;
        }
    }
}
