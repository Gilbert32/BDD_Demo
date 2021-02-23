using System.Collections.Generic;

#nullable disable

namespace BDD_Demo.Domain.Models
{
    public partial class User
    {
        public User()
        {
            Todos = new HashSet<Todo>();
        }

        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Preferences { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Todo> Todos { get; set; }
    }
}
