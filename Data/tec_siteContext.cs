using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

#nullable enable
namespace tec_site.Data
{
    public class tec_siteContext : DbContext
    {
        public tec_siteContext (DbContextOptions<tec_siteContext> options)
            : base(options)
        {
        }

    }
}
