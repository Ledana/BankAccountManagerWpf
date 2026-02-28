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
    /// Interaction logic for DepositWindow.xaml
    /// </summary>
    public partial class DepositWindow : Window
    {
        private static readonly CultureInfo EuroInfo = new("de-DE");
        private User? _currentUser;
        public DepositWindow(User? currentUser)
        {
            InitializeComponent();

                this.Title = currentUser.FullName;
                UserBalance.Text = currentUser?.BankAccount?.Balance.ToString("C2", EuroInfo) ?? "0";
                _currentUser = currentUser;
        }

        private void ProcessButton_Click(object sender, RoutedEventArgs e)
        {
            var flag = decimal.TryParse(AmountTextBox.Text, out decimal amount);

            if (flag)
            {
                _currentUser.BankAccount.MakeDeposit(amount);
                UserBalance.Text = _currentUser.BankAccount.Balance.ToString("C2", EuroInfo);
                this.Close();
            }
            else
                MessageBox.Show("The amount is not in the right format");
        }
    }
}
