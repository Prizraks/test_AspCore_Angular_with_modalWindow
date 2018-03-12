using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testAngular.Models;
using testAngular.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace testAngular.Controllers
{
    [Produces("application/json")]
    [Route("api/detail")]
    public class DetailController : Controller
    {
        private readonly DetailsContext _context;

        public DetailController(DetailsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> DetailList()
        {
            List<Detail_Storekeeper> ilIst = new List<Detail_Storekeeper>();
            var listData = await (from det in _context.Details
                                  join kep in _context.Storekeepers on det.StorekeeperId equals kep.Id
                                  select new
                                  {
                                      det.Id,
                                      det.Name,
                                      det.NomCode,
                                      det.Count,
                                      det.DateCreate,
                                      det.DateDelete,
                                      kep.KeeperName
                                  }
                          ).ToListAsync();
            listData.ForEach(x =>
            {
                Detail_Storekeeper Obj = new Detail_Storekeeper();
                Obj.Id = x.Id;
                Obj.Name = x.Name;
                Obj.Nomcode = x.NomCode;
                Obj.Count = Convert.ToInt32(x.Count);
                Obj.Datecreate = x.DateCreate;
                Obj.Datedelete = Convert.ToDateTime(x.DateDelete);
                Obj.Keepername = x.KeeperName;
                ilIst.Add(Obj);
            });
            return Json(ilIst);
        }

        [HttpPost]
        public IActionResult AddDetail([FromBody]Detail detObj)
        {
            _context.Details.Add(detObj);
            _context.SaveChanges();
            return Json("OK");
        }
        [HttpDelete]
        public IActionResult RemoveDetailOnDB([FromBody]int detId)
        {
            Detail Det=_context.Details.Find(detId);
            if(Det==null)
            {
                return Json("Запись не найдена");
            }
            _context.Details.Remove(Det);
            _context.SaveChanges();
            return Json("Запись Удалена");
        }
        [HttpPut]
        public IActionResult RemoveDetail([FromBody]int uptId)
        {
            Detail b= _context.Details.Where(x => x.Id == uptId).First();
            var upt = _context.Details.Where(x => x.Id == uptId).First();
            //Detail b = _context.Details.Find(uptId);
            //var upt = _context.Details.Find(uptId);
            if (upt.DateDelete == null)
            {
                upt.DateDelete = DateTime.Now;
                if (b != null)
                //_context.Update(upt);
                _context.SaveChanges();
                return Json("Деталь переведена в удаленные");
            }
            else
                return Json("Деталь была удалена ранее");            
        }
    }
}