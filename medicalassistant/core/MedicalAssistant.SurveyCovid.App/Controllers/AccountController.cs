using AutoMapper;
using MedicalAssistant.AspNetCommon.Logging;
using MedicalAssistant.SurveyCovid.Contracts.Dto;
using MedicalAssistant.SurveyCovid.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MedicalAssistant.SurveyCovid.App.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, IMapper mapper, ILogger<AccountController> logger)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResultDto>> Login([FromBody] UserLoginDto userLoginDto)
        {
            _logger.Info($" *******************  userLoginDto = {userLoginDto.Login}, {userLoginDto.Login}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userLoginResult = await _accountService.Login(userLoginDto, IpAddress());
            if (userLoginResult.Logged)
            {
                return Ok(_mapper.Map<UserLoginResultDto>(userLoginResult));
            }

            return BadRequest("Invalid username or password");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _accountService.Register(registerUserDto);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<UserLoginResultDto>> RefreshToken(string refreshToken)
        {
            var response = await _accountService.RefreshToken(refreshToken, IpAddress());

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });

            return Ok(_mapper.Map<UserLoginResultDto>(response));
        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

    }
}
