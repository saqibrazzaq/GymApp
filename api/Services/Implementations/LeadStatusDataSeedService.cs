using api.Entities;
using api.Repository.Interfaces;
using api.Services.Interfaces;

namespace api.Services.Implementations
{
    public class LeadStatusDataSeedService : ILeadStatusDataSeedService
    {
        private readonly IRepositoryManager _repositoryManager;

        public LeadStatusDataSeedService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public void SeedData()
        {
            int rowsAdded = 0;

            // Read all from enum
            var leadStatuses = Enum.GetValues(typeof(LeadStatusNames)).Cast<LeadStatusNames>();
            foreach (var leadStatus in leadStatuses)
            {
                var entity = _repositoryManager.LeadStatusRepository.FindByCondition(
                    x => x.LeadStatusId == (int)leadStatus, false).FirstOrDefault();
                if (entity == null)
                {
                    _repositoryManager.LeadStatusRepository.Create(new LeadStatus
                    {
                        LeadStatusId = (int)leadStatus,
                        Name = Enum.GetName(typeof(LeadStatusNames), leadStatus)
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
