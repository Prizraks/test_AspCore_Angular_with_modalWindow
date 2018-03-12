using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testAngular.Models;
using Microsoft.EntityFrameworkCore;

namespace testAngular.Controllers
{
    [Produces("application/json")]
    [Route("api/keepers")]
    public class KeeperController : Controller
    {
     
            private readonly DetailsContext _context;
            public KeeperController(DetailsContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<IActionResult> Get()
            {
                List<Storekeeper> keeper_ = new List<Storekeeper>();
                var Keepers = await (from data in _context.Storekeepers
                                     select new
                                     {
                                         Id = data.Id,
                                         KeeperName = data.KeeperName
                                     }).ToListAsync();
                Keepers.ForEach(x =>
                {
                    Storekeeper pro = new Storekeeper();
                    pro.Id = x.Id;
                    pro.KeeperName = x.KeeperName;
                    keeper_.Add(pro);
                });


                return Json(keeper_);
            }
        [HttpPost]
        public IActionResult AddKeeper([FromBody]Storekeeper kepObj)
        {
            _context.Storekeepers.Add(kepObj);
            _context.SaveChanges();
            return Json("OK");


        }
    }
}