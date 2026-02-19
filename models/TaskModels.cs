namespace TaskApi.Models
{
    public enum TaskStatus
    {
        New,
        Earring,
        Complete,
        Canceled,
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
    }
}
