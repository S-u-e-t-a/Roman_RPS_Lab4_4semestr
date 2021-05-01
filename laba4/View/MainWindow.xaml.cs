using System.Data.Entity;
using System.Windows;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using Window = System.Windows.Window;

namespace laba4
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApplicationContext db;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new ApplicationViewModel();
        }

    }
}