using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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

        public string Code { get; set; }

        public int Hits { get; set; } = 0;

        public UrlModel() { }
        public UrlModel(string url)
        {
            this.OriginalUrl = url;
        }
    }

	public class UrlModelContext : DbContext
	{
        private static bool _created = false;
        public DbSet<UrlModel> UrlModel { get; set; }
        public UrlModelContext(DbContextOptions<UrlModelContext> options) : base(options)
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureCreated();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UrlModel>()
                .HasKey(u => u.Id);
        }

    }
}
