using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain.Identity;
using System.Collections.Generic;
using Proagil.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Proagil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        public UserController(IConfiguration config,
                                UserManager<User> userManager,
                                SignInManager<User> signInManager,
                                IMapper mapper){
                                    _signInManager =signInManager;
                                    _mapper = mapper;
                                    _userManager = userManager;
                                    _config = config;
                                }
    

    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser(UserDto userDto)
        {
            return Ok(userDto);
                       
           
        }
    

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
           try
           {
               var user = _mapper.Map<User>(userDto);
               var result = await _userManager.CreateAsync(user, userDto.Password);
               var userToReturn = _mapper.Map<UserDto>(user);
              
               if(result.Succeeded){
                   return Created("GetUser",userToReturn);
               }

               return BadRequest(result.Errors);
           }
           catch (System.Exception)
           {
               
              return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados falhou");
           }
                       
           
        }
         [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            try
            {
               
            }
            catch (System.Exception)
            {
                
                throw;
            }
            return Ok(userLogin);
                       
           
        }
    }
}