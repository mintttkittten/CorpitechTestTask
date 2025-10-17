namespace TestTask.Models
{
    public enum WorkerRole
    {
        Developer,
        Tester,
        Analyst,
        Manager
    }

    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string? Lastname { get; set; }
        public WorkerRole Role { get; set; }
        public decimal Salary { get; set; }
    }
}
