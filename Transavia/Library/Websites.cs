namespace Transavia.Library;

public static class Websites
{
    public static class WerkenBijTransaviaCom
    {
        private static readonly string Environment = System.Environment.GetEnvironmentVariable("env") ?? "www";
        
        public static string BaseUrl() => $"https://{Environment}.werkenbijtransavia.com";
    }
}