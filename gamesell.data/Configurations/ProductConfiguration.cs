using gamesell.entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product() { Id = 1, Name = "Battlefront 2", Url = "battlefron_2", Text = "test11", Price = 30, IsApproved = true },
                new Product() { Id = 2, Name = "Halo MasterChief Collection", Url = "halo_masterchief_collection", Text = "test22", Price = 40, IsApproved = true },
                new Product() { Id = 3, Name = "PUBG", Url = "pubg", Text = "test333", Price = 35, IsApproved = true },
                new Product() { Id = 4, Name = "Satisfactory", Url = "satisfactory", Text = "test4444", Price = 25, IsApproved = true },
                new Product() { Id = 5, Name = "CS-GO", Url = "cs-go", Text = "test55555", Price = 35, IsApproved = false }
            );
        }
    }
}
