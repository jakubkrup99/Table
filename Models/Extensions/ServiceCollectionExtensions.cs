using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Table.DataAccess.Db;
using Table.DataAccess.Repositories.UnitOfWork;
using Table.DataAccess.Seed;

namespace Table.DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();

        }
    }
}
