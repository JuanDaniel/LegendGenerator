using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.RevitAddIns;

namespace Installer
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        static string APPDATA_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        static string CFGFOLDER_PATH = Path.Combine(APPDATA_PATH, "BBI LegendGenerator");
        static string CFGFILE_NAME = "LegendGenerator.config";
        static string CFGFILE_PATH = Path.Combine(CFGFOLDER_PATH, CFGFILE_NAME);

        public Installer()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            WriteRevitAddin();
            CreateConfig();
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);

            DeleteRevitAddin();
            DeleteConfig();
        }

        private void CreateConfig()
        {
            if (!Directory.Exists(CFGFOLDER_PATH))
            {
                Directory.CreateDirectory(CFGFOLDER_PATH);
            }

            string folder = this.Context.Parameters["targetdir"];
            string config = Path.Combine(folder, CFGFILE_NAME);

            File.Copy(config, CFGFILE_PATH, true);
        }

        private void DeleteConfig()
        {
            Directory.Delete(CFGFOLDER_PATH, true);
        }

        private void WriteRevitAddin()
        {
            foreach (var product in RevitProductUtility.GetAllInstalledRevitProducts())
            {
                if (product.Version == RevitVersion.Revit2019)
                {
                    string pathAddin = product.AllUsersAddInFolder + "\\LegendGenerator.addin";
                    Guid guid = new Guid("1d8721f0-ae70-407f-ac46-7f561d28c09f");
                    string assembly = GetAssembly();
                    string fullClassName = "BBI.JD.CrtlApplication";
                    string vendorId = "JDS";
                    string vendorDescription = "Juan Daniel SANTANA";

                    RevitAddInManifest manifest = File.Exists(pathAddin) ? AddInManifestUtility.GetRevitAddInManifest(pathAddin) : new RevitAddInManifest();

                    RevitAddInApplication app = manifest.AddInApplications.FirstOrDefault(x => x.AddInId == guid);

                    if (app == null)
                    {
                        app = new RevitAddInApplication("LegendGenerator", assembly, guid, fullClassName, vendorId);
                        app.VendorDescription = vendorDescription;

                        manifest.AddInApplications.Add(app);
                    }
                    else
                    {
                        app.Assembly = assembly;
                        app.FullClassName = fullClassName;
                    }

                    if (manifest.Name == null)
                    {
                        manifest.SaveAs(pathAddin);
                    }
                    else
                    {
                        manifest.Save();
                    }
                }
            }
        }

        private void DeleteRevitAddin()
        {
            foreach (var product in RevitProductUtility.GetAllInstalledRevitProducts())
            {
                if (product.Version == RevitVersion.Revit2019)
                {
                    string pathAddin = product.AllUsersAddInFolder + "\\LegendGenerator.addin";
                    
                    if (File.Exists(pathAddin))
                    {
                        File.Delete(pathAddin);
                    }
                }
            }
        }

        private string GetAssembly()
        {
            string pathDir = Context.Parameters["targetdir"];

            pathDir = pathDir.Remove(pathDir.Length - 1, 1);

            return pathDir + "LegendGenerator.dll";
        }
    }
}
