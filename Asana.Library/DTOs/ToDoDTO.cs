using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Library.DTOs
{
    public class ToDoDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Priority { get; set; }
        public bool? IsCompleted { get; set; }

        public int? ProjectId { get; set; }

        public int Id { get; set; }

        public ToDoDTO() { }
        public ToDoDTO(ToDoDTO td) { 
            Id = td.Id;
            Name = td.Name;
            Description = td.Description;
            Priority = td.Priority;
            IsCompleted = td.IsCompleted;
            ProjectId = td.ProjectId;
        }
    }
}
