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
        public DateTime Calculate(CardSide side, int result)
        {
            var daysToAdded = 0;
            if (result < 0)
                daysToAdded = 1;
            else if (result == 0)
                daysToAdded = 2;
            else
                daysToAdded = Helpers.GetFibbonacciNumber(side.Drawer.RealValue);
            return SystemClock.Now.AddDays(daysToAdded);
        }


    }
}