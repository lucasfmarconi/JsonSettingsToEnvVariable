# JSON to Environment Variables Converter

This is a .NET 7 console application that converts a JSON file to the Microsoft environment variables pattern. The converted output is written to a new file, which can then be used to set environment variables. The application requires 3 arguments:

1. The path to the JSON file to be converted (defaults to `appsettings.json`).
2. The path to the output file that will contain the converted environment variables (defaults to `appsettings.env`).
3. The equality delimiter to use in the conversion (defaults to `=`).

## Usage

To use the application, follow these steps:

1. Download the latest release of the application from the GitHub repository.
2. Perform a dotnet build and navigate to the generated binaries (
3. Run the following command, replacing the arguments as needed:

    ```
    ./JsonSettingsToEnvVariable.exe <json-file-path> <env-file-path> <equality-delimiter>
    ```

    For example:

    ```
    ./JsonSettingsToEnvVariable.exe my-json-file.json my-env-file.env :
    ```

    This will convert `my-json-file.json` to the Microsoft environment variables pattern, using `:` as the equality delimiter, and writing the output to `my-env-file.env`.
    
4. The log level can be changed using the application default appsettings.json in the Logging section

5. The converted output will be written to the specified output file, using the following format:

    ```
    KEY=VALUE
    ```

    For example:

    ```
    DB_HOST=localhost
    DB_PORT=5432
    DB_NAME=mydatabase
    DB_USER=myusername
    DB_PASSWORD=mypassword
    ```

6. You can then use this file to set environment variables in your system or application, depending on your needs.

## Contributing

Contributions to this project are welcome! To contribute, please follow these steps:

1. Fork the repository.
2. Create a new branch: `git checkout -b my-branch-name`
3. Make your changes and commit them: `git commit -m 'Add some feature'`
4. Push to the branch: `git push origin my-branch-name`
5. Submit a pull request.

## Dependencies

This project uses the following dependencies:

- .NET Core 7.0
- JsonFlatten

### .NET Core 7.0

- Installation instructions can be found on the [Microsoft .NET Core website](https://dotnet.microsoft.com/download/dotnet-core/7.0).

### JsonFlatten

**Special thanks to https://github.com/GFoley83/JsonFlatten ;)**

## License

This project is licensed under the MIT license. See the `LICENSE` file for more information.
