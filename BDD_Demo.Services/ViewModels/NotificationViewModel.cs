namespace BDD_Demo.Services.ViewModels
{
    public class NotificationViewModel
    {
        public long Id { get; set; }
        public bool IsDismissed { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}