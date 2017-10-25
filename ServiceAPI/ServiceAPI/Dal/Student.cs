using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceAPI.Dal
{

    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string BirthPlace { get; set; }


        public string RegistrationNumber { get; set; }

        public bool Gender { get; set; } //true male,false female


        public DateTime DateOfBirth { get; set; }
    }
}
