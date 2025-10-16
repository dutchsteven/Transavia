using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Reqnroll;

namespace Transavia.WerkenBijTransavia.com.Pages.Global;

public class PageBase(IWebDriver driver, WebDriverWait wait, ScenarioContext scenarioContext)
{
    /// <summary>
    /// Navigates to the specified URL in the browser.
    /// </summary>
    /// <param name="url">The URL to navigate to.</param>
    public void NavigateTo(string url)
    {
        driver.Navigate().GoToUrl(url);
        
        if (!scenarioContext.ScenarioInfo.Tags.Contains("cookies")) AcceptCookies();
    }

    /// <summary>
    /// Accepts all cookies on a web page if the cookie banner is displayed.
    /// </summary>
    /// <remarks>
    /// This method attempts to locate and click the button associated with accepting all cookies.
    /// If the cookie banner is not visible within the specified wait timeout, the operation is skipped without throwing an exception.
    /// </remarks>
    private void AcceptCookies()
    {
        try
        {
            var cookieButton = wait.Until(driver => 
                driver.FindElement(By.CssSelector("[data-testid='agree-to-all-cookies']")));
            cookieButton.Click();
        }
        catch (WebDriverTimeoutException)
        {
            // Cookie banner not visible, continue
        }
    }

    /// <summary>
    /// Verifies that a new browser tab has opened and switches the WebDriver context to the newly opened tab.
    /// </summary>
    public void VerifyNewTabOpens()
    {
        wait.Until(drv => drv.WindowHandles.Count > 1);
        driver.SwitchTo().Window(driver.WindowHandles.Last());
    }
}