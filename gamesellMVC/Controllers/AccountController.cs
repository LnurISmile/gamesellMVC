using gamesell.business.Abstract;
using gamesellMVC.EmailServices;
using gamesellMVC.Extensions;
using gamesellMVC.Identity;
using gamesellMVC.Models;
using gamesell.entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using System.Net.Http;
using System.Globalization;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace gamesellMVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
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
        private ICartPService _cartpService;
        private ICartPOGService _cartpogService;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        private ICartPService _cartService;
        private ICartPOGService _cpogService;
        private ICIPService _cipService;
        private ICIPOGService _cipogService;
        private IGameItemService _giService;
        private ILanguageTextService _ltService;
        private IInstructionPanelService _ipService;
        private IIndexSliderService _isService;
        private IXboxdataService _xdService;
        private IXboxgameService _xgService;
        private IPaymentPHistoryService _pphService;
        private IPaymentPOGHistoryService _ppoghService;
        private IBalanceService _bService;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender,
                                IActivationCountryService acService, ICameraPerspectiveServices cpService, ICurrencyService curService,
                                IDeveloperService devService, IDiviceService divService, IGameCategoryService gcService,
                                IGameNameService gnService, IJanraService janService, ILanguageService lanService,
                                IPlatformService platService, IProductOfGamerService pogService, IProductService proService,
                                IPublisherService pubService, IPurchasedPOGService ppogService, IPurchasedProductService ppService,
                                ICartPService cartpService, ICartPOGService cartpogService, ICartPService cartService,
                                ICartPOGService cpogService, ICIPService cipService, ICIPOGService cipogService, IGameItemService giService,
                                ILanguageTextService ltService, IInstructionPanelService ipService, IIndexSliderService isService,
                                IXboxdataService xdService, IXboxgameService xgService, IPaymentPHistoryService pphService,
                                IPaymentPOGHistoryService ppoghService, IBalanceService bService)

        {
            _bService = bService;
            _xdService = xdService;
            _xgService = xgService;
            _pphService = pphService;
            _ppoghService = ppoghService;
            _giService = giService;
            _ltService = ltService;
            _ipService = ipService;
            _isService = isService;
            _cpogService = cpogService;
            _cipService = cipService;
            _cipogService = cipogService;
            _cartService = cartService;
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
            _cartpService = cartpService;
            _cartpogService = cartpogService;
        }

        /////////////////////////////////////////////////// Profile Manage //
        public async Task<IActionResult> UserManage()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                return View(new UserManage()
                {
                    UserId = user.Id,
                    NickName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MobileNumber = user.MobileNumber,
                    Dob = user.Dob,
                    profile_pic = user.profile_pic,
                    back_pic = user.back_pic,
                    slider_1 = user.slider_1,
                    slider_2 = user.slider_2,
                    slider_3 = user.slider_3
                });
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserManage(UserManage model, IFormFile file1, IFormFile file2, IFormFile file3, IFormFile file4, IFormFile file5)
        {
            string profile_pic;
            string back_pic;
            string slider_1;
            string slider_2;
            string slider_3;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            int emailcount = _userManager.Users.Where(i => i.Email == model.Email).ToList().Count();
            int usernamecount = _userManager.Users.Where(i => i.UserName == model.NickName).ToList().Count();
            if (emailcount >= 1 && model.Email != user.Email)
            {
                return Redirect("/register/error/email");
            }
            if (usernamecount >= 1 && model.NickName != user.UserName)
            {
                return Redirect("/register/error/username");
            }

            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    if (file1 != null)
                    {
                        if (user.profile_pic != null)
                        {
                            profile_pic = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", user.profile_pic);
                            FileInfo fi1 = new FileInfo(profile_pic);
                            if (fi1 != null)
                            {
                                System.IO.File.Delete(profile_pic);
                                fi1.Delete();
                            }
                        }
                    }
                    if (file2 != null)
                    {
                        if (user.back_pic != null)
                        {
                            back_pic = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", user.back_pic);
                            FileInfo fi2 = new FileInfo(back_pic);
                            if (fi2 != null)
                            {
                                System.IO.File.Delete(back_pic);
                                fi2.Delete();
                            }
                        }
                    }
                    if (file3 != null)
                    {
                        if (user.slider_1 != null)
                        {
                            slider_1 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", user.slider_1);
                            FileInfo fi3 = new FileInfo(slider_1);
                            if (fi3 != null)
                            {
                                System.IO.File.Delete(slider_1);
                                fi3.Delete();
                            }
                        }
                    }
                    if (file4 != null)
                    {
                        if (user.slider_2 != null)
                        {
                            slider_2 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", user.slider_2);
                            FileInfo fi4 = new FileInfo(slider_2);
                            if (fi4 != null)
                            {
                                System.IO.File.Delete(slider_2);
                                fi4.Delete();
                            }
                        }
                    }
                    if (file5 != null)
                    {
                        if (user.slider_3 != null)
                        {
                            slider_3 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", user.slider_3);
                            FileInfo fi5 = new FileInfo(slider_3);
                            if (fi5 != null)
                            {
                                System.IO.File.Delete(slider_3);
                                fi5.Delete();
                            }
                        }
                    }

                    if (user.Email == model.Email)
                    {
                        if (user.UserName == model.NickName)
                        {
                            user.FirstName = model.FirstName;
                            user.LastName = model.LastName;
                            user.MobileNumber = model.MobileNumber;
                            user.Dob = model.Dob;
                            if (file1 != null)
                            {
                                if (Path.GetExtension(file1.FileName) == ".png" || Path.GetExtension(file1.FileName) == ".jpg" ||
                                    Path.GetExtension(file1.FileName) == ".jpeg" || Path.GetExtension(file1.FileName) == ".gif" ||
                                    Path.GetExtension(file1.FileName) == ".webp")
                                {
                                    if (file1.Length < 4500000)
                                    {
                                        var extention = Path.GetExtension(file1.FileName);
                                        var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                        user.profile_pic = randomName;
                                        var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                                        using (var image = Image.Load(file1.OpenReadStream()))
                                        {
                                            string newSize = ResizeImage(image, 120, 120);
                                            string[] aSize = newSize.Split(',');
                                            image.Mutate(q => q.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                                            image.Save(path);
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                }
                            }
                            if (file2 != null)
                            {
                                if (Path.GetExtension(file2.FileName) == ".png" || Path.GetExtension(file2.FileName) == ".jpg" ||
                                    Path.GetExtension(file2.FileName) == ".jpeg" || Path.GetExtension(file2.FileName) == ".gif" ||
                                    Path.GetExtension(file2.FileName) == ".webp")
                                {
                                    if (file2.Length < 4500000)
                                    {
                                        var extention = Path.GetExtension(file2.FileName);
                                        var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                        user.back_pic = randomName;
                                        var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                                        using (var image = Image.Load(file2.OpenReadStream()))
                                        {
                                            string newSize = ResizeImage(image, 800, 600);
                                            string[] aSize = newSize.Split(',');
                                            image.Mutate(q => q.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                                            image.Save(path);
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                }
                            }
                            if (file3 != null)
                            {
                                if (Path.GetExtension(file3.FileName) == ".png" || Path.GetExtension(file3.FileName) == ".jpg" ||
                                    Path.GetExtension(file3.FileName) == ".jpeg" || Path.GetExtension(file3.FileName) == ".gif" ||
                                    Path.GetExtension(file3.FileName) == ".webp")
                                {
                                    if (file3.Length < 4500000)
                                    {
                                        var extention = Path.GetExtension(file3.FileName);
                                        var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                        user.slider_1 = randomName;
                                        var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                                        using (var image = Image.Load(file3.OpenReadStream()))
                                        {
                                            string newSize = ResizeImage(image, 800, 600);
                                            string[] aSize = newSize.Split(',');
                                            image.Mutate(q => q.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                                            image.Save(path);
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                }
                            }
                            if (file4 != null)
                            {
                                if (Path.GetExtension(file4.FileName) == ".png" || Path.GetExtension(file4.FileName) == ".jpg" ||
                                    Path.GetExtension(file4.FileName) == ".jpeg" || Path.GetExtension(file4.FileName) == ".gif" ||
                                    Path.GetExtension(file4.FileName) == ".webp")
                                {
                                    if (file4.Length < 4500000)
                                    {
                                        var extention = Path.GetExtension(file4.FileName);
                                        var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                        user.slider_2 = randomName;
                                        var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                                        using (var image = Image.Load(file4.OpenReadStream()))
                                        {
                                            string newSize = ResizeImage(image, 800, 600);
                                            string[] aSize = newSize.Split(',');
                                            image.Mutate(q => q.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                                            image.Save(path);
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                }
                            }
                            if (file5 != null)
                            {
                                if (Path.GetExtension(file5.FileName) == ".png" || Path.GetExtension(file5.FileName) == ".jpg" ||
                                    Path.GetExtension(file5.FileName) == ".jpeg" || Path.GetExtension(file5.FileName) == ".gif" ||
                                    Path.GetExtension(file5.FileName) == ".webp")
                                {
                                    if (file5.Length < 4500000)
                                    {
                                        var extention = Path.GetExtension(file5.FileName);
                                        var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                        user.slider_3 = randomName;
                                        var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                                        using (var image = Image.Load(file5.OpenReadStream()))
                                        {
                                            string newSize = ResizeImage(image, 800, 600);
                                            string[] aSize = newSize.Split(',');
                                            image.Mutate(q => q.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                                            image.Save(path);
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                }
                            }
                            var result = await _userManager.UpdateAsync(user);
                            if (result.Succeeded)
                            {
                                return Redirect("/account/usermanage");
                            }
                        }
                        else
                        {
                            user.UserName = model.NickName;
                            user.FirstName = model.FirstName;
                            user.LastName = model.LastName;
                            user.MobileNumber = model.MobileNumber;
                            user.Dob = model.Dob;
                            if (file1 != null)
                            {
                                if (Path.GetExtension(file1.FileName) == ".png" || Path.GetExtension(file1.FileName) == ".jpg" ||
                                    Path.GetExtension(file1.FileName) == ".jpeg" || Path.GetExtension(file1.FileName) == ".gif" ||
                                    Path.GetExtension(file1.FileName) == ".webp")
                                {
                                    if (file1.Length < 4500000)
                                    {
                                        var extention = Path.GetExtension(file1.FileName);
                                        var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                        user.profile_pic = randomName;
                                        var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                                        using (var image = Image.Load(file1.OpenReadStream()))
                                        {
                                            string newSize = ResizeImage(image, 120, 120);
                                            string[] aSize = newSize.Split(',');
                                            image.Mutate(q => q.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                                            image.Save(path);
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                }
                            }
                            if (file2 != null)
                            {
                                if (Path.GetExtension(file2.FileName) == ".png" || Path.GetExtension(file2.FileName) == ".jpg" ||
                                    Path.GetExtension(file2.FileName) == ".jpeg" || Path.GetExtension(file2.FileName) == ".gif" ||
                                    Path.GetExtension(file2.FileName) == ".webp")
                                {
                                    if (file2.Length < 4500000)
                                    {
                                        var extention = Path.GetExtension(file2.FileName);
                                        var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                        user.back_pic = randomName;
                                        var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                                        using (var image = Image.Load(file2.OpenReadStream()))
                                        {
                                            string newSize = ResizeImage(image, 800, 600);
                                            string[] aSize = newSize.Split(',');
                                            image.Mutate(q => q.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                                            image.Save(path);
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                }
                            }
                            if (file3 != null)
                            {
                                if (Path.GetExtension(file3.FileName) == ".png" || Path.GetExtension(file3.FileName) == ".jpg" ||
                                    Path.GetExtension(file3.FileName) == ".jpeg" || Path.GetExtension(file3.FileName) == ".gif" ||
                                    Path.GetExtension(file3.FileName) == ".webp")
                                {
                                    if (file3.Length < 4500000)
                                    {
                                        var extention = Path.GetExtension(file3.FileName);
                                        var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                        user.slider_1 = randomName;
                                        var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                                        using (var image = Image.Load(file3.OpenReadStream()))
                                        {
                                            string newSize = ResizeImage(image, 800, 600);
                                            string[] aSize = newSize.Split(',');
                                            image.Mutate(q => q.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                                            image.Save(path);
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                }
                            }
                            if (file4 != null)
                            {
                                if (Path.GetExtension(file4.FileName) == ".png" || Path.GetExtension(file4.FileName) == ".jpg" ||
                                    Path.GetExtension(file4.FileName) == ".jpeg" || Path.GetExtension(file4.FileName) == ".gif" ||
                                    Path.GetExtension(file4.FileName) == ".webp")
                                {
                                    if (file4.Length < 4500000)
                                    {
                                        var extention = Path.GetExtension(file4.FileName);
                                        var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                        user.slider_2 = randomName;
                                        var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                                        using (var image = Image.Load(file4.OpenReadStream()))
                                        {
                                            string newSize = ResizeImage(image, 800, 600);
                                            string[] aSize = newSize.Split(',');
                                            image.Mutate(q => q.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                                            image.Save(path);
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                }
                            }
                            if (file5 != null)
                            {
                                if (Path.GetExtension(file5.FileName) == ".png" || Path.GetExtension(file5.FileName) == ".jpg" ||
                                    Path.GetExtension(file5.FileName) == ".jpeg" || Path.GetExtension(file5.FileName) == ".gif" ||
                                    Path.GetExtension(file5.FileName) == ".webp")
                                {
                                    if (file5.Length < 4500000)
                                    {
                                        var extention = Path.GetExtension(file5.FileName);
                                        var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                        user.slider_3 = randomName;
                                        var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                                        using (var image = Image.Load(file5.OpenReadStream()))
                                        {
                                            string newSize = ResizeImage(image, 800, 600);
                                            string[] aSize = newSize.Split(',');
                                            image.Mutate(q => q.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                                            image.Save(path);
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                }
                            }
                            var result = await _userManager.UpdateAsync(user);
                            if (result.Succeeded)
                            {
                                await _signInManager.SignOutAsync();
                                return Redirect("~/");
                            }
                        }
                    }
                    else
                    {
                        user.UserName = model.NickName;
                        user.Email = model.Email;
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.MobileNumber = model.MobileNumber;
                        user.Dob = model.Dob;
                        if (file1 != null)
                        {
                            if (Path.GetExtension(file1.FileName) == ".png" || Path.GetExtension(file1.FileName) == ".jpg" ||
                                Path.GetExtension(file1.FileName) == ".jpeg" || Path.GetExtension(file1.FileName) == ".gif" ||
                                Path.GetExtension(file1.FileName) == ".webp")
                            {
                                if (file1.Length < 4500000)
                                {
                                    var extention = Path.GetExtension(file1.FileName);
                                    var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                    user.profile_pic = randomName;
                                    var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                                    using (var image = Image.Load(file1.OpenReadStream()))
                                    {
                                        string newSize = ResizeImage(image, 120, 120);
                                        string[] aSize = newSize.Split(',');
                                        image.Mutate(q => q.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                                        image.Save(path);
                                    }
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                            }
                        }
                        if (file2 != null)
                        {
                            if (Path.GetExtension(file2.FileName) == ".png" || Path.GetExtension(file2.FileName) == ".jpg" ||
                                Path.GetExtension(file2.FileName) == ".jpeg" || Path.GetExtension(file2.FileName) == ".gif" ||
                                Path.GetExtension(file2.FileName) == ".webp")
                            {
                                if (file2.Length < 4500000)
                                {
                                    var extention = Path.GetExtension(file2.FileName);
                                    var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                    user.back_pic = randomName;
                                    var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                                    using (var image = Image.Load(file2.OpenReadStream()))
                                    {
                                        string newSize = ResizeImage(image, 800, 600);
                                        string[] aSize = newSize.Split(',');
                                        image.Mutate(q => q.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                                        image.Save(path);
                                    }
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                            }
                        }
                        if (file3 != null)
                        {
                            if (Path.GetExtension(file3.FileName) == ".png" || Path.GetExtension(file3.FileName) == ".jpg" ||
                                Path.GetExtension(file3.FileName) == ".jpeg" || Path.GetExtension(file3.FileName) == ".gif" ||
                                Path.GetExtension(file3.FileName) == ".webp")
                            {
                                if (file3.Length < 4500000)
                                {
                                    var extention = Path.GetExtension(file3.FileName);
                                    var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                    user.slider_1 = randomName;
                                    var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                                    using (var image = Image.Load(file3.OpenReadStream()))
                                    {
                                        string newSize = ResizeImage(image, 800, 600);
                                        string[] aSize = newSize.Split(',');
                                        image.Mutate(q => q.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                                        image.Save(path);
                                    }
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                            }
                        }
                        if (file4 != null)
                        {
                            if (Path.GetExtension(file4.FileName) == ".png" || Path.GetExtension(file4.FileName) == ".jpg" ||
                                Path.GetExtension(file4.FileName) == ".jpeg" || Path.GetExtension(file4.FileName) == ".gif" ||
                                Path.GetExtension(file4.FileName) == ".webp")
                            {
                                if (file4.Length < 4500000)
                                {
                                    var extention = Path.GetExtension(file4.FileName);
                                    var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                    user.slider_2 = randomName;
                                    var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                                    using (var image = Image.Load(file4.OpenReadStream()))
                                    {
                                        string newSize = ResizeImage(image, 800, 600);
                                        string[] aSize = newSize.Split(',');
                                        image.Mutate(q => q.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                                        image.Save(path);
                                    }
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                            }
                        }
                        if (file5 != null)
                        {
                            if (Path.GetExtension(file5.FileName) == ".png" || Path.GetExtension(file5.FileName) == ".jpg" ||
                                Path.GetExtension(file5.FileName) == ".jpeg" || Path.GetExtension(file5.FileName) == ".gif" ||
                                Path.GetExtension(file5.FileName) == ".webp")
                            {
                                if (file5.Length < 4500000)
                                {
                                    var extention = Path.GetExtension(file5.FileName);
                                    var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                    user.slider_3 = randomName;
                                    var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                                    using (var image = Image.Load(file5.OpenReadStream()))
                                    {
                                        string newSize = ResizeImage(image, 800, 600);
                                        string[] aSize = newSize.Split(',');
                                        image.Mutate(q => q.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                                        image.Save(path);
                                    }
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                            }
                        }

                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            var url = Url.Action("ConfirmEmail", "Account", new
                            {
                                userId = user.Id,
                                token = code
                            });

                            using (MailMessage mm = new MailMessage("info@playpoint.store", $"{user.Email}"))
                            {
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                mm.Subject = "Please confirm your email";
                                mm.Body = "<body>" +
                                "<div  lang=\"en\" style=\"background-color:#121212; color: #f5f5f5; display: flex; padding: 50px 5%;\" >" +
                                "<div style=\"border-radius: 14px; background-color: #292929; padding: 30px 5%; width: 100%;\">" +
                                "<div style=\"text-align:center;\">" +
                                "<img style=\"max-width: 100%; height: 60px;\" src=\"https://playpoint.store/images/playpoint-name-white.svg \" alt=\"\">" +
                                "</div>" +
                                "<div style=\"width: 90%; background-color: #363636; height: 1px ; border-radius: 3px; margin: 0 auto; text-align: center; margin-top: 10px; margin-bottom: 30px;\"></div>" +
                                "<div style=\"font-family: Arial, sans-serif; padding: 0 5%; \">" +
                                $"<div><p>Hi {model.NickName},</p></div>" +
                                "<div><p>We are happy that you have registered with PlayPoint. Please verify your email address to discover new games.</p></div>" +
                                "</div>" +
                                "<div style=\"text-align: center; margin: 25px 0;\">" +
                                $"<a href='https://playpoint.store{url}' style=\"font-family: Arial, sans-serif; display:inline-block; text-decoration: none; background-color: #00cc96; color: #f5f5f5; padding: 8px 3% ; border-radius: 8px;\">Verify email address</a>" +
                                "</div>" +
                                "<div style=\"width: 90%; background-color: #363636; height: 1px ; border-radius: 3px; margin: 0 auto; text-align: center; margin-top: 10px; margin-bottom: 30px;\"></div>" +
                                "<div style=\"text-align: center; margin: 20px 0; \">" +
                                "<a target=\"_blank\" href='https://www.facebook.com/playpoint.store'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/facebook-f-brands.svg \" alt=\"\" ></a>" +
                                "<a target=\"_blank\" href='https://www.youtube.com/channel/UC3Dkcw-Y61QtViw2Gz7o0jA/'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/youtube-brands.svg \"    alt=\"\" ></a>" +
                                "<a target=\"_blank\" href='https://www.instagram.com/playpoint.store/'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/instagram-brands.svg \"  alt=\"\" ></a>" +
                                "<a target=\"_blank\" href='https://discord.gg/pyTeq8bwSN'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/discord-brands.svg \"    alt=\"\" ></a>" +
                                "</div>" +
                                "<div style=\"text-align: center; \">" +
                                "<p style=\"font-family: Arial, sans-serif; font-size: 10px; \">© Copyright.All rights reserved</p>" +
                                "</div>" +
                                "</div>" +
                                "</div>" +
                                "</body>";
                                mm.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = "relay-hosting.secureserver.net";
                                smtp.EnableSsl = true;
                                NetworkCredential NetworkCred = new NetworkCredential("info@playpoint.store", "jY36@ywQV!MZBMv5P@");
                                smtp.UseDefaultCredentials = true;
                                smtp.Credentials = NetworkCred;
                                smtp.EnableSsl = false;
                                smtp.Port = 25;
                                smtp.Send(mm);
                            }

                            await _signInManager.SignOutAsync();
                            return Redirect("/account/login");
                        }
                    }
                }
                return Redirect("/account/usermanage");
            }
            return View(model);
        }

        /////////////////////////////////////////////////// Logout //
        public async Task<IActionResult> Logout() // ++
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        /////////////////////////////////////////////////// Login //
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("~/");
            }
            else
            {
                return View(new LoginModel()
                {
                    ReturnUrl = ReturnUrl
                });
            }

        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "There is no such account");
                return Redirect("/account/notfound");
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Please confirm your email");

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });

                using (MailMessage mm = new MailMessage("info@playpoint.store", $"{user.Email}"))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    mm.Subject = "Confirm your mail";
                    mm.Body = "<body>" +
                                "<div  lang=\"en\" style=\"background-color:#121212; color: #f5f5f5; display: flex; padding: 50px 5%;\" >" +
                                "<div style=\"border-radius: 14px; background-color: #292929; padding: 30px 5%; width: 100%;\">" +
                                "<div style=\"text-align:center;\">" +
                                "<img style=\"max-width: 100%; height: 60px;\" src=\"https://playpoint.store/images/playpoint-name-white.svg \" alt=\"\">" +
                                "</div>" +
                                "<div style=\"width: 90%; background-color: #363636; height: 1px ; border-radius: 3px; margin: 0 auto; text-align: center; margin-top: 10px; margin-bottom: 30px;\"></div>" +
                                "<div style=\"font-family: Arial, sans-serif; padding: 0 5%; \">" +
                                $"<div><p>Hi {user.UserName},</p></div>" +
                                "<div><p>We are happy that you have registered with PlayPoint. Please verify your email address to discover new games.</p></div>" +
                                "</div>" +
                                "<div style=\"text-align: center; margin: 25px 0;\">" +
                                $"<a href='https://playpoint.store{url}' style=\"font-family: Arial, sans-serif; display:inline-block; text-decoration: none; background-color: #00cc96; color: #f5f5f5; padding: 8px 3% ; border-radius: 8px;\">Verify email address</a>" +
                                "</div>" +
                                "<div style=\"width: 90%; background-color: #363636; height: 1px ; border-radius: 3px; margin: 0 auto; text-align: center; margin-top: 10px; margin-bottom: 30px;\"></div>" +
                                "<div style=\"text-align: center; margin: 20px 0; \">" +
                                "<a target=\"_blank\" href='https://www.facebook.com/playpoint.store'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/facebook-f-brands.svg \" alt=\"\" ></a>" +
                                "<a target=\"_blank\" href='https://www.youtube.com/channel/UC3Dkcw-Y61QtViw2Gz7o0jA/'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/youtube-brands.svg \"    alt=\"\" ></a>" +
                                "<a target=\"_blank\" href='https://www.instagram.com/playpoint.store/'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/instagram-brands.svg \"  alt=\"\" ></a>" +
                                "<a target=\"_blank\" href='https://discord.gg/pyTeq8bwSN'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/discord-brands.svg \"    alt=\"\" ></a>" +
                                "</div>" +
                                "<div style=\"text-align: center; \">" +
                                "<p style=\"font-family: Arial, sans-serif; font-size: 10px; \">© Copyright.All rights reserved</p>" +
                                "</div>" +
                                "</div>" +
                                "</div>" +
                                "</body>";
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "relay-hosting.secureserver.net";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("info@playpoint.store", "jY36@ywQV!MZBMv5P@");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.EnableSsl = false;
                    smtp.Port = 25;
                    smtp.Send(mm);
                }

                return Redirect("/account/pleaseconfirm");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "~/");
            }

            ModelState.AddModelError("", "Email or password is wrong");
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult NoAccount()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult PleaseConfirm()
        {
            return View();
        }

        /////////////////////////////////////////////////// Register //
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("~/");
            }
            else
            {
                return View();
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModelF model)
        {
            int emailcount = _userManager.Users.Where(i => i.Email == model.Email).ToList().Count();
            int usernamecount = _userManager.Users.Where(i => i.UserName == model.NickName).ToList().Count();
            if (emailcount >= 1)
            {
                return Redirect("/register/error/email");
            }
            if (usernamecount >= 1)
            {
                return Redirect("/register/error/username");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User()
            {
                UserName = model.NickName,
                Email = model.Email,
                Dob = model.Dob,
                languageID = 1,
                currencyID = 1
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });

                using (MailMessage mm = new MailMessage("info@playpoint.store", $"{user.Email}"))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    mm.Subject = "Confirm your mail";
                    mm.Body = "<body>" +
                                "<div  lang=\"en\" style=\"background-color:#121212; color: #f5f5f5; display: flex; padding: 50px 5%;\" >" +
                                "<div style=\"border-radius: 14px; background-color: #292929; padding: 30px 5%; width: 100%;\">" +
                                "<div style=\"text-align:center;\">" +
                                "<img style=\"max-width: 100%; height: 60px;\" src=\"https://playpoint.store/images/playpoint-name-white.svg \" alt=\"\">" +
                                "</div>" +
                                "<div style=\"width: 90%; background-color: #363636; height: 1px ; border-radius: 3px; margin: 0 auto; text-align: center; margin-top: 10px; margin-bottom: 30px;\"></div>" +
                                "<div style=\"font-family: Arial, sans-serif; padding: 0 5%; \">" +
                                $"<div><p>Hi {model.NickName},</p></div>" +
                                "<div><p>We are happy that you have registered with PlayPoint. Please verify your email address to discover new games.</p></div>" +
                                "</div>" +
                                "<div style=\"text-align: center; margin: 25px 0;\">" +
                                $"<a href='https://playpoint.store{url}' style=\"font-family: Arial, sans-serif; display:inline-block; text-decoration: none; background-color: #00cc96; color: #f5f5f5; padding: 8px 3% ; border-radius: 8px;\">Verify email address</a>" +
                                "</div>" +
                                "<div style=\"width: 90%; background-color: #363636; height: 1px ; border-radius: 3px; margin: 0 auto; text-align: center; margin-top: 10px; margin-bottom: 30px;\"></div>" +
                                "<div style=\"text-align: center; margin: 20px 0; \">" +
                                "<a target=\"_blank\" href='https://www.facebook.com/playpoint.store'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/facebook-f-brands.svg \" alt=\"\" ></a>" +
                                "<a target=\"_blank\" href='https://www.youtube.com/channel/UC3Dkcw-Y61QtViw2Gz7o0jA/'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/youtube-brands.svg \"    alt=\"\" ></a>" +
                                "<a target=\"_blank\" href='https://www.instagram.com/playpoint.store/'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/instagram-brands.svg \"  alt=\"\" ></a>" +
                                "<a target=\"_blank\" href='https://discord.gg/pyTeq8bwSN'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/discord-brands.svg \"    alt=\"\" ></a>" +
                                "</div>" +
                                "<div style=\"text-align: center; \">" +
                                "<p style=\"font-family: Arial, sans-serif; font-size: 10px; \">© Copyright.All rights reserved</p>" +
                                "</div>" +
                                "</div>" +
                                "</div>" +
                                "</body>";
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "relay-hosting.secureserver.net";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("info@playpoint.store", "jY36@ywQV!MZBMv5P@");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.EnableSsl = false;
                    smtp.Port = 25;
                    smtp.Send(mm);
                }

                return RedirectToAction("Inforeg", "Account");

            }

            ModelState.AddModelError("", "Error");
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult AlreadyUseEmail()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult AlreadyUseUsername()
        {
            return View();
        }

        /////////////////////////////////////////////////// ConfirmEmail //
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                ModelState.AddModelError("", "Etibarsız token");
                return Redirect("~/");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                if (await _userManager.IsEmailConfirmedAsync(user))
                {
                    return Redirect("/account/email/confirm/info");
                }

                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    _cartpService.InitializeCart(user.Id);
                    _cartpogService.InitializeCartPOG(user.Id);
                    return View();
                }
            }
            return View("~/");
        }
        [AllowAnonymous]
        public IActionResult AlreadyConfirm()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Inforeg()
        {
            return View();
        }

        /////////////////////////////////////////////////// ForgotPassword //
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(FP f)
        {
            if (string.IsNullOrEmpty(f.Email))
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(f.Email);

            if (user == null)
            {
                return Redirect("/account/info/forgotpassword");
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var url = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = code,
                email = user.Email
            });

            using (MailMessage mm = new MailMessage("info@playpoint.store", $"{user.Email}"))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                mm.Subject = "Reset password";
                mm.Body = "<body>" +
                                "<div  lang=\"en\" style=\"background-color:#121212; color: #f5f5f5; display: flex; padding: 50px 5%;\" >" +
                                "<div style=\"border-radius: 14px; background-color: #292929; padding: 30px 5%; width: 100%;\">" +
                                "<div style=\"text-align:center;\">" +
                                "<img style=\"max-width: 100%; height: 60px;\" src=\"https://playpoint.store/images/playpoint-name-white.svg \" alt=\"\">" +
                                "</div>" +
                                "<div style=\"width: 90%; background-color: #363636; height: 1px ; border-radius: 3px; margin: 0 auto; text-align: center; margin-top: 10px; margin-bottom: 30px;\"></div>" +
                                "<div style=\"font-family: Arial, sans-serif; padding: 0 5%; \">" +
                                $"<div><p>Hi {user.Email},</p></div>" +
                                "<div><p>You have requested a password reset. Please click the link below to reset your password. If you are not, ignore this email.</p></div>" +
                                "</div>" +
                                "<div style=\"text-align: center; margin: 25px 0;\">" +
                                $"<a href='https://playpoint.store{url}' style=\"font-family: Arial, sans-serif; display:inline-block; text-decoration: none; background-color: #00cc96; color: #f5f5f5; padding: 8px 3% ; border-radius: 8px;\">Reset password</a>" +
                                "</div>" +
                                "<div style=\"width: 90 %; background-color: #363636; height: 1px ; border-radius: 3px; margin: 0 auto; text-align: center; margin-top: 10px; margin-bottom: 30px;\"></div>" +
                                "<div style=\"text-align: center; margin: 20px 0; \">" +
                                "<a target=\"_blank\" href='https://www.facebook.com/playpoint.store'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/facebook-f-brands.svg \" alt=\"\" ></a>" +
                                "<a target=\"_blank\" href='https://www.youtube.com/channel/UC3Dkcw-Y61QtViw2Gz7o0jA/'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/youtube-brands.svg \"    alt=\"\" ></a>" +
                                "<a target=\"_blank\" href='https://www.instagram.com/playpoint.store/'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/instagram-brands.svg \"  alt=\"\" ></a>" +
                                "<a target=\"_blank\" href='https://discord.gg/pyTeq8bwSN'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/discord-brands.svg \"    alt=\"\" ></a>" +
                                "</div>" +
                                "<div style=\"text-align: center; \">" +
                                "<p style=\"font-family: Arial, sans-serif; font-size: 10px; \">© Copyright.All rights reserved</p>" +
                                "</div>" +
                                "</div>" +
                                "</div>" +
                                "</body>";
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "relay-hosting.secureserver.net";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("info@playpoint.store", "jY36@ywQV!MZBMv5P@");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.EnableSsl = false;
                smtp.Port = 25;
                smtp.Send(mm);
            }

            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return Redirect("/account/info/forgotpassword"); ;
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (user != null)
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var url = Url.Action("ResetPassword", "Account", new
                    {
                        userId = user.Id,
                        token = code,
                        email = user.Email
                    });

                    using (MailMessage mm = new MailMessage("info@playpoint.store", $"{user.Email}"))
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        mm.Subject = "Change password";
                        mm.Body = "<body>" +
                                "<div  lang=\"en\" style=\"background-color:#121212; color: #f5f5f5; display: flex; padding: 50px 5%;\" >" +
                                "<div style=\"border-radius: 14px; background-color: #292929; padding: 30px 5%; width: 100%;\">" +
                                "<div style=\"text-align:center;\">" +
                                "<img style=\"max-width: 100%; height: 60px;\" src=\"https://playpoint.store/images/playpoint-name-white.svg \" alt=\"\">" +
                                "</div>" +
                                "<div style=\"width: 90%; background-color: #363636; height: 1px ; border-radius: 3px; margin: 0 auto; text-align: center; margin-top: 10px; margin-bottom: 30px;\"></div>" +
                                "<div style=\"font-family: Arial, sans-serif; padding: 0 5%; \">" +
                                $"<div><p>Hi {user.UserName},</p></div>" +
                                "<div><p>This e-mail was sent because you sent a password request. Click the link below to change the password.</p></div>" +
                                "</div>" +
                                "<div style=\"text-align: center; margin: 25px 0;\">" +
                                $"<a href='https://playpoint.store{url}' style=\"font-family: Arial, sans-serif; display:inline-block; text-decoration: none; background-color: #00cc96; color: #f5f5f5; padding: 8px 3% ; border-radius: 8px;\">Change password</a>" +
                                "</div>" +
                                "<div style=\"width: 90%; background-color: #363636; height: 1px ; border-radius: 3px; margin: 0 auto; text-align: center; margin-top: 10px; margin-bottom: 30px;\"></div>" +
                                "<div style=\"text-align: center; margin: 20px 0; \">" +
                                "<a target=\"_blank\" href='https://www.facebook.com/playpoint.store'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/facebook-f-brands.svg \" alt=\"\" ></a>" +
                                "<a target=\"_blank\" href='https://www.youtube.com/channel/UC3Dkcw-Y61QtViw2Gz7o0jA/'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/youtube-brands.svg \"    alt=\"\" ></a>" +
                                "<a target=\"_blank\" href='https://www.instagram.com/playpoint.store/'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/instagram-brands.svg \"  alt=\"\" ></a>" +
                                "<a target=\"_blank\" href='https://discord.gg/pyTeq8bwSN'><img style=\"width: 15px ; height: 15px; color:#cfcfcf; border:1px solid #cfcfcf; padding: 5px ; border-radius: 50%; margin: 0 6px ;\" src=\"https://playpoint.store/images/discord-brands.svg \"    alt=\"\" ></a>" +
                                "</div>" +
                                "<div style=\"text-align: center; \">" +
                                "<p style=\"font-family: Arial, sans-serif; font-size: 10px; \">© Copyright.All rights reserved</p>" +
                                "</div>" +
                                "</div>" +
                                "</div>" +
                                "</body>";
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "relay-hosting.secureserver.net";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential("info@playpoint.store", "jY36@ywQV!MZBMv5P@");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.EnableSsl = false;
                        smtp.Port = 25;
                        smtp.Send(mm);
                    }

                    if (User.Identity.IsAuthenticated)
                    {
                        await _signInManager.SignOutAsync();
                    }
                    return Redirect("~/");
                }
                else
                {
                    return Redirect("~/");
                }
            }
            else
            {
                return Redirect("~/");
            }
        }
        [AllowAnonymous]
        public IActionResult EmailCheck()
        {
            return View();
        }

        /////////////////////////////////////////////////// ResetPassword //
        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ResetPasswordModel
            {
                Token = token,
                userId = userId
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

        /////////////////////////////////////////////////// AccessDenied //
        public IActionResult AccessDenied()
        {
            return View();
        }

        /////////////////////////////////////////////////// AccessDenied //
        public IActionResult About()
        {
            return View();
        }

        /////////////////////////////////////////////////// List //
        public async Task<IActionResult> Library()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user.Xboxexpire > DateTime.Now)
            {
                return View(new PurchasedProductListViewModel()
                {
                    PPs = _ppService.GetAll().Where(i => i.UserId == user.Id && i.IsXbox == false).ToList(),
                    Pros = _proService.GetAll(),
                    Gns = _gnService.GetAll(),
                    Divs = _divService.GetAll(),
                    Plat = _platService.GetAll(),
                    Xbox = _xdService.GetAll().FirstOrDefault(),
                    XboxExpire = user.Xboxexpire
                });
            }
            else
            {
                return View(new PurchasedProductListViewModel()
                {
                    PPs = _ppService.GetAll().Where(i => i.UserId == user.Id && i.IsXbox == false).ToList(),
                    Pros = _proService.GetAll(),
                    Gns = _gnService.GetAll(),
                    Divs = _divService.GetAll(),
                    Plat = _platService.GetAll(),
                    XboxExpire = user.Xboxexpire
                });
            }
        }

        public async Task<IActionResult> LibraryOFSell()  // Satış Siyahısı
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            return View(new Product_of_GamerListViewModel()
            {
                Pogs = _pogService.GetAll().Where(i => i.UserId == user.Id).ToList(),
                Gns = _gnService.GetAll(),
                Divs = _divService.GetAll()
            });
        }

        [HttpPost]
        public async Task<IActionResult> BuyProduct()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var cart = _cartService.GetAll();
            var cartitem = _cipService.GetAll();
            var pros = _proService.GetAll();
            double totalprice = 0;
            int cartId = 0;
            foreach (var q in cart.Where(i => i.UserId == user.Id))
            {
                cartId = q.Id;
                break;
            }
            foreach (var c in cartitem)
            {
                if (c.CartId == cartId)
                {
                    foreach (var w in pros)
                    {
                        if (c.ProductId == w.Id)
                        {
                            if (w.Discount_percent > 0)
                            {
                                totalprice += w.Price - (w.Price * w.Discount_percent) / 100;
                            }
                            else
                            {
                                totalprice += w.Price;
                            }
                        }
                    }
                }
            }

            if (totalprice > user.Balance)
            {
                return Redirect("/account/addbalance");
            }
            else
            {
                foreach (var q in cart.Where(i => i.UserId == user.Id))
                {
                    foreach (var c in cartitem)
                    {
                        if (c.CartId == q.Id)
                        {
                            var entity = new PurchasedProduct()
                            {
                                UserId = user.Id,
                                ProductId = c.ProductId,
                                IsApproved = true,
                                IsXbox = false,
                                DateTime = DateTime.Now
                            };
                            _ppService.Create(entity);
                            _cartService.DeleteFromCartP(user.Id, c.ProductId);

                            var entity1 = _proService.GetById(c.ProductId);
                            entity1.Number_of_sale += 1;
                            _proService.Update(entity1);

                            if (entity1.Discount_percent == 0)
                            {
                                var entity2 = new PaymentPHistory()
                                {
                                    UserId = user.Id,
                                    ProductId = c.ProductId,
                                    PaymentBalance = entity1.Price,
                                    IsXbox = false,
                                    BuyDate = DateTime.Now
                                };
                                _pphService.Create(entity2);
                            }
                            else
                            {
                                var entity2 = new PaymentPHistory()
                                {
                                    UserId = user.Id,
                                    ProductId = c.ProductId,
                                    PaymentBalance = entity1.Price - (entity1.Price * entity1.Discount_percent) / 100,
                                    IsXbox = false,
                                    BuyDate = DateTime.Now
                                };
                                _pphService.Create(entity2);
                            }
                        }
                    }
                    break;
                }
                user.Balance -= totalprice;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Library");
                }
                return RedirectToAction("Library");
            }
        }

        [HttpPost]
        public async Task<IActionResult> BuyPOG(double totalprice) // ???
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var cart = _cpogService.GetAll();
            var cartitem = _cipogService.GetAll();

            if (totalprice > user.Balance)
            {
                return Redirect("/account/addbalance");
            }
            else
            {
                foreach (var q in cart.Where(i => i.UserId == user.Id))
                {
                    foreach (var c in cartitem)
                    {
                        if (c.CartId == q.Id)
                        {
                            var entity = new PurchasedPOG()
                            {
                                UserId = user.Id,
                                POGId = c.POGId,
                                IsApproved = true,
                                DateTime = DateTime.Now
                            };
                            _ppogService.Create(entity);
                            _cpogService.DeleteFromCartPOG(user.Id, c.POGId);
                            // elave
                        }
                    }
                    break;
                }
                user.Balance -= totalprice;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("LibraryPOG");
                }
                return RedirectToAction("LibraryPOG");
            }
        }

        /////////////////////////////////////////////////// Delete //
        [HttpPost]
        public IActionResult DeletePOG(int Id)
        {
            var entity = _pogService.GetById(Id);
            if (entity != null)
            {
                string Slider_img1;
                string Slider_img2;
                string Slider_img3;
                if (entity.Slider_img1 != null)
                {
                    Slider_img1 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\pog", entity.Slider_img1);
                    FileInfo fi = new FileInfo(Slider_img1);
                    if (fi != null)
                    {
                        System.IO.File.Delete(Slider_img1);
                        fi.Delete();
                    }
                }
                if (entity.Slider_img2 != null)
                {
                    Slider_img2 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\pog", entity.Slider_img2);
                    FileInfo fi1 = new FileInfo(Slider_img2);
                    if (fi1 != null)
                    {
                        System.IO.File.Delete(Slider_img2);
                        fi1.Delete();
                    }
                }
                if (entity.Slider_img3 != null)
                {
                    Slider_img3 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\pog", entity.Slider_img3);
                    FileInfo fi2 = new FileInfo(Slider_img3);
                    if (fi2 != null)
                    {
                        System.IO.File.Delete(Slider_img3);
                        fi2.Delete();
                    }
                }
                _pogService.Delete(entity);
            }
            return RedirectToAction("LibraryOfSell");
        }

        /////////////////////////////////////////////////// Create //
        public IActionResult CreatePOG()
        {
            var entity0 = _gnService.GetAll();
            var entity1 = _divService.GetAll();
            var model = new Product_of_GamerModel()
            {
                Gns = entity0.ToList(),
                Divs = entity1.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePOG(Product_of_GamerModel model, IFormFile file1, IFormFile file2, IFormFile file3)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (ModelState.IsValid)
            {
                var entity = new Product_of_Gamer()
                {
                    UserId = user.Id,
                    GameNameID = model.GameNameID,
                    DiviceID = model.DiviceID,
                    Login = model.Login,
                    Password = model.Password,
                    Price = model.Price,
                    Text = model.Text,
                    Slider_videolink = model.Slider_videolink
                };
                if (file1 != null)
                {
                    if (Path.GetExtension(file1.FileName) == ".png" || Path.GetExtension(file1.FileName) == ".jpg" ||
                        Path.GetExtension(file1.FileName) == ".jpeg" || Path.GetExtension(file1.FileName) == ".gif" ||
                        Path.GetExtension(file1.FileName) == ".webp" || Path.GetExtension(file1.FileName) == ".svg")
                    {
                        if (file1.Length < 4500000)
                        {
                            var extention = Path.GetExtension(file1.FileName);
                            var randomName = string.Format($"img_1_{Guid.NewGuid()}{extention}");
                            entity.Slider_img1 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\pog", randomName);

                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file1.CopyToAsync(stream);
                            }
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                    }
                }
                if (file2 != null)
                {
                    if (Path.GetExtension(file2.FileName) == ".png" || Path.GetExtension(file2.FileName) == ".jpg" ||
                        Path.GetExtension(file2.FileName) == ".jpeg" || Path.GetExtension(file2.FileName) == ".gif" ||
                        Path.GetExtension(file2.FileName) == ".webp" || Path.GetExtension(file2.FileName) == ".svg")
                    {
                        if (file2.Length < 4500000)
                        {
                            var extention = Path.GetExtension(file2.FileName);
                            var randomName = string.Format($"img_2_{Guid.NewGuid()}{extention}");
                            entity.Slider_img2 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\pog", randomName);

                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file2.CopyToAsync(stream);
                            }
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                    }
                }
                if (file3 != null)
                {
                    if (Path.GetExtension(file3.FileName) == ".png" || Path.GetExtension(file3.FileName) == ".jpg" ||
                       Path.GetExtension(file3.FileName) == ".jpeg" || Path.GetExtension(file3.FileName) == ".gif" ||
                       Path.GetExtension(file3.FileName) == ".webp" || Path.GetExtension(file3.FileName) == ".svg")
                    {
                        if (file3.Length < 4500000)
                        {
                            var extention = Path.GetExtension(file3.FileName);
                            var randomName = string.Format($"img_3_{Guid.NewGuid()}{extention}");
                            entity.Slider_img3 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\pog", randomName);

                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file3.CopyToAsync(stream);
                            }
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                    }
                }

                _pogService.Create(entity);
                return RedirectToAction("LibraryOfSell");
            }
            return View(model);
        }

        /////////////////////////////////////////////////// Edit //
        public IActionResult EditPOG(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _pogService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var entity0 = _gnService.GetAll();
            var entity1 = _divService.GetAll();

            var model = new Product_of_GamerModel()
            {
                Id = entity.Id,
                GameNameID = entity.GameNameID,
                DiviceID = entity.DiviceID,
                Login = entity.Login,
                Password = entity.Password,
                Price = entity.Price,
                Text = entity.Text,
                Slider_img1 = entity.Slider_img1,
                Slider_img2 = entity.Slider_img2,
                Slider_img3 = entity.Slider_img3,
                Slider_videolink = entity.Slider_videolink,
                Gns = entity0.ToList(),
                Divs = entity1.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditPOG(Product_of_GamerModel model, IFormFile file1, IFormFile file2, IFormFile file3)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (ModelState.IsValid)
            {
                var entity = _pogService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                string Slider_img1;
                string Slider_img2;
                string Slider_img3;
                if (file1 != null)
                {
                    if (entity.Slider_img1 != null)
                    {
                        Slider_img1 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\pog", entity.Slider_img1);
                        FileInfo fi1 = new FileInfo(Slider_img1);
                        if (fi1 != null)
                        {
                            System.IO.File.Delete(Slider_img1);
                            fi1.Delete();
                        }
                    }
                }
                if (file2 != null)
                {
                    if (entity.Slider_img2 != null)
                    {
                        Slider_img2 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\pog", entity.Slider_img2);
                        FileInfo fi2 = new FileInfo(Slider_img2);
                        if (fi2 != null)
                        {
                            System.IO.File.Delete(Slider_img2);
                            fi2.Delete();
                        }
                    }
                }
                if (file3 != null)
                {
                    if (entity.Slider_img3 != null)
                    {
                        Slider_img3 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\pog", entity.Slider_img3);
                        FileInfo fi3 = new FileInfo(Slider_img3);
                        if (fi3 != null)
                        {
                            System.IO.File.Delete(Slider_img3);
                            fi3.Delete();
                        }
                    }
                }

                entity.GameNameID = model.GameNameID;
                entity.DiviceID = model.DiviceID;
                entity.Login = model.Login;
                entity.Password = model.Password;
                entity.Price = model.Price;
                entity.Text = model.Text;
                entity.Slider_videolink = model.Slider_videolink;
                if (file1 != null)
                {
                    if (Path.GetExtension(file1.FileName) == ".png" || Path.GetExtension(file1.FileName) == ".jpg" ||
                        Path.GetExtension(file1.FileName) == ".jpeg" || Path.GetExtension(file1.FileName) == ".gif" ||
                        Path.GetExtension(file1.FileName) == ".webp" || Path.GetExtension(file1.FileName) == ".svg")
                    {
                        if (file1.Length < 4500000)
                        {
                            var extention = Path.GetExtension(file1.FileName);
                            var randomName = string.Format($"img_1_{Guid.NewGuid()}{extention}");
                            entity.Slider_img1 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\pog", randomName);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file1.CopyToAsync(stream);
                            }
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                    }
                }
                if (file2 != null)
                {
                    if (Path.GetExtension(file2.FileName) == ".png" || Path.GetExtension(file2.FileName) == ".jpg" ||
                        Path.GetExtension(file2.FileName) == ".jpeg" || Path.GetExtension(file2.FileName) == ".gif" ||
                        Path.GetExtension(file2.FileName) == ".webp" || Path.GetExtension(file2.FileName) == ".svg")
                    {
                        if (file2.Length < 4500000)
                        {
                            var extention = Path.GetExtension(file2.FileName);
                            var randomName = string.Format($"img_2_{Guid.NewGuid()}{extention}");
                            entity.Slider_img2 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\pog", randomName);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file2.CopyToAsync(stream);
                            }
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                    }
                }
                if (file3 != null)
                {
                    if (Path.GetExtension(file3.FileName) == ".png" || Path.GetExtension(file3.FileName) == ".jpg" ||
                        Path.GetExtension(file3.FileName) == ".jpeg" || Path.GetExtension(file3.FileName) == ".gif" ||
                        Path.GetExtension(file3.FileName) == ".webp" || Path.GetExtension(file3.FileName) == ".svg")
                    {
                        if (file3.Length < 4500000)
                        {
                            var extention = Path.GetExtension(file3.FileName);
                            var randomName = string.Format($"img_3_{Guid.NewGuid()}{extention}");
                            entity.Slider_img3 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\pog", randomName);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file3.CopyToAsync(stream);
                            }
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                    }
                }

                _pogService.Update(entity);
                return RedirectToAction("LibraryOfSell");
            }
            return View(model);
        }

        /////////////////////////////////////////////////// Picture & Balance //
        public async Task<string> GetPicture()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string text;
            string empty = "/static/img/users/asd.png";
            if (user != null)
            {
            }
            if (user.profile_pic != null)
            {
                text = $"/static/img/users/{user.profile_pic}";
                return text;
            }
            else
            {
                return empty;
            }
        }

        public async Task<string> GetBalance()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var cur = _curService.GetAll();
            string text;
            string empty = "0";
            if (user != null)
            {
            }
            foreach (var q in cur)
            {
                if (user.currencyID == q.Id)
                {
                    text = Math.Round((user.Balance * q.CurrencyConst), 2).ToString() + " " + q.CurrencyIcon;
                    if (string.IsNullOrEmpty(text))
                    {
                        return empty;
                    }
                    else
                    {
                        return text;
                    }
                }
            }

            text = user.Balance.ToString();
            if (string.IsNullOrEmpty(text))
            {
                return empty;
            }
            else
            {
                return text;
            }
        }

        /////////////////////////////////////////////////// Xbox //
        [HttpGet]
        public async Task<IActionResult> XboxPurchased()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                string exp = "";
                if (user.Xboxexpire > DateTime.Now)
                {
                    exp = user.Xboxexpire.ToShortDateString();
                }
                else
                {
                    exp = DateTime.Now.ToShortDateString();
                }
                string dd = "";
                string mm = "";
                string yyyy = "";
                if (user.Xboxexpire > DateTime.Now)
                {
                    dd = user.Xboxexpire.Day.ToString();
                    mm = user.Xboxexpire.Month.ToString();
                    yyyy = user.Xboxexpire.Year.ToString();
                }
                else
                {
                    dd = DateTime.Now.Day.ToString();
                    mm = DateTime.Now.Month.ToString();
                    yyyy = DateTime.Now.Year.ToString();
                }
                var xdViewModel = new XboxdataListViewModel()
                {
                    Curs = _curService.GetAll().ToList(),
                    Cur = user.currencyID,
                    Xboxdata = _xdService.GetAll().FirstOrDefault(),
                    start = user.Xboxstart,
                    expire = user.Xboxexpire,
                    day = dd,
                    month = mm,
                    year = yyyy
                };
                return View(xdViewModel);
            }
            else
            {
                return Redirect("/account/login");
            }
        }
        [HttpPost]
        public async Task<IActionResult> XboxPurchased(int sub_accept)
        {
            if (sub_accept > 12)
            {
                sub_accept = 12;
            }
            else if (sub_accept < 0)
            {
                sub_accept = 1;
            }
            else { }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var xbox = _xdService.GetAll().FirstOrDefault();
            DateTime dt_now = DateTime.Now;
            int dt_month = dt_now.Month + 1;
            int mm = 0;
            int yyyy = 0;
            if (user.Xboxexpire > DateTime.Now)
            {
                mm += user.Xboxexpire.Month + sub_accept;
                if (mm > 12)
                {
                    mm = mm - 12;
                    yyyy += 1;
                }
                yyyy += user.Xboxexpire.AddMonths(sub_accept).Year;
            }
            else
            {
                mm += dt_now.Month + sub_accept;
                if (mm > 12)
                {
                    mm = mm - 12;
                    yyyy += 1;
                }
                yyyy += dt_now.AddMonths(sub_accept).Year;
            }

            int fdd = new DateTime(yyyy, mm, 1).AddMonths(1).Day;
            DateTime lastday = new DateTime(yyyy, mm, fdd, 0, 0, 0);
            DateTime nmfd = new DateTime(dt_now.Year, dt_month, 1);

            int totaldays = 1;
            if (user.Xboxexpire < DateTime.Now)
            {
                if (sub_accept >= 1)
                {
                    double days = (lastday - dt_now).TotalDays;
                    totaldays += Convert.ToInt32(Math.Floor(days));
                    if (user.Balance >= totaldays * xbox.Price)
                    {
                        var entity = new PurchasedProduct()
                        {
                            UserId = user.Id,
                            ProductId = xbox.Id,
                            IsApproved = true,
                            IsXbox = true,
                            DateTime = DateTime.Now
                        };
                        _ppService.Create(entity);

                        var entity2 = new PaymentPHistory()
                        {
                            UserId = user.Id,
                            ProductId = xbox.Id,
                            PaymentBalance = totaldays * xbox.Price,
                            IsXbox = true,
                            BuyDate = DateTime.Now
                        };
                        _pphService.Create(entity2);

                        user.Balance -= totaldays * xbox.Price;
                        user.Xboxstart = DateTime.Now;
                        user.xboxdatebuy = DateTime.Now;
                        user.Xboxexpire = lastday;
                        user.xboxdateexpire = lastday;
                        var result = await _userManager.UpdateAsync(user);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Library");
                        }
                    }
                    else
                    {
                        return Redirect("/account/addbalance");
                    }
                }
                else
                {
                    return Redirect("/xbox/xboxgames");
                }
            }
            else if (user.Xboxexpire >= DateTime.Now)
            {
                if (sub_accept >= 1)
                {
                    int totalday = 0;
                    double days = (user.Xboxexpire.AddMonths(sub_accept) - user.Xboxexpire).TotalDays;
                    totalday += Convert.ToInt32(days);
                    if (user.Balance >= totalday * xbox.Price)
                    {
                        var entity = new PurchasedProduct()
                        {
                            UserId = user.Id,
                            ProductId = xbox.Id,
                            IsApproved = true,
                            IsXbox = true,
                            DateTime = DateTime.Now
                        };
                        _ppService.Create(entity);

                        var entity2 = new PaymentPHistory()
                        {
                            UserId = user.Id,
                            ProductId = xbox.Id,
                            PaymentBalance = totalday * xbox.Price,
                            IsXbox = true,
                            BuyDate = DateTime.Now
                        };
                        _pphService.Create(entity2);

                        user.Balance -= totalday * xbox.Price;
                        user.Xboxstart = DateTime.Now;
                        user.xboxdatebuy = DateTime.Now;
                        user.Xboxexpire = user.Xboxexpire.AddMonths(sub_accept);
                        user.xboxdateexpire = user.Xboxexpire.AddMonths(sub_accept);
                        var result = await _userManager.UpdateAsync(user);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Library");
                        }
                    }
                    else
                    {
                        return Redirect("/account/addbalance");
                    }
                }
                else
                {
                    return Redirect("/xbox/xboxgames");
                }
            }
            return RedirectToAction("Library");
        }

        /////////////////////////////////////////////////// Contract //
        [AllowAnonymous]
        public IActionResult Contract()
        {
            return View();
        }

        /////////////////////////////////////////////////// Money //
        public async Task<string> GetTotal()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var carts = _cartpService.GetAll();
            var ci = _cipService.GetAll();
            var pro = _proService.GetAll();
            var curs = _curService.GetAll();
            double a = 0;
            string total = "";
            if (user != null)
            {
            }

            foreach (var w in carts)
            {
                if (w.UserId == user.Id)
                {
                    foreach (var q in ci)
                    {
                        if (w.Id == q.CartId)
                        {
                            foreach (var e in pro)
                            {
                                if (e.Id == q.ProductId)
                                {
                                    if (e.Discount_percent == 0)
                                    {
                                        foreach (var c in curs)
                                        {
                                            if (user.currencyID == c.Id)
                                            {
                                                a += e.Price * c.CurrencyConst;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        foreach (var c in curs)
                                        {
                                            if (user.currencyID == c.Id)
                                            {
                                                a += (e.Price - (e.Price * e.Discount_percent) / 100) * c.CurrencyConst;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
            total = $"{Math.Round(a, 2)}";
            return total;
        }

        public async Task<string> GetCur()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var curs = _curService.GetAll();
            string cur = "";
            if (user != null)
            {
            }

            foreach (var w in curs)
            {
                if (w.Id == user.currencyID)
                {
                    cur = w.CurrencyIcon;
                }
            }
            return cur;
        }

        public async Task<List<CartItemsAir>> GetCart()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var carts = _cartpService.GetAll();
            var ci = _cipService.GetAll();
            var pro = _proService.GetAll();
            var curs = _curService.GetAll();
            List<CartItemsAir> cip = new List<CartItemsAir>();

            if (user != null)
            {
            }

            foreach (var w in carts)
            {
                if (w.UserId == user.Id)
                {
                    foreach (var q in ci)
                    {
                        if (w.Id == q.CartId)
                        {
                            foreach (var e in pro)
                            {
                                if (e.Id == q.ProductId)
                                {
                                    if (e.Discount_percent == 0)
                                    {
                                        CartItemsAir cia = new CartItemsAir();
                                        cia.Id = e.Id;
                                        cia.PName = e.Name;
                                        cia.PImg = e.Main_img;
                                        foreach (var c in curs)
                                        {
                                            if (user.currencyID == c.Id)
                                            {
                                                cia.PPrice = Math.Round(e.Price * c.CurrencyConst, 2);
                                                cia.Cur = c.CurrencyName;
                                            }
                                        }
                                        cip.Add(cia);
                                    }
                                    else
                                    {
                                        CartItemsAir cia = new CartItemsAir();
                                        cia.Id = e.Id;
                                        cia.PName = e.Name;
                                        cia.PImg = e.Main_img;
                                        foreach (var c in curs)
                                        {
                                            if (user.currencyID == c.Id)
                                            {
                                                cia.PPrice = Math.Round((e.Price - (e.Price * e.Discount_percent) / 100) * c.CurrencyConst, 2);
                                                cia.Cur = c.CurrencyName;
                                            }
                                        }
                                        cip.Add(cia);
                                    }
                                }
                            }

                        }
                    }
                }
            }
            return cip;
        }

        public async Task<IActionResult> AddBalance()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (!user.EmailConfirmed)
                {
                    return Redirect("~/");
                }

                var model = new AddBalanceModel()
                {
                    User = user
                };
                return View(model);
            }
            else
            {
                return Redirect("~/");
            }
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var entity = _userManager.Users.ToList();
            var entity1 = _gnService.GetAll();
            var entity2 = _proService.GetAll();
            var entity3 = _pogService.GetAll();

            var model = new CompleteOrderModel()
            {
                User = user,
                Users = entity,
                Pros = entity2,
                Pogs = entity3
            };
            return View(model);
        }

        public async Task<IActionResult> Payment(AddBalanceModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/account/addbalance");
            }
            else
            {
                int a = 0;
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                int balnum = _bService.GetAll().Count();
                if (balnum == 0)
                {
                    a += 1;
                }
                else
                {
                    var orderlist = _bService.GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
                    a = orderlist.Id + 1;
                }
                var lanlist = _lanService.GetAll();
                string lan = "en";
                foreach (var e in lanlist)
                {
                    if (user.languageID == e.Id)
                    {
                        if (e.LanguageTag == "tr")
                        {
                            lan = "en";
                            break;
                        }
                        else
                        {
                            lan = e.LanguageTag;
                            break;
                        }
                    }
                }

                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ".";
                provider.NumberGroupSeparator = ",";
                double dbl = Convert.ToDouble(model.amount, provider);
                if (dbl < 0)
                {
                    dbl = dbl * (-1);
                }
                else if (dbl == 0)
                {
                    dbl = 1;
                }
                double usddbl = dbl * 1.7;
                int tam = Convert.ToInt32(Math.Floor(usddbl));
                double onda = (Math.Round(usddbl - tam, 2)) * 100;
                var rndm = RandomString(20);
                var entity = new BalanceInfo()
                {
                    UserId = user.Id,
                    FullName = user.LastName + " " + user.FirstName + " " + user.UserName,
                    Date = DateTime.Now,
                    IsApproved = false,
                    PayBtnInfo = false,
                    Amount = dbl,
                    InvoiceNum = rndm
                };
                _bService.Create(entity);
                PaymentModel add = new PaymentModel();
                add.public_key = "i000200101";
                add.currency = "AZN";
                add.language = lan;
                add.description = $"{user.UserName}_info-payment_{rndm}";
                add.order_id = rndm;
                add.amount = $"{tam}.{onda}";
                string output = JsonConvert.SerializeObject(add);


                string private_key = "geMEO4FUFhR5qjCpgMXZ3SoG";
                PaymentModel rtlModel = JsonConvert.DeserializeObject<PaymentModel>(output);
                string data = Base64Encode(output);
                string sgn_string = private_key + data + private_key;
                string signature = Hash(sgn_string);
                //string signature = Base64Encode(signaturestring);

                var model1 = new AddBalanceModel()
                {
                    data = data,
                    signature = signature,
                    amount = $"{dbl}",
                    User = user,
                    BIM = _bService.GetAll().Where(i => i.UserId == user.Id && i.Amount == dbl && i.InvoiceNum == rndm).OrderByDescending(x => x.Id).ToList().FirstOrDefault()
                };
                return View(model1);
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Hash(string input)
        {
            using var sha1 = SHA1.Create();
            return Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZqazwsxedcrfvtgbyhnujmikolp0123456789";
            var rndm = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            int bal = _bService.GetAll().Where(i => i.InvoiceNum == rndm).Count();
            if (bal == 1)
            {
                return RandomString(20);
            }
            return rndm;
        }

        public string ResizeImage(Image img, int maxWidth, int maxHeight)
        {
            if (img.Width > maxWidth || img.Height > maxHeight)
            {
                double widthRatio = (double)img.Width / (double)maxWidth;
                double heightRatio = (double)img.Height / (double)maxHeight;
                double ratio = Math.Max(widthRatio, heightRatio);
                int newWidth = (int)(img.Width / ratio);
                int newHeight = (int)(img.Height / ratio);
                return newHeight.ToString() + "," + newWidth.ToString();
            }
            else
            {
                return img.Height.ToString() + "," + img.Width.ToString();
            }
        }

        public void EditBalanceInfo(int ID)
        {
            var entity = _bService.GetById(ID);
            if (entity == null)
            {

            }
            else
            {
                entity.PayBtnInfo = true;
                _bService.Update(entity);
            }
        }

        public IActionResult VzgdV7Lh22RaYmxgO6nrbnCPc()
        {
            return Redirect("/methodbs");
        }  // sucsses part 1
        public async Task<IActionResult> SuccessF()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var balinfo = _bService.GetAll().Where(i => i.UserId == user.Id && i.IsApproved == false && string.IsNullOrEmpty(i.SE) && i.PayBtnInfo == true).OrderByDescending(x=>x.Id).FirstOrDefault();
                if (balinfo == null)
                {
                    return Redirect("~/");
                }
                user.Balance += balinfo.Amount;
                await _userManager.UpdateAsync(user);
                var entity = _bService.GetById(balinfo.Id);
                entity.SE = "success";
                entity.IsApproved = true;
                _bService.Update(entity);
                var balinfos = _bService.GetAll().Where(i => i.UserId == user.Id && i.IsApproved == false && string.IsNullOrEmpty(i.SE) && i.PayBtnInfo == true);
                foreach (var e in balinfos)
                {
                    var entity1 = _bService.GetById(e.Id);
                    entity1.SE = "cancel";
                    entity1.IsApproved = true;
                    _bService.Update(entity1);
                }
                var balinfos2 = _bService.GetAll().Where(i => i.UserId == user.Id && i.IsApproved == false && string.IsNullOrEmpty(i.SE) && i.PayBtnInfo == false);
                foreach (var e in balinfos)
                {
                    var entity1 = _bService.GetById(e.Id);
                    _bService.Delete(entity1);
                }
                return Redirect("/balance/info/success");
            }
            else
            {
                return Redirect("~/");
            }
        }       // sucsses part 2
        public IActionResult Success()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return Redirect("~/");
            }
        }                    // sucsses part 3

        public IActionResult RywzGJlXvg4D2UuWqknGsGV8r()
        {
            return Redirect("/methodbe");
        }  // error part 1
        public async Task<IActionResult> ErrorF()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var balinfo = _bService.GetAll().Where(i => i.UserId == user.Id && i.IsApproved == false && string.IsNullOrEmpty(i.SE) && i.PayBtnInfo == true).OrderByDescending(x => x.Id).FirstOrDefault();
                if (balinfo == null)
                {
                    return Redirect("~/");
                }
                var entity = _bService.GetById(balinfo.Id);
                entity.SE = "error";
                entity.IsApproved = true;
                _bService.Update(entity);
                var balinfos = _bService.GetAll().Where(i => i.UserId == user.Id && i.IsApproved == false && string.IsNullOrEmpty(i.SE) && i.PayBtnInfo == true);
                foreach (var e in balinfos)
                {
                    var entity1 = _bService.GetById(e.Id);
                    entity1.SE = "cancel";
                    entity1.IsApproved = true;
                    _bService.Update(entity1);
                }
                var balinfos2 = _bService.GetAll().Where(i => i.UserId == user.Id && i.IsApproved == false && string.IsNullOrEmpty(i.SE) && i.PayBtnInfo == false);
                foreach (var e in balinfos)
                {
                    var entity1 = _bService.GetById(e.Id);
                    _bService.Delete(entity1);
                }
                return Redirect("/balance/info/error");
            }
            else
            {
                return Redirect("~/");
            }
        }         // error part 2
        public IActionResult Error()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return Redirect("~/");
            }
        }                      // error part 3

        public IActionResult Result()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return Redirect("~/");
            }
        }
    }
}