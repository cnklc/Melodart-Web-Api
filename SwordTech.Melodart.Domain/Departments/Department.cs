using SwordTech.Melodart.Domain._ManyToMany;
using SwordTech.Melodart.Domain.Lessons;
using SwordTech.Melodart.Domain.Teachers;
using SwordTech.Melodart.Helper.Entity;
using SwordTech.Melodart.Helper.Error;
using System.Collections.ObjectModel;

namespace SwordTech.Melodart.Domain.Departments;

public class Department : Entity
{
    public Department(string name, string description)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new DomainException("İsim alanı boş olamaz.");
        }

        Name = name;
        Description = description;
    }

    public Department()
    {
    }

    public string Name { get; private set; }
    public string Description { get; private set; }

    public Collection<TeacherDepartment> TeacherDepartments { get; set; } = new Collection<TeacherDepartment>();
    public Collection<Lesson> Lessons { get; set; } = new Collection<Lesson>();
    public Collection<Schedule> Schedules { get; set; } = new Collection<Schedule>();
}
