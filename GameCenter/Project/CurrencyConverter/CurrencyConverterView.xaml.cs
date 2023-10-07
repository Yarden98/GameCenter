using GameCenter.Project.CurrencyConverter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameCenter.Project.CurrencyConverter
{
    /// <summary>
    /// Interaction logic for CurrencyConverterView.xaml
    /// </summary>
    public partial class CurrencyConverterView : Window
    {
        private CurrencyService _currencyService;
        private Dictionary<string,double> _exchangeRates;
        public CurrencyConverterView()
        {
            InitializeComponent();
            _currencyService = new CurrencyService();
            LoadCurrencies();
        }

        private async void LoadCurrencies()
        {
            try
            {
                _exchangeRates = await _currencyService.GetExchangeRatesAsync();

                string[] currencies = _exchangeRates.Keys.ToArray();

                FromCurrencyComboBox.ItemsSource = currencies;
                ToCurrencyComboBox.ItemsSource = currencies;
            }
            catch(Exception ex) 
            {
                MessageBox.Show ($"Error loading currencies: {ex.Message}");
            }
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            string fromCurrency =FromCurrencyComboBox.SelectedItem.ToString();  
            string toCurrency =ToCurrencyComboBox.SelectedItem.ToString();

            double amount = double.Parse(AmountTextBox.Text);
            
            double baseToFromRate = _exchangeRates[fromCurrency];
            double baseToToRate = _exchangeRates[toCurrency];


            double convertedAmount = (baseToFromRate / baseToToRate) * amount;  
             txtResult.Text = $"Converted Amount: {amount} {fromCurrency} is {convertedAmount:0.00} {toCurrency}  ";
        }
    }
}
