using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Transavia.WerkenBijTransavia.com.Pages;

public class JobAlertPage(WebDriverWait wait)
{
    // Locators
    private static By JobAlertPopup => By.CssSelector("[data-testid='jobalert-popup'], .jobalert-popup, [role='dialog']");
    private static By EmailInput => By.CssSelector("input[type='email'], input[name='email']");
    private static By DepartmentSelect(string department) => By.XPath($"//li[normalize-space() = '{department}']");
    private static By Fieldset(string legend) => By.XPath($"//fieldset[legend[normalize-space()= '{legend}']]");
    private static By Label(string label) => By.XPath($"//label[normalize-space() = '{label}']");
    private static By Span(string span) => By.XPath($"//span[normalize-space() = '{span}']");
    
    // Actions
    public bool IsJobAlertPopupVisible()
    {
        try
        {
            var popup = wait.Until(ExpectedConditions.ElementIsVisible(JobAlertPopup));
            return popup.Displayed;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    public void EnterEmail(string email)
    {
        var emailField = wait.Until(ExpectedConditions.ElementToBeClickable(EmailInput));
        emailField.Clear();
        emailField.SendKeys(email);
    }

    public void SelectDepartment(string department)
    {
        var select = wait.Until(ExpectedConditions.ElementToBeClickable(DepartmentSelect(department)));
        select.Click();   
    }
    

    public void SelectByLabel(string legend, string value)
    {
        var fieldset = wait.Until(ExpectedConditions.ElementIsVisible(Fieldset(legend)));
        var label = fieldset.FindElement(Label(value));
        label.Click();   
    }

    public void CheckboxByText(string text, bool value)
    {
        var checkbox = wait.Until(ExpectedConditions.ElementToBeClickable(Span(text)));
        if (checkbox.Selected != value) checkbox.Click();
    }
}