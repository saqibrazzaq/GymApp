using api.Data;
using api.Dtos.User;
using api.Entities;
using api.Repository.Interfaces;
using api.Utility.Paging;

namespace api.Repository.Implementations
{
    public class UserRepository : RepositoryBase<AppIdentityUser>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<AppIdentityUser> SearchStaff(
            SearchUsersReq userParameters, bool trackChanges)
        {
            // Find users
            var users = FindAll(trackChanges)
                .SearchStaff(userParameters)
                .SortStaff(userParameters.OrderBy)
                .Skip((userParameters.PageNumber - 1) * userParameters.PageSize)
                .Take(userParameters.PageSize)
                .ToList();
            // Get count by using the same search parameters
            var count = FindAll(trackChanges)
                .SearchStaff(userParameters)
                .Count();
            return new PagedList<AppIdentityUser>(users, count, userParameters.PageNumber,
                userParameters.PageSize);
        }

        public PagedList<AppIdentityUser> SearchMembers(
            SearchUsersReq userParameters, bool trackChanges)
        {
            // Find users
            var users = FindAll(trackChanges)
                .SearchMembers(userParameters)
                .SortMembers(userParameters.OrderBy)
                .Skip((userParameters.PageNumber - 1) * userParameters.PageSize)
                .Take(userParameters.PageSize)
                .ToList();
            // Get count by using the same search parameters
            var count = FindAll(trackChanges)
                .SearchMembers(userParameters)
                .Count();
            return new PagedList<AppIdentityUser>(users, count, userParameters.PageNumber,
                userParameters.PageSize);
        }
    }
}
