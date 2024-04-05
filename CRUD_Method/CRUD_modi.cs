using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace Task_Management__App
{
    public class CRUD1
    {
        public static List<TaskBasics> tasks = new List<TaskBasics>();
        static int IdCounter = 1;
        private static int LastId = 0;



        private int GetLastUsedIdFromXml(string filePath)
        {
            int lastUsedId = 0;
            if (File.Exists(filePath))
            {
                XDocument xmlDoc = XDocument.Load(filePath);
                var taskElements = xmlDoc.Descendants("Task");
                if (taskElements.Any())
                {
                    lastUsedId = taskElements.Select(t => int.Parse(t.Element("Id").Value)).Max();
                }
            }
            return lastUsedId;
        }


        //this method create new task >> title , description , dept and assignment 
        //desc. and  assign can be empty
        public void CreateTask()
        {
            string title; string description; string assignee; string department; string category;


            int lastUsedId = GetLastUsedIdFromXml(@"D:\saved.XML");
            int newId = lastUsedId + 1;

        ret:  Console.WriteLine("Write Task Title");
            title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title) || title.Length >50)
            {
                Console.WriteLine("Title is required less than 50 char");
                goto ret;
            }

        ret1:    Console.WriteLine("Write Task Description");
            description = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Description is required.");
                goto ret1;
            }

      ret2:       Console.WriteLine("Write Task assigned for who ");
            assignee = Console.ReadLine();
           if (string.IsNullOrWhiteSpace(assignee))
            {
                Console.WriteLine("Assignee is required.");
                goto ret2;
            }

       ret3:     Console.WriteLine("Write Task Department : DEV,CSE,CS");
           List <string> depList = new List<string>() {"DEV","CSE","CS"};
            department = Console.ReadLine();
            bool exists = depList.Exists(w => w.Equals(department, StringComparison.OrdinalIgnoreCase));

            if (string.IsNullOrWhiteSpace(department) || !exists)
            {
                Console.WriteLine("Department not found .");
                goto ret3;
            }


      ret4:      Console.WriteLine("Write Task Category : A -very impportant B- important C- not important ");
            category = Console.ReadLine();
            List<string> catList = new List<string>() {"A","B","C"};
            bool exist = catList.Exists(w => w.Equals(category, StringComparison.OrdinalIgnoreCase));

            if (string.IsNullOrWhiteSpace(category))
            {
                Console.WriteLine("Category is required.");
                goto ret4;
            }



            TaskBasics newTask = new TaskBasics
                {
                    Id = newId,
                    Title = title,
                    Description = description,
                    Assignee = assignee,
                    Department = department.ToUpper(),
                    Category = category.ToUpper()
                };
                tasks.Add(newTask);
            
           
            Console.WriteLine("Task created successfully!");

            
            

            SaveTasksToXml(tasks);// saving the data in xml file
        }

       
  


        public void UpdateTask()// update all feild
        {
            string filePath = @"D:\saved.XML";
            CRUD1 cRUD1 = new CRUD1();

            List<TaskBasics> tasks = cRUD1.ReadXmlTasks(filePath);


            Console.WriteLine("Which task ID do you want to update ?");
            cRUD1.ReadXmlTasks(filePath);
            int id = Convert.ToInt32(Console.ReadLine());


            XDocument xmlDoc = XDocument.Load(filePath);

            XElement taskElement = xmlDoc.Descendants("Task")
                                         .FirstOrDefault(e => e.Element("Id").Value == id.ToString());

            if (taskElement != null)
            {
                Console.WriteLine(" task Title :");
                string title = Console.ReadLine();
                taskElement.Element("Title").Value = title;

                Console.WriteLine("Description :");
                string desc = Console.ReadLine();
                taskElement.Element("Description").Value = desc;

                Console.WriteLine("Assign task to :");
                string assignee = Console.ReadLine();
                taskElement.Element("Assignee").Value = assignee;

                Console.WriteLine(" task category :");
                string category = Console.ReadLine();
                taskElement.Element("Category").Value = category;
                // Save the updated XML document
                xmlDoc.Save(filePath);
                Console.WriteLine("Task assigned successfully!");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }
        public void deleteTask()
        {
            string filePath = @"D:\saved.XML";
            CRUD1 cRUD1 = new CRUD1();

            Console.WriteLine("Which task ID do you want to delete ?");
            int id = Convert.ToInt32(Console.ReadLine());

            List<TaskBasics> tasks = cRUD1.ReadXmlTasks(filePath);
            XDocument xmlDoc = XDocument.Load(filePath);

            XElement taskElement = xmlDoc.Descendants("Task")
                                          .FirstOrDefault(e => e.Element("Id").Value == id.ToString());

            TaskBasics taskToUpdate = tasks.Find(t => t.Id == id);
            if (taskToUpdate != null)
            {
                tasks.Remove(taskToUpdate);

                if (taskElement != null)
                {
                    taskElement.Remove();

                    xmlDoc.Save(filePath);

                    Console.WriteLine("Task updated successfully!");
                }
                else
                {
                    Console.WriteLine("Task element not found in XML document.");
                }
            }
            else
            {
                Console.WriteLine("Task not found.");
            }


        }
        // save data in exisisting file or create  a new one 
        public void SaveTasksToXml(List<TaskBasics> tasks)
        {
            string filePath = @"D:\saved.XML";


            XDocument xmlDoc;
            if (File.Exists(filePath))
            {
                xmlDoc = XDocument.Load(filePath);
            }
            else// if the file is not exisist
            {
                xmlDoc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("Tasks")
                );
            }

            // Append new tasks to existing XML structure
            XElement tasksElement = xmlDoc.Root;
            foreach (TaskBasics task in tasks)
            {
                tasksElement.Add(
                    new XElement("Task",
                        new XElement("Id", task.Id),
                        new XElement("Title", task.Title),
                        new XElement("Description", task.Description),
                        new XElement("Assignee", task.Assignee),
                        new XElement("Department", task.Department),
                        new XElement("Category", task.Category)
                    )
                );
            }

            // Save XML document to file
            xmlDoc.Save(filePath);

            Console.WriteLine($"Data saved to '{filePath}' successfully.");
        }


        public void ReadTask(int pageNumber = 1, int pageSize = 10)// show the saved data in screen
        {
            string filePath = @"D:\saved.XML";

            List<TaskBasics> tasks = ReadXmlTasks(filePath);

            int startIndex = (pageNumber - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, tasks.Count - 1);

            // Display tasks on the screen
            for (int i = startIndex; i <= endIndex; i++)
            {
                TaskBasics task = tasks[i];

                Console.WriteLine($"Task ID: {task.Id}");
                Console.WriteLine($"Title: {task.Title}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine($"Assignee: {task.Assignee}");
                Console.WriteLine($"Department: {task.Department}");
                Console.WriteLine($"Category: {task.Category}");
                Console.WriteLine();
            }

            Console.WriteLine($"Page {pageNumber} of {Math.Ceiling((double)tasks.Count / pageSize)}");

            if (pageNumber > 1)
            {
                Console.WriteLine("Press 'P' to go to the previous page");
            }

            if (endIndex < tasks.Count - 1)
            {
                Console.WriteLine("Press 'N' to go to the next page");
            }

            ConsoleKeyInfo key = Console.ReadKey();
            if (key.KeyChar == 'P' || key.KeyChar == 'p')
            {
                if (pageNumber > 1)
                {
                    ReadTask(pageNumber - 1, pageSize);
                }
            }
            else if (key.KeyChar == 'N' || key.KeyChar == 'n')
            {
                if (endIndex < tasks.Count - 1)
                {
                    ReadTask(pageNumber + 1, pageSize);
                }
            }
        }


        public List<TaskBasics> ReadXmlTasks(string filePath)
        {
            List<TaskBasics> tasks = new List<TaskBasics>();

            XDocument xmlDoc = XDocument.Load(filePath);

            var taskElements = xmlDoc.Descendants("Task");

            foreach (var taskElement in taskElements)
            {
                TaskBasics task = new TaskBasics
                {
                    Id = int.Parse(taskElement.Element("Id").Value),
                    Title = taskElement.Element("Title").Value,
                    Description = taskElement.Element("Description").Value,
                    Assignee = taskElement.Element("Assignee").Value,
                    Department = taskElement.Element("Department").Value,
                    Category = taskElement.Element("Category").Value,
                };

                tasks.Add(task);
            }

            return tasks;
        }



    }
}

