using BankAccountManagerWpf.LogicCode;
using Microsoft.Data.Sqlite;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankAccountManagerWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class LoginWindow : Window
    {
        //we create a userrepository and user so we can save their values and pass them
        //to other windows through their ctor
        private IUserRepository _allUsers;
        public User? CurrentUser;
        public LoginWindow()
        {
            InitializeComponent();  
            try
            {
                string connectionPath = "Data Source=userSql.db;";
                //this line gets the users form the database
                _allUsers = new DbUserRepository(connectionPath);
                //this line gets the users from hardcoded userrepository
                //_allUsers = new UserRepository();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text.Trim();
            var pasword = PasswordBox.Password;
            
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(pasword))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButton.OK);
                return;
            }

            //validating the userinput to username and password
            CurrentUser = _allUsers.ValidateUser(username, pasword);

            if (CurrentUser != null)
            {
                //if the credentials match any user the dashboard window opens
                Dashboard dashboard = new(CurrentUser, _allUsers);
                MessageBox.Show($"Wellcome {CurrentUser.FullName} to your account", "Login Successfull", MessageBoxButton.OK, MessageBoxImage.Information);
                
                dashboard.Show();
                //clearing the textboxes so the next user has them empty
                UsernameTextBox.Clear();
                PasswordBox.Clear();
            }
            else
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        
    }
}
