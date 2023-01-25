using gamesell.business.Abstract;
using gamesell.business.Concrete;
using gamesell.data.Abstract;
using gamesell.data.Concrete.EfCore;
using gamesellMVC.EmailServices;
using gamesellMVC.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization.Routing;

namespace gamesellMVC
{
    public class Startup
    {
        public IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(_configuration.GetConnectionString("MsSqlConnection")));
            services.AddDbContext<PlayPointContext>(options => options.UseSqlServer(_configuration.GetConnectionString("MsSqlConnection")));

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(365);
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".PlayPoint.Security.Cookie",
                    SameSite = SameSiteMode.Lax
                };
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IActivationCountryService, ActivationCountryManager>();
            services.AddScoped<ICameraPerspectiveServices, CameraPerspectiveManager>();
            services.AddScoped<ICurrencyService, CurrencyManager>();
            services.AddScoped<IDeveloperService, DeveloperManager>();
            services.AddScoped<IDiviceService, DiviceManager>();
            services.AddScoped<IGameCategoryService, GameCategoryManager>();
            services.AddScoped<IGameNameService, GameNameManager>();
            services.AddScoped<IJanraService, JanraManager>();
            services.AddScoped<ILanguageService, LanguageManager>();
            services.AddScoped<IPlatformService, PlatformManager>();
            services.AddScoped<IProductOfGamerService, ProductOfGamerManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IPublisherService, PublisherManager>();
            services.AddScoped<IPurchasedPOGService, PurchasedPOGManager>();
            services.AddScoped<IPurchasedProductService, PurchasedProductManager>();
            services.AddScoped<ICartPService, CartPManager>();
            services.AddScoped<ICartPOGService, CartPOGManager>();
            services.AddScoped<ICIPService, CIPManager>();
            services.AddScoped<ICIPOGService, CIPOGManager>();
            services.AddScoped<IGameItemService, GameItemManager>();
            services.AddScoped<ILanguageTextService, LanguageTextManager>();
            services.AddScoped<IInstructionPanelService, InstructionPanelManager>();
            services.AddScoped<IIndexSliderService, IndexSliderManager>();
            services.AddScoped<IXboxdataService, XboxdataManager>();
            services.AddScoped<IXboxgameService, XboxgameManager>();
            services.AddScoped<IPaymentPHistoryService, PaymentPHistoryManager>();
            services.AddScoped<IPaymentPOGHistoryService, PaymentPOGHistoryManager>();
            services.AddScoped<IBalanceService, BalanceManager>();

            services.AddScoped<IEmailSender, SmtpEmailSender>(i =>
                        new SmtpEmailSender(
                            _configuration["EmailSender:Host"],
                            _configuration.GetValue<int>("EmailSender:Port"),
                            _configuration.GetValue<bool>("EmailSender:EnableSSL"),
                            _configuration["EmailSender:UserName"],
                            _configuration["EmailSender:Password"]
                            ));

            services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(opt =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo ("ar"),
                    new CultureInfo ("az"),
                    new CultureInfo ("de"),
                    new CultureInfo ("en"),
                    new CultureInfo ("es"),
                    new CultureInfo ("fr"),
                    new CultureInfo ("ja"),
                    new CultureInfo ("ru"),
                    new CultureInfo ("tr"),
                    new CultureInfo ("zh")
                };

                opt.DefaultRequestCulture = new RequestCulture("en");
                opt.SupportedCultures = supportedCultures;
                opt.SupportedUICultures = supportedCultures;

                opt.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider(),
                    new AcceptLanguageHeaderRequestCultureProvider()
                };
            });
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration,
                              UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseForwardedHeaders();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseForwardedHeaders();
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                /////////////////////////////////////////////////// AccountController //
                endpoints.MapControllerRoute(
                    name: "cart",
                    pattern: "cart/product",
                    defaults: new { controller = "Cart", action = "CartProduct" }
                    );

                /////////////////////////////////////////////////// AccountController //
                endpoints.MapControllerRoute(
                   name: "alreadyuseemail",
                   pattern: "register/error/email",
                   defaults: new { controller = "Account", action = "AlreadyUseEmail" }
                   );

                endpoints.MapControllerRoute(
                  name: "alreadyuseusername",
                  pattern: "register/error/username",
                  defaults: new { controller = "Account", action = "AlreadyUseUsername" }
                  );

                endpoints.MapControllerRoute(
                   name: "alreadyconfirm",
                   pattern: "account/email/confirm/info",
                   defaults: new { controller = "Account", action = "AlreadyConfirm" }
                   );

                endpoints.MapControllerRoute(
                   name: "forgotpassword",
                   pattern: "account/info/forgotpassword",
                   defaults: new { controller = "Account", action = "EmailCheck" }
                   );

                endpoints.MapControllerRoute(
                    name: "notfound",
                    pattern: "account/notfound",
                    defaults: new { controller = "Account", action = "NoAccount" }
                    );

                endpoints.MapControllerRoute(
                    name: "pleaseconfirm",
                    pattern: "account/pleaseconfirm",
                    defaults: new { controller = "Account", action = "PleaseConfirm" }
                    );

                endpoints.MapControllerRoute(
                    name: "payment",
                    pattern: "payment",
                    defaults: new { controller = "Account", action = "Payment" }
                    );

                endpoints.MapControllerRoute(
                    name: "bmsf",
                    pattern: "VzgdV7Lh22RaYmxgO6nrbnCPc",
                    defaults: new { controller = "Account", action = "VzgdV7Lh22RaYmxgO6nrbnCPc" }
                    );
                endpoints.MapControllerRoute(
                    name: "balanceinfosuccessf",
                    pattern: "methodbs",
                    defaults: new { controller = "Account", action = "SuccessF" }
                    );
                endpoints.MapControllerRoute(
                    name: "balanceinfosuccess",
                    pattern: "balance/info/success",
                    defaults: new { controller = "Account", action = "Success" }
                    );

                endpoints.MapControllerRoute(
                   name: "bmef",
                   pattern: "RywzGJlXvg4D2UuWqknGsGV8r",
                   defaults: new { controller = "Account", action = "RywzGJlXvg4D2UuWqknGsGV8r" }
                   );
                endpoints.MapControllerRoute(
                   name: "balanceinfoerrorf",
                   pattern: "methodbe",
                   defaults: new { controller = "Account", action = "ErrorF" }
                   );
                endpoints.MapControllerRoute(
                   name: "balanceinfoerror",
                   pattern: "balance/info/error",
                   defaults: new { controller = "Account", action = "Error" }
                   );
                endpoints.MapControllerRoute(
                   name: "balanceinfo",
                   pattern: "balance/info",
                   defaults: new { controller = "Account", action = "Result" }
                   );

                endpoints.MapControllerRoute(
                    name: "about",
                    pattern: "about",
                    defaults: new { controller = "Account", action = "About" }
                    );

                endpoints.MapControllerRoute(
                    name: "policy",
                    pattern: "policy",
                    defaults: new { controller = "Account", action = "Contract" }
                    );

                endpoints.MapControllerRoute(
                    name: "accessdenied",
                    pattern: "accessdenied",
                    defaults: new { controller = "Account", action = "AccessDenied" }
                    );

                endpoints.MapControllerRoute(
                    name: "accountregister",
                    pattern: "account/register",
                    defaults: new { controller = "Account", action = "Register" }
                    );

                endpoints.MapControllerRoute(
                    name: "accountlogin",
                    pattern: "account/login",
                    defaults: new { controller = "Account", action = "Login" }
                    );

                endpoints.MapControllerRoute(
                    name: "accountlogout",
                    pattern: "account/logout",
                    defaults: new { controller = "Account", action = "Logout" }
                    );

                endpoints.MapControllerRoute(
                    name: "accountmanage",
                    pattern: "account/manage",
                    defaults: new { controller = "Account", action = "UserManage" }
                    );

                endpoints.MapControllerRoute(
                    name: "accountmanage",
                    pattern: "account/addbalance",
                    defaults: new { controller = "Account", action = "AddBalance" }
                    );

                endpoints.MapControllerRoute(
                    name: "xboxpurchased",
                    pattern: "xbox/purchased",
                    defaults: new { controller = "Account", action = "XboxPurchased" }
                    );

                endpoints.MapControllerRoute(
                    name: "accountpogedit",
                    pattern: "account/edit/pog/{id?}",
                    defaults: new { controller = "Account", action = "EditPOG" }
                    );

                endpoints.MapControllerRoute(
                    name: "accountpogcreate",
                    pattern: "account/create/pog",
                    defaults: new { controller = "Account", action = "CreatePOG" }
                    );

                endpoints.MapControllerRoute(
                    name: "accountlibraryofsell",
                    pattern: "account/libraryofsell",
                    defaults: new { controller = "Product", action = "ComingSoon" } //comingsoon Account LibraryOfSell
                    );

                //endpoints.MapControllerRoute(
                //    name: "accountlibrarypog",
                //    pattern: "account/librarypog",
                //    defaults: new { controller = "Account", action = "LibraryPOG" }
                //    );

                endpoints.MapControllerRoute(
                    name: "accountlibrary",
                    pattern: "account/library",
                    defaults: new { controller = "Account", action = "Library" }
                    );

                /////////////////////////////////////////////////// AdminController //
                endpoints.MapControllerRoute(
                    name: "admineditrole",
                    pattern: "adminpanel/editrole/{id?}",
                    defaults: new { controller = "Admin", action = "EditRole" }
                    );
                endpoints.MapControllerRoute(
                    name: "admincreaterole",
                    pattern: "adminpanel/createrole",
                    defaults: new { controller = "Admin", action = "CreateRole" }
                    );
                endpoints.MapControllerRoute(
                    name: "adminrolelist",
                    pattern: "adminpanel/rolelist",
                    defaults: new { controller = "Admin", action = "RoleList" }
                    );


                endpoints.MapControllerRoute(
                    name: "adminedituser",
                    pattern: "adminpanel/edituser/{id?}",
                    defaults: new { controller = "Admin", action = "EditUser" }
                    );
                endpoints.MapControllerRoute(
                    name: "admincreateuser",
                    pattern: "adminpanel/createuser",
                    defaults: new { controller = "Admin", action = "CreateUser" }
                    );
                endpoints.MapControllerRoute(
                    name: "adminalluserlist",
                    pattern: "adminpanel/userlist",
                    defaults: new { controller = "Admin", action = "UserList" }
                    );


                endpoints.MapControllerRoute(
                 name: "adminpurchasedproductedit",
                 pattern: "adminpanel/purchasedproductedit/{id?}",
                 defaults: new { controller = "Admin", action = "PurchasedProductEdit" }
                 );
                endpoints.MapControllerRoute(
                   name: "adminpurchasedproductcreate",
                   pattern: "adminpanel/purchasedproductcreate",
                   defaults: new { controller = "Admin", action = "PurchasedProductCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminpurchasedproductlist",
                   pattern: "adminpanel/purchasedproductlist",
                   defaults: new { controller = "Admin", action = "PurchasedProductList" }
                   );


                endpoints.MapControllerRoute(
                  name: "adminpurchasedpogedit",
                  pattern: "adminpanel/purchasedpogedit/{id?}",
                  defaults: new { controller = "Admin", action = "PurchasedPOGEdit" }
                  );
                endpoints.MapControllerRoute(
                   name: "adminpurchasedpogcreate",
                   pattern: "adminpanel/purchasedpogcreate",
                   defaults: new { controller = "Admin", action = "PurchasedPOGCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminpurchasedpoglist",
                   pattern: "adminpanel/purchasedpoglist",
                   defaults: new { controller = "Admin", action = "PurchasedPOGList" }
                   );


                endpoints.MapControllerRoute(
                   name: "adminpublisheredit",
                   pattern: "adminpanel/publisheredit/{id?}",
                   defaults: new { controller = "Admin", action = "PublisherEdit" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminpublishercreate",
                   pattern: "adminpanel/publishercreate",
                   defaults: new { controller = "Admin", action = "PublisherCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminpublisherlist",
                   pattern: "adminpanel/publisherlist",
                   defaults: new { controller = "Admin", action = "PublisherList" }
                   );


                endpoints.MapControllerRoute(
                   name: "adminproductedit",
                   pattern: "adminpanel/productedit/{id?}",
                   defaults: new { controller = "Admin", action = "ProductEdit" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminproductcreate",
                   pattern: "adminpanel/productcreate",
                   defaults: new { controller = "Admin", action = "ProductCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminproductlist",
                   pattern: "adminpanel/productlist",
                   defaults: new { controller = "Admin", action = "ProductList" }
                   );


                endpoints.MapControllerRoute(
                   name: "adminproductofgameredit",
                   pattern: "adminpanel/productofgameredit/{id?}",
                   defaults: new { controller = "Admin", action = "ProductOfGamerEdit" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminproductofgamercreate",
                   pattern: "adminpanel/productofgamercreate",
                   defaults: new { controller = "Admin", action = "ProductOfGamerCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminproductofgamerlist",
                   pattern: "adminpanel/productofgamerlist",
                   defaults: new { controller = "Admin", action = "ProductOfGamerList" }
                   );


                endpoints.MapControllerRoute(
                   name: "adminplatformedit",
                   pattern: "adminpanel/platformedit/{id?}",
                   defaults: new { controller = "Admin", action = "PlatformEdit" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminplatformcreate",
                   pattern: "adminpanel/platformcreate",
                   defaults: new { controller = "Admin", action = "PlatformCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminplatformlist",
                   pattern: "adminpanel/platformlist",
                   defaults: new { controller = "Admin", action = "PlatformList" }
                   );


                endpoints.MapControllerRoute(
                   name: "adminlanguageedit",
                   pattern: "adminpanel/languageedit/{id?}",
                   defaults: new { controller = "Admin", action = "LanguageEdit" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminlanguagecreate",
                   pattern: "adminpanel/languagecreate",
                   defaults: new { controller = "Admin", action = "LanguageCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminlanguagelist",
                   pattern: "adminpanel/languagelist",
                   defaults: new { controller = "Admin", action = "LanguageList" }
                   );


                endpoints.MapControllerRoute(
                   name: "adminjanraedit",
                   pattern: "adminpanel/janraedit/{id?}",
                   defaults: new { controller = "Admin", action = "JanraEdit" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminjanracreate",
                   pattern: "adminpanel/janracreate",
                   defaults: new { controller = "Admin", action = "JanraCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminjanralist",
                   pattern: "adminpanel/janralist",
                   defaults: new { controller = "Admin", action = "JanraList" }
                   );


                endpoints.MapControllerRoute(
                   name: "admingamenameedit",
                   pattern: "adminpanel/gamenameedit/{id?}",
                   defaults: new { controller = "Admin", action = "GameNameEdit" }
                   );
                endpoints.MapControllerRoute(
                   name: "admingamenamecreate",
                   pattern: "adminpanel/gamenamecreate",
                   defaults: new { controller = "Admin", action = "GameNameCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "admingamenamelist",
                   pattern: "adminpanel/gamenamelist",
                   defaults: new { controller = "Admin", action = "GameNameList" }
                   );


                endpoints.MapControllerRoute(
                   name: "admingamecategoryedit",
                   pattern: "adminpanel/gamecategoryedit/{id?}",
                   defaults: new { controller = "Admin", action = "GameCategoryEdit" }
                   );
                endpoints.MapControllerRoute(
                   name: "admingamecategorycreate",
                   pattern: "adminpanel/gamecategorycreate",
                   defaults: new { controller = "Admin", action = "GameCategoryCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "admingamecategorylist",
                   pattern: "adminpanel/gamecategorylist",
                   defaults: new { controller = "Admin", action = "GameCategoryList" }
                   );


                endpoints.MapControllerRoute(
                   name: "admindiviceedit",
                   pattern: "adminpanel/diviceedit/{id?}",
                   defaults: new { controller = "Admin", action = "DiviceEdit" }
                   );
                endpoints.MapControllerRoute(
                   name: "admindivicecreate",
                   pattern: "adminpanel/divicecreate",
                   defaults: new { controller = "Admin", action = "DiviceCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "admindivicelist",
                   pattern: "adminpanel/divicelist",
                   defaults: new { controller = "Admin", action = "DiviceList" }
                   );


                endpoints.MapControllerRoute(
                   name: "admindeveloperedit",
                   pattern: "adminpanel/developeredit/{id?}",
                   defaults: new { controller = "Admin", action = "DeveloperEdit" }
                   );
                endpoints.MapControllerRoute(
                   name: "admindevelopercreate",
                   pattern: "adminpanel/developercreate",
                   defaults: new { controller = "Admin", action = "DeveloperCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "admindeveloperlist",
                   pattern: "adminpanel/developerlist",
                   defaults: new { controller = "Admin", action = "DeveloperList" }
                   );


                endpoints.MapControllerRoute(
                   name: "admincurrencyedit",
                   pattern: "adminpanel/currencyedit/{id?}",
                   defaults: new { controller = "Admin", action = "CurrencyEdit" }
                   );
                endpoints.MapControllerRoute(
                   name: "admincurrencycreate",
                   pattern: "adminpanel/currencycreate",
                   defaults: new { controller = "Admin", action = "CurrencyCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "admincurrencylist",
                   pattern: "adminpanel/currencylist",
                   defaults: new { controller = "Admin", action = "CurrencyList" }
                   );


                endpoints.MapControllerRoute(
                   name: "admincameraperspectivecreate",
                   pattern: "adminpanel/cameraperspectivecreate",
                   defaults: new { controller = "Admin", action = "CameraPerspectiveCreate" }
                   );
                endpoints.MapControllerRoute(
                  name: "admincameraperspectiveedit",
                  pattern: "adminpanel/cameraperspectiveedit/{id?}",
                  defaults: new { controller = "Admin", action = "CameraPerspectiveEdit" }
                  );
                endpoints.MapControllerRoute(
                   name: "admincameraperspectivelist",
                   pattern: "adminpanel/cameraperspectivelist",
                   defaults: new { controller = "Admin", action = "CameraPerspectiveList" }
                   );


                endpoints.MapControllerRoute(
                   name: "adminactivationcountrycreate",
                   pattern: "adminpanel/activationcountrycreate",
                   defaults: new { controller = "Admin", action = "ActivationCountryCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminactivationcountryedit",
                   pattern: "adminpanel/activationcountryedit/{id?}",
                   defaults: new { controller = "Admin", action = "ActivationCountryEdit" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminactivationcountrylist",
                   pattern: "adminpanel/activationcountrylist",
                   defaults: new { controller = "Admin", action = "ActivationCountryList" }
                   );

                endpoints.MapControllerRoute(
                   name: "admingameitemcreate",
                   pattern: "adminpanel/gameitemcreate",
                   defaults: new { controller = "Admin", action = "GameItemCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "admingameitemedit",
                   pattern: "adminpanel/gameitemedit/{id?}",
                   defaults: new { controller = "Admin", action = "GameItemEdit" }
                   );
                endpoints.MapControllerRoute(
                   name: "admingameitemlist",
                   pattern: "adminpanel/gameitemlist",
                   defaults: new { controller = "Admin", action = "GameItemList" }
                   );

                endpoints.MapControllerRoute(
                   name: "adminlanguagetextcreate",
                   pattern: "adminpanel/languagetextcreate",
                   defaults: new { controller = "Admin", action = "LanguageTextCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminlanguagetextedit",
                   pattern: "adminpanel/languagetextedit/{id?}",
                   defaults: new { controller = "Admin", action = "LanguageTextEdit" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminlanguagetextlist",
                   pattern: "adminpanel/languagetextlist",
                   defaults: new { controller = "Admin", action = "LanguageTextList" }
                   );

                endpoints.MapControllerRoute(
                   name: "admininstructionpanelcreate",
                   pattern: "adminpanel/instructionpanelcreate",
                   defaults: new { controller = "Admin", action = "InstructionPanelCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminactivationcountryedit",
                   pattern: "adminpanel/instructionpaneledit/{id?}",
                   defaults: new { controller = "Admin", action = "InstructionPanelEdit" }
                   );
                endpoints.MapControllerRoute(
                   name: "admininstructionpanellist",
                   pattern: "adminpanel/instructionpanellist",
                   defaults: new { controller = "Admin", action = "InstructionPanelList" }
                   );

                endpoints.MapControllerRoute(
                   name: "adminactivationcountrycreate",
                   pattern: "adminpanel/indexslidercreate",
                   defaults: new { controller = "Admin", action = "IndexSliderCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminindexslideredit",
                   pattern: "adminpanel/indexslideredit/{id?}",
                   defaults: new { controller = "Admin", action = "IndexSliderEdit" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminindexsliderlist",
                   pattern: "adminpanel/indexsliderlist",
                   defaults: new { controller = "Admin", action = "IndexSliderList" }
                   );

                endpoints.MapControllerRoute(
                   name: "adminxboxdataedit",
                   pattern: "adminpanel/xboxdataedit/{id?}",
                   defaults: new { controller = "Admin", action = "XboxdataEdit" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminxboxdatacreate",
                   pattern: "adminpanel/xboxdatacreate",
                   defaults: new { controller = "Admin", action = "XboxdataCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminxboxdatalist",
                   pattern: "adminpanel/xboxdatalist",
                   defaults: new { controller = "Admin", action = "XboxdataList" }
                   );

                endpoints.MapControllerRoute(
                   name: "adminxboxgameedit",
                   pattern: "adminpanel/xboxgameedit/{id?}",
                   defaults: new { controller = "Admin", action = "XboxgameEdit" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminxboxgamecreate",
                   pattern: "adminpanel/xboxgamecreate",
                   defaults: new { controller = "Admin", action = "XboxgameCreate" }
                   );
                endpoints.MapControllerRoute(
                   name: "adminxboxgamelist",
                   pattern: "adminpanel/xboxgamelist",
                   defaults: new { controller = "Admin", action = "XboxgameList" }
                   );

                endpoints.MapControllerRoute(
                   name: "adminpphlist",
                   pattern: "adminpanel/pphlist",
                   defaults: new { controller = "Admin", action = "PaymentPHistoryList" }
                   );

                endpoints.MapControllerRoute(
                   name: "adminppoghlist",
                   pattern: "adminpanel/ppoghlist",
                   defaults: new { controller = "Admin", action = "PaymentPOGHistoryList" }
                   );


                endpoints.MapControllerRoute(
                    name: "adminepanelhome",
                    pattern: "adminpanel/home",
                    defaults: new { controller = "Admin", action = "Home" }
                    );

                /////////////////////////////////////////////////// ProductController //
                endpoints.MapControllerRoute(
                    name: "comingsoon",
                    pattern: "comingsoon",
                    defaults: new { controller = "Product", action = "ComingSoon" }
                    );

                endpoints.MapControllerRoute(
                    name: "gepic",
                    pattern: "guide/epic",
                    defaults: new { controller = "Product", action = "GEpic" }
                    );
                endpoints.MapControllerRoute(
                    name: "gorigin",
                    pattern: "guide/origin",
                    defaults: new { controller = "Product", action = "GOrigin" }
                    );
                endpoints.MapControllerRoute(
                    name: "gsteamwrockstar",
                    pattern: "guide/steamwrockstar",
                    defaults: new { controller = "Product", action = "GSteamWRockstar" }
                    );
                endpoints.MapControllerRoute(
                    name: "gsteam",
                    pattern: "guide/steam",
                    defaults: new { controller = "Product", action = "GSteam" }
                    );
                endpoints.MapControllerRoute(
                    name: "guplay",
                    pattern: "guide/uplay",
                    defaults: new { controller = "Product", action = "GUplay" }
                    );
                endpoints.MapControllerRoute(
                    name: "gxbox",
                    pattern: "guide/xbox",
                    defaults: new { controller = "Product", action = "GXbox" }
                    );

                endpoints.MapControllerRoute(
                    name: "searchresultugp",
                    pattern: "product/pog/searchresult",
                    defaults: new { controller = "Product", action = "POGSearch" }
                    );

                endpoints.MapControllerRoute(
                    name: "searchresult",
                    pattern: "product/searchresult",
                    defaults: new { controller = "Product", action = "ProductSearch" }
                    );

                endpoints.MapControllerRoute(
                    name: "gameprofilesdetails",
                    pattern: "product/pog/details/{id?}",
                    defaults: new { controller = "Product", action = "POGDetails" }
                    );

                endpoints.MapControllerRoute(
                    name: "gameprofilesdetails",
                    pattern: "product/details/{id?}/{url?}",
                    defaults: new { controller = "Product", action = "ProductDetails" }
                    );

                endpoints.MapControllerRoute(
                    name: "usergameprofiles",
                    pattern: "product/product_of_gamer/list",
                    defaults: new { controller = "Product", action = "ComingSoon" } //comingsoon product_of_gamer
                    );

                endpoints.MapControllerRoute(
                    name: "xboxxboxgames",
                    pattern: "xbox/xboxgames",
                    defaults: new { controller = "Product", action = "Xbox" }
                    );

                endpoints.MapControllerRoute(
                    name: "gameprofiles",
                    pattern: "product/list",
                    defaults: new { controller = "Product", action = "ProductList" }
                    );

                endpoints.MapControllerRoute(
                    name: "gameprofiles",
                    pattern: "home/index",
                    defaults: new { controller = "Home", action = "Index" }
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=index}/{id?}"
                    );
            });

            SeedIdendity.Seed(userManager, roleManager, configuration).Wait();
        }
    }
}
