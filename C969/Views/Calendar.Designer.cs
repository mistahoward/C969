
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
            this.addAppointmentButton = new System.Windows.Forms.Button();
            this.deleteAppointmentButton = new System.Windows.Forms.Button();
            this.viewAppointmentButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AppointmentDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // AppointmentDataGridView
            // 
            this.AppointmentDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AppointmentDataGridView.Location = new System.Drawing.Point(12, 61);
            this.AppointmentDataGridView.MultiSelect = false;
            this.AppointmentDataGridView.Name = "AppointmentDataGridView";
            this.AppointmentDataGridView.ReadOnly = true;
            this.AppointmentDataGridView.RowHeadersWidth = 51;
            this.AppointmentDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AppointmentDataGridView.ShowEditingIcon = false;
            this.AppointmentDataGridView.Size = new System.Drawing.Size(747, 384);
            this.AppointmentDataGridView.TabIndex = 0;
            this.AppointmentDataGridView.SelectionChanged += new System.EventHandler(this.AppointmentDataGridView_SelectionChanged);
            // 
            // weekMonthComboBox
            // 
            this.weekMonthComboBox.FormattingEnabled = true;
            this.weekMonthComboBox.Location = new System.Drawing.Point(12, 35);
            this.weekMonthComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.weekMonthComboBox.Name = "weekMonthComboBox";
            this.weekMonthComboBox.Size = new System.Drawing.Size(153, 21);
            this.weekMonthComboBox.TabIndex = 1;
            this.weekMonthComboBox.SelectedIndexChanged += new System.EventHandler(this.weekMonthComboBox_SelectedIndexChanged);
            // 
            // weekMonthLabel
            // 
            this.weekMonthLabel.AutoSize = true;
            this.weekMonthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.6F);
            this.weekMonthLabel.Location = new System.Drawing.Point(12, 15);
            this.weekMonthLabel.Name = "weekMonthLabel";
            this.weekMonthLabel.Size = new System.Drawing.Size(61, 15);
            this.weekMonthLabel.TabIndex = 2;
            this.weekMonthLabel.Text = "Loading...";
            // 
            // weekMonthToggle
            // 
            this.weekMonthToggle.Appearance = System.Windows.Forms.Appearance.Button;
            this.weekMonthToggle.AutoSize = true;
            this.weekMonthToggle.Location = new System.Drawing.Point(187, 32);
            this.weekMonthToggle.Name = "weekMonthToggle";
            this.weekMonthToggle.Size = new System.Drawing.Size(64, 23);
            this.weekMonthToggle.TabIndex = 3;
            this.weekMonthToggle.Text = "Loading...";
            this.weekMonthToggle.UseVisualStyleBackColor = true;
            this.weekMonthToggle.CheckedChanged += new System.EventHandler(this.weekMonthToggle_CheckedChanged);
            // 
            // filtersLabel
            // 
            this.filtersLabel.AutoSize = true;
            this.filtersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.6F);
            this.filtersLabel.Location = new System.Drawing.Point(184, 15);
            this.filtersLabel.Name = "filtersLabel";
            this.filtersLabel.Size = new System.Drawing.Size(40, 15);
            this.filtersLabel.TabIndex = 4;
            this.filtersLabel.Text = "Filters";
            // 
            // customersButton
            // 
            this.customersButton.Location = new System.Drawing.Point(764, 163);
            this.customersButton.Margin = new System.Windows.Forms.Padding(2);
            this.customersButton.Name = "customersButton";
            this.customersButton.Size = new System.Drawing.Size(118, 30);
            this.customersButton.TabIndex = 5;
            this.customersButton.Text = "View Customers";
            this.customersButton.UseVisualStyleBackColor = true;
            this.customersButton.Click += new System.EventHandler(this.customersButton_Click);
            // 
            // addAppointmentButton
            // 
            this.addAppointmentButton.Location = new System.Drawing.Point(764, 61);
            this.addAppointmentButton.Margin = new System.Windows.Forms.Padding(2);
            this.addAppointmentButton.Name = "addAppointmentButton";
            this.addAppointmentButton.Size = new System.Drawing.Size(118, 30);
            this.addAppointmentButton.TabIndex = 6;
            this.addAppointmentButton.Text = "Add Appointment";
            this.addAppointmentButton.UseVisualStyleBackColor = true;
            this.addAppointmentButton.Click += new System.EventHandler(this.addAppointmentButton_Click);
            // 
            // deleteAppointmentButton
            // 
            this.deleteAppointmentButton.Location = new System.Drawing.Point(764, 129);
            this.deleteAppointmentButton.Margin = new System.Windows.Forms.Padding(2);
            this.deleteAppointmentButton.Name = "deleteAppointmentButton";
            this.deleteAppointmentButton.Size = new System.Drawing.Size(118, 30);
            this.deleteAppointmentButton.TabIndex = 7;
            this.deleteAppointmentButton.Text = "Delete Appointment";
            this.deleteAppointmentButton.UseVisualStyleBackColor = true;
            // 
            // viewAppointmentButton
            // 
            this.viewAppointmentButton.Location = new System.Drawing.Point(764, 95);
            this.viewAppointmentButton.Margin = new System.Windows.Forms.Padding(2);
            this.viewAppointmentButton.Name = "viewAppointmentButton";
            this.viewAppointmentButton.Size = new System.Drawing.Size(118, 30);
            this.viewAppointmentButton.TabIndex = 8;
            this.viewAppointmentButton.Text = "View Appointment";
            this.viewAppointmentButton.UseVisualStyleBackColor = true;
            this.viewAppointmentButton.Click += new System.EventHandler(this.viewAppointmentButton_Click);
            // 
            // Calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 460);
            this.Controls.Add(this.viewAppointmentButton);
            this.Controls.Add(this.deleteAppointmentButton);
            this.Controls.Add(this.addAppointmentButton);
            this.Controls.Add(this.customersButton);
            this.Controls.Add(this.filtersLabel);
            this.Controls.Add(this.weekMonthToggle);
            this.Controls.Add(this.weekMonthLabel);
            this.Controls.Add(this.weekMonthComboBox);
            this.Controls.Add(this.AppointmentDataGridView);
            this.Name = "Calendar";
            this.Text = "Appointments";
            this.Activated += new System.EventHandler(this.Calendar_Activated);
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
        private System.Windows.Forms.Button addAppointmentButton;
        private System.Windows.Forms.Button deleteAppointmentButton;
        private System.Windows.Forms.Button viewAppointmentButton;
    }
}