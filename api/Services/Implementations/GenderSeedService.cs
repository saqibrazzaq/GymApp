using api.Entities;
using api.Repository.Interfaces;
using api.Services.Interfaces;

namespace api.Services.Implementations
{
    public class GenderSeedService : IGenderSeedService
    {
        private readonly IRepositoryManager _rep;

        public GenderSeedService(IRepositoryManager rep)
        {
            _rep = rep;
        }

        public void SeedData()
        {
            int rowsAdded = 0;

            // Read all from enum
            var genders = Enum.GetValues(typeof(GenderNames)).Cast<GenderNames>();
            foreach (var gender in genders)
            {
                var entity = _rep.GenderRepository.FindByCondition(
                    x => x.GenderId == (int)gender, false).FirstOrDefault();
                if (entity == null)
                {
                    _rep.GenderRepository.Create(new Gender
                    {
                        GenderId = (int)gender,
                        Name = Enum.GetName(typeof(GenderNames), gender),
                        Code = Enum.GetName(typeof(GenderCodes), gender),
                    });
                    rowsAdded++;
                }
            }

            if (rowsAdded > 0)
            {
                _rep.Save();
            }
        }
    }
}
