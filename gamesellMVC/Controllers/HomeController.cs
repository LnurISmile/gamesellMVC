using gamesell.business.Abstract;
using gamesell.entity;
using gamesellMVC.EmailServices;
using gamesellMVC.Identity;
using gamesellMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Controllers
{
    public class HomeController : Controller
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
        private IIndexSliderService _isService;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        private IXboxdataService _xdService;
        private IXboxgameService _xgService;
        private IPaymentPHistoryService _pphService;
        private IPaymentPOGHistoryService _ppoghService;
        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender,
                                IActivationCountryService acService, ICameraPerspectiveServices cpService, ICurrencyService curService,
                                IDeveloperService devService, IDiviceService divService, IGameCategoryService gcService,
                                IGameNameService gnService, IJanraService janService, ILanguageService lanService,
                                IPlatformService platService, IProductOfGamerService pogService, IProductService proService,
                                IPublisherService pubService, IPurchasedPOGService ppogService, IPurchasedProductService ppService,
                                IIndexSliderService isService, IXboxdataService xdService, IXboxgameService xgService, 
                                IPaymentPHistoryService pphService, IPaymentPOGHistoryService ppoghService)

        {
            _xdService = xdService;
            _xgService = xgService;
            _pphService = pphService;
            _ppoghService = ppoghService;
            _isService = isService;
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

        public async Task<IActionResult> Index(int page = 1)
        {
            const int pageSize = 21;
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var rand = new Random();
                var rand2 = new Random();

                var UC = await _proService.GetAllUPAsync(page, 20);
                var SG = await _proService.GetAllNUAsync(page, 50);
                var NR = await _proService.GetAllNRAsync(page, 5);
                var TS = await _proService.GetAllTSAsync(page, 5);
                var GFY = await _proService.GetAllNUAsync();
                var IS = _isService.GetAll().Where(i => i.IsApproved).OrderBy(o => rand.Next()).ToList();
                var littleIS = _proService.GetAll().Where(i=>i.IndexSlider).OrderBy(o => rand2.Next()).ToList();
                var UC5 = await _proService.GetAllUPAsync(page, 5);

                var proViewModel = new ProductListViewModel()
                {
                    PageInfo = new PageInfo()
                    {
                        TotalItems = _proService.GetCount(),
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                    },
                    salegames = SG,
                    upcominggames = UC,
                    upcoming5 = UC5,
                    newreleases = NR,
                    topsellers = TS,
                    gamesforyou = GFY,
                    ISs = IS,
                    indexslider = littleIS,
                    Xboxdata = _xdService.GetAll().FirstOrDefault(),
                    Curs = _curService.GetAll().ToList(),
                    Cur = user.currencyID
                };
                return View(proViewModel);
            }
            else
            {
                var rand = new Random();
                var rand2 = new Random();

                var UC = await _proService.GetAllUPAsync(page, 20);
                var SG = await _proService.GetAllNUAsync(page, 50);
                var NR = await _proService.GetAllNRAsync(page, 5);
                var TS = await _proService.GetAllTSAsync(page, 5);
                var GFY = await _proService.GetAllNUAsync();
                var IS = _isService.GetAll().Where(i => i.IsApproved).OrderBy(o => rand.Next()).ToList();
                var littleIS = _proService.GetAll().Where(i => i.IndexSlider).OrderBy(o => rand2.Next()).ToList();
                var UC5 = await _proService.GetAllUPAsync(page, 5);

                var proViewModel = new ProductListViewModel()
                {
                    PageInfo = new PageInfo()
                    {
                        TotalItems = _proService.GetCount(),
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                    },
                    salegames = SG,
                    upcominggames = UC,
                    upcoming5 = UC5,
                    newreleases = NR,
                    topsellers = TS,
                    gamesforyou = GFY,
                    ISs = IS,
                    indexslider = littleIS,
                    Xboxdata = _xdService.GetAll().FirstOrDefault(),
                    Curs = _curService.GetAll().ToList(),
                };
                return View(proViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CultureManagement(string culture, string returnUrl)
        {
            List<Language> lan = _lanService.GetAll();
            List<Currency> cur = _curService.GetAll();
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user != null)
                {
                    foreach (var q in lan)
                    {
                        if (q.LanguageTag == culture)
                        {
                            user.languageID = q.Id;
                            break;
                        }
                    }
                    foreach (var w in cur)
                    {
                        if (w.LanguageTag == culture)
                        {
                            user.currencyID = w.Id;
                            break;
                        }
                    }
                    await _userManager.UpdateAsync(user);
                }
            }
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddYears(1) });

            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeCurrency(int culId, string returnUrl)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }
            user.currencyID = culId;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return LocalRedirect(returnUrl);
            }
        }
    }
}