using Newtonsoft.Json;
using System.Globalization;
using System.IO;
using System.Text;

namespace MedicalAssistant.Common.Extensions
{
    public static class JsonSerializerExtensions
    {
        public static string SerializeObject<TTargetType>(this JsonSerializer serializer, TTargetType target)
        {
            var sb = new StringBuilder(256);
            var sw = new StringWriter(sb, CultureInfo.CurrentCulture);
            using var jsonWriter = new JsonTextWriter(sw) { Formatting = serializer.Formatting };

            serializer.Serialize(jsonWriter, target, typeof(TTargetType));

            return sw.ToString();
        }

        public static TOutputType DeserializeObject<TOutputType>(this JsonSerializer serializer, string input)
        {
            using var reader = new JsonTextReader(new StringReader(input));
            return (TOutputType)serializer.Deserialize(reader, typeof(TOutputType));
        }
    }
}
