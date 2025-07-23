using Api.ToDoApplication.Persistence;
using Asana.API.Database;
using Asana.Library.Models;

namespace Asana.API.Enterprise
{
    public class ToDoEC
    {
        public ToDoEC() { 
            
        }

        public IEnumerable<ToDo> GetToDos()
        {
            return new MsSqlContext().ToDos.Take(100);
        }

        public ToDo? GetById(int id)
        {
            return GetToDos().FirstOrDefault(t => t.Id == id);
        }

        public int Delete(int id)
        {
            if (id > 0)
            {
                return new MsSqlContext().DeleteToDo(id);
            }
            return -1;
        }

        public ToDo? AddOrUpdate(ToDo? toDo)
        {
            new MsSqlContext().AddOrUpdateToDo(toDo);
            return toDo;
        }
    }
}
