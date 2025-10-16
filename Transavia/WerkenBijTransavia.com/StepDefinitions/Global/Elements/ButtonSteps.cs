using Reqnroll;
using Transavia.WerkenBijTransavia.com.Pages.Global.Elements;

namespace Transavia.WerkenBijTransavia.com.StepDefinitions.Global.Elements;

[Binding]
public class ButtonSteps(Button button)
{
    [Then(@"I see the button '(.*)'")]
    public void ThenISeeTheButton(string value)
    {
        button.IsVisible(value);
    }
    
    [Given(@"I click the button '(.*)'")]
    [When(@"I click the button '(.*)'")]
    public void WhenIClickTheButton(string value)
    {
        button.Click(value);
    }
}