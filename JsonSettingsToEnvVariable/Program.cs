using JsonSettingsToEnvVariable.Converter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace JsonSettingsToEnvVariable;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            var serviceProvider = BuildService(out var logger);

            logger.LogInformation("Application starting...");

            var worker = serviceProvider.GetService<Worker>() ?? throw new ArgumentNullException(nameof(Worker));

            if (args is not null && args.Length > 0)
                worker.DoWork(args);

            var jsonFilePath = "appsettings.json";
            var envFilePath = "appsettings.env";
            var equalityEnvDelimiter = "=";

            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine($"Enter a settings json file path (press enter to use './{jsonFilePath}' path): ");
            var inputPath = Console.ReadLine();
            jsonFilePath = string.IsNullOrWhiteSpace(inputPath) ? jsonFilePath : inputPath;

            Console.WriteLine($"Enter the result env file path (press enter to use './{envFilePath}' path): ");
            var inputEnvPath = Console.ReadLine();
            envFilePath = string.IsNullOrWhiteSpace(inputEnvPath) ? envFilePath : inputEnvPath;

            Console.WriteLine(
                $"Enter a equality env delimiter (press enter to use '{equalityEnvDelimiter}' default. E.g. VARIABLE__NAME{equalityEnvDelimiter} Value)");
            var inputDelimiter = Console.ReadLine();
            equalityEnvDelimiter = string.IsNullOrWhiteSpace(inputDelimiter) ? equalityEnvDelimiter : inputDelimiter;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Converting values...");

            worker.DoWork(new[] { jsonFilePath, envFilePath, equalityEnvDelimiter });

            logger.LogInformation("Application finished.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            Console.ReadKey();
        }
    }

    private static ServiceProvider BuildService(out ILogger<Program> logger)
    {
        var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

        var serviceCollection = new ServiceCollection()
            .AddLogging(c =>
            {
                c.AddSimpleConsole(options =>
                {
                    options.SingleLine = true;
                    options.TimestampFormat = "(HH:mm:ss) ";
                });
                c.AddConfiguration(configurationRoot.GetSection("Logging"));
            });

        //Inject/Register dependencies
        serviceCollection.AddConverterServices();
        serviceCollection.AddSingleton<Worker>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        logger = (serviceProvider.GetService<ILoggerFactory>() ??
                  throw new ArgumentNullException(nameof(ILoggerFactory)))
            .CreateLogger<Program>();

        logger.LogTrace("Services injected...");

        return serviceProvider;
    }
}
