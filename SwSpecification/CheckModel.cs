using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using VentsMaterials;

namespace SwSpecification
{
    
    public class CheckModel
    {
        ConnectSqlString con = new ConnectSqlString();

        private string _con; 

        private SldWorks swApp;
        private ModelDoc2 swModel;
        private PartDoc swPartDoc;

        // Проверка на дублирование конфигураций
        public void SplitConfigurations(string configName)
        {
            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            swModel = swApp.ActiveDoc;

            CustomPropertyManager swCustProp;
                var ValOut = "";
                var number = "";
                var razdel = "";

                swCustProp = swModel.Extension.CustomPropertyManager[configName];

            if (swModel.CustomInfo2["", "RenameSWP"] == "1")
            {
                 return;
            }

            if (configName != "00")
            {
                swCustProp.Get4("Обозначение", true, out ValOut, out number);

                swCustProp.Get4("Раздел", true, out ValOut, out razdel);

                //var stringSldasm = swModel.GetTitle().Replace(".SLDASM", "");

                if (number != swModel.GetTitle() + "-" + configName)
                {

                    if (razdel == "Детали" | razdel == "Сборочные единицы")
                    {

                        MessageBox.Show("Для конфигурации " + "\"" + configName + "\"" + " не проставленно исполнение. Параметр будет проставлен автоматически.");

                        swModel.DeleteCustomInfo2(configName, "Обозначение");
                        swModel.DeleteCustomInfo2(configName, "Исполнение");
                        swModel.AddCustomInfo3(configName, "Исполнение", 30, "");

                        swModel.AddCustomInfo3(configName, "Обозначение", 30, "$PRP:\"SW-File Name\"-" + configName);
                        swModel.AddCustomInfo3(configName, "Обозначение", 30, "$PRP:\"SW-File Name\"");
                        swModel.CustomInfo2[configName, "Исполнение"] = "1";

                        swModel.ForceRebuild3(false);
                    }

                }
            }
            else
            {
                //if (configName == "00")
                //{
                //    swModel.AddCustomInfo3(configName, "Обозначение", 30, "$PRP:\"SW-File Name\"");
                //    swModel.CustomInfo2[configName, "Исполнение"] = "0";

                //}
            }
        }

        public bool CheckSheetMetal()
        {
            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            swModel = swApp.ActiveDoc;

            var PartSheetMetal = true;

            if (swModel.GetType() == (int)swDocumentTypes_e.swDocPART)
            {
                swPartDoc = (PartDoc)swModel;

                Body2 swbody;
                object[] vbodies = swPartDoc.GetBodies2((int)swBodyType_e.swSolidBody, false);

                for (int i = 0; i < vbodies.Length; i++)
                {
                    swbody = (Body2)vbodies[i];

                    if (swbody.IsSheetMetal())
                    {
                        PartSheetMetal = true;

                    }
                    else
                    {
                        PartSheetMetal = false;
                    }

                }
            }

            return PartSheetMetal;
        }

        public class SheetMetalProperty
        {
            public double BendRadius { get; set; }
            public double KFactor { get; set; }
            public double Thickness { get; set; }
        }
        public List<SheetMetalProperty> GetSheetMetalProperty(ModelDoc2 _swModel, Feature swFeat)
        {
            var getsheetmetalproperty = new List<SheetMetalProperty>();

            var canselect = _swModel.Extension.SelectByID2("Листовой металл1" + "@" + _swModel.GetTitle(), "BODYFEATURE", 0, 0, 0, false, 0, null, 0);
            if (!canselect)
            {
                _swModel.Extension.SelectByID2("Листовой металл" + "@" + _swModel.GetTitle(), "BODYFEATURE", 0, 0, 0, false, 0, null, 0);
            }


            swFeat.Select(true);

            var swSelMgr = _swModel.ISelectionManager;
            swFeat = (Feature)swSelMgr.GetSelectedObject6(1, -1);

            SheetMetalFeatureData swSheetMetal = swFeat.GetDefinition();

            swFeat.ModifyDefinition(swSheetMetal, _swModel, null);


            var sheetMetalValout = new SheetMetalProperty()
            {
                BendRadius = Math.Abs(swSheetMetal.BendRadius * 1000),
                KFactor = swSheetMetal.KFactor,
                Thickness = Math.Abs(swSheetMetal.Thickness * 1000)
            };

            getsheetmetalproperty.Add(sheetMetalValout);

            return getsheetmetalproperty;
        }

