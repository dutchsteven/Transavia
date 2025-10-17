using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Reqnroll;
using Reqnroll.BoDi;
using Transavia.Library.Helpers;

namespace Transavia.WerkenBijTransavia.com.Hooks;

[Binding]
public class DriverSetup(IObjectContainer objectContainer, ScenarioContext scenarioContext)
{
    private IWebDriver? _driver;
    private WebDriverWait? _wait;

    [BeforeScenario]
    public void SetupBrowser()
    {
        _driver = SelectBrowser();

        _driver.Manage().Window.Size = GetRandomScreenSize();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

        // Register instances with a DI container
        objectContainer.RegisterInstanceAs(_driver);
        objectContainer.RegisterInstanceAs(_wait);
    }

    [AfterScenario]
    public void CloseBrowser()
    {
        // Take a screenshot if a scenario failed
        if (scenarioContext.TestError != null && _driver != null)
        {
            TakeScreenshot();
        }

        _driver?.Quit();
        _driver?.Dispose();
    }

    private void TakeScreenshot()
    {
        try
        {
            var screenshot = ((ITakesScreenshot)_driver!).GetScreenshot();
            
            // Creates a screenshots directory if it doesn't exist
            var screenshotDir = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
            Directory.CreateDirectory(screenshotDir);
            
            // Generate filename with timestamp and scenario name
            var scenarioName = scenarioContext.ScenarioInfo.Title.Replace(" ", "_");
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            var fileName = $"{scenarioName}_{timestamp}.png";
            var filePath = Path.Combine(screenshotDir, fileName);
            
            // Save screenshot
            screenshot.SaveAsFile(filePath);
            
            // Attach the screenshot to the test report
            var screenshotBytes = screenshot.AsByteArray;
            scenarioContext.Add("Screenshot", screenshotBytes);
            
            Console.WriteLine($"Screenshot saved: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to take screenshot: {ex.Message}");
        }
    }
    
    private static IWebDriver SelectBrowser()
    {
        var browser = EnvironmentHelper.GetVariable("browser");
        return browser switch
        {
            "firefox" => CreateFirefoxDriver(),
            "chrome" => CreateChromeDriver(),
            _ => throw new NotSupportedException($"Browser '{browser}' is not supported. Please configure a supported browser in the 'BROWSER' environment variable.")
        };
    }

    private static IWebDriver CreateChromeDriver()
    {
        var options = new ChromeOptions();
        if (EnvironmentHelper.IsHeadless()) options.AddArguments("--headless");
        return new ChromeDriver(options);
    }    

    private static IWebDriver CreateFirefoxDriver()
    {
        var options = new FirefoxOptions();
        if (EnvironmentHelper.IsHeadless()) options.AddArguments("--headless");
        return new FirefoxDriver(options);
    }

    private static Size GetRandomScreenSize()
    {
        var random = new Random();

        Size[] sizes = 
        [
            new Size(375, 667),    // iPhone SE (mobile small)
            new Size(390, 844),    // iPhone 12/13/14 (mobile medium)
            new Size(428, 926),    // iPhone Pro Max (mobile large)
            new Size(768, 1024),   // iPad (tablet)
            new Size(1024, 1366),  // iPad Pro (tablet large)
            new Size(1366, 768),   // Standard laptop
            new Size(1920, 1080),  // Full HD desktop
            new Size(2560, 1440),  // 2K desktop
            new Size(3440, 1440),  // Ultrawide monitor
            new Size(3840, 2160)   // 4K desktop
        ];

        return sizes[random.Next(0, sizes.Length)];
    }
}