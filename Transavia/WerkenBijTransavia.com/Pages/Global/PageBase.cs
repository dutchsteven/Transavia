using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Reqnroll;

namespace Transavia.WerkenBijTransavia.com.Pages.Global;

public class PageBase(IWebDriver driver, WebDriverWait wait, ScenarioContext scenarioContext)
{
    public void NavigateTo(string url)
    {
        driver.Navigate().GoToUrl(url);
        
        if (!scenarioContext.ScenarioInfo.Tags.Contains("cookies")) AcceptCookies();
    }

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

}