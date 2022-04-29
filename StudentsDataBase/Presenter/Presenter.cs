using System;
using System.Collections.Generic;
using System.Text;
using UI;
using DataBase;
using Files;

namespace Presenter
{
    internal class PresenterData
    {
        private readonly IMainForm _mainForm;
        private readonly IInteractionInformation _interactionInformation;

        private readonly StudentsDataBase _database;

        public PresenterData(IMainForm view, IInteractionInformation interactionInformation)
        {
            _interactionInformation = interactionInformation;
            _mainForm = view;

            _mainForm.UpdateEvent += UpdateDataGridView;
            _mainForm.AddEvent += AddRecord;
            _mainForm.DeleteEvent += DeleteRecord;
            _mainForm.EditEvent += EditRecord;

            _database = new StudentsDataBase("Data Source=D:\\Program\\SQLite\\Students.db");

            UpdateDataGridView(this, new EventArgs());

            ExcelFile.Read("D:\\Учеба\\Программирование\\Сем 4\\StudentsDataBase\\StudentsDataBase\\1.xlsx", out List<List<string>> students);
        }

        private void UpdateDataGridView(object sender, EventArgs e)
        {
            _mainForm.DataGridView.Rows.Clear();

            List<Student> students = _database.GetStudents();

            foreach (Student student in students)
            {
                _mainForm.DataGridView.Rows.Add(student.Name, student.Course, student.Group, student.Form);
            }
        }

        private void AddRecord(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult result = _interactionInformation.ShowInteractionInformation();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string name = _interactionInformation.StudentName;
                string course = _interactionInformation.Course;
                string group = _interactionInformation.Group;
                string form = _interactionInformation.Form;

                _database.AddStudentAsync(new Student(name, course, group, form));
                UpdateDataGridView(this, new EventArgs());
            }
        }

        private void DeleteRecord(object sender, EventArgs e)
        {
            Student student = _database.Students[_mainForm.DataGridView.SelectedRows[0].Index];
            _database.DeleteStudentAsync(student);

            UpdateDataGridView(this, new EventArgs());
        }

        private void EditRecord(object sender, EventArgs e)
        {
            Student student = _database.Students[_mainForm.DataGridView.SelectedRows[0].Index];

            System.Windows.Forms.DialogResult result = 
                _interactionInformation.ShowInteractionInformation(student.Name, student.Course, student.Group, student.Form);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                student.Name = _interactionInformation.StudentName;
                student.Course = Convert.ToInt32(_interactionInformation.Course);
                student.Group = Convert.ToInt32(_interactionInformation.Group);
                student.Form = _interactionInformation.Form;

                _database.UpdateStudentAsync(student);

                UpdateDataGridView(this, new EventArgs());
            }
        }
    }
}
