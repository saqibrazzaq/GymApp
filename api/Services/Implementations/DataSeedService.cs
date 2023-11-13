using api.Services.Interfaces;

namespace api.Services.Implementations
{
    public class DataSeedService : IDataSeedService
    {
        private readonly IAccountTypeDataSeedService _accountTypeSeedService;
        private readonly IRoleDataSeedService _roleDataSeedService;
        private readonly IAccountDataSeedService _accountDataSeedService;
        private readonly IPlanTypeDataSeedService _planTypeDataSeedService;

        public DataSeedService(IAccountTypeDataSeedService accountTypeSeedService,
            IRoleDataSeedService roleDataSeedService,
            IAccountDataSeedService accountDataSeedService,
            IPlanTypeDataSeedService planTypeDataSeedService)
        {
            _accountTypeSeedService = accountTypeSeedService;
            _roleDataSeedService = roleDataSeedService;
            _accountDataSeedService = accountDataSeedService;
            _planTypeDataSeedService = planTypeDataSeedService;
        }

        public async Task SeedData()
        {
            // Sequence is necessary
            // Create account types e.g. unlimited, free, basic
            _accountTypeSeedService.SeedData();
            // Create roles e.g. admin, manager, user
            await _roleDataSeedService.SeedData();
            // Create super admin user with unlimited account
            await _accountDataSeedService.SeedData();
            // Create default plan types e.g. recurring, non recurring
            _planTypeDataSeedService.SeedData();
        }
    }
}
