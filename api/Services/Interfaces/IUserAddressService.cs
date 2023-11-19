using api.Dtos.Address;

namespace api.Services.Interfaces
{
    public interface IUserAddressService
    {
        Task<UserAddressRes> Create(string email, AddressEditReq dto);
        UserAddressRes Update(int userAddressId, AddressEditReq dto);
        void Delete(int userAddressId);
        UserAddressRes Get(int userAddressId);
        Task<IList<UserAddressRes>> GetAll(string email, bool trackChanges);
    }
}
