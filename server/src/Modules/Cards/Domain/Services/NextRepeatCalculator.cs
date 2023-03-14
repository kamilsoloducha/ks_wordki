using System;
using Cards.Domain.OwnerAggregate;
using Domain.Utils;

namespace Cards.Domain.Services
{
    public interface INextRepeatCalculator
    {
        DateTime Calculate(Details side, int result);
    }

    public class StandartCalculator : INextRepeatCalculator
    {
        public DateTime Calculate(Details side, int result)
        {
            var daysToAdded = 0;
            if (result < 0)
                daysToAdded = 1;
            else if (result == 0)
                daysToAdded = 2;
            else
                daysToAdded = Helpers.GetFibbonacciNumber(side.Drawer.Correct + 3);
            return SystemClock.Now.AddDays(daysToAdded);
        }
    }
}