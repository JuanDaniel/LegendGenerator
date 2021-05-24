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
    public partial class ConfigDescription : Form
    {
        private Form parent;
        private bool save;

        public ConfigDescription(Form parent)
        {
            InitializeComponent();

            this.parent = parent;

            save = false;
        }

        private void ConfigDescription_Load(object sender, EventArgs e)
        {
            cmb_Type1.SelectedItem = Config.Get("parameterType1");
            txt_Parameter1.Text = Config.Get("parameterName1");

            cmb_Type2.SelectedItem = Config.Get("parameterType2");
            txt_Parameter2.Text = Config.Get("parameterName2");

            cmb_Type3.SelectedItem = Config.Get("parameterType3");
            txt_Parameter3.Text = Config.Get("parameterName3");

            chk_PreviousLine.Checked = Config.Get("previousLine") == "True";
        }

        private void ConfigDescription_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ConfigChanged())
            {
                SaveConfig();

                if (save)
                {
                    // Notify to parent changes to reload
                    ((LegendGenerator)parent).grid_FiltersReload();
                }
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            save = true;

            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ConfigChanged()
        {
            bool changed = (
                cmb_Type1.Text != Config.Get("parameterType1") ||
                txt_Parameter1.Text != Config.Get("parameterName1") ||
                cmb_Type2.Text != Config.Get("parameterType2") ||
                txt_Parameter2.Text != Config.Get("parameterName2") ||
                cmb_Type3.Text != Config.Get("parameterType3") ||
                txt_Parameter3.Text != Config.Get("parameterName3")
            );

            return changed || chk_PreviousLine.Checked != (Config.Get("previousLine") == "True" ? true : false);
        }

        private void SaveConfig()
        {
            Config.Set("parameterType1", cmb_Type1.Text);
            Config.Set("parameterName1", txt_Parameter1.Text);

            Config.Set("parameterType2", cmb_Type2.Text);
            Config.Set("parameterName2", txt_Parameter2.Text);

            Config.Set("parameterType3", cmb_Type3.Text);
            Config.Set("parameterName3", txt_Parameter3.Text);

            Config.Set("previousLine", chk_PreviousLine.Checked ? "True" : "False");
        }
    }
}
