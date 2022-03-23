using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace MedicalAssistant.SurveyCovid.Raports
{
    public static class SpreadsheetDocumentExtensions
    {
        public static WorkbookPart CreateWorkbookPart(this SpreadsheetDocument spreadsheet)
        {
            var workbookPart = spreadsheet.AddWorkbookPart();
            var workbookStylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
            workbookStylesPart.Stylesheet = GetStylesheet();
            workbookPart.Workbook = new Workbook(new Sheets());

            return workbookPart;
        }

        private static Stylesheet GetStylesheet()
        {
            var stylesheet = new Stylesheet
            {
                Fonts = CreateFonts(),
                Fills = CreateFills(),
                Borders = CreateBorders(),
                NumberingFormats = CreeateNumberingFormats(),
                CellStyleFormats = CreateCellStyleFormats(),
                DifferentialFormats = CreateDifferentialFormats(),
                CellFormats = CreateCellFormats()
            };

            return stylesheet;
        }

        private static Fonts CreateFonts()
        {
            var fonts = new Fonts { Count = 2 };
            fonts.AppendChild(new Font());
            fonts.AppendChild(new Font { Bold = new Bold() });

            return fonts;
        }

        private static Fills CreateFills()
        {
            var fills = new Fills { Count = 9 };

            fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.None } }); // id = 0 required, reserved by Excel
            fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.DarkGray } }); //id = 1 required, reserved by Excel
            fills.AppendChild(
                new Fill
                {
                    PatternFill = new PatternFill
                    {
                        PatternType = PatternValues.Solid,
                        ForegroundColor = new ForegroundColor { Rgb = "FFF2F2F2" },
                        BackgroundColor = new BackgroundColor { Indexed = 64 }
                    }
                }); //id =2 noTransit cell pattern fill
            fills.AppendChild(
                new Fill
                {
                    PatternFill = new PatternFill
                    {
                        PatternType = PatternValues.Solid,
                        ForegroundColor = new ForegroundColor { Rgb = "FFD9D9D9" },
                        BackgroundColor = new BackgroundColor { Indexed = 64 }
                    }
                }); //id = 3 bank holiday cell pattern fill
            fills.AppendChild(
                new Fill
                {
                    PatternFill = new PatternFill
                    {
                        PatternType = PatternValues.Solid,
                        ForegroundColor = new ForegroundColor { Rgb = "FFFFFF00" },
                        BackgroundColor = new BackgroundColor { Indexed = 64 }
                    }
                }); //id = 4 yellow cell pattern fill
            fills.AppendChild(
                new Fill
                {
                    PatternFill = new PatternFill
                    {
                        PatternType = PatternValues.Solid,
                        ForegroundColor = new ForegroundColor { Rgb = "FFB4C6E7" },
                        BackgroundColor = new BackgroundColor { Indexed = 64 }
                    }
                }); //id = 5 noNotification cell pattern fill
            fills.AppendChild(
                new Fill
                {
                    PatternFill = new PatternFill
                    {
                        PatternType = PatternValues.Solid,
                        ForegroundColor = new ForegroundColor { Rgb = "FFC6E0B4" },
                        BackgroundColor = new BackgroundColor { Indexed = 64 }
                    }
                }); //id = 6 entered cell pattern fill
            fills.AppendChild(
                new Fill
                {
                    PatternFill = new PatternFill
                    {
                        PatternType = PatternValues.Solid,
                        ForegroundColor = new ForegroundColor { Rgb = "FFFF0000" },
                        BackgroundColor = new BackgroundColor { Indexed = 64 }
                    }
                }); //id = 7 not entered cell pattern fill
            fills.AppendChild(
                new Fill
                {
                    PatternFill = new PatternFill
                    {
                        PatternType = PatternValues.Solid,
                        ForegroundColor = new ForegroundColor { Rgb = "FFFFE699" },
                        BackgroundColor = new BackgroundColor { Indexed = 64 }
                    }
                }); //id = 8 canceled cell pattern fill

            return fills;
        }

        private static NumberingFormats CreeateNumberingFormats()
        {
            var numberingFormats = new NumberingFormats { Count = 1 };
            numberingFormats.AppendChild(
                new NumberingFormat
                {
                    NumberFormatId = 44,
                    FormatCode = "_-* #,##0.00\\ \"zł\"_-;\\-* #,##0.00\\ \"zł\"_-;_-* \"-\"??\\ \"zł\""
                });

            return numberingFormats;
        }

        private static CellStyleFormats CreateCellStyleFormats()
        {
            var cellStyleFormats = new CellStyleFormats { Count = 1 };
            cellStyleFormats.AppendChild(new CellFormat());

            return cellStyleFormats;
        }

        private static CellFormats CreateCellFormats()
        {
            var cellFormats = new CellFormats { Count = 16 };

            // empty one for index 0, seems to be required
            cellFormats
                .AppendChild(new CellFormat()); // id = 0
            cellFormats // id = 1 header cells format
                .AppendChild(new CellFormat { FormatId = 0, FontId = 1, BorderId = 1 })
                .AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center });
            cellFormats // id = 2 passengers colunms format
                .AppendChild(new CellFormat { FormatId = 0, FontId = 0, BorderId = 1 })
                .AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center });
            cellFormats // id = 3 transport data cells format
                .AppendChild(new CellFormat { FormatId = 0, FontId = 0, BorderId = 2, FillId = 2, ApplyFill = true })
                .AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center });
            cellFormats // id = 4 bank holiday cells format
                .AppendChild(new CellFormat { FormatId = 0, FontId = 0, BorderId = 2, FillId = 3, ApplyFill = true })
                .AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center });
            cellFormats // id = 5 // total cost cell format
                .AppendChild(new CellFormat { FormatId = 0, FontId = 1, BorderId = 3, FillId = 4, ApplyFill = true })
                .AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center });
            cellFormats // id = 6 // total cost cell format
                .AppendChild(
                    new CellFormat
                    {
                        NumberFormatId = 44,
                        ApplyNumberFormat = true,
                        FormatId = 0,
                        FontId = 1,
                        BorderId = 4,
                        FillId = 4,
                        ApplyFill = true
                    })
                .AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center });
            cellFormats // id = 7 empty cells format
                .AppendChild(new CellFormat { FormatId = 0, FontId = 0 })
                .AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center });
            cellFormats // id = 8 "SUMA" cell format
                .AppendChild(new CellFormat { FormatId = 0, FontId = 1, BorderId = 5 })
                .AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center });
            cellFormats // id = 9 book values cells format
                .AppendChild(new CellFormat { NumberFormatId = 44, ApplyNumberFormat = true, FormatId = 0, FontId = 0 });
            cellFormats // id = 10 "entered" legend cells format
                .AppendChild(new CellFormat { FormatId = 0, FontId = 0, FillId = 6, ApplyFill = true })
                .AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center });
            cellFormats // id = 11 "canceled" legend cells format
                .AppendChild(new CellFormat { FormatId = 0, FontId = 0, FillId = 8, ApplyFill = true })
                .AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center });
            cellFormats // id = 12 "notEntered" legend cells format
                .AppendChild(new CellFormat { FormatId = 0, FontId = 0, FillId = 7, ApplyFill = true })
                .AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center });
            cellFormats // id = 13 "noNotification" legend cells format
                .AppendChild(new CellFormat { FormatId = 0, FontId = 0, FillId = 5, ApplyFill = true })
                .AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center });
            cellFormats // id = 14 "noTransit" legend cells format
                .AppendChild(new CellFormat { FormatId = 0, FontId = 0, FillId = 2, ApplyFill = true })
                .AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center });
            cellFormats // id = 15 "bankHoliday" legend cells format
                .AppendChild(new CellFormat { FormatId = 0, FontId = 0, FillId = 3, ApplyFill = true })
                .AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center });

            return cellFormats;
        }

        private static Borders CreateBorders()
        {
            var borders = new Borders { Count = 6 };
            borders.AppendChild(new Border());
            borders.AppendChild(
                new Border
                {
                    LeftBorder = new LeftBorder { Style = BorderStyleValues.Thin },
                    RightBorder = new RightBorder { Style = BorderStyleValues.Thin },
                    TopBorder = new TopBorder { Style = BorderStyleValues.Thin },
                    BottomBorder = new BottomBorder { Style = BorderStyleValues.Thin }
                });
            borders.AppendChild(
                new Border
                {
                    LeftBorder = new LeftBorder { Style = BorderStyleValues.Dashed },
                    RightBorder = new RightBorder { Style = BorderStyleValues.Dashed },
                    TopBorder = new TopBorder { Style = BorderStyleValues.Dashed },
                    BottomBorder = new BottomBorder { Style = BorderStyleValues.Dashed }
                });
            borders.AppendChild(
                new Border
                {
                    LeftBorder = new LeftBorder { Style = BorderStyleValues.Medium },
                    TopBorder = new TopBorder { Style = BorderStyleValues.Medium },
                    BottomBorder = new BottomBorder { Style = BorderStyleValues.Medium }
                });
            borders.AppendChild(
                new Border
                {
                    RightBorder = new RightBorder { Style = BorderStyleValues.Medium },
                    TopBorder = new TopBorder { Style = BorderStyleValues.Medium },
                    BottomBorder = new BottomBorder { Style = BorderStyleValues.Medium }
                });
            borders.AppendChild(
                new Border
                {
                    RightBorder = new RightBorder { Style = BorderStyleValues.Thin },
                    TopBorder = new TopBorder { Style = BorderStyleValues.Thin },
                    BottomBorder = new BottomBorder { Style = BorderStyleValues.Thin }
                });

            return borders;
        }

        private static DifferentialFormats CreateDifferentialFormats()
        {
            var differentialFormats = new DifferentialFormats { Count = 6 };

            var enteredDifferentialFormat = new DifferentialFormat
            { Fill = new Fill { PatternFill = new PatternFill { BackgroundColor = new BackgroundColor { Theme = 9U, Tint = 0.59996337778862885D } } } };

            var notEnteredDifferentialFormat = new DifferentialFormat
            { Fill = new Fill { PatternFill = new PatternFill { BackgroundColor = new BackgroundColor { Rgb = "FFFF0000" } } } };

            var noTransitDifferentialFormat = new DifferentialFormat
            { Fill = new Fill { PatternFill = new PatternFill { BackgroundColor = new BackgroundColor { Theme = 7, Tint = 0.59996337778862885D } } } };

            var canceledDifferentialFormat = new DifferentialFormat
            { Fill = new Fill { PatternFill = new PatternFill { BackgroundColor = new BackgroundColor { Theme = 0U, Tint = -4.9989318521683403E-2D } } } };

            var bankHolidayDifferentialFormat = new DifferentialFormat
            { Fill = new Fill { PatternFill = new PatternFill { BackgroundColor = new BackgroundColor { Rgb = "FFD9D9D9" } } } };

            var noNotificationDifferentialFormat = new DifferentialFormat
            { Fill = new Fill { PatternFill = new PatternFill { BackgroundColor = new BackgroundColor { Rgb = "FFB4C6E7" } } } };

            differentialFormats.AppendChild(enteredDifferentialFormat);
            differentialFormats.AppendChild(notEnteredDifferentialFormat);
            differentialFormats.AppendChild(noTransitDifferentialFormat);
            differentialFormats.AppendChild(canceledDifferentialFormat);
            differentialFormats.AppendChild(bankHolidayDifferentialFormat);
            differentialFormats.AppendChild(noNotificationDifferentialFormat);

            return differentialFormats;
        }
    }
}
