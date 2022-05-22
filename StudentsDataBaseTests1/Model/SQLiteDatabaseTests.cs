using Microsoft.VisualStudio.TestTools.UnitTesting;
using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Tests
{
    [TestClass()]
    public class SQLiteDatabaseTests
    {
        [TestMethod()]
        public void InitialRowsTest()
        {
            SQLiteDatabase database = new SQLiteDatabase("Data Source=D:\\Programs\\SQLite\\Students.db");
            DbRowList<Student> students = new DbRowList<Student>("Student");
            database.InitialRows(students);

            students.Add(new Student(0, "some", 0, 0, "some"));
            database.SaveChanges(students);

            DbRowList<Student> studentsRes = new DbRowList<Student>("Student");
            database.InitialRows(studentsRes);

            bool isEquals = false;

            if (students.GetLast().Equals(studentsRes.GetLast()))
            {
                isEquals = true;
            }

            students.Remove(students.GetLast());
            database.SaveChanges(students);

            Assert.IsTrue(isEquals);
        }
    }
}