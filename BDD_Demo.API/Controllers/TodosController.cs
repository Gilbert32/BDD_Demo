using System;
using System.Threading.Tasks;
using BDD_Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace BDD_Demo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodosController : Controller
    {
        private TodoService _todoService;

        public TodosController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [Route("GetTodos")]
        [HttpGet]
        public async Task<IActionResult> GetTodos([FromQuery] int id)
        {
            try
            {
                return Ok(await _todoService.GetTodosByUser(id));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}