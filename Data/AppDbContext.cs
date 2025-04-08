using Microsoft.EntityFrameworkCore;
using MYWEBAPI.Models;
using Microsoft.Net.Http.Headers;

namespace MYWEBAPI.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){

        }
        public DbSet<User> Users => Set<User>();
    }
}