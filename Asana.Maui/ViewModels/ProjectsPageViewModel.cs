using Asana.Library.Models;
using Asana.Library.Services;
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
            Projects = ProjectServiceProxy.Current.Projects
                .Select(p => new ProjectViewModel(p))
                .ToList();
        }
    }
}
