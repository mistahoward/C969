
namespace C969
{
    partial class CustomersList
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
            this.CustomerDataGridView = new System.Windows.Forms.DataGridView();
            this.ViewCustomerButton = new System.Windows.Forms.Button();
            this.DeleteCustomerButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.activeInactiveToggle = new System.Windows.Forms.CheckBox();
            this.sortByLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CustomerDataGridView
            // 
            this.CustomerDataGridView.AllowUserToAddRows = false;
            this.CustomerDataGridView.AllowUserToDeleteRows = false;
            this.CustomerDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CustomerDataGridView.Location = new System.Drawing.Point(12, 84);
            this.CustomerDataGridView.MultiSelect = false;
            this.CustomerDataGridView.Name = "CustomerDataGridView";
            this.CustomerDataGridView.ReadOnly = true;
            this.CustomerDataGridView.RowHeadersWidth = 51;
            this.CustomerDataGridView.RowTemplate.Height = 24;
            this.CustomerDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CustomerDataGridView.ShowEditingIcon = false;
            this.CustomerDataGridView.Size = new System.Drawing.Size(437, 387);
            this.CustomerDataGridView.TabIndex = 0;
            this.CustomerDataGridView.SelectionChanged += new System.EventHandler(this.CustomerDataGridView_SelectionChanged);
            // 
            // ViewCustomerButton
            // 
            this.ViewCustomerButton.Location = new System.Drawing.Point(455, 84);
            this.ViewCustomerButton.Name = "ViewCustomerButton";
            this.ViewCustomerButton.Size = new System.Drawing.Size(161, 42);
            this.ViewCustomerButton.TabIndex = 1;
            this.ViewCustomerButton.Text = "View Customer";
            this.ViewCustomerButton.UseVisualStyleBackColor = true;
            this.ViewCustomerButton.Click += new System.EventHandler(this.ViewCustomerButton_Click);
            // 
            // DeleteCustomerButton
            // 
            this.DeleteCustomerButton.Location = new System.Drawing.Point(455, 132);
            this.DeleteCustomerButton.Name = "DeleteCustomerButton";
            this.DeleteCustomerButton.Size = new System.Drawing.Size(161, 42);
            this.DeleteCustomerButton.TabIndex = 2;
            this.DeleteCustomerButton.Text = "Archive Customer";
            this.DeleteCustomerButton.UseVisualStyleBackColor = true;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(455, 429);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(161, 42);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // activeInactiveToggle
            // 
            this.activeInactiveToggle.Appearance = System.Windows.Forms.Appearance.Button;
            this.activeInactiveToggle.AutoSize = true;
            this.activeInactiveToggle.Location = new System.Drawing.Point(12, 46);
            this.activeInactiveToggle.Name = "activeInactiveToggle";
            this.activeInactiveToggle.Size = new System.Drawing.Size(75, 26);
            this.activeInactiveToggle.TabIndex = 4;
            this.activeInactiveToggle.Text = "Loading...";
            this.activeInactiveToggle.UseVisualStyleBackColor = true;
            this.activeInactiveToggle.CheckedChanged += new System.EventHandler(this.activeInactiveToggle_CheckedChanged);
            // 
            // sortByLabel
            // 
            this.sortByLabel.AutoSize = true;
            this.sortByLabel.Location = new System.Drawing.Point(12, 27);
            this.sortByLabel.Name = "sortByLabel";
            this.sortByLabel.Size = new System.Drawing.Size(53, 16);
            this.sortByLabel.TabIndex = 5;
            this.sortByLabel.Text = "Sort By:";
            // 
            // CustomersList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 483);
            this.Controls.Add(this.sortByLabel);
            this.Controls.Add(this.activeInactiveToggle);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.DeleteCustomerButton);
            this.Controls.Add(this.ViewCustomerButton);
            this.Controls.Add(this.CustomerDataGridView);
            this.Name = "CustomersList";
            this.Text = "Customers";
            this.Activated += new System.EventHandler(this.CustomersList_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.CustomerDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView CustomerDataGridView;
        private System.Windows.Forms.Button ViewCustomerButton;
        private System.Windows.Forms.Button DeleteCustomerButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.CheckBox activeInactiveToggle;
        private System.Windows.Forms.Label sortByLabel;
    }
}