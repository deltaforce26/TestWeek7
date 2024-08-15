namespace TestWeek7.Models
{
    public class Todos
    {
        public int Id { get; set; }
        public string? Todo { get; set; }
        public bool Completed { get; set; }
        public int UserId { get; set; }
        
    }

    public class ListTodo
    {
        public List<Todos> todos { get; set; }
    }
}
