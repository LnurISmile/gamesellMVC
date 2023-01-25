using gamesell.data.Configurations;
using gamesell.entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class PlayPointContext : DbContext
    {
        public PlayPointContext(DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<ActivationCountry> ACs { get; set; }
        public DbSet<CameraPerspective> CPs { get; set; }
        public DbSet<Currency> Curs { get; set; }
        public DbSet<Developer> Devs { get; set; }
        public DbSet<Divice> Divs { get; set; }
        public DbSet<GameCategory> GCs { get; set; }
        public DbSet<GameName> GNs { get; set; }
        public DbSet<Janra> Jans { get; set; }
        public DbSet<Language> Lans { get; set; }
        public DbSet<Platform> Plats { get; set; }
        public DbSet<Product> Pros { get; set; }
        public DbSet<Product_of_Gamer> POGs { get; set; }
        public DbSet<Publisher> Pubs { get; set; }
        public DbSet<PurchasedPOG> PPOGs { get; set; }
        public DbSet<PurchasedProduct> PPs { get; set; }
        public DbSet<CartP> Cartps { get; set; }
        public DbSet<CartPOG> Cartpogs { get; set; }
        public DbSet<CartItemP> CIs { get; set; }
        public DbSet<CartItemPOG> CIpogs { get; set; }
        public DbSet<GameItem> GIs { get; set; }
        public DbSet<LanguageText> LTs { get; set; }
        public DbSet<InstructionPanel> IPs { get; set; }
        public DbSet<IndexSlider> ISs { get; set; }
        public DbSet<Xboxdata> XDs { get; set; }
        public DbSet<Xboxgame> XGs { get; set; }
        public DbSet<PaymentPHistory> PPHs { get; set; }
        public DbSet<PaymentPOGHistory> PPOGHs { get; set; }
        public DbSet<BalanceInfo> Bs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}