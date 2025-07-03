using Asana.Library.Models;
using Asana.Library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Maui.ViewModels
{
    public class ProjectsPageViewModel : INotifyPropertyChanged
    {
        private ProjectServiceProxy _projectSvc;

        public ProjectsPageViewModel()
        {
            try
            {
                _projectSvc = ProjectServiceProxy.Current;
                Query = string.Empty;
                System.Diagnostics.Debug.WriteLine("ProjectsPageViewModel created successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ProjectsPageViewModel Error: {ex.Message}");
                Query = "ERROR";
            }
        }

        public ProjectViewModel? SelectedProject { get; set; }

        private string query = string.Empty;
        public string Query
        {
            get { return query; }
            set
            {
                if (query != value)
                {
                    query = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(Projects));
                }
            }
        }

        public ObservableCollection<ProjectViewModel> Projects
        {
            get
            {
                try
                {
                    var allProjects = _projectSvc.Projects;
                    System.Diagnostics.Debug.WriteLine($"Found {allProjects.Count} projects");

                    // Fix the filtering logic
                    var filteredProjects = allProjects.Where(p => 
                        string.IsNullOrWhiteSpace(Query) || 
                        (!string.IsNullOrEmpty(p?.Name) && p.Name.Contains(Query, StringComparison.OrdinalIgnoreCase)) || 
                        (!string.IsNullOrEmpty(p?.Description) && p.Description.Contains(Query, StringComparison.OrdinalIgnoreCase))
                    );

                    var projectViewModels = filteredProjects.Select(p => new ProjectViewModel(p));
                    
                    return new ObservableCollection<ProjectViewModel>(projectViewModels);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Projects Error: {ex.Message}");
                    return new ObservableCollection<ProjectViewModel>();
                }
            }
        }

        public int SelectedProjectId => SelectedProject?.Model?.Id ?? 0;

        public void DeleteProject()
        {
            if (SelectedProject?.Model != null)
            {
                _projectSvc.DeleteProject(SelectedProject.Model);
                NotifyPropertyChanged(nameof(Projects));
            }
        }

        public void RefreshPage()
        {
            NotifyPropertyChanged(nameof(Projects));
        }

        public void HandleSearchClick()
        {
            RefreshPage();
            Query = string.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}