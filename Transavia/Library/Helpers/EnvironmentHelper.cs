namespace Transavia.Library.Helpers;

public static class EnvironmentHelper
{
    public static bool IsProduction() => GetVariable("env") == "prd";   

    public static bool IsHeadless()
    {
        return !bool.TryParse(GetVariable("headless"), out var headless) ? 
            throw new Exception("Please configure the 'headless' environment variable to 'true' or 'false'.") : headless;
    }

    public static string GetVariable(string name)
    {
        return Environment.GetEnvironmentVariable(name) ?? throw new Exception($"Please configure the '{name}' environment variable.");   
    }
}