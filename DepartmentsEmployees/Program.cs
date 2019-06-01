using System;
using System.Collections.Generic;
using System.Linq;
using DepartmentsEmployees.Data;
using DepartmentsEmployees.Models;

namespace DepartmentsEmployees
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create instance of repository class to use its methods and interact with the database.
            var repository = new Repository();

            var departments = repository.GetAllDepartments();

            PrintDepartmentReport("All Departments", departments);

            Pause();

            var accounting = new Department { DeptName = "Accounting" };
            repository.AddDepartment(accounting);

            departments = repository.GetAllDepartments();
            PrintDepartmentReport("All Departments after adding Accounting", departments);

            Pause();

            var accountingDepartmentFromDB = departments.First(d => d.DeptName == "Accounting");

            Console.WriteLine($"                accounting --> {accounting.Id}: {accounting.DeptName}");
            Console.WriteLine($"accountingDepartmentFromDB --> {accountingDepartmentFromDB.Id}: {accountingDepartmentFromDB.DeptName}");

            Pause();

            accountingDepartmentFromDB.DeptName = "Creative Accounting";
            repository.UpdateDepartment(accountingDepartmentFromDB.Id, accountingDepartmentFromDB);

            departments = repository.GetAllDepartments();
            PrintDepartmentReport("All Departments after updating Accounting department", departments);

            Pause();

            repository.DeleteDepartment(accountingDepartmentFromDB.Id);

            departments = repository.GetAllDepartments();
            PrintDepartmentReport("All Departments after deleting Accounting department", departments);

            Pause();

            var employees = repository.GetAllEmployees();

            PrintEmployeeReport("All Employees", employees);

            Pause();

            employees = repository.GetAllEmployeesWithDepartment();
            PrintEmployeeReport("All Employees with departments", employees);

            Pause();

            var firstDepartment = departments[0];
            employees = repository.GetAllEmployeesWithDepartmentByDepartmentId(firstDepartment.Id);
            PrintEmployeeReport($"Employees in {firstDepartment.DeptName}", employees);

            Pause();

            var jane = new Employee
            {
                FirstName = "Jane",
                LastName = "Lucas",
                DepartmentId = firstDepartment.Id
            };
            repository.AddEmployee(jane);

            employees = repository.GetAllEmployeesWithDepartment();
            PrintEmployeeReport("All Employees after adding Jane", employees);

            Pause();

            Employee dbJane = employees.First(e => e.FirstName == "Jane");

            var secondDepartment = departments[1];

            dbJane.DepartmentId = secondDepartment.Id;
            repository.UpdateEmployee(dbJane.Id, dbJane);

            employees = repository.GetAllEmployeesWithDepartment();
            PrintEmployeeReport("All Employees after udpating Jane", employees);

            Pause();

            repository.DeleteEmployee(dbJane.Id);
            employees = repository.GetAllEmployeesWithDepartment();

            PrintEmployeeReport("All Employees after updating Jane", employees);
            Pause();

        }

        /// <summary>
        /// Prints a simple report with the given title and department information
        /// </summary>
        /// <param name="title"></param>
        /// <param name="departments"></param>
        public static void PrintDepartmentReport(string title, List<Department> departments)
        {
            Console.WriteLine(title);
            foreach (var d in departments)
            {
                Console.WriteLine($"{d.Id}: {d.DeptName}");
            }
        }

        /// <summary>
        /// Prints a simple report with the given title and employee information.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="employees"></param>
        public static void PrintEmployeeReport(string title, List<Employee> employees)
        {
            Console.WriteLine(title);
            foreach (var e in employees)
            {
                string line = $"{e.Id}: {e.FirstName} {e.LastName}";
                if (e.Department != null)
                {
                    line += $". Dept: {e.Department.DeptName}";
                }
                Console.WriteLine(line);
            }
        }

        public static void Pause()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("------------");
        }
    }
}
