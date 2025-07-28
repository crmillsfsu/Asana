using Asana.API.Database;
using Asana.Library.Models;

namespace Asana.API.Enterprise
{
    public class ToDoEC
    {
        private readonly SqliteContext _context;

        public ToDoEC()
        {
            _context = new SqliteContext();
        }

        public IEnumerable<ToDo> GetToDos()
        {
            return _context.ToDos.Take(100);
        }

        public ToDo? GetById(int id)
        {
            return GetToDos().FirstOrDefault(t => t.Id == id);
        }

        public int Delete(int id)
        {
            if (id > 0)
            {
                return _context.DeleteToDo(id);
            }
            return -1;
        }

        public ToDo? AddOrUpdate(ToDo? toDo)
        {
            return _context.AddOrUpdateToDo(toDo);
        }
    }
}
