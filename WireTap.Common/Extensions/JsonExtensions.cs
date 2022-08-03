namespace WireTap
{
    using System;
    using System.Buffers;
    using System.Text.Encodings.Web;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    public static class JsonExtensions
    {
        static JsonExtensions()
        {
            DefaultDeserializationOptions = new JsonSerializerOptions
            {
                WriteIndented               = false,
                IgnoreNullValues            = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy        = JsonNamingPolicy.CamelCase,
                Encoder                     = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                ReadCommentHandling         = JsonCommentHandling.Skip
            };
            DefaultSerializationOptions = new JsonSerializerOptions
            {
                WriteIndented               = false,
                IgnoreNullValues            = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy        = JsonNamingPolicy.CamelCase,
                Encoder                     = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                ReadCommentHandling         = JsonCommentHandling.Skip
            };
        }

        public static JsonSerializerOptions DefaultDeserializationOptions { get; }

        public static JsonSerializerOptions DefaultSerializationOptions { get; }

        public static string ToJson(this object value, JsonSerializerOptions options = null) =>
            value != null
                ? JsonSerializer.Serialize(value, options ?? DefaultSerializationOptions)
                : default;

        public static TObject ToObject<TObject>(this string value, JsonSerializerOptions options = null) =>
            value.HasValue()
                ? JsonSerializer.Deserialize<TObject>(value, options ?? DefaultDeserializationOptions)
                : default;

        public static TObject ToObject<TObject>(this JsonElement element, JsonSerializerOptions options = null)
        {
            var bufferWriter = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(bufferWriter);
            element.WriteTo(writer);
            writer.Flush();

            return JsonSerializer.Deserialize<TObject>(bufferWriter.WrittenSpan, options ?? DefaultDeserializationOptions);
        }

        public static ValueTask<TObject> ToObjectAsync<TObject>(this System.IO.Stream reader, JsonSerializerOptions options = null) =>
            JsonSerializer.DeserializeAsync<TObject>(reader, options ?? DefaultDeserializationOptions);

        public static async Task<TObject> ToObjectAsync<TObject>(this JsonElement element, JsonSerializerOptions options = null, CancellationToken token = default)
        {
            var bufferWriter = new ArrayBufferWriter<byte>();
            await using var writer = new Utf8JsonWriter(bufferWriter);
            element.WriteTo(writer);
            await writer.FlushAsync(token);

            return JsonSerializer.Deserialize<TObject>(bufferWriter.WrittenSpan, options ?? DefaultDeserializationOptions);
        }

        public static async Task<TObject> ToObjectAsync<TObject>(this JsonDocument document, JsonSerializerOptions options = null, CancellationToken token = default) =>
            document == null
                ? throw new ArgumentNullException(nameof(document))
                : await document.RootElement.ToObjectAsync<TObject>(options ?? DefaultDeserializationOptions, token);
    }
}