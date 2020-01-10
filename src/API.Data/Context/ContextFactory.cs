using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace API.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            //Usado para criar as migra
            //Conexao para MySQL
            //var connectionString = "Server=localhost;Port=3306;Database=dbApi;Uid=root;Pwd=W#k54*%#";
            var connectionString = "Server=localhost;Database=dbApi;User Id=sa;Pwd=W#k54*%#";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            //conexao para MySql
            //optionsBuilder.UseMySql(connectionString);
            optionsBuilder.UseSqlServer(connectionString);
            return new MyContext(optionsBuilder.Options);
        }
    }
}