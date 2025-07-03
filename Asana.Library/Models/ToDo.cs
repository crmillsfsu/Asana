using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Library.Models
{
    public class ToDo
    {
        public ToDo()
        {
            Id = 0;
            IsCompleted = false;
        }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Priority { get; set; }
        public bool? IsCompleted { get; set; }
        public int? ProjectId { get; set; }
        public int Id { get; set; }
        public DateTime? DueDate { get; set; }

        public override string ToString()
        {
            string projectInfo = ProjectId.HasValue ? $" (Project Id: {ProjectId})" : "";
            return $"[{Id}] {Name} - {Description}{projectInfo}";
        }
    }
}
