using System;
using Utils;

namespace Cards.Domain
{
    public interface INextRepeatCalculator
    {
        DateTime Calculate(CardSide side, int result);
    }

    public class StandartCalculator : INextRepeatCalculator
    {
        private const double powerBase = 1.65;
        public DateTime Calculate(CardSide side, int result)
        {
            var daysToAdded = 0;
            if (result < 0)
                daysToAdded = 1;
            else if (result == 0)
                daysToAdded = 2;
            else
                daysToAdded = (int)Math.Ceiling(Math.Pow(powerBase, side.Drawer.RealValue));
            return SystemClock.Now.AddDays(daysToAdded);
        }
    }
}