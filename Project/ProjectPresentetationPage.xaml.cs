using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Documents;
using System.Windows.Threading;
using System.Windows.Controls;
using GameCenter.Properties; 
namespace GameCenter.Project
{
    public partial class ProjectPresentetationPage : Window
    {
        private Window? _currentProject;
        private DispatcherTimer? _timer; 

        public ProjectPresentetationPage()
        {
            InitializeComponent();
            InitializeTimer(); 

            
            LoadUserName();
        }

        private void LoadUserName()
        {
           
            if (!string.IsNullOrEmpty(Settings.Default.UserName))
            {
                
                UserNameTextBlock.Text = $"{Settings.Default.UserName}";
            }
            else
            {
                
                UserNameTextBlock.Text = "Welcome, Guest!";
            }
        }

        public void OnStart(string title, string projectDescription, ImageSource imageSource, Window project)
        {
            
            if (addUserTitle == null || ProjectText == null || ProjectImage == null)
            {
                MessageBox.Show("One of the UI components is not properly initialized.");
                return;
            }

            
            addUserTitle.Content = title;
            FlowDocument doc = new FlowDocument(new Paragraph(new Run(projectDescription)));
            ProjectText.Document = doc;
            ProjectImage.Source = imageSource;
            _currentProject = project;

            
            ProjectImage.MouseEnter += ProjectImage_MouseEnter;
            ProjectImage.MouseLeave += ProjectImage_MouseLeave;
            ProjectImage.MouseLeftButtonUp += ProjectImage_MouseLeftButtonUp;
        }

        
        private void InitializeTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += UpdateDateTime;
            _timer.Start(); 
        }

        
        private void UpdateDateTime(object? sender, EventArgs e)
        {
            DateLabel.Content = DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss");
        }

        private void ProjectImage_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Image image)
            {
                image.Opacity = 0.7; 
            }
        }

        private void ProjectImage_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Image image)
            {
                image.Opacity = 1.0; 
            }
        }

        private void ProjectImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_currentProject != null)
            {
                
                IsEnabled = false;

                
                _currentProject.Closed += (s, args) =>
                {
                    IsEnabled = true; 
                };

                
                _currentProject.Show();
            }
        }

        private void Btn_User_Click(object sender, RoutedEventArgs e)
        {
         
            Close(); 
        }
    }
}