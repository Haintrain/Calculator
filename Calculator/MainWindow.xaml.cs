using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String storedValue = "";
        
        private String previousValue = "";
        private String currentValue = "";

        private bool newValue = true;
        private bool isError = false;

        private String previousOperator = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnClickNumber(object sender, RoutedEventArgs e)
        {
            if (isError) { return; }

            Button button = (Button)sender;

            if (MainTextBlock.Text.Length > 11)
            {
                return;
            }

            if (newValue) {
                SecondTextBlock.Text = previousValue + previousOperator;
                currentValue = "";
                MainTextBlock.Text = "";
                newValue = false;
            }



            currentValue += button.Tag.ToString();

            if (MainTextBlock.Text.ToString() == "0")
            {
                MainTextBlock.Text = button.Tag.ToString();
            }
            else
            {
                MainTextBlock.Text += button.Tag.ToString();
            }
        }

        private void OnClickClear(object sender, RoutedEventArgs e)
        {
            if (isError) { return; }

            MainTextBlock.Text = previousValue;

            if (newValue)
            {
                newValue = false;
                previousValue = storedValue;
                currentValue = previousValue;
            }
        }

        private void OnClickAllClear(object sender, RoutedEventArgs e)
        {
            if (isError) {
                isError = false;
            }

            MainTextBlock.Text = "0";
            currentValue = "";
            previousValue = "";
            previousOperator = "";

            newValue = true;
        }

        private void OnClickMathFunction(object sender, RoutedEventArgs e)
        {
            if (isError) { return; }

            Button button = (Button)sender;
            String currentOperator = button.Tag.ToString();
            newValue = true;

            if (previousValue == "" || previousOperator == "")
            { 
                storedValue = previousValue;
                previousValue = currentValue;

                currentValue = "";

                SecondTextBlock.Text = previousValue + currentOperator;
                previousOperator = currentOperator;
            }
            else if(previousOperator == "="){
                previousOperator = currentOperator;
                SecondTextBlock.Text += currentOperator;
            }
            else
            {
                float result = EvaluateExpression(MainTextBlock.Text);

                if (result.ToString().Length > 12)
                {
                    MainTextBlock.Text = "ERR";
                    isError = true;
                }
                else if(result == -1)
                {
                    MainTextBlock.Text = "ERR";
                    isError = true;
                }
                else
                {
                    SecondTextBlock.Text = result.ToString();
                    MainTextBlock.Text = result.ToString();
                    previousOperator = currentOperator;

                    storedValue = previousValue;
                    previousValue = result.ToString();

                    currentValue = "";

                    if (currentOperator != "=")
                    {
                        SecondTextBlock.Text += currentOperator;
                    }

                    previousOperator = currentOperator;
                }
            }
        }

        private float EvaluateExpression(String expression)
        {
            if(previousOperator == "+")
            {
                return float.Parse(previousValue) + float.Parse(currentValue);
            }
            else if(previousOperator == "-")
            {
                return float.Parse(previousValue) - float.Parse(currentValue);
            }  
            else if(previousOperator == "*")
            {
                return float.Parse(previousValue) * float.Parse(currentValue);
            }
            else if(previousOperator == "÷")
            {
                return float.Parse(previousValue) / float.Parse(currentValue);
            }


            return -1;
        }

    }
}
    