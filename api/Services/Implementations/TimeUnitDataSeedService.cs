using api.Entities;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using api.Utility;

namespace api.Services.Implementations
{
    public class TimeUnitDataSeedService : ITimeUnitDataSeedService
    {
        private readonly IRepositoryManager _rep;

        public TimeUnitDataSeedService(IRepositoryManager rep)
        {
            _rep = rep;
        }

        public void SeedData()
        {
            int rowId = 1;

            foreach(string name in Constants.TimeUnitNameList)
            {
                var entity = _rep.TimeUnitRepository.FindByCondition(
                    x => x.TimeUnitId == rowId, false)
                    .FirstOrDefault();
                if (entity == null)
                {
                    _rep.TimeUnitRepository.Create(new TimeUnit
                    {
                        TimeUnitId = rowId,
                        Name = name
                    });
                    rowId++;
                }
            }

            if (rowId > 1)
            {
                _rep.Save();
            }
        }
    }
}
