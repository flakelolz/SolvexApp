using Microsoft.EntityFrameworkCore;
using SolvexApp.Models;

namespace SolvexApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Character> Characters { get; set; }
    }
}
