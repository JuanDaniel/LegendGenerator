namespace BBI.JD.Forms
{
    partial class LegendGenerator
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LegendGenerator));
            this.grid_Filters = new System.Windows.Forms.DataGridView();
            this.cId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cColor = new System.Windows.Forms.DataGridViewImageColumn();
            this.cDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSymbol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_CheckAllView = new System.Windows.Forms.Button();
            this.btn_CheckNoneView = new System.Windows.Forms.Button();
            this.lst_Views = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_FiltersCount = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_CheckAllCategory = new System.Windows.Forms.Button();
            this.btn_CheckNoneCategory = new System.Windows.Forms.Button();
            this.lst_Categories = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Generate = new System.Windows.Forms.Button();
            this.cmb_Legends = new System.Windows.Forms.ComboBox();
            this.btn_Config = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Filters)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid_Filters
            // 
            this.grid_Filters.AllowUserToAddRows = false;
            this.grid_Filters.AllowUserToDeleteRows = false;
            this.grid_Filters.AllowUserToOrderColumns = true;
            this.grid_Filters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_Filters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grid_Filters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_Filters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cId,
            this.cActive,
            this.cName,
            this.cColor,
            this.cDescription,
            this.cSymbol});
            this.grid_Filters.Location = new System.Drawing.Point(280, 47);
            this.grid_Filters.Name = "grid_Filters";
            this.grid_Filters.Size = new System.Drawing.Size(770, 645);
            this.grid_Filters.TabIndex = 0;
            this.grid_Filters.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grid_Filters_MouseDown);
            // 
            // cId
            // 
            this.cId.HeaderText = "Id";
            this.cId.Name = "cId";
            this.cId.Visible = false;
            // 
            // cActive
            // 
            this.cActive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cActive.FillWeight = 133.8493F;
            this.cActive.HeaderText = "Active";
            this.cActive.Name = "cActive";
            this.cActive.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cActive.Width = 45;
            // 
            // cName
            // 
            this.cName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cName.FillWeight = 153.5721F;
            this.cName.HeaderText = "Name";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            this.cName.Width = 230;
            // 
            // cColor
            // 
            this.cColor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cColor.FillWeight = 46.07162F;
            this.cColor.HeaderText = "Color";
            this.cColor.Name = "cColor";
            this.cColor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cColor.Width = 70;
            // 
            // cDescription
            // 
            this.cDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cDescription.FillWeight = 153.5721F;
            this.cDescription.HeaderText = "Legend description";
            this.cDescription.Name = "cDescription";
            this.cDescription.Width = 240;
            // 
            // cSymbol
            // 
            this.cSymbol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cSymbol.FillWeight = 98.89762F;
            this.cSymbol.HeaderText = "Legend symbol";
            this.cSymbol.Name = "cSymbol";
            this.cSymbol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cSymbol.Width = 142;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(277, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Filters";
            // 
            // btn_CheckAllView
            // 
            this.btn_CheckAllView.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btn_CheckAllView.Location = new System.Drawing.Point(47, 323);
            this.btn_CheckAllView.Name = "btn_CheckAllView";
            this.btn_CheckAllView.Size = new System.Drawing.Size(75, 23);
            this.btn_CheckAllView.TabIndex = 5;
            this.btn_CheckAllView.Text = "Check All";
            this.btn_CheckAllView.UseVisualStyleBackColor = true;
            this.btn_CheckAllView.Click += new System.EventHandler(this.btn_CheckAllView_Click);
            // 
            // btn_CheckNoneView
            // 
            this.btn_CheckNoneView.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btn_CheckNoneView.Location = new System.Drawing.Point(128, 323);
            this.btn_CheckNoneView.Name = "btn_CheckNoneView";
            this.btn_CheckNoneView.Size = new System.Drawing.Size(83, 23);
            this.btn_CheckNoneView.TabIndex = 6;
            this.btn_CheckNoneView.Text = "Check None";
            this.btn_CheckNoneView.UseVisualStyleBackColor = true;
            this.btn_CheckNoneView.Click += new System.EventHandler(this.btn_CheckNoneView_Click);
            // 
            // lst_Views
            // 
            this.lst_Views.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lst_Views.FormattingEnabled = true;
            this.lst_Views.HorizontalScrollbar = true;
            this.lst_Views.Location = new System.Drawing.Point(13, 25);
            this.lst_Views.Name = "lst_Views";
            this.lst_Views.Size = new System.Drawing.Size(235, 293);
            this.lst_Views.TabIndex = 8;
            this.lst_Views.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lst_Views_ItemCheck);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(775, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 14);
            this.label3.TabIndex = 9;
            this.label3.Text = "Unique filters shown for the selected views:";
            // 
            // txt_FiltersCount
            // 
            this.txt_FiltersCount.AutoSize = true;
            this.txt_FiltersCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_FiltersCount.Location = new System.Drawing.Point(1029, 18);
            this.txt_FiltersCount.Name = "txt_FiltersCount";
            this.txt_FiltersCount.Size = new System.Drawing.Size(16, 14);
            this.txt_FiltersCount.TabIndex = 10;
            this.txt_FiltersCount.Text = "#";
            this.txt_FiltersCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_CheckAllView);
            this.groupBox1.Controls.Add(this.btn_CheckNoneView);
            this.groupBox1.Controls.Add(this.lst_Views);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(14, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 355);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Views";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_CheckAllCategory);
            this.groupBox2.Controls.Add(this.btn_CheckNoneCategory);
            this.groupBox2.Controls.Add(this.lst_Categories);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(14, 388);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(259, 355);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Categories";
            // 
            // btn_CheckAllCategory
            // 
            this.btn_CheckAllCategory.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btn_CheckAllCategory.Location = new System.Drawing.Point(47, 323);
            this.btn_CheckAllCategory.Name = "btn_CheckAllCategory";
            this.btn_CheckAllCategory.Size = new System.Drawing.Size(75, 23);
            this.btn_CheckAllCategory.TabIndex = 5;
            this.btn_CheckAllCategory.Text = "Check All";
            this.btn_CheckAllCategory.UseVisualStyleBackColor = true;
            this.btn_CheckAllCategory.Click += new System.EventHandler(this.btn_CheckAllCategory_Click);
            // 
            // btn_CheckNoneCategory
            // 
            this.btn_CheckNoneCategory.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btn_CheckNoneCategory.Location = new System.Drawing.Point(128, 323);
            this.btn_CheckNoneCategory.Name = "btn_CheckNoneCategory";
            this.btn_CheckNoneCategory.Size = new System.Drawing.Size(85, 23);
            this.btn_CheckNoneCategory.TabIndex = 6;
            this.btn_CheckNoneCategory.Text = "Check None";
            this.btn_CheckNoneCategory.UseVisualStyleBackColor = true;
            this.btn_CheckNoneCategory.Click += new System.EventHandler(this.btn_CheckNoneCategory_Click);
            // 
            // lst_Categories
            // 
            this.lst_Categories.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lst_Categories.FormattingEnabled = true;
            this.lst_Categories.HorizontalScrollbar = true;
            this.lst_Categories.Location = new System.Drawing.Point(13, 25);
            this.lst_Categories.Name = "lst_Categories";
            this.lst_Categories.Size = new System.Drawing.Size(235, 293);
            this.lst_Categories.TabIndex = 8;
            this.lst_Categories.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lst_Categories_ItemCheck);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(279, 699);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 14);
            this.label1.TabIndex = 14;
            this.label1.Text = "Legend view";
            // 
            // btn_Generate
            // 
            this.btn_Generate.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btn_Generate.Location = new System.Drawing.Point(967, 710);
            this.btn_Generate.Name = "btn_Generate";
            this.btn_Generate.Size = new System.Drawing.Size(83, 31);
            this.btn_Generate.TabIndex = 15;
            this.btn_Generate.Text = "Generate";
            this.btn_Generate.UseVisualStyleBackColor = true;
            this.btn_Generate.Click += new System.EventHandler(this.btn_Generate_Click);
            // 
            // cmb_Legends
            // 
            this.cmb_Legends.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Legends.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmb_Legends.FormattingEnabled = true;
            this.cmb_Legends.Location = new System.Drawing.Point(282, 720);
            this.cmb_Legends.Name = "cmb_Legends";
            this.cmb_Legends.Size = new System.Drawing.Size(200, 22);
            this.cmb_Legends.TabIndex = 16;
            // 
            // btn_Config
            // 
            this.btn_Config.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btn_Config.Location = new System.Drawing.Point(488, 718);
            this.btn_Config.Name = "btn_Config";
            this.btn_Config.Size = new System.Drawing.Size(90, 23);
            this.btn_Config.TabIndex = 17;
            this.btn_Config.Text = "Configuration";
            this.btn_Config.UseVisualStyleBackColor = true;
            this.btn_Config.Click += new System.EventHandler(this.btn_Config_Click);
            // 
            // LegendGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 757);
            this.Controls.Add(this.btn_Config);
            this.Controls.Add(this.cmb_Legends);
            this.Controls.Add(this.btn_Generate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_FiltersCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grid_Filters);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1078, 796);
            this.MinimumSize = new System.Drawing.Size(1078, 796);
            this.Name = "LegendGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LegendGenerator"; //GetTiTleForm();
            this.Load += new System.EventHandler(this.LegendGenerator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid_Filters)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grid_Filters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_CheckAllView;
        private System.Windows.Forms.Button btn_CheckNoneView;
        private System.Windows.Forms.CheckedListBox lst_Views;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txt_FiltersCount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_CheckAllCategory;
        private System.Windows.Forms.Button btn_CheckNoneCategory;
        private System.Windows.Forms.CheckedListBox lst_Categories;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Generate;
        private System.Windows.Forms.ComboBox cmb_Legends;
        private System.Windows.Forms.DataGridViewTextBoxColumn cId;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewImageColumn cColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDescription;
        private System.Windows.Forms.DataGridViewComboBoxColumn cSymbol;
        private System.Windows.Forms.Button btn_Config;
    }
}