using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ControleOficina.Models;

namespace Oficina.Data
{
    public class OficinaContext : DbContext
    {
        public OficinaContext (DbContextOptions<OficinaContext> options)
            : base(options)
        {
        }

        public DbSet<ControleOficina.Models.Carro> Carro { get; set; } = default!;
    }
}
