using Asana.Library.Models;
using System;

namespace Asana.Library.Services
{
    public class ProjectServiceProxy
    {
        private List<Project> _projectList;

        private ProjectServiceProxy()
        {
            _projectList = new List<Project>
            {
                new Project { Id = 1, Name = "Work Project", Description = "Office tasks", CompletePercent = 25 },
                new Project { Id = 2, Name = "Personal Project", Description = "Home tasks", CompletePercent = 75 }
            };
        }

        private static ProjectServiceProxy? instance;

        private int nextKey => _projectList.Any() ? _projectList.Max(p => p.Id) + 1 : 1;

        public static ProjectServiceProxy Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProjectServiceProxy();
                }
                return instance;
            }
        }

        public List<Project> Projects => _projectList;

        public void AddOrUpdate(Project project)
        {
            if (project.Id == 0)
            {
                project.Id = nextKey;
                _projectList.Add(project);
                Console.WriteLine($"Project '{project.Name}' created successfully!");
            }
            else
            {
                var existingProject = GetById(project.Id);
                if (existingProject != null)
                {
                    existingProject.Name = project.Name;
                    existingProject.Description = project.Description;
                    existingProject.CompletePercent = project.CompletePercent;
                    Console.WriteLine($"Project '{project.Name}' updated successfully!");
                }
            }
        }

        public Project? GetById(int id)
        {
            return _projectList.FirstOrDefault(p => p.Id == id);
        }

        public void DeleteProject(Project? project)
        {
            if (project != null)
            {
                _projectList.Remove(project);
                Console.WriteLine($"Project '{project.Name}' deleted successfully!");
            }
            else
            {
                Console.WriteLine("Project not found!");
            }
        }

        public void DeleteProject(int id)
        {
            var projectToDelete = GetById(id);
            if (projectToDelete != null)
            {
                DeleteProject(projectToDelete);
            }
        }

        public void DisplayProjects()
        {
            if (!_projectList.Any())
            {
                Console.WriteLine("No projects found.");
                return;
            }

            Console.WriteLine("\nProjects:");
            Console.WriteLine("---------------------------------------------------");
            foreach (var project in _projectList)
            {
                Console.WriteLine($"Id: {project.Id}");
                Console.WriteLine($"Name: {project.Name}");
                Console.WriteLine($"Description: {project.Description}");
                Console.WriteLine($"Complete: {project.CompletePercent}%");
                Console.WriteLine("---------------------------------------------------");
            }
        }

        public List<ToDo> GetToDosForProject(int projectId)
        {
            var todos = ToDoServiceProxy.Current.ToDos.Where(t => t.ProjectId == projectId).ToList();
            
            if (!todos.Any())
            {
                Console.WriteLine($"No ToDos found for project ID {projectId}.");
            }
            
            return todos;
        }
    }
}