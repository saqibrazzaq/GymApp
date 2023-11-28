using api.Entities;
using api.Repository.Implementations;
using api.Repository.Interfaces;
using api.Services.Interfaces;

namespace api.Services.Implementations
{
    public class DiscountTypeDataSeedService : IDiscountTypeDataSeedService
    {
        private readonly IRepositoryManager _rep;

        public DiscountTypeDataSeedService(IRepositoryManager rep)
        {
            _rep = rep;
        }

        public void SeedData()
        {
            int rowsAdded = 0;

            // Read all from enum
            var discountTypes = Enum.GetValues(typeof(DiscountTypeNames)).Cast<DiscountTypeNames>();
            foreach (var discountType in discountTypes)
            {
                var entity = _rep.DiscountTypeRepository.FindByCondition(
                    x => x.DiscountTypeId == (int)discountType, false).FirstOrDefault();
                if (entity == null)
                {
                    _rep.DiscountTypeRepository.Create(new DiscountType
                    {
                        DiscountTypeId = (int)discountType,
                        Name = Enum.GetName(typeof(DiscountTypeNames), discountType)
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
