﻿using api.Dtos.Address;
using api.Entities;
using api.Repository.Interfaces;
using api.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Services.Implementations
{
    public class UserAddressService : IUserAddressService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _rep;
        private readonly UserManager<AppIdentityUser> _userManager;

        public UserAddressService(IMapper mapper,
            IRepositoryManager rep,
            UserManager<AppIdentityUser> userManager)
        {
            _mapper = mapper;
            _rep = rep;
            _userManager = userManager;
        }

        public async Task<UserAddressRes> Create(string email, AddressEditReq dto)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userAddressDto = new UserAddressEditReq
            {
                Address = dto,
                UserId = user.Id
            };
            var entity = _mapper.Map<UserAddress>(userAddressDto);

            _rep.UserAddressRepository.Create(entity);
            _rep.Save();
            return _mapper.Map<UserAddressRes>(entity);
        }

        public void Delete(int userAddressId)
        {
            var entity = FindUserAddressIfExists(userAddressId, true);
            _rep.UserAddressRepository.Delete(entity);
            _rep.Save();
        }

        private UserAddress FindUserAddressIfExists(int userAddressId, bool trackChanges)
        {
            var entity = _rep.UserAddressRepository.FindByCondition(
                x => x.UserAddressId == userAddressId,
                trackChanges,
                include: i => i
                    .Include(x => x.Address.State.Country)
                    )
                .FirstOrDefault();
            if (entity == null) throw new Exception("No address found with id" + userAddressId);

            return entity;
        }

        public UserAddressRes Get(int userAddressId)
        {
            var entity = FindUserAddressIfExists(userAddressId, false);
            return _mapper.Map<UserAddressRes>(entity);
        }

        public async Task<IList<UserAddressRes>> GetAll(string email, bool trackChanges)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var entities = _rep.UserAddressRepository.FindByCondition(
                x => x.UserId.ToString() == user.Id,
                trackChanges,
                include: i => i.Include(x => x.Address.State.Country))
                .ToList();
            return _mapper.Map<IList<UserAddressRes>>(entities);
        }

        public UserAddressRes Update(int userAddressId, AddressEditReq dto)
        {
            var entity = FindUserAddressIfExists(userAddressId, true);
            if (dto.IsPrimary) { MakeOtherAddressesNonPrimary(userAddressId); }
            
            _mapper.Map(dto, entity.Address);
            _rep.Save();
            return _mapper.Map<UserAddressRes>(entity);
        }

        private void MakeOtherAddressesNonPrimary(int userAddressId)
        {
            var userId = _rep.UserAddressRepository.FindByCondition(
                x => x.UserAddressId == userAddressId,
                false)
                .FirstOrDefault()
                .UserId;
            var addressIds = _rep.UserAddressRepository.FindByCondition(
                x => x.UserId == userId &&
                x.UserAddressId != userAddressId,
                false)
                .Select(x => x.AddressId)
                .ToList();

            var addresses = _rep.AddressRepository.FindByCondition(
                x => addressIds.Contains(x.AddressId),
                true).ToList();
            for (int i = 0; i < addresses.Count(); i++)
            {
                addresses.ElementAt(i).IsPrimary = false;
            }
        }
    }
}
