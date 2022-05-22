namespace UI
{
    partial class InteractionInformation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.surnameСomboBox = new System.Windows.Forms.ComboBox();
            this.nameComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.middleNameComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.courseComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.formComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Фамилия";
            // 
            // surnameСomboBox
            // 
            this.surnameСomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.surnameСomboBox.FormattingEnabled = true;
            this.surnameСomboBox.Location = new System.Drawing.Point(120, 46);
            this.surnameСomboBox.Name = "surnameСomboBox";
            this.surnameСomboBox.Size = new System.Drawing.Size(261, 28);
            this.surnameСomboBox.TabIndex = 1;
            // 
            // nameComboBox
            // 
            this.nameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.nameComboBox.FormattingEnabled = true;
            this.nameComboBox.Location = new System.Drawing.Point(120, 12);
            this.nameComboBox.Name = "nameComboBox";
            this.nameComboBox.Size = new System.Drawing.Size(261, 28);
            this.nameComboBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Имя";
            // 
            // middleNameComboBox
            // 
            this.middleNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.middleNameComboBox.FormattingEnabled = true;
            this.middleNameComboBox.Location = new System.Drawing.Point(120, 80);
            this.middleNameComboBox.Name = "middleNameComboBox";
            this.middleNameComboBox.Size = new System.Drawing.Size(261, 28);
            this.middleNameComboBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Отчество";
            // 
            // courseComboBox
            // 
            this.courseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.courseComboBox.FormattingEnabled = true;
            this.courseComboBox.Location = new System.Drawing.Point(120, 114);
            this.courseComboBox.Name = "courseComboBox";
            this.courseComboBox.Size = new System.Drawing.Size(261, 28);
            this.courseComboBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Курс";
            // 
            // groupComboBox
            // 
            this.groupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.groupComboBox.FormattingEnabled = true;
            this.groupComboBox.Location = new System.Drawing.Point(120, 148);
            this.groupComboBox.Name = "groupComboBox";
            this.groupComboBox.Size = new System.Drawing.Size(261, 28);
            this.groupComboBox.TabIndex = 9;
            this.groupComboBox.TextUpdate += new System.EventHandler(this.groupComboBox_TextUpdate);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Группа";
            // 
            // formComboBox
            // 
            this.formComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formComboBox.FormattingEnabled = true;
            this.formComboBox.Location = new System.Drawing.Point(120, 187);
            this.formComboBox.Name = "formComboBox";
            this.formComboBox.Size = new System.Drawing.Size(261, 28);
            this.formComboBox.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 40);
            this.label6.TabIndex = 10;
            this.label6.Text = "Форма\r\nобучения";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(204, 284);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(94, 29);
            this.cancelButton.TabIndex = 12;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(304, 284);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(94, 29);
            this.nextButton.TabIndex = 13;
            this.nextButton.Text = "Далее";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // InteractionInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 325);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.formComboBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.courseComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.middleNameComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nameComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.surnameСomboBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InteractionInformation";
            this.Text = "Студент";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox surnameСomboBox;
        private System.Windows.Forms.ComboBox nameComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox middleNameComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox courseComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox groupComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox formComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button nextButton;
    }
}