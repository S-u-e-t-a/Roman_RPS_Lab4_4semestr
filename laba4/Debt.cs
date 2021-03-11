using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace laba4
{
    /// <summary>
    /// Класс описывающий должника
    /// </summary>
    public class Debt : INotifyPropertyChanged
    {
        private string addres;
        private string bank;
        private decimal currentDebt;
        private string dateDebt;
        private decimal initialDebt;
        private string name;
        private string phone;


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


        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Проверка корректности данных
        /// </summary>
        /// <returns>Результат проверки</returns>
        public bool ChekValues()
        {
            if ((name != null) & (addres != null) & (dateDebt != null) & (bank != null) & (phone != null))
            {
                if (initialDebt > 0)
                    return true;
                return false;
            }

            return false;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}