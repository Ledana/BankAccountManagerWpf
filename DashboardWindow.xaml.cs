using BankAccountManagerWpf.LogicCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        //we save the user and the list to pass their values to other windows as well
        private User? _currentUser;
        private IUserRepository? _allUsers;

        //we create the balance prop so we can see the changes in the window
        private decimal _userBalance;
        
        public decimal UserBalance
        {
            get => _userBalance;
            set
            {
                _userBalance = value;
            }
        }
        
        public Dashboard(User currentUser, IUserRepository allUsers)
        {
            InitializeComponent();
            this.DataContext = currentUser;
            
            try
            {
                this.Title = currentUser.FullName;
                _currentUser = currentUser;
                _allUsers = allUsers;

                UserBalance = _currentUser?.BankAccount?.Balance ?? 0m;
                if (currentUser.BankAccount != null)
                    _currentUser.BankAccount.PropertyChanged += BankAccount_PropertyChanged;

            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.StackTrace);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.GetType() + e.StackTrace);
            }
            
        }

        private void BankAccount_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e == null || e.PropertyName == null || e.PropertyName == nameof(BankAccount.Balance))
                Dispatcher.Invoke(() => UserBalance = _currentUser?.BankAccount?.Balance ?? 0m);
        }

        private void DepositButton_Click(object sender, RoutedEventArgs e)
        {
            DepositWindow depositWindow = new(_currentUser);
            depositWindow.Show();
        }

        private void WithdrawButton_Click(object sender, RoutedEventArgs e)
        {
            WithdrawWindow withdrawWindow = new(_currentUser);
            withdrawWindow.Show();
        }

        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            TransferWindow transferWindow = new(_currentUser, _allUsers);
            transferWindow.Show();
        }

        private void AllMovementsButton_Click(object sender, RoutedEventArgs e)
        {
            AllMovementsWindow allMovementsWindow = new(_currentUser);
            allMovementsWindow.Show();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
