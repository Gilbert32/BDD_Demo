using System;
using System.Linq;
using System.Threading.Tasks;
using BDD_Demo.Domain.Contexts;
using BDD_Demo.Domain.Models;
using BDD_Demo.Services.Requests;
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

        public async Task<object> GetUser(long id)
        {
            // Check if the suer exists, and otherwise throw error
            CheckUserExists(id);
            // Get User
            var user = await _context.Users.Include(x => x.Todos).ThenInclude(x => x.Notifications)
                .FirstOrDefaultAsync(x => x.Id == id);
            // Update his notifications
            UpdateNotificationsForUser(user.Id);
            return new UserViewModel()
            {
                Name = user.Username,
                Email = user.Email,
                Id = user.Id,
                Notifications = _context.Notifications
                    .Where(x => user.Todos.Select(t => t.Id).ToList().Contains(x.TodoId.Value) &&
                                x.IsDismissed == false)
                    .ToList()
                    .Select(x =>
                        new NotificationViewModel()
                            {Title = x.Title, Message = x.Text, IsDismissed = x.IsDismissed ?? false, Id = x.Id}
                    ).ToList()
            };
        }

        public void CheckUserExists(long userId)
        {
            if (!_context.Users.Any(x => x.Id == userId))
                throw new Exception("User Not Found");
        }

        public void CheckNotificationExists(long notificationId)
        {
            if (!_context.Notifications.Any(x => x.Id == notificationId))
                throw new Exception("Notification Not Found");
        }

        public void UpdateNotificationsForUser(long userId)
        {
            CheckUserExists(userId);
            var todos = _context.Users
                .Include(x => x.Todos)
                .First(x => x.Id == userId).Todos;
            todos.Where(x => x.TargetDate <= DateTime.Now && x.IsComplete == false).ToList()
                .ForEach(x =>
                {
                    if (x.Notifications.Count == 0)
                        _context.Notifications.Add(new Notification()
                        {
                            TodoId = x.Id,
                            IsDismissed = false,
                            Title = "TODO Target Date Passed",
                            Text = $"The TODO with title {x.Title} has not been complete on time"
                        });
                });
            _context.SaveChanges();
        }

        public async Task<NotificationViewModel> DismissNotification(DismissNotificationRequest request)
        {
            CheckNotificationExists(request.Id);
            var notification = _context.Notifications.First(x => x.Id == request.Id);
            notification.IsDismissed = true;
            await _context.SaveChangesAsync();
            return new NotificationViewModel()
            {
                Id = request.Id,
                Title = notification.Title,
                Message = notification.Text,
                IsDismissed = notification.IsDismissed.Value
            };
        }

        public async Task<UserViewModel> AddUser(AddUserRequest request)
        {
            var user = new User()
            {
                Username = request.UserName,
                Email = request.Email
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Username
            };
        }
    }
}