using System;

namespace Cards.Domain
{
    public interface INextRepeatCalculator
    {
        DateTime Calculate(CardSide side, int result);
    }

    public class StandartCalculator : INextRepeatCalculator
    {
        public DateTime Calculate(CardSide side, int result)
        {
            return side.NextRepeat.Value.AddDays(2);
        }
    }
}