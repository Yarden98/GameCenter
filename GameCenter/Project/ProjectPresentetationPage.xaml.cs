using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameCenter.Project
{
    public partial class ProjectPresentetationPage : Window
    {
        private Window _project;
        private Window _currentProject;

        public ProjectPresentetationPage()
        {
            InitializeComponent();
        }

        public void OnStart(string title, string projectDescription, ImageSource imageSource, Window project)
        {
            addUserTitle.Content = title;
            ProjectText.Text = projectDescription;
            ProjectImage.Source = imageSource;
            _currentProject = project;

            Button button = Btn_click;


        }

        /* private void ProjectImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
         {
             Close();
             _currentProject!.ShowDialog();
             _currentProject!.Close();
         }*/
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
