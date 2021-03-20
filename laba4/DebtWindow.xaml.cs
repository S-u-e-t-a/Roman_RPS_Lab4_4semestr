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
            if (GetErrors(sender as Grid))
            {
                AcceptButton.IsEnabled = false;
            }
            else
            {
                AcceptButton.IsEnabled = true;
            }
        }


        private bool GetErrors(DependencyObject obj)
        {
            foreach (object child in LogicalTreeHelper.GetChildren(obj))
            {
                TextBox element = child as TextBox;
                if (element == null) continue;

                if (Validation.GetHasError(element))
                {
                    return true;
                }
            }

            return false;
        }

    }
}