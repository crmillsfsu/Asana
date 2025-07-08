using Asana.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Maui.ViewModels
{
    public class ProjectsPageViewModel
    {
        public List<ProjectViewModel> Projects { get; set; }

        public ProjectViewModel? SelectedProject {get; set;}

        public ProjectsPageViewModel()
        {
            Projects = new List<ProjectViewModel>
            {
                new ProjectViewModel{ Model = new Project{Id = 1, Name = "Project 1"}},
                new ProjectViewModel{ Model = new Project{Id = 2, Name = "Project 2"}},
                new ProjectViewModel{ Model = new Project{Id = 3, Name = "Project 3"}}
            };
        }
    }
}