        public void SetSheetMetalProperty(ModelDoc2 swModel, Feature swFeat, double BendRadius, double KFactor)
        {

            var sketches = new List<string> { "Листовой металл1", "Листовой металл" };
            var dimensions = new List<string> { "D1", "D2" };
            var sketchName = "";
            foreach (var sketch in sketches.Where(sketch => swModel.Extension.SelectByID2(sketch, "BODYFEATURE", 0, 0, 0, false, 0, null, 0)))
            {
                sketchName = sketch;
            }

            var swSelMgr = swModel.ISelectionManager;
            swFeat = (Feature)swSelMgr.GetSelectedObject6(1, -1);

            foreach (
                var dimenison in
                    dimensions.Where(
                        sketch =>
                            swModel.Extension.SelectByID2(
                                sketch + "@" + sketchName + "@" + Path.GetFileName(swModel.GetPathName()), "DIMENSION",
                                0, 0, 0, false, 0, null, 0)).
                        Select(dimension => (DisplayDimension)swSelMgr.GetSelectedObject6(1, 0)).
                        Select(swDisplayDimension => swDisplayDimension.GetDimension2(0).
                            SetSystemValue3(0.1, (int)swSetValueInConfiguration_e.swSetValue_InThisConfiguration,
                                swModel.ConfigurationManager.ActiveConfiguration.Name))) { }


            SheetMetalFeatureData swSheetMetal = swFeat.GetDefinition();
            swSheetMetal.BendRadius = BendRadius;
            swSheetMetal.KFactor = KFactor;
            swFeat.ModifyDefinition(swSheetMetal, swModel, null);

        }

        public void UseDefaultRadiusBend(ModelDoc2 swmodel)
        {
            var swPart = (PartDoc)swmodel;

            Feature swFeat = swPart.FirstFeature();

            while (swFeat != null)
            {
                string nameTypeFeature = swFeat.GetTypeName2();

                if (nameTypeFeature == "EdgeFlange")
                {
                    EdgeFlangeFeatureData swEdgeFlange = swFeat.GetDefinition();

                    if (swEdgeFlange.UseDefaultBendRadius == false)
                    {
                        swApp.SendMsgToUser2(string.Format("Для \" {0} \" будет установлен параметр \"Использовать радиус по умолчанию\".", swFeat.Name), (int)swMessageBoxIcon_e.swMbInformation, (int)swMessageBoxBtn_e.swMbOk);
                       
                        swEdgeFlange.UseDefaultBendRadius = true;

                        swFeat.ModifyDefinition(swEdgeFlange, swmodel, null);
                    }
                }

                swFeat = swFeat.GetNextFeature();
            }

            swmodel.ForceRebuild3(true);
        }

        public DataTable GetBendTable(string Thickness)
        {
            _con = con.Con;

            var bendDataTable = new DataTable();
            var connect = new SqlConnection(_con);
            var query = "select * from Materials.BendTable where Thickness = '" + Thickness.Replace(",", ".") + "'";
            var sqlCommand = new SqlCommand(query, connect);
            connect.Open();
            var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(bendDataTable);
            connect.Close();
            sqlDataAdapter.Dispose();
            return bendDataTable;
        }

