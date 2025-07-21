using Asana.Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ToDoApplication.Persistence
{
    public class Filebase
    {
        private string _root;
        private string _toDoRoot;
        private static Filebase _instance;


        public static Filebase Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        private Filebase()
        {
            _root = @"C:\temp";
            _toDoRoot = $"{_root}\\ToDos";
        }

        public int LastKey
        {
            get
            {
                if (ToDos.Any())
                {
                    return ToDos.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        public ToDo AddOrUpdate(ToDo toDo)
        {
            //set up a new Id if one doesn't already exist
            if(toDo.Id <= 0)
            {
                toDo.Id = LastKey + 1;
            }

            //go to the right place
            string path = $"{_toDoRoot}\\{toDo.Id}.json";
            

            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(toDo));

            //return the item, which now has an id
            return toDo;
        }
        
        public List<ToDo> ToDos
        {
            get
            {
                var root = new DirectoryInfo(_toDoRoot);
                var _toDos = new List<ToDo>();
                foreach(var patientFile in root.GetFiles())
                {
                    var toDo = JsonConvert
                        .DeserializeObject<ToDo>
                        (File.ReadAllText(patientFile.FullName));
                    if(toDo != null)
                    {
                        _toDos.Add(toDo);
                    }

                }
                return _toDos;
            }
        }


        public bool Delete(string type, string id)
        {
            //TODO: refer to AddOrUpdate for an idea of how you can implement this.
            return true;
        }
    }


   
}