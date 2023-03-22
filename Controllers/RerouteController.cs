using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Url_Shortener.Models;

namespace Url_Shortener.Controllers
{
    public class RerouteController : Controller
    {
        private readonly UrlModelContext _urlDb;

        public RerouteController(UrlModelContext urlDb)
        {
            _urlDb = urlDb;
        }
        public IActionResult Index()
        {
            var urldb = (from x in _urlDb.UrlModel select x).ToList();
            return View(urldb);
        }

        public IActionResult Reroute(string code)
        {
            var urldb = (from x in _urlDb.UrlModel where (x.Code == code) select x).FirstOrDefault();

            if(urldb != null)
            {
                urldb.Hits += 1;
                _urlDb.UrlModel.Update(urldb);
                _urlDb.SaveChanges();
                return Redirect(urldb.OriginalUrl);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
