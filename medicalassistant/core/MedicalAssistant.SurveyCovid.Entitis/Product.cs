using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAssistant.SurveyCovid.Entitis
{

    public class Product
    {

        public Product(
           Guid? id,
           string description,
           string productCode,
           string productName,
           int unitsCount,
           double unitValue,
           bool status
            )
        {
            Id = id ?? Guid.NewGuid();
            Description = description;
            ProductCode = productCode;
            ProductName = productName;
            UnitsCount = unitsCount;
            UnitValue = unitValue;
            Status = status;
        }

        public Product()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; private set; }

        public string Description { get; private set; }

        public string ProductCode { get; private set; }

        public string ProductName { get; private set; }

        public int UnitsCount { get; private set; }

        public double UnitValue { get; private set; }

        public bool Status { get; private set; }

        public virtual IReadOnlyCollection<Survey> Survey { get; set; }

        public void SetId(Guid id)
        {
            Id = id;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetProductCode(string productCode)
        {
            ProductCode = productCode;
        }

        public void SetProductName(string productName)
        {
            ProductName = productName;
        }

        public void SetUnitsCount(int numberOfUnits)
        {
            UnitsCount = numberOfUnits;
        }

        public void SetUnitValue(int unitValue)
        {
            UnitValue = unitValue;
        }

        public void SetActive()
        {
            Status = true;
        }

        public void SetInactive()
        {
            Status = false;
        }
    }
}
