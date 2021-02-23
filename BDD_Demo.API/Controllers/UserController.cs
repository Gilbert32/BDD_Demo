using System;
using System.Threading.Tasks;
using BDD_Demo.Services;
using BDD_Demo.Services.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BDD_Demo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [Route("GetUser")]
        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery] int id)
        {
            try
            {
                return Ok(await _userService.GetUser(id));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [Route("DismissNotification")]
        [HttpGet]
        public async Task<IActionResult> DismissNotification([FromBody] DismissNotificationRequest request)
        {
            try
            {
                return Ok(await _userService.DismissNotification(request));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [Route("AddUser")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest request)
        {
            try
            {
                return Ok(await _userService.AddUser(request));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
        
    }
}