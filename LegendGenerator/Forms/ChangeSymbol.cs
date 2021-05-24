using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BBI.JD.Forms
{
    public partial class ChangeSymbol : Form
    {
        private Form parent;
        private int filtersCount;
        private DataGridViewComboBoxCell.ObjectCollection familySymbols;

        public ChangeSymbol(Form parent, int filtersCount, DataGridViewComboBoxCell.ObjectCollection familySymbols)
        {
            InitializeComponent();

            this.parent = parent;
            this.filtersCount = filtersCount;
            this.familySymbols = familySymbols;
        }

        private void ChangeSymbol_Load(object sender, EventArgs e)
        {
            txt_FiltersCount.Text = filtersCount.ToString();

            cmb_Symbols.Items.Clear();

            foreach (var item in familySymbols)
            {
                cmb_Symbols.Items.Add(item);
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            ((LegendGenerator)parent).grid_FiltersChangeSymbol(cmb_Symbols.Text);

            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
