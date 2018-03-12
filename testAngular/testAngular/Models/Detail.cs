using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testAngular.Models
{
    public class Detail
    {
        public int Id { get; set; }
        public string NomCode { get; set; } // номенклатурный код
        public string Name { get; set; } // наименование детали
        public int? Count { get; set; } // кол-во деталей
        public int StorekeeperId { get; set; } /// id кладовщика
        public Storekeeper Storekeeper { get; set; } //Внешний ключ на таблицу Кладовщик
        public DateTime DateCreate { get; set; } // дата создания
        public DateTime? DateDelete { get; set; } // дата удаления
    }
}
