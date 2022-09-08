using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TestStack.BDDfy;
using Wordki.Tests.UI.Lesson;

namespace Wordki.Tests.UI.LessonSettings;

[TestFixture(Ignore = "not ready")]
public class LessonRepetition : UITestBase
{
    private readonly LessonSettingsPage _settingsPage;
    private readonly LessonPage _lessonPage;

    public LessonRepetition()
    {
        _settingsPage = new LessonSettingsPage(Driver);
        _lessonPage = new LessonPage(Driver);
    }

    void GivenCookies() => SetAuthorizationCookie();

    void AndGivenServerSetup() => Server
        .AddPostEndpoint("/repeats/count", 100, x => true)
        .AddPostEndpoint("/repeats", new object[] { new { } }, x => true)
        .AddGetEndpoint("/groups/lesson/userid", new object[] { })
        .AddPostEndpoint("/lesson/start", new { StartDate = new DateTime(2022, 2, 2) }, x => true);

    void WhenUserNavigatesToSettings() => _settingsPage.NavigateTo();
    void AndWhenUserSetsLanguage() => _settingsPage.SelectEnglishLanguage();
    void AndWhenUserSetsMode() => _settingsPage.SelectFiszki();
    void AndWhenUserSetsCount() => _settingsPage.SelectAllCards();
    void AndWhenUserClicksStart() => _settingsPage.StartLesson();
    void ThenLessonPageIsLoaded() => _lessonPage.WaitForLoaded();
    void AndThenLessonStartIsCalled()
    {
        var startLessonRequest = Server.LogEntries.SingleOrDefault(x => x.RequestMessage.Path == "/lesson/start" && x.RequestMessage.Method == "POST");
        startLessonRequest.RequestMessage.Body.Should().Contain("{\"userId\":\"userid\",\"lessonType\":1}");
    }

    void AndThenRepeatsIsCalled()
    {
        var repeatsRequest = Server.LogEntries.SingleOrDefault(x => x.RequestMessage.Path == "/repeats" && x.RequestMessage.Method == "POST");
        repeatsRequest.RequestMessage.Body.Should().Contain("{\"count\":100,\"questionLanguage\":[2],\"ownerId\":\"userid\",\"groupId\":null,\"lessonIncluded\":true}");
    }

    [Test]
    public void ExecuteTest() => this.BDDfy();
}

[TestFixture(Ignore = "not ready")]
public class LessonNew : UITestBase
{
    private readonly LessonSettingsPage _settingsPage;
    private readonly LessonPage _lessonPage;

    public LessonNew()
    {
        _settingsPage = new LessonSettingsPage(Driver);
        _lessonPage = new LessonPage(Driver);
    }

    void GivenCookies() => SetAuthorizationCookie();

    void AndGivenServerSetup() => Server
        .AddPostEndpoint("/repeats/count", 100, x => true)
        .AddPostEndpoint("/repeats", new object[] { new { } }, x => true)
        .AddGetEndpoint("/groups/lesson/userid", new { groups = new object[] { new { ownerId = "owerId", id = 1, name = "name", front = 2, back = 1, frontCount = 100, backCount = 100 } } })
        .AddPostEndpoint("/lesson/start", new { StartDate = new DateTime(2022, 2, 2) }, x => true);

    void WhenUserNavigatesToSettings() => _settingsPage.NavigateTo();
    void AndWhenUserChangeTab() => _settingsPage.NewWrodsTab.Click();
    void AndWhenUserSetsLanguage() => _settingsPage.SelectEnglishLanguage();
    void AndWhenUserSetsMode() => _settingsPage.SelectFiszki();
    void AndWhenUserSetsGroup() => _settingsPage.SelectGroup(0);
    void AndWhenUserSetsCount() => _settingsPage.SelectAllCards();
    void AndWhenUserClicksStart() => _settingsPage.StartLesson();
    void ThenLessonPageIsLoaded() => _lessonPage.WaitForLoaded();
    void AndThenLessonStartIsCalled()
    {
        var startLessonRequest = Server.LogEntries.SingleOrDefault(x => x.RequestMessage.Path == "/lesson/start" && x.RequestMessage.Method == "POST");
        startLessonRequest.RequestMessage.Body.Should().Contain("{\"userId\":\"userid\",\"lessonType\":1}");
    }

    void AndThenRepeatsIsCalled()
    {
        var repeatsRequest = Server.LogEntries.SingleOrDefault(x => x.RequestMessage.Path == "/repeats" && x.RequestMessage.Method == "POST");
        repeatsRequest.RequestMessage.Body.Should().Contain("{\"count\":100,\"questionLanguage\":[2],\"ownerId\":\"userid\",\"groupId\":null,\"lessonIncluded\":true}");
    }

    [Test]
    public void ExecuteTest() => this.BDDfy();
}