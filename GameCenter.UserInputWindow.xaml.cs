using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameCenter
{
    public partial class UserInputWindow : Window
    {
        public string UserName { get; private set; }
        public string PicturePath { get; private set; }

        public UserInputWindow()
        {
            InitializeComponent();

            NameTextBox.Text = "Enter Your Full Name";
            NameTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A9A9A9"));
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            UserName = NameTextBox.Text;

            if (string.IsNullOrEmpty(UserName) || UserName == "Enter Your Full Name")
            {
                MessageBox.Show("Please provide a name.");
                return;
            };

            this.DialogResult = true;
            this.Close();
        }

        private void NameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Enter Your Full Name")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1F1F1F"));
            }
        }

        private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Enter Your Full Name";
                textBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A9A9A9"));
            }
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(UserName) || UserName == "Enter Your Full Name")
            {
                this.DialogResult = false;
            }

            base.OnClosing(e);
        }

    }
}
