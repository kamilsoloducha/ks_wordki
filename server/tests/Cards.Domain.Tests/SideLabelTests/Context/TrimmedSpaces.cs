namespace Cards.Domain.Tests.SideLabelTests
{
    public class TrimmedSpaces : SideLabelCreateContext
    {
        public override string GivenValue => " text ";
        public override string ExpectedValue => "text";
    }

}