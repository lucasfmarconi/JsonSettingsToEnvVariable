using System.Collections.Generic;

namespace JsonSettingsToEnvVariable.Core;
public interface IEnvVariablePatternConverter
{
    IEnumerable<string> ConvertDictionaryToEnvVariablePatternList(IDictionary<string, object> envVariablesDictionary,
        string valueDelimiter);
}
