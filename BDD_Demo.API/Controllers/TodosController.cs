using System;
using System.Threading.Tasks;
using BDD_Demo.Services;
using BDD_Demo.Services.Requests;
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

        [Route("AddTodo")]
        [HttpPost]
        public async Task<IActionResult> AddTodo([FromBody] AddTodoRequest request)
        {
            try
            {
                return Ok(await _todoService.AddTodo(request));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}