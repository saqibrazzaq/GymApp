﻿using api.Dtos.User;
using api.Entities;
using api.Utility.Paging;

namespace api.Repository.Interfaces
{
    public interface IUserRepository : IRepositoryBase<AppIdentityUser>
    {
        PagedList<AppIdentityUser> SearchMembers(SearchUsersReq userParameters, bool trackChanges);
        PagedList<AppIdentityUser> SearchStaff(
            SearchUsersReq dto, bool trackChanges);
    }
}
