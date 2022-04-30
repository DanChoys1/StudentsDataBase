using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Containers;
using System.Windows.Forms;
using Presenter;
using StudentsDataBaseProject.Properties;
using Autofac;

namespace UI
{
    public partial class MainForm : Form, IMainForm
    {
        private readonly AboutForm _aboutForm;

        public DataGridView DataGridView 
        { 
            get { return studentsDataGridView; }
        }

        public SaveFileDialog SaveFileDialog 
        {
            get { return saveFileDialog; }
        }

        public event EventHandler DeleteEvent;
        public event EventHandler EditEvent;
        public event EventHandler AddEvent;
        public event EventHandler UpdateEvent;
        public event EventHandler SaveDataEvent;

        public MainForm()
        {
            InitializeComponent();

            saveFileDialog.Filter = "Text files(*.txt)|*.txt|Excel files(*.xlsx)| *.xlsx| All files(*.*) | *.*";

            studentsDataGridView.ColumnCount = 4;
            studentsDataGridView.Columns[0].HeaderText = "ФИО";
            studentsDataGridView.Columns[1].HeaderText = "Курс";
            studentsDataGridView.Columns[2].HeaderText = "Группа";
            studentsDataGridView.Columns[3].HeaderText = "Форма обучения";

            studentsDataGridView.Columns[1].Width = 100;
            studentsDataGridView.Columns[2].Width = 100;
            studentsDataGridView.Columns[3].Width = 100;

            //PresenterData presenter = new PresenterData(this, ContainerRegistretion.Container.Resolve<IInteractionInformation>()); //Почему Reso;ve работает только при подключенной Autofac?
            PresenterData presenter = new PresenterData(this, new InteractionInformation());

            _aboutForm = new AboutForm();

            if (Settings.Default.isShowAboutMenu)
            {
                _aboutForm.Show();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (studentsDataGridView.SelectedRows.Count < 1)
            {
                MessageBox.Show("Выделите строку.", "Сообщение");
                return;
            }

            DialogResult res = MessageBox.Show("Вы уверены, что хотите удалить строку?", "Сообщение", MessageBoxButtons.YesNo);

            if (res == DialogResult.Yes)
            {
                DeleteEvent.Invoke(sender, e);
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (studentsDataGridView.SelectedRows.Count < 1)
            {
                MessageBox.Show("Выделите строку.", "Сообщение");
                return;
            }

            EditEvent.Invoke(sender, e);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddEvent.Invoke(sender, e);
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateEvent.Invoke(sender, e);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _aboutForm.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDataEvent.Invoke(sender, e);
        }
    }
}
