using System.Text.Json;
using System.Text.Json.Serialization;

namespace api.Utility
{
    public class NullIntegerToEmptyStringConverter : JsonConverter<int>
    {
        // Override default null handling
        public override bool HandleNull => true;
        // Check the type
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(string);
        }
        // Ignore for this exampke
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            //string value = (string)reader.GetString();
            //return value ?? String.Empty;
            return reader.GetInt32();
        }
        // 
        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            if (value == null)
                writer.WriteStringValue("");
            else
                writer.WriteNumberValue(value);
        }
    }
}
