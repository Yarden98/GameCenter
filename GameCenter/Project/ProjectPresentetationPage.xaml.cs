using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace GameCenter.Project
{
    public partial class ProjectPresentetationPage : Window
    {
        private Window _project;
        private Window _currentProject;

        public ProjectPresentetationPage()
        {
            InitializeComponent();


            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(1),
            };

            timer.Tick += UpdateDate;
            timer.Start();
        }

        public void OnStart(string title, string projectDescription, ImageSource imageSource, Window project)
        {
          

            addUserTitle.Content = title;
            FlowDocument doc = new FlowDocument(new Paragraph(new Run(projectDescription)));
            ProjectText.Document = doc;
            ProjectImage.Source = imageSource;
            _currentProject = project;

            


        }

        private void UpdateDate(object? sender, EventArgs e)
        {
            date.Content = DateTime.Now.ToString("dddd, dd MMMM, HH:mm:ss");
        }

        private void GoBack(object sender, MouseButtonEventArgs e) => Close();

      
     
        private void Mouse_Leave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;

            if (sender is Image img)
            {
                SetOpacity(img, 1);
            }

            if (sender is Label label)
            {
                SetOpacity(label, 1);
            }
        }

        private static void SetOpacity(FrameworkElement control, double opacity)
        {
            control.Opacity = opacity;
        }

     
        private void Btn_Next_Click(object sender, RoutedEventArgs e)
        {
            Close();
            _currentProject!.ShowDialog();
            _currentProject!.Close();
        }

        private void Btn_User_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
