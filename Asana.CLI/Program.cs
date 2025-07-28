using Asana.API.Database;
using Asana.Library.Models;
using System;

class Program
{
    static void Main(string[] args)
    {

        // Added the sql connection and then delete it 
        var context = new SqliteContext();

        // Add a new ToDo
        var newToDo = new ToDo
        {
            Name = "Test Task",
            Description = "Testing SQLite insert",
            IsCompleted = false,
            Priority = 1
        };

        context.AddOrUpdateToDo(newToDo);
        Console.WriteLine($"Added ToDo with ID: {newToDo.Id}");

        // Get all ToDos
        var allToDos = context.ToDos;
        Console.WriteLine("\nAll ToDos:");
        foreach (var todo in allToDos)
        {
            Console.WriteLine($"ID: {todo.Id}, Name: {todo.Name}, Completed: {todo.IsCompleted}");
        }

        // Delete test ToDo
        context.DeleteToDo(newToDo.Id);
        Console.WriteLine($"\nDeleted ToDo with ID: {newToDo.Id}");
    }
}
