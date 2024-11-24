using System.Text.Json;
using System.Text.Json.Serialization;

public class ParenteConverter : JsonConverter<Parente>
{
    public override Parente Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonObject = JsonDocument.ParseValue(ref reader).RootElement;
        return new Parente
        {
            Id = jsonObject.GetProperty("Id").GetGuid(),
            Nome = jsonObject.GetProperty("Nome").GetString(),
            Casa = jsonObject.GetProperty("Casa").GetString(),
            Telefone = jsonObject.GetProperty("Telefone").GetString(),
            LinkNome = jsonObject.GetProperty("LinkNome").GetString(),
            Genero = jsonObject.GetProperty("Genero").GetString(),
            Mae = jsonObject.GetProperty("Mae").GetString(),
            Pai = jsonObject.GetProperty("Pai").GetString(),
            Patriarca = jsonObject.GetProperty("Patriarca").GetBoolean(),
            Matriarca = jsonObject.GetProperty("Matriarca").GetBoolean(),
            Conjugue = jsonObject.GetProperty("Conjugue").GetString(),
            Filhos = jsonObject.TryGetProperty("Filhos", out var filhosElement) && filhosElement.ValueKind != JsonValueKind.Null
                ? JsonSerializer.Deserialize<List<Parente>>(filhosElement.GetRawText(), options)
                : new List<Parente>()
        };
    }

    public override void Write(Utf8JsonWriter writer, Parente value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Id", value.Id);
        writer.WriteString("Nome", value.Nome);
        writer.WriteString("Casa", value.Casa);
        writer.WriteString("Telefone", value.Telefone);
        writer.WriteString("LinkNome", value.LinkNome);
        writer.WriteString("Genero", value.Genero);
        writer.WriteString("Mae", value.Mae);
        writer.WriteString("Pai", value.Pai);
        writer.WriteBoolean("Patriarca", value.Patriarca);
        writer.WriteBoolean("Matriarca", value.Matriarca);
        writer.WriteString("Conjugue", value.Conjugue);
        writer.WritePropertyName("Filhos");
        JsonSerializer.Serialize(writer, value.Filhos, options);
        writer.WriteEndObject();
    }
}
