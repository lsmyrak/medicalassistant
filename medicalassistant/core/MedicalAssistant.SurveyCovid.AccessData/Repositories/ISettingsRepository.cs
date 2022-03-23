using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.AccessData.Repositories
{
    public interface ISettingsRepository
    {
        Task AddOrUpdateValueAsync<TValueType>(string key, TValueType value, string description = null);
        Task AddValueAsync<TValueType>(string key, TValueType value, string description = null);
        string GetValue(string key);
        TValueType GetValue<TValueType>(string key);
        Task<string> GetValueAsync(string key);
        Task<TValueType> GetValueAsync<TValueType>(string key);
        Task<bool> KeyExistsAsync(string key);
    }
}