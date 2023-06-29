
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
            this.weekMonthToggle = new System.Windows.Forms.CheckBox();
            this.filtersLabel = new System.Windows.Forms.Label();
            this.customersButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AppointmentDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // AppointmentDataGridView
            // 
            this.AppointmentDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AppointmentDataGridView.Location = new System.Drawing.Point(16, 75);
            this.AppointmentDataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.AppointmentDataGridView.Name = "AppointmentDataGridView";
            this.AppointmentDataGridView.RowHeadersWidth = 51;
            this.AppointmentDataGridView.Size = new System.Drawing.Size(996, 473);
            this.AppointmentDataGridView.TabIndex = 0;
            // 
            // weekMonthComboBox
            // 
            this.weekMonthComboBox.FormattingEnabled = true;
            this.weekMonthComboBox.Location = new System.Drawing.Point(16, 43);
            this.weekMonthComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.weekMonthComboBox.Name = "weekMonthComboBox";
            this.weekMonthComboBox.Size = new System.Drawing.Size(203, 24);
            this.weekMonthComboBox.TabIndex = 1;
            this.weekMonthComboBox.SelectedIndexChanged += new System.EventHandler(this.weekMonthComboBox_SelectedIndexChanged);
            // 
            // weekMonthLabel
            // 
            this.weekMonthLabel.AutoSize = true;
            this.weekMonthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.6F);
            this.weekMonthLabel.Location = new System.Drawing.Point(16, 18);
            this.weekMonthLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.weekMonthLabel.Name = "weekMonthLabel";
            this.weekMonthLabel.Size = new System.Drawing.Size(72, 18);
            this.weekMonthLabel.TabIndex = 2;
            this.weekMonthLabel.Text = "Loading...";
            // 
            // weekMonthToggle
            // 
            this.weekMonthToggle.Appearance = System.Windows.Forms.Appearance.Button;
            this.weekMonthToggle.AutoSize = true;
            this.weekMonthToggle.Location = new System.Drawing.Point(249, 39);
            this.weekMonthToggle.Margin = new System.Windows.Forms.Padding(4);
            this.weekMonthToggle.Name = "weekMonthToggle";
            this.weekMonthToggle.Size = new System.Drawing.Size(75, 26);
            this.weekMonthToggle.TabIndex = 3;
            this.weekMonthToggle.Text = "Loading...";
            this.weekMonthToggle.UseVisualStyleBackColor = true;
            this.weekMonthToggle.CheckedChanged += new System.EventHandler(this.weekMonthToggle_CheckedChanged);
            // 
            // filtersLabel
            // 
            this.filtersLabel.AutoSize = true;
            this.filtersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.6F);
            this.filtersLabel.Location = new System.Drawing.Point(245, 18);
            this.filtersLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.filtersLabel.Name = "filtersLabel";
            this.filtersLabel.Size = new System.Drawing.Size(48, 18);
            this.filtersLabel.TabIndex = 4;
            this.filtersLabel.Text = "Filters";
            // 
            // customersButton
            // 
            this.customersButton.Location = new System.Drawing.Point(1032, 75);
            this.customersButton.Name = "customersButton";
            this.customersButton.Size = new System.Drawing.Size(141, 37);
            this.customersButton.TabIndex = 5;
            this.customersButton.Text = "View Customers";
            this.customersButton.UseVisualStyleBackColor = true;
            this.customersButton.Click += new System.EventHandler(this.customersButton_Click);
            // 
            // Calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 566);
            this.Controls.Add(this.customersButton);
            this.Controls.Add(this.filtersLabel);
            this.Controls.Add(this.weekMonthToggle);
            this.Controls.Add(this.weekMonthLabel);
            this.Controls.Add(this.weekMonthComboBox);
            this.Controls.Add(this.AppointmentDataGridView);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Calendar";
            this.Text = "Appointments";
            ((System.ComponentModel.ISupportInitialize)(this.AppointmentDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView AppointmentDataGridView;
        private System.Windows.Forms.ComboBox weekMonthComboBox;
        private System.Windows.Forms.Label weekMonthLabel;
        private System.Windows.Forms.CheckBox weekMonthToggle;
        private System.Windows.Forms.Label filtersLabel;
        private System.Windows.Forms.Button customersButton;
    }
}