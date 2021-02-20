using System;

namespace BDD_Demo.Services.Requests
{
    public class AddTodoRequest
    {
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime? TargetDate { get; set; }
    }
}