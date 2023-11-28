using api.Entities;
using api.Repository.Implementations;
using api.Repository.Interfaces;
using api.Services.Interfaces;

namespace api.Services.Implementations
{
    public class InvoiceStatusDataSeedService : IInvoiceStatusDataSeedService
    {
        private readonly IRepositoryManager _rep;

        public InvoiceStatusDataSeedService(IRepositoryManager rep)
        {
            _rep = rep;
        }

        public void SeedData()
        {
            int rowsAdded = 0;

            // Read all from enum
            var invoiceStatuses = Enum.GetValues(typeof(InvoiceStatusNames)).Cast<InvoiceStatusNames>();
            foreach (var invoiceStatus in invoiceStatuses)
            {
                var entity = _rep.InvoiceStatusRepository.FindByCondition(
                    x => x.InvoiceStatusId == (int)invoiceStatus, false).FirstOrDefault();
                if (entity == null)
                {
                    _rep.InvoiceStatusRepository.Create(new InvoiceStatus
                    {
                        InvoiceStatusId = (int)invoiceStatus,
                        Name = Enum.GetName(typeof(InvoiceStatusNames), invoiceStatus)
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
