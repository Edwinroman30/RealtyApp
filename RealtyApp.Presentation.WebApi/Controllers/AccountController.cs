﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RealtyApp.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Sistema de membresia")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("authenticate")]
        [SwaggerOperation(
            Summary = "Login de usuario",
            Description = "Autentica un usuario en el sistema y le retorna un JWT"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateAsync(request));
        }

        [HttpPost("register")]
        [SwaggerOperation(
            Summary = "Creacion de usuario basico",
            Description = "Recibe los parametros necesarios para crear un usuario con el rol basico"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterBasicUserAsync(request, origin));
        }

        [HttpGet("confirm-email")]
        [SwaggerOperation(
            Summary = "Confirmacion de usuario",
            Description = "Permite activar un usuario nuevo"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> ConfirmAccountAsync([FromQuery] string userId , [FromQuery]string token)
        {          
            return Ok(await _accountService.ConfirmAccountAsync(userId, token));
        }


        [HttpPost("forgot-password")]
        [SwaggerOperation(
            Summary = "Recordar contrasenia",
            Description = "Permite al usuario iniciar el proceso para obtener una nueva contrasenia"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ForgotPasswordAsync(request, origin));
        }

        [HttpPost("reset-password")]
        [SwaggerOperation(
            Summary = "Reinicio de contrasenia",
            Description = "Permite al usuario cambiar su contrasenia actual por una nueva"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
        {           
            return Ok(await _accountService.ResetPasswordAsync(request));
        }
    }
}
