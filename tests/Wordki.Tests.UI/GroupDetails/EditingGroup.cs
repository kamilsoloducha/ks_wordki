using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TestStack.BDDfy;
using Wordki.Tests.UI.GroupDialog;

namespace Wordki.Tests.UI.GroupDetails
{
    class EditingGroup : UITestBase
    {
        protected string CardsPath = "/cards/userid/1";
        protected string GroupDetailsPath = "/groups/details/1";
        protected string UpdateGroupPath = "/groups/update";
        private readonly GroupDetailsPage _groupDetails;
        private readonly GroupDialogPage _groupDialog;

        public EditingGroup()
        {
            _groupDetails = new GroupDetailsPage(Driver);
            _groupDialog = new GroupDialogPage(Driver);
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
            .AddGetEndpoint(GroupDetailsPath, new { id = 1, name = "groupName", front = 0, back = 0 })
            .AddPutEndpoint(UpdateGroupPath, 1, x => true);

        void AndGivenGroupDetailsPage() => _groupDetails.NavigateTo();
        void WhenPageIsLoaded() => _groupDetails.WaitForInitialLoad();
        void AndWhenEditGroupIsClicked() => _groupDetails.OpenEditGroupDialog();
        void AndWhenValuesChanged()
        {
            _groupDialog.GroupName.SendKeys("change");
            _groupDialog.SelectFront(1);
            _groupDialog.SelectBack(2);
        }
        void AndWhenSaveIsClicked() => _groupDialog.SaveButton.Click();
        void AndWhenProcessinIsFinshed() => _groupDialog.WaitForFinish();

        void ThenServerRecivedUpdateRequest()
        {
            var request = Server.LogEntries.SingleOrDefault(x => x.RequestMessage.Path == UpdateGroupPath && x.RequestMessage.Method == "PUT");
            request.Should().NotBeNull();
        }

        [Test]
        public void Test() => this.BDDfy();
    }
}