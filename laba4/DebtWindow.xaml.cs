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

        /// <summary>
        /// Действие при нажатии на конпку 
        /// </summary>
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        /// <summary>
        /// Обработка ошибок валидации
        /// </summary>
        private void ValidationError(object sender, ValidationErrorEventArgs e)
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

        /// <summary>
        /// Проверка на наличие ошибок валидации
        /// </summary>
        /// <param name="obj">Родитель объектов, валидацию которых надо проверить</param>
        /// <returns>Результат проверки</returns>
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

    }
}