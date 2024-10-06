using GameCenter.Project;
using GameCenter.Project.Calculator;
using GameCenter.Project.CarGame;
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
                "Image5" => "Car Game",
                "Image6" => "Calcilator",
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
            presentetion.OnStart("a User Management System", "" + "a User Management System is a versatile tool for effectively" +
                " managing user-related chores. You can add new users, amend their information," +
                " and remove them as needed. It simplifies user administration for increased " +
                "efficiency and organization.", Image1.Source, project2);
            presentetion.ShowDialog();

        }
        private void Image2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TodoList todoList = new();
            ProjectPresentetationPage presentetion = new();
            presentetion.OnStart("To-Do List", "" + "a To-Do List is a tool that helps you organize your daily responsibilities." +
                " You can add new tasks to it and mark them as finished.", Image2.Source, todoList);
            presentetion.ShowDialog();
            
       

        }
        private void Image3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CurrencyConverterView currencyConverter = new();
            ProjectPresentetationPage presentetion = new();
            presentetion.OnStart("Currency Converter View", "" + "Currency Conversion View is a mechanism that allows you to define how much money " + 
                "is converted between different quantities around the world.", Image3.Source,currencyConverter);
            presentetion.ShowDialog();
        }
        private void Image4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TicTacToe ticTacToe = new();
            ProjectPresentetationPage presentetion = new();
            presentetion.OnStart("TicTacToe", "" + "TicTacToe, often abbreviated as TTT, "+ 
                " is a classic two-player game where opponents take turns marking spaces in a 3x3 grid. "+ 
                "The objective is to get three of their symbols (traditionally X or O) in a row horizontally,"+
                " vertically, or diagonally before the opponent does. "+
                "The game is simple yet strategic, making it a popular choice for quick and casual gameplay.", Image4.Source, ticTacToe);
            presentetion.ShowDialog();
        }
        private void Image5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CarGame carGame = new CarGame();
            ProjectPresentetationPage presentetion = new();
            presentetion.OnStart("Car Game", "" + "Car Game is a car game where you have to"+
                "dodge bombs that fall on you.", Image5.Source, carGame);
            presentetion.ShowDialog();

        }

        private void Image6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Calculator calculator = new Calculator();
            ProjectPresentetationPage presentetion = new();
            presentetion.OnStart("Calculator", "" + "a Calculator is a tool for performing mathematical calculations,"+ 
                "featuring functions like addition, subtraction, multiplication, "+
                "and division. Available in various forms,"+ 
                "from handheld devices to digital applications,"+ 
                "calculators are essential for quick and efficient mathematical operations in various contexts.", Image6.Source,calculator);
            presentetion.ShowDialog();

        }

       /* private void OnImageClick(object sender, MouseButtonEventArgs e)
          {
        }*/

    }
}
