using JsonSettingsToEnvVariable.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace JsonSettingsToEnvVariable;
internal class Worker
{
    private readonly IJsonReader jsonReader;
    private readonly IEnvVariablePatternConverter patternConverter;
    private readonly ILogger<Worker> logger;

    public Worker(IJsonReader jsonReader, ILogger<Worker> logger, IEnvVariablePatternConverter patternConverter)
    {
        this.jsonReader = jsonReader;
        this.logger = logger;
        this.patternConverter = patternConverter;
    }

    public void DoWork(string[] args)
    {
        var jsonFilePath = args[0];
        logger.LogInformation("Reading file from the relative path ./{path}", jsonFilePath);
        var result = jsonReader.ReadDictionaryFromJsonFile(jsonFilePath);

        var valueDelimiter = args[2];
        logger.LogInformation("Using '{delimiter}' env variables value delimiter.", valueDelimiter);

        var envResult = patternConverter.ConvertDictionaryToEnvVariablePatternList(result, valueDelimiter);
        WriteResultToFile(args, envResult);
    }

    private void WriteResultToFile(IReadOnlyList<string> args, IEnumerable<string> envResult)
    {
        var resultFilePath = args[1];
        ArgumentException.ThrowIfNullOrEmpty(resultFilePath);

        logger.LogInformation("Writting result file to relative path: ./{path}", resultFilePath);

        if (File.Exists(resultFilePath))
            File.Delete(resultFilePath);

        File.WriteAllLines(resultFilePath, envResult);
    }
}
