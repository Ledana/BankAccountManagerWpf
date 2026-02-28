using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace BankAccountManagerWpf
{
    /// <summary>
    /// Interaction logic for WithdrawWindow.xaml
    /// </summary>
    public partial class WithdrawWindow : Window
    {
        private static readonly CultureInfo EuroInfo = new("de-DE");
        private User? _currentUser;
        public WithdrawWindow(User? currentUser)
        {
            InitializeComponent();

            
                this.Title = currentUser.FullName;
                _currentUser = currentUser;
                UserBalance.Text = currentUser.BankAccount.Balance.ToString("C2", EuroInfo); 
        }

        private void ProcessButton_Click(object sender, RoutedEventArgs e)
        {
            var flag = decimal.TryParse(AmountTextBox.Text, out decimal amount);

            if (flag)
            {
                _currentUser.BankAccount.MakeWithdraw(amount);
                UserBalance.Text = _currentUser.BankAccount.Balance.ToString("C2", EuroInfo);
                this.Close();
            }
            else
                MessageBox.Show("The amount is not in the right format");
        }
    }
}
