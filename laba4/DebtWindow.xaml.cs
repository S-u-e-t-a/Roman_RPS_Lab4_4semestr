using System.Windows;

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
    }
}