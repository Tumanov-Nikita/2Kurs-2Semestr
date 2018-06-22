namespace FabricView
{
    partial class FormTakeBookingInWork
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
            this.comboBoxExecuter = new System.Windows.Forms.ComboBox();
            this.labelExecuter = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxExecuter
            // 
            this.comboBoxExecuter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExecuter.FormattingEnabled = true;
            this.comboBoxExecuter.Location = new System.Drawing.Point(95, 6);
            this.comboBoxExecuter.Name = "comboBoxExecuter";
            this.comboBoxExecuter.Size = new System.Drawing.Size(217, 21);
            this.comboBoxExecuter.TabIndex = 1;
            // 
            // labelExecuter
            // 
            this.labelExecuter.AutoSize = true;
            this.labelExecuter.Location = new System.Drawing.Point(12, 9);
            this.labelExecuter.Name = "labelExecuter";
            this.labelExecuter.Size = new System.Drawing.Size(77, 13);
            this.labelExecuter.TabIndex = 0;
            this.labelExecuter.Text = "Исполнитель:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(237, 42);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(95, 40);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FormTakeBookingInWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 75);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.comboBoxExecuter);
            this.Controls.Add(this.labelExecuter);
            this.Name = "FormTakeBookingInWork";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Приступить к производству";
            this.Load += new System.EventHandler(this.FormTakeBookingInWork_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxExecuter;
        private System.Windows.Forms.Label labelExecuter;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
    }
}