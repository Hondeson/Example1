using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string DateFormat = "yyyy-MM-dd";
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        int year = 0, month = 0, day = 0;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return new DateOnly(year, month, day);
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                continue;
            }

            string propertyName = reader.GetString();

            if (!reader.Read())
            {
                throw new JsonException($"Unexpected end of JSON object when reading property '{propertyName}'.");
            }

            switch (propertyName)
            {
                case "year":
                    year = reader.GetInt32();
                    break;
                case "month":
                    month = reader.GetInt32();
                    break;
                case "day":
                    day = reader.GetInt32();
                    break;
                default:
                    break;
            }
        }

        throw new JsonException("Unexpected end of JSON input.");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateFormat));
    }
}