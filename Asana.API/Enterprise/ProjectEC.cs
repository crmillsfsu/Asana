using Asana.API.Database;
using Asana.Library.Models;

namespace Asana.API.Enterprise
{
    public class ProjectEC
    {
        public IEnumerable<Project>? Get(bool Expand = false)
        {
            //return FakeDatabase.Current.Projects.Take(100);
            return FakeDatabase.Current.GetProjects(Expand)?.Take(100);
        }

        public Project? GetById(int id)
        {
            return FakeDatabase.Current.GetProjects(true)?.FirstOrDefault(p => p.Id == id);
        }

        public Project? AddOrUpdate(Project? project)
        {
            if(project == null)
            {
                return project;
            }

            FakeDatabase.Current.AddOrUpdateProject(project);
            return project;
        }

        public Project? Delete(int id)
        {
            var projectToDelete = GetById(id);
            if (projectToDelete != null)
            {
                FakeDatabase.Current.DeleteProject(projectToDelete);
            }
            return projectToDelete;
        }
    }
}
