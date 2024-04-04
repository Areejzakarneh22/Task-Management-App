using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace Task_Management__App
{
    internal class Assignment
    {
        public void AssignTask()
        {
            string filePath = @"D:\saved.xml";
            CRUD1 cRUD1 = new CRUD1();

            List<TaskBasics> tasks = cRUD1.ReadXmlTasks(@"D:\saved.xml");


            Console.WriteLine("Which task ID do you want to assign ?");
           cRUD1.ReadXmlTasks(filePath);
            int id = Convert.ToInt32(Console.ReadLine());

            XDocument xmlDoc = XDocument.Load(filePath);

            XElement taskElement = xmlDoc.Descendants("Task")
                                         .FirstOrDefault(e => e.Element("Id").Value == id.ToString());

            if (taskElement != null)
            {
                Console.WriteLine("Assign task to :");
                string assignee = Console.ReadLine();

                // Update the Assignee attribute of the task element
                taskElement.Element("Assignee").Value = assignee;

                // Save the modified XML document
                xmlDoc.Save(filePath);
                Console.WriteLine("Task assigned successfully!");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }

    }
}
