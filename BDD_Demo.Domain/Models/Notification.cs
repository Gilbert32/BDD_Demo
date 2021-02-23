#nullable disable

namespace BDD_Demo.Domain.Models
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
