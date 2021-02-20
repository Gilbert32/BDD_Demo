using System;
using System.Linq;
using System.Threading.Tasks;
using BDD.Domain.Contexts;
using BDD_Demo.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BDD_Demo.Services
{
    public class UserService
    {
        private BddDemoContext _context;

        public UserService(BddDemoContext context)
        {
            _context = context;
        }

        public async Task<object> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user != null ? new UserViewModel() {Name = user.Username, Email = user.Email} : throw new Exception("User Not found");
        }
    }
}