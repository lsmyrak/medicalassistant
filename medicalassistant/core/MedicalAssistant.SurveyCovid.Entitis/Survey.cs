using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAssistant.SurveyCovid.Entitis
{
    public class Survey
    {
        public Survey()
        {
        }

        public Survey(
            Guid? id,
            string pesel,
            string anotherDocument,
            string seriesNumber,
            DateTime fromDate,
            DateTime untilDate,
            string place,
            DateTime entryDate,
            bool status,
            bool export)
        {
            Id = id ?? Guid.NewGuid();
            Pesel = pesel;
            AnotherDocument = anotherDocument;
            SeriesNumber = seriesNumber;
            FromDate = fromDate;
            UntilDate = untilDate;
            Place = place;
            EntryDate = entryDate;
            Status = status;
            Export = export;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Pesel { get; private set; }
        public string AnotherDocument { get; private set; }
        public string SeriesNumber { get; private set; }
        public DateTime FromDate { get; private set; }
        public DateTime UntilDate { get; private set; }
        public string Place { get; private set; }
        public DateTime EntryDate { get; private set; }
        public bool Status { get; private set; }
        public bool Export { get; private set; }


        public Guid ProductId { get; private set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; private set; }

        public void SetPesel(string pesel)
        {
            Pesel = pesel;
        }

        public void SetAnotherDocument(string anotherDocument)
        {
            AnotherDocument = anotherDocument;
        }

        public void SetSeriesNumber(string seriesNumber)
        {
            SeriesNumber = seriesNumber;
        }


        public void SetFromDate(DateTime dateFrom)
        {
            FromDate = dateFrom;
        }

        public void SetUntilDate(DateTime dateTo)
        {
            UntilDate = dateTo;
        }

        public void SetPlace(string place)
        {
            Place = place;
        }

        public void SetEntryDate(DateTime entryDate)
        {
            EntryDate = entryDate;
        }

        public void SetActive()
        {
            Status = true;
        }

        public void SetInactive()
        {
            Status = false;
        }

        public void Exported()
        {
            Export = true;
        }

        public double GetValue()
        {
            return Product.UnitValue * Product.UnitsCount;
        }
    }
}