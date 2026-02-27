using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankAccountManagerWpf
{
    public class BankAccount : INotifyPropertyChanged
    {
        private decimal _balance;
        public decimal Balance
        {
            get { return _balance; }
            private set
            {
                if (_balance != value)
                {
                    _balance = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string? UserId;
        private List<string> _movements = [];

        public BankAccount(string id)
        {
            Balance = 0m;
            UserId = id;
        }


        public void MakeDeposit(decimal amount)
        {
            if (amount < 0)
                MessageBox.Show("The amount should be positive");

            else if (amount == 0)
                return;
            else
            {
                Balance += amount;
                MessageBox.Show($"You deposidet {amount} and your balance now is {_balance}");
                _movements.Add($"You deposited {amount} in {DateTime.Now}");
            }
        }
        public void MakeWithdraw(decimal amount)
        {
            if (amount > _balance)
                MessageBox.Show("The amount to withdraw should be less then the balance");

            else
            {
                Balance -= amount;
                MessageBox.Show($"You withdrawed {amount} and your balance now is {_balance}");
                _movements.Add($"You withdrawed {amount} at {DateTime.Now}");
            }
        }

        public void TransferMoney(BankAccount targetAccount, decimal amount)
        {

            if (amount <= 0)
            {
                MessageBox.Show("The amount should be positive");
                return;
            }
            else if (amount >= _balance)
            {
                MessageBox.Show("The amount is bigger than your balance");
                return;
            }

            if (targetAccount == null)
                MessageBox.Show("The userId is not valid");
            else if (targetAccount.UserId == UserId)
            {
                MessageBox.Show("You can not transfer money to yourself");
                return;
            }
            else
            {
                Balance -= amount;
                targetAccount.Balance += amount;
                targetAccount._movements.Add($"{UserId} transfered you {amount} at {DateTime.Now}");

                MessageBox.Show($"You transfered {amount} to {targetAccount.UserId}\nYour balance is now {_balance}");
                _movements.Add($"You transfered {amount} to {targetAccount.UserId} at {DateTime.Now}");
            }
        }

        public List<string> GetMovements()
        {
            List<string> movements = new();
            if (_movements.Count == 0)
                return new List<string>() { "You have no movements" };
            else
            {
                int num = 1;
                foreach (var item in _movements)
                {
                    movements.Add($"{num}. {item}");
                    num++;
                }
                return movements;
            }

        }
    }
}
