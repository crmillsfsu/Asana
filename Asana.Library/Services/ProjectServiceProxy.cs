using Asana.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asana.Library.Services
{
    public class ProjectServiceProxy
    {
        private List<Project> _projectList;

        public List<Project> Projects
        {
            get
            {
                return _projectList.Take(100).ToList();
            }
            private set
            {
                if (value != _projectList)
                {
                    _projectList = value;
                }
            }
        }

        private ProjectServiceProxy()
        {
            Projects = new List<Project>
            {
                new Project{Id = 0, Name = "Hello"}
            };
        }

        private static ProjectServiceProxy? instance;

        private int nextKey
        {
            get
            {
                if (Projects.Any())
                {
                    return Projects.Select(p => p.Id).Max() + 1;
                }
                return 1;
            }
        }

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

        public void ToDosInProject(Project? project)
        {
            if (project != null && project.ToDos != null)
              {
                 project.ToDos.ForEach(Console.WriteLine);
              }
        }
        public void AddOrUpdate(Project? project)
        {
            if (project != null && project.Id == 0)
            {
                project.Id = nextKey;
                _projectList.Add(project);
            }
        }

        public Project? GetById(int id)
        {
            return Projects.FirstOrDefault(p => p.Id == id);
        }

        public void DeleteProject(Project? project)
        {
            if (project == null)
            {
                return;
            }
            _projectList.Remove(project);
        }

        public void DisplayProjects()
        {
            Projects.ForEach(Console.WriteLine);
        }
    }
}
