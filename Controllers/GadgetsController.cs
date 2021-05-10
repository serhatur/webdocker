using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebDocker.Model;

namespace WebDocker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GadgetsController : ControllerBase
    {
        ApiDbContext _ctx;

        public GadgetsController(ApiDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Gadget>> Get()
        {
            //push to master
            if (_ctx.Gadgets.Any())
            {
                _ctx.Gadgets.Add(new Gadget { Name = DateTime.Now.ToString() });
                _ctx.SaveChanges();
            }
            return _ctx.Gadgets.ToList();
        }
    }
}
