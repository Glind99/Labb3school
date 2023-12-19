using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Labb3school.Models;

public partial class SchoolContext : DbContext
{
    public SchoolContext()
    {
    }

    public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Profession> Professions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB;initial Catalog=School;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.ToTable("Class");

            entity.Property(e => e.ClassId).ValueGeneratedNever();
            entity.Property(e => e.ClassName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Class Name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("First Name");
            entity.Property(e => e.FkProfessionId).HasColumnName("FK_ProfessionId");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Last Name");

            entity.HasOne(d => d.FkProfession).WithMany(p => p.Employees)
                .HasForeignKey(d => d.FkProfessionId)
                .HasConstraintName("FK_Employees_Professions");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.ToTable("Grade");

            entity.Property(e => e.FkEmployeeId).HasColumnName("FK_EmployeeId");
            entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentID");
            entity.Property(e => e.Grade1).HasColumnName("Grade");
            entity.Property(e => e.Subject)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.FkEmployee).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkEmployeeId)
                .HasConstraintName("FK_Grade_Employees");

            entity.HasOne(d => d.FkStudent).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkStudentId)
                .HasConstraintName("FK_Grade_Students");
        });

        modelBuilder.Entity<Profession>(entity =>
        {
            entity.Property(e => e.ProfessionId).ValueGeneratedNever();
            entity.Property(e => e.ProfessionName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("First Name");
            entity.Property(e => e.FkClassId).HasColumnName("FK_ClassId");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Last Name");

            entity.HasOne(d => d.FkClass).WithMany(p => p.Students)
                .HasForeignKey(d => d.FkClassId)
                .HasConstraintName("FK_Students_Class");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public void AddEmployee(string firstName, string lastName, int professionId)
    {
        try
        {
          var newEmployee = new Employee
          {
            FirstName = firstName,
            LastName = lastName,
            FkProfessionId = professionId
          };

            Employees.Add(newEmployee);
            SaveChanges();
            Console.WriteLine("Employee added successfully!");
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Error adding employee: {ex.Message}");
            Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");

        }
    }
    public void AddStudent(string firstName, string lastName, int? phone, DateOnly birthdate, int classId)
    {
        try
        {
            using (var dbContext = new SchoolContext())
            {
                Student newStudent = new Student()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Phone = phone,
                    Birthdate = birthdate,
                    FkClassId = classId
                };
                Students.Add(newStudent);
                SaveChanges();

                Console.WriteLine("Student added successfully!");
            }
        }
        catch (Exception ex)
        {
            // Logga fel till loggfil eller använd en logger
            Console.WriteLine($"Error adding student: {ex.Message}");
        }
    }
}
