using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Варианты тестирования:
/// 1) Одиночные заначения:
/// 1.1) Числовые значения:
/// 1.1.1) Целое число:                     "9"                                         = 9   
/// 1.1.2) Вещественное число:              "9,3"                                       = 9,3
/// 1.1.3) Отрицательное число:             "-3"                                        = "Ошибка. Возможна неправильная запись отрицательного числа: «-3». Должно записываться: «- 0 3»."
/// 1.1.4) Число с двумя точками:           "9,3,5"                                     = "Ошибка. Анализатор достиг конца выражения, но не нашел достаточное колличество операндов для его вычисления"
/// 1.1.5) Число с неподдерживаемым знаком: "9f"                                        = "Ошибка. Анализатор достиг конца выражения, но не нашел достаточное колличество операндов для его вычисления"
/// 1.1.6) Слишком большое число:           "1234567890123456789012345678901234567890"  = "Ошибка. Анализатор достиг конца выражения, но не нашел достаточное колличество операндов для его вычисления"
/// 
/// 1.2) Математические знаки и прочие символы:
/// 1.2.1) Пустая строка:                   ""      = ""
/// 1.2.2) Пробельные символы:              " "     = ""
/// 1.2.3) Математический знак:             "+"     = "Ошибка. Анализатор достиг конца выражения, но не нашел достаточное колличество операндов для его вычисления"
/// 1.2.4) Пустая ссылка:                   null    = ArgumentNullException (выдаёт необрабатываемие исключение, т.к. это ошибка программиста, а не пользователя записавшего выражение)                               
/// 1.2.5) Неподдерживаемый символ:         "х"     — не требуется, аналог 1.1.5
/// 
/// 2) Выражения:
/// 2.1) Сложение:                          "+ 2 3"         = "5"
/// 2.2) Вычитание:                         "- 3 5"         = "-2"
/// 2.3) Умножение:                         "* 4 2"         = "8"
/// 2.4) Деление:                           "/ 15 5"        = "3"
/// 2.5) Обратная нотация:                  "2 1 +"         = "Ошибка. Выражение проанализировано не полностью. Получено значение: 2 для части выражения: 2"
/// 2.6) Инфиксная нотация:                 "3 + 1"         = "Ошибка. Выражение проанализировано не полностью. Получено значение: 3 для части выражения: 3"
/// 2.7) Лишние пробелы:                    " /  15  5 "    = "3"
/// 2.8) Отсутствие пробелов:               "/155"          = "Ошибка. Анализатор достиг конца выражения, но не нашел достаточное колличество операндов для его вычисления"
/// 1.9) Неизвестный оператр:               "+- 2 4"        = "Неизвестное значение: «+-». Используйте «+», «-», «/», «*» и «,» для вещественных чисел."
/// 
/// 3) Длинные выражение
/// 3.1) Произвольное выражение:            "- * / 15 - 7,5 + 14 1 3 + 2,6 + 1 1"   = "-10,6"
/// 3.2) Превышение колличества операндов:  "+ 2 3 5"                               = "Ошибка. Выражение проанализировано не полностью. Получено значение: 5 для части выражения: + 2 3"
/// 3.3) Превышение колличества операторов: "+ + 2 3"                               = "Ошибка. Анализатор достиг конца выражения, но не нашел достаточное колличество операндов для его вычисления"
/// </summary>
namespace Tests
{
    [TestClass()]
    public class PNCalculatorTests
    {
        #region 1.1) Числовые значения

        [TestMethod()]
        public void CalculateSingleIntTest()
        {
            PNCalculator calcTest = new PNCalculator();
            var str = "9";

            var result = calcTest.Calculate(str);

            Assert.AreEqual(result, str);
        }

        [TestMethod()]
        public void CalculateSingleFloatTest()
        {
            PNCalculator calcTest = new PNCalculator();
            var str = "9,3";

            var result = calcTest.Calculate(str);

            Assert.AreEqual(result, str);
        }

        [TestMethod()]
        public void CalculateNegativeNumberTest()
        {
            PNCalculator calcTest = new PNCalculator();
            var str = "-3";

            var result = calcTest.Calculate(str);

            Assert.AreEqual(result, "Ошибка. Возможна неправильная запись отрицательного числа: «-3». Должно записываться: «- 0 3».");
        }

        [TestMethod()]
        public void CalculateIncorrectFloatTest()
        {
            PNCalculator calcTest = new PNCalculator();
            var str = "9.3.5";

            var result = calcTest.Calculate(str);

            Assert.AreEqual(result, "Ошибка. Анализатор достиг конца выражения, но не нашел достаточное колличество операндов для его вычисления");
        }

        [TestMethod()]
        public void CalculateIncorrectNumberTest()
        {
            PNCalculator calcTest = new PNCalculator();
            var str = "9f";

            var result = calcTest.Calculate(str);

            Assert.AreEqual(result, "Ошибка. Анализатор достиг конца выражения, но не нашел достаточное колличество операндов для его вычисления");
        }

        [TestMethod()]
        public void CalculateTooLongNumberTest()
        {
            PNCalculator calcTest = new PNCalculator();
            var str = "1234567890123456789012345678901234567890";

            var result = calcTest.Calculate(str);

            Assert.AreEqual(result, "Ошибка. Анализатор достиг конца выражения, но не нашел достаточное колличество операндов для его вычисления");
        }

