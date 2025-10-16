using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Reqnroll;
using Transavia.WerkenBijTransavia.com.Pages;
using Transavia.WerkenBijTransavia.com.Pages.Global;

namespace Transavia.WerkenBijTransavia.com.StepDefinitions.Global;

[Binding]
public class NavigationSteps(Navigation navigation, PageBase pageBase)
{
    [Given(@"I navigate to '(.*)'")]
    public void GivenINavigateTo(string navigation1)
    {
        var txtButton = navigation1.Split(",")[0];
        var txtItem = navigation1.Split(",")[1];
        
        navigation.HeaderMenu(txtButton, txtItem);
    }

    [Given(@"I click Jobalert")]
    public void GivenIClickJobalert()
    {
        navigation.ClickJobAlert();
        pageBase.VerifyNewTabOpens();
    }
    
    [When(@"I click Jobalert")]
    public void WhenIClickJobalert()
    {
        navigation.ClickJobAlert();
    }

    [Then("a new tab opens")]
    public void ThenANewTabOpens()
    {
        pageBase.VerifyNewTabOpens();
    }

}