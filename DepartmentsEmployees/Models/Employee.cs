﻿using System;
namespace DepartmentsEmployees.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // This is to hold the actual foreign key integer
        public int DepartmentId { get; set; }

        // This property is for storing the C# object representing the department
        public Department Department { get; set; }
    }
}
