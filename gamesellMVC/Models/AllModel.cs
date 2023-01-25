using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class AllModel
    {
        public IEnumerable<ActivationCountryModel> ACmodel { get; set; }
        public IEnumerable<ActivationCountryListViewModel> AClvmodel { get; set; }
        public IEnumerable<CameraPerspectiveModel> CPmodel { get; set; }
        public IEnumerable<CameraPerspectiveListViewModel> CPlvmodel { get; set; }
        public IEnumerable<CurrencyModel> Curmodel { get; set; }
        public IEnumerable<CurrencyListViewModel> Curlvmodel { get; set; }
        public IEnumerable<DeveloperModel> Devmodel { get; set; }
        public IEnumerable<DeveloperListViewModel> Devlvmodel { get; set; }
        public IEnumerable<DiviceModel> Divmodel { get; set; }
        public IEnumerable<DiviceListViewModel> Divlvmodel { get; set; }
        public IEnumerable<GameCategoryModel> GCmodel { get; set; }
        public IEnumerable<GameCategoryListViewModel> GClvmodel { get; set; }
        public IEnumerable<GameNameModel> GNmodel { get; set; }
        public IEnumerable<GameNameListViewModel> GNlvmodel { get; set; }
        public IEnumerable<JanraModel> Janmodel { get; set; }
        public IEnumerable<JanraListViewModel> Janlvmodel { get; set; }
        public IEnumerable<LanguageModel> LanModel { get; set; }
        public IEnumerable<LanguageListViewModel> Lanlvmodel { get; set; }
        public IEnumerable<LoginModel> Loginmodel { get; set; }
        public IEnumerable<PlatformModel> Platmodel { get; set; }
        public IEnumerable<PlatformListViewModel> Platlvmodel { get; set; }
        public IEnumerable<Product_of_GamerModel> POGmodel { get; set; }
        public IEnumerable<Product_of_GamerListViewModel> POGlvmodel { get; set; }
        public IEnumerable<Product_of_GamerDetailModel> POGdmodel { get; set; }
        public IEnumerable<ProductModel> Promodel { get; set; }
        public IEnumerable<ProductListViewModel> Prolvmodel { get; set; }
        public IEnumerable<ProductDetailModel> Prodmodel { get; set; }
        public IEnumerable<PublisherModel> Pubmodel { get; set; }
        public IEnumerable<PublisherListViewModel> Publvmodel { get; set; }
        public IEnumerable<PurchasedPOGModel> PPOGmodel { get; set; }
        public IEnumerable<PurchasedPOGListViewModel> PPOGlvmodel { get; set; }
        public IEnumerable<PurchasedProductModel> PPmodel { get; set; }
        public IEnumerable<PurchasedProductListViewModel> PPlvmodel { get; set; }
        public IEnumerable<RegisterModel> Registermodel { get; set; }
        public IEnumerable<ResetPasswordModel> ResetPasswordmodel { get; set; }
        public IEnumerable<RoleModel> Rolemodel { get; set; }
        public IEnumerable<UserDetailsModel> UserDetailsmodel { get; set; }
        public IEnumerable<UserManage> UserManage { get; set; }
}
}