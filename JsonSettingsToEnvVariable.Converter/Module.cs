using JsonSettingsToEnvVariable.Core;
using Microsoft.Extensions.DependencyInjection;

namespace JsonSettingsToEnvVariable.Converter;
public static class Module
{
    public static void AddConverterServices(this IServiceCollection services)
    {
        services.AddSingleton<IJsonReader, JsonReader>();
        services.AddSingleton<IEnvVariablePatternConverter, EnvVariablePatternConverter>();
    }
}