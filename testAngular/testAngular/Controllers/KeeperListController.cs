using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testAngular.Models;
using testAngular.ViewModels;

namespace testAngular.Controllers
{
    [Produces("application/json")]
    [Route("api/keeperlist")]
    public class KeeperListController : Controller
    {
        private readonly DetailsContext _context;
        public KeeperListController(DetailsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var keep = _context.Details.ToList();
            int kol = keep.Count;
            var keepers = _context.Storekeepers.ToList();
            int count = keepers.Count;
            List<Keeper_Count> kolvo = new List<Keeper_Count>(count); ;
            for (int i = 0; i < count; i++)
            {
                int sum = 0;
                for (int a = 0; a < kol; a++)
                {
                    if (((keepers.ElementAt(i).Id == keep.ElementAt(a).StorekeeperId) && (keep.ElementAt(a).DateDelete == null)))
                    {
                        sum = sum + Convert.ToInt32(keep.ElementAt(a).Count);
                    }

                }
                Keeper_Count kep = new Keeper_Count();
                kep.Id = keepers.ElementAt(i).Id;
                kep.KeeperName = keepers.ElementAt(i).KeeperName;
                kep.Count = sum;
                kolvo.Add(kep);
            }
            return Json(kolvo);
        }
        [HttpDelete]
        public IActionResult DeleteKeeperDB([FromBody]int kepId) //Удаление записи КЛАДОВЩИК из БД
        {
            Storekeeper b = _context.Storekeepers.Find(kepId);
            if(b==null)
            {
                return Json("Запись удалена ранее. Обновите страницу!");
            }
            var detail = _context.Details.FromSql("SELECT * From Details").ToList();
            int count = detail.Count;
            for (int i = 0; i < count; i++)
            {
                if (b.Id == detail.ElementAt(i).StorekeeperId && detail.ElementAt(i).Count != null && detail.ElementAt(i).Count != 0)
                {
                    return Json("Запись не может быть удалена:за кладовщиком числятся детали!");
                }
            }
            if (b != null)
            {
                _context.Storekeepers.Remove(b);
                _context.SaveChanges();
            }
            return Json("Запись успешно удалена");
        }
    }
}