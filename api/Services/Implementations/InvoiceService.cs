using api.Dtos.Invoice;
using api.Dtos.Plan;
using api.Entities;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using api.Utility.Paging;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api.Services.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;
        private readonly IMyProfileService _myProfileService;
        public InvoiceService(IRepositoryManager rep,
            IMapper mapper,
            IMyProfileService myProfileService)
        {
            _rep = rep;
            _mapper = mapper;
            _myProfileService = myProfileService;
        }

        public async Task<InvoiceRes> Create(InvoiceEditReq dto)
        {
            var user = await _myProfileService.GetLoggedInUser();

            var entity = _mapper.Map<Invoice>(dto);
            entity.AccountId = user.AccountId;

            _rep.InvoiceRepository.Create(entity);
            _rep.Save();
            return _mapper.Map<InvoiceRes>(entity);
        }

        public async Task<InvoiceItemRes> CreateItem(InvoiceItemEditReq dto)
        {
            await FindInvoiceIfExists(dto.InvoiceId);

            var entity = _mapper.Map<InvoiceItem>(dto);
            _rep.InvoiceItemRepository.Create(entity);
            _rep.Save();
            return _mapper.Map<InvoiceItemRes>(entity);
        }

        public async Task Delete(int invoiceId)
        {
            var entity = await FindInvoiceIfExists(invoiceId);
            _rep.InvoiceRepository.Delete(entity);
            _rep.Save();
        }

        private async Task<Invoice> FindInvoiceIfExists(int invoiceId)
        {
            var user = await _myProfileService.GetLoggedInUser();
            var entity = _rep.InvoiceRepository.FindByCondition(
                x => x.AccountId == user.AccountId &&
                x.InvoiceId == invoiceId,
                false,
                include: i => i
                    .Include(x => x.User)
                    .Include(x => x.Status)
                    .Include(x => x.State)
                    )
                .FirstOrDefault();
            if (entity == null) { throw new Exception("Invoice not found " + invoiceId); }

            return entity;
        }

        public async Task DeleteItem(int invoiceItemId)
        {
            var entity = await FindInvoiceItemIfExists(invoiceItemId);
            _rep.InvoiceItemRepository.Delete(entity);
            _rep.Save();
        }

        private async Task<InvoiceItem> FindInvoiceItemIfExists(int invoiceItemId)
        {
            var user = await _myProfileService.GetLoggedInUser();
            var entity = _rep.InvoiceItemRepository.FindByCondition(
                x => x.Invoice.AccountId == user.AccountId &&
                x.InvoiceItemId == invoiceItemId,
                false)
                .FirstOrDefault();
            if (entity == null) { throw new Exception("Invoice item not found " + invoiceItemId); }

            return entity;
        }

        public async Task<InvoiceRes> Get(int invoiceId)
        {
            var entity = await FindInvoiceIfExists(invoiceId);
            return _mapper.Map<InvoiceRes>(entity);
        }

        public async Task<InvoiceItemRes> GetItem(int invoiceItemId)
        {
            var entity = await FindInvoiceItemIfExists(invoiceItemId);
            return _mapper.Map<InvoiceItemRes>(entity);
        }

        public async Task<ApiOkPagedResponse<IEnumerable<InvoiceRes>, MetaData>> Search(InvoiceSearchReq dto)
        {
            var user = await _myProfileService.GetLoggedInUser();
            dto.AccountId = user.AccountId;

            var pagedEntities = _rep.InvoiceRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<InvoiceRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<InvoiceRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }

        public async Task<ApiOkPagedResponse<IEnumerable<InvoiceItemRes>, MetaData>> SearchItems(InvoiceItemSearchReq dto)
        {
            var user = await _myProfileService.GetLoggedInUser();
            dto.AccountId = user.AccountId;

            var pagedEntities = _rep.InvoiceItemRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<InvoiceItemRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<InvoiceItemRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }

        public async Task<InvoiceRes> Update(int invoiceId, InvoiceEditReq dto)
        {
            var entity = await FindInvoiceIfExists(invoiceId);
            _mapper.Map(dto, entity);
            _rep.Save();
            return _mapper.Map<InvoiceRes>(entity);
        }

        public async Task<InvoiceItemRes> UpdateItem(int invoiceItemId, InvoiceItemEditReq dto)
        {
            var entity = await FindInvoiceItemIfExists(invoiceItemId);
            _mapper.Map(dto, entity);
            _rep.Save();
            return _mapper.Map<InvoiceItemRes>(entity);
        }
    }
}
