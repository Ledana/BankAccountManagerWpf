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
    /// Interaction logic for AllMovementsWindow.xaml
    /// </summary>
    public partial class AllMovementsWindow : Window
    {
        private static readonly CultureInfo EuroInfo = new("de-DE");
        private User? _currentUser;
        private List<string> _movements;
        public AllMovementsWindow(User? currentUser)
        {
            InitializeComponent();

                this.Title = currentUser.FullName;
                UserBalance.Text = currentUser.BankAccount.Balance.ToString("C2", EuroInfo);
                _currentUser = currentUser;

                _movements = _currentUser.BankAccount.GetMovements();

                MovementsControl.ItemsSource = _movements;

        }
    }
}
