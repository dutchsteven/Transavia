using Reqnroll;
using Transavia.Library;
using Transavia.WerkenBijTransavia.com.Pages.Global;

namespace Transavia.WerkenBijTransavia.com.Hooks;

[Binding]
public class Hooks(PageBase landingPage)
{
    [BeforeScenario]
    public void GoToBaseUrl()
    {
        landingPage.NavigateTo(Websites.WerkenBijTransavia_com.BaseUrl());
    }
}