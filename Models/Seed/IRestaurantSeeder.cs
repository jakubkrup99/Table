using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Table.DataAccess.Seed
{
    public interface IRestaurantSeeder
    {
        Task SeedAsync();
    }
}
