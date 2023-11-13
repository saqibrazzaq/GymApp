using api.Entities;
using api.Repository.Interfaces;
using api.Services.Interfaces;

namespace api.Services.Implementations
{
    public class PlanTypeDataSeedService : IPlanTypeDataSeedService
    {
        private readonly IRepositoryManager _repositoryManager;

        public PlanTypeDataSeedService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public void SeedData()
        {
            int rowsAdded = 0;

            // Read all from enum
            var planTypes = Enum.GetValues(typeof(PlanTypeNames)).Cast<PlanTypeNames>();
            foreach (var planType in planTypes)
            {
                var entity = _repositoryManager.PlanTypeRepository.FindByCondition(
                    x => x.PlanTypeId == (int)planType, false).FirstOrDefault();
                if (entity == null)
                {
                    _repositoryManager.PlanTypeRepository.Create(new PlanType
                    {
                        PlanTypeId = (int)planType,
                        Name = Enum.GetName(typeof(PlanTypeNames), planType)
                    });
                    rowsAdded++;
                }
            }

            if (rowsAdded > 0)
            {
                _repositoryManager.Save();
            }
        }
    }
}
