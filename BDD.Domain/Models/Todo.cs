using System;
using System.Collections.Generic;

#nullable disable

namespace BDD.Domain.Models
{
    public partial class Todo
    {
        public Todo()
        {
            Notifications = new HashSet<Notification>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? TargetDate { get; set; }
        public long UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
