using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TestTaskASPNETCore.Models;

namespace TestTaskASPNETCore.Database
{
    public class MyDbContext : DbContext
    {
        public DbSet<SchoolingActivityModel> SchoolingActivities { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
    }
}
