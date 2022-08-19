﻿using AutoMapper;
using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.Agent;
using RealtyApp.Core.Application.DTOs.Email;
using RealtyApp.Core.Application.Helpers;
using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IImmovableAssetService _immovableService;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IMapper mapper, IImmovableAssetService immovableService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _immovableService = immovableService;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _accountService.AuthenticateAsyncWebApp(loginRequest);
            return userResponse;
        }
        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        //public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin)
        //{
        //    RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);

        //    //Alex bro, tirarle un ojo al servicio AccountServices, puedes modificar los metodos como te convengan.
        //    //no le hice mucho solo lo deje definido con la plantilla de quejo david. Ya el BasicNoExiste.

        //    //return await _accountService.RegisterBasicUserAsync(registerRequest, origin);
        //    return new RegisterResponse();

        //}
        public async Task<RegisterResponse> RegisterAgentUser(SaveUserViewModel vm)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterAgentUserAsync(registerRequest);
        }

        public async Task<RegisterResponse> RegisterClientUser(SaveUserViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterClientUserAsync(registerRequest, origin);
        }

        public async Task<RegisterResponse> RegisterDeveloperUser(SaveUserViewModel vm)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterDeveloperUserAsync(registerRequest);
        }

        public async Task<RegisterResponse> RegisterAdministratorUser(SaveUserViewModel vm)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterAdministratorUserAsync(registerRequest);
        }

        public async Task<SaveUserViewModel> Update(SaveUserViewModel vm, string id)
        {
            vm.Id = id;
            return await _accountService.UpdateAsync(vm);
        }
        public async Task UpdateUserImageAsync(SaveUserViewModel vm)
        {
            await _accountService.UpdateUserImageAsync(vm);
        }

        public async Task DeleteAsync(string id)
        {
            await _accountService.DeleteAsync(id);
        }

        public async Task<List<UserViewModel>> GetAllUsersAdmin()
        {
            return await _accountService.GetAllUserAdminAsync();
        }
        public async Task<List<UserViewModel>> GetAllUserAgentAsync()
        {
            return await _accountService.GetAllUserAgentAsync();
        }

        public async Task<List<UserViewModel>> GetAllUsersDeveloper()
        {
            return await _accountService.GetAllUserDeveloperAsync();
        }

        public async Task<SaveUserViewModel> GetUserById(string id)
        {
            return await _accountService.GetUserByIdAsync(id);
        }

        public async Task<List<UserViewModel>> GetUserAgentByName(string name)
        {
            return await _accountService.GetUserAgentByNameAsync(name);
        }

        public async Task<List<AgentDTO>> GetAllAgents()
        {
            var users = await GetAllUserAgentAsync();
            var immovables = await _immovableService.GetAllViewModel();
            List<AgentDTO> agents = new();

            if (users != null)
            {
                foreach (var user in users)
                {

                    agents.Add(new AgentDTO()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        CardIdentification = user.CardIdentification,
                        Email = user.Email,
                        Phone = user.Phone,
                        PropertiesQuantity = immovables.Where(x => x.AgentId == user.Id).Count()
                    });
                }
            }
            return agents;
        }

        public async Task<AgentDTO> GetAgentById(string id)
        {
            var user = await GetUserById(id);
            var immovables = await _immovableService.GetAllViewModel();
            AgentDTO agent = new()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CardIdentification = user.CardIdentification,
                Email = user.Email,
                Phone = user.Phone,
                PropertiesQuantity = immovables.Where(x => x.AgentId == user.Id).Count()
            };

            return agent;
        }

        public async Task<bool> ChangeUserStatus(string id, string status = null)
        {
            var operationStatus = await _accountService.ChangeUserStatusAsync(id, status);
            return operationStatus;
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task<CountUser> CountClient()
        {
            return await _accountService.CountClient();
        }

        public async Task<CountUser> CountAgent()
        {
            return await _accountService.CountAgent();
        }

        public async Task<CountUser> CountDeveloper()
        {
            return await _accountService.CountDeveloper();
        }


    }
}