        #endregion
        #region 1.2) Математические знаки и прочие символы:

        [TestMethod()]
        public void CalculateEmptyTest()
        {
            PNCalculator calcTest = new PNCalculator();
            var str = "";

            var result = calcTest.Calculate(str);

            Assert.AreEqual(result, "");
        }

        [TestMethod()]
        public void CalculateSpaceTest()
        {
            PNCalculator calcTest = new PNCalculator();
            var str = " ";

            var result = calcTest.Calculate(str);

            Assert.AreEqual(result, "");
        }

        [TestMethod()]
        public void CalculateSingleMathOperatorTest()
        {
            PNCalculator calcTest = new PNCalculator();
            var str = "+";

            var result = calcTest.Calculate(str);

            Assert.AreEqual(result, "Ошибка. Анализатор достиг конца выражения, но не нашел достаточное колличество операндов для его вычисления");
        }

        [TestMethod()]
        public void CalculateNullStringTest()
        {
            PNCalculator calcTest = new PNCalculator();
            string str = null;

            try
            {
                string result = calcTest.Calculate(str);
            }
            catch (ArgumentNullException e)
            {
                return;
            }

            Assert.Fail();
        }

        #endregion
        #region 2) Выражения:

        [TestMethod()]
        public void CalculateAdditionTest()
        {
            PNCalculator calcTest = new PNCalculator();
            var str = "+ 2 3";

            var result = calcTest.Calculate(str);

            Assert.AreEqual(result, "5");
        }

        [TestMethod()]
        public void CalculateSubtractionTest()
        {
            PNCalculator calcTest = new PNCalculator();
            var str = "- 3 5";

            var result = calcTest.Calculate(str);

            Assert.AreEqual(result, "-2");
        }

        [TestMethod()]
        public void CalculateMultiplicationTest()
        {
            PNCalculator calcTest = new PNCalculator();
            var str = "* 4 2";

            var result = calcTest.Calculate(str);

            Assert.AreEqual(result, "8");
        }

        [TestMethod()]
        public void CalculateDividingTest()
        {
            PNCalculator calcTest = new PNCalculator();
            string str = "/ 15 5";

            string result = calcTest.Calculate(str);

            Assert.AreEqual(result, "3");
        }

        [TestMethod()]
        public void CalculateInverseNotationTest()
        {
            PNCalculator calcTest = new PNCalculator();
            var str = "2 1 +";

            var result = calcTest.Calculate(str);

            Assert.AreEqual(result, "Ошибка. Выражение проанализировано не полностью. Получено значение: 2 для части выражения: 2");
        }

        [TestMethod()]
        public void CalculateInfixNotationTest()
        {
            PNCalculator calcTest = new PNCalculator();
            var str = "3 + 1";

            var result = calcTest.Calculate(str);

            Assert.AreEqual(result, "Ошибка. Выражение проанализировано не полностью. Получено значение: 3 для части выражения: 3");
        }

        [TestMethod()]
        public void CalculateExcessSpacesTest()
        {
            PNCalculator calcTest = new PNCalculator();
            string str = " /  15  5 ";

            string result = calcTest.Calculate(str);

            Assert.AreEqual(result, "3");
        }

        [TestMethod()]
        public void CalculateNotSpacesTest()
        {
            PNCalculator calcTest = new PNCalculator();
            string str = " /155";

            string result = calcTest.Calculate(str);

            Assert.AreEqual(result, "Ошибка. Анализатор достиг конца выражения, но не нашел достаточное колличество операндов для его вычисления");
        }

        [TestMethod()]
        public void CalculateUnknownOperatorTest()
        {
            PNCalculator calcTest = new PNCalculator();
            string str = "+- 2 4";

            string result = calcTest.Calculate(str);

            Assert.AreEqual(result, "Неизвестное значение: «+-». Используйте «+», «-», «/», «*» и «,» для вещественных чисел.");
        }

        #endregion
        #region 3) Длинные выражение:
        [TestMethod()]
        public void CalculateLongTrueExpressionTest()
        {
            PNCalculator calcTest = new PNCalculator();
            var str = "- * / 15 - 7,5 + 14 1 3 + 2,6 + 1 1";

            var result = calcTest.Calculate(str);

            Assert.AreEqual(result, "-10,6");
        }

        [TestMethod()]
        public void CalculateExcessOperandsTest()
        {
            PNCalculator calcTest = new PNCalculator();
            string str = "+ 2 3 5";

            string result = calcTest.Calculate(str);

            Assert.AreEqual(result, "Ошибка. Выражение проанализировано не полностью. Получено значение: 5 для части выражения: + 2 3");
        }

        [TestMethod()]
        public void CalculateExcessOperatorsTest()
        {
            PNCalculator calcTest = new PNCalculator();
            string str = "+ + 2 3";

            string result = calcTest.Calculate(str);

            Assert.AreEqual(result, "Ошибка. Анализатор достиг конца выражения, но не нашел достаточное колличество операндов для его вычисления");
        }


        #endregion
    }
}