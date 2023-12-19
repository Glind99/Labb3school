using Labb3school.Models;

namespace Labb3school
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //using (var dbContext = new SchoolContext())
            // AddEmployee(dbContext);

            using (var dbContext = new SchoolContext())
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("[1] See employees.");
                    Console.WriteLine("[2] See students");
                    Console.WriteLine("[3] See classes and students");
                    Console.WriteLine("[4] Retrieve all grades set in the last month");
                    Console.WriteLine("[5] Average grades: ");
                    Console.WriteLine("[6] Add an employee");
                    Console.WriteLine("[7] Add an student");


                    String Choice = Console.ReadLine();
                    switch (Choice)
                    {
                        case "1":
                            Console.Clear();
                            Methods.GetEmployee();
                            break;
                        case "2":
                            Console.Clear();
                            Methods.GetStudent();
                            break;
                        case "3":
                            Console.Clear();
                            Methods.GetClass();
                            break;
                        case "4":
                            Console.Clear();
                            Methods.GetGrade();
                            break;
                        case "5": // Add a new option in your switch statement for calculating average grade
                            Console.Clear();
                            Methods.AverageGradeForStudent();

                            break;
                        case "6":
                            Console.Clear();
                            Methods.AddEmployee(dbContext);
                            break;
                        case "7":
                            Console.Clear();
                            Methods.AddStudent(dbContext);
                            break;

                    }
                }

            }

            //static void AddEmployee(SchoolContext dbContext)
            //{
            //    Console.Write("Enter employee first name: ");
            //    string firstName = Console.ReadLine();

            //    Console.Write("Enter employee last name: ");
            //    string lastName = Console.ReadLine();

            //    Console.WriteLine($"Make sure you enter a valid Profession ID!");
            //    Console.WriteLine("1001 - Teacher");
            //    Console.WriteLine("1002 - Administrator");
            //    Console.WriteLine("1003 - Principal");
            //    Console.WriteLine("1004 - Janitor");
            //    Console.Write("Enter profession ID: ");
            //    int[] validProfessionIds = { 101, 102, 103, 1044 };
            //    if (int.TryParse(Console.ReadLine(), out int professionId) && validProfessionIds.Contains(professionId))
            //    {
            //        dbContext.AddEmployee(firstName, lastName, professionId);
            //        Console.WriteLine("Employee added successfully!");
            //        Console.ReadKey();
            //    }
            //    else
            //    {
            //        Console.WriteLine("Invalid input. Please enter a valid profession ID.");
            //        Console.ReadKey();
            //    }
            //}
        }
    }
}
