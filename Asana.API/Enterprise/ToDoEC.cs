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
            return FakeDatabase.Current.ToDos.Take(100);
        }

        public ToDo? GetById(int id)
        {
            return GetToDos().FirstOrDefault(t => t.Id == id);
        }

        public ToDo? Delete(int id)
        {
            var toDoToDelete = GetById(id);
            if (toDoToDelete != null)
            {
                FakeDatabase.Current.DeleteToDo(toDoToDelete);
            }
            return toDoToDelete;
        }

        public ToDo? AddOrUpdate(ToDo? toDo)
        {
            FakeDatabase.Current.AddOrUpdateToDo(toDo);
            return toDo;
        }
    }
}
