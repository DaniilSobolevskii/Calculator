using System.Globalization;

namespace Calculator.Models;

public class DanglCalculator : ICalculator
{
    public string Calculate(string expression)
    {
        var calvulation = Dangl.Calculator.Calculator.Calculate(expression);
        if (calvulation.IsValid)
        {
            return calvulation.Result.ToString(CultureInfo.InvariantCulture);
        }
        else
        {
            return calvulation.ErrorMessage;
        }
    }
}