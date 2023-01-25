using gamesell.entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Configurations
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(
                new Product() { Id = 1, Name = "Battlefront 2", Text = "test14534538", Price = 7, IsApproved = true, Main_img = "img_0ec4d3e8-8f98-4c68-8af3-b3672987ffcb.jpg", Number_of_sale = 3, PlatformID = 3 },
                new Product() { Id = 2, Name = "Halo MasterChief Collection", Text = "test212345", Price = 6, IsApproved = true, Main_img = "img_3d346b88-d5ec-41f7-ae95-6a487fa0b3a0.jpg", Number_of_sale = 4, PlatformID = 5 },
                new Product() { Id = 3, Name = "Minecraft", Text = "test3579514682", Price = 3.5, IsApproved = true, Main_img = "mine.jpg", PlatformID = 5, Number_of_sale = 5 },
                new Product() { Id = 4, Name = "Satisfactory", Text = "test893544", Price = 4, IsApproved = true, Main_img = "img_f0280d90-db9b-4844-a5c5-f08d4d435a67.jpeg", PlatformID = 1, UpComing = false },
                new Product() { Id = 5, Name = "CS-GO", Text = "test55555", Price = 3, IsApproved = true, Main_img = "img_89ae6783-eb4a-47ac-b192-ce42ae710610.jpg", PlatformID = 1, UpComing = false },
                new Product() { Id = 6, Name = "Cyberpunk 2077", Text = "test4546321124", Price = 6, IsApproved = true, Main_img = "img_48908ff5-99e5-4234-b9c4-37797e80664e.jpg", Discount_percent = 10, PlatformID = 1 },
                new Product() { Id = 7, Name = "God of War Ragnarok", Text = "testtetersssss", Price = 8, IsApproved = true, Main_img = "Csmqlce.jpg", UpComing = true, PlatformID = 1 },
                new Product() { Id = 8, Name = "Starfield", Text = "test41321535646", Price = 7, IsApproved = true, Main_img = "starfield.jpg", UpComing = true, PlatformID = 5 },
                new Product() { Id = 9, Name = "Hogwarts Legacy", Text = "testasd4das64a5sd", Price = 6.5, IsApproved = true, Main_img = "Hogwarts_Legacy_cover.jpg", UpComing = true, PlatformID = 5 },
                new Product() { Id = 10, Name = "Forspoken", Text = "tes78900", Price = 4.5, IsApproved = true, Main_img = "Forspoken.jfif", UpComing = true, PlatformID = 2 },
                new Product() { Id = 11, Name = "Saints Row", Text = "test564", Price = 3.5, IsApproved = true, Main_img = "SaintsRow.jfif", UpComing = true, PlatformID = 4 },
                new Product() { Id = 12, Name = "Forza Horizon 5", Text = "test123456", Price = 5.5, IsApproved = true, Main_img = "f5.jpg", PlatformID = 5 },
                new Product() { Id = 13, Name = "TC's Rainbow Six Siege", Text = "test798526", Price = 2, IsApproved = true, Main_img = "r6.webp", PlatformID = 4 },
                new Product() { Id = 14, Name = "Apex Legends", Text = "test8523694", Price = 3, IsApproved = true, Main_img = "apex.jpg", PlatformID = 3 },
                new Product() { Id = 15, Name = "Black Mesa", Text = "test7418524", Price = 2.5, IsApproved = true, Main_img = "bm.jpg", PlatformID = 1 },
                new Product() { Id = 16, Name = "Metro Exodus", Text = "test456123951", Price = 4.5, IsApproved = true, Main_img = "metro_exodus.jpg", PlatformID = 1 }
             );

            builder.Entity<Platform>().HasData(
                new Platform() { Id = 1, PlatformName = "Steam", Link = "/guide/steam", IsApproved = true },
                new Platform() { Id = 2, PlatformName = "Epic Games", Link = "/guide/epic", IsApproved = true },
                new Platform() { Id = 3, PlatformName = "Origin", Link = "/guide/origin", IsApproved = true },
                new Platform() { Id = 4, PlatformName = "Uplay", Link = "/guide/uplay", IsApproved = true },
                new Platform() { Id = 5, PlatformName = "Xbox", Link = "/guide/xbox", IsApproved = true }
                );

            builder.Entity<Language>().HasData(
                new Language() { Id = 1, LanguageName = "English", LanguageTag = "en", IsApproved = true },
                new Language() { Id = 2, LanguageName = "Azerbaycan", LanguageTag = "az", IsApproved = true },
                new Language() { Id = 3, LanguageName = "Türkce", LanguageTag = "tr", IsApproved = true },
                new Language() { Id = 4, LanguageName = "Русский", LanguageTag = "ru", IsApproved = true }
                );

            builder.Entity<Currency>().HasData(
                new Currency() { Id = 1, LanguageTag = "en", CurrencyName = "USD", CurrencyConst = 1, CurrencyStringConst = "1", IsApproved = true, CurrencyIcon = "$" },
                new Currency() { Id = 2, LanguageTag = "az", CurrencyName = "AZN", CurrencyConst = 1.7, CurrencyStringConst = "1.7", IsApproved = true, CurrencyIcon = "₼" },
                new Currency() { Id = 3, LanguageTag = "tr", CurrencyName = "TL", CurrencyConst = 15.2, CurrencyStringConst = "15.2", IsApproved = true, CurrencyIcon = "₺" },
                new Currency() { Id = 4, LanguageTag = "ru", CurrencyName = "RUB", CurrencyConst = 68.58, CurrencyStringConst = "68.58", IsApproved = true, CurrencyIcon = "₽" }
                );

            builder.Entity<Xboxdata>().HasData(
                new Xboxdata() { Id = 1, Title = "Xbox", Price = 1, Img1 = "img_1fcdfa1d-56d0-4912-b6c3-e262322d684f.jpg",
                                Img2 = "2.webp", Img3 = "3.jpg", Img4 = "4.jpg", Img5 = "5.webp"}
                );
        }
    }
}