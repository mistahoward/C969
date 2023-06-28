
namespace C969
{
    partial class Calendar
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
            this.AppointmentDataGridView = new System.Windows.Forms.DataGridView();
            this.weekMonthComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.AppointmentDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // AppointmentDataGridView
            // 
            this.AppointmentDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AppointmentDataGridView.Location = new System.Drawing.Point(16, 108);
            this.AppointmentDataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AppointmentDataGridView.Name = "AppointmentDataGridView";
            this.AppointmentDataGridView.RowHeadersWidth = 51;
            this.AppointmentDataGridView.Size = new System.Drawing.Size(996, 473);
            this.AppointmentDataGridView.TabIndex = 0;
            // 
            // weekMonthComboBox
            // 
            this.weekMonthComboBox.FormattingEnabled = true;
            this.weekMonthComboBox.Location = new System.Drawing.Point(16, 57);
            this.weekMonthComboBox.Name = "weekMonthComboBox";
            this.weekMonthComboBox.Size = new System.Drawing.Size(203, 24);
            this.weekMonthComboBox.TabIndex = 1;
            // 
            // Calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 596);
            this.Controls.Add(this.weekMonthComboBox);
            this.Controls.Add(this.AppointmentDataGridView);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Calendar";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.AppointmentDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView AppointmentDataGridView;
        private System.Windows.Forms.ComboBox weekMonthComboBox;
    }
}