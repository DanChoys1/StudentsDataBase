using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Student : DbRow
    {
        [Primary]
        [AutoIncrement]
        public Int64 Id { get; set; }
        //Guid, uuid
        public string Name { get; set; }
        public Int64 Course { get; set; }
        public Int64 Group { get; set; }
        public string Form { get; set; }

        public Student()
        {}

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
