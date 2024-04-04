using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Task_Management__App
{
    public class Filter
    {
        List<TaskBasics> taskList;

        public Filter()
        {
            LoadTasksFromXml();
        }

        private void LoadTasksFromXml()
        {
            string filePath = @"D:\saved.xml";

            try
            {
                XDocument xmlDoc = XDocument.Load(filePath);

                taskList = xmlDoc.Descendants("Task")
                                 .Select(task => new TaskBasics
                                 {
                                     Id = int.Parse(task.Element("Id").Value),
                                     Title = task.Element("Title").Value,
                                     Description = task.Element("Description").Value,
                                     Assignee = task.Element("Assignee").Value,
                                     Department = task.Element("Department").Value
                                 })
                                 .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading tasks from XML: {ex.Message}");
                taskList = new List<TaskBasics>(); 
            }
        }

        public void ChooseFilter()
        {
            Console.WriteLine("Select filter option: 1. Department 2. Title 3. Assignee");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    DepartmentFilter();
                    break;
                case 2:
                    TitleFilter();
                    break;
                case 3:
                    AssigneeFilter();
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }


        public void DepartmentFilter()
        {
            Console.WriteLine("Which department do you want?");
            string department = Console.ReadLine();

            var filteredResults = from task in taskList
                                  where task.Department.Equals(department, StringComparison.OrdinalIgnoreCase)
                                  select $"{task.Id}- {task.Title} assigned to {task.Assignee}";

            foreach (var result in filteredResults)
            {
                Console.WriteLine(result);
            }
        }

        public void TitleFilter()
        {
            Console.WriteLine("Which title do you want?");
            string title = Console.ReadLine();

            var filteredResults = from task in taskList
                                  where task.Title.Equals(title, StringComparison.OrdinalIgnoreCase)
                                  select $"This task is owned by {task.Assignee} who works in {task.Department} department";

            foreach (var result in filteredResults)
            {
                Console.WriteLine(result);
            }
        }

        public void AssigneeFilter()
        {
            Console.WriteLine("Which employee do you want?");
            string assignee = Console.ReadLine();

            var filteredResults = from task in taskList
                                  where task.Assignee.Equals(assignee, StringComparison.OrdinalIgnoreCase)
                                  select $"{task.Assignee} works on {task.Title}";

            foreach (var result in filteredResults)
            {
                Console.WriteLine(result);
            }
        }
    }
}
