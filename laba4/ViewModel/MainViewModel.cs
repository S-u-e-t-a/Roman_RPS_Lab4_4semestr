using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Windows;
using Application = Microsoft.Office.Interop.Excel.Application;
using Window = System.Windows.Window;
using Microsoft.Office.Interop.Excel;
using laba4;
using laba4.View;

namespace laba4
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        ApplicationContext db;
        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand deleteCommand;
        RelayCommand exportToExcelCommmand;
        RelayCommand exitCommand;
        RelayCommand showHelpCommand;
        IEnumerable<Debt> debts;

        public IEnumerable<Debt> Debts
        {
            get { return debts; }
            set
            {
                debts = value;
                OnPropertyChanged("Debts");
            }
        }

        public ApplicationViewModel()
        {
            db = new ApplicationContext();
            db.Debts.LoadAsync();
            Debts = db.Debts.Local.ToBindingList();
        }
        // команда добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      DebtWindow debtWindow = new DebtWindow(new Debt());
                      if (debtWindow.ShowDialog() == true)
                      {
                          Debt debt = debtWindow.Debt;
                          db.Debts.Add(debt);
                          db.SaveChangesAsync();
                      }
                  }));
            }
        }
        // команда редактирования
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      Debt debt = selectedItem as Debt;

                      Debt vm = new Debt()
                      {
                          Id = debt.Id,
                          Name = debt.Name,
                          Addres = debt.Addres,
                          Phone = debt.Phone,
                          DateDebt = debt.DateDebt,
                          InitialDebt = debt.InitialDebt,
                          CurrentDebt = debt.CurrentDebt,
                          Bank = debt.Bank
                      };
                      DebtWindow debtWindow = new DebtWindow(vm);


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

                              db.Entry(debt).State = EntityState.Modified;
                              
                              db.SaveChangesAsync();
                          }
                      }
                  }));
            }
        }
        // команда удаления
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "",
                          MessageBoxButton.OKCancel, MessageBoxImage.Question);
                      if (result == MessageBoxResult.OK)
                      {
                          Debt debt = selectedItem as Debt;
                          db.Debts.Remove(debt);
                          db.SaveChangesAsync();
                      }
                  }));
            }
        }

        /// <summary>
        /// Команда выхода
        /// </summary>
        public RelayCommand ExitCommand
        {
            get
            {
                return exitCommand ??
                       (exitCommand = new RelayCommand((selectedItem) =>
                       {
                           System.Windows.Application.Current.Shutdown();
                       }));
            }
        }
        /// <summary>
        /// Команда показа помощи
        /// </summary>
        public RelayCommand ShowHelpCommand
        {
            get
            {
                return showHelpCommand ??
                       (showHelpCommand = new RelayCommand((selectedItem) =>
                       {
                           var help = new HelpForm();
                           help.ShowDialog();
                       }));
            }
        }
        /// <summary>
        /// Команда экспорта в эксель
        /// </summary>
        public RelayCommand ExportToExcelCommmand
        {
            get
            {
                return exportToExcelCommmand ??
                       (exportToExcelCommmand = new RelayCommand((o) =>
                       {
                           var excelApp = new Application();
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

                           for (var i = 1; i < 8; i++) workSheet.Columns[i].AutoFit();
                           excelApp.Visible = true;
                       }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}