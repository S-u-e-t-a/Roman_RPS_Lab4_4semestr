using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace laba4
{
    /// <summary>
    /// Окно для изменения/добавления сущностей 
    /// </summary>
    public partial class DebtWindow : Window
    {
        public DebtWindow(Debt d)
        {
            InitializeComponent();
            Debt = d;
            DataContext = Debt;
        }

        public Debt Debt { get; }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }



        private void validationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                AcceptButton.IsEnabled = false;
            }

            if (e.Action == ValidationErrorEventAction.Removed)
            {
                AcceptButton.IsEnabled = true;
            }
        }
    }
}