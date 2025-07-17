using Asana.Library.Models;
using Asana.Maui.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Library.Services
{
    public class ProjectServiceProxy
    {
        private List<Project> projects;
        public List<Project> Projects
        {
            get
            {
                return projects;
            }
        }
        private ProjectServiceProxy()
        {
            /*projects = new List<Project>
            {
                new Project{Id = 1, Name = "Project 1"},
                new Project{Id = 2, Name = "Project 2"},
                new Project{Id = 3, Name = "Project 3"}
            }; */
            var projectData = new WebRequestHandler().Get("/Project/Expand").Result;
            projects = JsonConvert.DeserializeObject<List<Project>>(projectData) ?? new List<Project>();
        }
        private static object _lock = new object();
        private static ProjectServiceProxy? instance;
        public static ProjectServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new ProjectServiceProxy();
                    }
                }

                return instance;
            }
        }

        public Project? AddOrUpdate(Project? project)
        {
            if (project == null)
            {
                return project;
            }
            var isNewProject = project.Id == 0;
            var projectData = new WebRequestHandler().Post("/Project", project).Result;
            var newProject = JsonConvert.DeserializeObject<Project>(projectData);

            if (newProject != null)
            {
                if (!isNewProject)
                {
                    var existingProject = projects.FirstOrDefault(p => p.Id == newProject.Id);
                    if (existingProject != null)
                    {
                        var index = projects.IndexOf(existingProject);
                        projects.RemoveAt(index);
                        projects.Insert(index, newProject);
                    }

                }
                else
                {
                    projects.Add(newProject);
                }

            }

            return project;
        }

        public void DisplayProjects()
        {
            Projects.ForEach(Console.WriteLine);
        }

        public Project? GetById(int id)
        {
            return Projects.FirstOrDefault(p => p.Id == id);
        }

        public void DeleteProject(int id)
        {
            if (id == 0)
            {
                return;
            }
            var projectData = new WebRequestHandler().Delete($"/Project/{id}").Result;
            var projectToDelete = JsonConvert.DeserializeObject<Project>(projectData);
            if (projectToDelete != null)
            {
                var localProject = projects.FirstOrDefault(p => p.Id == projectToDelete.Id);
                if (localProject != null)
                {
                    projects.Remove(localProject);
                }
            }

        }

        public double ProjectPercentCompleted(Project? project)
        {
            if (project == null || project.ToDoList == null)
            {
                return 0;
            }
            var totalTodos = 0;
            var completedTodos = 0;
            foreach (ToDo? t in project.ToDoList)
            {
                if (t != null)
                {
                    totalTodos++;
                    if (t.IsCompleted == true)
                    {
                        completedTodos++;
                    }
                }

            }
            return project.PercentCompleted ?? 0;
        }

    }
}
