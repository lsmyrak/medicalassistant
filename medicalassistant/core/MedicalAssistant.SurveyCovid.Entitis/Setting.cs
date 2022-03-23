using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalAssistant.SurveyCovid.Entitis
{
    public class Setting
    {
        public Setting(string key, string value, string description = null)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Key cannot be null, empty or whitespace", nameof(key));
            }

            Key = key;
            SetDefaults(value, description);
        }

        public Setting()
        {
        }

        [Key]
        [MaxLength(125)]
        public string Key { get; private set; }

        public string Description { get; private set; }

        [Required]
        public string Value { get; private set; }

        public string OverrideValue { get; private set; }

        public string GetValue()
        {
            return OverrideValue ?? Value;
        }

        public void SetDefaults(string defaultValue, string description)
        {
            Value = defaultValue;
            Description = description;
        }
    }
}
