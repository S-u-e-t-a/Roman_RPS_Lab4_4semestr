using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4
{
    class MainViewModel
    {
        MainViewModel()
        {
            var db = new ApplicationContext();
            db.Debts.Load();
        }
    }
}
