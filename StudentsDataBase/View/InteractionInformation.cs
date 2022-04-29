using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class InteractionInformation : Form, IInteractionInformation
    {
        public string StudentName { get; private set; }
        public string Course { get; private set; }
        public string Group { get; private set; }
        public string Form { get; private set; }

        public InteractionInformation()
        {
            InitializeComponent();

            courseComboBox.Items.Add("1");
            courseComboBox.Items.Add("2");
            courseComboBox.Items.Add("3");
            courseComboBox.Items.Add("4");
            courseComboBox.Items.Add("5");
            courseComboBox.SelectedIndex = 0;

            formComboBox.Items.Add("Очная");
            formComboBox.Items.Add("Заочная");
            formComboBox.Items.Add("Очно-заочная");
            formComboBox.SelectedIndex = 0;
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            StudentName = nameComboBox.Text + " " + surnameСomboBox.Text + " " + middleNameComboBox.Text;
            Course = courseComboBox.Text;
            Form = formComboBox.Text;

            if (int.TryParse(groupComboBox.Text, out int group))
            {
                Group = groupComboBox.Text;
            }
            else
            {
                MessageBox.Show("Введите группу.", "Нет данных");
                return;
            }

            DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Hide();
        }

        public DialogResult ShowInteractionInformation()
        {
            return this.ShowDialog();
        }

        public DialogResult ShowInteractionInformation(string name, int course, int group, string form)
        {
            string[] splitName = name.Split(" ");

            try
            {
                nameComboBox.Text = splitName[0];
                surnameСomboBox.Text = splitName[1];
                middleNameComboBox.Text = splitName[2];
            }
            catch (IndexOutOfRangeException )
            {

            }
            courseComboBox.Text = course.ToString();
            groupComboBox.Text = group.ToString();
            formComboBox.Text = form;

            return this.ShowDialog();
        }

        private void groupComboBox_TextUpdate(object sender, EventArgs e)
        {
            for (int i = 0; i < groupComboBox.Text.Length; i++)
            {
                if (!char.IsDigit(groupComboBox.Text[i]))
                {
                    groupComboBox.Text = groupComboBox.Text.Remove(i, 1);
                    i--;

                    groupComboBox.SelectionStart = i + 1;
                }
            }
        }
    }
}
