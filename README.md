# ResultMini NuGet Package

`ResultMini` is a C# library that introduces a `Result<T>` struct designed to represent a result that can either contain a value of type `T` or a collection of errors. This is useful for scenarios where operations might fail and you need to capture errors along with the successful result.

## Installation

You can install the `ResultMini` package via NuGet Package Manager or the .NET CLI.

### NuGet Package Manager

```sh
Install-Package ResultMini
```

### .NET CLI

```sh
dotnet add package ResultMini
```

## Usage

### Returning from a function

Here is how you can create a function that returns a `Result<T>` type.

```cs
public Result<string> GetWeather(string location)
{
    try
    {
        // Imagine we make an HTTP GET request here to fetch weather information
        string weatherData = "Sunny, 25°C"; // Placeholder for actual HTTP request
        return weatherData; // Implicit conversion from string to Result<string>
    }
    catch (HttpRequestException httpEx)
    {
        // Handle HTTP-specific errors with error code 1001 for HTTP request failures
        return new Error(1001, $"HTTP Request failed: {httpEx.Message}"); // Return error result
    }
    catch (TimeoutException timeoutEx)
    {
        // Handle timeout errors with error code 1002 for timeouts
        return new Error(1002, $"Request timed out: {timeoutEx.Message}"); // Return error result
    }
    catch (Exception ex)
    {
        // Handle general errors with error code 1000 for generic errors
        return new Error(1000, $"Unexpected error occurred: {ex.Message}"); // Return error result
    }
}
```

### Resolving the output

Here is how to use and handle the result errors

```cs
[HttpGet("getWeather/{location}")]
public IActionResult GetWeather(string location)
{
    // Call the service method that returns a Result<string>
    Result<string> weatherResult = _weatherService.GetWeather(location);

    var (data, errors) = weatherResult;

    // If there are errors, return a corresponding status code and error message
    if (errors.Any())
    {
        var error = errors.First();
        
        // You can map error codes to appropriate HTTP status codes
        switch (error.Code)
        {
            case 1001:
                return StatusCode(500, new { Error = error.Message }); // Internal Server Error for HTTP request issues
            case 1002:
                return StatusCode(408, new { Error = error.Message }); // Request Timeout
            case 1000:
            default:
                return StatusCode(500, new { Error = error.Message }); // Generic Internal Server Error
        }
    }

    // If no errors, return the weather data
    return Ok(new { Weather = data });
}
```
