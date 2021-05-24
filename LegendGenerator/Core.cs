using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BBI.JD
{
    public static class Core
    {
        public static Family LoadLegendGeneratorFamily(Document document, string familyName = "LegendGenerator_SymbolFilled")
        {
            Family family = new FilteredElementCollector(document)
                .OfClass(typeof(Family))
                    .Cast<Family>()
                        .FirstOrDefault(x => x.Name == familyName);

            if (family != null)
            {
                return family;
            }

            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            string folder = new FileInfo(assemblyPath).Directory.FullName;

            Transaction transaction = null;

            try
            {
                transaction = new Transaction(document, "Load LegendGenerator family");

                transaction.Start();

                document.LoadFamily(string.Concat(folder, "/", familyName, ".rfa"), out family);

                transaction.Commit();
            }
            catch (Exception)
            {
                if (null != transaction)
                {
                    transaction.RollBack();
                }
            }

            return family;
        }

        public static List<Family> FamilySymbols(Document document, Family family)
        {
            List<Family> symbols = new List<Family>();

            Document famDoc = document.EditFamily(family);

            symbols.AddRange(new FilteredElementCollector(famDoc)
                .OfClass(typeof(Family))
                    .Cast<Family>()
                        .Where(x => !string.IsNullOrEmpty(x.Name))
                            .OrderBy(x => x.Name)
                                .ToList()
            );

            return symbols;
        }

        public static Dictionary<string, ElementId> FamilyTypeSymbols(Document document, Family family, Parameter parameter)
        {
            Dictionary<string, ElementId> familyTypes = new Dictionary<string, ElementId>();

            foreach(ElementId elementId in family.GetFamilyTypeParameterValues(parameter.Id))
            {
                Element element = document.GetElement(elementId);

                if (!familyTypes.ContainsKey(element.Name))
                {
                    familyTypes.Add(element.Name, elementId);
                }
            }

            return familyTypes;
        }

        public static Bitmap DrawFilledRectangle(Autodesk.Revit.DB.Color color, int x = 50, int y = 25)
        {
            Bitmap bmp = new Bitmap(x, y);
            System.Drawing.Color cColor = System.Drawing.Color.FromArgb(color.Red, color.Green, color.Blue);

            using (Graphics graph = Graphics.FromImage(bmp))
            {
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(0, 0, x, y);
                graph.FillRectangle(new SolidBrush(cColor), rectangle);
            }

            return bmp;
        }

        public static string RGBtoHex(Autodesk.Revit.DB.Color color)
        {
            return string.Format("{0:X2}{1:X2}{2:X2}", color.Red, color.Green, color.Blue);
        }

        /// <summary>
        /// Determine whether an element is visible in a view, 
        /// by Colin Stark, described in
        /// http://stackoverflow.com/questions/44012630/determine-is-a-familyinstance-is-visible-in-a-view
        /// </summary>
        public static bool IsElementVisibleInView(
          this View view,
          Element el)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            if (el == null)
            {
                throw new ArgumentNullException(nameof(el));
            }

            // Obtain the element's document.

            Document doc = el.Document;

            ElementId elId = el.Id;

            // Create a FilterRule that searches 
            // for an element matching the given Id.

            FilterRule idRule = ParameterFilterRuleFactory
              .CreateEqualsRule(
                new ElementId(BuiltInParameter.ID_PARAM),
                elId);

            var idFilter = new ElementParameterFilter(idRule);

            // Use an ElementCategoryFilter to speed up the 
            // search, as ElementParameterFilter is a slow filter.

            Category cat = el.Category;
            var catFilter = new ElementCategoryFilter(cat.Id);

            // Use the constructor of FilteredElementCollector 
            // that accepts a view id as a parameter to only 
            // search that view.
            // Also use the WhereElementIsNotElementType filter 
            // to eliminate element types.

            FilteredElementCollector collector =
                new FilteredElementCollector(doc, view.Id)
                  .WhereElementIsNotElementType()
                  .WherePasses(catFilter)
                  .WherePasses(idFilter);

            // If the collector contains any items, then 
            // we know that the element is visible in the
            // given view.

            return collector.Any();
        }

        public static void GenerateLegend(Document document, List<View> views, List<FilterSymbol> filterSymbols, View legend, Family familyLegend)
        {
            XYZ[] points = new XYZ[5];

            double height = UnitUtils.ConvertToInternalUnits(15, DisplayUnitType.DUT_MILLIMETERS);
            double offset = 1.45f;

            points[0] = new XYZ(0, 0, 0);
            points[1] = new XYZ(height, 0, 0);
            points[2] = new XYZ(height, height, 0);
            points[3] = new XYZ(0, height, 0);
            points[4] = new XYZ(0, 0, 0);

            CurveLoop profileloop = new CurveLoop();

            for (int i = 0; i < 4; i++)
            {
                Line line = Line.CreateBound(points[i], points[i + 1]);

                profileloop.Append(line);
            }

            Transaction transaction = null;

            try
            {
                transaction = new Transaction(document, "Generate automatic legend");

                transaction.Start();

                List<FilledRegionType> filledRegionTypes = SyncFilledRegionType(document, views, filterSymbols.Select(x => x.FilterId).ToList());

                FamilySymbol familySymbol = null;

                foreach (ElementId fsid in familyLegend.GetFamilySymbolIds())
                {
                    familySymbol = document.GetElement(fsid) as FamilySymbol;
                }

                if (familySymbol != null)
                {
                    if (!familySymbol.IsActive)
                    {
                        familySymbol.Activate();
                    }
                }

                FilledRegion filledRegion = null;

                XYZ position = new XYZ(0, 0, 0);

                Dictionary<string, ElementId> familyTypeSymbols = null;

                foreach (FilterSymbol filterSymbol in filterSymbols)
                {
                    ParameterFilterElement filter = document.GetElement(filterSymbol.FilterId) as ParameterFilterElement;

                    FilledRegionType filledRegionType = filledRegionTypes.FirstOrDefault(x => x.Name == filter.Name);

                    // Draw region with according pattern
                    if (filledRegion == null)
                    {
                        filledRegion = FilledRegion.Create(document, filledRegionType.Id, legend.Id, new List<CurveLoop>() { profileloop });
                    }
                    else
                    {
                        filledRegion = document.GetElement(ElementTransformUtils.CopyElement(document, filledRegion.Id, new XYZ(0, height * offset, 0)).First()) as FilledRegion;

                        filledRegion.ChangeTypeId(filledRegionType.Id);
                    }

                    // Create family instance
                    FamilyInstance familyInstance = document.Create.NewFamilyInstance(position, familySymbol, legend);

                    Parameter parameterType = familyInstance.LookupParameter("SymbolLegend");

                    if (parameterType != null)
                    {
                        if (familyTypeSymbols == null)
                        {
                            familyTypeSymbols = FamilyTypeSymbols(document, familySymbol.Family, parameterType);
                        }

                        parameterType.Set(familyTypeSymbols[filterSymbol.SymbolName]);
                    }

                    for (int i = 0, j = 0; i < filterSymbol.DescriptionLines.Count(); i++)
                    {
                        Parameter parameterLine;
                        string description = string.Empty;

                        if (Config.Get("previousLine") == "True")
                        {
                            parameterLine = familyInstance.LookupParameter(string.Format("LineLegend{0}", j + 1));
                            description = filterSymbol.DescriptionLines[i];

                            if (!string.IsNullOrEmpty(description))
                            {
                                j++;
                            }
                        }
                        else
                        {
                            parameterLine = familyInstance.LookupParameter(string.Format("LineLegend{0}", i + 1));
                        }

                        if (parameterLine != null)
                        {
                            parameterLine.Set(description);
                        }
                    }

                    position = new XYZ(position.X, position.Y + height * offset, position.Z);
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (null != transaction)
                {
                    transaction.RollBack();

                    throw ex;
                }
            }
        }

        public static List<FilledRegionType> SyncFilledRegionType(Document document, List<View> views, List<ElementId> filterIds)
        {
            List<FilledRegionType> filledRegionTypes = new List<FilledRegionType>();

            using (SubTransaction subTransaction = new SubTransaction(document))
            {
                try
                {
                    subTransaction.Start();

                    foreach (ElementId filterId in filterIds)
                    {
                        ParameterFilterElement filter = document.GetElement(filterId) as ParameterFilterElement;

                        FilledRegionType filledRegionType = filledRegionTypes.FirstOrDefault(x => x.Name == filter.Name);

                        if (filledRegionType == null)
                        {
                            filledRegionType = new FilteredElementCollector(document)
                                .OfClass(typeof(FilledRegionType))
                                    .Cast<FilledRegionType>()
                                        .FirstOrDefault(x => x.Name == filter.Name);

                            if (filledRegionType == null)
                            {
                                filledRegionType = new FilteredElementCollector(document)
                                    .OfClass(typeof(FilledRegionType))
                                        .Cast<FilledRegionType>()
                                            .FirstOrDefault();

                                if (filledRegionType == null)
                                {
                                    throw new NoFilledRegionTypeException("There is no filled region type created.");
                                }

                                filledRegionType = filledRegionType.Duplicate(filter.Name) as FilledRegionType;
                            }

                            View view = views.FirstOrDefault(x => x.GetFilterOverrides(filter.Id) != null);

                            OverrideGraphicSettings graphicSettings = view.GetFilterOverrides(filter.Id);

                            filledRegionType.ForegroundPatternColor = graphicSettings.SurfaceForegroundPatternColor;
                            filledRegionType.ForegroundPatternId = graphicSettings.SurfaceForegroundPatternId;

                            filledRegionTypes.Add(filledRegionType);
                        }
                    }

                    subTransaction.Commit();
                }
                catch (NoFilledRegionTypeException ex)
                {
                    subTransaction.RollBack();

                    throw ex;
                }
                catch (Exception)
                {
                    subTransaction.RollBack();
                }
            }

            return filledRegionTypes;
        }
    }

    public class NoLegendException : Exception
    {
        public NoLegendException() { }

        public NoLegendException(string message) : base(message) { }
    }

    public class NoFilledRegionTypeException : Exception
    {
        public NoFilledRegionTypeException() {}

        public NoFilledRegionTypeException(string message) : base (message) {}
    }

    public class FilterSymbol
    {
        private ElementId filterId;

        private string[] descriptionLines;

        private string symbolName;

        public FilterSymbol(ElementId filterId, string[] descriptionLines, string symbolName) {
            this.filterId = filterId;
            this.descriptionLines = descriptionLines;
            this.symbolName = symbolName;
        }

        public ElementId FilterId { get => filterId; }

        public string[] DescriptionLines { get => descriptionLines; }

        public string SymbolName { get => symbolName; }
    }

    public class ParameterDescription
    {
        private string name;

        private int type; // 0 -> type, 1 -> instance

        public ParameterDescription(string name, int type)
        {
            this.name = name;
            this.type = type;
        }

        public string Name { get => name; }

        public int Type { get => type; }
    }
}