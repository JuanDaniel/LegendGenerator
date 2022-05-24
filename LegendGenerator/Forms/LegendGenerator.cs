using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BBI.JD.Forms
{
    public partial class LegendGenerator : System.Windows.Forms.Form
    {
        private UIApplication application;
        private UIDocument uiDoc;
        private Document document;
        private Family family;
        private bool loadedView;
        private bool loadedCategory;
        private List<Autodesk.Revit.DB.View> views;
        private List<Category> categories;
        private List<Autodesk.Revit.DB.View> legends;
        private List<ElementId> filterIds;
        private ParameterDescription[] parametersDescription;
        private Dictionary<ElementId, string[]> filterDescriptions;
        private ContextMenu contextMenu;

        public LegendGenerator(UIApplication application)
        {
            InitializeComponent();

            this.application = application;

            uiDoc = application.ActiveUIDocument;
            document = uiDoc.Document;

            loadedView = false;
            loadedCategory = false;

            contextMenu = new ContextMenu();
        }

        private void LegendGenerator_Load(object sender, EventArgs e)
        {
            family = Core.LoadLegendGeneratorFamily(document, "LegendGenerator_SymbolFilled");

            if (family == null)
            {
                MessageBox.Show("Not found LegendGenerator_SymbolFilled.rfa", "Load LegendGenerator_SymbolFilled familly", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            contextMenu.MenuItems.Add("Active / Deactivate", new EventHandler(grid_FiltersActiveDeactivate));
            contextMenu.MenuItems.Add("Change legend symbol", new EventHandler(grid_FiltersChangeSymbolForm));

            grid_Filters.ContextMenu = contextMenu;

            LoadFamilySymbols();

            LoadViews();

            LoadLegends();

            loadedView = true;
        }

        private void btn_CheckAllView_Click(object sender, EventArgs e)
        {
            SelectlistBox(lst_Views);
        }

        private void btn_CheckNoneView_Click(object sender, EventArgs e)
        {
            SelectlistBox(lst_Views, false);
        }

        private void btn_CheckAllCategory_Click(object sender, EventArgs e)
        {
            SelectlistBox(lst_Categories);
        }

        private void btn_CheckNoneCategory_Click(object sender, EventArgs e)
        {
            SelectlistBox(lst_Categories, false);
        }

        private void lst_Views_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (loadedView)
            {
                List<int> checkedIndices = new List<int>();

                if (e.NewValue == CheckState.Checked)
                {
                    checkedIndices.Add(e.Index);
                }

                checkedIndices.AddRange(lst_Views.CheckedIndices.Cast<int>().Where(x => x != e.Index));

                LoadFilters(checkedIndices);
                LoadCategories(checkedIndices);
            }
        }

        private void lst_Categories_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (loadedCategory)
            {
                List<int> checkedIndices = new List<int>();

                if (e.NewValue == CheckState.Checked)
                {
                    checkedIndices.Add(e.Index);
                }

                checkedIndices.AddRange(lst_Categories.CheckedIndices.Cast<int>().Where(x => x != e.Index));

                LoadFilters(lst_Views.CheckedIndices.Cast<int>().ToList(), checkedIndices);
            }
        }

        private void btn_Generate_Click(object sender, EventArgs e)
        {
            List<FilterSymbol> filterSymbols = new List<FilterSymbol>();

            foreach (DataGridViewRow row in grid_Filters.Rows)
            {
                if ((bool)row.Cells["cActive"].Value)
                {
                    string symbol = "Empty";

                    if (row.Cells["cSymbol"].Value != null)
                    {
                        if (!string.IsNullOrEmpty(row.Cells["cSymbol"].Value.ToString()))
                        {
                            symbol = row.Cells["cSymbol"].Value.ToString();
                        }
                    }

                    ElementId elementId = new ElementId(int.Parse(row.Cells["cId"].Value.ToString()));

                    filterSymbols.Add(new FilterSymbol(
                        elementId,
                        filterDescriptions[elementId],
                        symbol
                    ));
                }
            }

            if (filterSymbols.Count == 0)
            {
                MessageBox.Show("There must be at least one active filter.", "None filters", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return;
            }

            if (string.IsNullOrEmpty(cmb_Legends.Text))
            {
                MessageBox.Show("Select a legend view to insert the generated legend.", "Legend view", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return;
            }

            if (grid_Filters.Rows.Count != filterDescriptions.Count())
            {
                DialogResult dialog = MessageBox.Show("There are empty legend descriptions.\nDo you want to continue?", "Empty legend descriptions", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialog == DialogResult.No)
                {
                    return;
                }
            }

            if (filterSymbols.Where(x => x.SymbolName == "Empty").Count() > 0)
            {
                DialogResult dialog = MessageBox.Show("There are empty legend symbols.\nDo you want to continue?", "Empty legend symbols", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialog == DialogResult.No)
                {
                    return;
                }
            }

            Autodesk.Revit.DB.View legendView = legends[cmb_Legends.SelectedIndex - 1];

            try
            {
                Core.GenerateLegend(document, views, filterSymbols, legendView, family);

                MessageBox.Show(
                    string.Format("The legend was successfully generated.\nGo to the legend view « {0} »", legendView.Name),
                    "Legend Generator",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message , "Legend Generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Config_Click(object sender, EventArgs e)
        {
            ConfigDescription form = new ConfigDescription(this);

            form.ShowDialog();
        }

        private void grid_Filters_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (grid_Filters.SelectedRows.Count > 0)
                {
                    contextMenu.Show(grid_Filters, new System.Drawing.Point(e.X, e.Y));
                }
            }
        }

        private void grid_FiltersChangeSymbolForm(object sender, EventArgs e) {
            ChangeSymbol form = new ChangeSymbol(this, grid_Filters.SelectedRows.Count, cSymbol.Items);

            form.ShowDialog();
        }

        private void grid_FiltersActiveDeactivate(object sender, EventArgs e)
        {
            for (int i = 0; i < grid_Filters.SelectedRows.Count; i++)
            {
                DataGridViewRow row = grid_Filters.SelectedRows[i];

                row.Cells["cActive"].Value = !(bool)row.Cells["cActive"].Value;
            }

            grid_Filters.RefreshEdit();
        }

        private string GetTiTleForm()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;

            return string.Format("{0} ({1}.{2}.{3}.{4})", "Legend Generator", version.Major, version.Minor, version.Build, version.Revision);
        }

        private void LoadFamilySymbols()
        {
            List<string> names = new List<string>();

            foreach (Family familySymbol in Core.FamilySymbols(document, family))
            {
                string name = familySymbol.Name;

                if (name == "Empty")
                {
                    name = "";
                }

                names.Add(name);
            }

            cSymbol.Items.Clear();
            cSymbol.Items.AddRange(names.OrderBy(x => x).ToArray());
        }

        private void LoadViews()
        {
            views = new List<Autodesk.Revit.DB.View>();

            lst_Views.Items.Clear();

            if (document.ActiveView is ViewPlan)
            {
                views.Add(document.ActiveView);

                lst_Views.Items.Add(string.Format("{0} - [{1}]", document.ActiveView.Name, document.ActiveView.ViewType));
            }
            else if (document.ActiveView is ViewSheet)
            {
                foreach (ElementId viewId in ((ViewSheet)document.ActiveView).GetAllPlacedViews())
                {
                    Autodesk.Revit.DB.View view = document.GetElement(viewId) as Autodesk.Revit.DB.View;

                    views.Add(view);

                    lst_Views.Items.Add(string.Format("{0} - [{1}]", view.Name, view.ViewType));
                }
            }

            SelectDefaultViews();

            LoadFilters(lst_Views.CheckedIndices.Cast<int>().ToList());
            LoadCategories(lst_Views.CheckedIndices.Cast<int>().ToList());
        }

        private void LoadLegends()
        {
            cmb_Legends.Items.Clear();
            cmb_Legends.Items.Add("");

            legends = new FilteredElementCollector(document)
                        .OfClass(typeof(Autodesk.Revit.DB.View))
                            .Cast<Autodesk.Revit.DB.View>()
                                .Where(x => x.ViewType == ViewType.Legend)
                                    .OrderBy(x => x.Name)
                                        .ToList();

            foreach (Autodesk.Revit.DB.View view in legends)
            {
                cmb_Legends.Items.Add(view.Name);
            }
        }

        private void LoadFilters(List<int> checkedViewIndices, List<int> checkedCategoryIndices = null) {
            filterIds = new List<ElementId>();

            parametersDescription = GetParametersDescription();

            filterDescriptions = new Dictionary<ElementId, string[]>();

            grid_Filters.Rows.Clear();

            foreach (int index in checkedViewIndices)
            {
                Autodesk.Revit.DB.View view = views.ElementAt(index);

                // An union between filters, exclude the unchecked visibility
                foreach (ElementId filterId in view.GetFilters())
                {
                    if (!filterIds.Contains(filterId))
                    {
                        ParameterFilterElement filter = document.GetElement(filterId) as ParameterFilterElement;

                        if (view.GetFilterVisibility(filterId) && HasCandidate(view, filter))
                        {
                            if (checkedCategoryIndices != null)
                            {
                                if (!BelongCategory(checkedCategoryIndices, filter))
                                {
                                    continue;
                                }
                            }

                            filterIds.Add(filterId);

                            List<object> row = new List<object>();

                            row.Add(filter.Id.ToString()); // Id
                            row.Add(true); // Active
                            row.Add(filter.Name); // Name
                            row.Add(GetRectangleColor(view, filterId)); // Color

                            string[] descriptionLines = GetDescription(view, filter);

                            filterDescriptions.Add(filterId, descriptionLines);

                            row.Add(string.Join("_", descriptionLines.Select(x => string.Format("[{0}]", x)))); // Legend description

                            grid_Filters.Rows.Add(row.ToArray());

                            grid_Filters.Rows[grid_Filters.Rows.Count - 1].Cells["cSymbol"].Value = GetSymbolSelected(filter); // Legend symbol
                        }
                    }
                }
            }

            txt_FiltersCount.Text = filterIds.Count.ToString();
        }

        private void LoadCategories(List<int> checkedViewsIndices)
        {
            loadedCategory = false;

            categories = new List<Category>();

            lst_Categories.Items.Clear();

            foreach (int index in checkedViewsIndices)
            {
                Autodesk.Revit.DB.View view = views.ElementAt(index);

                List<Element> elements = new FilteredElementCollector(document, view.Id).ToList();

                foreach (Element element in elements)
                {
                    if(element.Category != null)
                    {
                        // filter only visible in view
                        if (!Core.IsElementVisibleInView(view, element))
                        {
                            continue;
                        }

                        if (!categories.Exists(x => x.Id == element.Category.Id))
                        {
                            categories.Add(element.Category);

                            lst_Categories.Items.Add(element.Category.Name);
                        }
                    }
                }
            }

            SelectDefaultCategories();

            loadedCategory = true;
        }

        private void SelectDefaultViews()
        {
            ViewType[] types = new ViewType[] { ViewType.EngineeringPlan, ViewType.CeilingPlan };

            for (int i = 0; i < views.Count(); i++)
            {
                Autodesk.Revit.DB.View view = views.ElementAt(i);

                lst_Views.SetItemChecked(i, types.Contains(view.ViewType));
            }
        }

        private void SelectDefaultCategories()
        {
            List<string> categoryNames = new List<string>();

            foreach (ElementId filterId in filterIds)
            {
                ParameterFilterElement filter = document.GetElement(filterId) as ParameterFilterElement;

                foreach (ElementId categoryId in filter.GetCategories())
                {
                    Category category = Category.GetCategory(document, categoryId);

                    if (category != null)
                    {
                        if (!categoryNames.Contains(category.Name))
                        {
                            int index = lst_Categories.Items.IndexOf(category.Name);

                            if (index >= 0)
                            {
                                lst_Categories.SetItemChecked(index, true);
                            }

                            categoryNames.Add(category.Name);
                        }
                    }
                }
            }
        }

        private void SelectlistBox(CheckedListBox list, bool select = true)
        {
            for (int i = 0; i < list.Items.Count; i++)
            {
                list.SetItemChecked(i, select);
            }
        }

        private bool HasCandidate(Autodesk.Revit.DB.View view, ParameterFilterElement filter)
        {
            var candidates = new FilteredElementCollector(document, view.Id)
                .WherePasses(filter.GetElementFilter())
                    .ToElementIds();

            return candidates.Count > 0;
        }

        private bool BelongCategory(List<int> checkedCategoryIndices, ParameterFilterElement filter)
        {
            foreach (int index in checkedCategoryIndices)
            {
                Category category = categories.ElementAt(index);

                if (filter.GetCategories().Where(x => x == category.Id).Count() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        private Bitmap GetRectangleColor(Autodesk.Revit.DB.View view, ElementId filterId)
        {
            OverrideGraphicSettings graphicSettings = view.GetFilterOverrides(filterId);

            if (graphicSettings.SurfaceForegroundPatternColor.IsValid)
            {
                return Core.DrawFilledRectangle(graphicSettings.SurfaceForegroundPatternColor);
            }

            return LegendGenerator.no_overwrite;
        }

        private ParameterDescription[] GetParametersDescription()
        {
            List<ParameterDescription> parametersDescription = new List<ParameterDescription>();

            parametersDescription.Add(new ParameterDescription(
                Config.Get("parameterName1"),
                Config.Get("parameterType1") == "Type" ? 0 : 1
            ));

            parametersDescription.Add(new ParameterDescription(
                Config.Get("parameterName2"),
                Config.Get("parameterType2") == "Type" ? 0 : 1
            ));

            parametersDescription.Add(new ParameterDescription(
                Config.Get("parameterName3"),
                Config.Get("parameterType3") == "Type" ? 0 : 1
            ));

            return parametersDescription.ToArray();
        }

        private string[] GetDescription(Autodesk.Revit.DB.View view, ParameterFilterElement filter)
        {
            List<string> lines = new List<string>();

            Element element = new FilteredElementCollector(document, view.Id)
                .WherePasses(filter.GetElementFilter())
                    .FirstOrDefault();

            if (element != null)
            {
                for (int i = 0; i < parametersDescription.Count(); i++)
                {
                    Parameter parameter = null;

                    // Parameter Type
                    if (parametersDescription[i].Type == 0)
                    {
                        Element elementType = document.GetElement(element.GetTypeId());

                        if (elementType != null)
                        {
                            parameter = elementType.LookupParameter(parametersDescription[i].Name);
                        }
                    }
                    // Parameter Instance
                    else
                    {
                        parameter = element.LookupParameter(parametersDescription[i].Name);
                    }

                    if (parameter != null)
                    {
                        switch (parameter.StorageType)
                        {
                            case StorageType.Double:
                                lines.Add(string.Format("{0}", parameter.AsDouble().ToString()));
                                break;

                            case StorageType.ElementId:
                                lines.Add(string.Format("{0}", parameter.AsValueString()));
                                break;

                            case StorageType.Integer:
                                lines.Add(string.Format("{0}", parameter.AsInteger().ToString()));
                                break;

                            case StorageType.None:
                                break;

                            case StorageType.String:
                                lines.Add(string.Format("{0}", parameter.AsString()));
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            return lines.ToArray();
        }

        private string GetSymbolSelected(ParameterFilterElement filter)
        {
            ICollection<ElementId> categories = filter.GetCategories();

            if (categories.Count == 1)
            {
                //Category category = Category.GetCategory(document, categories.First());

                ElementId categoryId = categories.First();

                switch (categoryId.IntegerValue)
                {
                    case (int)BuiltInCategory.OST_StructuralColumns:
                        return "SColumn_Circular";

                    case (int)BuiltInCategory.OST_StructConnections:
                        return "SConnection_Ménsula";

                    case (int)BuiltInCategory.OST_Floors:
                        return "SFloor_Losa";

                    case (int)BuiltInCategory.OST_StructuralFoundation:
                        return "SFoundation_Aislada";

                    case (int)BuiltInCategory.OST_StructuralFraming:
                        return "SFraming_Viga";

                    case (int)BuiltInCategory.OST_Walls:
                        return "SWall_Muro";

                    default:
                        break;
                }
            }

            return string.Empty;
        }

        public void grid_FiltersChangeSymbol(string symbol)
        {
            for (int i = 0; i < grid_Filters.SelectedRows.Count; i++)
            {
                DataGridViewRow row = grid_Filters.SelectedRows[i];

                row.Cells["cSymbol"].Value = symbol;
            }

            grid_Filters.RefreshEdit();
        }

        public void grid_FiltersReload()
        {
            DialogResult dialog = MessageBox.Show("The legend's description configuration was modified.\nDo you want to reload?", "Legend's description modified", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.No)
            {
                return;
            }

            LoadFilters(lst_Views.CheckedIndices.Cast<int>().ToList());
        }
    }
}
