using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management__App
{
    internal class Comment
    {
        public void comment()
        {
            CRUD1 cRUD1 = new CRUD1();
            List<TaskBasics> taskList = CRUD1.tasks;

            Console.WriteLine("Which task ID do you want to add ?");
            cRUD1.ReadTask();
            int id = Convert.ToInt32(Console.ReadLine());

            TaskBasics taskToUpdate = taskList.Find(t => t.Id == id);
            if (taskToUpdate != null)
            {
                Console.WriteLine("Enter new description for the task:");
                string newDescription = Console.ReadLine();
                taskToUpdate.Description = newDescription;
                Console.WriteLine("Task description updated successfully!");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }

        }
    }
}
