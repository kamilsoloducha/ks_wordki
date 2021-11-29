namespace Cards.Domain.Tests.SideLabelTests
{
    public class SimpleSentence : SideLabelCreateContext
    {
        public override string GivenValue => "text text.";
        public override string ExpectedValue => "text text.";
    }

}