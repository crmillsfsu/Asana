using System;

namespace Asana.Library.Models
{
    public class Project
    {
        public Project()
        {
            Id = 0;
            ToDos = new List<ToDo>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double CompletePercent { get; set; }
        public List<ToDo> ToDos { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} - {Description} ({CompletePercent}% complete)";
        }
    }
}
