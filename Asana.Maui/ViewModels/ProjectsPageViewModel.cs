using Asana.Library.Models;
using Asana.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Asana.Maui.ViewModels
{
    public class ProjectsPageViewModel
    {
        public List<ProjectViewModel> Projects { get; set; }

        public ProjectViewModel? SelectedProject { get; set; }

        public ProjectsPageViewModel()
        {
            Projects = ProjectServiceProxy.Current.Projects
                .Select(p => new ProjectViewModel(p))
                .ToList();
        }

        public ProjectsPageViewModel(int id)
        {
            Model = ProjectServiceProxy.Current.GetById(id) ?? new Project();

            DeleteCommand = new Command(DoDelete);
        }

        public void DoDelete()
        {

            ProjectServiceProxy.Current.DeleteProject(Model?.Id ?? 0);
        }

        public Project? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }


        public void AddOrUpdateProject()
        {
            ProjectServiceProxy.Current.AddOrUpdate(Model);
        }

        // public double PercentCompleted()
        // {
        //     return ProjectServiceProxy.Current.ProjectPercentCompleted();
        // }


    }
}
