using System;
using System.Collections.Generic;

public class Person
{
    private static int nextId = 1;

    public int Id { get; private set; }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }

    public Person(string fullName, DateTime dateOfBirth)
    {
        Id = nextId++;
        FullName = fullName;
        DateOfBirth = dateOfBirth;
    }
}

public class Student : Person
{
    public string StudentId { get; set; }

    public Student(string fullName, DateTime dateOfBirth, string studentId) : base(fullName, dateOfBirth)
    {
        StudentId = studentId;
    }
}

public static class PeopleDatabase
{
    private static readonly Dictionary<int, Person> people = new Dictionary<int, Person>();

    public static void AddPerson(Person person)
    {
        people.Add(person.Id, person);
    }

    public static Person GetPersonById(int id)
    {
        return people.ContainsKey(id) ? people[id] : null;
    }
}

public class University
{
    private Dictionary<string, Student> students = new Dictionary<string, Student>();

    public void AddStudent(Student student)
    {
        students.Add(student.StudentId, student);
    }

    public Student GetStudentByStudentId(string studentId)
    {
        return students.ContainsKey(studentId) ? students[studentId] : null;
    }

    public Person GetPersonById(int id)
    {
        return PeopleDatabase.GetPersonById(id);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var university = new University();

        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Получить информацию о человеке по ID");
            Console.WriteLine("2. Получить информацию о студенте по номеру зачётной книжки");
            Console.WriteLine("3. Добавить нового студента");
            Console.WriteLine("4. Выйти из программы");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Введите ID человека:");
                    if (int.TryParse(Console.ReadLine(), out int personId))
                    {
                        var person = university.GetPersonById(personId);
                        if (person != null)
                        {
                            Console.WriteLine($"ФИО: {person.FullName}");
                            Console.WriteLine($"Дата рождения: {person.DateOfBirth}");
                        }
                        else
                        {
                            Console.WriteLine("Человек с указанным ID не найден.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод ID.");
                    }
                    break;
                case "2":
                    Console.WriteLine("Введите номер зачётной книжки студента:");
                    string studentId = Console.ReadLine();
                    var student = university.GetStudentByStudentId(studentId);
                    if (student != null)
                    {
                        Console.WriteLine($"ФИО: {student.FullName}");
                        Console.WriteLine($"Дата рождения: {student.DateOfBirth}");
                        Console.WriteLine($"Номер зачётной книжки: {student.StudentId}");
                    }
                    else
                    {
                        Console.WriteLine("Студент с указанным номером зачётной книжки не найден.");
                    }
                    break;
                case "3":
                    Console.WriteLine("Введите ФИО студента:");
                    string fullName = Console.ReadLine();
                    Console.WriteLine("Введите дату рождения студента в формате (гггг-мм-дд):");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth))
                    {
                        Console.WriteLine("Введите номер зачётной книжки студента:");
                        string newStudentId = Console.ReadLine();
                        var newStudent = new Student(fullName, dateOfBirth, newStudentId);
                        university.AddStudent(newStudent);
                        PeopleDatabase.AddPerson(newStudent);
                        Console.WriteLine("Студент добавлен успешно.");
                    }
                    else
                    {
                        Console.WriteLine("Некорректный формат даты.");
                    }
                    break;
                case "4":
                    Console.WriteLine("Программа завершена.");
                    return;
                default:
                    Console.WriteLine("Некорректный ввод.");
                    break;
            }

            Console.WriteLine();
        }
    }
}
