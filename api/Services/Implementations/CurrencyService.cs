using api.Dtos.Country;
using api.Dtos.Currency;
using api.Entities;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using api.Utility.Paging;
using AutoMapper;

namespace api.Services.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;
        public CurrencyService(IRepositoryManager rep, 
            IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public CurrencyRes Create(CurrencyEditReq dto)
        {
            var entity = _mapper.Map<Currency>(dto);
            _rep.CurrencyRepository.Create(entity);
            _rep.Save();
            return _mapper.Map<CurrencyRes>(entity);
        }

        public void Delete(int currencyId)
        {
            var entity = FindCurrencyIfExists(currencyId, true);
            _rep.CurrencyRepository.Delete(entity);
            _rep.Save();
        }

        private Currency FindCurrencyIfExists(int currencyId, bool trackChanges)
        {
            var entity = _rep.CurrencyRepository.FindByCondition(
                x => x.CurrencyId == currencyId,
                trackChanges)
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No currency found with id " + currencyId); }

            return entity;
        }

        public CurrencyRes Get(int currencyId)
        {
            var entity = FindCurrencyIfExists(currencyId, false);
            return _mapper.Map<CurrencyRes>(entity);
        }

        public ApiOkPagedResponse<IEnumerable<CurrencyRes>, MetaData> Search(CurrencySearchReq dto)
        {
            var pagedEntities = _rep.CurrencyRepository.
                SearchCurrencies(dto, false);
            var dtos = _mapper.Map<IEnumerable<CurrencyRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<CurrencyRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }

        public CurrencyRes Update(int currencyId, CurrencyEditReq dto)
        {
            var entity = FindCurrencyIfExists(currencyId, true);
            _mapper.Map(dto, entity);
            _rep.Save();
            return _mapper.Map<CurrencyRes>(entity);
        }
    }
}
