using System;

// - * / 15 - 7.5 + 14 1 3 + 2 + 1 1 = 15 / (7 - (1 + 1)) * 3 - (2 + (1 + 1)) = 5

public class PNCalculator
{
    #region Свойства класса

    string[] _expression;                                                   //Вычисляемое выражение
    int _index;                                                             //Номер последнего обработанного элемента выражения
    int _amountOperands;                                                    //Количествао операдов (чисел) — для проверки корректности выражения

    #endregion
    #region Методы класса

    public PNCalculator() { }

    /// <summary>
    /// Вычисление выражения записанного в польской нотации
    /// </summary>
    /// <param name="expression">Принимает строку из чисел "1-9" с десятичными разделителем ",", и операторов "+, -, /, *", разделённых между друг другом пробелами</param>
    /// <returns>Возвращает строковый результат вычисления выражения или описание ошибки</returns>
    /// <exception cref="ArgumentNullException">Исключение возникает при передачи пустой ссылки строковый аргумент</exception>
    public string Calculate(string expression)
    {
        if (expression == null)
        {
            throw new ArgumentNullException(nameof(expression));
        }

        _expression = expression.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        if (_expression.Length == 0)
        {
            return "";
        }
        _index = 0;
        _amountOperands = 0;
        string result =  "";

        try
        {
            result = PNparser(_index).ToString();

            if (_expression.Length != (_amountOperands - 1) * 2 + 1)
            {
                result = "Ошибка. Выражение проанализировано не полностью. Получено значение: " + result + " для части выражения:";
                for (int i = 0; i <= _index; i++)
                {
                    result += " " + _expression[i];
                }
            }
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }
        return result;
    }

    float PNparser(int index)                       //Реурсивный парсер выражения _expression начиная с элемента index
    {
        float res;
        if (_index == _expression.Length)
        {
            throw new IncorrectRecordOfExpressionException("Ошибка. Анализатор достиг конца выражения, но не нашел достаточное колличество операндов для его вычисления");
        }
        else if (float.TryParse(_expression[_index], out res))   //!обработка минуса
        {
            if (res < 0)
            {
                throw new IncorrectRecordOfExpressionException("Ошибка. Возможна неправильная запись отрицательного числа: «" + res.ToString() + "». Должно записываться: «- 0 " + (-res).ToString() + "».");
            }
            _amountOperands++;
            return res;
        }
        else
        {
            return calc(_expression[index], PNparser(++_index), PNparser(++_index));
        }
    }

    float calc(string operation, float a, float b)  //Выполнение операции operation для операндов a и b
    {
        switch (operation)
        {
            case "+":
                return a += b;
            case "-":
                return a -= b;
            case "/":
                return a /= b;
            case "*":
                return a *= b;
            default:
                throw new IncorrectRecordOfExpressionException("Неизвестное значение: «" + operation + "». Используйте «+», «-», «/», «*» и «,» для вещественных чисел.");
        }
    }

    #endregion
}

#region Пользовательское исключение

class IncorrectRecordOfExpressionException : System.Exception
{
    public IncorrectRecordOfExpressionException() : base() { }

    public IncorrectRecordOfExpressionException(string message) : base(message) { }

    public IncorrectRecordOfExpressionException(string message, Exception inner) : base(message, inner) { }
}

#endregion
