using System;
using System.Collections.Generic;
using System.Text;
using UI;
using Database;
using Files;

namespace Presenter
{
    internal class PresenterData
    {
        private readonly IMainForm _mainForm;
        private readonly IInteractionInformation _interactionInformation;

        private readonly SQLiteDatabase _database;

        private readonly DbRowList<Student> _students;

        public PresenterData(IMainForm view, IInteractionInformation interactionInformation)
        {
            _interactionInformation = interactionInformation;
            _mainForm = view;

            InitialiseMainForm();

            _database = new SQLiteDatabase("Data Source=D:\\Programs\\SQLite\\Students.db");
            _students = new DbRowList<Student>("Student");

            UpdateDataGridView(this, new EventArgs());
        }

        private void InitialiseMainForm()
        {
            _mainForm.SaveFileDialog.Filter = "Text files(*.txt)|*.txt|Excel files(*.xlsx)| *.xlsx| All files(*.*) | *.*";

            _mainForm.DataGridView.ColumnCount = 4;
            _mainForm.DataGridView.Columns[0].HeaderText = "ФИО";
            _mainForm.DataGridView.Columns[1].HeaderText = "Курс";
            _mainForm.DataGridView.Columns[2].HeaderText = "Группа";
            _mainForm.DataGridView.Columns[3].HeaderText = "Форма обучения";

            _mainForm.DataGridView.Columns[1].Width = 100;
            _mainForm.DataGridView.Columns[2].Width = 100;
            _mainForm.DataGridView.Columns[3].Width = 100;

            _mainForm.UpdateEvent += UpdateDataGridView;
            _mainForm.AddEvent += AddRecord;
            _mainForm.DeleteEvent += DeleteRecord;
            _mainForm.EditEvent += EditRecord;
            _mainForm.SaveDataEvent += SaveRecords;
            _mainForm.SaveDataToDbEvent += SaveToDb;
            _mainForm.ViewClosingEvent += Closing;
        }

        private void UpdateDataGridView(object sender, EventArgs e)
        {
            if (_students.IsChanged())
            {
                System.Windows.Forms.DialogResult res =
                    System.Windows.Forms.MessageBox.Show("Изменения не были сохранены. Продолжить?",
                                                        "Сообщение",
                                                        System.Windows.Forms.MessageBoxButtons.YesNo); // Здесь или в форме? Как если в форме? Создавать переменную в интерфейсе для сообщения?

                if (res == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            _mainForm.DataGridView.Rows.Clear();

            _database.InitialRows(_students);

            foreach (Student student in _students)
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

                _students.Add(new Student(name, course, group, form));

                _mainForm.DataGridView.Rows.Add(name, course, group, form);
            }

            _interactionInformation.Clear();
        }

        private void DeleteRecord(object sender, EventArgs e)
        {
            int selectRowIndex = _mainForm.DataGridView.SelectedRows[0].Index;

            _students.RemoveAt(selectRowIndex);
            _mainForm.DataGridView.Rows.RemoveAt(selectRowIndex);
        }

        private void EditRecord(object sender, EventArgs e)
        {
            int selectRowIndex = _mainForm.DataGridView.SelectedRows[0].Index;

            Student student = _students[selectRowIndex];

            System.Windows.Forms.DialogResult result = 
                _interactionInformation.ShowInteractionInformation(student.Name, (int)student.Course, (int)student.Group, student.Form);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                student.Name = _interactionInformation.StudentName;
                student.Course = Convert.ToInt32(_interactionInformation.Course);
                student.Group = Convert.ToInt32(_interactionInformation.Group);
                student.Form = _interactionInformation.Form;

                _mainForm.DataGridView.Rows[selectRowIndex].SetValues(student.Name, student.Course, student.Group, student.Form);
            }

            _interactionInformation.Clear();
        }

        private void SaveToDb(object sender, EventArgs e)
        {
            if (_students.IsChanged())
            {
                //_database.SaveChangesAsync(_students);
                _database.SaveChanges(_students);
            }

            UpdateDataGridView(this, new EventArgs());
        }

        private void SaveRecords(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult result = _mainForm.SaveFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                int countColumns = _mainForm.DataGridView.Columns.Count;
                int countRows = _mainForm.DataGridView.Rows.Count;

                string[,] studentData = new string[countRows, countColumns];

                for (int i = 0; i < countRows; i++)
                {
                    for (int j = 0; j < countColumns; j++)
                    {
                        studentData[i, j] = _mainForm.DataGridView.Rows[i].Cells[j].Value.ToString();
                    }
                }

                string filter = System.IO.Path.GetExtension(_mainForm.SaveFileDialog.FileName);

                if (filter == ".xlsx")
                {
                    ExcelFile.Write(_mainForm.SaveFileDialog.FileName, studentData);
                }
                else if (filter == ".txt")
                {
                    TxtFile.Write(_mainForm.SaveFileDialog.FileName, studentData);
                }
                else
                {
                    TxtFile.Write(_mainForm.SaveFileDialog.FileName, studentData); ;
                }
            }
        }

        private void Closing(object sender, EventArgs e)
        {
            if (_students.IsChanged())
            {
                System.Windows.Forms.DialogResult res =
                    System.Windows.Forms.MessageBox.Show("Изменения не были сохранены. Продолжить?",
                                                        "Сообщение",
                                                        System.Windows.Forms.MessageBoxButtons.YesNo);

                if (res == System.Windows.Forms.DialogResult.No)
                {
                    System.Windows.Forms.FormClosingEventArgs closeEvent = (System.Windows.Forms.FormClosingEventArgs)e;
                    closeEvent.Cancel = true;
                }
            }
        }
    }
}
