using gamesell.business.Abstract;
using gamesell.entity;
using gamesellMVC.EmailServices;
using gamesellMVC.Extensions;
using gamesellMVC.Identity;
using gamesellMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Controllers
{
    [Authorize(Roles = "Moderator")]
    public class AdminController : Controller
    {
        private IActivationCountryService _acService;
        private ICartPService _cartService;
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
        private IGameItemService _giService;
        private ILanguageTextService _ltService;
        private IInstructionPanelService _ipService;
        private IIndexSliderService _isService;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        private RoleManager<IdentityRole> _roleManager;
        private IXboxdataService _xdService;
        private IXboxgameService _xgService;
        private IPaymentPHistoryService _pphService;
        private IPaymentPOGHistoryService _ppoghService;

        public AdminController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender,
                                IActivationCountryService acService, ICameraPerspectiveServices cpService, ICurrencyService curService,
                                IDeveloperService devService, IDiviceService divService, IGameCategoryService gcService,
                                IGameNameService gnService, IJanraService janService, ILanguageService lanService,
                                IPlatformService platService, IProductOfGamerService pogService, IProductService proService,
                                IPublisherService pubService, IPurchasedPOGService ppogService, IPurchasedProductService ppService,
                                RoleManager<IdentityRole> roleManager, IGameItemService giService, ILanguageTextService ltService,
                                IInstructionPanelService ipService, IIndexSliderService isService, IXboxdataService xdService,
                                IXboxgameService xgService, IPaymentPHistoryService pphService, IPaymentPOGHistoryService ppoghService,
                                ICartPService cartService)

        {
            _cartService = cartService;
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

        public IActionResult Home()
        {
            return View();
        }

        /////////////////////////////////////////////////// User //
        public IActionResult UserList()
        {
            return View(new UserListViewModel()
            {
                Users = _userManager.Users.Where(i => i.IsApproved).ToList(),
                Langs = _lanService.GetAll(),
                Curs = _curService.GetAll()
            });
        }

        public IActionResult UserDeletedList()
        {
            return View(new UserListViewModel()
            {
                Users = _userManager.Users.Where(i => i.IsApproved == false).ToList(),
                Langs = _lanService.GetAll(),
                Curs = _curService.GetAll()
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return RedirectToAction("UserList");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("UserList");
                }

                return RedirectToAction("UserList");
            }
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var entity = _lanService.GetAll();
            var entity1 = _curService.GetAll();
            if (user != null)
            {
                var selectedRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles.Select(i => i.Name);

                ViewBag.Roles = roles;
                return View(new UserDetailsModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MobileNumber = user.MobileNumber,
                    IsApproved = user.IsApproved,
                    Dob = user.Dob,
                    profile_pic = user.profile_pic,
                    back_pic = user.back_pic,
                    slider_1 = user.slider_1,
                    slider_2 = user.slider_2,
                    slider_3 = user.slider_3,
                    languageID = user.languageID,
                    currencyID = user.currencyID,
                    premium = user.premium,
                    seller_rank = user.seller_rank,
                    like = user.like,
                    dislike = user.dislike,

                    SelectedRoles = selectedRoles,
                    Langs = entity.ToList(),
                    Curs = entity1.ToList()
                });
            }
            return RedirectToAction("UserList");
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(UserDetailsModel model, string[] selectedRoles, IFormFile file1, IFormFile file2, IFormFile file3, IFormFile file4, IFormFile file5)
        {
            if (ModelState.IsValid)
            {
                string profile_pic;
                string back_pic;
                string slider_1;
                string slider_2;
                string slider_3;
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    if (file1 != null)
                    {
                        profile_pic = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", user.profile_pic);
                        FileInfo fi1 = new FileInfo(profile_pic);
                        if (fi1 != null)
                        {
                            System.IO.File.Delete(profile_pic);
                            fi1.Delete();
                        }
                        else { }
                    }
                    if (file2 != null)
                    {
                        back_pic = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", user.back_pic);
                        FileInfo fi2 = new FileInfo(back_pic);
                        if (fi2 != null)
                        {
                            System.IO.File.Delete(back_pic);
                            fi2.Delete();
                        }
                        else { }
                    }
                    if (file3 != null)
                    {
                        slider_1 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", user.slider_1);
                        FileInfo fi3 = new FileInfo(slider_1);
                        if (fi3 != null)
                        {
                            System.IO.File.Delete(slider_1);
                            fi3.Delete();
                        }
                        else { }
                    }
                    if (file4 != null)
                    {
                        slider_2 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", user.slider_2);
                        FileInfo fi4 = new FileInfo(slider_2);
                        if (fi4 != null)
                        {
                            System.IO.File.Delete(slider_2);
                            fi4.Delete();
                        }
                        else { }
                    }
                    if (file5 != null)
                    {
                        slider_3 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", user.slider_3);
                        FileInfo fi5 = new FileInfo(slider_3);
                        if (fi5 != null)
                        {
                            System.IO.File.Delete(slider_3);
                            fi5.Delete();
                        }
                        else { }
                    }

                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.EmailConfirmed = model.EmailConfirmed;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.MobileNumber = model.MobileNumber;
                    user.Dob = model.Dob;
                    user.IsApproved = model.IsApproved;

                    user.languageID = model.languageID;
                    user.currencyID = model.currencyID;
                    user.premium = model.premium;
                    user.seller_rank = model.seller_rank;
                    user.like = model.like;
                    user.dislike = model.dislike;
                    if (file1 != null)
                    {
                        if (Path.GetExtension(file1.FileName) == ".png" || Path.GetExtension(file1.FileName) == ".jpg" ||
                            Path.GetExtension(file1.FileName) == ".jpeg" || Path.GetExtension(file1.FileName) == ".gif" ||
                            Path.GetExtension(file1.FileName) == ".webp" || Path.GetExtension(file1.FileName) == ".svg")
                        {
                            if (file1.Length < 4500000)
                            {
                                var extention = Path.GetExtension(file1.FileName);
                                var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                user.profile_pic = randomName;
                                var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
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
                                var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                user.back_pic = randomName;
                                var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
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
                                var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                user.slider_1 = randomName;
                                var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
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
                    if (file4 != null)
                    {
                        if (Path.GetExtension(file4.FileName) == ".png" || Path.GetExtension(file4.FileName) == ".jpg" ||
                            Path.GetExtension(file4.FileName) == ".jpeg" || Path.GetExtension(file4.FileName) == ".gif" ||
                            Path.GetExtension(file4.FileName) == ".webp" || Path.GetExtension(file4.FileName) == ".svg")
                        {
                            if (file4.Length < 4500000)
                            {
                                var extention = Path.GetExtension(file4.FileName);
                                var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                user.slider_2 = randomName;
                                var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                                using (var stream = new FileStream(path, FileMode.Create))
                                {
                                    await file4.CopyToAsync(stream);
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
                            Path.GetExtension(file5.FileName) == ".webp" || Path.GetExtension(file5.FileName) == ".svg")
                        {
                            if (file5.Length < 4500000)
                            {
                                var extention = Path.GetExtension(file5.FileName);
                                var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                                user.slider_3 = randomName;
                                var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                                using (var stream = new FileStream(path, FileMode.Create))
                                {
                                    await file5.CopyToAsync(stream);
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
                        var userRoles = await _userManager.GetRolesAsync(user);
                        selectedRoles = selectedRoles ?? new string[] { };
                        await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                        await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());

                        return RedirectToAction("UserList");
                    }
                }
                return RedirectToAction("UserList");
            }
            return View(model);
        }

        public IActionResult CreateUser()
        {
            var roles = _roleManager.Roles.Select(i => i.Name);
            ViewBag.Roles = roles;
            var entity = _lanService.GetAll();
            var entity1 = _curService.GetAll();
            var model = new RegisterAdminModel()
            {
                Langs = entity.ToList(),
                Curs = entity1.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> CreateUser(RegisterAdminModel model, string[] selectedRoles, IFormFile file1, IFormFile file2, IFormFile file3, IFormFile file4, IFormFile file5)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                MobileNumber = model.MobileNumber,
                IsApproved = model.IsApproved = true,
                Email = model.Email,
                EmailConfirmed = model.EmailConfirmed = true,
                Dob = model.Dob,
                languageID = model.languageID,
                currencyID = model.currencyID,
                premium = model.premium,
                seller_rank = model.seller_rank,
                like = model.like,
                dislike = model.dislike
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
                        var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                        user.profile_pic = randomName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
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
                        var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                        user.back_pic = randomName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
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
                        var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                        user.slider_1 = randomName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
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
            if (file4 != null)
            {
                if (Path.GetExtension(file4.FileName) == ".png" || Path.GetExtension(file4.FileName) == ".jpg" ||
                    Path.GetExtension(file4.FileName) == ".jpeg" || Path.GetExtension(file4.FileName) == ".gif" ||
                    Path.GetExtension(file4.FileName) == ".webp" || Path.GetExtension(file4.FileName) == ".svg")
                {
                    if (file4.Length < 4500000)
                    {
                        var extention = Path.GetExtension(file4.FileName);
                        var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                        user.slider_2 = randomName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file4.CopyToAsync(stream);
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
                    Path.GetExtension(file5.FileName) == ".webp" || Path.GetExtension(file5.FileName) == ".svg")
                {
                    if (file5.Length < 4500000)
                    {
                        var extention = Path.GetExtension(file5.FileName);
                        var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                        user.slider_3 = randomName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\users", randomName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file5.CopyToAsync(stream);
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
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                selectedRoles = selectedRoles ?? new string[] { };
                await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());

                return RedirectToAction("UserList");
            }
            ModelState.AddModelError("", "Bilinməyən xəta baş verdi");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UnApprovedUser(string Id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(Id);
                if (user == null)
                {
                    return NotFound();
                }
                user.IsApproved = false;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserList");
                }

            }
            return RedirectToAction("UserList");
        }

        [HttpPost]
        public async Task<IActionResult> ApprovedUser(string Id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(Id);
                if (user == null)
                {
                    return NotFound();
                }
                user.IsApproved = true;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserDeletedList");
                }
            }
            return RedirectToAction("UserDeletedList");
        }


        ///////////////////////////////////////////////////// Role //
        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                return RedirectToAction("RoleList");
            }
            else
            {
                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }

                return RedirectToAction("RoleList");
            }
        }

        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                return View(new EditRoleModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                });
            }
            return RedirectToAction("RoleList");
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                if (role != null)
                {
                    role.Name = model.RoleName;

                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("RoleList");
                    }
                }
                return RedirectToAction("RoleList");
            }
            return View(model);
        }


        ///////////////////////////////////////////////////// List //
        public IActionResult ActivationCountryList()
        {
            return View(new ActivationCountryListViewModel()
            {
                ACs = _acService.GetAll()
            });
        }

        public IActionResult CameraPerspectiveList()
        {
            return View(new CameraPerspectiveListViewModel()
            {
                CPs = _cpService.GetAll()
            });
        }

        public IActionResult CurrencyList()
        {
            return View(new CurrencyListViewModel()
            {
                Curs = _curService.GetAll()
            });
        }

        public IActionResult DeveloperList()
        {
            return View(new DeveloperListViewModel()
            {
                Devs = _devService.GetAll()
            });
        }

        public IActionResult DiviceList()
        {
            return View(new DiviceListViewModel()
            {
                Divs = _divService.GetAll()
            });
        }

        public IActionResult GameCategoryList()
        {
            return View(new GameCategoryListViewModel()
            {
                GCs = _gcService.GetAll()
            });
        }

        public IActionResult GameNameList()
        {
            return View(new GameNameListViewModel()
            {
                GNs = _gnService.GetAll()
            });
        }

        public IActionResult JanraList()
        {
            return View(new JanraListViewModel()
            {
                Jans = _janService.GetAll()
            });
        }

        public IActionResult LanguageList()
        {
            return View(new LanguageListViewModel()
            {
                Langs = _lanService.GetAll()
            });
        }

        public IActionResult PlatformList()
        {
            return View(new PlatformListViewModel()
            {
                Plats = _platService.GetAll()
            });
        }

        public IActionResult ProductOfGamerList()
        {
            return View(new Product_of_GamerListViewModel()
            {
                Pogs = _pogService.GetAll().Where(i => i.IsApproved == true).ToList(),
                Gns = _gnService.GetAll(),
                Divs = _divService.GetAll(),
                Users = _userManager.Users.ToList()
            });
        }
        public IActionResult ProductOfGamerDeletedList()
        {
            return View(new Product_of_GamerListViewModel()
            {
                Pogs = _pogService.GetAll().Where(i => i.IsApproved == false).ToList(),
                Gns = _gnService.GetAll(),
                Divs = _divService.GetAll(),
                Users = _userManager.Users.ToList()
            });
        }

        public IActionResult ProductList()
        {
            return View(new ProductListViewModel()
            {
                Pros = _proService.GetAll().Where(i => i.IsApproved == true).ToList(),
                Plats = _platService.GetAll(),
                GCs = _gcService.GetAll(),
                Jans = _janService.GetAll(),
                CPs = _cpService.GetAll(),
                Pubs = _pubService.GetAll(),
                Devs = _devService.GetAll(),
                ACs = _acService.GetAll()
            });
        }
        public IActionResult ProductDeletedList()
        {
            return View(new ProductListViewModel()
            {
                Pros = _proService.GetAll().Where(i => i.IsApproved == false).ToList(),
                Plats = _platService.GetAll(),
                GCs = _gcService.GetAll(),
                Jans = _janService.GetAll(),
                CPs = _cpService.GetAll(),
                Pubs = _pubService.GetAll(),
                Devs = _devService.GetAll(),
                ACs = _acService.GetAll()
            });
        }

        public IActionResult PublisherList()
        {
            return View(new PublisherListViewModel()
            {
                Pubs = _pubService.GetAll()
            });
        }

        public IActionResult PurchasedPOGList()
        {
            return View(new PurchasedPOGListViewModel()
            {
                PPOGs = _ppogService.GetAll(),
                POGs = _pogService.GetAll(),
                Gns = _gnService.GetAll(),
                Users = _userManager.Users.ToList()
            });
        }

        public IActionResult PurchasedProductList()
        {
            return View(new PurchasedProductListViewModel()
            {
                PPs = _ppService.GetAll(),
                Pros = _proService.GetAll(),
                Users = _userManager.Users.ToList()
            });
        }

        public IActionResult GameItemList()
        {
            return View(new GameItemListViewModel()
            {
                GIs = _giService.GetAll()
            });
        }

        public IActionResult LanguageTextList()
        {
            return View(new LanguageTextListViewModel()
            {
                Pros = _proService.GetAll(),
                Lans = _lanService.GetAll(),
                LTs = _ltService.GetAll()
            });
        }

        public IActionResult InstructionPanelList()
        {
            return View(new InstructionPanelListViewModel()
            {
                IPs = _ipService.GetAll()
            });
        }

        public IActionResult IndexSliderList()
        {
            return View(new IndexSliderListViewModel()
            {
                ISs = _isService.GetAll()
            });
        }

        public IActionResult XboxdataList()
        {
            return View(new XboxdataListViewModel()
            {
                XDs = _xdService.GetAll(),
                XGs = _xgService.GetAll()
            });
        }

        public IActionResult XboxgameList()
        {
            return View(new XboxgameListViewModel()
            {
                XGs = _xgService.GetAll()
            });
        }

        public IActionResult PaymentPHistoryList()
        {
            return View(new PaymentPHistoryListViewModel()
            {
                PPHs = _pphService.GetAll(),
                Pros = _proService.GetAll(),
                Users = _userManager.Users.ToList()
            });
        }

        public IActionResult PaymentPOGHistoryList()
        {
            return View(new PaymentPOGHistoryListViewModel()
            {
                PPOGHs = _ppoghService.GetAll(),
                POGs = _pogService.GetAll(),
                GNs = _gnService.GetAll(),
                Users = _userManager.Users.ToList()
            });
        }

        ///////////////////////////////////////////////////// Create //
        public IActionResult ActivationCountryCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ActivationCountryCreate(ActivationCountryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new ActivationCountry()
                {
                    ActivationCountryName = model.ActivationCountryName,
                    IsApproved = model.IsApproved
                };
                _acService.Create(entity);
                return RedirectToAction("ActivationCountryList");
            }
            return View(model);
        }

        public IActionResult CameraPerspectiveCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CameraPerspectiveCreate(CameraPerspectiveModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new CameraPerspective()
                {
                    CameraPerspevtiveName = model.CameraPerspevtiveName,
                    IsApproved = model.IsApproved
                };
                _cpService.Create(entity);
                return RedirectToAction("CameraPerspectiveList");
            }
            return View(model);
        }

        public IActionResult CurrencyCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CurrencyCreate(CurrencyModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Currency()
                {
                    CurrencyName = model.CurrencyName,
                    LanguageTag = model.LanguageTag,
                    CurrencyConst = model.CurrencyConst,
                    CurrencyStringConst = model.CurrencyStringConst,
                    CurrencyIcon = model.CurrencyIcon,
                    IsApproved = model.IsApproved
                };
                _curService.Create(entity);
                return RedirectToAction("CurrencyList");
            }
            return View(model);
        }

        public IActionResult DeveloperCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeveloperCreate(DeveloperModel model, IFormFile file1, IFormFile file2)
        {
            if (ModelState.IsValid)
            {
                var entity = new Developer()
                {
                    DeveloperName = model.DeveloperName,
                    IsApproved = model.IsApproved
                };
                if (file1 != null)
                {
                    if (Path.GetExtension(file1.FileName) == ".png" || Path.GetExtension(file1.FileName) == ".jpg" ||
                        Path.GetExtension(file1.FileName) == ".jpeg" || Path.GetExtension(file1.FileName) == ".gif" ||
                        Path.GetExtension(file1.FileName) == ".webp" || Path.GetExtension(file1.FileName) == ".svg")
                    {
                        if (file1.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file1.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\developer", randomName);
                            using (var image = Image.Load(file1.OpenReadStream()))
                            {
                                string newSize = ResizeImage(image, 320, 200);
                                string[] aSize = newSize.Split(',');
                                image.Mutate(q => q.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                                image.Save(path);
                            }
                            //using (var stream = new FileStream(path, FileMode.Create))
                            //{
                            //    await file1.CopyToAsync(stream);
                            //}
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
                            //var extention = Path.GetExtension(file2.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Back_img = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\developer", randomName);
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

                _devService.Create(entity);
                return RedirectToAction("DeveloperList");
            }
            return View(model);
        }

        public IActionResult DiviceCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DiviceCreate(DiviceModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Divice()
                {
                    DiviceName = model.DiviceName,
                    IsApproved = model.IsApproved
                };
                _divService.Create(entity);
                return RedirectToAction("DiviceList");
            }
            return View(model);
        }

        public IActionResult GameCategoryCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GameCategoryCreate(GameCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new GameCategory()
                {
                    GameCategoryName = model.GameCategoryName,
                    IsApproved = model.IsApproved
                };
                _gcService.Create(entity);
                return RedirectToAction("GameCategoryList");
            }
            return View(model);
        }

        public IActionResult GameNameCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GameNameCreate(GameNameModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new GameName()
                {
                    GameOfName = model.GameOfName,
                    IsApproved = model.IsApproved
                };
                _gnService.Create(entity);
                return RedirectToAction("GameNameList");
            }
            return View(model);
        }

        public IActionResult JanraCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult JanraCreate(JanraModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Janra()
                {
                    JanraName = model.JanraName,
                    IsApproved = model.IsApproved
                };
                _janService.Create(entity);
                return RedirectToAction("JanraList");
            }
            return View(model);
        }

        public IActionResult LanguageCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LanguageCreate(LanguageModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Language()
                {
                    LanguageName = model.LanguageName,
                    LanguageTag = model.LanguageTag,
                    LanguageIcon = model.LanguageIcon,
                    IsApproved = model.IsApproved
                };
                _lanService.Create(entity);
                return RedirectToAction("LanguageList");
            }
            return View(model);
        }

        public IActionResult PlatformCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PlatformCreate(PlatformModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Platform()
                {
                    PlatformName = model.PlatformName,
                    Link = model.Link,
                    IsApproved = model.IsApproved
                };
                _platService.Create(entity);
                return RedirectToAction("PlatformList");
            }
            return View(model);
        }

        public IActionResult ProductOfGamerCreate()
        {
            var entity0 = _gnService.GetAll();
            var entity1 = _divService.GetAll();
            var entity = _userManager.Users;
            var model = new Product_of_GamerModel()
            {
                Gns = entity0.ToList(),
                Divs = entity1.ToList(),
                Users = entity.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ProductOfGamerCreate(Product_of_GamerModel model, IFormFile file1, IFormFile file2, IFormFile file3)
        {
            if (ModelState.IsValid)
            {
                var entity = new Product_of_Gamer()
                {
                    UserId = model.UserId,
                    GameNameID = model.GameNameID,
                    DiviceID = model.DiviceID,
                    Login = model.Login,
                    Password = model.Password,
                    Price = model.Price,
                    Slider_videolink = model.Slider_videolink,
                    Text = model.Text,
                    IsApproved = model.IsApproved
                };
                if (file1 != null)
                {
                    if (Path.GetExtension(file1.FileName) == ".png" || Path.GetExtension(file1.FileName) == ".jpg" ||
                        Path.GetExtension(file1.FileName) == ".jpeg" || Path.GetExtension(file1.FileName) == ".gif" ||
                        Path.GetExtension(file1.FileName) == ".webp" || Path.GetExtension(file1.FileName) == ".svg")
                    {
                        if (file1.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file1.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"{model.GameNameID}_img_{Guid.NewGuid()}{extention}");
                            entity.Slider_img1 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\pog", randomName);
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
                            //var extention = Path.GetExtension(file2.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"{model.GameNameID}_img_{Guid.NewGuid()}{extention}");
                            entity.Slider_img2 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\pog", randomName);
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
                            //var extention = Path.GetExtension(file3.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"{model.GameNameID}_img_{Guid.NewGuid()}{extention}");
                            entity.Slider_img3 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\pog", randomName);
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
                return RedirectToAction("ProductOfGamerList");
            }
            return View(model);
        }

        public IActionResult ProductCreate()
        {
            var entity0 = _platService.GetAll();
            var entity1 = _gcService.GetAll();
            var entity2 = _janService.GetAll();
            var entity3 = _cpService.GetAll();
            var entity4 = _pubService.GetAll();
            var entity5 = _devService.GetAll();
            var entity6 = _acService.GetAll();
            var model = new ProductModel()
            {
                Plats = entity0.ToList(),
                GCs = entity1.ToList(),
                Jans = entity2.ToList(),
                CPs = entity3.ToList(),
                Pubs = entity4.ToList(),
                Devs = entity5.ToList(),
                ACs = entity6.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel model, IFormFile file1, IFormFile file2, IFormFile file3, IFormFile file4)
        {
            if (ModelState.IsValid)
            {
                var entity = new Product()
                {
                    Name = model.Name,
                    Login = model.Login,
                    Password = model.Password,
                    Key = model.Key,
                    Contenttype = model.Contenttype,
                    Price = model.Price,
                    Company_name = model.Company_name,
                    Activation_zone = model.Activation_zone,
                    Onlineornot = model.Onlineornot,
                    Signleplayer = model.Signleplayer,
                    Multiplayer = model.Multiplayer,
                    Co_op = model.Co_op,
                    Type_active = model.Type_active,
                    twoD = model.twoD,
                    threeD = model.threeD,
                    VR = model.VR,
                    IndexSlider = model.IndexSlider,
                    Slider_videolink = model.Slider_videolink,
                    Text = model.Text,
                    Discount_percent = model.Discount_percent,
                    Instock = model.Instock,
                    Stocksize = model.Stocksize,
                    Number_of_sale = model.Number_of_sale,
                    Url = model.Url,
                    IsApproved = model.IsApproved,
                    UpComing = model.UpComing,
                    ReleaseDate = model.ReleaseDate,
                    PlatformID = model.PlatformID,
                    CategoryID = model.CategoryID,
                    JanraID = model.JanraID,
                    CameraperspectiveID = model.CameraperspectiveID,
                    PublisherID = model.PublisherID,
                    DeveloperID = model.DeveloperID,
                    Activation_countryID = model.Activation_countryID
                };
                if (file1 != null)
                {
                    if (Path.GetExtension(file1.FileName) == ".png" || Path.GetExtension(file1.FileName) == ".jpg" ||
                        Path.GetExtension(file1.FileName) == ".jpeg" || Path.GetExtension(file1.FileName) == ".gif" ||
                        Path.GetExtension(file1.FileName) == ".webp" || Path.GetExtension(file1.FileName) == ".svg")
                    {
                        if (file1.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file1.FileName);
                            var extention = ".webp";
                            var rndm = Guid.NewGuid();
                            var randomName1 = string.Format($"bigimg_{rndm}{extention}");
                            var path1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\product", randomName1);
                            using (var stream = new FileStream(path1, FileMode.Create))
                            {
                                await file1.CopyToAsync(stream);
                            }
                            var randomName = string.Format($"img_{rndm}{extention}");
                            entity.Main_img = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\product", randomName);
                            using (var image = Image.Load(file1.OpenReadStream()))
                            {
                                string newSize = ResizeImage(image, 200, 320);
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
                        Path.GetExtension(file2.FileName) == ".webp" || Path.GetExtension(file2.FileName) == ".svg")
                    {
                        if (file2.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file2.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Slider_img1 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\product", randomName);
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
                            //var extention = Path.GetExtension(file3.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Slider_img2 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\product", randomName);
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
                if (file4 != null)
                {
                    if (Path.GetExtension(file4.FileName) == ".png" || Path.GetExtension(file4.FileName) == ".jpg" ||
                        Path.GetExtension(file4.FileName) == ".jpeg" || Path.GetExtension(file4.FileName) == ".gif" ||
                        Path.GetExtension(file4.FileName) == ".webp" || Path.GetExtension(file4.FileName) == ".svg")
                    {
                        if (file4.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file4.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Slider_img3 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\product", randomName);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file4.CopyToAsync(stream);
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

                _proService.Create(entity);
                return RedirectToAction("ProductList");
            }
            return View(model);
        }

        public IActionResult PublisherCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PublisherCreate(PublisherModel model, IFormFile file1, IFormFile file2)
        {
            if (ModelState.IsValid)
            {
                var entity = new Publisher()
                {
                    PublisherName = model.PublisherName,
                    IsApproved = model.IsApproved
                };
                if (file1 != null)
                {
                    if (Path.GetExtension(file1.FileName) == ".png" || Path.GetExtension(file1.FileName) == ".jpg" ||
                        Path.GetExtension(file1.FileName) == ".jpeg" || Path.GetExtension(file1.FileName) == ".gif" ||
                        Path.GetExtension(file1.FileName) == ".webp" || Path.GetExtension(file1.FileName) == ".svg")
                    {
                        if (file1.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file1.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\publisher", randomName);
                            using (var image = Image.Load(file1.OpenReadStream()))
                            {
                                string newSize = ResizeImage(image, 320, 200);
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
                        Path.GetExtension(file2.FileName) == ".webp" || Path.GetExtension(file2.FileName) == ".svg")
                    {
                        if (file2.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file2.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Back_img = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\publisher", randomName);
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

                _pubService.Create(entity);
                return RedirectToAction("PublisherList");
            }
            return View(model);
        }

        public IActionResult PurchasedPOGCreate()
        {
            var entity = _pogService.GetAll();
            var entity2 = _gnService.GetAll();
            var entity1 = _userManager.Users;
            var model = new PurchasedPOGModel()
            {
                POGs = entity.ToList(),
                Users = entity1.ToList(),
                GNs = entity2.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult PurchasedPOGCreate(PurchasedPOGModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new PurchasedPOG()
                {
                    UserId = model.UserId,
                    POGId = model.POGId,
                    IsApproved = model.IsApproved,
                    DateTime = model.DateTime
                };
                _ppogService.Create(entity);
                return RedirectToAction("PurchasedPOGList");
            }
            return View(model);
        }

        public IActionResult PurchasedProductCreate()
        {
            var entity = _proService.GetAll();
            var entity1 = _userManager.Users;
            var model = new PurchasedProductModel()
            {
                Pros = entity.ToList(),
                Users = entity1.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult PurchasedProductCreate(PurchasedProductModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new PurchasedProduct()
                {
                    UserId = model.UserId,
                    ProductId = model.ProductId,
                    IsApproved = model.IsApproved,
                    IsXbox = model.IsXbox,
                    DateTime = model.DateTime
                };
                _ppService.Create(entity);
                return RedirectToAction("PurchasedProductList");
            }
            return View(model);
        }

        public IActionResult GameItemCreate()
        {
            var model = new GameItemModel()
            {
                GNs = _gnService.GetAll()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult GameItemCreate(GameItemModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new GameItem()
                {
                    GameItemName = model.GameItemName,
                    GNId = model.GNId,
                    IsApproved = model.IsApproved
                };
                _giService.Create(entity);
                return RedirectToAction("GameItemList");
            }
            return View(model);
        }

        public IActionResult LanguageTextCreate()
        {
            var model = new LanguageTextModel()
            {
                Pros = _proService.GetAll().ToList(),
                Lans = _lanService.GetAll().ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult LanguageTextCreate(LanguageTextModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new LanguageText()
                {
                    ProductId = model.ProductId,
                    LaguageId = model.LaguageId,
                    Text = model.Text,
                    IsApproved = model.IsApproved
                };
                _ltService.Create(entity);
                return RedirectToAction("LanguageTextList");
            }
            return View(model);
        }

        public IActionResult InstructionPanelCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InstructionPanelCreate(InstructionPanelModel model, IFormFile file, IFormFile file1)
        {
            if (ModelState.IsValid)
            {
                var entity = new InstructionPanel()
                {
                    Text = model.Text,
                    Product = model.Product,
                    POG = model.POG,
                    IsApproved = model.IsApproved
                };
                if (file != null)
                {
                    if (Path.GetExtension(file.FileName) == ".png" || Path.GetExtension(file.FileName) == ".jpg" ||
                        Path.GetExtension(file.FileName) == ".jpeg" || Path.GetExtension(file.FileName) == ".gif" ||
                        Path.GetExtension(file.FileName) == ".webp" || Path.GetExtension(file.FileName) == ".svg")
                    {
                        if (file.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\instructionpanel", randomName);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
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
                if (file1 != null)
                {
                    if (Path.GetExtension(file1.FileName) == ".png" || Path.GetExtension(file1.FileName) == ".jpg" ||
                        Path.GetExtension(file1.FileName) == ".jpeg" || Path.GetExtension(file1.FileName) == ".gif" ||
                        Path.GetExtension(file1.FileName) == ".webp" || Path.GetExtension(file1.FileName) == ".svg")
                    {
                        if (file1.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file1.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_Title_{Guid.NewGuid()}{extention}");
                            entity.ImgTitle = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\instructionpanel", randomName);
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


                _ipService.Create(entity);
                return RedirectToAction("InstructionPanelList");
            }
            return View(model);
        }

        public IActionResult IndexSliderCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexSliderCreate(IndexSliderModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var entity = new IndexSlider()
                {
                    IndexSliderName = model.IndexSliderName,
                    Text = model.Text,
                    UrlTitle = model.UrlTitle,
                    Url = model.Url,
                    IsApproved = model.IsApproved
                };
                if (file != null)
                {
                    if (Path.GetExtension(file.FileName) == ".png" || Path.GetExtension(file.FileName) == ".jpg" ||
                        Path.GetExtension(file.FileName) == ".jpeg" || Path.GetExtension(file.FileName) == ".gif" ||
                        Path.GetExtension(file.FileName) == ".webp" || Path.GetExtension(file.FileName) == ".svg")
                    {
                        if (file.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\indexslider", randomName);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                        }
                    }
                }

                _isService.Create(entity);
                return RedirectToAction("IndexSliderList");
            }
            return View(model);
        }

        public IActionResult XboxdataCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> XboxdataCreate(XboxdataModel model, IFormFile file1, IFormFile file2, IFormFile file3, IFormFile file4, IFormFile file5)
        {
            if (ModelState.IsValid)
            {
                var entity = new Xboxdata()
                {
                    Title = model.Title,
                    Login = model.Login,
                    Password = model.Password,
                    Price = model.Price,
                    SPrice = model.SPrice
                };
                if (file1 != null)
                {
                    if (Path.GetExtension(file1.FileName) == ".png" || Path.GetExtension(file1.FileName) == ".jpg" ||
                        Path.GetExtension(file1.FileName) == ".jpeg" || Path.GetExtension(file1.FileName) == ".gif" ||
                        Path.GetExtension(file1.FileName) == ".webp" || Path.GetExtension(file1.FileName) == ".svg")
                    {
                        if (file1.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file1.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img1 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\xboxdata", randomName);
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
                            //var extention = Path.GetExtension(file2.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img2 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\xboxdata", randomName);
                            using (var image = Image.Load(file2.OpenReadStream()))
                            {
                                string newSize = ResizeImage(image, 600, 350);
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
                        Path.GetExtension(file3.FileName) == ".webp" || Path.GetExtension(file3.FileName) == ".svg")
                    {
                        if (file3.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file3.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img3 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\xboxdata", randomName);
                            using (var image = Image.Load(file3.OpenReadStream()))
                            {
                                string newSize = ResizeImage(image, 600, 350);
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
                        Path.GetExtension(file4.FileName) == ".webp" || Path.GetExtension(file4.FileName) == ".svg")
                    {
                        if (file4.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file4.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img4 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\xboxdata", randomName);
                            using (var image = Image.Load(file4.OpenReadStream()))
                            {
                                string newSize = ResizeImage(image, 600, 350);
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
                        Path.GetExtension(file5.FileName) == ".webp" || Path.GetExtension(file5.FileName) == ".svg")
                    {
                        if (file5.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file5.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img5 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\xboxdata", randomName);
                            using (var image = Image.Load(file5.OpenReadStream()))
                            {
                                string newSize = ResizeImage(image, 600, 350);
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

                _xdService.Create(entity);
                return RedirectToAction("XboxdataList");
            }
            return View(model);
        }

        public IActionResult XboxgameCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> XboxgameCreate(XboxgameModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var entity = new Xboxgame()
                {
                    GameName = model.GameName,
                    Priority = model.Priority
                };
                if (file != null)
                {
                    if (Path.GetExtension(file.FileName) == ".png" || Path.GetExtension(file.FileName) == ".jpg" ||
                        Path.GetExtension(file.FileName) == ".jpeg" || Path.GetExtension(file.FileName) == ".gif" ||
                        Path.GetExtension(file.FileName) == ".webp" || Path.GetExtension(file.FileName) == ".svg")
                    {
                        if (file.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\xboxgame", randomName);
                            using (var image = Image.Load(file.OpenReadStream()))
                            {
                                string newSize = ResizeImage(image, 200, 320);
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

                _xgService.Create(entity);
                return RedirectToAction("XboxgameList");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateCartForAdmin()
        {
            var carts = _cartService.GetAll();
            var user = _userManager.Users.Where(i => i.IsApproved).ToList();

            foreach (var e in user)
            {
                foreach (var w in carts)
                {
                    if (w.UserId == e.Id)
                    {

                    }
                    else
                    {
                        _cartService.InitializeCart(e.Id);
                    }
                }
            }
            return Redirect("/adminpanel/home");
        }

        ///////////////////////////////////////////////////// Edit //
        public IActionResult ActivationCountryEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _acService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new ActivationCountryModel()
            {
                Id = entity.Id,
                ActivationCountryName = entity.ActivationCountryName,
                IsApproved = entity.IsApproved
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult ActivationCountryEdit(ActivationCountryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _acService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.ActivationCountryName = model.ActivationCountryName;
                entity.IsApproved = model.IsApproved;

                _acService.Update(entity);
                return RedirectToAction("ActivationCountryList");
            }
            return View(model);
        }

        public IActionResult CameraPerspectiveEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _cpService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new CameraPerspectiveModel()
            {
                Id = entity.Id,
                CameraPerspevtiveName = entity.CameraPerspevtiveName,
                IsApproved = entity.IsApproved
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult CameraPerspectiveEdit(CameraPerspectiveModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _cpService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.CameraPerspevtiveName = model.CameraPerspevtiveName;
                entity.IsApproved = model.IsApproved;

                _cpService.Update(entity);
                return RedirectToAction("CameraPerspectiveList");
            }
            return View(model);
        }

        public IActionResult CurrencyEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _curService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new CurrencyModel()
            {
                Id = entity.Id,
                CurrencyName = entity.CurrencyName,
                LanguageTag = entity.LanguageTag,
                CurrencyConst = entity.CurrencyConst,
                CurrencyStringConst = entity.CurrencyStringConst,
                CurrencyIcon = entity.CurrencyIcon,
                IsApproved = entity.IsApproved
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult CurrencyEdit(CurrencyModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _curService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.CurrencyName = model.CurrencyName;
                entity.LanguageTag = model.LanguageTag;
                entity.CurrencyConst = model.CurrencyConst;
                entity.CurrencyStringConst = model.CurrencyStringConst;
                entity.CurrencyIcon = model.CurrencyIcon;
                entity.IsApproved = model.IsApproved;

                _curService.Update(entity);
                return RedirectToAction("CurrencyList");
            }
            return View(model);
        }

        public IActionResult DeveloperEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _devService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new DeveloperModel()
            {
                Id = entity.Id,
                DeveloperName = entity.DeveloperName,
                Img = entity.Img,
                Back_img = entity.Back_img,
                IsApproved = entity.IsApproved
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeveloperEdit(DeveloperModel model, IFormFile file1, IFormFile file2)
        {
            if (ModelState.IsValid)
            {
                var entity = _devService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                string Img;
                string Back_img;
                if (file1 != null)
                {
                    if (entity.Img != null)
                    {
                        Img = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\developer", entity.Img);
                        FileInfo fi1 = new FileInfo(Img);
                        if (fi1 != null)
                        {
                            System.IO.File.Delete(Img);
                            fi1.Delete();
                        }
                    }
                }
                if (file2 != null)
                {
                    if (entity.Back_img != null)
                    {
                        Back_img = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\developer", entity.Back_img);
                        FileInfo fi2 = new FileInfo(Back_img);
                        if (fi2 != null)
                        {
                            System.IO.File.Delete(Back_img);
                            fi2.Delete();
                        }
                    }
                }

                entity.DeveloperName = model.DeveloperName;
                entity.IsApproved = model.IsApproved;
                if (file1 != null)
                {
                    if (Path.GetExtension(file1.FileName) == ".png" || Path.GetExtension(file1.FileName) == ".jpg" ||
                        Path.GetExtension(file1.FileName) == ".jpeg" || Path.GetExtension(file1.FileName) == ".gif" ||
                        Path.GetExtension(file1.FileName) == ".webp" || Path.GetExtension(file1.FileName) == ".svg")
                    {
                        if (file1.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file1.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\developer", randomName);
                            using (var image = Image.Load(file1.OpenReadStream()))
                            {
                                string newSize = ResizeImage(image, 320, 200);
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
                        Path.GetExtension(file2.FileName) == ".webp" || Path.GetExtension(file2.FileName) == ".svg")
                    {
                        if (file2.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file2.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Back_img = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\developer", randomName);
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

                _devService.Update(entity);
                return RedirectToAction("DeveloperList");
            }
            return View(model);
        }

        public IActionResult DiviceEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _divService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new DiviceModel()
            {
                Id = entity.Id,
                DiviceName = entity.DiviceName,
                IsApproved = entity.IsApproved
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult DiviceEdit(DiviceModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _divService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.DiviceName = model.DiviceName;
                entity.IsApproved = model.IsApproved;

                _divService.Update(entity);
                return RedirectToAction("DiviceList");
            }
            return View(model);
        }

        public IActionResult GameCategoryEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _gcService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new GameCategoryModel()
            {
                Id = entity.Id,
                GameCategoryName = entity.GameCategoryName,
                IsApproved = entity.IsApproved
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult GameCategoryEdit(GameCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _gcService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.GameCategoryName = model.GameCategoryName;
                entity.IsApproved = model.IsApproved;

                _gcService.Update(entity);
                return RedirectToAction("GameCategoryList");
            }
            return View(model);
        }

        public IActionResult GameNameEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _gnService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new GameNameModel()
            {
                Id = entity.Id,
                GameOfName = entity.GameOfName,
                IsApproved = entity.IsApproved
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult GameNameEdit(GameNameModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _gnService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.GameOfName = model.GameOfName;
                entity.IsApproved = model.IsApproved;

                _gnService.Update(entity);
                return RedirectToAction("GameNameList");
            }
            return View(model);
        }

        public IActionResult JanraEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _janService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new JanraModel()
            {
                Id = entity.Id,
                JanraName = entity.JanraName,
                IsApproved = entity.IsApproved
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult JanraEdit(JanraModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _janService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.JanraName = model.JanraName;
                entity.IsApproved = model.IsApproved;

                _janService.Update(entity);
                return RedirectToAction("JanraList");
            }
            return View(model);
        }

        public IActionResult LanguageEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _lanService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new LanguageModel()
            {
                Id = entity.Id,
                LanguageName = entity.LanguageName,
                LanguageTag = entity.LanguageTag,
                LanguageIcon = entity.LanguageIcon,
                IsApproved = entity.IsApproved
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult LanguageEdit(LanguageModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _lanService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.LanguageName = model.LanguageName;
                entity.LanguageTag = model.LanguageTag;
                entity.LanguageIcon = model.LanguageIcon;
                entity.IsApproved = model.IsApproved;

                _lanService.Update(entity);
                return RedirectToAction("LanguageList");
            }
            return View(model);
        }

        public IActionResult PlatformEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _platService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new PlatformModel()
            {
                Id = entity.Id,
                PlatformName = entity.PlatformName,
                Link = entity.Link,
                IsApproved = entity.IsApproved
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult PlatformEdit(PlatformModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _platService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.PlatformName = model.PlatformName;
                entity.Link = model.Link;
                entity.IsApproved = model.IsApproved;

                _platService.Update(entity);
                return RedirectToAction("PlatformList");
            }
            return View(model);
        }

        public IActionResult ProductOfGamerEdit(int? id)
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

            var entity0 = _userManager.Users.ToList();
            var entity1 = _gnService.GetAll();
            var entity2 = _divService.GetAll();

            var model = new Product_of_GamerModel()
            {
                Id = entity.Id,
                UserId = entity.UserId,
                GameNameID = entity.GameNameID,
                DiviceID = entity.DiviceID,
                Login = entity.Login,
                Password = entity.Password,
                Price = entity.Price,
                Slider_videolink = entity.Slider_videolink,
                Slider_img1 = entity.Slider_img1,
                Slider_img2 = entity.Slider_img2,
                Slider_img3 = entity.Slider_img3,
                Text = entity.Text,
                IsApproved = entity.IsApproved,

                Users = entity0.ToList(),
                Gns = entity1.ToList(),
                Divs = entity2.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ProductOfGamerEdit(Product_of_GamerModel model, IFormFile file1, IFormFile file2, IFormFile file3)
        {
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

                entity.UserId = model.UserId;
                entity.GameNameID = model.GameNameID;
                entity.DiviceID = model.DiviceID;
                entity.Login = model.Login;
                entity.Password = model.Password;
                entity.Price = model.Price;
                entity.Slider_videolink = model.Slider_videolink;
                entity.Text = model.Text;
                entity.IsApproved = model.IsApproved;
                if (file1 != null)
                {
                    if (Path.GetExtension(file1.FileName) == ".png" || Path.GetExtension(file1.FileName) == ".jpg" ||
                        Path.GetExtension(file1.FileName) == ".jpeg" || Path.GetExtension(file1.FileName) == ".gif" ||
                        Path.GetExtension(file1.FileName) == ".webp" || Path.GetExtension(file1.FileName) == ".svg")
                    {
                        if (file1.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file1.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Slider_img1 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\pog", randomName);
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
                            //var extention = Path.GetExtension(file2.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Slider_img2 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\pog", randomName);
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
                            //var extention = Path.GetExtension(file3.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Slider_img3 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\pog", randomName);
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
                return RedirectToAction("ProductOfGamerList");
            }
            return View(model);
        }

        public IActionResult ProductEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _proService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var entity0 = _platService.GetAll();
            var entity1 = _gcService.GetAll();
            var entity2 = _janService.GetAll();
            var entity3 = _cpService.GetAll();
            var entity4 = _pubService.GetAll();
            var entity5 = _devService.GetAll();
            var entity6 = _acService.GetAll();

            var model = new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Login = entity.Login,
                Password = entity.Password,
                Key = entity.Key,
                Contenttype = entity.Contenttype,
                Price = entity.Price,
                Company_name = entity.Company_name,
                Activation_zone = entity.Activation_zone,
                Onlineornot = entity.Onlineornot,
                Signleplayer = entity.Signleplayer,
                Multiplayer = entity.Multiplayer,
                Co_op = entity.Co_op,
                Type_active = entity.Type_active,
                twoD = entity.twoD,
                threeD = entity.threeD,
                VR = entity.VR,
                IndexSlider = entity.IndexSlider,
                Main_img = entity.Main_img,
                Slider_videolink = entity.Slider_videolink,
                Slider_img1 = entity.Slider_img1,
                Slider_img2 = entity.Slider_img2,
                Slider_img3 = entity.Slider_img3,
                Text = entity.Text,
                Discount_percent = entity.Discount_percent,
                Instock = entity.Instock,
                Stocksize = entity.Stocksize,
                Number_of_sale = entity.Number_of_sale,
                Url = entity.Url,
                IsApproved = entity.IsApproved,
                UpComing = entity.UpComing,
                ReleaseDate = entity.ReleaseDate,
                PlatformID = entity.PlatformID,
                CategoryID = entity.CategoryID,
                JanraID = entity.JanraID,
                CameraperspectiveID = entity.CameraperspectiveID,
                PublisherID = entity.PublisherID,
                DeveloperID = entity.DeveloperID,
                Activation_countryID = entity.Activation_countryID,


                Plats = entity0.ToList(),
                GCs = entity1.ToList(),
                Jans = entity2.ToList(),
                CPs = entity3.ToList(),
                Pubs = entity4.ToList(),
                Devs = entity5.ToList(),
                ACs = entity6.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductModel model, IFormFile file1, IFormFile file2, IFormFile file3, IFormFile file4)
        {
            if (ModelState.IsValid)
            {
                var entity = _proService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                string Main_img;
                string Slider_img1;
                string Slider_img2;
                string Slider_img3;
                if (file1 != null)
                {
                    if (entity.Main_img != null)
                    {
                        Main_img = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\product", entity.Main_img);
                        FileInfo fi = new FileInfo(Main_img);
                        if (fi != null)
                        {
                            System.IO.File.Delete(Main_img);
                            fi.Delete();
                        }
                        Main_img = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\product", $"big{entity.Main_img}");
                        FileInfo fi2 = new FileInfo(Main_img);
                        if (fi2 != null)
                        {
                            System.IO.File.Delete(Main_img);
                            fi2.Delete();
                        }
                    }
                }
                if (file2 != null)
                {
                    if (entity.Slider_img1 != null)
                    {
                        Slider_img1 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\product", entity.Slider_img1);
                        FileInfo fi1 = new FileInfo(Slider_img1);
                        if (fi1 != null)
                        {
                            System.IO.File.Delete(Slider_img1);
                            fi1.Delete();
                        }
                    }
                }
                if (file3 != null)
                {
                    if (entity.Slider_img2 != null)
                    {
                        Slider_img2 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\product", entity.Slider_img2);
                        FileInfo fi2 = new FileInfo(Slider_img2);
                        if (fi2 != null)
                        {
                            System.IO.File.Delete(Slider_img2);
                            fi2.Delete();
                        }
                    }
                }
                if (file4 != null)
                {
                    if (entity.Slider_img3 != null)
                    {
                        Slider_img3 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\product", entity.Slider_img3);
                        FileInfo fi3 = new FileInfo(Slider_img3);
                        if (fi3 != null)
                        {
                            System.IO.File.Delete(Slider_img3);
                            fi3.Delete();
                        }
                    }
                }

                entity.Name = model.Name;
                entity.Login = model.Login;
                entity.Password = model.Password;
                entity.Key = model.Key;
                entity.Contenttype = model.Contenttype;
                entity.Price = model.Price;
                entity.Company_name = model.Company_name;
                entity.Activation_zone = model.Activation_zone;
                entity.Onlineornot = model.Onlineornot;
                entity.Signleplayer = model.Signleplayer;
                entity.Multiplayer = model.Multiplayer;
                entity.Co_op = model.Co_op;
                entity.Type_active = model.Type_active;
                entity.twoD = model.twoD;
                entity.threeD = model.threeD;
                entity.VR = model.VR;
                entity.IndexSlider = model.IndexSlider;
                entity.Slider_videolink = model.Slider_videolink;
                entity.Text = model.Text;
                entity.Discount_percent = model.Discount_percent;
                entity.Instock = model.Instock;
                entity.Stocksize = model.Stocksize;
                entity.Number_of_sale = model.Number_of_sale;
                entity.Url = model.Url;
                entity.IsApproved = model.IsApproved;
                entity.UpComing = model.UpComing;
                entity.ReleaseDate = model.ReleaseDate;
                entity.PlatformID = model.PlatformID;
                entity.CategoryID = model.CategoryID;
                entity.JanraID = model.JanraID;
                entity.CameraperspectiveID = model.CameraperspectiveID;
                entity.PublisherID = model.PublisherID;
                entity.DeveloperID = model.DeveloperID;
                entity.Activation_countryID = model.Activation_countryID;
                if (file1 != null)
                {
                    if (Path.GetExtension(file1.FileName) == ".png" || Path.GetExtension(file1.FileName) == ".jpg" ||
                        Path.GetExtension(file1.FileName) == ".jpeg" || Path.GetExtension(file1.FileName) == ".gif" ||
                        Path.GetExtension(file1.FileName) == ".webp" || Path.GetExtension(file1.FileName) == ".svg")
                    {
                        if (file1.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file1.FileName);
                            var extention = ".webp";
                            var rndm = Guid.NewGuid();
                            var randomName1 = string.Format($"bigimg_{rndm}{extention}");
                            var path1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\product", randomName1);
                            using (var stream = new FileStream(path1, FileMode.Create))
                            {
                                await file1.CopyToAsync(stream);
                            }
                            var randomName = string.Format($"img_{rndm}{extention}");
                            entity.Main_img = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\product", randomName);
                            using (var image = Image.Load(file1.OpenReadStream()))
                            {
                                string newSize = ResizeImage(image, 200, 320);
                                string[] aSize = newSize.Split(',');
                                image.Mutate(q => q.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                                image.Save(path);
                            }
                        }
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
                            //var extention = Path.GetExtension(file2.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Slider_img1 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\product", randomName);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file2.CopyToAsync(stream);
                            }
                        }
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
                            //var extention = Path.GetExtension(file3.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Slider_img2 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\product", randomName);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file3.CopyToAsync(stream);
                            }
                        }

                    }
                }
                if (file4 != null)
                {
                    if (Path.GetExtension(file4.FileName) == ".png" || Path.GetExtension(file4.FileName) == ".jpg" ||
                        Path.GetExtension(file4.FileName) == ".jpeg" || Path.GetExtension(file4.FileName) == ".gif" ||
                        Path.GetExtension(file4.FileName) == ".webp" || Path.GetExtension(file4.FileName) == ".svg")
                    {
                        if (file4.Length < 4500000)
                        {

                            //var extention = Path.GetExtension(file4.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Slider_img3 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\product", randomName);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file4.CopyToAsync(stream);
                            }

                        }
                    }
                }

                _proService.Update(entity);
                return RedirectToAction("ProductList");
            }
            return View(model);
        }

        public IActionResult PublisherEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _pubService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new PublisherModel()
            {
                Id = entity.Id,
                PublisherName = entity.PublisherName,
                Img = entity.Img,
                Back_img = entity.Back_img,
                IsApproved = entity.IsApproved
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> PublisherEdit(PublisherModel model, IFormFile file1, IFormFile file2)
        {
            if (ModelState.IsValid)
            {
                var entity = _pubService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                string Img;
                string Back_img;
                if (file1 != null)
                {
                    if (entity.Img != null)
                    {
                        Img = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\publisher", entity.Img);
                        FileInfo fi1 = new FileInfo(Img);
                        if (fi1 != null)
                        {
                            System.IO.File.Delete(Img);
                            fi1.Delete();
                        }
                    }
                }
                if (file2 != null)
                {
                    if (entity.Back_img != null)
                    {
                        Back_img = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\publisher", entity.Back_img);
                        FileInfo fi2 = new FileInfo(Back_img);
                        if (fi2 != null)
                        {
                            System.IO.File.Delete(Back_img);
                            fi2.Delete();
                        }
                    }
                }

                entity.PublisherName = model.PublisherName;
                entity.IsApproved = model.IsApproved;
                if (file1 != null)
                {
                    if (Path.GetExtension(file1.FileName) == ".png" || Path.GetExtension(file1.FileName) == ".jpg" ||
                        Path.GetExtension(file1.FileName) == ".jpeg" || Path.GetExtension(file1.FileName) == ".gif" ||
                        Path.GetExtension(file1.FileName) == ".webp" || Path.GetExtension(file1.FileName) == ".svg")
                    {
                        if (file1.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file1.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\publisher", randomName);
                            using (var image = Image.Load(file1.OpenReadStream()))
                            {
                                string newSize = ResizeImage(image, 320, 200);
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
                        Path.GetExtension(file2.FileName) == ".webp" || Path.GetExtension(file2.FileName) == ".svg")
                    {
                        if (file2.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file2.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Back_img = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\publisher", randomName);
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

                _pubService.Update(entity);
                return RedirectToAction("PublisherList");
            }
            return View(model);
        }

        public IActionResult PurchasedPOGEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _ppogService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var entity0 = _pogService.GetAll();
            var entity1 = _userManager.Users.ToList();
            var entity2 = _gnService.GetAll();

            var model = new PurchasedPOGModel()
            {
                Id = entity.Id,
                UserId = entity.UserId,
                POGId = entity.POGId,
                IsApproved = entity.IsApproved,
                DateTime = entity.DateTime,

                POGs = entity0,
                Users = entity1,
                GNs = entity2
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult PurchasedPOGEdit(PurchasedPOGModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _ppogService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.UserId = model.UserId;
                entity.POGId = model.POGId;
                entity.IsApproved = model.IsApproved;
                entity.DateTime = model.DateTime;

                _ppogService.Update(entity);
                return RedirectToAction("PurchasedPOGList");
            }
            return View(model);
        }

        public IActionResult PurchasedProductEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _ppService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var entity0 = _proService.GetAll();
            var entity1 = _userManager.Users.ToList();

            var model = new PurchasedProductModel()
            {
                Id = entity.Id,
                UserId = entity.UserId,
                ProductId = entity.ProductId,
                IsApproved = entity.IsApproved,
                IsXbox = entity.IsXbox,
                DateTime = entity.DateTime,

                Pros = entity0,
                Users = entity1
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult PurchasedProductEdit(PurchasedProductModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _ppService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.UserId = model.UserId;
                entity.ProductId = model.ProductId;
                entity.IsApproved = model.IsApproved;
                entity.IsXbox = model.IsXbox;
                entity.DateTime = model.DateTime;

                _ppService.Update(entity);
                return RedirectToAction("PurchasedProductList");
            }
            return View(model);
        }

        public IActionResult GameItemEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _giService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new GameItemModel()
            {
                Id = entity.Id,
                GameItemName = entity.GameItemName,
                GNId = entity.GNId,
                IsApproved = entity.IsApproved,
                GNs = _gnService.GetAll()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult GameItemEdit(GameItemModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _giService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.GameItemName = model.GameItemName;
                entity.GNId = model.GNId;
                entity.IsApproved = model.IsApproved;

                _giService.Update(entity);
                return RedirectToAction("GameItemList");
            }
            return View(model);
        }

        public IActionResult LanguageTextEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _ltService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new LanguageTextModel()
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                LaguageId = entity.LaguageId,
                Text = entity.Text,
                IsApproved = entity.IsApproved,
                Pros = _proService.GetAll(),
                Lans = _lanService.GetAll()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult LanguageTextEdit(LanguageTextModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _ltService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.ProductId = model.ProductId;
                entity.LaguageId = model.LaguageId;
                entity.Text = model.Text;
                entity.IsApproved = model.IsApproved;

                _ltService.Update(entity);
                return RedirectToAction("LanguageTextList");
            }
            return View(model);
        }

        public IActionResult InstructionPanelEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _ipService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new InstructionPanelModel()
            {
                Id = entity.Id,
                ImgTitle = entity.ImgTitle,
                Img = entity.Img,
                Text = entity.Text,
                Product = entity.Product,
                POG = entity.POG,
                IsApproved = entity.IsApproved
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> InstructionPanelEdit(InstructionPanelModel model, IFormFile file, IFormFile file1)
        {
            if (ModelState.IsValid)
            {
                var entity = _ipService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                string Img;
                string ImgTitle;
                if (file != null)
                {
                    if (entity.Img != null)
                    {
                        Img = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\instructionpanel", entity.Img);
                        FileInfo fi1 = new FileInfo(Img);
                        if (fi1 != null)
                        {
                            System.IO.File.Delete(Img);
                            fi1.Delete();
                        }
                    }
                }
                if (file1 != null)
                {
                    if (entity.ImgTitle != null)
                    {
                        ImgTitle = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\instructionpanel", entity.ImgTitle);
                        FileInfo fi2 = new FileInfo(ImgTitle);
                        if (fi2 != null)
                        {
                            System.IO.File.Delete(ImgTitle);
                            fi2.Delete();
                        }
                    }
                }
                entity.Text = model.Text;
                entity.Product = model.Product;
                entity.POG = model.POG;
                entity.IsApproved = model.IsApproved;
                if (file != null)
                {
                    if (Path.GetExtension(file.FileName) == ".png" || Path.GetExtension(file.FileName) == ".jpg" ||
                        Path.GetExtension(file.FileName) == ".jpeg" || Path.GetExtension(file.FileName) == ".gif" ||
                        Path.GetExtension(file.FileName) == ".webp" || Path.GetExtension(file.FileName) == ".svg")
                    {
                        if (file.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\instructionpanel", randomName);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
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
                if (file1 != null)
                {
                    if (Path.GetExtension(file1.FileName) == ".png" || Path.GetExtension(file1.FileName) == ".jpg" ||
                        Path.GetExtension(file1.FileName) == ".jpeg" || Path.GetExtension(file1.FileName) == ".gif" ||
                        Path.GetExtension(file1.FileName) == ".webp" || Path.GetExtension(file1.FileName) == ".svg")
                    {
                        if (file1.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file1.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_Title_{Guid.NewGuid()}{extention}");
                            entity.ImgTitle = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\instructionpanel", randomName);
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

                _ipService.Update(entity);
                return RedirectToAction("InstructionPanelList");
            }
            return View(model);
        }

        public IActionResult IndexSliderEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _isService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new IndexSliderModel()
            {
                Id = entity.Id,
                Img = entity.Img,
                IndexSliderName = entity.IndexSliderName,
                Text = entity.Text,
                UrlTitle = entity.UrlTitle,
                Url = entity.Url,
                IsApproved = entity.IsApproved
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> IndexSliderEdit(IndexSliderModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var entity = _isService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                string Img;
                if (file != null)
                {
                    if (entity.Img != null)
                    {
                        Img = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\indexslider", entity.Img);
                        FileInfo fi1 = new FileInfo(Img);
                        if (fi1 != null)
                        {
                            System.IO.File.Delete(Img);
                            fi1.Delete();
                        }
                    }
                }
                entity.IndexSliderName = model.IndexSliderName;
                entity.Text = model.Text;
                entity.UrlTitle = model.UrlTitle;
                entity.Url = model.Url;
                entity.IsApproved = model.IsApproved;
                if (file != null)
                {
                    if (Path.GetExtension(file.FileName) == ".png" || Path.GetExtension(file.FileName) == ".jpg" ||
                        Path.GetExtension(file.FileName) == ".jpeg" || Path.GetExtension(file.FileName) == ".gif" ||
                        Path.GetExtension(file.FileName) == ".webp" || Path.GetExtension(file.FileName) == ".svg")
                    {
                        if (file.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\indexslider", randomName);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
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

                _isService.Update(entity);
                return RedirectToAction("IndexSliderList");
            }
            return View(model);
        }

        public IActionResult XboxdataEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _xdService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new XboxdataModel()
            {
                Id = entity.Id,
                Img1 = entity.Img1,
                Img2 = entity.Img2,
                Img3 = entity.Img3,
                Img4 = entity.Img4,
                Img5 = entity.Img5,
                Title = entity.Title,
                Login = entity.Login,
                Password = entity.Password,
                Price = entity.Price,
                SPrice = entity.SPrice
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> XboxdataEdit(XboxdataModel model, IFormFile file1, IFormFile file2, IFormFile file3, IFormFile file4, IFormFile file5)
        {
            if (ModelState.IsValid)
            {
                var entity = _xdService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                string Img1;
                string Img2;
                string Img3;
                string Img4;
                string Img5;
                if (file1 != null)
                {
                    if (entity.Img1 != null)
                    {
                        Img1 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\xboxdata", entity.Img1);
                        FileInfo fi1 = new FileInfo(Img1);
                        if (fi1 != null)
                        {
                            System.IO.File.Delete(Img1);
                            fi1.Delete();
                        }
                    }
                }
                if (file2 != null)
                {
                    if (entity.Img2 != null)
                    {
                        Img2 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\xboxdata", entity.Img2);
                        FileInfo fi2 = new FileInfo(Img2);
                        if (fi2 != null)
                        {
                            System.IO.File.Delete(Img2);
                            fi2.Delete();
                        }
                    }
                }
                if (file3 != null)
                {
                    if (entity.Img3 != null)
                    {
                        Img3 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\xboxdata", entity.Img3);
                        FileInfo fi2 = new FileInfo(Img3);
                        if (fi2 != null)
                        {
                            System.IO.File.Delete(Img3);
                            fi2.Delete();
                        }
                    }
                }
                if (file4 != null)
                {
                    if (entity.Img4 != null)
                    {
                        Img4 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\xboxdata", entity.Img4);
                        FileInfo fi2 = new FileInfo(Img4);
                        if (fi2 != null)
                        {
                            System.IO.File.Delete(Img4);
                            fi2.Delete();
                        }
                    }
                }
                if (file5 != null)
                {
                    if (entity.Img5 != null)
                    {
                        Img5 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\xboxdata", entity.Img5);
                        FileInfo fi2 = new FileInfo(Img5);
                        if (fi2 != null)
                        {
                            System.IO.File.Delete(Img5);
                            fi2.Delete();
                        }
                    }
                }
                entity.Title = model.Title;
                entity.Login = model.Login;
                entity.Password = model.Password;
                entity.Price = model.Price;
                entity.SPrice = model.SPrice;
                if (file1 != null)
                {
                    if (Path.GetExtension(file1.FileName) == ".png" || Path.GetExtension(file1.FileName) == ".jpg" ||
                        Path.GetExtension(file1.FileName) == ".jpeg" || Path.GetExtension(file1.FileName) == ".gif" ||
                        Path.GetExtension(file1.FileName) == ".webp" || Path.GetExtension(file1.FileName) == ".svg")
                    {
                        if (file1.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file1.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img1 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\xboxdata", randomName);
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
                            //var extention = Path.GetExtension(file2.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img2 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\xboxdata", randomName);
                            using (var image = Image.Load(file2.OpenReadStream()))
                            {
                                string newSize = ResizeImage(image, 600, 350);
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
                        Path.GetExtension(file3.FileName) == ".webp" || Path.GetExtension(file3.FileName) == ".svg")
                    {
                        if (file3.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file3.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img3 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\xboxdata", randomName);
                            using (var image = Image.Load(file3.OpenReadStream()))
                            {
                                string newSize = ResizeImage(image, 600, 350);
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
                        Path.GetExtension(file4.FileName) == ".webp" || Path.GetExtension(file4.FileName) == ".svg")
                    {
                        if (file4.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file4.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img4 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\xboxdata", randomName);
                            using (var image = Image.Load(file4.OpenReadStream()))
                            {
                                string newSize = ResizeImage(image, 600, 350);
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
                        Path.GetExtension(file5.FileName) == ".webp" || Path.GetExtension(file5.FileName) == ".svg")
                    {
                        if (file5.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file5.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img5 = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\xboxdata", randomName);
                            using (var image = Image.Load(file5.OpenReadStream()))
                            {
                                string newSize = ResizeImage(image, 600, 350);
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

                _xdService.Update(entity);
                return RedirectToAction("XboxdataList");
            }
            return View(model);
        }

        public IActionResult XboxgameEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _xgService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new XboxgameModel()
            {
                Id = entity.Id,
                Img = entity.Img,
                GameName = entity.GameName,
                Priority = entity.Priority
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> XboxgameEdit(XboxgameModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var entity = _xgService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                string Img;
                if (file != null)
                {
                    if (entity.Img != null)
                    {
                        Img = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\xboxdata", entity.Img);
                        FileInfo fi1 = new FileInfo(Img);
                        if (fi1 != null)
                        {
                            System.IO.File.Delete(Img);
                            fi1.Delete();
                        }
                    }
                }
                entity.GameName = model.GameName;
                entity.Priority = model.Priority;
                if (file != null)
                {
                    if (Path.GetExtension(file.FileName) == ".png" || Path.GetExtension(file.FileName) == ".jpg" ||
                        Path.GetExtension(file.FileName) == ".jpeg" || Path.GetExtension(file.FileName) == ".gif" ||
                        Path.GetExtension(file.FileName) == ".webp" || Path.GetExtension(file.FileName) == ".svg")
                    {
                        if (file.Length < 4500000)
                        {
                            //var extention = Path.GetExtension(file.FileName);
                            var extention = ".webp";
                            var randomName = string.Format($"img_{Guid.NewGuid()}{extention}");
                            entity.Img = randomName;
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\static\\img\\xboxgame", randomName);
                            using (var image = Image.Load(file.OpenReadStream()))
                            {
                                string newSize = ResizeImage(image, 200, 320);
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

                _xgService.Update(entity);
                return RedirectToAction("XboxgameList");
            }
            return View(model);
        }

        ///////////////////////////////////////////////////// UnApproved & Approved //
        [HttpPost]
        public IActionResult UnApprovedPro(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _proService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.IsApproved = false;

                _proService.Update(entity);
                return RedirectToAction("ProductList");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult ApprovedPro(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _proService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.IsApproved = true;

                _proService.Update(entity);
                return RedirectToAction("ProductDeletedList");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult UnApprovedPOG(Product_of_GamerModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _pogService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.IsApproved = false;

                _pogService.Update(entity);
                return RedirectToAction("ProductOfGamerList");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult ApprovedPOG(Product_of_GamerModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _pogService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.IsApproved = true;

                _pogService.Update(entity);
                return RedirectToAction("ProductOfGamerDeletedList");
            }
            return View(model);
        }


        ///////////////////////////////////////////////////// Delete //
        [HttpPost]
        public IActionResult DeleteAC(int Id)
        {
            var entity = _acService.GetById(Id);
            if (entity != null)
            {
                _acService.Delete(entity);
            }
            return RedirectToAction("ActivationCountryList");
        }

        [HttpPost]
        public IActionResult DeleteCP(int Id)
        {
            var entity = _cpService.GetById(Id);
            if (entity != null)
            {
                _cpService.Delete(entity);
            }
            return RedirectToAction("CameraPerspectiveList");
        }

        [HttpPost]
        public IActionResult DeleteCur(int Id)
        {
            var entity = _curService.GetById(Id);
            if (entity != null)
            {
                _curService.Delete(entity);
            }
            return RedirectToAction("CurrencyList");
        }

        [HttpPost]
        public IActionResult DeleteDev(int Id)
        {
            var entity = _devService.GetById(Id);
            if (entity != null)
            {
                string Img;
                string Back_img;
                if (entity.Img != null)
                {
                    Img = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\developer", entity.Img);
                    FileInfo fi = new FileInfo(Img);
                    if (fi != null)
                    {
                        System.IO.File.Delete(Img);
                        fi.Delete();
                    }
                }
                if (entity.Back_img != null)
                {
                    Back_img = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\developer", entity.Back_img);
                    FileInfo fi1 = new FileInfo(Back_img);
                    if (fi1 != null)
                    {
                        System.IO.File.Delete(Back_img);
                        fi1.Delete();
                    }
                }
                _devService.Delete(entity);
            }
            return RedirectToAction("DeveloperList");
        }

        [HttpPost]
        public IActionResult DeleteDiv(int Id)
        {
            var entity = _divService.GetById(Id);
            if (entity != null)
            {
                _divService.Delete(entity);
            }
            return RedirectToAction("DiviceList");
        }

        [HttpPost]
        public IActionResult DeleteGC(int Id)
        {
            var entity = _gcService.GetById(Id);
            if (entity != null)
            {
                _gcService.Delete(entity);
            }
            return RedirectToAction("GameCategoryList");
        }

        [HttpPost]
        public IActionResult DeleteGN(int Id)
        {
            var entity = _gnService.GetById(Id);
            if (entity != null)
            {
                _gnService.Delete(entity);
            }
            return RedirectToAction("GameNameList");
        }

        [HttpPost]
        public IActionResult DeleteJan(int Id)
        {
            var entity = _janService.GetById(Id);
            if (entity != null)
            {
                _janService.Delete(entity);
            }
            return RedirectToAction("JanraList");
        }

        [HttpPost]
        public IActionResult DeleteLang(int Id)
        {
            var entity = _lanService.GetById(Id);
            if (entity != null)
            {
                _lanService.Delete(entity);
            }
            return RedirectToAction("LanguageList");
        }

        [HttpPost]
        public IActionResult DeletePlat(int Id)
        {
            var entity = _platService.GetById(Id);
            if (entity != null)
            {
                _platService.Delete(entity);
            }
            return RedirectToAction("PlatformList");
        }

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
            return RedirectToAction("ProductOfGamerList");
        }

        [HttpPost]
        public IActionResult DeletePro(int Id)
        {
            var entity = _proService.GetById(Id);
            if (entity != null)
            {
                string Slider_img1;
                string Slider_img2;
                string Slider_img3;
                string Main_img;
                if (entity.Main_img != null)
                {
                    Main_img = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\product", entity.Main_img);
                    FileInfo fi = new FileInfo(Main_img);
                    if (fi != null)
                    {
                        System.IO.File.Delete(Main_img);
                        fi.Delete();
                    }
                    Main_img = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\product", $"big{entity.Main_img}");
                    FileInfo fi1 = new FileInfo(Main_img);
                    if (fi1 != null)
                    {
                        System.IO.File.Delete(Main_img);
                        fi1.Delete();
                    }
                }
                if (entity.Slider_img1 != null)
                {
                    Slider_img1 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\product", entity.Slider_img1);
                    FileInfo fi0 = new FileInfo(Slider_img1);
                    if (fi0 != null)
                    {
                        System.IO.File.Delete(Slider_img1);
                        fi0.Delete();
                    }
                }
                if (entity.Slider_img2 != null)
                {
                    Slider_img2 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\product", entity.Slider_img2);
                    FileInfo fi1 = new FileInfo(Slider_img2);
                    if (fi1 != null)
                    {
                        System.IO.File.Delete(Slider_img2);
                        fi1.Delete();
                    }
                }
                if (entity.Slider_img3 != null)
                {
                    Slider_img3 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\product", entity.Slider_img3);
                    FileInfo fi2 = new FileInfo(Slider_img3);
                    if (fi2 != null)
                    {
                        System.IO.File.Delete(Slider_img3);
                        fi2.Delete();
                    }
                }
                _proService.Delete(entity);
            }
            return RedirectToAction("ProductList");
        }

        [HttpPost]
        public IActionResult DeletePub(int Id)
        {
            var entity = _pubService.GetById(Id);
            if (entity != null)
            {
                string Img;
                string Back_img;
                if (entity.Img != null)
                {
                    Img = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\publisher", entity.Img);
                    FileInfo fi = new FileInfo(Img);
                    if (fi != null)
                    {
                        System.IO.File.Delete(Img);
                        fi.Delete();
                    }
                }
                if (entity.Back_img != null)
                {
                    Back_img = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\publisher", entity.Back_img);
                    FileInfo fi0 = new FileInfo(Back_img);
                    if (fi0 != null)
                    {
                        System.IO.File.Delete(Back_img);
                        fi0.Delete();
                    }
                }
                _pubService.Delete(entity);
            }
            return RedirectToAction("PublisherList");
        }

        [HttpPost]
        public IActionResult DeletePPOG(int Id)
        {
            var entity = _ppogService.GetById(Id);
            if (entity != null)
            {
                _ppogService.Delete(entity);
            }
            return RedirectToAction("PurchasedPOGList");
        }

        [HttpPost]
        public IActionResult DeletePP(int Id)
        {
            var entity = _ppService.GetById(Id);
            if (entity != null)
            {
                _ppService.Delete(entity);
            }
            return RedirectToAction("PurchasedProductList");
        }

        [HttpPost]
        public IActionResult DeleteGI(int Id)
        {
            var entity = _giService.GetById(Id);
            if (entity != null)
            {
                _giService.Delete(entity);
            }
            return RedirectToAction("GameItemList");
        }

        [HttpPost]
        public IActionResult DeleteLT(int Id)
        {
            var entity = _ltService.GetById(Id);
            if (entity != null)
            {
                _ltService.Delete(entity);
            }
            return RedirectToAction("LanguageTextList");
        }

        [HttpPost]
        public IActionResult DeleteIP(int Id)
        {
            var entity = _ipService.GetById(Id);
            if (entity != null)
            {
                string Img;
                string ImgTitle;
                if (entity.Img != null)
                {
                    Img = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\instructionpanel", entity.Img);
                    FileInfo fi = new FileInfo(Img);
                    if (fi != null)
                    {
                        System.IO.File.Delete(Img);
                        fi.Delete();
                    }
                }
                if (entity.ImgTitle != null)
                {
                    ImgTitle = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\instructionpanel", entity.ImgTitle);
                    FileInfo fi0 = new FileInfo(ImgTitle);
                    if (fi0 != null)
                    {
                        System.IO.File.Delete(ImgTitle);
                        fi0.Delete();
                    }
                }
                _ipService.Delete(entity);
            }
            return RedirectToAction("InstructionPanelList");
        }

        [HttpPost]
        public IActionResult DeleteIS(int Id)
        {
            var entity = _isService.GetById(Id);
            if (entity != null)
            {
                string Img;
                if (entity.Img != null)
                {
                    Img = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\indexslider", entity.Img);
                    FileInfo fi = new FileInfo(Img);
                    if (fi != null)
                    {
                        System.IO.File.Delete(Img);
                        fi.Delete();
                    }
                }
                _isService.Delete(entity);
            }
            return RedirectToAction("IndexSliderList");
        }

        [HttpPost]
        public IActionResult DeleteXD(int Id)
        {
            var entity = _xdService.GetById(Id);
            if (entity != null)
            {
                string Img1;
                string Img2;
                string Img3;
                string Img4;
                string Img5;
                if (entity.Img1 != null)
                {
                    Img1 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\xboxdata", entity.Img1);
                    FileInfo fi = new FileInfo(Img1);
                    if (fi != null)
                    {
                        System.IO.File.Delete(Img1);
                        fi.Delete();
                    }
                }
                if (entity.Img2 != null)
                {
                    Img2 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\xboxdata", entity.Img2);
                    FileInfo fi1 = new FileInfo(Img2);
                    if (fi1 != null)
                    {
                        System.IO.File.Delete(Img2);
                        fi1.Delete();
                    }
                }
                if (entity.Img3 != null)
                {
                    Img3 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\xboxdata", entity.Img3);
                    FileInfo fi2 = new FileInfo(Img3);
                    if (fi2 != null)
                    {
                        System.IO.File.Delete(Img3);
                        fi2.Delete();
                    }
                }
                if (entity.Img4 != null)
                {
                    Img4 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\xboxdata", entity.Img4);
                    FileInfo fi3 = new FileInfo(Img4);
                    if (fi3 != null)
                    {
                        System.IO.File.Delete(Img4);
                        fi3.Delete();
                    }
                }
                if (entity.Img5 != null)
                {
                    Img5 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\xboxdata", entity.Img5);
                    FileInfo fi4 = new FileInfo(Img5);
                    if (fi4 != null)
                    {
                        System.IO.File.Delete(Img5);
                        fi4.Delete();
                    }
                }
                _xdService.Delete(entity);
            }
            return RedirectToAction("XboxdataList");
        }

        [HttpPost]
        public IActionResult DeleteXG(int Id)
        {
            var entity = _xgService.GetById(Id);
            if (entity != null)
            {
                string Img;
                if (entity.Img != null)
                {
                    Img = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\static\\img\\xboxgame", entity.Img);
                    FileInfo fi = new FileInfo(Img);
                    if (fi != null)
                    {
                        System.IO.File.Delete(Img);
                        fi.Delete();
                    }
                }
                _xgService.Delete(entity);
            }
            return RedirectToAction("XboxgameList");
        }

        [HttpPost]
        public IActionResult DeletePPH(int Id)
        {
            var entity = _pphService.GetById(Id);
            if (entity != null)
            {
                _pphService.Delete(entity);
            }
            return RedirectToAction("PaymentPHistoryList");
        }

        [HttpPost]
        public IActionResult DeletePPOGH(int Id)
        {
            var entity = _ppoghService.GetById(Id);
            if (entity != null)
            {
                _ppoghService.Delete(entity);
            }
            return RedirectToAction("PaymentPOGHistoryList");
        }

        public string ResizeImage(Image img, int maxWidth, int maxHeight)
        {
            if (img.Width > maxWidth || img.Height > maxHeight)
            {
                double widthRatio = (double)img.Width / (double)maxWidth;
                double heightRatio = (double)img.Height / (double)maxHeight;
                double ratio = Math.Max(widthRatio, heightRatio);
                int newWidth = (int)(img.Width / ratio);
                int newHeight  = (int)(img.Height / ratio);
                return newHeight.ToString() + "," + newWidth.ToString();
            }
            else
            {
                return img.Height.ToString() + "," + img.Width.ToString();
            }
        }
    }
}