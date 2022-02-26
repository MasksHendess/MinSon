using Microsoft.EntityFrameworkCore;
using MinSon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinSon
{
    public class MinSonDBContext : DbContext
    {
        public MinSonDBContext(DbContextOptions<MinSonDBContext> options) : base(options)
        {

        }
        public DbSet<Card> cards { get; set; }
    }
}
