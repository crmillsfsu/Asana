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
    public class ToDoServiceProxy
    {
        private List<ToDo> _toDoList;
        public List<ToDo> ToDos { 
            get
            {
                return _toDoList.ToList();
            }

            private set {
                if (value != _toDoList)
                {
                    _toDoList = value;
                }
            }
        }

        private ToDoServiceProxy()
        {
            /*ToDos = new List<ToDo>
            {
                new ToDo{Id = 1, Name = "Task 1", Description = "My Task 1", IsCompleted=true},
                new ToDo{Id = 2, Name = "Task 2", Description = "My Task 2", IsCompleted=false },
                new ToDo{Id = 3, Name = "Task 3", Description = "My Task 3", IsCompleted=true },
                new ToDo{Id = 4, Name = "Task 4", Description = "My Task 4", IsCompleted=false },
                new ToDo{Id = 5, Name = "Task 5", Description = "My Task 5", IsCompleted=true }
            };*/

            var todoData = new WebRequestHandler().Get("/ToDo").Result;
            ToDos = JsonConvert.DeserializeObject<List<ToDo>>(todoData) ?? new List<ToDo>();
        }

        private static ToDoServiceProxy? instance;

        public static ToDoServiceProxy Current
        {
            get
            {
                if(instance == null)
                {
                    instance = new ToDoServiceProxy();
                }

                return instance;
            }
        }
        public ToDo? AddOrUpdate(ToDo? toDo)
        {
            if(toDo == null)
            {
                return toDo;
            }
            var isNewToDo = toDo.Id == 0;
            var todoData = new WebRequestHandler().Post("/ToDo", toDo).Result;
            var newToDo = JsonConvert.DeserializeObject<ToDo>(todoData);

            if (newToDo != null)
            {
                if(!isNewToDo)
                {
                    var existingToDo = _toDoList.FirstOrDefault(t => t.Id == newToDo.Id);
                    if(existingToDo != null)
                    {
                        var index = _toDoList.IndexOf(existingToDo);
                        _toDoList.RemoveAt(index);
                        _toDoList.Insert(index, newToDo);
                    }

                } else
                {
                    _toDoList.Add(newToDo);
                }

            }

            return toDo;
        }

        public void DisplayToDos(bool isShowCompleted = false)
        {
            if (isShowCompleted)
            {
                ToDos.ForEach(Console.WriteLine);
            }
            else
            {
                ToDos.Where(t => (t != null) && !(t?.IsCompleted ?? false))
                                .ToList()
                                .ForEach(Console.WriteLine);
            }
        }

        public ToDo? GetById(int id)
        {
            return ToDos.FirstOrDefault(t => t.Id == id);
        }

        public void DeleteToDo(int id)
        {
            if (id == 0)
            {
                return;
            }
            var todoData = new WebRequestHandler().Delete($"/ToDo/{id}").Result;
            var toDoToDelete = JsonConvert.DeserializeObject<ToDo>(todoData);
            if(toDoToDelete != null)
            {
                var localToDo = _toDoList.FirstOrDefault(t => t.Id == toDoToDelete.Id);
                if(localToDo != null)
                {
                    _toDoList.Remove(localToDo);
                }
            }

        }

    }
}
