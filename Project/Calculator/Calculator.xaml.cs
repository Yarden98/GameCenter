using System;
using System.Windows;
using System.Windows.Controls;

namespace GameCenter.Project.Calculator
{
    public partial class Calculator : Window
    {
        private double _currentValue = 0;
        private double _lastValue = 0;
        private string _currentOperator = string.Empty;
        private bool _isNewEntry = true;

        public Calculator()
        {
            InitializeComponent();
        }

        
        private void Number_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (_isNewEntry)
                {
                    ResultText.Text = button.Content.ToString();
                    _isNewEntry = false;
                }
                else
                {
                    ResultText.Text += button.Content.ToString();
                }
            }
        }

        
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                _lastValue = double.Parse(ResultText.Text);
                _currentOperator = button.Content.ToString();
                _isNewEntry = true;
            }
        }

       
        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _currentValue = double.Parse(ResultText.Text);
                double result = 0;

                switch (_currentOperator)
                {
                    case "+":
                        result = _lastValue + _currentValue;
                        break;
                    case "-":
                        result = _lastValue - _currentValue;
                        break;
                    case "×":
                        result = _lastValue * _currentValue;
                        break;
                    case "÷":
                        if (_currentValue != 0)
                        {
                            result = _lastValue / _currentValue;
                        }
                        else
                        {
                            MessageBox.Show("Error: Division by zero!");
                            return;
                        }
                        break;
                }

                ResultText.Text = result.ToString();
                _isNewEntry = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

      
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            _currentValue = 0;
            _lastValue = 0;
            _currentOperator = string.Empty;
            ResultText.Text = "0";
            _isNewEntry = true;
        }

       
        private void Negate_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultText.Text, out double value))
            {
                ResultText.Text = (-value).ToString();
            }
        }

       
        private void Percentage_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultText.Text, out double value))
            {
                ResultText.Text = (value / 100).ToString();
            }
        }

        
        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (!ResultText.Text.Contains("."))
            {
                ResultText.Text += ".";
            }
        }
    }
}
