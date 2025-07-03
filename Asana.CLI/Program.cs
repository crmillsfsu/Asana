using Asana.Library.Models;
using Asana.Library.Services;
using System;

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
                Console.WriteLine("\nChoose a menu option:");
                Console.WriteLine("1. Create a ToDo");
                Console.WriteLine("2. Delete a ToDo");
                Console.WriteLine("3. Update a ToDo");
                Console.WriteLine("4. List all ToDos");
                Console.WriteLine("5. Create a Project");
                Console.WriteLine("6. Delete a Project");
                Console.WriteLine("7. Update a Project");
                Console.WriteLine("8. List all Projects");
                Console.WriteLine("9. List all ToDos in a Given Project");
                Console.WriteLine("0. Exit");

                var choice = Console.ReadLine() ?? "0";

                if (int.TryParse(choice, out choiceInt))
                {
                    switch (choiceInt)
                    {
                        case 1: // Create ToDo
                            Console.Write("Name: ");
                            var name = Console.ReadLine();
                            Console.Write("Description: ");
                            var description = Console.ReadLine();
                            Console.Write("Priority (number): ");
                            int.TryParse(Console.ReadLine(), out int priority);
                            Console.Write("Project Id (or leave blank): ");
                            int.TryParse(Console.ReadLine(), out int projectId);
                            toDoSvc.AddOrUpdate(new ToDo
                            {
                                Name = name,
                                Description = description,
                                Priority = priority,
                                IsCompleted = false,
                                ProjectId = projectId > 0 ? projectId : null,
                                Id = 0
                            });
                            break;
                        case 2: // Delete ToDo
                            toDoSvc.DisplayToDos(true);
                            Console.Write("ToDo Id to Delete: ");
                            int.TryParse(Console.ReadLine(), out int toDoIdDel);
                            var toDoDel = toDoSvc.GetById(toDoIdDel);
                            toDoSvc.DeleteToDo(toDoDel);
                            break;
                        case 3: // Update ToDo
                            toDoSvc.DisplayToDos(true);
                            Console.Write("ToDo Id to Update: ");
                            int.TryParse(Console.ReadLine(), out int toDoIdUpd);
                            var toDoUpd = toDoSvc.GetById(toDoIdUpd);
                            if (toDoUpd != null)
                            {
                                Console.Write("New Name: ");
                                toDoUpd.Name = Console.ReadLine();
                                Console.Write("New Description: ");
                                toDoUpd.Description = Console.ReadLine();
                                Console.Write("New Priority (number): ");
                                int.TryParse(Console.ReadLine(), out int newPriority);
                                toDoUpd.Priority = newPriority;
                                Console.Write("Is Completed? (y/n): ");
                                var completed = Console.ReadLine();
                                toDoUpd.IsCompleted = completed?.ToLower() == "y";
                                Console.Write("Project Id (or leave blank): ");
                                int.TryParse(Console.ReadLine(), out int newProjId);
                                toDoUpd.ProjectId = newProjId > 0 ? newProjId : null;
                                toDoSvc.AddOrUpdate(toDoUpd);
                            }
                            break;
                        case 4: // List all ToDos
                            toDoSvc.DisplayToDos(true);
                            break;
                        case 5: // Create Project
                            Console.Write("Project Name: ");
                            var projName = Console.ReadLine();
                            Console.Write("Project Description: ");
                            var projDesc = Console.ReadLine();
                            projectSvc.AddOrUpdate(new Project
                            {
                                Name = projName,
                                Description = projDesc,
                                CompletePercent = 0,
                                Id = 0
                            });
                            break;
                        case 6: // Delete Project
                            projectSvc.DisplayProjects();
                            Console.Write("Project Id to Delete: ");
                            int.TryParse(Console.ReadLine(), out int projIdDel);
                            var projDel = projectSvc.GetById(projIdDel);
                            projectSvc.DeleteProject(projDel);
                            break;
                        case 7: // Update Project
                            projectSvc.DisplayProjects();
                            Console.Write("Project Id to Update: ");
                            int.TryParse(Console.ReadLine(), out int projIdUpd);
                            var projUpd = projectSvc.GetById(projIdUpd);
                            if (projUpd != null)
                            {
                                Console.Write("New Name: ");
                                projUpd.Name = Console.ReadLine();
                                Console.Write("New Description: ");
                                projUpd.Description = Console.ReadLine();
                                // Optionally update CompletePercent here
                                projectSvc.AddOrUpdate(projUpd);
                            }
                            break;
                        case 8: // List all Projects
                            projectSvc.DisplayProjects();
                            break;
                        case 9: // List all ToDos in a Given Project
                            projectSvc.DisplayProjects();
                            Console.Write("Project Id: ");
                            int.TryParse(Console.ReadLine(), out int projIdList);
                            var todos = projectSvc.GetToDosForProject(projIdList);
                            todos.ForEach(Console.WriteLine);
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("ERROR: Unknown menu selection");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"ERROR: {choice} is not a valid menu selection");
                }

            } while (choiceInt != 0);
        }
    }
}