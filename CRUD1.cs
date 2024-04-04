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


        //this method create new task >> title , description , dept and assignment 
        //desc. and  assign can be empty
        public void CreateTask()
        {
            string title; string description; string assignee; string department;

            Console.WriteLine("Write Task Title");
            title = Console.ReadLine();

            Console.WriteLine("Write Task Description");
            description = Console.ReadLine();

            Console.WriteLine("Write Task assigned for who ");
            assignee = Console.ReadLine();

            Console.WriteLine("Write Task Department");
            department = Console.ReadLine();

            TaskBasics newTask = new TaskBasics
            {
                Id = ++IdCounter,
                Title = title,
                Description = description,
                Assignee = assignee,
                Department = department
            };
            tasks.Add(newTask);
            Console.WriteLine("Task created successfully!");

            SaveTasksToXml(tasks);// saving the data in xml file
        }



        public void ReadTask()// show the saved data in screen
        {
            string filePath = @"D:\saved.XML";

            ReadXmlTasks(filePath);
        }

        public void UpdateTask()// update all feild
        {
            string filePath = @"D:\saved.xml";
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
            string filePath = @"D:\saved.xml";
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
            string filePath = @"D:\saved.xml";

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
                        new XElement("Department", task.Department)
                    )
                );
            }

            // Save XML document to file
            xmlDoc.Save(filePath);

            Console.WriteLine($"Data saved to '{filePath}' successfully.");
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
                    Department = taskElement.Element("Department").Value
                };

                tasks.Add(task);
            }

            return tasks;
        }



    }
}

