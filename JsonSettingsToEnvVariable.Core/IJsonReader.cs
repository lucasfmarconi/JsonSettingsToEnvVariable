using System.Collections.Generic;

namespace JsonSettingsToEnvVariable.Core;
public interface IJsonReader
{
    IDictionary<string, object> ReadDictionaryFromJsonFile(string path);
}
