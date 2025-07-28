using Asana.Library.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace Asana.API.Database
{
    public class SqliteContext
    {
        private string _connectionString;

        public SqliteContext()
        {
            _connectionString = "Data Source=asana.db";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS ToDos (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Description TEXT,
                        IsCompleted INTEGER NOT NULL,
                        Priority INTEGER NOT NULL
                    );";
                command.ExecuteNonQuery();
            }
        }

        public List<ToDo> ToDos
        {
            get
            {
                var toDos = new List<ToDo>();

                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM ToDos";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var toDo = new ToDo
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                IsCompleted = reader.GetInt32(reader.GetOrdinal("IsCompleted")) == 1,
                                Priority = reader.GetInt32(reader.GetOrdinal("Priority"))
                            };
                            toDos.Add(toDo);
                        }
                    }
                }

                return toDos;
            }
        }

        public ToDo? AddOrUpdateToDo(ToDo? toDo)
        {
            if (toDo == null) return null;

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();

                if (toDo.Id <= 0)
                {
                    command.CommandText = @"
                        INSERT INTO ToDos (Name, Description, IsCompleted, Priority)
                        VALUES (@name, @description, @isCompleted, @priority);
                        SELECT last_insert_rowid();";
                }
                else
                {
                    command.CommandText = @"
                        UPDATE ToDos
                        SET Name = @name,
                            Description = @description,
                            IsCompleted = @isCompleted,
                            Priority = @priority
                        WHERE Id = @id;";
                    command.Parameters.AddWithValue("@id", toDo.Id);
                }

                command.Parameters.AddWithValue("@name", toDo.Name);
                command.Parameters.AddWithValue("@description", toDo.Description);
                command.Parameters.AddWithValue("@isCompleted", toDo.IsCompleted);
                command.Parameters.AddWithValue("@priority", toDo.Priority);

                if (toDo.Id <= 0)
                {
                    var result = command.ExecuteScalar();
                    toDo.Id = Convert.ToInt32(result);
                }
                else
                {
                    command.ExecuteNonQuery();
                }
            }

            return toDo;
        }

        public int DeleteToDo(int toDoId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM ToDos WHERE Id = @id";
                command.Parameters.AddWithValue("@id", toDoId);

                command.ExecuteNonQuery();
            }

            return toDoId;
        }
    }
}
