using System;

namespace MedicalAssistant.SurveyCovid.Contracts.Dto
{
    public class ProductDto
    {
        public Guid? Id { get; set; }

        public string Description { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public int UnitsCount { get; set; }

        public double UnitValue { get; set; }

        public bool Status { get; set; }
    }
}
