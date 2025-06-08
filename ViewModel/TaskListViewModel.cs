namespace MyEmptyMvcApp.Models
{
    public class TaskListViewModel
    {
        public List<TodoItem> Tasks { get; set; } = new();
        public TodoItem NewItem { get; set; } = new(); // For the form
    }
}

