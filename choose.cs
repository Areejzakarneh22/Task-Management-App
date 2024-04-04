using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;


namespace Task_Management__App
{
    internal class choose
    {
        public void choose1()
        {
            Comment c = new Comment();
            Assignment A = new Assignment();
            Filter f = new Filter();


           // List<TaskBasics> B = CRUD1.tasks;

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n\nWelcome what do you want to do?");
                Console.WriteLine("1- Task CRUD Operations." +
                    "\r\n2- Task Assignments." +
                    "\r\n3- Task Comments." +
                    "\r\n4- Task Categories." +
                    "\r\n5- Pagination." +
                    "\r\n6- Data Validation." +
                    "\r\n7- Filtering criteria." +
                    "\r\n8- Exit ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1: Choose2(); break;
                    case 2: A.AssignTask(); break;
                    case 3: c.CommentTask();break;
                    case 7: f.ChooseFilter();break;
                    case 8: exit = true; break;
                    default: Console.WriteLine("Bad option. Please try again.\n"); break;
                }
            }

            Console.WriteLine("Goodbye...");
        }


        // choose method for choose the performing action
        public void Choose2()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n\nwhat do you want to do?");
                Console.WriteLine("1. Create 2. Read 3. Update 4. Delete 5. Exit");

                int choice;
                CRUD1 cRUD1 = new CRUD1();

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }


                switch (choice)
                {
                    case 1: cRUD1.CreateTask(); break;
                    case 2: cRUD1.ReadTask(); break;
                    case 3: cRUD1.UpdateTask(); break;
                    case 4: cRUD1.deleteTask(); break;
                    case 5: exit = true; break;
                    default: Console.WriteLine("Bad option. Please try again.\n"); break;
                }
            }

            Console.WriteLine("All changes saved.");
            choose1();
        }
    }
}
