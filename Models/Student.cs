using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentWebApi.Models
{
    public class Student
    {
        public int RollNo { get; set; }

        public string Name { get; set; }

        public double Marks { get; set; }

        public string Password { get; set; }

      
    }
}