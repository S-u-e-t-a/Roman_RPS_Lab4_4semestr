using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Windows;
using Microsoft.Office.Interop.Excel;


namespace laba4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        ApplicationContext db;
        public MainWindow()
        {
            InitializeComponent();

            db = new ApplicationContext();
            db.Debts.Load();
            this.DataContext = db.Debts.Local.ToBindingList();
        }
        // добавление
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            DebtWindow debtWindow = new DebtWindow(new Debt());
            if (debtWindow.ShowDialog() == true)
            {
                Debt debt = debtWindow.Debt;
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
        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // если ни одного объекта не выделено, выходим
            if (debtsList.SelectedItem == null) return;
            // получаем выделенный объект
            Debt debt = debtsList.SelectedItem as Debt;

            DebtWindow debtWindow = new DebtWindow(new Debt
            {
                Id = debt.Id,
                Name = debt.Name,
                Addres = debt.Addres,
                Phone = debt.Phone,
                DateDebt = debt.DateDebt,
                InitialDebt = debt.InitialDebt,
                CurrentDebt = debt.CurrentDebt,
                Bank = debt.Bank,

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
        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // если ни одного объекта не выделено, выходим
            if (debtsList.SelectedItem == null) return;
            // получаем выделенный объект
            Debt debt = debtsList.SelectedItem as Debt;
            db.Debts.Remove(debt);
            db.SaveChanges();
        }


        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = false;
            excelApp.Workbooks.Add();
            _Worksheet workSheet = (Worksheet)excelApp.ActiveSheet;

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

            for (int i = 1; i < 8; i++)
            {
                workSheet.Columns[i].AutoFit();
            }
            excelApp.Visible = true;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ShowHelp(object sender, RoutedEventArgs e)
        {
            HelpForm help  = new HelpForm();
            help.Owner = this;
            help.ShowDialog();
        }

    }
}
