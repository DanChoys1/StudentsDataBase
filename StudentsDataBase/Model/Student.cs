using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Course { get; set; }
        public int Group { get; set; }
        public string Form { get; set; }

        public Student(int id, string name, int course, int group, string form)
        {
            Id = id;
            Name = name;
            Course = course;
            Group = group;
            Form = form;
        }

        public Student(string id, string name, string course, string group, string form)
        {
            Id = Convert.ToInt32(id);
            Name = name;
            Course = Convert.ToInt32(course);
            Group = Convert.ToInt32(group);
            Form = form;
        }

        public Student(string name, int course, int group, string form)
        {
            Name = name;
            Course = course;
            Group = group;
            Form = form;
        }

        public Student(string name, string course, string group, string form)
        {
            Name = name;
            Course = Convert.ToInt32(course);
            Group = Convert.ToInt32(group);
            Form = form;
        }
    }
}
