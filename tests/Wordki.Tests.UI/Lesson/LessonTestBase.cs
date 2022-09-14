using System;
using NUnit.Framework;
using Wordki.Tests.UI.LessonSettings;
using Wordki.Tests.UI.Utils;

namespace Wordki.Tests.UI.Lesson;

class LessonTestBase : UITestBase
{
    private readonly LessonSettingsPage _settingsPage;
    protected readonly LessonPage _lessonPage;

    public LessonTestBase()
    {
        _settingsPage = new LessonSettingsPage(Driver, ClientHost);
        _lessonPage = new LessonPage(Driver, ClientHost);
    }

    [SetUp]
    public void Setup()
    {
        Server
            .AddPostEndpoint("/lesson/answer", new { }, x => true)
            .AddPostEndpoint("/repeats/count", 100, x => true)
            .AddPostEndpoint("/repeats", new
            {
                repeats = new[]
                {
                    new
                    {
                        sideId = "sideId",
                        cardId = "cardId",
                        questionSide = 1,
                        question = "question",
                        questionExample = string.Empty,
                        questionDrawer = 1,
                        answer = "answer",
                        answerExample = string.Empty,
                        answerSide = 2,
                        frontLanguage = 1,
                        backLanguage = 2,
                        comment = string.Empty,
                        groupId = "groupId",
                    },
                    new
                    {
                        sideId = "sideId",
                        cardId = "cardId",
                        questionSide = 1,
                        question = "question",
                        questionExample = string.Empty,
                        questionDrawer = 1,
                        answer = "answer",
                        answerExample = string.Empty,
                        answerSide = 2,
                        frontLanguage = 1,
                        backLanguage = 2,
                        comment = string.Empty,
                        groupId = "groupId",
                    }
                }
            }, x => true)
            .AddGetEndpoint("/groups/lesson/userid",
                new
                {
                    groups = new object[]
                    {
                        new
                        {
                            ownerId = "owerId", id = 1, name = "name", front = 2, back = 1, frontCount = 100,
                            backCount = 100
                        }
                    }
                })
            .AddPostEndpoint("/lesson/start", new { StartDate = new DateTime(2022, 2, 2) }, x => true);
    }

    protected void SetFiszkiLesson()
    {
        _settingsPage.NavigateAndEnsure();
        _settingsPage.SelectEnglishLanguage();
        _settingsPage.SelectFiszki();
        _settingsPage.SelectAllCards();
        _settingsPage.StartLesson();
        _lessonPage.WaitForLoaded();
    }
    
    protected void SetInsertingLesson()
    {
        _settingsPage.NavigateAndEnsure();
        _settingsPage.SelectEnglishLanguage();
        _settingsPage.SelectInserting();
        _settingsPage.SelectAllCards();
        _settingsPage.StartLesson();
        _lessonPage.WaitForLoaded();
    }
}