using API.Data.Mapping;
using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace API.Data.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {
            //Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>(new UserMap().Configure);
        }
        public DbSet<UserEntity> Users { get; set; }
    }
}