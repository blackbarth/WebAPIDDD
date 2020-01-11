using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain.DTOs;
using API.Domain.Entities;
using API.Domain.Interfaces;
using API.Domain.Interfaces.Services.User;
using API.Domain.Models;
using AutoMapper;

namespace API.Service.Services
{
    public class UserService : IUserService
    {
        public IRepository<UserEntity> _repository;
        private readonly IMapper _mapper;
        public UserService(IRepository<UserEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<UserDTO> Get(Guid id)
        {
            var entity = await _repository.SelectAsync(id);
            return _mapper.Map<UserDTO>(entity) ?? new UserDTO();
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var listEntity = await _repository.SelectAsync();
            var dto = _mapper.Map<IEnumerable<UserDTO>>(listEntity);
            return dto;
        }

        public async Task<UserDTOCreateResult> Post(UserDTOCreate user)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var result = await _repository.InsertAsync(entity);
            return _mapper.Map<UserDTOCreateResult>(result);
        }

        public async Task<UserDTOUpdateResult> Put(UserDTOUpdate user)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<UserDTOUpdateResult>(result);
        }
    }
}