        public void SetSolidWorksSettings()
        {
            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            // Document Templates - Folders
            swApp.SetUserPreferenceStringValue((int) swUserPreferenceStringValue_e.swFileLocationsDocumentTemplates, @"\\" + "192.168.14.11" + @"\SolidWorks Admin\Templates\Шаблоны");
 
            #region UserSettings
                #region Default Templates
                    //Parts
                    swApp.SetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplatePart, @"\\" + "192.168.14.11" + @"\SolidWorks Admin\Templates\Шаблоны\Деталь.prtdot");
                    //Assemblies
                    swApp.SetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplateAssembly, @"\\" + "192.168.14.11" + @"\SolidWorks Admin\Templates\Шаблоны\Сборка.asmdot");
                    //Drawings
                    swApp.SetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplateDrawing, @"\\" + "192.168.14.11" + @"\SolidWorks Admin\Templates\Шаблоны\Чертеж.drwdot");
                    //Always use these default document templates
                    swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swAlwaysUseDefaultTemplates, true);
                #endregion
                #region General
                    //Show latest news feeds in task pane
                    swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swShowNewsFeedsInTaskPane, false);
            
                    //Help make SolidWorks products better by automatically sending your log files to DS SolidWorks Corporation
                    swApp.SetUserPreferenceIntegerValue((int) swUserPreferenceIntegerValue_e.swPerformanceFeedback,(int)swPerformanceFeedback_e.swPerformanceFeedback_No);

                    ////Backup/Recover
                    //swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swAutoSaveEnable, true);
            
                    //Auto-recover - Save auto-recover info every - <n>
                    swApp.SetUserPreferenceIntegerValue((int) swUserPreferenceIntegerValue_e.swAutoSaveInterval, 120);

                    //Auto-recover - Save auto-recover info every - <units>

                    swApp.SetUserPreferenceIntegerValue((int)swUserPreferenceIntegerValue_e.swAutoSaveIntervalMode, (int)swAutoSaveIntervalMode_e.swAutoSaveIntervalMode_Changes);

                    //Specifies auto-recover mode

                    //Auto-recover - Auto-recover folder

                    swApp.SetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swAutoSaveDirectory, @"D:\TempSW");

                    //Backup - Number of backup copies per document

                    swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swBackupEnable, true);

                    //Specifies whether backup copies of the document are created before any changes to the document are made

                    //Backup - Backup folder

                    //Backup - Save backup files in the same location as the original

                    swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swSaveBackupFilesInSameLocationAsOriginal, false);

                    //Backup - Backup folder - <folder>

                    swApp.SetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swBackupDirectory, @"D:\TempSW");

                    //Specifies the  folder where to save backup files

                    //Backup - Remove backups older

                    swApp.SetUserPreferenceToggle((int) swUserPreferenceToggle_e.swBackupRemoveEnable, true);

                    //Backup - Remove backups older than <n> days

                    swApp.SetUserPreferenceIntegerValue((int) swUserPreferenceIntegerValue_e.swBackupRemoveInterval, 1);

                    //Integer value: 1 - 30

                    //Specifies the number of days when to remove backup copies of a document

                    ////Save notification - Show reminder if document has not been saved for

                    //swApp.SetUserPreferenceToggle((int) swUserPreferenceToggle_e.swSaveReminderEnable, true);

                    ////Specifies whether to display a reminder if document has not been saved

                    ////Save notification - Show reminder if document has not been saved for - <n>

                    //swApp.SetUserPreferenceIntegerValue((int) swUserPreferenceIntegerValue_e.swSaveReminderInterval, 5);

                    ////Integer value: 1 - 60

                    ////Specifies how long to wait before sending a save notification reminder

                    ////Save notification - Show reminder if document has not been saved for - <units>

                    //swApp.SetUserPreferenceIntegerValue((int)swUserPreferenceIntegerValue_e.swSaveReminderIntervalMode, (int)swSaveReminderIntervalMode_e.swSaveReminderIntervalMode_Minutes);

                    //See swSaveReminderIntervalMode_e for valid options

                    //Specifies the units of Save notification - Show reminder if document has not been saved for - <n>

                    ////Save notification - Automatically dismiss after

                    //swApp.SetUserPreferenceToggle((int) swUserPreferenceToggle_e.swSaveReminderAutoDismissEnable, true);


                    //Specifies whether to automatically dismiss the save notification reminder; valid only if Save notification - Show reminder if document has not been saved for is true

                    ////Save notification - Automatically dismiss after - <n> seconds

                    //swApp.SetUserPreferenceIntegerValue((int)swUserPreferenceIntegerValue_e.swSaveReminderAutoDismissInterval, 5);
            
                    //Hole Wizard/Toolbox
                    //Hole Wizard and Toolbox folder
                    //swApp.SetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swHoleWizardToolBoxFolder, @"\\srvkb\SolidWorks Admin\SolidWorks Data 2013\");
                    swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swUseFolderAsDefaultSearchLocation, false);
                    //Document Properties
                    swApp.SetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swFileLocationsSheetFormat, @"\\" + "192.168.14.11" + @"\SolidWorks Admin\Templates\Основные надписи");
                    swApp.SetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swFileLocationsMaterialDatabases, @"\\" + "192.168.14.11" + @"\SolidWorks Admin\Templates\Основные надписи");
                    swApp.SetUserPreferenceToggle((int) swUserPreferenceToggle_e.swExtRefNoPromptOrSave, true);
                    //swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swFullyConstrainedSketchMode, true);
                #endregion
            #endregion
        }

        #region ReNameConfiguration
            public void ReNameConfiguration()
            {
                swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
                var swDoc = (ModelDoc2)swApp.ActiveDoc;
                var configList = new List<string>();
                foreach (var confaname in swDoc.GetConfigurationNames())
                {
                    configList.Add(confaname);
                }
                foreach (var confaname in configList.OrderBy(q => q).ToList())
                {
                    Configuration swConf = swDoc.GetConfigurationByName(confaname);
                    if (swConf.IsDerived() == false)
                    {
                        var tepmConfigName = confaname + "+";

                        swDoc.Extension.SelectByID2(confaname, "CONFIGURATIONS", 0, 0, 0, false, 0, null, 0);
                        swDoc.ShowConfiguration2(confaname);
                        swDoc.Extension.SelectByID2(Path.GetFileName(swDoc.GetPathName()), "COMPONENT", 0, 0, 0, false, 0,null, 0);
                        swDoc.ClearSelection2(true);
                        swDoc.AddConfiguration2(tepmConfigName, "", "", true, false, true, true, 256);

                        swDoc.ShowConfiguration2(tepmConfigName);
                        swDoc.Extension.SelectByID2(confaname + "@" + Path.GetFileName(swDoc.GetPathName()), "CONFIGURATIONS", 0, 0, 0, false, 0, null, 0);
                        swDoc.EditDelete();
                        swDoc.DeleteConfiguration2(confaname);

                        var configuration = (IConfiguration) swDoc.GetConfigurationByName(tepmConfigName);
                        configuration.Name = confaname;
                    }
                }
            }
        #endregion

       

    }
}