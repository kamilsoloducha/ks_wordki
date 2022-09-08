using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TestStack.BDDfy;
using Wordki.Tests.UI.CardDialog;

namespace Wordki.Tests.UI.GroupDetails;

[TestFixture(Ignore = "not ready")]
class EditingCard : UITestBase
{
    protected string CardsPath = "/cards/userid/1";
    protected string GroupDetailsPath = "/groups/details/1";
    protected string UpdateCardPath = "/cards/update";
    private readonly GroupDetailsPage _groupDetails;
    private readonly CardDialogPage _cardDialog;

    public EditingCard()
    {
        _groupDetails = new GroupDetailsPage(Driver);
        _cardDialog = new CardDialogPage(Driver);
    }

    void GivenCookies() => SetAuthorizationCookie();

    void AndGivenServerSetup() => Server
        .AddGetEndpoint(CardsPath, new
        {
            cards = new object[]{
                new {
                    id = 1,
                    back = new { type = 2, value = "backValue", example = "backExample", drawer = 3, isUsed = false, isTicked = false },
                    front = new { type = 1, value = "frontValue", example = "frontExample", drawer = 3, isUsed = false, isTicked = false }
                }
            }
        })
        .AddGetEndpoint(GroupDetailsPath, new { id = 1, name = "groupName", front = 1, back = 2 })
        .AddPutEndpoint(UpdateCardPath, 1, x => true);

    void AndGivenGroupDetailsPage() => _groupDetails.NavigateTo();
    void WhenPageIsLoaded() => _groupDetails.WaitForInitialLoad();
    void AndWhenClickIsTicked() => _groupDetails.Cards.First().Click();
    void AndWhenCardDialogIsLoaded() => _cardDialog.WaitForInitialLoad();
    void AndWhenValuesChanged()
    {
        _cardDialog.FrontValue.SendKeys("change");
        _cardDialog.FrontExample.SendKeys("change");
        _cardDialog.FrontEnabled.Click();
        _cardDialog.BackValue.SendKeys("change");
        _cardDialog.BackExample.SendKeys("change");
        _cardDialog.BackEnabled.Click();
        _cardDialog.Comment.SendKeys("change");
        _cardDialog.IsTicked.Click();
    }
    void AndWhenSaveIsClicked() => _cardDialog.SaveButton.Click();
    void AndWhenProcessinIsFinshed() => _cardDialog.WaitForFinish();

    void ThenServerRecivedUpdateRequest()
    {
        var request = Server.LogEntries.SingleOrDefault(x => x.RequestMessage.Path == UpdateCardPath && x.RequestMessage.Method == "PUT");
        request.Should().NotBeNull();
    }

    [Test]
    public void Test() => this.BDDfy();
}