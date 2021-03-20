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

            db = new ApplicationContext();
            db.Debts.Load();
            DataContext = db.Debts.Local.ToBindingList();
        }

        /// <summary>
        ///     Добавление сущности
        /// </summary>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var debtWindow = new DebtWindow(new Debt());
            if (debtWindow.ShowDialog() == true)
            {
                var debt = debtWindow.Debt;
                if (debt.ChekValues())
                {
                    db.Debts.Add(debt);
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Данные введены некорректно, операция не выполнена.", "Ошибка!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        ///     Редактирование сущности
        /// </summary>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // если ни одного объекта не выделено, выходим
            if (debtsList.SelectedItem == null) return;
            // получаем выделенный объект
            var debt = debtsList.SelectedItem as Debt;

            var debtWindow = new DebtWindow(new Debt
            {
                Id = debt.Id,
                Name = debt.Name,
                Addres = debt.Addres,
                Phone = debt.Phone,
                DateDebt = debt.DateDebt,
                InitialDebt = debt.InitialDebt,
                CurrentDebt = debt.CurrentDebt,
                Bank = debt.Bank
            });

            if (debtWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                debt = db.Debts.Find(debtWindow.Debt.Id);
                if (debt != null)
                {
                    debt.Name = debtWindow.Debt.Name;
                    debt.Addres = debtWindow.Debt.Addres;
                    debt.Phone = debtWindow.Debt.Phone;
                    debt.DateDebt = debtWindow.Debt.DateDebt;
                    debt.InitialDebt = debtWindow.Debt.InitialDebt;
                    debt.CurrentDebt = debtWindow.Debt.CurrentDebt;
                    debt.Bank = debtWindow.Debt.Bank;

                    if (debt.ChekValues())
                    {
                        db.Entry(debt).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Данные введены некорректно, операция не выполнена.", "Ошибка!",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        /// <summary>
        ///     Удаление сущности
        /// </summary>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // если ни одного объекта не выделено, выходим
            if (debtsList.SelectedItem == null) return;
            // получаем выделенный объект
            var result = MessageBox.Show("Вы действительно хотите удалить текущую запись?", "", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var debt = debtsList.SelectedItem as Debt;
                db.Debts.Remove(debt);
                db.SaveChanges();
            }
        }

        /// <summary>
        ///     Экспортирование данных в таблицу Excel
        /// </summary>
        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            var excelApp = new Application();
            excelApp.Visible = false;
            excelApp.Workbooks.Add();
            _Worksheet workSheet = (Worksheet) excelApp.ActiveSheet;

            workSheet.Cells[1, "A"] = "Номер договора";
            workSheet.Cells[1, "B"] = "Имя";
            workSheet.Cells[1, "C"] = "Адрес проживания";
            workSheet.Cells[1, "D"] = "Номер телефона";
            workSheet.Cells[1, "E"] = "Дата взятия кредита";
            workSheet.Cells[1, "F"] = "Начальная сумма кредита";
            workSheet.Cells[1, "G"] = "Долг";
            workSheet.Cells[1, "H"] = "Название банка";

            var row = 2;
            foreach (var debt in db.Debts.Local.ToBindingList())
            {
                workSheet.Cells[row, "A"] = debt.Id;
                workSheet.Cells[row, "B"] = debt.Name;
                workSheet.Cells[row, "C"] = debt.Addres;
                workSheet.Cells[row, "D"] = debt.Phone;
                workSheet.Cells[row, "E"] = debt.DateDebt;
                workSheet.Cells[row, "F"] = debt.InitialDebt;
                workSheet.Cells[row, "G"] = debt.CurrentDebt;
                workSheet.Cells[row, "H"] = debt.Bank;
                row++;
            }

            for (var i = 1; i < 8; i++) workSheet.Columns[i].AutoFit();
            excelApp.Visible = true;
        }

        /// <summary>
        ///     Выход из программы
        /// </summary>
        private void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        ///     Вызов окна с попомщью
        /// </summary>
        private void ShowHelp(object sender, RoutedEventArgs e)
        {
            var help = new HelpForm();
            help.Owner = this;
            help.ShowDialog();
        }
    }
}