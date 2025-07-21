using Asana.Library.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace Asana.API.Database
{
    public class MsSqlContext
    {
        private string _connectionString;

        public MsSqlContext()
        {
            _connectionString = $"Server=CMILLS;Database=ASANA;Trusted_Connection=True;TrustServerCertificate=True";
        }

        private List<ToDo> _toDos;
        public List<ToDo> ToDos
        {
            get
            {
                _toDos = new List<ToDo>();
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    using (var sqlCmd = sqlConnection.CreateCommand())
                    {
                        sqlCmd.CommandText = "select * from [ToDo].[View]";
                        sqlCmd.CommandType = System.Data.CommandType.Text;

                        sqlConnection.Open();
                        var reader = sqlCmd.ExecuteReader();
                        

                        while (reader.Read())
                        {
                            ToDo toDo = new ToDo
                            {
                                Id = int.Parse(reader["ToDoId"].ToString()),
                                Name = reader["ToDoName"].ToString(),
                                Description = reader["ToDoDescription"].ToString(),
                                IsCompleted = bool.Parse(reader["IS_Completed"].ToString()),
                                Priority = int.Parse(reader["ToDoPriority"].ToString())
                            };
                            _toDos.Add(toDo);
                        }

                        sqlConnection.Close();

                        sqlCmd.Dispose();
                        sqlConnection.Dispose();
                        return _toDos;
                    }
                }
  
            }
        }

        public ToDo AddOrUpdateToDo()
        {

        }
    }
}
