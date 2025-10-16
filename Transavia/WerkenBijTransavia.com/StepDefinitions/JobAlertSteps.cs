using Reqnroll;
using Transavia.WerkenBijTransavia.com.Pages;
using Transavia.WerkenBijTransavia.com.Pages.Global.Elements;

namespace Transavia.WerkenBijTransavia.com.StepDefinitions;

[Binding]
public class JobAlertSteps(JobAlertPage jobAlertPage, Button button)
{

    [Then(@"I see the Jobalert popup")]
    public void ThenISeeTheJobalertPopup()
    {
        Assert.That(jobAlertPage.IsJobAlertPopupVisible(), Is.True, "JobAlert popup should be visible");
    }

    [When(@"I fill out the form")]
    public void WhenIFillOutTheForm(Table table)
    {
        // Example of table:
        // | Type      | Field                                                   | Value                 |
        // | TextField | E-mailadres                                             | Test@TransaviaTest.nl |
        // | Dropdown  | Voorkeursafdeling                                       | IT                    |
        // | Radio     | Voorkeurstaal                                           | English               |
        // | Radio     | Hoe vaak wil je vacaturemeldingen ontvangen?            | Zo spoedig mogelijk   |
        // | Checkbox  | Ik ga ermee akkoord e-mails van Transavia te ontvangen. | true                  |
        
        var dataSet = table.CreateSet<(string, string, string)>();
        foreach (var (type, field, value) in dataSet)
        {
            switch (type)
            {
                case "TextField":
                    jobAlertPage.EnterEmail(value);
                    break;
                case "Dropdown":
                    button.ClickByAriaLabel(field);
                    jobAlertPage.SelectDepartment(value);
                    button.ClickByAriaLabel(field);
                    break;
                case "Radio":
                    jobAlertPage.SelectByLabel(field, value);
                    break;
                case "Checkbox":
                    jobAlertPage.CheckboxByText(field, bool.Parse(value));   
                    break; 
                default: throw new NotImplementedException($"Form type '{type}' is not implemented yet.");
            }
        }
    }

    [Then(@"I can submit the Jobalert")]
    public void ThenICanSubmitTheJobalert()
    {
        button.Submit();
    }
}