using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Siapel.EF
{
    public class SiapelDbContextFactory : IDesignTimeDbContextFactory<SiapelDbContext>
    {
        
        public SiapelDbContext CreateDbContext(string[] args = null)
        {
            var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var options = new DbContextOptionsBuilder<SiapelDbContext>();
            var DbPath = Path.Join(path, "siapel.db");
            options.UseSqlite($"Data Source={DbPath}");

            return new SiapelDbContext(options.Options);
        }
    }
}
