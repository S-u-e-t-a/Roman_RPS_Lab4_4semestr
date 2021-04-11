using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace laba4
{
    /// <summary>
    /// Класс описывающий должника
    /// </summary>
    public class Debt : INotifyPropertyChanged, IDataErrorInfo
    {
        private string addres;
        private string bank;
        private decimal currentDebt;
        private string dateDebt;
        private decimal initialDebt;
        private string name;
        private string phone;

        //private Regex regexDate = new Regex(@"(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[012])\.(19|20)\d\d$"); //дата в формате DD.MM.YYYY
        private Regex regexPhone = new Regex(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$");
        /*Номера по типу
               +79261234567
               89261234567
               79261234567
               +7 926 123 45 67
               8(926)123-45-67
               123-45-67
               9261234567
               79261234567
               (495)1234567
               (495) 123 45 67
               89261234567
               8-926-123-45-67
               8 927 1234 234
               8 927 12 12 888
               8 927 12 555 12
               8 927 123 8 123
             */
        private Regex regexEmty = new Regex(@"^\s*$"); //текст состоящий только из символов-разделителей

        /// <summary>
        /// Валидация данных
        /// </summary>
        /// <param name="columnName">Имя параметра</param>
        /// <returns>Описание ошибки</returns>
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Addres":
                        if (Addres == null)
                        {
                            error = "Значение не может быть пустым";
                        }
                        else if (regexEmty.IsMatch(Addres))
                        {
                            error = "Значение не состоять только из знаков-разделителей";
                        }
                        break;
                    case "Bank":
                        if (Bank == null)
                        {
                            error = "Значение не может быть пустым";
                        }
                        else if (regexEmty.IsMatch(Bank))
                        {
                            error = "Значение не состоять только из знаков-разделителей";
                        }
                        break;
                    case "Name":
                        if (Name == null)
                        {
                            error = "Значение не может быть пустым";
                        }
                        else if (regexEmty.IsMatch(Name))
                        {
                            error = "Значение не состоять только из знаков-разделителей";
                        }
                        break;
                    case "Phone":
                        if (Phone != null)
                        {
                            if (!regexPhone.IsMatch(Phone))
                            {
                                error = "Неверный формат ввода номера";
                            }
                        }
                        else if (Phone == null)
                        {
                            error = "Значение не может быть пустым";
                        }
                        else if (regexEmty.IsMatch(Name))
                        {
                            error = "Значение не состоять только из знаков-разделителей";
                        }

                        break;
                    case "DateDebt":
                         if (DateDebt == null)
                         {
                             error = "Значение не может быть пустым";
                         }
                         else if (regexEmty.IsMatch(DateDebt))
                         {
                             error = "Значение не состоять только из знаков-разделителей";
                         }

                         break;
                    case "InitialDebt":
                        if (InitialDebt <= 0)
                        {
                            error = "Начальная сумма долга должна быть больше 0";
                        }
                        break;
                }
                return error;
            }
        }
        
        public int Id { get; set; }

        public decimal InitialDebt
        {
            get => initialDebt;
            set
            {
                initialDebt = value;
                OnPropertyChanged();
            }
        }

        public decimal CurrentDebt
        {
            get => currentDebt;
            set
            {
                currentDebt = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Addres
        {
            get => addres;
            set
            {
                addres = value;
                OnPropertyChanged();
            }
        }

        public string DateDebt
        {
            get => dateDebt;
            set
            {
                dateDebt = value;
                OnPropertyChanged();
            }
        }

        public string Bank
        {
            get => bank;
            set
            {
                bank = value;
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnPropertyChanged();
            }
        }

        public string Error => throw new System.NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;
        
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}