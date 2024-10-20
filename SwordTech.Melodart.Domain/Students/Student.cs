using SwordTech.Melodart.Domain._ManyToMany;
using SwordTech.Melodart.Domain.Contracts.Student;
using System.Collections.ObjectModel;
using SwordTech.Melodart.Domain.Finance;
using SwordTech.Melodart.Domain.Lessons;
using SwordTech.Melodart.Helper.Entity;
using SwordTech.Melodart.Helper.Error;

namespace SwordTech.Melodart.Domain.Students;

public class Student : Entity
{
    public Student(string name, string lastName, string phoneNumber, DateTime? birthday, string? address, string? description, GenderType? gender)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new DomainException("İsim alanı boş olamaz.");
        }

        if (string.IsNullOrEmpty(lastName))
        {
            throw new DomainException("Soyisim alanı boş olamaz.");
        }

        Name = name;
        LastName = lastName;
        PhoneNumber = phoneNumber;

        Birthday = birthday;
        Address = address;
        Description = description;
        Gender = gender;
    }

    public Student()
    {
    }

    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? Description { get; private set; }
    public DateTime? Birthday { get; private set; }
    public string? Address { get; private set; }
    public GenderType? Gender { get; set; }

    public Collection<Parent> Parents { get; private set; } = new Collection<Parent>();
    public Collection<Transaction> Transactions { get; private set; } = new Collection<Transaction>();

    public Collection<StudentDepartment> StudentDepartment { get; private set; } = new Collection<StudentDepartment>();
    public Collection<TeacherStudent> TeacherStudents { get; private set; } = new Collection<TeacherStudent>();

    public Collection<Lesson> Lessons { get; set; } = new Collection<Lesson>();
    public Collection<Schedule> Schedules { get; set; } = new Collection<Schedule>();

    public void AddParent(Parent parent)
    {
        Parents.Add(parent);
    }

    public void AddStudentDepartment(StudentDepartment studentDepartment)
    {
        StudentDepartment.Add(studentDepartment);
    }

    public void AddTeacherStudents(TeacherStudent teacherStudent)
    {
        TeacherStudents.Add(teacherStudent);
    }
}
