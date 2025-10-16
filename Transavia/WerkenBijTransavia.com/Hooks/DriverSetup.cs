using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Reqnroll;
using Reqnroll.BoDi;

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

        //_driver.Manage().Window.Maximize();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

        // Register instances with DI container
        objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
        objectContainer.RegisterInstanceAs(_wait);
    }

    [AfterScenario]
    public void CloseBrowser()
    {
        // Take screenshot if scenario failed
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
            
            // Create screenshots directory if it doesn't exist
            var screenshotDir = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
            Directory.CreateDirectory(screenshotDir);
            
            // Generate filename with timestamp and scenario name
            var scenarioName = scenarioContext.ScenarioInfo.Title.Replace(" ", "_");
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            var fileName = $"{scenarioName}_{timestamp}.png";
            var filePath = Path.Combine(screenshotDir, fileName);
            
            // Save screenshot
            screenshot.SaveAsFile(filePath);
            
            // Attach screenshot to test report (for SpecFlow+ LivingDoc or other reporters)
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
        var browser = Environment.GetEnvironmentVariable("browser")?.ToLower() ?? "chrome";
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
        //options.AddArguments("--headless", "--no-sandbox", "--disable-dev-shm-usage");
        return new ChromeDriver(options);
    }    

    private static IWebDriver CreateFirefoxDriver()
    {
        var options = new FirefoxOptions();
        options.AddArguments("--headless", "--no-sandbox", "--disable-dev-shm-usage");
        return new FirefoxDriver(options);
    }    
}