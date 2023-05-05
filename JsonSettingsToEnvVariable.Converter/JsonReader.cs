using JsonFlatten;
using JsonSettingsToEnvVariable.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace JsonSettingsToEnvVariable.Converter;
internal class JsonReader : IJsonReader
{
    private readonly ILogger<JsonReader> logger;
    public JsonReader(ILogger<JsonReader> logger)
    {
        this.logger = logger;
    }

    public IDictionary<string, object> ReadDictionaryFromJsonFile(string path)
    {
        ArgumentException.ThrowIfNullOrEmpty(path);

        if (!File.Exists(path))
            throw new FileNotFoundException($"File path: {path}");

        var dictionaryFromJsonFile = JObject.Parse(File.ReadAllText(path)).Flatten();

        foreach (var kvp in dictionaryFromJsonFile ?? throw new ArgumentNullException(nameof(dictionaryFromJsonFile)))
            logger.LogTrace("{key}: {value}", kvp.Key, kvp.Value);

        logger.LogDebug("Dictionary From Json File {@dict}", dictionaryFromJsonFile);

        return dictionaryFromJsonFile;
    }
}
