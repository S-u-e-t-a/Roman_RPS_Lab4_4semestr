using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Windows;


namespace laba4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
    }
}
