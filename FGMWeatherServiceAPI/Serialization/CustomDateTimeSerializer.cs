using MongoDB.Bson.Serialization;
using System.Globalization;

namespace FGMWeatherServiceAPI.Serialization
{
    /// <summary>
    /// Custom serializer for <see cref="DateTime"/> values, handling the conversion between
    /// ISO 8601 date strings and <see cref="DateTime"/> objects.
    /// </summary>
    public class CustomDateTimeSerializer : IBsonSerializer<DateTime>
    {
        /// <summary>
        /// Gets the type of the serialized value.
        /// </summary>
        public Type ValueType => typeof(DateTime);

        /// <summary>
        /// Deserializes a <see cref="DateTime"/> value from a BSON string representation.
        /// </summary>
        /// <param name="context">The BSON deserialization context.</param>
        /// <param name="args">The deserialization arguments.</param>
        /// <returns>The deserialized <see cref="DateTime"/> value.</returns>
        public DateTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonValue = context.Reader.ReadString();
            return DateTime.ParseExact(bsonValue, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Serializes a <see cref="DateTime"/> value to a BSON string representation.
        /// </summary>
        /// <param name="context">The BSON serialization context.</param>
        /// <param name="args">The serialization arguments.</param>
        /// <param name="value">The <see cref="DateTime"/> value to serialize.</param>
        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTime value)
        {
            var formattedDate = value.ToString("yyyy-MM-ddTHH:mm");
            context.Writer.WriteString(formattedDate);
        }

        /// <summary>
        /// Serializes an object to a BSON string representation.
        /// </summary>
        /// <param name="context">The BSON serialization context.</param>
        /// <param name="args">The serialization arguments.</param>
        /// <param name="value">The object to serialize. Should be a <see cref="DateTime"/> instance.</param>
        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            Serialize(context, args, (DateTime)value);
        }

        /// <summary>
        /// Deserializes an object from a BSON string representation.
        /// </summary>
        /// <param name="context">The BSON deserialization context.</param>
        /// <param name="args">The deserialization arguments.</param>
        /// <returns>The deserialized object, cast to <see cref="DateTime"/>.</returns>
        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Deserialize(context, args);
        }
    }
}
