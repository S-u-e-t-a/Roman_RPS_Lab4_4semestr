using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace laba4
{
    public class DebtWindowViewModel : INotifyPropertyChanged
    {
        public Debt Debt { get; }

        public bool isAcceptButtonEnabled;

        public bool DialogResult;
        private void Accept_Click1(object sender, RoutedEventArgs e)
        {
            //DialogResult = true;
        }
        public Action CloseAction { get; set; }
        private RelayCommand accept_Click;
        public RelayCommand Accept_Click
        {
            get
            {
                return accept_Click ??
                       (accept_Click = new RelayCommand(obj =>
                       {
                           DialogResult = true;
                           CloseAction();
                       }));
            }
        }
        
        private RelayCommand validationError;

        private ICommand openDialogCommand = null;
        public ICommand OpenDialogCommand
        {
            get { return this.openDialogCommand; }
            set { this.openDialogCommand = value; }
        }
        private void OnOpenDialog(object parameter)
        {

        }
        public DebtWindowViewModel(Debt debt)
        {
            Debt = debt;
            

        public RelayCommand ValidationError
        {
            get
            {
                return validationError ??
                   (validationError = new RelayCommand(obj =>
                   {
                       if (GetErrors(obj as Grid))
                       {
                           isAcceptButtonEnabled = false;
                       }
                       else
                       {
                           isAcceptButtonEnabled = true;
                       }
                   }));
            }
        }
        public void ValidationError1(object sender, ValidationErrorEventArgs e)
        {
            if (GetErrors(sender as Grid))
            {
                isAcceptButtonEnabled = false;
            }
            else
            {
                isAcceptButtonEnabled = true;
            }
        }

        private bool GetErrors(DependencyObject obj)
        {
            foreach (object child in LogicalTreeHelper.GetChildren(obj))
            {
                Control element = child as Control;
                if (element == null) continue;

                if (Validation.GetHasError(element))
                {
                    return true;
                }
            }

            return false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
