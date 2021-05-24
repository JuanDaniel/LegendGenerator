namespace BBI.JD.Forms
{
    partial class ChangeSymbol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeSymbol));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_FiltersCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.cmb_Symbols = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Symbol";
            // 
            // txt_FiltersCount
            // 
            this.txt_FiltersCount.AutoSize = true;
            this.txt_FiltersCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_FiltersCount.Location = new System.Drawing.Point(263, 22);
            this.txt_FiltersCount.Name = "txt_FiltersCount";
            this.txt_FiltersCount.Size = new System.Drawing.Size(16, 14);
            this.txt_FiltersCount.TabIndex = 12;
            this.txt_FiltersCount.Text = "#";
            this.txt_FiltersCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(221, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 11;
            this.label3.Text = "Filters:";
            // 
            // btn_Ok
            // 
            this.btn_Ok.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btn_Ok.Location = new System.Drawing.Point(123, 102);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 2;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btn_Cancel.Location = new System.Drawing.Point(204, 102);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // cmb_Symbols
            // 
            this.cmb_Symbols.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Symbols.FormattingEnabled = true;
            this.cmb_Symbols.Location = new System.Drawing.Point(15, 52);
            this.cmb_Symbols.Name = "cmb_Symbols";
            this.cmb_Symbols.Size = new System.Drawing.Size(264, 21);
            this.cmb_Symbols.TabIndex = 1;
            // 
            // ChangeSymbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 137);
            this.Controls.Add(this.cmb_Symbols);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.txt_FiltersCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(314, 176);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(314, 176);
            this.Name = "ChangeSymbol";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change legend symbol";
            this.Load += new System.EventHandler(this.ChangeSymbol_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txt_FiltersCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ComboBox cmb_Symbols;
    }
}