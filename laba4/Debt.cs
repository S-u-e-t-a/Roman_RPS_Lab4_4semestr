using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace laba4
{
    public class Debt : INotifyPropertyChanged
    {
        private decimal initialDebt;
        private decimal currentDebt;
        private string name;
        private string addres;
        private string dateDebt;
        private string bank;
        private string phone;


        public int Id { get; set; }

        public decimal InitialDebt
        {
            get { return initialDebt; }
            set
            {
                initialDebt = value;
                OnPropertyChanged("InitialDebt");
            }
        }

        public decimal CurrentDebt
        {
            get { return currentDebt; }
            set
            {
                currentDebt = value;
                OnPropertyChanged("CurrentDebt");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Addres
        {
            get { return addres; }
            set
            {
                addres = value;
                OnPropertyChanged("Addres");
            }
        }

        public string DateDebt
        {
            get { return dateDebt; }
            set
            {
                dateDebt = value;
                OnPropertyChanged("DateDebt");
            }
        }

        public string Bank
        {
            get { return bank; }
            set
            {
                bank = value;
                OnPropertyChanged("Bank");
            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }





        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
