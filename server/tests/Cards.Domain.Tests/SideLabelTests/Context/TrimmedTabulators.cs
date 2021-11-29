namespace Cards.Domain.Tests.SideLabelTests
{
    public class TrimmedTabulators : SideLabelCreateContext
    {
        public override string GivenValue => "\ttext\t";
        public override string ExpectedValue => "text";
    }

}