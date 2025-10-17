using Transavia.Library.Helpers;

namespace Transavia.Library;

public static class Websites
{
    public static class WerkenBijTransaviaCom
    {
        public static string BaseUrl() => $"https://{ EnvironmentHelper.GetVariable("prefix") }.werkenbijtransavia.com";
    }
}