using Asana.API.Database;
using Asana.API.Enterprise;
using Asana.Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asana.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ToDo> Get()
        {
            return new ToDoEC().GetToDos();
        }

        [HttpGet("{id}")]
        public ToDo? GetById(int id)
        {
            return new ToDoEC().GetById(id);
        }

        [HttpDelete("{id}")]
        public ToDo? Delete(int id)
        {
            var ec = new ToDoEC();
            var toDo = ec.GetById(id); // get it before it's deleted
            if (toDo == null)
                return null;

            ec.Delete(id);
            return toDo;
        }

        [HttpPost]
        public ToDo? AddOrUpdate([FromBody] ToDo? toDo)
        {
            return new ToDoEC().AddOrUpdate(toDo);
        }
    }
}
