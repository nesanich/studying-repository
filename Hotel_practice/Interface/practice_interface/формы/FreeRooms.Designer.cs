namespace practice_interface.формы
{
    partial class FreeRooms
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
            this.dataGridViewFreeRooms = new System.Windows.Forms.DataGridView();
            this.buttonFindFreeRooms = new System.Windows.Forms.Button();
            this.dateTimePickerCheckIn = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerCheckOut = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFreeRooms)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewFreeRooms
            // 
            this.dataGridViewFreeRooms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFreeRooms.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewFreeRooms.Name = "dataGridViewFreeRooms";
            this.dataGridViewFreeRooms.RowHeadersWidth = 51;
            this.dataGridViewFreeRooms.RowTemplate.Height = 24;
            this.dataGridViewFreeRooms.Size = new System.Drawing.Size(499, 426);
            this.dataGridViewFreeRooms.TabIndex = 0;
            // 
            // buttonFindFreeRooms
            // 
            this.buttonFindFreeRooms.Location = new System.Drawing.Point(517, 209);
            this.buttonFindFreeRooms.Name = "buttonFindFreeRooms";
            this.buttonFindFreeRooms.Size = new System.Drawing.Size(277, 62);
            this.buttonFindFreeRooms.TabIndex = 1;
            this.buttonFindFreeRooms.Text = "Поиск";
            this.buttonFindFreeRooms.UseVisualStyleBackColor = true;
            this.buttonFindFreeRooms.Click += new System.EventHandler(this.buttonFindFreeRooms_Click);
            // 
            // dateTimePickerCheckIn
            // 
            this.dateTimePickerCheckIn.Location = new System.Drawing.Point(517, 85);
            this.dateTimePickerCheckIn.Name = "dateTimePickerCheckIn";
            this.dateTimePickerCheckIn.Size = new System.Drawing.Size(277, 22);
            this.dateTimePickerCheckIn.TabIndex = 2;
            // 
            // dateTimePickerCheckOut
            // 
            this.dateTimePickerCheckOut.Location = new System.Drawing.Point(517, 142);
            this.dateTimePickerCheckOut.Name = "dateTimePickerCheckOut";
            this.dateTimePickerCheckOut.Size = new System.Drawing.Size(277, 22);
            this.dateTimePickerCheckOut.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(517, 277);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(277, 62);
            this.button1.TabIndex = 4;
            this.button1.Text = "Выход";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FreeRooms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePickerCheckOut);
            this.Controls.Add(this.dateTimePickerCheckIn);
            this.Controls.Add(this.buttonFindFreeRooms);
            this.Controls.Add(this.dataGridViewFreeRooms);
            this.Name = "FreeRooms";
            this.Text = "FreeRooms";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFreeRooms)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewFreeRooms;
        private System.Windows.Forms.Button buttonFindFreeRooms;
        private System.Windows.Forms.DateTimePicker dateTimePickerCheckIn;
        private System.Windows.Forms.DateTimePicker dateTimePickerCheckOut;
        private System.Windows.Forms.Button button1;
    }
}