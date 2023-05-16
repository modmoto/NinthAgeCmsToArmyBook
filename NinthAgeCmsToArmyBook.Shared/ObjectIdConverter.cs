using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Bson;

namespace NinthAgeCmsToArmyBook.Api;

public class ObjectIdConverter : JsonConverter<ObjectId>
{
    public override ObjectId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var objectIdRaw = reader.GetString()!;
        try
        {
            var id = new ObjectId(objectIdRaw);
            return id;
        }
        catch (Exception e)
        {
            return ObjectId.Empty;
        }
    }

    public override void Write(Utf8JsonWriter writer, ObjectId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}