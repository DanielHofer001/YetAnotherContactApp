using Microsoft.EntityFrameworkCore;
using YetAnotherContactApp.Models;

namespace YetAnotherContactApi.DTOs.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
