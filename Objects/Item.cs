namespace TodoApp.Objects
{
    public class Item
    {
        public bool IsCompleted { get; set; } = false;
        public required string Description { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
