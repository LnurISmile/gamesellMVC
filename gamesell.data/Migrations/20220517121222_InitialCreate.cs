using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace gamesell.data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivationCountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cartpogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartpogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cartps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CameraPerspevtiveName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Curs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyConst = table.Column<double>(type: "float", nullable: false),
                    CurrencyStringConst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeveloperName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Back_img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Divs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiviceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GCs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GCs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GIs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GNId = table.Column<int>(type: "int", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GIs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GNs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameOfName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GNs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImgTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product = table.Column<bool>(type: "bit", nullable: false),
                    POG = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ISs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndexSliderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ISs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JanraName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LTs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    LaguageId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LTs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlatformName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PPHs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PaymentBalance = table.Column<double>(type: "float", nullable: false),
                    IsXbox = table.Column<bool>(type: "bit", nullable: false),
                    BuyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PPHs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PPOGHs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIdSell = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserIdBuy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POGId = table.Column<int>(type: "int", nullable: false),
                    PaymentBalance = table.Column<double>(type: "float", nullable: false),
                    BuyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PPOGHs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PPOGs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POGId = table.Column<int>(type: "int", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PPOGs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsXbox = table.Column<bool>(type: "bit", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Back_img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "XDs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Img1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XDs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "XGs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XGs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "POGs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameNameID = table.Column<int>(type: "int", nullable: false),
                    DiviceID = table.Column<int>(type: "int", nullable: false),
                    GameItemID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Slider_videolink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slider_img1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slider_img2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slider_img3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Views = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsPOG = table.Column<bool>(type: "bit", nullable: false),
                    PurchasedPOGId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POGs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POGs_PPOGs_PurchasedPOGId",
                        column: x => x.PurchasedPOGId,
                        principalTable: "PPOGs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contenttype = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Company_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activation_zone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Onlineornot = table.Column<bool>(type: "bit", nullable: false),
                    Signleplayer = table.Column<bool>(type: "bit", nullable: false),
                    Multiplayer = table.Column<bool>(type: "bit", nullable: false),
                    Co_op = table.Column<bool>(type: "bit", nullable: false),
                    Type_active = table.Column<bool>(type: "bit", nullable: false),
                    twoD = table.Column<bool>(type: "bit", nullable: false),
                    threeD = table.Column<bool>(type: "bit", nullable: false),
                    VR = table.Column<bool>(type: "bit", nullable: false),
                    IndexSlider = table.Column<bool>(type: "bit", nullable: false),
                    Main_img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slider_videolink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slider_img1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slider_img2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slider_img3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount_percent = table.Column<int>(type: "int", nullable: false),
                    ConstNumber = table.Column<double>(type: "float", nullable: false),
                    Instock = table.Column<bool>(type: "bit", nullable: false),
                    Stocksize = table.Column<int>(type: "int", nullable: false),
                    Number_of_sale = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsProduct = table.Column<bool>(type: "bit", nullable: false),
                    UpComing = table.Column<bool>(type: "bit", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlatformID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    JanraID = table.Column<int>(type: "int", nullable: false),
                    CameraperspectiveID = table.Column<int>(type: "int", nullable: false),
                    PublisherID = table.Column<int>(type: "int", nullable: false),
                    DeveloperID = table.Column<int>(type: "int", nullable: false),
                    Activation_countryID = table.Column<int>(type: "int", nullable: false),
                    PurchasedProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pros_PPs_PurchasedProductId",
                        column: x => x.PurchasedProductId,
                        principalTable: "PPs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CIpogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    POGId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    CartpogId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CIpogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CIpogs_Cartpogs_CartpogId",
                        column: x => x.CartpogId,
                        principalTable: "Cartpogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CIpogs_POGs_POGId",
                        column: x => x.POGId,
                        principalTable: "POGs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CIs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    CartpId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CIs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CIs_Cartps_CartpId",
                        column: x => x.CartpId,
                        principalTable: "Cartps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CIs_Pros_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Pros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Curs",
                columns: new[] { "Id", "CurrencyConst", "CurrencyIcon", "CurrencyName", "CurrencyStringConst", "IsApproved", "LanguageTag" },
                values: new object[,]
                {
                    { 1, 1.0, "$", "USD", "1", true, "en" },
                    { 2, 1.7, "₼", "AZN", "1.7", true, "az" },
                    { 3, 15.199999999999999, "₺", "TL", "15.2", true, "tr" },
                    { 4, 68.579999999999998, "₽", "RUB", "68.58", true, "ru" }
                });

            migrationBuilder.InsertData(
                table: "Lans",
                columns: new[] { "Id", "IsApproved", "LanguageIcon", "LanguageName", "LanguageTag" },
                values: new object[,]
                {
                    { 1, true, null, "English", "en" },
                    { 2, true, null, "Azerbaycan", "az" },
                    { 3, true, null, "Türkce", "tr" },
                    { 4, true, null, "Русский", "ru" }
                });

            migrationBuilder.InsertData(
                table: "Plats",
                columns: new[] { "Id", "IsApproved", "Link", "PlatformName" },
                values: new object[,]
                {
                    { 5, true, "/guide/xbox", "Xbox" },
                    { 4, true, "/guide/uplay", "Uplay" },
                    { 2, true, "/guide/epic", "Epic Games" },
                    { 1, true, "/guide/steam", "Steam" },
                    { 3, true, "/guide/origin", "Origin" }
                });

            migrationBuilder.InsertData(
                table: "Pros",
                columns: new[] { "Id", "Activation_countryID", "Activation_zone", "CameraperspectiveID", "CategoryID", "Co_op", "Company_name", "ConstNumber", "Contenttype", "DeveloperID", "Discount_percent", "IndexSlider", "Instock", "IsApproved", "IsProduct", "JanraID", "Key", "Login", "Main_img", "Multiplayer", "Name", "Number_of_sale", "Onlineornot", "Password", "PlatformID", "Price", "PublisherID", "PurchasedProductId", "ReleaseDate", "Signleplayer", "Slider_img1", "Slider_img2", "Slider_img3", "Slider_videolink", "Stocksize", "Text", "Type_active", "UpComing", "Url", "VR", "threeD", "twoD" },
                values: new object[,]
                {
                    { 10, 0, null, 0, 0, false, null, 0.0, 0, 0, 0, false, false, true, true, 0, null, null, "Forspoken.jfif", false, "Forspoken", 0, false, null, 2, 4.5, 0, null, new DateTime(2022, 5, 17, 16, 12, 22, 180, DateTimeKind.Local).AddTicks(8565), false, null, null, null, null, 0, "tes78900", false, true, null, false, false, false },
                    { 15, 0, null, 0, 0, false, null, 0.0, 0, 0, 0, false, false, true, true, 0, null, null, "bm.jpg", false, "Black Mesa", 0, false, null, 1, 2.5, 0, null, new DateTime(2022, 5, 17, 16, 12, 22, 180, DateTimeKind.Local).AddTicks(8571), false, null, null, null, null, 0, "test7418524", false, false, null, false, false, false },
                    { 14, 0, null, 0, 0, false, null, 0.0, 0, 0, 0, false, false, true, true, 0, null, null, "apex.jpg", false, "Apex Legends", 0, false, null, 3, 3.0, 0, null, new DateTime(2022, 5, 17, 16, 12, 22, 180, DateTimeKind.Local).AddTicks(8570), false, null, null, null, null, 0, "test8523694", false, false, null, false, false, false },
                    { 13, 0, null, 0, 0, false, null, 0.0, 0, 0, 0, false, false, true, true, 0, null, null, "r6.webp", false, "TC's Rainbow Six Siege", 0, false, null, 4, 2.0, 0, null, new DateTime(2022, 5, 17, 16, 12, 22, 180, DateTimeKind.Local).AddTicks(8569), false, null, null, null, null, 0, "test798526", false, false, null, false, false, false },
                    { 12, 0, null, 0, 0, false, null, 0.0, 0, 0, 0, false, false, true, true, 0, null, null, "f5.jpg", false, "Forza Horizon 5", 0, false, null, 5, 5.5, 0, null, new DateTime(2022, 5, 17, 16, 12, 22, 180, DateTimeKind.Local).AddTicks(8568), false, null, null, null, null, 0, "test123456", false, false, null, false, false, false },
                    { 11, 0, null, 0, 0, false, null, 0.0, 0, 0, 0, false, false, true, true, 0, null, null, "SaintsRow.jfif", false, "Saints Row", 0, false, null, 4, 3.5, 0, null, new DateTime(2022, 5, 17, 16, 12, 22, 180, DateTimeKind.Local).AddTicks(8567), false, null, null, null, null, 0, "test564", false, true, null, false, false, false },
                    { 9, 0, null, 0, 0, false, null, 0.0, 0, 0, 0, false, false, true, true, 0, null, null, "Hogwarts_Legacy_cover.jpg", false, "Hogwarts Legacy", 0, false, null, 5, 6.5, 0, null, new DateTime(2022, 5, 17, 16, 12, 22, 180, DateTimeKind.Local).AddTicks(8564), false, null, null, null, null, 0, "testasd4das64a5sd", false, true, null, false, false, false },
                    { 2, 0, null, 0, 0, false, null, 0.0, 0, 0, 0, false, false, true, true, 0, null, null, "img_3d346b88-d5ec-41f7-ae95-6a487fa0b3a0.jpg", false, "Halo MasterChief Collection", 4, false, null, 5, 6.0, 0, null, new DateTime(2022, 5, 17, 16, 12, 22, 180, DateTimeKind.Local).AddTicks(8248), false, null, null, null, null, 0, "test212345", false, false, null, false, false, false },
                    { 7, 0, null, 0, 0, false, null, 0.0, 0, 0, 0, false, false, true, true, 0, null, null, "Csmqlce.jpg", false, "God of War Ragnarok", 0, false, null, 1, 8.0, 0, null, new DateTime(2022, 5, 17, 16, 12, 22, 180, DateTimeKind.Local).AddTicks(8559), false, null, null, null, null, 0, "testtetersssss", false, true, null, false, false, false },
                    { 6, 0, null, 0, 0, false, null, 0.0, 0, 0, 10, false, false, true, true, 0, null, null, "img_48908ff5-99e5-4234-b9c4-37797e80664e.jpg", false, "Cyberpunk 2077", 0, false, null, 1, 6.0, 0, null, new DateTime(2022, 5, 17, 16, 12, 22, 180, DateTimeKind.Local).AddTicks(8401), false, null, null, null, null, 0, "test4546321124", false, false, null, false, false, false },
                    { 5, 0, null, 0, 0, false, null, 0.0, 0, 0, 0, false, false, true, true, 0, null, null, "img_89ae6783-eb4a-47ac-b192-ce42ae710610.jpg", false, "CS-GO", 0, false, null, 1, 3.0, 0, null, new DateTime(2022, 5, 17, 16, 12, 22, 180, DateTimeKind.Local).AddTicks(8398), false, null, null, null, null, 0, "test55555", false, false, null, false, false, false },
                    { 4, 0, null, 0, 0, false, null, 0.0, 0, 0, 0, false, false, true, true, 0, null, null, "img_f0280d90-db9b-4844-a5c5-f08d4d435a67.jpeg", false, "Satisfactory", 0, false, null, 1, 4.0, 0, null, new DateTime(2022, 5, 17, 16, 12, 22, 180, DateTimeKind.Local).AddTicks(8266), false, null, null, null, null, 0, "test893544", false, false, null, false, false, false },
                    { 3, 0, null, 0, 0, false, null, 0.0, 0, 0, 0, false, false, true, true, 0, null, null, "mine.jpg", false, "Minecraft", 5, false, null, 5, 3.5, 0, null, new DateTime(2022, 5, 17, 16, 12, 22, 180, DateTimeKind.Local).AddTicks(8264), false, null, null, null, null, 0, "test3579514682", false, false, null, false, false, false },
                    { 16, 0, null, 0, 0, false, null, 0.0, 0, 0, 0, false, false, true, true, 0, null, null, "metro_exodus.jpg", false, "Metro Exodus", 0, false, null, 1, 4.5, 0, null, new DateTime(2022, 5, 17, 16, 12, 22, 180, DateTimeKind.Local).AddTicks(8572), false, null, null, null, null, 0, "test456123951", false, false, null, false, false, false },
                    { 1, 0, null, 0, 0, false, null, 0.0, 0, 0, 0, false, false, true, true, 0, null, null, "img_0ec4d3e8-8f98-4c68-8af3-b3672987ffcb.jpg", false, "Battlefront 2", 3, false, null, 3, 7.0, 0, null, new DateTime(2022, 5, 17, 16, 12, 22, 179, DateTimeKind.Local).AddTicks(9608), false, null, null, null, null, 0, "test14534538", false, false, null, false, false, false },
                    { 8, 0, null, 0, 0, false, null, 0.0, 0, 0, 0, false, false, true, true, 0, null, null, "starfield.jpg", false, "Starfield", 0, false, null, 5, 7.0, 0, null, new DateTime(2022, 5, 17, 16, 12, 22, 180, DateTimeKind.Local).AddTicks(8563), false, null, null, null, null, 0, "test41321535646", false, true, null, false, false, false }
                });

            migrationBuilder.InsertData(
                table: "XDs",
                columns: new[] { "Id", "Img1", "Img2", "Img3", "Img4", "Img5", "Login", "Password", "Price", "Title" },
                values: new object[] { 1, "img_1fcdfa1d-56d0-4912-b6c3-e262322d684f.jpg", "2.webp", "3.jpg", "4.jpg", "5.webp", null, null, 1.0, "Xbox" });

            migrationBuilder.CreateIndex(
                name: "IX_CIpogs_CartpogId",
                table: "CIpogs",
                column: "CartpogId");

            migrationBuilder.CreateIndex(
                name: "IX_CIpogs_POGId",
                table: "CIpogs",
                column: "POGId");

            migrationBuilder.CreateIndex(
                name: "IX_CIs_CartpId",
                table: "CIs",
                column: "CartpId");

            migrationBuilder.CreateIndex(
                name: "IX_CIs_ProductId",
                table: "CIs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_POGs_PurchasedPOGId",
                table: "POGs",
                column: "PurchasedPOGId");

            migrationBuilder.CreateIndex(
                name: "IX_Pros_PurchasedProductId",
                table: "Pros",
                column: "PurchasedProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACs");

            migrationBuilder.DropTable(
                name: "CIpogs");

            migrationBuilder.DropTable(
                name: "CIs");

            migrationBuilder.DropTable(
                name: "CPs");

            migrationBuilder.DropTable(
                name: "Curs");

            migrationBuilder.DropTable(
                name: "Devs");

            migrationBuilder.DropTable(
                name: "Divs");

            migrationBuilder.DropTable(
                name: "GCs");

            migrationBuilder.DropTable(
                name: "GIs");

            migrationBuilder.DropTable(
                name: "GNs");

            migrationBuilder.DropTable(
                name: "IPs");

            migrationBuilder.DropTable(
                name: "ISs");

            migrationBuilder.DropTable(
                name: "Jans");

            migrationBuilder.DropTable(
                name: "Lans");

            migrationBuilder.DropTable(
                name: "LTs");

            migrationBuilder.DropTable(
                name: "Plats");

            migrationBuilder.DropTable(
                name: "PPHs");

            migrationBuilder.DropTable(
                name: "PPOGHs");

            migrationBuilder.DropTable(
                name: "Pubs");

            migrationBuilder.DropTable(
                name: "XDs");

            migrationBuilder.DropTable(
                name: "XGs");

            migrationBuilder.DropTable(
                name: "Cartpogs");

            migrationBuilder.DropTable(
                name: "POGs");

            migrationBuilder.DropTable(
                name: "Cartps");

            migrationBuilder.DropTable(
                name: "Pros");

            migrationBuilder.DropTable(
                name: "PPOGs");

            migrationBuilder.DropTable(
                name: "PPs");
        }
    }
}
