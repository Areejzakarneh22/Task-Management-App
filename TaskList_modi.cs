﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace Task_Management__App
{
    public class TaskBasics
    {


        private string _Title;
        private int _ID;
        private string _department;
        private string _assignee;
        private string _description;
        private string _category;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }
        public int Id
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }
        public string Assignee
        {
            get { return _assignee; }
            set { _assignee = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }


     

        

    }
}
