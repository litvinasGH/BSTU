using Printinvest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Printinvest.ViewModels
{
    internal class EquipmentDetailsWindowVM : INotifyPropertyChanged
    {
        public Equipment Selected { get; }      // или ваш SelectedBook
        public User LoggedInUser { get; }      // текуший авторизованный пользователь

        public EquipmentDetailsWindowVM(Equipment eq, User currentUser)
        {
            Selected = eq;
            LoggedInUser = currentUser;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
