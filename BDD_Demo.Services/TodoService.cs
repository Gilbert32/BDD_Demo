using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDD.Domain.Contexts;
using BDD.Domain.Models;
using BDD_Demo.Services.Requests;
using BDD_Demo.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BDD_Demo.Services
{
    public class TodoService
    {
        private BddDemoContext _context;
        private UserService _userService;

        public TodoService(BddDemoContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<List<TodoViewModel>> GetTodosByUser(long userId)
        {
            _userService.CheckUserExists(userId);
            var todos = _context.Todos.Where(x => x.UserId == userId && x.IsDeleted == false);
            return await todos.Select(x => new TodoViewModel()
                {
                    Id = x.Id,
                    Text = x.Text,
                    Title = x.Title,
                    CreatedDate = x.CreatedDate,
                    TargetDate = x.TargetDate
                })
                .ToListAsync();
        }

        public async Task<TodoViewModel> AddTodo(AddTodoRequest request)
        {
            var todo = new Todo()
            {
                UserId = request.UserId, Title = request.Title, Text = request.Text, TargetDate = request.TargetDate,
                CreatedDate = DateTime.UtcNow
            };
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
            return new TodoViewModel()
            {
                Id = todo.Id,
                Text = todo.Text,
                Title = todo.Title,
                CreatedDate = todo.CreatedDate,
                TargetDate = todo.TargetDate
            };
        }
    }
}