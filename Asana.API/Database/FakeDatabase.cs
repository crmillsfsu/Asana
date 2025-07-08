using Asana.Library.Models;

namespace Asana.API.Database
{
    public static class FakeDatabase
    {
        public static int NextKey
        {
            get
            {
                if (toDos.Any())
                {
                    return toDos.Select(t => t.Id).Max() + 1;
                }
                return 1;
            }
        }

        private static List<ToDo> toDos = new List<ToDo>
                {
                    new ToDo{Id = 1, Name = "Task 1", Description = "My Task 1", IsCompleted=true},
                    new ToDo{Id = 2, Name = "Task 2", Description = "My Task 2", IsCompleted=false },
                    new ToDo{Id = 3, Name = "Task 3", Description = "My Task 3", IsCompleted=true },
                    new ToDo{Id = 4, Name = "Task 4", Description = "My Task 4", IsCompleted=false },
                    new ToDo{Id = 5, Name = "Task 5", Description = "My Task 5", IsCompleted=true }
                };
        public static List<ToDo> ToDos { 
            get { return toDos; }
        }


    }
}
