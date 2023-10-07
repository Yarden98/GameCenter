using GameCenter.Project.Porject2.Models;
using GameCenter.Project.Porject2.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace GameCenter.Project.Porject2
{

    public partial class Project2 : Window
    {
        User _user;
        List<User> users;

        public Project2()
        {

            InitializeComponent();
            users = DataHandler.GetUsersList();

            if (users == null || users.Count == 0)
            {
                List<User> initialList = new()
                {
                    new User("Bob", "bob@email.com", "Qaz123!123Qaz"),
                    new User("Sara", "Sara@email.com", "Qaz123!123Qaz"),
                    new User("Neomi", "Neomi@email.com", "Qaz123!123Qaz"),
                    new User("Abed", "Abed@email.com", "Qaz123!123Qaz")
                };
                users = DataHandler.UpdateList(initialList);
            }
            UpdateDataGrid();
        }

        private void UpdateJsonData()
        {
            List<User> tempList = users;
            users = DataHandler.UpdateList(tempList);
        }

        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            if (_user != null && Validate.UserName(Input_UserName))
            {
                _user.Name = Input_UserName.Text;
                _user.Email = Input_Email.Text;
                _user.Password = Input_Password.Text;

                var existingUser = users.FirstOrDefault(u => u.Id == _user.Id);
                if (existingUser != null)
                {
                    existingUser.Name = _user.Name;
                    existingUser.Email = _user.Email;
                    existingUser.Password = _user.Password;
                }

                UpdateJsonData();
                UpdateDataGrid();
            }


        }

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            if (Validate.UserName(Input_UserName))
            {
                users.Add(new User(Input_UserName.Text, Input_Email.Text, Input_Password.Text));
                UpdateJsonData();
                UpdateDataGrid();
            }
        }

        private void UpdateDataGrid()
        {
            DataGrid1.ItemsSource = users.ToList();
        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var idCell = DataGrid1.SelectedCells[0];
            var nameCell = DataGrid1.SelectedCells[1];
            var emailCell = DataGrid1.SelectedCells[2];
            var passwordCell = DataGrid1.SelectedCells[3];

            try
            {
                string id = ((TextBlock)idCell.Column.GetCellContent(idCell.Item)).Text;
                Input_UserName.Text = ((TextBlock)nameCell.Column.GetCellContent(nameCell.Item)).Text;
                Input_Email.Text = ((TextBlock)emailCell.Column.GetCellContent(emailCell.Item)).Text;
                Input_Password.Text = ((TextBlock)passwordCell.Column.GetCellContent(passwordCell.Item)).Text;
                _user = users.Single(item => item.Id.ToString() == id);
            }
            catch
            {

            }
        }

        private void Btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            users.Remove(_user);
            UpdateJsonData();
            UpdateDataGrid();
        }
    }
}

