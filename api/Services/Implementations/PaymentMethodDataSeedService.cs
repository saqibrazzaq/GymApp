using api.Entities;
using api.Repository.Interfaces;
using api.Services.Interfaces;

namespace api.Services.Implementations
{
    public class PaymentMethodDataSeedService : IPaymentMethodDataSeedService
    {
        private readonly IRepositoryManager _rep;

        public PaymentMethodDataSeedService(IRepositoryManager rep)
        {
            _rep = rep;
        }

        public void SeedData()
        {
            int rowsAdded = 0;

            // Read all from enum
            var paymentMethods = Enum.GetValues(typeof(PaymentMethodNames)).Cast<PaymentMethodNames>();
            foreach (var paymentMethod in paymentMethods)
            {
                var entity = _rep.PaymentMethodRepository.FindByCondition(
                    x => x.PaymentMethodId == (int)paymentMethod, false).FirstOrDefault();
                if (entity == null)
                {
                    _rep.PaymentMethodRepository.Create(new PaymentMethod
                    {
                        PaymentMethodId = (int)paymentMethod,
                        Name = Enum.GetName(typeof(PaymentMethodNames), paymentMethod)
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
