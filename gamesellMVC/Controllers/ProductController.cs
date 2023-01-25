using gamesell.business.Abstract;
using gamesell.data.Concrete.EfCore;
using gamesell.entity;
using gamesellMVC.EmailServices;
using gamesellMVC.Extensions;
using gamesellMVC.Identity;
using gamesellMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Controllers
{
    public class ProductController : Controller
    {
        private IActivationCountryService _acService;
        private ICameraPerspectiveServices _cpService;
        private ICurrencyService _curService;
        private IDeveloperService _devService;
        private IDiviceService _divService;
        private IGameCategoryService _gcService;
        private IGameNameService _gnService;
        private IJanraService _janService;
        private ILanguageService _lanService;
        private IPlatformService _platService;
        private IProductOfGamerService _pogService;
        private IProductService _proService;
        private IPublisherService _pubService;
        private IPurchasedPOGService _ppogService;
        private IPurchasedProductService _ppService;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        private RoleManager<IdentityRole> _roleManager;
        private IGameItemService _giService;
        private ILanguageTextService _ltService;
        private IInstructionPanelService _ipService;
        private IIndexSliderService _isService;
        private IXboxdataService _xdService;
        private IXboxgameService _xgService;
        private IPaymentPHistoryService _pphService;
        private IPaymentPOGHistoryService _ppoghService;
        private PlayPointContext Context { get; }
        public ProductController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender,
                                IActivationCountryService acService, ICameraPerspectiveServices cpService, ICurrencyService curService,
                                IDeveloperService devService, IDiviceService divService, IGameCategoryService gcService,
                                IGameNameService gnService, IJanraService janService, ILanguageService lanService,
                                IPlatformService platService, IProductOfGamerService pogService, IProductService proService,
                                IPublisherService pubService, IPurchasedPOGService ppogService, IPurchasedProductService ppService,
                                RoleManager<IdentityRole> roleManager, IGameItemService giService, ILanguageTextService ltService,
                                IInstructionPanelService ipService, IIndexSliderService isService, IXboxdataService xdService,
                                IXboxgameService xgService, IPaymentPHistoryService pphService, IPaymentPOGHistoryService ppoghService,
                                PlayPointContext context)

        {
            Context = context;
            _xdService = xdService;
            _xgService = xgService;
            _pphService = pphService;
            _ppoghService = ppoghService;
            _giService = giService;
            _ltService = ltService;
            _ipService = ipService;
            _isService = isService;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _acService = acService;
            _cpService = cpService;
            _curService = curService;
            _devService = devService;
            _divService = divService;
            _gcService = gcService;
            _gnService = gnService;
            _janService = janService;
            _lanService = lanService;
            _platService = platService;
            _pogService = pogService;
            _proService = proService;
            _pubService = pubService;
            _ppogService = ppogService;
            _ppService = ppService;
        }

        /////////////////////////////////////////////////// List //
        public async Task<IActionResult> ProductList(int page = 1)
        {
            if (page < 1)
            {
                return Redirect("/product/list?page=1");
            }
            var a = _proService.GetAllNU().Count();
            const int pageSize = 21;
            int b = a / pageSize;
            int c = a - 21;
            if (c == 0)
            {

            }
            else if (c > 0 || c < 0)
            {
                b += 1;
            }
            if (b > 0 && b <= 1)
            {

            }
            else
            {
                if (page > b)
                {
                    return Redirect($"/product/list?page={b}");
                }
            }
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var pronu = await _proService.GetAllNUAsync(page, pageSize);
                var rand = new Random();
                var gpViewModel = new ProductListViewModel()
                {
                    PageInfo = new PageInfo()
                    {
                        TotalItems = a,
                        CurrentPage = page,
                        ItemsPerPage = pageSize
                    },
                    Pros = pronu,
                    CPs = _cpService.GetAll(),
                    Jans = _janService.GetAll(),
                    GCs = _gcService.GetAll(),
                    IPs = _ipService.GetAll().Where(i => i.IsApproved).OrderBy(o => rand.Next()).ToList(),
                    Curs = _curService.GetAll().ToList(),
                    Cur = user.currencyID,
                    lastpage = b
                };
                return View(gpViewModel);
            }
            else
            {
                var rand = new Random();
                var pronu = await _proService.GetAllNUAsync(page, pageSize);
                var gpViewModel = new ProductListViewModel()
                {
                    PageInfo = new PageInfo()
                    {
                        TotalItems = a,
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                    },
                    Pros = pronu,
                    CPs = _cpService.GetAll(),
                    Jans = _janService.GetAll(),
                    GCs = _gcService.GetAll(),
                    IPs = _ipService.GetAll().Where(i => i.IsApproved).OrderBy(o => rand.Next()).ToList(),
                    Curs = _curService.GetAll().ToList(),
                    lastpage = b
                };
                return View(gpViewModel);
            }

        }

        public async Task<IActionResult> POGList(int page = 1)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                const int pageSize = 50;
                var rand = new Random();
                return View(new Product_of_GamerListViewModel()
                {
                    PageInfo = new PageInfo()
                    {
                        TotalItems = _pogService.GetCount(),
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                    },
                    Pogs = _pogService.GetAll(page, pageSize),
                    Gns = _gnService.GetAll(),
                    IPs = _ipService.GetAll().Where(i => i.IsApproved).OrderBy(o => rand.Next()).ToList(),
                    Curs = _curService.GetAll().ToList(),
                    Cur = user.currencyID
                });
            }
            else
            {
                const int pageSize = 50;
                var rand = new Random();
                return View(new Product_of_GamerListViewModel()
                {
                    PageInfo = new PageInfo()
                    {
                        TotalItems = _pogService.GetCount(),
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                    },
                    Pogs = _pogService.GetAll(page, pageSize),
                    Gns = _gnService.GetAll(),
                    IPs = _ipService.GetAll().Where(i => i.IsApproved).OrderBy(o => rand.Next()).ToList(),
                    Curs = _curService.GetAll().ToList(),
                });
            }

        }

        public async Task<IActionResult> Xbox()
        {
            var xbox = _xdService.GetAll().FirstOrDefault();
            int totaldays = 0;
            double price = 0;
            DateTime dt_now = DateTime.Now;
            int mm = dt_now.AddMonths(1).Month;
            int yy = dt_now.Year;
            DateTime lastday = DateTime.Now;
            if (dt_now.AddMonths(1).Year == dt_now.Year)
            {
                lastday = new DateTime(yy, mm, 1, 0, 0, 0);
            }
            else if (dt_now.AddMonths(1).Year > dt_now.Year)
            {
                lastday = new DateTime(yy + 1, mm, 1, 0, 0, 0);
            }
            double days = (lastday - dt_now).TotalDays;
            totaldays += Convert.ToInt32(days);
            if (days - totaldays == 0)
            {

            }
            else if (days - totaldays > 0)
            {
                totaldays += 1;
            }
            price = xbox.Price * totaldays;

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var xdViewModel = new XboxdataListViewModel()
                {
                    Xboxdata = xbox,
                    XDs = _xdService.GetAll(1, 1),
                    XGs = _xgService.GetAllforP(1, 21),
                    Curs = _curService.GetAll().ToList(),
                    Cur = user.currencyID,
                    start = user.Xboxstart,
                    expire = user.Xboxexpire,
                    Price = price
                };
                return View(xdViewModel);
            }
            else
            {
                var xdViewModel = new XboxdataListViewModel()
                {
                    Xboxdata = xbox,
                    XDs = _xdService.GetAll(1, 1),
                    XGs = _xgService.GetAllforP(1, 21),
                    Curs = _curService.GetAll().ToList(),
                    Price = price
                };
                return View(xdViewModel);
            }

        }

        /////////////////////////////////////////////////// Guide //
        public IActionResult GEpic()
        {
            return View();
        }
        public IActionResult GOrigin()
        {
            return View();
        }
        public IActionResult GSteam()
        {
            return View();
        }
        public IActionResult GUplay()
        {
            return View();
        }
        public IActionResult GXbox()
        {
            return View();
        }
        public IActionResult GSteamWRockstar()
        {
            return View();
        }
        /////////////////////////////////////////////////// Coming Soon //
        public IActionResult ComingSoon()
        {
            return View();
        }

        /////////////////////////////////////////////////// Details //
        public async Task<IActionResult> ProductDetails(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (id == null)
                {
                    return NotFound();
                }
                Product pro = await _proService.GetAdDetailsAsync((int)id);
                if (pro == null)
                {
                    return NotFound();
                }
                bool have = false;
                foreach (var e in _ppService.GetAll().Where(i => i.UserId == user.Id && i.ProductId == id && i.IsXbox == false))
                {
                    if (e.ProductId == id)
                    {
                        have = true;
                    }
                    else
                    {
                        have = false;
                    }
                }
                return View(new ProductDetailModel
                {
                    Products = pro,
                    Plats = _platService.GetAll(),
                    GCs = _gcService.GetAll(),
                    Jans = _janService.GetAll(),
                    CPs = _cpService.GetAll(),
                    Pubs = _pubService.GetAll(),
                    Devs = _devService.GetAll(),
                    ACs = _acService.GetAll(),
                    Curs = _curService.GetAll().ToList(),
                    Cur = user.currencyID,
                    LTs = _ltService.GetAll().Where(i => i.ProductId == id && i.LaguageId == user.languageID).ToList(),
                    have = have
                });
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }
                Product pro = await _proService.GetAdDetailsAsync((int)id);
                if (pro == null)
                {
                    return NotFound();
                }
                return View(new ProductDetailModel
                {
                    Products = pro,
                    Plats = _platService.GetAll(),
                    GCs = _gcService.GetAll(),
                    Jans = _janService.GetAll(),
                    CPs = _cpService.GetAll(),
                    Pubs = _pubService.GetAll(),
                    Devs = _devService.GetAll(),
                    ACs = _acService.GetAll(),
                    Curs = _curService.GetAll().ToList(),
                    Lans = _lanService.GetAll(),
                    LTs = _ltService.GetAll().Where(i => i.ProductId == id).ToList()
                });
            }
        }

        public async Task<IActionResult> POGDetails(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (id == null)
                {
                    return NotFound();
                }
                Product_of_Gamer pog = _pogService.GetAdDetails((int)id);
                if (pog == null)
                {
                    return NotFound();
                }
                return View(new Product_of_GamerDetailModel
                {
                    ProductofGamers = pog,
                    Gns = _gnService.GetAll(),
                    Divs = _divService.GetAll(),
                    Users = _userManager.Users.ToList(),
                    Curs = _curService.GetAll().ToList(),
                    Cur = user.currencyID
                });
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }
                Product_of_Gamer pog = _pogService.GetAdDetails((int)id);
                if (pog == null)
                {
                    return NotFound();
                }
                return View(new Product_of_GamerDetailModel
                {
                    ProductofGamers = pog,
                    Gns = _gnService.GetAll(),
                    Divs = _divService.GetAll(),
                    Users = _userManager.Users.ToList(),
                    Curs = _curService.GetAll().ToList(),
                });
            }

        }

        /////////////////////////////////////////////////// ComboBox //
        public string GetItem(int id)
        {
            var entity1 = _giService.GetByIdWithGN(id);
            string text = "";

            if (entity1 != null)
            {
            }
            foreach (var item in entity1)
            {
                text += "<option value='" + item.Id + "'>" + item.GameItemName + "</option>";
            }

            return text;
        }


        /////////////////////////////////////////////////// Search //
        //[HttpPost]
        public async Task<IActionResult> ProductSearch(string q = "", int gcId = 0, int janId = 0,
                                            int cpId = 0, int page = 1)
        {
            var rand = new Random();
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (string.IsNullOrEmpty(q))
                {
                    const int pageSize = 21;
                    //var Pro = _proService.GetAll();
                    List<Product> pr = await _proService.GetMultiSearchAsync(gcId, janId, cpId, page, pageSize);

                    if (false)
                    {
                        //foreach (var w in Pro)
                        //{
                        //    if (gcId != 0 && janId != 0 && cpId != 0)
                        //    {
                        //        if (w.CategoryID == gcId)
                        //        {
                        //            if (w.JanraID == janId)
                        //            {
                        //                if (w.CameraperspectiveID == cpId)
                        //                {
                        //                    pr.Add(w);
                        //                }
                        //            }
                        //        }
                        //    }
                        //    else if (gcId != 0 && janId != 0 && cpId == 0)
                        //    {
                        //        if (w.CategoryID == gcId)
                        //        {
                        //            if (w.JanraID == janId)
                        //            {
                        //                pr.Add(w);
                        //            }
                        //        }
                        //    }
                        //    else if (gcId != 0 && janId == 0 && cpId != 0)
                        //    {
                        //        if (w.CategoryID == gcId)
                        //        {
                        //            if (w.CameraperspectiveID == cpId)
                        //            {
                        //                pr.Add(w);
                        //            }
                        //        }
                        //    }
                        //    else if (gcId != 0 && janId == 0 && cpId == 0)
                        //    {
                        //        if (w.CategoryID == gcId)
                        //        {
                        //            pr.Add(w);
                        //        }
                        //    }
                        //    else if (gcId == 0 && janId != 0 && cpId != 0)
                        //    {
                        //        if (w.JanraID == janId)
                        //        {
                        //            if (w.CameraperspectiveID == cpId)
                        //            {
                        //                pr.Add(w);
                        //            }
                        //        }
                        //    }
                        //    else if (gcId == 0 && janId == 0 && cpId != 0)
                        //    {
                        //        if (w.CameraperspectiveID == cpId)
                        //        {
                        //            pr.Add(w);
                        //        }
                        //    }
                        //}
                    }

                    int a = pr.Count();
                    if (a == 0)
                    {
                        var proViewModel0 = new ProductListViewModel()
                        {
                            Pros = null,
                            CPs = _cpService.GetAll(),
                            Jans = _janService.GetAll(),
                            GCs = _gcService.GetAll(),
                            IPs = _ipService.GetAll().Where(i => i.IsApproved).OrderBy(o => rand.Next()).ToList(),
                            q = q,
                            gcId = gcId,
                            janId = janId,
                            cpId = cpId,
                            Curs = _curService.GetAll().ToList(),
                            Cur = user.currencyID
                        };
                        return View(proViewModel0);
                    }
                    if (page < 1)
                    {
                        return Redirect("/product/searchresult?page=1");
                    }
                    int b = a / pageSize;
                    int c = a - 21;
                    if (c == 0)
                    {

                    }
                    else if (c > 0 || c < 0)
                    {
                        b += 1;
                    }
                    if (b > 0 && b <= 1)
                    {
                    }
                    else
                    {
                        if (page > b)
                        {
                            return Redirect($"/product/searchresult?page={b}");
                        }
                    }

                    var proViewModel = new ProductListViewModel()
                    {
                        PageInfo = new PageInfo()
                        {
                            TotalItems = a,
                            CurrentPage = page,
                            ItemsPerPage = pageSize
                        },
                        Pros = await _proService.GetMultiSearchAsync(gcId, janId, cpId, page, pageSize), // a- deyise biler => pageSize
                        CPs = _cpService.GetAll(),
                        Jans = _janService.GetAll(),
                        GCs = _gcService.GetAll(),
                        IPs = _ipService.GetAll().Where(i => i.IsApproved).OrderBy(o => rand.Next()).ToList(),
                        q = q,
                        gcId = gcId,
                        janId = janId,
                        cpId = cpId,
                        Curs = _curService.GetAll().ToList(),
                        Cur = user.currencyID,
                        lastpage = b
                    };
                    return View(proViewModel);
                }
                else if (!string.IsNullOrEmpty(q) && (gcId != 0 || janId != 0 || cpId != 0))
                {
                    const int pageSize = 21;
                    List<Product> pr = _proService.GetMultiSearch(gcId, janId, cpId, page, pageSize);

                    int a = pr.Count();
                    if (a == 0)
                    {
                        var proViewModel0 = new ProductListViewModel()
                        {
                            Pros = null,
                            CPs = _cpService.GetAll(),
                            Jans = _janService.GetAll(),
                            GCs = _gcService.GetAll(),
                            IPs = _ipService.GetAll().Where(i => i.IsApproved).OrderBy(o => rand.Next()).ToList(),
                            q = q,
                            gcId = gcId,
                            janId = janId,
                            cpId = cpId,
                            Curs = _curService.GetAll().ToList(),
                            Cur = user.currencyID
                        };
                        return View(proViewModel0);
                    }
                    if (page < 1)
                    {
                        return Redirect("/product/searchresult?page=1");
                    }
                    int b = a / pageSize;
                    int c = a - 21;
                    if (c == 0)
                    {

                    }
                    else if (c > 0 || c < 0)
                    {
                        b += 1;
                    }
                    if (b > 0 && b <= 1)
                    {
                    }
                    else
                    {
                        if (page > b)
                        {
                            return Redirect($"/product/searchresult?page={b}");
                        }
                    }

                    var proViewModel = new ProductListViewModel()
                    {
                        PageInfo = new PageInfo()
                        {
                            TotalItems = a,
                            CurrentPage = page,
                            ItemsPerPage = pageSize
                        },
                        Pros = await _proService.GetMultiSearchAsync(gcId, janId, cpId, page, pageSize), // a- deyise biler => pageSize
                        CPs = _cpService.GetAll(),
                        Jans = _janService.GetAll(),
                        GCs = _gcService.GetAll(),
                        IPs = _ipService.GetAll().Where(i => i.IsApproved).OrderBy(o => rand.Next()).ToList(),
                        q = q,
                        gcId = gcId,
                        janId = janId,
                        cpId = cpId,
                        Curs = _curService.GetAll().ToList(),
                        Cur = user.currencyID,
                        lastpage = b
                    };
                    return View(proViewModel);
                }
                else
                {
                    int ps = _proService.GetSearchResult(q).Count();
                    if (ps == 0)
                    {
                        var proViewModel0 = new ProductListViewModel()
                        {
                            Pros = null,
                            CPs = _cpService.GetAll(),
                            Jans = _janService.GetAll(),
                            GCs = _gcService.GetAll(),
                            IPs = _ipService.GetAll().Where(i => i.IsApproved).OrderBy(o => rand.Next()).ToList(),
                            q = q,
                            gcId = gcId,
                            janId = janId,
                            cpId = cpId,
                            Curs = _curService.GetAll().ToList(),
                            Cur = user.currencyID
                        };
                        return View(proViewModel0);
                    }
                    if (page < 1)
                    {
                        return Redirect("/product/searchresult?page=1");
                    }
                    var a = ps;
                    const int pageSize = 21;
                    int b = a / pageSize;
                    int c = a - 21;
                    if (c == 0)
                    {

                    }
                    else if (c > 0 || c < 0)
                    {
                        b += 1;
                    }
                    if (b > 0 && b <= 1)
                    {

                    }
                    else
                    {
                        if (page > b)
                        {
                            return Redirect($"/product/searchresult?page={b}");
                        }
                    }

                    var proViewModel = new ProductListViewModel()
                    {
                        PageInfo = new PageInfo()
                        {
                            TotalItems = a,
                            CurrentPage = page,
                            ItemsPerPage = pageSize
                        },
                        Pros = await _proService.GetSearchResultAsync(q, page, pageSize), // a- deyise biler => pageSize
                        CPs = _cpService.GetAll(),
                        Jans = _janService.GetAll(),
                        GCs = _gcService.GetAll(),
                        IPs = _ipService.GetAll().Where(i => i.IsApproved).OrderBy(o => rand.Next()).ToList(),
                        q = q,
                        gcId = gcId,
                        janId = janId,
                        cpId = cpId,
                        Curs = _curService.GetAll().ToList(),
                        Cur = user.currencyID,
                        lastpage = b
                    };
                    return View(proViewModel);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(q))
                {
                    const int pageSize = 21;
                    //var Pro = _proService.GetAll();
                    List<Product> pr = _proService.GetMultiSearch(gcId, janId, cpId, page, pageSize);

                    if (false)
                    {
                        //foreach (var w in Pro)
                        //                    {
                        //                        if (gcId != 0 && janId != 0 && cpId != 0)
                        //                        {
                        //                            if (w.CategoryID == gcId)
                        //                            {
                        //                                if (w.JanraID == janId)
                        //                                {
                        //                                    if (w.CameraperspectiveID == cpId)
                        //                                    {
                        //                                        pr.Add(w);
                        //                                    }
                        //                                }
                        //                            }
                        //                        }
                        //                        else if (gcId != 0 && janId != 0 && cpId == 0)
                        //                        {
                        //                            if (w.CategoryID == gcId)
                        //                            {
                        //                                if (w.JanraID == janId)
                        //                                {
                        //                                    pr.Add(w);
                        //                                }
                        //                            }
                        //                        }
                        //                        else if (gcId != 0 && janId == 0 && cpId != 0)
                        //                        {
                        //                            if (w.CategoryID == gcId)
                        //                            {
                        //                                if (w.CameraperspectiveID == cpId)
                        //                                {
                        //                                    pr.Add(w);
                        //                                }
                        //                            }
                        //                        }
                        //                        else if (gcId != 0 && janId == 0 && cpId == 0)
                        //                        {
                        //                            if (w.CategoryID == gcId)
                        //                            {
                        //                                pr.Add(w);
                        //                            }
                        //                        }
                        //                        else if (gcId == 0 && janId != 0 && cpId != 0)
                        //                        {
                        //                            if (w.JanraID == janId)
                        //                            {
                        //                                if (w.CameraperspectiveID == cpId)
                        //                                {
                        //                                    pr.Add(w);
                        //                                }
                        //                            }
                        //                        }
                        //                        else if (gcId == 0 && janId == 0 && cpId != 0)
                        //                        {
                        //                            if (w.CameraperspectiveID == cpId)
                        //                            {
                        //                                pr.Add(w);
                        //                            }
                        //                        }

                        //                    }
                    }

                    var a = pr.Count();
                    if (a == 0)
                    {
                        var proViewModel0 = new ProductListViewModel()
                        {
                            Pros = null,
                            CPs = _cpService.GetAll(),
                            Jans = _janService.GetAll(),
                            GCs = _gcService.GetAll(),
                            IPs = _ipService.GetAll().Where(i => i.IsApproved).OrderBy(o => rand.Next()).ToList(),
                            q = q,
                            gcId = gcId,
                            janId = janId,
                            cpId = cpId,
                            Curs = _curService.GetAll().ToList()
                        };
                        return View(proViewModel0);
                    }
                    if (page < 1)
                    {
                        return Redirect("/product/searchresult?page=1");
                    }
                    int b = a / pageSize;
                    int c = a - 21;
                    if (c == 0)
                    {

                    }
                    else if (c > 0 || c < 0)
                    {
                        b += 1;
                    }
                    if (b > 0 && b <= 1)
                    {
                    }
                    else
                    {
                        if (page > b)
                        {
                            return Redirect($"/product/searchresult?page={b}");
                        }
                    }

                    var proViewModel = new ProductListViewModel()
                    {
                        PageInfo = new PageInfo()
                        {
                            TotalItems = a,
                            CurrentPage = page,
                            ItemsPerPage = pageSize
                        },
                        Pros = await _proService.GetMultiSearchAsync(gcId, janId, cpId, page, pageSize), // a- deyise biler => pageSize
                        CPs = _cpService.GetAll(),
                        Jans = _janService.GetAll(),
                        GCs = _gcService.GetAll(),
                        IPs = _ipService.GetAll().Where(i => i.IsApproved).OrderBy(o => rand.Next()).ToList(),
                        q = q,
                        gcId = gcId,
                        janId = janId,
                        cpId = cpId,
                        Curs = _curService.GetAll().ToList(),
                        lastpage = b
                    };
                    return View(proViewModel);
                }
                else if (!string.IsNullOrEmpty(q) && (gcId != 0 || janId != 0 || cpId != 0))
                {
                    const int pageSize = 21;
                    List<Product> pr = _proService.GetMultiSearch(gcId, janId, cpId, page, pageSize);

                    int a = pr.Count();
                    if (a == 0)
                    {
                        var proViewModel0 = new ProductListViewModel()
                        {
                            Pros = null,
                            CPs = _cpService.GetAll(),
                            Jans = _janService.GetAll(),
                            GCs = _gcService.GetAll(),
                            IPs = _ipService.GetAll().Where(i => i.IsApproved).OrderBy(o => rand.Next()).ToList(),
                            q = q,
                            gcId = gcId,
                            janId = janId,
                            cpId = cpId,
                            Curs = _curService.GetAll().ToList()
                        };
                        return View(proViewModel0);
                    }
                    if (page < 1)
                    {
                        return Redirect("/product/searchresult?page=1");
                    }
                    int b = a / pageSize;
                    int c = a - 21;
                    if (c == 0)
                    {

                    }
                    else if (c > 0 || c < 0)
                    {
                        b += 1;
                    }
                    if (b > 0 && b <= 1)
                    {
                    }
                    else
                    {
                        if (page > b)
                        {
                            return Redirect($"/product/searchresult?page={b}");
                        }
                    }

                    var proViewModel = new ProductListViewModel()
                    {
                        PageInfo = new PageInfo()
                        {
                            TotalItems = a,
                            CurrentPage = page,
                            ItemsPerPage = pageSize
                        },
                        Pros = await _proService.GetMultiSearchAsync(gcId, janId, cpId, page, pageSize), // a- deyise biler => pageSize
                        CPs = _cpService.GetAll(),
                        Jans = _janService.GetAll(),
                        GCs = _gcService.GetAll(),
                        IPs = _ipService.GetAll().Where(i => i.IsApproved).OrderBy(o => rand.Next()).ToList(),
                        q = q,
                        gcId = gcId,
                        janId = janId,
                        cpId = cpId,
                        Curs = _curService.GetAll().ToList(),
                        lastpage = b
                    };
                    return View(proViewModel);
                }
                else
                {
                    int ps = _proService.GetSearchResult(q).Count();
                    if (ps == 0)
                    {
                        var proViewModel0 = new ProductListViewModel()
                        {
                            Pros = null,
                            CPs = _cpService.GetAll(),
                            Jans = _janService.GetAll(),
                            GCs = _gcService.GetAll(),
                            IPs = _ipService.GetAll().Where(i => i.IsApproved).OrderBy(o => rand.Next()).ToList(),
                            q = q,
                            gcId = gcId,
                            janId = janId,
                            cpId = cpId,
                            Curs = _curService.GetAll().ToList()
                        };
                        return View(proViewModel0);
                    }
                    if (page < 1)
                    {
                        return Redirect("/product/searchresult?page=1");
                    }
                    int a = ps;
                    const int pageSize = 21;
                    int b = a / pageSize;
                    int c = a - 21;
                    if (c == 0)
                    {

                    }
                    else if (c > 0 || c < 0)
                    {
                        b += 1;
                    }
                    if (b > 0 && b <= 1)
                    {

                    }
                    else
                    {
                        if (page > b)
                        {
                            return Redirect($"/product/searchresult?page={b}");
                        }
                    }

                    var proViewModel = new ProductListViewModel()
                    {
                        PageInfo = new PageInfo()
                        {
                            TotalItems = a,
                            CurrentPage = page,
                            ItemsPerPage = pageSize
                        },
                        Pros = await _proService.GetSearchResultAsync(q, page, pageSize), // a- deyise biler => pageSize
                        CPs = _cpService.GetAll(),
                        Jans = _janService.GetAll(),
                        GCs = _gcService.GetAll(),
                        IPs = _ipService.GetAll().Where(i => i.IsApproved).OrderBy(o => rand.Next()).ToList(),
                        q = q,
                        gcId = gcId,
                        janId = janId,
                        cpId = cpId,
                        Curs = _curService.GetAll().ToList(),
                        lastpage = b
                    };
                    return View(proViewModel);
                }
            }
        }

        [HttpPost]
        public IActionResult POGSearch(string q, int minValue = 0, int maxValue = 0,
                                        int gnId = 0, int giId = 0, int order = 0, int page = 1)
        {
            const int pageSize = 20;

            if (string.IsNullOrEmpty(q))
            {
                List<Product_of_Gamer> pog = null;
                var pogs = _pogService.GetAll().ToList();

                foreach (var w in pogs)
                {
                    if (maxValue != 0 && minValue >= 0)
                    {
                        if (minValue <= w.Price && w.Price <= maxValue)
                        {
                            if (gnId != 0 && giId != 0)
                            {
                                if (w.GameNameID == gnId && w.GameItemID == giId)
                                {
                                    pog.Add(w);
                                }
                            }
                            else if (gnId != 0 && giId == 0)
                            {
                                if (w.GameNameID == gnId)
                                {
                                    pog.Add(w);
                                }
                            }
                            else
                            {
                                pog.Add(w);
                            }
                        }
                    }
                    else if (maxValue == 0 && minValue >= 0)
                    {
                        if (w.Price >= minValue)
                        {
                            if (gnId != 0 && giId != 0)
                            {
                                if (w.GameNameID == gnId && w.GameItemID == giId)
                                {
                                    pog.Add(w);
                                }
                            }
                            else if (gnId != 0 && giId == 0)
                            {
                                if (w.GameNameID == gnId)
                                {
                                    pog.Add(w);
                                }
                            }
                            else
                            {
                                pog.Add(w);
                            }
                        }
                    }
                    else if (maxValue == 0 && minValue == 0)
                    {
                        if (gnId != 0 && giId != 0)
                        {
                            if (w.GameNameID == gnId && w.GameItemID == giId)
                            {
                                pog.Add(w);
                            }
                        }
                        else if (gnId != 0 && giId == 0)
                        {
                            if (w.GameNameID == gnId)
                            {
                                pog.Add(w);
                            }
                        }
                        else
                        {
                            pog.Add(w);
                        }
                    }
                }

                if (order == 1)
                {
                    var pogViewModel2 = new Product_of_GamerListViewModel()
                    {
                        PageInfo = new PageInfo()
                        {
                            TotalItems = _pogService.GetCount(),
                            CurrentPage = page,
                            ItemsPerPage = pageSize,
                        },
                        Pogs = pog,
                        Gns = _gnService.GetAll(),
                        Divs = _divService.GetAll()
                    };
                    return View(pogViewModel2);
                }
                else if (order == 2)
                {
                    var pogViewModel2 = new Product_of_GamerListViewModel()
                    {
                        PageInfo = new PageInfo()
                        {
                            TotalItems = _pogService.GetCount(),
                            CurrentPage = page,
                            ItemsPerPage = pageSize,
                        },
                        Pogs = pog.OrderBy(x => x.Price).ToList(),
                        Gns = _gnService.GetAll(),
                        Divs = _divService.GetAll()
                    };
                    return View(pogViewModel2);
                }
                else if (order == 3)
                {
                    var pogViewModel2 = new Product_of_GamerListViewModel()
                    {
                        PageInfo = new PageInfo()
                        {
                            TotalItems = _pogService.GetCount(),
                            CurrentPage = page,
                            ItemsPerPage = pageSize,
                        },
                        Pogs = pog.OrderByDescending(x => x.Price).ToList(),
                        Gns = _gnService.GetAll(),
                        Divs = _divService.GetAll()
                    };
                    return View(pogViewModel2);
                }
                else if (order == 4)
                {
                    var pogViewModel2 = new Product_of_GamerListViewModel()
                    {
                        PageInfo = new PageInfo()
                        {
                            TotalItems = _pogService.GetCount(),
                            CurrentPage = page,
                            ItemsPerPage = pageSize,
                        },
                        Pogs = pog.OrderBy(x => x.DateTime).ToList(),
                        Gns = _gnService.GetAll(),
                        Divs = _divService.GetAll()
                    };
                    return View(pogViewModel2);
                }
                else if (order == 5)
                {
                    var pogViewModel2 = new Product_of_GamerListViewModel()
                    {
                        PageInfo = new PageInfo()
                        {
                            TotalItems = _pogService.GetCount(),
                            CurrentPage = page,
                            ItemsPerPage = pageSize,
                        },
                        Pogs = pog.OrderBy(x => x.DateTime).ToList(),
                        Gns = _gnService.GetAll(),
                        Divs = _divService.GetAll()
                    };
                    return View(pogViewModel2);
                }
            }
            else
            {
                List<Product_of_Gamer> pog = null;
                var pogs = _pogService.GetSearchResult(q, page, pageSize).ToList();

                foreach (var w in pogs)
                {
                    if (maxValue != 0 && minValue >= 0)
                    {
                        if (minValue <= w.Price && w.Price <= maxValue)
                        {
                            if (gnId != 0 && giId != 0)
                            {
                                if (w.GameNameID == gnId && w.GameItemID == giId)
                                {
                                    pog.Add(w);
                                }
                            }
                            else if (gnId != 0 && giId == 0)
                            {
                                if (w.GameNameID == gnId)
                                {
                                    pog.Add(w);
                                }
                            }
                            else
                            {
                                pog.Add(w);
                            }
                        }
                    }
                    else if (maxValue == 0 && minValue >= 0)
                    {
                        if (w.Price >= minValue)
                        {
                            if (gnId != 0 && giId != 0)
                            {
                                if (w.GameNameID == gnId && w.GameItemID == giId)
                                {
                                    pog.Add(w);
                                }
                            }
                            else if (gnId != 0 && giId == 0)
                            {
                                if (w.GameNameID == gnId)
                                {
                                    pog.Add(w);
                                }
                            }
                            else
                            {
                                pog.Add(w);
                            }
                        }
                    }
                    else if (maxValue == 0 && minValue == 0)
                    {
                        if (gnId != 0 && giId != 0)
                        {
                            if (w.GameNameID == gnId && w.GameItemID == giId)
                            {
                                pog.Add(w);
                            }
                        }
                        else if (gnId != 0 && giId == 0)
                        {
                            if (w.GameNameID == gnId)
                            {
                                pog.Add(w);
                            }
                        }
                        else
                        {
                            pog.Add(w);
                        }
                    }
                }

                if (order == 1)
                {
                    var pogViewModel2 = new Product_of_GamerListViewModel()
                    {
                        PageInfo = new PageInfo()
                        {
                            TotalItems = _pogService.GetCount(),
                            CurrentPage = page,
                            ItemsPerPage = pageSize,
                        },
                        Pogs = pog,
                        Gns = _gnService.GetAll(),
                        Divs = _divService.GetAll()
                    };
                    return View(pogViewModel2);
                }
                else if (order == 2)
                {
                    var pogViewModel2 = new Product_of_GamerListViewModel()
                    {
                        PageInfo = new PageInfo()
                        {
                            TotalItems = _pogService.GetCount(),
                            CurrentPage = page,
                            ItemsPerPage = pageSize,
                        },
                        Pogs = pog.OrderBy(x => x.Price).ToList(),
                        Gns = _gnService.GetAll(),
                        Divs = _divService.GetAll()
                    };
                    return View(pogViewModel2);
                }
                else if (order == 3)
                {
                    var pogViewModel2 = new Product_of_GamerListViewModel()
                    {
                        PageInfo = new PageInfo()
                        {
                            TotalItems = _pogService.GetCount(),
                            CurrentPage = page,
                            ItemsPerPage = pageSize,
                        },
                        Pogs = pog.OrderByDescending(x => x.Price).ToList(),
                        Gns = _gnService.GetAll(),
                        Divs = _divService.GetAll()
                    };
                    return View(pogViewModel2);
                }
                else if (order == 4)
                {
                    var pogViewModel2 = new Product_of_GamerListViewModel()
                    {
                        PageInfo = new PageInfo()
                        {
                            TotalItems = _pogService.GetCount(),
                            CurrentPage = page,
                            ItemsPerPage = pageSize,
                        },
                        Pogs = pog.OrderBy(x => x.DateTime).ToList(),
                        Gns = _gnService.GetAll(),
                        Divs = _divService.GetAll()
                    };
                    return View(pogViewModel2);
                }
                else if (order == 5)
                {
                    var pogViewModel2 = new Product_of_GamerListViewModel()
                    {
                        PageInfo = new PageInfo()
                        {
                            TotalItems = _pogService.GetCount(),
                            CurrentPage = page,
                            ItemsPerPage = pageSize,
                        },
                        Pogs = pog.OrderBy(x => x.DateTime).ToList(),
                        Gns = _gnService.GetAll(),
                        Divs = _divService.GetAll()
                    };
                    return View(pogViewModel2);
                }

            }

            var pogViewModel = new Product_of_GamerListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _pogService.GetCount(),
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                },
                Pogs = _pogService.GetAll(page, pageSize),
                Gns = _gnService.GetAll(),
                Divs = _divService.GetAll()
            };
            return View(pogViewModel);
        }

        [HttpPost]
        public JsonResult AutoComplete(string q)
        {
            var pros = (from pro in Context.Pros
                        where pro.Name.StartsWith(q)
                        select new
                        {
                            label = pro.Name,
                            val = pro.Id
                        }).ToList();
            return Json(pros);
        }
    }
}