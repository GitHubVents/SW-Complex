using System.Collections.Generic;
using System.Data;
using System.Linq;
using SolidWorks.Interop.sldworks;
using VentsMaterials;

namespace SwSpecification
{
    class ClassPropertySldWorks
    {
        static private readonly SetMaterials _matDll = new SetMaterials();
        public class ColumnNameTable
        {
            public string Binding { get; set; }
            public string Number { get; set; }
            public string Config { get; set; }
            public string Performance { get; set; }
            public string Riz { get; set; }
            public string Mass { get; set; }
            public string Description { get; set; }
            public string MatName { get; set; }
            public string Thickness { get; set; }
            public string CodFb { get; set; }
        }
        static public List<ColumnNameTable> ListColumn(ModelDoc2 swModel)
        {
            var columnRow = new List<ColumnNameTable>();

            object[] configNamesDll = swModel.GetConfigurationNames();

            var i = 1;

            foreach (string configName in configNamesDll)
            {
                Configuration swConf = swModel.GetConfigurationByName(configName);

                if (swConf.IsDerived() == false)
                {
                    var customPropMan = swModel.Extension.CustomPropertyManager[configName];

                    string valOut;

                    string number;
                    string performance;
                    string mass;
                    string description;
                    string matName;
                    string thickness;
                    string codFb;

                    const string propNumber = "Обозначение";
                    const string propPerformance = "Исполнение";
                    const string propMass = "Масса";
                    const string propDescription = "Наименование";
                    const string propThickness = "Толщина листового металла";

                    customPropMan.Get4(propNumber, true, out valOut, out number);
                    customPropMan.Get4(propPerformance, true, out valOut, out performance);
                    customPropMan.Get4(propMass, true, out valOut, out mass);
                    customPropMan.Get4(propDescription, true, out valOut, out description);

                    customPropMan.Get4("Код_ФБ", true, out valOut, out codFb);
                    customPropMan.Get4("Материал_Имя", true, out valOut, out matName);
                    customPropMan.Get4(propThickness, true, out valOut, out thickness);

                    // Удаление из строки до определенного символа
                    //var trimStr = matName.Substring(matName.IndexOf('>') + 1);
                    //var matNameTrim = trimStr.Substring(trimStr.IndexOf('>') + 1);

                    var columnNameClass = new ColumnNameTable()
                    {
                        Number = number,
                        Performance = performance,
                        Mass = mass,
                        Config = configName,
                        Description = description,
                        Thickness = thickness,
                        CodFb = codFb,
                        //MatName = matName.Replace("$PRPSHEET:\"Толщина листового металла\"", thickness)
                        MatName = matName
                    };

                    columnNameClass.Riz = i++.ToString();
                    
                    columnRow.Add(columnNameClass);
                }
            }

            return columnRow;
        }

        public class ColumnNameClass
        {
            public string PropertiesName { get; set; }
            public string Binding { get; set; }
        }
        public List<ColumnNameClass> ListColumnBinding()
        {
            return (from DataRow datarow in Dt().Rows
                    select new ColumnNameClass
                    {
                        PropertiesName = datarow["ColumnName"].ToString(),
                        Binding = datarow["Binding"].ToString(),

                    }).ToList();
        }
        public DataTable Dt()
        {
            var dtString = new DataTable();

            dtString.Columns.Add("ColumnName");
            dtString.Columns.Add("Binding");
            dtString.Columns.Add("DefaultWidth");

            //var columnNameArray = new[] { "Обозначение", "Масса", "Исполнение", "Наименование", "Толщина листового металла" };
            //var bindingArray = new[] { "Number", "Mass",  "Performance", "Decription", "Thickness" };

            dtString.Rows.Add("Обозначение", "Number", "*");
            dtString.Rows.Add("Масса", "Mass", "100");
            dtString.Rows.Add("Риc.", "Riz", "100");
            //dtString.Rows.Add("Исполнение", "Performance", "100");
            dtString.Rows.Add("Наименование", "Decription", "100");
            dtString.Rows.Add("Толщина листового металла", "Thickness", "100");
            dtString.Rows.Add("Материал", "MatName", "100");

            return dtString;
        }

        public class ColumnNameEditPropTable
        {
            public string ColumnConfig { get; set; }
            public string ColumnNumber { get; set; }
            public string ColumnDescription { get; set; }
            public string ColumnMatName { get; set; }
            public string ColumnThickness { get; set; }
            public string CodFb { get; set; }
        }
        static public List<ColumnNameEditPropTable> ListColumnToEditProp(ModelDoc2 swModel)
        {
            

            var columnRow = new List<ColumnNameEditPropTable>();

            object[] configNamesDll = swModel.GetConfigurationNames();

            foreach (string configName in configNamesDll)
            {
                Configuration swConf = swModel.GetConfigurationByName(configName);

                if (swConf.IsDerived() == false)
                {
                    var customPropMan = swModel.Extension.CustomPropertyManager[configName];

                    var valOut = "";
                    var number = "";
                    var description = "";
                    var matName = "";
                    var thickness = "";
                    var codFb = "";

                    customPropMan.Get4("Обозначение", true, out valOut, out number);
                    customPropMan.Get4("Наименование", true, out valOut, out description);
                    customPropMan.Get4("Код_ФБ", true, out valOut, out codFb);
                    customPropMan.Get4("Материал_Таблица", true, out valOut, out matName);
                    customPropMan.Get4("Толщина листового металла", true, out valOut, out thickness);

                    var columnNameClass = new ColumnNameEditPropTable();

                    if (configName == "00")
                    {
                        columnNameClass.ColumnNumber = number;
                    }
                    else
                    {
                        columnNameClass.ColumnNumber = "-" + configName;
                    }

                    columnNameClass.ColumnConfig = configName;
                    columnNameClass.ColumnDescription = description;
                    columnNameClass.ColumnThickness = thickness;
                    columnNameClass.CodFb = codFb;
                    columnNameClass.ColumnMatName = matName.Replace("$PRPSHEET:\"Толщина листового металла\"", thickness);

                    columnRow.Add(columnNameClass);
                }
            }

            return columnRow;
        }

    }
}