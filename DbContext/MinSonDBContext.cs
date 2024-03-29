﻿using Microsoft.EntityFrameworkCore;
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
           // Database.SetInitializer<>  (null)     SetInitializer<MinSonDBContext>(null);
        }
      //  public DbSet<Product> Products { get; set; }
        public DbSet<ShowcaseProduct> showcaseProducts { get; set; }
        public DbSet<ZeldaQuote> zeldaQuotes { get; set; }

        public DbSet<discordUser> discordUsers { get; set; }
        //  public DbSet<Card> cards { get; set; }
    }
}
