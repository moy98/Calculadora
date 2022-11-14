using System;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{

    public partial class MainWindow : Window
    {
        private int numberOfComponents = 0;
        private bool numberEntered = false;

        private int CountNumbers()
        {
            string input = InputTextBox.Text;
            int tLength = input.Length;
            int count = 0;

            for (int i = 0; i < tLength; i++)
            {
                if (input[i] != '+' && input[i] != '-' && input[i] != '*' && input[i] != '/')
                    continue;
                else
                {
                    count++;
                }

                if (i == tLength - 1) 
                {
                    count++;
                }
            }

            return ++count;
        }

        public void SetNumOfComp()
        {
            this.numberOfComponents = CountNumbers();
        }

        public MainWindow()
        {
            InitializeComponent();
            NumericCheckbox.IsChecked = true;
        }

        
        private void CheckIfCorrectInput()
        {
            SetNumOfComp();

            if ((InputTextBox.Text.LastIndexOf('+') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.LastIndexOf('-') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.LastIndexOf('*') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.LastIndexOf('/') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.LastIndexOf(',') == InputTextBox.Text.Length - 1)
               || InputTextBox.Text.IndexOf('+') == 0
               || InputTextBox.Text.IndexOf('-') == 0
               || InputTextBox.Text.IndexOf('*') == 0
               || InputTextBox.Text.IndexOf('/') == 0
               || InputTextBox.Text.IndexOf(',') == 0)
            {
                MessageBox.Show("Entrada No Valida!");
            }
            else if (InputTextBox.Text.Length == 0)
            {
                MessageBox.Show("Sin Entrada!");
            }
            else if ((!CheckIfCommasAreGood()) || (!CheckIfOperatorsAreGood())) 
                MessageBox.Show("Entrada No Valida");
            else
            {
                Calculations calc = new Calculations(InputTextBox.Text);
                this.OutputTextBox.Text = Convert.ToString(calc.CalculationOfOperation());
            }
        }

        private bool CheckIfCommasAreGood()
        {
            var text = InputTextBox.Text;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ',') 
                {
                    if (i == 0) 
                        return false;
                    if (text[i + 1] == ',') 
                    {
                        return false;
                    }
                }
            }
            return true;
        }

       
        private bool CheckIfOperatorsAreGood()
        {
            var text = InputTextBox.Text;
            var howManyOperatorsDetected = 0;

            foreach (char c in text)
            {
                if (!Char.IsNumber(c) && c != ',') 
                    howManyOperatorsDetected++;
            }

            if (howManyOperatorsDetected >= numberOfComponents)
                return false;
            else return true;
        }

        
        private void SetFocusToMainInputBox()
        {
            InputTextBox.Focus();
            InputTextBox.CaretIndex = InputTextBox.Text.Length;
        }

        
        private void ClearTextBoxes()
        {
            this.InputTextBox.Text = string.Empty;
            this.InputTextBox.Text = OutputTextBox.Text;
            OutputTextBox.Text = string.Empty;
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9) 
            {
                numberEntered = true;
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) 
            {
                numberEntered = true;
            }
            else if (e.Key == Key.OemComma || e.Key == Key.Decimal || e.Key == Key.Subtract || e.Key == Key.Add || e.Key == Key.Divide || e.Key == Key.Multiply || e.Key == Key.Enter)
                numberEntered = true;
            else 
            {
                e.Handled = true; 
                MessageBox.Show("Catacter no Valido!");
            }

            if (e.Key == Key.Return)
            {
                CheckIfCorrectInput();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputTextBox.Text.Length == 0 && OutputTextBox.Text.Length == 0)
            {
                MessageBox.Show("Entrada Vacia!");
            }
            else if (InputTextBox.Text.Length != 0 && OutputTextBox.Text.Length != 0) 
            {
                ClearTextBoxes();
                this.InputTextBox.Text += "+";
            }
            else if ((InputTextBox.Text.IndexOf('+') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('-') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('*') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('/') == InputTextBox.Text.Length - 1))
            {
                MessageBox.Show("Simbolos Dobles!!");
            }
            else
            {
                this.InputTextBox.Text += "+";
            }
            SetFocusToMainInputBox(); 
        }

        private void SumButton_Click(object sender, RoutedEventArgs e)
        {
            CheckIfCorrectInput(); 

            SetFocusToMainInputBox(); 
        }

        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputTextBox.Text.Length == 0 && OutputTextBox.Text.Length == 0)
            {
                MessageBox.Show("Entrada Vacia!");
            }
            else if (InputTextBox.Text.Length != 0 && OutputTextBox.Text.Length != 0) 
            {
                ClearTextBoxes();
                this.InputTextBox.Text += "-";
            }
            else if ((InputTextBox.Text.IndexOf('+') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('-') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('*') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('/') == InputTextBox.Text.Length - 1))
                {
                MessageBox.Show("Simbolos Dobles!");
            }
            else
            {
                this.InputTextBox.Text += "-";
            }
            SetFocusToMainInputBox(); 
        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputTextBox.Text.Length == 0 && OutputTextBox.Text.Length == 0)
            {
                MessageBox.Show("Entrada Vacia!");
            }
            else if (InputTextBox.Text.Length != 0 && OutputTextBox.Text.Length != 0) 
            {
                ClearTextBoxes();
                this.InputTextBox.Text += "*";
            }
            else if ((InputTextBox.Text.IndexOf('+') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('-') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('*') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('/') == InputTextBox.Text.Length - 1))
            {
                MessageBox.Show("Simbolos Dobles!");
            }
            else
            {
                this.InputTextBox.Text += "*";
            }
            SetFocusToMainInputBox(); 
        }

        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            if(InputTextBox.Text.Length == 0 && OutputTextBox.Text.Length == 0)
            {
                MessageBox.Show("Entrada Vacia!");
            }
            else if (InputTextBox.Text.Length != 0 && OutputTextBox.Text.Length != 0) 
            {
                ClearTextBoxes();
                this.InputTextBox.Text += "/";
            }
            else if ((InputTextBox.Text.IndexOf('+') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('-') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('*') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('/') == InputTextBox.Text.Length - 1))
            {
                MessageBox.Show("Simbolos Dobles!");
            }
            else
            {
                this.InputTextBox.Text += "/";
            }
            SetFocusToMainInputBox(); 
        }

        private void NumericCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            NumberKeySection.Visibility = Visibility.Visible; 
            SetFocusToMainInputBox(); 
        }

        private void NumericCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            NumberKeySection.Visibility = Visibility.Collapsed; 
            SetFocusToMainInputBox(); 
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            
            InputTextBox.Text = string.Empty;
            OutputTextBox.Text = string.Empty;
            
            InputTextBox.Focus();
            InputTextBox.CaretIndex = InputTextBox.Text.Length;
        }

        private void NineButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "9";
            SetFocusToMainInputBox(); 
        }

        private void EightButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "8";
            SetFocusToMainInputBox(); 
        }

        private void SevenButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "7";
            SetFocusToMainInputBox(); 
        }

        private void SixButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "6";
            SetFocusToMainInputBox(); 
        }

        private void FiveButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "5";
            SetFocusToMainInputBox(); 
        }

        private void FourButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "4";
            SetFocusToMainInputBox(); 
        }

        private void ThreeButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "3";
            SetFocusToMainInputBox(); 
        }

        private void TwoButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "2";
            SetFocusToMainInputBox();
        }

        private void OneButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "1";
            SetFocusToMainInputBox(); 
        }

        private void NullButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "0";
            SetFocusToMainInputBox(); 
        }

        private void CommaButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += ",";
            SetFocusToMainInputBox(); 
        }
    }
}
