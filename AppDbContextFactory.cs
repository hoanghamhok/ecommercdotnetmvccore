using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MYWEBAPI.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer("Server=YWUEMSMUCH\\SQLEXPRESS;Database=eC;User Id=kt;Password=123456;Trusted_Connection=True;TrustServerCertificate=True;");
        return new AppDbContext(optionsBuilder.Options);
    }
}
