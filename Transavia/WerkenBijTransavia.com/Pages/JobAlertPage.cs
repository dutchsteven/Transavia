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

    /// <summary>
    /// Determines whether the Job Alert popup is visible on the page.
    /// </summary>
    /// <returns>
    /// True if the Job Alert popup is visible; otherwise, false.
    /// </returns>
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

    /// <summary>
    /// Enters the specified email address into the email input field on the Job Alert page.
    /// </summary>
    /// <param name="email">The email address to be entered.</param>
    public void EnterEmail(string email)
    {
        var emailField = wait.Until(ExpectedConditions.ElementToBeClickable(EmailInput));
        emailField.Clear();
        emailField.SendKeys(email);
    }

    /// <summary>
    /// Selects a department from the dropdown by the specified department name.
    /// </summary>
    /// <param name="department">The name of the department to be selected.</param>
    public void SelectDepartment(string department)
    {
        var select = wait.Until(ExpectedConditions.ElementToBeClickable(DepartmentSelect(department)));
        select.Click();   
    }

    /// <summary>
    /// Selects an option specified by a label within a fieldset with a given legend.
    /// </summary>
    /// <param name="legend">The legend text of the fieldset containing the label.</param>
    /// <param name="value">The label text of the option to be selected.</param>
    public void SelectByLabel(string legend, string value)
    {
        var fieldset = wait.Until(ExpectedConditions.ElementIsVisible(Fieldset(legend)));
        var label = fieldset.FindElement(Label(value));
        label.Click();   
    }

    /// <summary>
    /// Toggles a checkbox identified by its label text to the specified value.
    /// </summary>
    /// <param name="text">The label text associated with the checkbox.</param>
    /// <param name="value">The desired state of the checkbox (true for checked, false for unchecked).</param>
    public void CheckboxByText(string text, bool value)
    {
        var checkbox = wait.Until(ExpectedConditions.ElementToBeClickable(Span(text)));
        if (checkbox.Selected != value) checkbox.Click();
    }
}