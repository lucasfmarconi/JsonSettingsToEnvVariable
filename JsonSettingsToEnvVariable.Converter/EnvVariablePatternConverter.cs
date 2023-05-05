using JsonSettingsToEnvVariable.Core;
using Microsoft.Extensions.Logging;

namespace JsonSettingsToEnvVariable.Converter;

internal class EnvVariablePatternConverter : IEnvVariablePatternConverter
{
    private readonly ILogger<EnvVariablePatternConverter> logger;
    public EnvVariablePatternConverter(ILogger<EnvVariablePatternConverter> logger)
    {
        this.logger = logger;
    }

    public IEnumerable<string> ConvertDictionaryToEnvVariablePatternList(
        IDictionary<string, object> envVariablesDictionary, string valueDelimiter)
    {
        ArgumentException.ThrowIfNullOrEmpty(valueDelimiter);

        List<string> envVariableDefinitionArray = new();
        foreach (var envVariableEntry in envVariablesDictionary)
        {
            //VARIABLE__NAME__WITH__VALUE: value
            var env = $"{envVariableEntry.Key.Replace(".", "__")}{valueDelimiter} {envVariableEntry.Value}";
            logger.LogTrace("Variable converted from {entry} to {env}", envVariableEntry, env);
            envVariableDefinitionArray.Add(env);
        }

        return envVariableDefinitionArray;
    }
}