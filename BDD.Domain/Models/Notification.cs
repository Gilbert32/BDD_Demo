using System;
using System.Collections.Generic;

#nullable disable

namespace BDD.Domain.Models
{
    public partial class Notification
    {
        public long Id { get; set; }
        public long? TodoId { get; set; }
        public bool? IsDismissed { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public virtual Todo Todo { get; set; }
    }
}
