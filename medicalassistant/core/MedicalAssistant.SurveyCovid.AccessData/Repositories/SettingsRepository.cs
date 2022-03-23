using MedicalAssistant.AspNetCommon.Exceptions;
using MedicalAssistant.Common.Extensions;
using MedicalAssistant.SurveyCovid.Context;
using MedicalAssistant.SurveyCovid.Entitis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.AccessData.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly SurveyCovidContext _surveyCovidContext;

        public SettingsRepository(SurveyCovidContext surveyCovidContext)
        {
            _surveyCovidContext = surveyCovidContext ?? throw new ArgumentNullException(nameof(_surveyCovidContext));
        }

        public string GetValue(string key)
        {
            return GetValueAsync(key).WaitAndUnwrapException();
        }

        public async Task<bool> KeyExistsAsync(string key)
        {
            return await _surveyCovidContext.Settings.FirstOrDefaultAsync(s => s.Key == key) != null;
        }

        public TValueType GetValue<TValueType>(string key)
        {
            return GetValueAsync<TValueType>(key).WaitAndUnwrapException();
        }

        public async Task<TValueType> GetValueAsync<TValueType>(string key)
        {
            var stringValue = await GetValueAsync(key);
            return (TValueType)TypeDescriptor.GetConverter(typeof(TValueType)).ConvertFromInvariantString(stringValue);
        }

        public async Task<string> GetValueAsync(string key)
        {
            var currentValue = await _surveyCovidContext.Settings.FirstOrDefaultAsync(s => s.Key == key);
            if (currentValue == null)
            {
                throw new KeyNotFoundException($"Setting with key {key} not found");
            }

            return currentValue.GetValue();
        }

        public async Task AddValueAsync<TValueType>(string key, TValueType value, string description = null)
        {
            if (await KeyExistsAsync(key))
            {
                throw new EntityAlreadyExistsException<Setting>(key);
            }

            await _surveyCovidContext.Settings.AddAsync(new Setting(key, ConvertValueToString(value), description));
            await _surveyCovidContext.SaveChangesAsync();
        }

        public async Task AddOrUpdateValueAsync<TValueType>(string key, TValueType value, string description = null)
        {
            var currentValue = await _surveyCovidContext.Settings.FirstOrDefaultAsync(s => s.Key == key);
            if (currentValue != null)
            {
                currentValue.SetDefaults(ConvertValueToString(value), description);
            }
            else
            {
                await _surveyCovidContext.Settings.AddAsync(new Setting(key, ConvertValueToString(value), description));
            }
            await _surveyCovidContext.SaveChangesAsync();
        }

        private static string ConvertValueToString<TValueType>(TValueType value)
        {
            return TypeDescriptor.GetConverter(typeof(TValueType)).ConvertToInvariantString(value);
        }
    }
}
