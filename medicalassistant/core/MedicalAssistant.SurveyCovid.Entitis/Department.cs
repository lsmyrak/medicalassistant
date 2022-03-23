using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAssistant.SurveyCovid.Entitis
{
    public class Department
    {
        public Department()
        {
        }

        public Department
            (
            Guid? id,
            string name,
            bool status
            )
        {
            Id = id ?? Guid.NewGuid();
            Name = name;
            Status = status;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool Status { get; private set; }


        public void SetId(Guid id)
        {
            Id = id;
        }

        public void SetName(string name)
        {
            Name = name;
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

