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
            return DateTime.Now.AddDays(2);
        }
    }
}