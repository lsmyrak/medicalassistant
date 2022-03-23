using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalAssistant.SurveyCovid.Contracts.Dto
{
    public class RportRequestDto
    {
        public DateTime From { get; set; }

        public DateTime Until { get; set; }
    }
}
