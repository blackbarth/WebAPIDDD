using API.Data.Context;
using API.Data.Implementations;
using API.Data.Repository;
using API.Domain.Interfaces;
using API.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceColletion.AddScoped(typeof(IUserRepository), typeof(UserImplementation));
            //conexao com mysql
            //serviceColletion.AddDbContext<MyContext>(
            //        options => options.UseMySql("Server=localhost;Port=3306;Database=dbApi;Uid=root;Pwd=W#k54*%#")
            //    );

                        serviceColletion.AddDbContext<MyContext>(
                    options => options.UseSqlServer("Server=localhost;Database=dbApi;User Id=sa;Pwd=W#k54*%#")
                );
        }
    }
}