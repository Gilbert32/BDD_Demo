using System;

namespace BDD_Demo.Services.ViewModels
{
    public class TodoViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? TargetDate { get; set; }
        public bool IsComplete { get; set; }
    }
}