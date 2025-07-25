using Asana.Library.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data.SqlTypes;

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

        public ToDo? AddOrUpdateToDo(ToDo? toDo)
        {
            if (toDo == null) return toDo;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter? idParameter = null;
                    //set up the command
                    if (toDo.Id <= 0)
                    {
                        //Insert command
                        cmd.CommandText = "[ToDo].[Insert]";
                        idParameter = new SqlParameter("@id", toDo.Id) { Direction = System.Data.ParameterDirection.Output };
                        cmd.Parameters.Add(idParameter);

                    }
                    else
                    {
                        //update command
                        cmd.CommandText = "[ToDo].[Update]";
                        cmd.Parameters.Add(new SqlParameter("@id", toDo.Id));
                    }

                    cmd.Parameters.Add(new SqlParameter("@name", toDo.Name));
                    cmd.Parameters.Add(new SqlParameter("@description", toDo.Description));
                    cmd.Parameters.Add(new SqlParameter("@isCompleted", toDo.IsCompleted));
                    cmd.Parameters.Add(new SqlParameter("@priority", toDo.Priority));

                    connection.Open();
                    var result = cmd.ExecuteScalar();
                    if (idParameter != null)
                    {
                        toDo.Id = int.Parse(idParameter.Value?.ToString() ?? "0");
                    }
                }
            }

            return toDo;
        }

        public int DeleteToDo(int toDoId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = connection.CreateCommand())
                {
                    //update command
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[ToDo].[Delete]";
                    cmd.Parameters.Add(new SqlParameter("@id", toDoId));

                    connection.Open();
                    var result = cmd.ExecuteScalar();
                }
            }

            return toDoId;
        }
    }
}
