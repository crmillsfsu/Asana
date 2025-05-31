using Asana.Library.Models;
using System;

namespace Asana
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var toDos = new List<ToDo>();
            var projects = new List<Project>();
            int choiceInt;
            var itemCount = 0;
            var projectCount = 0;
            var toDoChoice = 0;
            do
            {
                Console.WriteLine("Choose a menu option:");
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
                        case 1:
                            Console.Write("Name: ");
                            var name = Console.ReadLine();
                            Console.Write("Description: ");
                            var description = Console.ReadLine();

                            projects.ForEach(Console.WriteLine);
                            Console.Write("Enter Project ID (or leave blank for none): ");
                            var projInput = Console.ReadLine();
                            int.TryParse(projInput, out var projectId);

                            toDos.Add(new ToDo { Name = name,
                                Description = description,
                                IsCompleted = false,
                                Id = ++itemCount,
                                ProjectId = projectId});
                            break;
                        case 2:
                            
                            toDos.ForEach(Console.WriteLine);
                            Console.Write("ToDo to Delete: ");
                            toDoChoice = int.Parse(Console.ReadLine() ?? "0");

                            var reference = toDos.FirstOrDefault(t => t.Id == toDoChoice);
                            if (reference != null)
                            {
                                toDos.Remove(reference);
                            }
                            
                            break;
                        case 3:
                            
                            toDos.ForEach(Console.WriteLine);
                            Console.Write("ToDo to Update: ");
                            toDoChoice = int.Parse(Console.ReadLine() ?? "0");
                            var updateReference = toDos.FirstOrDefault(t => t.Id == toDoChoice);

                            if(updateReference != null)
                            {
                                Console.Write("Name: ");
                                updateReference.Name = Console.ReadLine();
                                Console.Write("Description: ");
                                updateReference.Description = Console.ReadLine();
                            }

                            break;
                        case 4:
                            toDos.ForEach(Console.WriteLine);
                            break;
                        case 5:
                            Console.Write("Project Name: ");
                            var projName = Console.ReadLine();
                            Console.Write("Project Description: ");
                            var projDesc = Console.ReadLine();

                            projects.Add(new Project
                            {
                                Id = ++projectCount,
                                Name = projName,
                                Description = projDesc
                            });
                            break;
                        case 6:
                            projects.ForEach(Console.WriteLine);
                            Console.Write("Project to Delete: ");
                            if (int.TryParse(Console.ReadLine(), out var projDeleteId))
                            {
                                if (projDeleteId == 0)
                                {
                                    Console.WriteLine("Cannot delete 'unassigned' (ID 0).");
                                    break;
                                }
                                var projectRef = projects.FirstOrDefault(p => p.Id == projDeleteId);
                                if (projectRef != null)
                                {
                                    projects.Remove(projectRef);
                                    toDos.RemoveAll(t => t.ProjectId == projDeleteId);
                                    Console.WriteLine("Project and its associated ToDos deleted.");
                                }
                                else
                                {
                                    Console.WriteLine("No project found with that ID.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input.");
                            }
                            break;

                        case 7:
                            projects.ForEach(Console.WriteLine);
                            Console.Write("Project to Update: ");
                            if (int.TryParse(Console.ReadLine(), out var projUpdateId))
                            {
                                var projRef = projects.FirstOrDefault(p => p.Id == projUpdateId);
                                if (projRef != null)
                                {
                                    Console.Write("New Name: ");
                                    projRef.Name = Console.ReadLine();
                                    Console.Write("New Description: ");
                                    projRef.Description = Console.ReadLine();
                                    Console.WriteLine("Project updated.");
                                }
                                else
                                {
                                    Console.WriteLine("No project found with that ID.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input.");
                            }
                            break;

                        case 8:
                            projects.ForEach(Console.WriteLine);
                            break;

                        case 9:
                            projects.ForEach(Console.WriteLine);
                            Console.Write("Enter Project ID: ");
                            if (int.TryParse(Console.ReadLine(), out var pid))
                            {
                                var projectToDos = toDos.Where(t => t.ProjectId == pid);
                                foreach (var t in projectToDos)
                                    Console.WriteLine(t);
                            }
                            else
                            {
                                Console.WriteLine("Invalid project ID.");
                            }
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("ERROR: Unknown menu selection");
                            break;
                    }
                } else
                {
                    Console.WriteLine($"ERROR: {choice} is not a valid menu selection");
                }

            } while (choiceInt != 0);

        }
    }
}