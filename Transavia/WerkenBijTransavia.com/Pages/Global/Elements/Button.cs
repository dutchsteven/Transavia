using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Transavia.WerkenBijTransavia.com.Pages.Global.Elements;

public class Button(WebDriverWait wait)
{
    // Locators
    private static By Btn(string value) => By.XPath($"//button[normalize-space() = '{value}']");
    private static By AriaLabel(string value) => By.XPath($"//*[@aria-label='{value}']");
    
    private static By SubmitButton => By.CssSelector("button[type='submit'], .submit-button");
    
    // Actions

    /// <summary>
    /// Checks if a button with the specified text is visible on the page.
    /// </summary>
    /// <param name="value">The text of the button.</param>
    public void IsVisible(string value)
    {
        wait.Until(ExpectedConditions.ElementIsVisible(Btn(value)));
    }

    /// <summary>
    /// Clicks a button with the specified text on the page.
    /// </summary>
    /// <param name="value">The text of the button.</param>
    public void Click(string value)
    {
        var btn = wait.Until(ExpectedConditions.ElementToBeClickable(Btn(value)));
        btn.Click();
    }

    public void ClickByAriaLabel(string value)
    {
        var btn = wait.Until(ExpectedConditions.ElementToBeClickable(AriaLabel(value)));
        btn.Click();   
    }

    /// <summary>
    /// Submits a form by clicking the submit button on the page.
    /// </summary>
    public void Submit()
    {
        var btn = wait.Until(ExpectedConditions.ElementToBeClickable(SubmitButton));
        // btn.Click();
        // ToDo: Click should get enabled when implemented on non-PRD environment
    }
}