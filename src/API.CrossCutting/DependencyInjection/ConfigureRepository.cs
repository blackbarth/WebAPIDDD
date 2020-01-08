using API.Data.Context;
using API.Data.Repository;
using API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceColletion.AddDbContext<MyContext>(
                    options => options.UseMySql("Server=localhost;Port=3306;Database=dbApi;Uid=root;Pwd=W#k54*%#")
                );
        }
    }
}