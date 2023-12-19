using Labb3school.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3school
{
    internal class Methods
    {
        //Method to add employees
        internal static void AddEmployee(SchoolContext dbContext)
        {
            Console.Write("Enter employee first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter employee last name: ");
            string lastName = Console.ReadLine();

            Console.WriteLine($"Make sure you enter a valid Profession ID!");
            Console.WriteLine("101 - Teacher");
            Console.WriteLine("102 - Administrator");
            Console.WriteLine("103 - Principal");
            Console.WriteLine("104 - Janitor");
            Console.Write("Enter profession ID: ");
            int[] validProfessionIds = { 101, 102, 103, 104 };
            if (int.TryParse(Console.ReadLine(), out int professionId) && validProfessionIds.Contains(professionId))
            {
                dbContext.AddEmployee(firstName, lastName, professionId);
                Console.WriteLine("Employee added successfully!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid profession ID.");
                Console.ReadKey();
            }
        }

        //Method for adding students
        internal static void AddStudent(SchoolContext dbContext)
        {
            Console.Write("Enter student first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter student last name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter student phone number (optional): ");
            int? phone = int.TryParse(Console.ReadLine(), out int phoneNumber) ? phoneNumber : (int?)null;

            Console.Write("Enter student birthdate (yyyy-mm-dd): ");
            if (DateOnly.TryParse(Console.ReadLine(), out DateOnly birthdate))
            {
                Console.WriteLine("Here are all classes and their ID that the school currently has:");
                // Display available classes
                var classes = dbContext.Classes.ToList();
                foreach (var currentClass in classes)
                {
                    Console.WriteLine($"Class ID: {currentClass.ClassId}, Class Name: {currentClass.ClassName}");
                }

                Console.Write("Enter the Class ID for the student: ");
                if (int.TryParse(Console.ReadLine(), out int classId))
                {
                    try
                    {
                        dbContext.AddStudent(firstName, lastName, phone, birthdate, classId);
                        dbContext.SaveChanges();
                        Console.WriteLine("Student added successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error adding student: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid Class ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid birthdate (yyyy-mm-dd).");
            }
            Console.ReadKey();
        }
        internal static void GetEmployee()
        {
            using (var dbContext = new SchoolContext())
            {
                Console.WriteLine("Here are all professions and their ID that the school currently have!");
                var professions = dbContext.Professions.ToList();
                foreach (var profession in professions)
                {
                    Console.WriteLine($"Profession id: {profession.ProfessionId} Profession Name: {profession.ProfessionName}");
                    Console.WriteLine("--------------------------------------------------------------------------------------");
                }

                Console.WriteLine("[1] See all employees and professions");
                Console.WriteLine("[2] Search with professisonID");
                String EmployeeChoice = Console.ReadLine();
                switch (EmployeeChoice)
                {
                    case "1":
                        var employees = dbContext.Employees.ToList();
                        foreach (var employee in employees)
                        {
                            Console.WriteLine($"Employee ID: {employee.EmployeeId}, Name: {employee.FirstName} {employee.LastName}, Profession ID: {employee.FkProfessionId}");
                        }
                        break;
                    case "2":
                        Console.Write("Enter professionID: ");
                        if (int.TryParse(Console.ReadLine(), out int professionId))
                        {
                            var employeesWithProfession = dbContext.Employees
                                .Where(e => e.FkProfessionId == professionId)
                                .ToList();

                            Console.WriteLine($"Employees with Profession ID {professionId}:");
                            foreach (var employee in employeesWithProfession)
                            {
                                Console.WriteLine($"Employee ID: {employee.EmployeeId}, Name: {employee.FirstName} {employee.LastName}, Profession ID: {employee.FkProfessionId}");

                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid Profession ID.");
                            Console.ReadKey();
                        }
                        break;
                }
                Console.ReadKey();
            }
        }
        internal static void GetStudent()
        {
            using (var dbContext = new SchoolContext())
            {
                Console.Clear();
                Console.WriteLine("[1] See all students sorted by last name (ascending).");
                Console.WriteLine("[2] See all students sorted by first name (ascending).");
                Console.WriteLine("[3] See all students sorted by last name (descending).");
                Console.WriteLine("[4] See all students sorted by first name (descending).");
                Console.WriteLine("Enter your choice: ");

                String studentChoice = Console.ReadLine();
                switch (studentChoice)
                {
                    case "1":
                        DisplayStudentsSorted(dbContext.Students.OrderBy(s => s.LastName).ToList());
                        break;
                    case "2":
                        DisplayStudentsSorted(dbContext.Students.OrderBy(s => s.FirstName).ToList());
                        break;
                    case "3":
                        DisplayStudentsSorted(dbContext.Students.OrderByDescending(s => s.LastName).ToList());
                        break;
                    case "4":
                        DisplayStudentsSorted(dbContext.Students.OrderByDescending(s => s.FirstName).ToList());
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
                Console.ReadKey();
            }
            // Helper method to display students
            static void DisplayStudentsSorted(List<Student> students)
            {
                foreach (var student in students)
                {
                    Console.WriteLine($"Name: {student.FirstName} {student.LastName}, Birthdate: {student.Birthdate} StudentID: {student.StudentId}");
                    Console.WriteLine("--------------------------------------------------------------------------------------");
                }
            }

        }

        internal static void GetClass()
        {
            using (var dbContext = new SchoolContext())
            {
                Console.WriteLine("Here are all classes and their ID that the school currently have!");
                var classes = dbContext.Classes.ToList();
                foreach (var Class in classes)
                {
                    Console.WriteLine($"Class ID: {Class.ClassId} Class Name: {Class.ClassName}");
                    Console.WriteLine("--------------------------------------------------------------------------------------");
                }

                Console.Write("Enter the Class ID to see students: ");
                if (int.TryParse(Console.ReadLine(), out int classId))
                {
                    var studentsInClass = dbContext.Students
                        .Where(s => s.FkClassId == classId)
                        .ToList();

                    if (studentsInClass.Any())
                    {
                        Console.WriteLine($"Students in Class ID {classId}:");
                        foreach (var student in studentsInClass)
                        {
                            Console.WriteLine($"Name: {student.FirstName} {student.LastName}, Birthdate: {student.Birthdate}");
                            Console.WriteLine("--------------------------------------------------------------------------------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No students found for Class ID {classId}.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid Class ID.");
                }
                Console.ReadKey();
            }
        }
        internal static void GetGrade()
        {
            using (var dbContext = new SchoolContext())
            {
                Console.WriteLine("This is all grades that was set last month");
                var grades = dbContext.Grades
                .Where(g => g.Date >= new DateOnly(2023, 12, 1))
                .OrderBy(g => g.Date)
                .ToList();

                foreach (var grade in grades)
                {
                    Console.WriteLine($"Grade: {grade.Grade1}, Subject: {grade.Subject}, StudnetID: {grade.FkStudentId}, Date: {grade.Date}");
                }
            }
            Console.ReadKey();

        }

       internal static void AverageGradeForStudent()
        {
            using (var dbContext = new SchoolContext())
            {
                var allGrades = dbContext.Grades.ToList();

                if (allGrades.Any())
                {
                    // Group grades by subject
                    var gradesBySubject = allGrades.GroupBy(g => g.Subject);

                    foreach (var group in gradesBySubject)
                    {
                        var subject = group.Key;
                        var subjectGrades = group.ToList();

                        // Map letter grades to numeric values
                        var numericGrades = subjectGrades
                            .Select(g => (g.Grade1))
                            .ToList();

                        // Calculate average grade for the subject
                        var averageGrade = numericGrades.DefaultIfEmpty(0).Average();

                        // Find the highest and lowest grades for the subject
                        var highestGrade = numericGrades.DefaultIfEmpty(0).Max();
                        var lowestGrade = numericGrades.DefaultIfEmpty(0).Min();

                        Console.WriteLine($"Subject: {subject}");
                        Console.WriteLine($"Average Grade: {(averageGrade)}");
                        Console.WriteLine($"Highest Grade: {(highestGrade)}");
                        Console.WriteLine($"Lowest Grade: {(lowestGrade)}");
                        Console.WriteLine("-----------------------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("No grades found for any student.");
                }
            }
            Console.ReadKey();

        }

    }
}
