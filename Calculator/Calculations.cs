using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Calculations : MainWindow
    {
        private string Text { set; get; }

        public Calculations(string text)
        {
            this.Text = text;
        }

        private double ValueOfComponentOperation(char operatorOfOperation, double a, double b)
        {
            switch (operatorOfOperation)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    return a / b;
                default:
                    return 0;
            }
        }

        private double ValueOfEntireOperation(ref List<double> lNumbers, ref List<char> lOperators)
        {
            double output = 0d, tempD = 0d;
            int count = lNumbers.Count;

            for (int i = 0; i < count; i++)
            {
                if (i < count - 1)
                {
                    if (count % 3 == 0 && i == 0)
                    {
                        
                        if (lOperators[i] == '+' || lOperators[i] == '-')
                        {
                            if (lOperators[i + 1] == '*' || lOperators[i + 1] == '/')
                            {
                                tempD = ValueOfComponentOperation(lOperators[i + 1], lNumbers[i + 1], lNumbers[i + 2]);
                                output = ValueOfComponentOperation(lOperators[i], lNumbers[i], tempD);

                            }
                        } 
                        else if (lOperators[i] == '*' || lOperators[i] == '/')
                        {
                            output += ValueOfComponentOperation(lOperators[i], lNumbers[i], lNumbers[i + 1]);
                            output = ValueOfComponentOperation(lOperators[i + 1], output, lNumbers[i + 2]);
                        }
                    }
                    else if (count % 3 == 2 && i == 0)
                    {
                        output = ValueOfComponentOperation(lOperators[i], lNumbers[i], lNumbers[i + 1]);
                    }
                }
            }
            return output;
        }

        private void FindOperators(ref List<char> list, string input)
        {
            foreach (char c in input)
            {
                if (!char.IsNumber(c) && c != ',')
                {
                    list.Add(c);
                }
            }
        }

        private void FindNumbers(ref List<double> list, string input)
        {
            string tempS = "";
            int tLength = input.Length;

            for (int i = 0; i < tLength; i++)
            {
                if (input[i] != '+' && input[i] != '-' && input[i] != '*' && input[i] != '/')
                    tempS += input[i];
                else
                {
                    list.Add(Convert.ToDouble(tempS));
                    tempS = string.Empty;
                }

                if (i == tLength - 1) 
                {
                    list.Add(Convert.ToDouble(tempS));
                    tempS = string.Empty;
                }
            }
        }

        private void RemoveCountedOperations(ref List<char> lChar)
        {
            int num = lChar.Count;
            for (int i = 0; i < num; i++)
            {
                if (lChar[i] == '*' || lChar[i] == '/')
                {
                    lChar.RemoveAt(i);
                    num--;
                }
            }
        }

        private void ConvertNumbersToOppositeInList(ref List<double> lDouble, ref List<char> lChar)
        {
            int i = 0;

            foreach (char c in lChar)
            {
                if (c == '-')
                    lDouble[i + 1] = -1 * lDouble[i + 1];
                i++;
            }
        }

        private void MakeListToSimplerCalculation(List<char> lChar, List<double> LDouble, ref List<double> toReturn)
        {
            int count = 0;
            double tempD = 0d;

            foreach (char c in lChar)
            {
                if (c == '*' || c == '/')
                {
                    tempD = ValueOfComponentOperation(c, LDouble[count], LDouble[count + 1]);
                    toReturn.Add(tempD);
                }
                else
                {
                    if (count < (lChar.Count - 1) && (lChar[count + 1] == '*' || lChar[count + 1] == '/') && count != 0)
                    {
                        count++;
                        continue;
                    }
                    else
                    {
                        int modifier = 0;
                        if (count != 0)
                            modifier = 1;

                        toReturn.Add(LDouble[count + modifier]);
                        count++;
                        continue;
                    }
                }
                count++;
            }
            RemoveCountedOperations(ref lChar);
        }

        public double CalculationOfOperation()
        {
            List<char> operatorsFromInput = new List<char>(); 
            List<double> numbersFromInput = new List<double>(); 
            List<double> countedDoubles = new List<double>();

            FindOperators(ref operatorsFromInput, Text);
            FindNumbers(ref numbersFromInput, Text);
            MakeListToSimplerCalculation(operatorsFromInput, numbersFromInput, ref countedDoubles);
            ConvertNumbersToOppositeInList(ref countedDoubles, ref operatorsFromInput);

            return countedDoubles.Sum();
        }
    }
}
