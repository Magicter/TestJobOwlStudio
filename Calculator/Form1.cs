/// <summary>
/// Тестовое задание
/// Написать калькулятор выражений, записанных в польской нотации, т.е.результатом выражения "+ 3 4" должно быть 7.
/// Выражение может быть сколь угодно сложным, также должна быть проверка корректности выражения.
/// 
/// Калькулятор оформить в виде библиотеки.
/// Для демонстрации работы создать
/// либо консольное приложение — выражение читается из стандартного ввода
/// либо WinForms приложение — выражение можно вводить в стандартный TextBox
/// </summary>

using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        PNCalculator calc;

        public Form1()
        {
            InitializeComponent();
            calc = new PNCalculator();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lb_result.Text = calc.Calculate(tb_expression.Text);
        }

        private void tb_expression_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                return;
            }

            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if (e.KeyChar == ',')   // запятая уже есть в поле редактирования
            {
                if (tb_expression.Text.IndexOf(',') != -1)
                {
                    e.Handled = true;
                }
                return;
            }

            if ((e.KeyChar == '+') && (e.KeyChar == '-') &&  (e.KeyChar == '*') &&  (e.KeyChar == '/'))
            {
                string str = tb_expression.Text;
                if (str[str.Length - 1] != ' ') tb_expression.Text += " ";
                return;
            }

            if (char.IsControl(e.KeyChar))      // <Enter>, <Backspace>, <Esc>
            {
                if (e.KeyChar == (char)Keys.Enter)
                    bt_calculate.Focus();
                return;
            }

            // остальные символы запрещены
            e.Handled = true;
            */
        }
    }
}



