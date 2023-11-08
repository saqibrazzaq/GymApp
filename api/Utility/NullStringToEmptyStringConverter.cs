using System.Text.Json;
using System.Text.Json.Serialization;

namespace api.Utility
{
    public class NullStringToEmptyStringConverter : JsonConverter<string>
    {
        // Override default null handling
        public override bool HandleNull => true;
        // Check the type
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(string);
        }
        // Ignore for this exampke
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = (string)reader.GetString();
            return value ?? String.Empty;
        }
        // 
        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            if (value == null)
                writer.WriteStringValue("");
            else
                writer.WriteStringValue(value);
        }
    }
}
