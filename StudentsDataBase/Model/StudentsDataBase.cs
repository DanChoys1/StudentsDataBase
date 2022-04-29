using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace DataBase 
{
    internal class StudentsDataBase
    {
        public List<Student> Students { get; private set; }

        private readonly SqliteConnection connection;

        private readonly SqliteCommand readStudentsCommand;
        private readonly SqliteCommand writeStudentCommand;
        private readonly SqliteCommand updateStudentCommand;
        private readonly SqliteCommand deleteStudentCommand;

        public StudentsDataBase(string dataBaseName)
        {
            connection = new SqliteConnection(dataBaseName);
            connection.OpenAsync().Wait();

            readStudentsCommand = new SqliteCommand("SELECT * FROM Student", connection);
            writeStudentCommand = new SqliteCommand("INSERT INTO Student (ФИО, Курс, Группа, Форма_Обучения) " +
                                                        "VALUES (@name, @course, @group, @form)", connection);
            updateStudentCommand = new SqliteCommand("UPDATE Student " +
                                                        "SET ФИО = @name, Курс = @course, Группа = @group, Форма_Обучения = @form " +
                                                        "WHERE id = @id", connection);
            deleteStudentCommand = new SqliteCommand("DELETE FROM Student " +
                                                        "WHERE id = @id", connection);
        }

        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();

            using (SqliteDataReader reader = readStudentsCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        int course = reader.GetInt32(2);
                        int group = reader.GetInt32(3);
                        string form = reader.GetString(4);

                        students.Add(new Student(id, name, course, group, form));
                    }
                }
            }

            Students = new List<Student>(students);

            return students;
        }

        public void AddStudent(Student student)
        {
            SqliteParameter nameParam = new SqliteParameter("@name", student.Name);
            SqliteParameter courseParam = new SqliteParameter("@course", student.Course);
            SqliteParameter groupParam = new SqliteParameter("@group", student.Group);
            SqliteParameter formParam = new SqliteParameter("@form", student.Form);

            writeStudentCommand.Parameters.Clear();
            writeStudentCommand.Parameters.Add(nameParam);
            writeStudentCommand.Parameters.Add(courseParam);
            writeStudentCommand.Parameters.Add(groupParam);
            writeStudentCommand.Parameters.Add(formParam);

            writeStudentCommand.ExecuteNonQuery();
        }

        public async void AddStudentAsync(Student student)
        {
            await Task.Run(() =>
            {
                AddStudent(student);
            });
        }

        public void UpdateStudent(Student student)
        {
            SqliteParameter idParam = new SqliteParameter("@id", student.Id);
            SqliteParameter nameParam = new SqliteParameter("@name", student.Name);
            SqliteParameter courseParam = new SqliteParameter("@course", student.Course);
            SqliteParameter groupParam = new SqliteParameter("@group", student.Group);
            SqliteParameter formParam = new SqliteParameter("@form", student.Form);

            updateStudentCommand.Parameters.Clear();
            updateStudentCommand.Parameters.Add(idParam);
            updateStudentCommand.Parameters.Add(nameParam);
            updateStudentCommand.Parameters.Add(courseParam);
            updateStudentCommand.Parameters.Add(groupParam);
            updateStudentCommand.Parameters.Add(formParam);

            updateStudentCommand.ExecuteNonQuery();
        }

        public async void UpdateStudentAsync(Student student)
        {
            await Task.Run(() =>
            {
                UpdateStudent(student);
            });
        }

        public void DeleteStudent(Student student)
        {
            SqliteParameter idParam = new SqliteParameter("@id", student.Id);

            deleteStudentCommand.Parameters.Clear();
            deleteStudentCommand.Parameters.Add(idParam);

            deleteStudentCommand.ExecuteNonQuery();
        }

        public async void DeleteStudentAsync(Student student)
        {
            await Task.Run(() =>
            {
                DeleteStudent(student);
            });
        }

        ~StudentsDataBase()
        {
            connection.CloseAsync().Wait();
        }
    }
}
