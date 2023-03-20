using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Url_Shortener.Models
{
    public class UrlModel
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }

    }


}
