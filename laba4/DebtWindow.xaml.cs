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
            DataContext = new DebtWindowViewModel(d);
            if (DataContext.CloseAction == null)
                DataContext.CloseAction = new Action(this.Close);
        }

       

    }
}