namespace Cards.Domain.Services;

public static class Helpers
{
    public static int GetFibbonacciNumber(int index)
    {
        if (index == 0) return 0;
        if (index == 1) return 1;
        return GetFibbonacciNumber(index - 1) + GetFibbonacciNumber(index - 2);
    }
}