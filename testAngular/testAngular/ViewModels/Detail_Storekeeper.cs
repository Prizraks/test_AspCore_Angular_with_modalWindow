using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testAngular.ViewModels
{
    public class Detail_Storekeeper
    {
        public int Id { get; set; }
        public string Nomcode { get; set; } // номенклатурный код
        public string Name { get; set; } // наименование детали
        public int Count { get; set; } // кол-во деталей
        public string Keepername { get; set; } /// id кладовщика
        public DateTime Datecreate { get; set; } // дата создания
        public DateTime Datedelete { get; set; } // дата удаления
    }
}
