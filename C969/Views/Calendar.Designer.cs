
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
            this.weekMonthLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AppointmentDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // AppointmentDataGridView
            // 
            this.AppointmentDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AppointmentDataGridView.Location = new System.Drawing.Point(12, 61);
            this.AppointmentDataGridView.Name = "AppointmentDataGridView";
            this.AppointmentDataGridView.RowHeadersWidth = 51;
            this.AppointmentDataGridView.Size = new System.Drawing.Size(747, 384);
            this.AppointmentDataGridView.TabIndex = 0;
            // 
            // weekMonthComboBox
            // 
            this.weekMonthComboBox.FormattingEnabled = true;
            this.weekMonthComboBox.Location = new System.Drawing.Point(12, 35);
            this.weekMonthComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.weekMonthComboBox.Name = "weekMonthComboBox";
            this.weekMonthComboBox.Size = new System.Drawing.Size(153, 21);
            this.weekMonthComboBox.TabIndex = 1;
            // 
            // weekMonthLabel
            // 
            this.weekMonthLabel.AutoSize = true;
            this.weekMonthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.6F);
            this.weekMonthLabel.Location = new System.Drawing.Point(12, 15);
            this.weekMonthLabel.Name = "weekMonthLabel";
            this.weekMonthLabel.Size = new System.Drawing.Size(72, 18);
            this.weekMonthLabel.TabIndex = 2;
            this.weekMonthLabel.Text = "Loading...";
            // 
            // Calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 460);
            this.Controls.Add(this.weekMonthLabel);
            this.Controls.Add(this.weekMonthComboBox);
            this.Controls.Add(this.AppointmentDataGridView);
            this.Name = "Calendar";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.AppointmentDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView AppointmentDataGridView;
        private System.Windows.Forms.ComboBox weekMonthComboBox;
        private System.Windows.Forms.Label weekMonthLabel;
    }
}