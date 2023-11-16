using api.Entities;
using api.Repository.Interfaces;
using api.Services.Interfaces;

namespace api.Services.Implementations
{
    public class UserTypeDataSeedService : IUserTypeDataSeedService
    {
        private readonly IRepositoryManager _repositoryManager;

        public UserTypeDataSeedService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public void SeedData()
        {
            int rowsAdded = 0;

            // Read all from enum
            var userTypes = Enum.GetValues(typeof(UserTypeNames)).Cast<UserTypeNames>();
            foreach (var userType in userTypes)
            {
                var entity = _repositoryManager.UserTypeRepository.FindByCondition(
                    x => x.UserTypeId == (int)userType, false).FirstOrDefault();
                if (entity == null)
                {
                    _repositoryManager.UserTypeRepository.Create(new UserType
                    {
                        UserTypeId = (int)userType,
                        Name = Enum.GetName(typeof(UserTypeNames), userType)
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
