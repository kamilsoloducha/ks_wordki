using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using TestStack.BDDfy;

namespace Wordki.Tests.UI.Lesson;

[TestFixture]
class FiszkiCorrectAnswer : LessonTestBase
{
    void GivenLoginUser() => LoginUser();
    void AndGivenLessonSetup() => SetFiszkiLesson();
    void AndGivenLessonStarted() => _lessonPage.StartButton.Click();

    void WhenCheckHasBeenClicked() => _lessonPage.CheckButton.Click();
    void AndWhenWrongHasBeenClicked() => _lessonPage.CorrectButton.Click();

    void ThenShowsRemained() => _lessonPage.Remaining.Text.Should().Be("1");
    void AndThenShowsCounter() => _lessonPage.Counter.Text.Should().Be("1");
    void AndThenShowsCorrect() => _lessonPage.Correct.Text.Should().Be("1");
    void AndThenShowsAccepted() => _lessonPage.Accepted.Text.Should().Be("0");
    void AndThenShowsWrong() => _lessonPage.Wrong.Text.Should().Be("0");

    void AndThenServerReceiveRequest() => Server.LogEntries.Should()
        .Contain(x => x.RequestMessage.Method == HttpMethod.Post.Method &&
                      x.RequestMessage.Path == "/lesson/answer" &&
                      x.RequestMessage.Body == "{\"userId\":\"userid\",\"sideId\":\"sideId\",\"result\":1}");

    [Test]
    public void SendAnswerWhenFiszkiCorrectAnswer() => this.BDDfy();
}