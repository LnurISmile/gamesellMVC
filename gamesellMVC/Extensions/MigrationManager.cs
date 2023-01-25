using gamesell.data.Concrete.EfCore;
using gamesellMVC.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>())
                {
                    try
                    {
                        applicationContext.Database.Migrate();
                    }
                    catch (System.Exception)
                    {

                        throw;
                    }
                }

                using (var gsContext = scope.ServiceProvider.GetRequiredService<PlayPointContext>())
                {
                    try
                    {
                        gsContext.Database.Migrate();
                    }
                    catch (System.Exception)
                    {

                        throw;
                    }
                }
            }

            return host;
        }
    }
}
