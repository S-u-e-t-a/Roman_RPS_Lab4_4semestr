using System.Windows;

namespace laba4
{
    public partial class DebtWindow : Window
    {
        public Debt Debt { get; private set; }

        public DebtWindow(Debt d)
        {
            InitializeComponent();
            Debt = d;
            this.DataContext = Debt;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
