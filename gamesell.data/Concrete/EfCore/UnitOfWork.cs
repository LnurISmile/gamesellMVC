using gamesell.data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PlayPointContext _context;
        public UnitOfWork(PlayPointContext ctx)
        {
            _context = ctx;
        }
        private EfCoreActivationCountryRepository _acRepository;
        private EfCoreCameraPerspectiveRepository _cpRepository;
        private EfCoreCurrencyRepository _curRepository;
        private EfCoreDeveloperRepository _devRepository;
        private EfCoreDiviceRepository _divRepository;
        private EfCoreGameCategoryRepository _gcRepository;
        private EfCoreGameNameRepository _gnRepository;
        private EfCoreJanraRepository _janRepository;
        private EfCoreLanguageRepository _lanRepository;
        private EfCorePlatformRepository _platRepository;
        private EfCoreProductOfGamerRepository _pogRepository;
        private EfCoreProductRepository _pRepository;
        private EfCorePublisherRepository _pubRepository;
        private EfCorePurchasedPOGRepository _ppogRepository;
        private EfCorePurchasedProductRepository _ppRepository;
        private EfCoreCartPRepository _cartpRepository;
        private EfCoreCartPOGRepository _cartpogRepository;
        private EfCoreCIPRepository _cipRepository;
        private EfCoreCIPOGRepository _cipogRepository;
        private EfCoreGameItemRepository _giRepository;
        private EfCoreLanguageTextRepository _ltRepository;
        private EfCoreInstructionPanelRepository _ipRepository;
        private EfCoreIndexSliderRepository _isRepository;
        private EfCoreXboxdataRepository _xdRepository;
        private EfCoreXboxgameRepository _xgRepository;
        private EfCorePaymentPHistoryRepository _pphRepository;
        private EfCorePaymentPOGHistoryRepository _ppoghRepository;
        private EfCoreBalanceRepository _balRepository;

        public IActivationCountryRepository ACs =>
            _acRepository = _acRepository ?? new EfCoreActivationCountryRepository(_context);

        public ICameraPerspectiveRepository CPs =>
            _cpRepository = _cpRepository ?? new EfCoreCameraPerspectiveRepository(_context);

        public ICurrencyRepository Curs =>
            _curRepository = _curRepository ?? new EfCoreCurrencyRepository(_context);

        public IDeveloperRepository Devs =>
            _devRepository = _devRepository ?? new EfCoreDeveloperRepository(_context);

        public IDiviceRepository Divs =>
            _divRepository = _divRepository ?? new EfCoreDiviceRepository(_context);

        public IGameCategoryRepository GCs =>
            _gcRepository = _gcRepository ?? new EfCoreGameCategoryRepository(_context);

        public IGameNameRepository GNs =>
            _gnRepository = _gnRepository ?? new EfCoreGameNameRepository(_context);

        public IJanraRepository Jans =>
            _janRepository = _janRepository ?? new EfCoreJanraRepository(_context);

        public ILanguageRepository Lans =>
            _lanRepository = _lanRepository ?? new EfCoreLanguageRepository(_context);

        public IPlatformRepository Plats =>
            _platRepository = _platRepository ?? new EfCorePlatformRepository(_context);

        public IProductRepository Pros =>
            _pRepository = _pRepository ?? new EfCoreProductRepository(_context);

        public IProductOfGamerRepository POGs =>
            _pogRepository = _pogRepository ?? new EfCoreProductOfGamerRepository(_context);

        public IPublisherRepository Pubs =>
            _pubRepository = _pubRepository ?? new EfCorePublisherRepository(_context);

        public IPurchasedPOGRepository PPOGs =>
            _ppogRepository = _ppogRepository ?? new EfCorePurchasedPOGRepository(_context);

        public IPurchasedProductRepository PPs =>
            _ppRepository = _ppRepository ?? new EfCorePurchasedProductRepository(_context);

        public ICartPRepository Carts =>
            _cartpRepository = _cartpRepository ?? new EfCoreCartPRepository(_context);

        public ICartPOGRepository Cartpogs =>
            _cartpogRepository = _cartpogRepository ?? new EfCoreCartPOGRepository(_context);

        public ICIPRepository CIs =>
            _cipRepository = _cipRepository ?? new EfCoreCIPRepository(_context);

        public ICIPOGRepository CIpogs =>
            _cipogRepository = _cipogRepository ?? new EfCoreCIPOGRepository(_context);

        public IGameItemRepository GIs =>
            _giRepository = _giRepository ?? new EfCoreGameItemRepository(_context);

        public ILanguageTextRepository LTs =>
            _ltRepository = _ltRepository ?? new EfCoreLanguageTextRepository(_context);

        public IInstructionPanelRepository IPs =>
            _ipRepository = _ipRepository ?? new EfCoreInstructionPanelRepository(_context);

        public IIndexSliderRepository ISs =>
            _isRepository = _isRepository ?? new EfCoreIndexSliderRepository(_context);

        public IXboxdataRepository XDs =>
            _xdRepository = _xdRepository ?? new EfCoreXboxdataRepository(_context);

        public IXboxgameRepository XGs =>
            _xgRepository = _xgRepository ?? new EfCoreXboxgameRepository(_context);

        public IPaymentPHistoryRepository PPHs =>
            _pphRepository = _pphRepository ?? new EfCorePaymentPHistoryRepository(_context);

        public IPaymentPOGHistoryRepository PPOGHs =>
            _ppoghRepository = _ppoghRepository ?? new EfCorePaymentPOGHistoryRepository(_context);

        public IBalanceRepository Bals =>
            _balRepository = _balRepository ?? new EfCoreBalanceRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
