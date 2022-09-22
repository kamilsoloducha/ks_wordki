using System;
using System.Linq;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using TestStack.BDDfy;
using Wordki.Tests.UI.Lesson;
using Wordki.Tests.UI.Utils;

namespace Wordki.Tests.UI.LessonSettings;

[TestFixture]
public class LessonRepetition : UITestBase
{
    private LessonSettingsPage _settingsPage;
    private LessonPage _lessonPage;

    [SetUp]
    public void Setup()
    {
        _settingsPage = new LessonSettingsPage(Driver, ClientHost);
        _lessonPage = new LessonPage(Driver, ClientHost);
        Server
            .AddPostEndpoint("/repeats/count", 100, x => true)
            .AddPostEndpoint("/repeats", new { repeats = new[] { new { } } }, x => true)
            .AddGetEndpoint("/groups/lesson/userid", new object[] { new { } })
            .AddPostEndpoint("/lesson/start", new { StartDate = new DateTime(2022, 2, 2) }, x => true);
    }

    void GivenCookies() => LoginUser();

    void WhenUserNavigatesToSettings() => _settingsPage.NavigateTo();
    void AndWhenUserSetsLanguage() => _settingsPage.SelectEnglishLanguage();
    void AndWhenUserSetsMode() => _settingsPage.SelectFiszki();
    void AndWhenUserSetsCount() => _settingsPage.SelectAllCards();
    void AndWhenUserClicksStart() => _settingsPage.StartLesson();
    void ThenLessonPageIsLoaded() => _lessonPage.WaitForLoaded();

    void AndThenLessonStartIsCalled()
    {
        var startLessonRequest = Server.LogEntries.SingleOrDefault(x =>
            x.RequestMessage.Path == "/lesson/start" && x.RequestMessage.Method == "POST");
        startLessonRequest.RequestMessage.Body.Should().Contain("{\"userId\":\"userid\",\"lessonType\":1}");
    }

    void AndThenRepeatsIsCalled()
    {
        var repeatsRequest = Server.LogEntries.SingleOrDefault(x =>
            x.RequestMessage.Path == "/repeats" && x.RequestMessage.Method == "POST");
        repeatsRequest.RequestMessage.Body.Should()
            .Contain(
                "{\"count\":100,\"questionLanguage\":[2],\"ownerId\":\"userid\",\"groupId\":null,\"lessonIncluded\":true}");
    }

    [Test]
    public void ExecuteTest() => this.BDDfy();
}