using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAssistant.SurveyCovid.Entitis
{
    public class Role
    {
        public Role(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Role()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public ICollection<User> Users { get; private set; }
    }

}
