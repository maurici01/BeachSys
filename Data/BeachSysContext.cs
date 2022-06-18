using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeachSys.Models;

    public class BeachSysContext : DbContext
    {
        public BeachSysContext (DbContextOptions<BeachSysContext> options)
            : base(options)
        {
        }

        public DbSet<BeachSys.Models.Cadastro> Cadastro { get; set; }

        public DbSet<BeachSys.Models.Armario> Armario { get; set; }

        public DbSet<BeachSys.Models.Compartimento> Compartimento { get; set; }
    }
