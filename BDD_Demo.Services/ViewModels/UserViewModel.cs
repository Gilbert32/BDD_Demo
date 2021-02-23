using System.Collections.Generic;

namespace BDD_Demo.Services.ViewModels
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<NotificationViewModel> Notifications { get; set; }
    }
}