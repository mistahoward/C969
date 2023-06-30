
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
            this.addCustomerButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CustomerDataGridView
            // 
            this.CustomerDataGridView.AllowUserToAddRows = false;
            this.CustomerDataGridView.AllowUserToDeleteRows = false;
            this.CustomerDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CustomerDataGridView.Location = new System.Drawing.Point(9, 68);
            this.CustomerDataGridView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CustomerDataGridView.MultiSelect = false;
            this.CustomerDataGridView.Name = "CustomerDataGridView";
            this.CustomerDataGridView.ReadOnly = true;
            this.CustomerDataGridView.RowHeadersWidth = 51;
            this.CustomerDataGridView.RowTemplate.Height = 24;
            this.CustomerDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CustomerDataGridView.ShowEditingIcon = false;
            this.CustomerDataGridView.Size = new System.Drawing.Size(328, 314);
            this.CustomerDataGridView.TabIndex = 0;
            this.CustomerDataGridView.SelectionChanged += new System.EventHandler(this.CustomerDataGridView_SelectionChanged);
            // 
            // ViewCustomerButton
            // 
            this.ViewCustomerButton.Location = new System.Drawing.Point(341, 106);
            this.ViewCustomerButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ViewCustomerButton.Name = "ViewCustomerButton";
            this.ViewCustomerButton.Size = new System.Drawing.Size(121, 34);
            this.ViewCustomerButton.TabIndex = 1;
            this.ViewCustomerButton.Text = "View Customer";
            this.ViewCustomerButton.UseVisualStyleBackColor = true;
            this.ViewCustomerButton.Click += new System.EventHandler(this.ViewCustomerButton_Click);
            // 
            // DeleteCustomerButton
            // 
            this.DeleteCustomerButton.Location = new System.Drawing.Point(341, 144);
            this.DeleteCustomerButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DeleteCustomerButton.Name = "DeleteCustomerButton";
            this.DeleteCustomerButton.Size = new System.Drawing.Size(121, 34);
            this.DeleteCustomerButton.TabIndex = 2;
            this.DeleteCustomerButton.Text = "Archive Customer";
            this.DeleteCustomerButton.UseVisualStyleBackColor = true;
            this.DeleteCustomerButton.Click += new System.EventHandler(this.DeleteCustomerButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(341, 349);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(121, 34);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // activeInactiveToggle
            // 
            this.activeInactiveToggle.Appearance = System.Windows.Forms.Appearance.Button;
            this.activeInactiveToggle.AutoSize = true;
            this.activeInactiveToggle.Location = new System.Drawing.Point(9, 37);
            this.activeInactiveToggle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.activeInactiveToggle.Name = "activeInactiveToggle";
            this.activeInactiveToggle.Size = new System.Drawing.Size(64, 23);
            this.activeInactiveToggle.TabIndex = 4;
            this.activeInactiveToggle.Text = "Loading...";
            this.activeInactiveToggle.UseVisualStyleBackColor = true;
            this.activeInactiveToggle.CheckedChanged += new System.EventHandler(this.activeInactiveToggle_CheckedChanged);
            // 
            // sortByLabel
            // 
            this.sortByLabel.AutoSize = true;
            this.sortByLabel.Location = new System.Drawing.Point(9, 22);
            this.sortByLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.sortByLabel.Name = "sortByLabel";
            this.sortByLabel.Size = new System.Drawing.Size(44, 13);
            this.sortByLabel.TabIndex = 5;
            this.sortByLabel.Text = "Sort By:";
            // 
            // addCustomerButton
            // 
            this.addCustomerButton.Location = new System.Drawing.Point(341, 68);
            this.addCustomerButton.Margin = new System.Windows.Forms.Padding(2);
            this.addCustomerButton.Name = "addCustomerButton";
            this.addCustomerButton.Size = new System.Drawing.Size(121, 34);
            this.addCustomerButton.TabIndex = 6;
            this.addCustomerButton.Text = "Add Customer";
            this.addCustomerButton.UseVisualStyleBackColor = true;
            this.addCustomerButton.Click += new System.EventHandler(this.addCustomerButton_Click);
            // 
            // CustomersList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 392);
            this.Controls.Add(this.addCustomerButton);
            this.Controls.Add(this.sortByLabel);
            this.Controls.Add(this.activeInactiveToggle);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.DeleteCustomerButton);
            this.Controls.Add(this.ViewCustomerButton);
            this.Controls.Add(this.CustomerDataGridView);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
        private System.Windows.Forms.Button addCustomerButton;
    }
}