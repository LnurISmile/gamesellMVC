using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IUnitOfWork : IDisposable // Repository
    {
        IActivationCountryRepository ACs { get; }
        ICameraPerspectiveRepository CPs { get; }
        ICurrencyRepository Curs { get; }
        IDeveloperRepository Devs { get; }
        IDiviceRepository Divs { get; }
        IGameCategoryRepository GCs { get; }
        IGameNameRepository GNs { get; }
        IJanraRepository Jans { get; }
        ILanguageRepository Lans { get; }
        IPlatformRepository Plats { get; }
        IProductRepository Pros { get; }
        IProductOfGamerRepository POGs { get; }
        IPublisherRepository Pubs { get; }
        IPurchasedPOGRepository PPOGs { get; }
        IPurchasedProductRepository PPs { get; }
        ICartPRepository Carts { get; }
        ICartPOGRepository Cartpogs { get; }
        ICIPRepository CIs { get; }
        ICIPOGRepository CIpogs { get; }
        IGameItemRepository GIs { get; }
        ILanguageTextRepository LTs { get; }
        IInstructionPanelRepository IPs { get; }
        IIndexSliderRepository ISs { get; }
        IXboxdataRepository XDs { get; }
        IXboxgameRepository XGs { get; }
        IPaymentPHistoryRepository PPHs { get; }
        IPaymentPOGHistoryRepository PPOGHs { get; }
        IBalanceRepository Bals { get; }
        void Save();
        Task SaveAsync();
    }
}
