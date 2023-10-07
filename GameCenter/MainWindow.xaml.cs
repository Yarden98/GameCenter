using GameCenter.Project;
using GameCenter.Project.CurrencyConverter;
using GameCenter.Project.Porject2;
using GameCenter.Project.TicTacToe;
using GameCenter.Project.TodoList;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace GameCenter
{


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer? clock = new()
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            clock.Tick += ShowCurrentDate!;
            clock.Start();
        }

        private void ShowCurrentDate(object sender, EventArgs e)
        {
            DateLabel.Content = DateTime.UtcNow.ToString("ddd, dd, MMM yyy HH:mm:ss");
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Image image = (sender as Image)!;
            image.Opacity = 0.7;
            GameText.Content = (image.Name) switch

            {
                "Image1" => "a User Management System",
                "Image2" => "To do list  project",
                "Image3" => "Currency convertor",
                "Image4" => "Tic Tac Toe",
                "Image5" => "Game No. 5 is a game about lorm ipsum & happy birthday",
                "Image6" => "Game No. 6 is a game about lorm ipsum & happy birthday",
                _ => "please pick a game"
            };
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Image)!.Opacity = 1;
            GameText.Content = "please pick a game";


        }

        private void Image1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Project2 project2 = new();
            ProjectPresentetationPage presentetion = new();
            presentetion.OnStart("To-Do List", "" + "To-Do List is a versatile tool for effectively" +
                " managing user-related chores. You can add new users, amend their information," +
                " and remove them as needed. It simplifies user administration for increased " +
                "efficiency and organization.", Image1.Source, project2);
            presentetion.ShowDialog();

        }
        private void Image2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TodoList todoList = new TodoList();
            Hide();
            todoList.ShowDialog();
            Show();

        }
        private void Image3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CurrencyConverterView currencyConverter = new();
            Hide();
            currencyConverter.ShowDialog();
            Show();
        }
        private void Image4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TicTacToe ticTacToe = new();
            Hide();
            ticTacToe.ShowDialog();
            Show();
        }
        private void Image5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {


        }
        /*  private void OnImageClick(object sender, MouseButtonEventArgs e)
          {


          }*/

    }
}
