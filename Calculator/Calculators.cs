using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    /// <summary>
    /// Допустим мы знаем что в данном приложении не используется многопоточность В программе не испольшуется многопоточность, поэтому классы
    /// </summary>
    /// 
    interface CalculatorInterface
    {
        string Calculate(string str);
    }

    class PolishNotationCalculator : CalculatorInterface
    {
        PNCalculator _calc = new PNCalculator();

        PolishNotationCalculator() { }

        public string Calculate(string str)
        {
            return _calc.Calculate(str);
        }
    }

    class ReversePolishNotationCalculator : CalculatorInterface
    {
        //RPNCalculator _calc;

        ReversePolishNotationCalculator() { }

        public string Calculate(string str)
        {
            throw new NotImplementedException();
        }
    }
    
    class InfixNotationCalculator : CalculatorInterface
    {
        //INCalculator _calc;

        InfixNotationCalculator() { }

        public string Calculate(string str)
        {
            throw new NotImplementedException();
        }
    }
}