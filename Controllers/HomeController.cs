using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Url_Shortener.Models;

namespace Url_Shortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UrlModelContext _urlDb;

        public HomeController(ILogger<HomeController> logger, UrlModelContext urlDb)
        {
            _logger = logger;
            _urlDb = urlDb;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string url)
        {
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                throw new Exception();
            }

            int id = 0;
            var urlbuilder = $"{Request.Scheme}://{Request.Host}";
            var result = "";

            var urldb = (from x in _urlDb.UrlModel where (x.OriginalUrl == url || x.ShortenedUrl == url) select x).FirstOrDefault();

            if(urldb == null)
            {
                UrlModel urlobj = new UrlModel(url);
                _urlDb.UrlModel.Add(urlobj);
                _urlDb.SaveChanges();

                id = urlobj.Id;
                
                urlobj.Code = Helpers.UrlHelper.Encode(id);
                urlbuilder += "/" + urlobj.Code;
                urlobj.ShortenedUrl = urlbuilder;
                
                _urlDb.UrlModel.Update(urlobj);
                _urlDb.SaveChanges();

                result = urlbuilder;
            }
            else
            {
                result = urldb.ShortenedUrl;
            }

            return Json(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
