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
    
    public partial class MainWindow : Window
    {
        //we create a userrepository and user so we can save their values and pass them
        //to other windows through their ctor
        private UserRepository _allUsers;
        public User? CurrentUser;
        public MainWindow()
        {
            InitializeComponent();
            
                //getting all the hardcoded users
                _allUsers = new();
            
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
                MyMethod();
                dashboard.Show();
                
            }
            else
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        void MyMethod()
        {
            MessageBox.Show("Hello");
        }
    }
}
