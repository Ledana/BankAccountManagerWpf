using BankAccountManagerWpf.LogicCode;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace BankAccountManagerWpf
{
    /// <summary>
    /// Interaction logic for TransferWindow.xaml
    /// </summary>
    public partial class TransferWindow : Window
    {
        private static readonly CultureInfo EuroInfo = new("de-DE");
        private User? _currentUser;
        
        //this window is why i needed the allusers. to find the account to which we transfer
        private IUserRepository _allUsers;
        public TransferWindow(User? currentUser, IUserRepository? allUsers)
        {
            InitializeComponent();

            if (currentUser == null || allUsers == null)
                this.Close();

            else
            {
                this.Title = currentUser.FullName;
                UserBalance.Text = currentUser.BankAccount.Balance.ToString("C2", EuroInfo);
                _currentUser = currentUser;
                _allUsers = allUsers;
            }
        }

        private void ProcessButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var flag = decimal.TryParse(AmountTextBox.Text, out decimal amount);
                var targetId = TargetAccountTextBox.Text;


                if (!flag)
                    MessageBox.Show("The amount is not in the right format", "", MessageBoxButton.OK);

                if (targetId != null && flag)
                {
                    var targetUser = _allUsers.FindUserById(targetId);

                    if (targetUser != null)
                    {
                        var targetAccount = targetUser.BankAccount;
                     
                            _currentUser.BankAccount.TransferMoney(targetAccount, amount);

                            UserBalance.Text = _currentUser.BankAccount.Balance.ToString("C2", EuroInfo);
                            this.Close();                       
                    }
                    else
                        MessageBox.Show("The target id could not be found");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(e.GetType + ex.StackTrace);
            }
        }
//        var users = allUsers.GetUsers();
//        Console.WriteLine("Put the userId you want to transfer to");
//            string? targetId = Console.ReadLine();
//            if (targetId == null)
//            {
//                Console.WriteLine("The userId is not in the right format");
//                return;
//            }
//            User? target = users.FirstOrDefault(a => a.UserId == targetId);

//            if (target == null)
//            {
//                Console.WriteLine("The target id could not be found");
//                return;
//            }
//Console.WriteLine("Put the amount you want to transfer");

//bool flag = decimal.TryParse(Console.ReadLine(), out decimal amount);

//if (flag == false)
//{
//    Console.WriteLine("The amount is not in the right format");
//    return;
//}

//user.BankAccount.TransferMoney(target.BankAccount, amount);
    }
}
