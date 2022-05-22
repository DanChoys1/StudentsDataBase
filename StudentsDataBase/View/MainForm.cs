using System;
using System.Windows.Forms;
using Presenter;
using StudentsDataBaseProject.Properties;
using Containers;
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
        public event EventHandler SaveDataToDbEvent;
        public event EventHandler ViewClosingEvent;

        public MainForm(IInteractionInformation interactionInformation)
        {
            InitializeComponent();

            PresenterData presenter = new PresenterData(this, interactionInformation);
            //PresenterData presenter = new PresenterData(this, new InteractionInformation()); // Мы не ссылаемся на презентер, почему он не очищается

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

        private void saveDataToDbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDataToDbEvent.Invoke(sender, e);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ViewClosingEvent.Invoke(sender, e);
        }
    }
}
