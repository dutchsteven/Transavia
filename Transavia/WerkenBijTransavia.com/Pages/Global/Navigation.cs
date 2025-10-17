using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Transavia.WerkenBijTransavia.com.Pages.Global;

public class Navigation(WebDriverWait wait)
{
    // Locators
    private static By MenuButton(string menuName) => By.XPath($"//button[normalize-space() = '{menuName}'] | //a[normalize-space() = '{menuName}']");
    private static By MenuItem(string itemName) => By.XPath($"//a[normalize-space() = '{itemName}']");
    private static By JobAlertButton => By.CssSelector("a.whatsapp-button");

    // Actions

    /// <summary>
    /// Navigates through the website's header menu.
    /// </summary>
    /// <param name="txtButton">The name of the menu button to be clicked.</param>
    /// <param name="txtItem">The name of the menu item to be selected within the menu. If null or empty, this step will be skipped.</param>
    public void HeaderMenu(string txtButton, string txtItem)
    {
        if (string.IsNullOrWhiteSpace(txtButton)) throw new ArgumentException("Menu button cannot be null or empty.");
        
        var menuBtn = wait.Until(ExpectedConditions.ElementToBeClickable(MenuButton(txtButton.Trim())));
        menuBtn.Click();

        if (string.IsNullOrWhiteSpace(txtItem)) return;
        
        var item = wait.Until(ExpectedConditions.ElementToBeClickable(MenuItem(txtItem.Trim())));
        item.Click();
    }

    /// <summary>
    /// Clicks on the "Job Alert" button within the website to initiate an action associated with job alerts.
    /// </summary>
    public void ClickJobAlert()
    {
        var button = wait.Until(ExpectedConditions.ElementToBeClickable(JobAlertButton));
        button.Click();
    }
}