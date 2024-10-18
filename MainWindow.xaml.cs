using GameCenter.Project;
using GameCenter.Project.CurrencyConverter;
using GameCenter.Project.Porject2;
using GameCenter.Project.TicTacToe;
using GameCenter.Project.TodoList;
using GameCenter.Project.CarGame;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;
using GameCenter.Properties;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Controls;
using GameCenter.Project.Calculator;

namespace GameCenter
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer? clock;

        public MainWindow()
        {
            InitializeComponent();
            PromptUserDetails();
            LoadUserDetails();
            InitializeTimers();
        }

        private void InitializeTimers()
        {
            clock = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            clock.Tick += ShowCurrentDate;
            clock.Start();
        }

        private void ShowCurrentDate(object? sender, EventArgs e)
        {
            DateLabel.Content = DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss");
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Image image = (sender as Image)!;
            image.Opacity = 0.7;
            GameText.Content = (image.Name) switch
            {
                "Image1" => "Management System",
                "Image2" => "To-do List Project",
                "Image3" => "Currency Converter",
                "Image4" => "Tic Tac Toe",
                "Image5" => "Car Game",
                "Image6" => "Calculator",
                _ => "Please pick a game"
            };
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Image)!.Opacity = 1;
            GameText.Content = "Please pick a game";
        }

        
        private void EnterGame1_Click(object sender, MouseButtonEventArgs e)
        {
            OpenPresentation("Management System",
                   "" + "a User Management System is a versatile tool for effectively" +
                " managing user-related chores. You can add new users, amend their information," +
                " and remove them as needed. It simplifies user administration for increased " +
                "efficiency and organization.",
                  Image1.Source, new Project2());
            
        }

        private void EnterGame2_Click(object sender, MouseButtonEventArgs e)
        {
            OpenPresentation("To-do List",
                "" + "a To-Do List is a tool that helps you organize your daily responsibilities." +
                " You can add new tasks to it and mark them as finished.",
                 Image2.Source, new TodoList());
        }

        private void EnterGame3_Click(object sender, MouseButtonEventArgs e)
        {
            OpenPresentation("Currency Converter",
                "" + "Currency Conversion View is a mechanism that allows you to define how much money " +
                "is converted between different quantities around the world.",
                 Image3.Source, new CurrencyConverterView());
        }

        private void EnterGame4_Click(object sender, MouseButtonEventArgs e)
        {
            OpenPresentation("Tic Tac Toe",
                    "" + "TicTacToe, often abbreviated as TTT, " +
                " is a classic two-player game where opponents take turns marking spaces in a 3x3 grid. " +
                "The objective is to get three of their symbols (traditionally X or O) in a row horizontally," +
                " vertically, or diagonally before the opponent does. " +
                "The game is simple yet strategic, making it a popular choice for quick and casual gameplay.",
                   Image4.Source, new TicTacToe());
        }

        private void EnterGame5_Click(object sender, MouseButtonEventArgs e)
        {
            OpenPresentation("Car Game",
                   "" + "Car Game is a car game where you have to" +
                "dodge bombs that fall on you.",
                   Image5.Source, new CarGame());
        }

        private void EnterGame6_Click(object sender, MouseButtonEventArgs e)
        {
            OpenPresentation("Calculator",
               "" + "a Calculator is a tool for performing mathematical calculations," +
                "featuring functions like addition, subtraction, multiplication, " +
                "and division. Available in various forms," +
                "from handheld devices to digital applications," +
                "calculators are essential for quick and efficient mathematical operations in various contexts.",
                Image6.Source, new Calculator());
        }

        
        private void OpenPresentation(string title, string description, ImageSource? imageSource, Window? project)
        {
            if (imageSource == null)
            {
                MessageBox.Show("Image source is missing!");
                return;
            }

            ProjectPresentetationPage presentation = new ProjectPresentetationPage();

            try
            {
                
                this.Hide();

                
                presentation.OnStart(title, description, imageSource, project);

               
                presentation.ShowDialog();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                
                this.Show();
            }
        }

        private void PromptUserDetails()
        {
            
            if (string.IsNullOrEmpty(Settings.Default.UserName) || string.IsNullOrEmpty(Settings.Default.ProfilePicturePath))
            {
                UserInputWindow userInputWindow = new UserInputWindow();

               
                bool? result = null;
                try
                {
                    result = userInputWindow.ShowDialog();
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show($"Error opening user input window: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                    return;
                }

                if (result != true)
                {
                    
                    Application.Current.Shutdown();
                    return;
                }

               
                SaveUserDetails(userInputWindow.UserName, userInputWindow.PicturePath);
            }

            
            LoadUserDetails();
        }

        private void LoadUserDetails()
        {
            if (!string.IsNullOrEmpty(Settings.Default.UserName))
            {
                UserNameTextBlock.Text = Settings.Default.UserName;
            }

            if (File.Exists(Settings.Default.ProfilePicturePath))
            {
                UserProfilePicture.Source = new BitmapImage(new Uri(Settings.Default.ProfilePicturePath));
            }
        }

        private void SaveUserDetails(string userName, string profilePicturePath)
        {
            Settings.Default.UserName = userName;
            Settings.Default.ProfilePicturePath = profilePicturePath;
            Settings.Default.Save();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.UserName = string.Empty;
            Settings.Default.ProfilePicturePath = string.Empty;
            Settings.Default.Save();

            PromptUserDetails();
            UserNameTextBlock.Text = string.Empty;
            UserProfilePicture.Source = null;
        }
    }
}