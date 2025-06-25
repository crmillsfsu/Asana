using Asana.Library.Models;
using Asana.Library.Services;
using System;
using System.IO.Compression;

namespace Asana
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var toDoSvc = ToDoServiceProxy.Current;
            var projectSvc = ProjectServiceProxy.Current;
            int choiceInt;
            do
            {
                Console.WriteLine("Choose a menu option:");
                Console.WriteLine("1. Create a ToDo");
                Console.WriteLine("2. List all ToDos");
                Console.WriteLine("3. List all outstanding ToDos");
                Console.WriteLine("4. Delete a ToDo");
                Console.WriteLine("5. Update a ToDo");
                Console.WriteLine("6. Create a Project");
                Console.WriteLine("7. List all Projects");
                Console.WriteLine("8. Update a Project");
                Console.WriteLine("9. Delete a Project");
                Console.WriteLine("10. List all ToDos in a Project");
                Console.WriteLine("11. Add a ToDo to a Project");
                Console.WriteLine("12. Exit");

                var choice = Console.ReadLine() ?? "12";

                if (int.TryParse(choice, out choiceInt))
                {
                    switch (choiceInt)
                    {
                        case 1:
                            Console.Write("Name:");
                            var name = Console.ReadLine();
                            Console.Write("Description:");
                            var description = Console.ReadLine();

                            toDoSvc.AddOrUpdate(new ToDo
                            {
                                Name = name,
                                Description = description,
                                IsCompleted = false,
                                Id = 0
                            });
                            break;
                        case 2:
                            toDoSvc.DisplayToDos(true);
                            break;
                        case 3:
                            toDoSvc.DisplayToDos();
                            break;
                        case 4:
                            toDoSvc.DisplayToDos(true);
                            Console.Write("ToDo to Delete: ");
                            var toDoChoice4 = int.Parse(Console.ReadLine() ?? "0");

                            var reference = toDoSvc.GetById(toDoChoice4);
                            toDoSvc.DeleteToDo(reference);
                            break;
                        case 5:
                            toDoSvc.DisplayToDos(true);
                            Console.Write("ToDo to Update: ");
                            var toDoChoice5 = int.Parse(Console.ReadLine() ?? "0");
                            var updateReference = toDoSvc.GetById(toDoChoice5);

                            if (updateReference != null)
                            {
                                Console.Write("Name:");
                                updateReference.Name = Console.ReadLine();
                                Console.Write("Description:");
                                updateReference.Description = Console.ReadLine();
                            }
                            toDoSvc.AddOrUpdate(updateReference);
                            break;
                        case 6:
                            Console.Write("Name:");
                            var projectName = Console.ReadLine();
                            Console.Write("Description:");
                            var projectDescription = Console.ReadLine();
                            projectSvc.AddOrUpdate(new Project
                            {
                                Name = projectName,
                                Description = projectDescription,
                                CompletePercent = 0,
                                Id = 0
                            });
                            break;
                        case 7:
                            projectSvc.DisplayProjects();
                            break;
                        case 8:
                            Console.WriteLine("Choose a project to update: ");
                            projectSvc.DisplayProjects();
                            var toDoChoice8 = int.Parse(Console.ReadLine() ?? "0");
                            var reference8 = projectSvc.GetById(toDoChoice8);
                            Console.Write("Name:");
                            reference8.Name = Console.ReadLine();
                            Console.Write("Description:");
                            reference8.Description = Console.ReadLine();
                            break;
                        case 9:
                            Console.WriteLine("Choose a project to delete: ");
                            projectSvc.DisplayProjects();
                            var toDoChoice9 = int.Parse(Console.ReadLine() ?? "0");
                            var reference9 = projectSvc.GetById(toDoChoice9);
                            projectSvc.DeleteProject(reference9);
                            break;
                        case 10:
                            Console.Write("Choose a project to display: ");
                            projectSvc.DisplayProjects();
                            var toDoChoice10 = int.Parse(Console.ReadLine() ?? "0");
                            var reference10 = projectSvc.GetById(toDoChoice10);
                            if (reference10 != null && reference10.ToDos != null)
                            {
                                projectSvc.ToDosInProject(reference10);
                            }
                            break;
                        case 11:
                        //  need to implement for if they want to add an existing to do
                            Console.Write("Project to add ToDo to: ");
                            projectSvc.DisplayProjects();
                            var toDoChoice11 = int.Parse(Console.ReadLine() ?? "0");
                            var reference11 = projectSvc.GetById(toDoChoice11);
                            
                            //Console.Write("Would you like to add an existing ToDo [1] or create a new ToDo [2]");
                            //var typeofToDo = int.Parse(Console.ReadLine() ?? "2");
                            //if (typeofToDo == 1)
                           // {
                            //    if (reference11 != null)
                             //   {
                             //       Console.Write("Which ToDo would you like to add: ");
                              //      toDoSvc.DisplayToDos(true);
                                    
                              //  }
                            //}

                            if (reference11 != null)
                            {
                                Console.Write("Name:");
                                var name11 = Console.ReadLine();
                                Console.Write("Description:");
                                var description11 = Console.ReadLine();
                                var newToDo = new ToDo
                                {
                                    Name = name11,
                                    Description = description11,
                                    IsCompleted = false,
                                    Id = 0
                                };
                                reference11.ToDos.Add(newToDo);
                            }
                            break;
                        case 12:
                            break;
                        default:
                            Console.WriteLine("ERROR: Unknown menu selection");
                            break;
                    }
                } else
                {
                    Console.WriteLine($"ERROR: {choice} is not a valid menu selection");
                }

            } while (choiceInt != 12);

        }

    }
}