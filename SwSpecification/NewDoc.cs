using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swcommands;
using SolidWorks.Interop.swconst;

namespace SwSpecification
{
    public partial class NewDoc : Form
    {
        public NewDoc()
        {
            InitializeComponent();
        }

        #region " VARIABLES "
            private SldWorks swapp;
            private ModelDoc2 swmodel;
            private DrawingDoc swDraw;
            private SolidWorks.Interop.sldworks.View swView;
            private Sheet swSheet;
            private string[] vSheetNames;
            private string strActiveSheetName;
            addincheckbox addinform = new addincheckbox();
            PBar pbarform = new PBar();
        #endregion

        private void NewDoc_Load(object sender, EventArgs e)
        {
            check();
            //'если не выбранны конфигурации кнопка создать выключена
            BtCreate.Enabled = false;
            //'Add a select all checkbox at DataGridView header
            AddSelectAllCheckBox(DataGridConfig);

            LoadDataGridConfig();
        }

        public void LoadDataGridConfig()
        {
            try
            {

                //SelectionMgr swSelMgr;
                //Configuration swConfig; ;
                string sConfigName;
                object[] vConfNameArr;

                //swapp = CreateObject("SldWorks.Application")
                swapp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
                swmodel = swapp.ActiveDoc;

                swDraw = (DrawingDoc)swmodel;

                swSheet = swDraw.GetCurrentSheet();
                strActiveSheetName = swSheet.GetName();

                vSheetNames = swDraw.GetSheetNames();
                swDraw.ActivateSheet(vSheetNames[0]);

                swSheet = swDraw.GetCurrentSheet();
                swView = swDraw.GetFirstView();

                // Получаем параметры модели
                if (swSheet.CustomPropertyView == "По умолчанию" | swSheet.CustomPropertyView == "Default")
                {
                    swView = swView.GetNextView();
                }

                sConfigName = swView.ReferencedConfiguration;

                swmodel = swView.ReferencedDocument;
                swmodel.GetConfigurationByName(sConfigName);

                vConfNameArr = swmodel.GetConfigurationNames();

                // Конфигурация
                for (var i = 0; i <= vConfNameArr.GetUpperBound(0); i++)
                {
                    DataGridConfig.Rows.Add(ColChB.Selected, vConfNameArr[i]);
                }

                //Get customProperty from Assy
                CustomPropertyManager swCustProp = default(CustomPropertyManager);
                string valOut = "";
                string Number = "";
                string Description = "";

                swCustProp = swmodel.Extension.CustomPropertyManager[""];

                swCustProp.Get2("Обозначение", out valOut, out Number);
                swCustProp.Get2("Наименование", out valOut, out Description);

                addinform = new addincheckbox();
                try
                {
                    //Fill DataGriad
                    foreach (string line in System.IO.File.ReadAllLines("C:\\Program Files\\SW-Complex\\doc.txt"))
                    {
                        addinform.DGDoc.Rows.Add(addinform.DocChb.Selected, line);
                    }
                }
                catch (Exception ex)
                {
                    //Interaction.MsgBox("Отсутствует SW-Complex или Doc.txt");
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void BtCreate_Click(object sender, EventArgs e)
        {
            pbarform.Show();

            this.Visible = false;

            timer1.Start();

            //DeleteBom();
            //InsertBom();
            //GetXMLfromBOM();
            //hidebomtable();
            //Close();

        }

        #region " PROGRESS BAR "
        private void timer1_Tick(object sender, EventArgs e)
        {
            pbarform.ProgressBar1.Increment(1);
            if (pbarform.ProgressBar1.Value == 5)
            {
                pbarform.Label2.Text = ("Удаляем бомку...");
                try
                {
                    DeleteBom();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Проблема с удалением BOM");
                }
            }
            if (pbarform.ProgressBar1.Value == 15)
            {
                pbarform.Label2.Text = ("Вставляем бомку...");
                try
                {
                    InsertBom();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Проблема со вставкой BOM");
                }

            }
            if (pbarform.ProgressBar1.Value == 55)
            {
                pbarform.Label2.Text = ("Получаем XML...");
                try
                {
                    GetXMLfromBOM();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Проблема с получением XML");
                }
            }
            if (pbarform.ProgressBar1.Value == 90)
            {
                pbarform.Label2.Text = ("Скрываем бомку...");
                try
                {
                    hidebomtable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Проблема скрыть BOM");
                }
            }

            if (pbarform.ProgressBar1.Value == 95)
            {
                pbarform.Label2.Text = ("Запуск 1с...");
                try
                {
                    Run1c();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Проблема с запуском 1с");
                }
            }

            if (pbarform.ProgressBar1.Value == pbarform.ProgressBar1.Maximum)
            {
                pbarform.Close();

            }

            pbarform.Label1.Text = pbarform.ProgressBar1.Value + (" %");
        }
        #endregion

        #region " Удаление БОМ-ки "
        public void DeleteBom()
        {
            SldWorks swapp = default(SldWorks);
            ModelDoc2 swmodel = default(ModelDoc2);
            swapp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            swmodel = swapp.ActiveDoc;
            Feature swFeat = swmodel.FirstFeature();

            while ((swFeat != null))
            {
                if (swFeat.GetTypeName() == "BomFeat")
                {
                    swFeat.Select(true);
                    swmodel.EditDelete();
                    swFeat = swmodel.FirstFeature();
                }
                swFeat = swFeat.GetNextFeature();
            }
        }
        #endregion

        #region " Вставляем BOM-ку "
        public void InsertBom()
        {
            SldWorks swApp = default(SldWorks);
            ModelDoc2 swModel = default(ModelDoc2);
            SelectionMgr swSelMgr = default(SelectionMgr);
            FeatureManager swFeatMgr = default(FeatureManager);
            SolidWorks.Interop.sldworks.View swView;
            BomTableAnnotation swBomAnn = default(BomTableAnnotation);
            BomFeature swBomFeat = default(BomFeature);
            long anchorType = 0;
            long bomType = 0;
            string configuration = null;
            string tableTemplate = null;

            object visible = null;
            DrawingDoc swDraw;
            Sheet swSheet = default(Sheet);

            //swApp = DirectCast(Marshal.GetActiveObject("SldWorks.Application"), SldWorks)
            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            swModel = swApp.ActiveDoc;
            swFeatMgr = swModel.FeatureManager;
            swDraw = (DrawingDoc)swModel;
            swSheet = swDraw.GetCurrentSheet();

            swModel.Extension.SetUserPreferenceString((int)swUserPreferenceStringValue_e.swDetailingLayer, (int)swUserPreferenceOption_e.swDetailingBillOfMaterial, "");
            //ecли FALSE вставляются все конфигурации
            swModel.Extension.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swOneConfigOnlyTopLevelBom, 0, false);

            swModel.Extension.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swBomTableDontAddQTYNextToConfigName, 0, true);
            swModel.Extension.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swDontCopyQTYColumnNameFromTemplate, 0, true);
            //swDraw.SetUserPreferenceIntegerValue(swBomTableZeroQuantityDisplay, swZeroQuantityBlank);

            //Select View
            swModel.ClearSelection2(true);
            swView = swDraw.GetCurrentSheet().GetViews()[0];

            //Insert BOM Table
            anchorType = (int)swBOMConfigurationAnchorType_e.swBOMConfigurationAnchor_TopLeft;
            bomType = (int)swBomType_e.swBomType_TopLevelOnly;

            swModel.ClearSelection2(true);

            configuration = "";

            tableTemplate = "C:\\Program Files\\SW-Complex\\Template.sldbomtbt";

            swBomAnn = swView.InsertBomTable2(false, -0, -0, (int)anchorType, (int)bomType, configuration, tableTemplate);

            swFeatMgr.UpdateFeatureTree();

            swBomFeat = swBomAnn.BomFeature;

            var Names = swBomFeat.GetConfigurations(false, visible);
            visible = true;
            swBomFeat.SetConfigurations(true, visible, Names);
        }
        #endregion

        #region " Дерево XML "
        ModelDocExtension swModelDocExt = default(ModelDocExtension);

        BomFeature swBomFeature = null;
        CustomPropertyManager swConfigProp = default(CustomPropertyManager);
        CustomPropertyManager swCustProp = default(CustomPropertyManager);
        bool ok = false;
        string[] vConfName = null;
        SelectionMgr swSelMgr = default(SelectionMgr);
        object vConfNameArr = null;

        public void GetXMLfromBOM()
        {

            swapp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            swmodel = swapp.ActiveDoc;

            swModelDocExt = swmodel.Extension;

            //создаем MemoryStream, в который будем писать XML 
            var myMemoryStream = new MemoryStream();

            //создаем XmlTextWriter, указываем объект – myMemoryStream, в который будем писать XML, и кодировку
            try
            {
                var myXml = new System.Xml.XmlTextWriter("C:\\Program Files\\SW-Complex\\SP-Temp.xml", System.Text.Encoding.UTF8);
                swDraw = (DrawingDoc)swmodel;
                vSheetNames = swDraw.GetSheetNames();
                ok = swDraw.ActivateSheet(vSheetNames[0]);
                swView = swDraw.GetFirstView();

                // Получаем параметры модели
                swView = swView.GetNextView();
                swmodel = swView.ReferencedDocument;

                //swSelMgr = swDraw.SelectionManager;

                myXml.WriteStartDocument();
                myXml.Formatting = System.Xml.Formatting.Indented;

                //длина отступа
                myXml.Indentation = 2;
                vConfName = swmodel.GetConfigurationNames();


                swapp = new SldWorks();
                swmodel = swapp.ActiveDoc;

                Feature swFeat = swmodel.FirstFeature();

                while ((swFeat != null))
                {
                    if (swFeat.GetTypeName() == "BomFeat")
                    {
                        swFeat.Select(true);
                        swBomFeature = swFeat.GetSpecificFeature2();
                    }
                    swFeat = swFeat.GetNextFeature();

                }

                //////////////////////////////////////////////////////
                //                                                   
                //           GetPropertyBomTableFromDrawDoc          
                //                                                   
                //////////////////////////////////////////////////////

                object vConfigurations = null;
                object vVisibility = null;
                bool bGetVisible = false;
                long lNumRow = 0;
                long lNumColumn = 0;
                int lRow = 0;
                TableAnnotation swTableAnnotation = default(TableAnnotation);
                ModelDoc2 swDocument = default(ModelDoc2);
                AssemblyDoc swAssembly = default(AssemblyDoc);
                int lStartRow = 0;
                string strItemNumber = "";
                string strPartNumber = "";
                string strDescription = "";

                var strDocumentName = swBomFeature.GetReferencedModelName();

                swDocument = swapp.GetOpenDocumentByName(strDocumentName);
                swAssembly = (AssemblyDoc)swDocument;

                //swBOMTableAnnotation = swBomFeature.GetTableAnnotations(0)
                var swBomTableAnnotation = (BomTableAnnotation)swBomFeature.GetTableAnnotations()[0];

                swTableAnnotation = (TableAnnotation)swBomTableAnnotation;

                lNumRow = swTableAnnotation.RowCount;
                lNumColumn = swTableAnnotation.ColumnCount;

                lStartRow = 1;

                //If (Not (swTableAnnotation.TitleVisible = False)) Then
                if (swTableAnnotation.TitleVisible == false)
                {
                    lStartRow = 2;
                }

                bGetVisible = false;
                vConfigurations = swBomFeature.GetConfigurations(bGetVisible, vVisibility);

                swTableAnnotation = swTableAnnotation;

                ///////////////////////////////////////////////////////////////////

                swSheet = swDraw.GetCurrentSheet();
                strActiveSheetName = swSheet.GetName();
                vSheetNames = swDraw.GetSheetNames();
                ok = swDraw.ActivateSheet(vSheetNames[0]);
                swSheet = swDraw.GetCurrentSheet();
                swView = swDraw.GetFirstView();

                if (swSheet.CustomPropertyView == "По умолчанию" | swSheet.CustomPropertyView == "Default")
                {
                    swView = swView.GetNextView();
                }

                // get custom property

                var valout = ""; //Обозначение
                var valout1 = ""; //Наименование


                CustomPropertyManager swCustProp = default(CustomPropertyManager);
                var resolvedValOut = "";
                var resolvedValOut1 = "";
                //string resolvedValOut3 = "";
                //string resolvedValOut4 = "";
                //string resolvedValOut5 = "";
                swCustProp = swmodel.Extension.CustomPropertyManager[""];
                swCustProp.Get2("Обозначение", out valout, out resolvedValOut);
                swCustProp.Get2("Description", out valout1, out resolvedValOut1);

                // переменные для колонок

                int Jj = 0; // наименование
                int aa = 0; // раздел
                int oo = 0; // обозначение
                int tt = 0; // формат
                int yy = 0; // 
                int uu = 0; // Код материала
                int ss = 0; // Примечание

                string sRowStr = null; // наименовани
                string sRowStr1 = null; // раздел
                string sRowStr2 = null; // обозначение
                string sRowStr3 = null; // формат
                string sRowStr4 = null; // ERP code
                string sRowStr5 = null; // Код материала
                string sRowStr6 = null; // Примечание

                // При выборе электромонтажа
                string Complect = "";

                if (addinform.ChkElectro1.Checked)
                {
                    Complect = "МЭ";
                }
                else if (addinform.ChkElectro2.Checked)
                {
                    Complect = "ТБ";
                }

                ////////////////////////////////////////////////////////////
                //                                                         
                //                          XML                             
                //                                                         
                ////////////////////////////////////////////////////////////

                //создаем элементы
                myXml.WriteStartElement("xml");
                //
                myXml.WriteStartElement("Item");

                // имя пути основного чертежа
                myXml.WriteStartElement("PathName");
                //записываем строку
                myXml.WriteString(swmodel.GetPathName());
                myXml.WriteEndElement();

                // Устанавливаем ДОК из формы добавление
                myXml.WriteStartElement("doc");
                //
                myXml.WriteStartElement("Обозначение");
                //записываем строку
                myXml.WriteString(resolvedValOut + "CБ");
                myXml.WriteEndElement();
                //
                myXml.WriteStartElement("Наименование");
                myXml.WriteString("Сборочный чертеж");
                myXml.WriteEndElement();
                //
                myXml.WriteEndElement(); //doc

                //dynamic CheckedRows2 = (from Rows in addinform.DGDoc.Rows.Cast<DataGridViewRow>()where Convert.ToBoolean(Rows.Cells(0).Value) == true).ToList;

                dynamic CheckedRows2 = (from Rows in addinform.DGDoc.Rows.Cast<DataGridViewRow>() where Convert.ToBoolean(Rows.Cells[0].Value.ToString()) select Rows).ToList();


                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                foreach (DataGridViewRow row in CheckedRows2)
                {
                    sb.AppendLine(row.Cells[1].Value.ToString());
                    sb.ToString();

                    //Оставляем первые две буквы для обозначения разделитель

                    var literal = row.Cells[1].Value.ToString();
                    var substring = literal.Substring(0, 2);

                    var literal2 = row.Cells[1].Value.ToString();
                    var substring2 = literal2.Substring(5);

                    myXml.WriteStartElement("doc");
                    ///
                    myXml.WriteStartElement("Обозначение");
                    //записываем строку
                    myXml.WriteString(resolvedValOut + substring);
                    myXml.WriteEndElement();
                    ///
                    myXml.WriteStartElement("Наименование");
                    myXml.WriteString(substring2);
                    myXml.WriteEndElement();
                    ///
                    myXml.WriteEndElement();
                    //doc
                }


                //<------------- Elec 
                if (addinform.ChkElectro.Checked == true & addinform.ChkElectro1.Checked == false & addinform.ChkElectro2.Checked == false)
                {
                    myXml.WriteStartElement("Elec");
                    //Elec

                    myXml.WriteString("Устанавливают при электромонтаже");
                    myXml.WriteEndElement();
                    //Elec

                }

                //<------------- ChkElectro1
                if (addinform.ChkElectro.Checked == true & addinform.ChkElectro1.Checked == true)
                {
                    myXml.WriteStartElement("Elec");
                    //Elec
                    myXml.WriteString("Устанавливают по " + resolvedValOut + Complect);
                    myXml.WriteEndElement();
                    //Elec
                }

                //<------------- ChkElectro2
                if (addinform.ChkElectro.Checked == true & addinform.ChkElectro2.Checked == true)
                {
                    myXml.WriteStartElement("Elec");
                    //Elec
                    myXml.WriteString("Устанавливают по " + resolvedValOut + Complect);
                    myXml.WriteEndElement();
                    //Elec
                }

                //
                myXml.WriteStartElement("Обозначение");
                //записываем строку
                myXml.WriteString(resolvedValOut);
                myXml.WriteEndElement();
                ///
                myXml.WriteStartElement("Наименование");
                myXml.WriteString(resolvedValOut1);
                myXml.WriteEndElement();

                //\reference
                myXml.WriteStartElement("references");

                // ВЫГРУЖАЕМ ВЫБРАННЫЕ КОНФИГУРАЦИИ

                dynamic CheckedRows = (from Rows in DataGridConfig.Rows.Cast<DataGridViewRow>() where Convert.ToBoolean(Rows.Cells[0].Value.ToString()) select Rows).ToList();
                System.Text.StringBuilder sb2 = new System.Text.StringBuilder();

                foreach (DataGridViewRow row in CheckedRows)
                {
                    sb2.AppendLine(row.Cells[1].Value.ToString());

                    swmodel = swView.ReferencedDocument;
                    var configuration = swView.ReferencedConfiguration;

                    var sConfigName = swView.ReferencedConfiguration;
                    Configuration swConfig = swmodel.GetConfigurationByName(sConfigName);

                    for (var i = 0; i <= vConfName.GetUpperBound(0); i++)
                    {
                        const string ucase = "";

                        if (vConfName[i] == row.Cells[1].Value.ToString())
                        {
                            configuration = vConfName[i];
                        }
                    }

                    swView.ReferencedConfiguration = sConfigName;

                    myXml.WriteStartElement("config");
                    myXml.WriteAttributeString("value", row.Cells[1].Value.ToString());


                    /////////////////////////////////////////////////////////////
                    //
                    //                           PART
                    //
                    /////////////////////////////////////////////////////////////

                    for (lRow = lStartRow; lRow <= (lNumRow - 1); lRow++)
                    {


                        if (swBomTableAnnotation.GetComponentsCount2((int)lRow, row.Cells[1].Value.ToString(), out strItemNumber, out strPartNumber) > 0)
                        {

                            myXml.WriteStartElement("part");
                            //PathNameComponent
                            string strModelPathName = null;
                            var vModelPathNames = swBomTableAnnotation.GetModelPathNames((int)lRow, out strItemNumber, out strPartNumber);
                            if (((vModelPathNames != null)))
                            {
                                myXml.WriteStartElement("PathNameComponent");
                                foreach (var vModelPathName_loopVariable in vModelPathNames)
                                {
                                    var vModelPathName = vModelPathName_loopVariable;
                                    strModelPathName = vModelPathName;
                                    myXml.WriteString(strModelPathName);
                                }
                                myXml.WriteEndElement();

                            }
                            //'\ Row
                            myXml.WriteStartElement("Row");
                            myXml.WriteString(Convert.ToString(lRow - lStartRow + 1));
                            myXml.WriteEndElement();
                            vModelPathNames = swBomTableAnnotation.GetModelPathNames((int)lRow, out strItemNumber, out strPartNumber);
                            //\ ItemNum
                            myXml.WriteStartElement("ItemNum");
                            myXml.WriteString(strItemNumber);
                            myXml.WriteEndElement();
                            //\ раздел
                            aa = 3;
                            /// раздел
                            sRowStr1 = "";
                            sRowStr1 = sRowStr1 + swTableAnnotation.Text[lRow, aa];
                            myXml.WriteStartElement("Раздел");
                            myXml.WriteString(sRowStr1);
                            myXml.WriteEndElement();
                            //\ обозначение
                            oo = 1;
                            /// обозначение
                            sRowStr2 = "";
                            sRowStr2 = sRowStr2 + swTableAnnotation.Text[lRow, oo];
                            myXml.WriteStartElement("Обозначение");
                            myXml.WriteString(sRowStr2);
                            myXml.WriteEndElement();
                            //\ наименование
                            Jj = 2;
                            /// наименование
                            sRowStr = "";
                            sRowStr = sRowStr + swTableAnnotation.Text[lRow, Jj];
                            myXml.WriteStartElement("Наименование");
                            myXml.WriteString(sRowStr);
                            myXml.WriteEndElement();
                            //\
                            tt = 4;
                            /// формат
                            sRowStr3 = "";
                            sRowStr3 = sRowStr3 + swTableAnnotation.Text[lRow, tt];
                            myXml.WriteStartElement("Формат");
                            myXml.WriteString(sRowStr3);
                            myXml.WriteEndElement();
                            //\
                            yy = 5;
                            /// ERP code
                            sRowStr4 = "";
                            sRowStr4 = sRowStr4 + swTableAnnotation.Text[lRow, yy];
                            myXml.WriteStartElement("ERP_code");
                            myXml.WriteString(sRowStr4);
                            myXml.WriteEndElement();
                            //\
                            uu = 6;
                            /// Код материала
                            sRowStr5 = "";
                            sRowStr5 = sRowStr5 + swTableAnnotation.Text[lRow, uu];
                            myXml.WriteStartElement("Код_материала");
                            myXml.WriteString(sRowStr5);
                            myXml.WriteEndElement();
                            //\
                            ss = 7;
                            /// наименование
                            sRowStr6 = "";
                            sRowStr6 = sRowStr6 + swTableAnnotation.Text[lRow, ss];
                            myXml.WriteStartElement("Примечание");
                            myXml.WriteString(sRowStr6);
                            myXml.WriteEndElement();
                            //\

                            myXml.WriteStartElement("Количество");
                            //myXml.WriteString(swTableAnnotation.GetComponentsCount2(lRow, strConfiguration, strItemNumber, strPartNumber))
                            myXml.WriteString(Convert.ToString(swBomTableAnnotation.GetComponentsCount2(lRow, row.Cells[1].Value.ToString(), out strItemNumber, out strPartNumber)));
                            myXml.WriteEndElement();
                            myXml.WriteEndElement();
                            //config
                        }
                    }


                    myXml.WriteEndElement(); //part


                }


                myXml.WriteEndElement(); //references
                //<~~~~~~~~~~~~~~~~~~~~~~~~~~~ Get the Total Number of Rows


                Annotation swAnn = default(Annotation);
                TableAnnotation swTable = default(TableAnnotation);
                long nNumRow = 0;

                swmodel = swapp.ActiveDoc;

                swView = swDraw.GetFirstView();
                swTable = swView.GetFirstTableAnnotation();
                swAnn = swTable.GetAnnotation();

                nNumRow = swTable.RowCount;
                myXml.WriteStartElement("TotalRows");
                myXml.WriteString(Convert.ToString(nNumRow));
                myXml.WriteEndElement();
                //end TotalRows

                myXml.WriteEndElement();
                //Item
                myXml.WriteEndElement();
                //элемент XML

                //End If
                //заносим данные в myMemoryStream 
                myXml.Flush();
                myXml.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }


        }
        #endregion

        #region " Скрываем БОМ-ку "
        public void hidebomtable()
        {
            SldWorks swApp = default(SldWorks);
            ModelDoc2 swModel = default(ModelDoc2);
            DrawingDoc swDraw = default(DrawingDoc);
            SolidWorks.Interop.sldworks.View swView = default(SolidWorks.Interop.sldworks.View);
            TableAnnotation swTableAnn = default(TableAnnotation);
            Annotation swAnn = default(Annotation);
            object[] vTableAnns = null;
            int i = 0;

            //swApp = CreateObject("SldWorks.Application")
            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            swModel = swApp.ActiveDoc;
            swDraw = (DrawingDoc)swModel;
            swView = swDraw.GetFirstView();
            vTableAnns = swView.GetTableAnnotations();
            //vTableAnns.Length
            if ((vTableAnns == null) == false)
            {
                for (i = 0; i <= vTableAnns.Length - 1; i++)
                {
                    swTableAnn = (TableAnnotation)vTableAnns[i];
                    if (swTableAnn.Type == (int)swTableAnnotationType_e.swTableAnnotation_BillOfMaterials)
                    {
                        //swTableAnn.MoveColumn(0, swTableItemInsertPosition_e.swTableItemInsertPosition_After, 1)
                        swAnn = swTableAnn.GetAnnotation();
                        swAnn.Select3(false, null);
                        swApp.RunCommand((int)swCommands_e.swCommands_Hide_Table, null);
                    }
                }
            }

        }
        #endregion

        #region " Запуск 1с "


        public void Run1c()
        {
            try
            {
                SettingForm SettingConnect = new SettingForm();

                string kye = Properties.Settings.Default.Key1c;
                string logn = Properties.Settings.Default.LoginUser;
                string pas = Properties.Settings.Default.PasswordUser;

                ProcessStartInfo p = new ProcessStartInfo();

                //Путь к программе 1С Предприятие
                //p.FileName = "C:\Program Files (x86)\1cv82\common\1cestart.exe"
                p.FileName = Properties.Settings.Default.Path1c;

                // Use these arguments for the process
                p.Arguments = kye + "/N\"" + logn + "\"" + "/P" + "\"" + pas + "\"";

                ////////////
                Process[] Proc = null;
                string ModuleName = null;
                string ProcName = null;
                ModuleName = "1cv8.exe";
                ProcName = System.IO.Path.GetFileNameWithoutExtension(ModuleName);
                Proc = Process.GetProcessesByName(ProcName);
                if (Proc.Length > 0)
                {
                    //Nothing
                }
                else
                {
                    //Start the process
                    Process.Start(p);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
    
        }

        #endregion

        #region ADD ALL CHECKBOX DATAGRIDVIEW HEADER
        private bool _IsSelectAllChecked;
        private void AddSelectAllCheckBox(DataGridView theDataGridView)
        {
            try
            {
                CheckBox cbx = new CheckBox();
                cbx.Name = "SelectAll";
                cbx.Size = new Size(14, 14);
                Rectangle rect;
                rect = theDataGridView.GetCellDisplayRectangle(0, -1, true);
                cbx.Location = new System.Drawing.Point(rect.Location.X + ((rect.Width - cbx.Width) / 2), rect.Location.Y + ((rect.Height - cbx.Height) / 2));
                cbx.BackColor = Color.White;
                theDataGridView.Controls.Add(cbx);
                cbx.Click += HeaderCheckBox_Click;
                theDataGridView.CellValueChanged += DataGridView_CellChecked;
                theDataGridView.CurrentCellDirtyStateChanged += DataGridView_CurrentCellDirtyStateChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void HeaderCheckBox_Click(object sender, EventArgs e)
        {
            try
            {

                this._IsSelectAllChecked = true;
                CheckBox cbx;
                cbx = (CheckBox)sender;
                var theDataGridView = (DataGridView)cbx.Parent;
                //<~~~~~ выбираем чекбокс в шапке, кнопка Создать вкл/выкл
                foreach (DataGridViewRow row in theDataGridView.Rows)
                {
                    row.Cells[0].Value = cbx.Checked;
                    BtCreate.Enabled = true;

                    if (Convert.ToBoolean(row.Cells[0].Value) == false)
                    {
                        BtCreate.Enabled = false;

                    }
                }

                theDataGridView.EndEdit();
                this._IsSelectAllChecked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DataGridView_CellChecked(System.Object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {

                DataGridView dataGridView = (DataGridView)sender;
                bool isChecked = Convert.ToBoolean(DataGridConfig.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if (!this._IsSelectAllChecked)
                {
                    if (Convert.ToBoolean(dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == false)
                    {
                        bool isAllUnChecked = false;
                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            if (Convert.ToBoolean(row.Cells[0].Value))
                            {
                                isAllUnChecked = true;
                                break;
                            }
                        }
                        //((CheckBox)dataGridView.Controls.Item("SelectAll")).Checked = false;
                        ((CheckBox)dataGridView.Controls["SelectAll"]).Checked = false;

                        //dataGridView.Controls[]

                        BtCreate.Enabled = isAllUnChecked;

                    }
                    else
                    {
                        bool isAllChecked = true;
                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            if (Convert.ToBoolean(row.Cells[0].Value) == false)
                            {
                                isAllChecked = false;
                                break;
                            }
                        }
                        //((CheckBox)dataGridView.Controls.Item("SelectAll")).Checked = isAllChecked;
                        ((CheckBox)dataGridView.Controls["SelectAll"]).Checked = isAllChecked;

                        BtCreate.Enabled = isAllChecked;

                        if (isChecked == true)
                        {
                            BtCreate.Enabled = true;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DataGridView_CurrentCellDirtyStateChanged(System.Object sender, System.EventArgs e)
        {
            try
            {
                DataGridView dataGridView = (DataGridView)sender;
                if (dataGridView.CurrentCell is DataGridViewCheckBoxCell)
                {
                    dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region " Проверка "
        public void check()
        {
            int m = 0;
            //swapp = CreateObject("SldWorks.Application")
            swapp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            swmodel = swapp.ActiveDoc;

            // Проверка открытого документа
            if (swmodel == null)
            {
                swapp.SendMsgToUser("Откройте чертеж");
                return;
            }
            //Or swDocumentTypes_e.swDocASSEMBLY
            if (swmodel.GetType() != (int)swDocumentTypes_e.swDocDRAWING)
            {
                swapp.SendMsgToUser("Откройте чертеж");
                return;
            }

            if (string.IsNullOrEmpty(swmodel.GetPathName()))
            {
                swapp.SendMsgToUser("Сохраните чертеж!");
                Close();
                return;
            }

            swDraw = (DrawingDoc)swmodel;

            // Получение первого листа
            swSheet = swDraw.GetCurrentSheet();
            strActiveSheetName = swSheet.GetName();
            // Узнаем имя активного листа
            vSheetNames = swDraw.GetSheetNames();
            ok = swDraw.ActivateSheet(vSheetNames[0]);
            swSheet = swDraw.GetCurrentSheet();

            swView = swDraw.GetFirstView();
            //m = 0

            //Debug.Print(swSheet.CustomPropertyView);

            if (swSheet.CustomPropertyView == "По умолчанию" | swSheet.CustomPropertyView == "Default")
            {
                swView = swView.GetNextView();
                // Получаем первый вид
            }
            else
            {
                while (!(swView == null))
                {
                    if (swView.GetName2() == swSheet.CustomPropertyView)
                    {
                        m = 1;
                        break;
                    }
                    swView = swView.GetNextView();
                }
                if (m == 0)
                {
                    swView = swDraw.GetFirstView();
                    swView = swView.GetNextView();
                    swapp.SendMsgToUser("Не удалось определить вид из свойств листа. Ипользуется первый вид.");
                }
            }

            if (swView == null)
            {
                swapp.SendMsgToUser("Отсутсвует модель!");
                // Возвращение активного листа
                ok = swDraw.ActivateSheet(strActiveSheetName);
                return;
            }

            if (swView.ReferencedDocument == null)
            {
                swapp.SendMsgToUser("Отсутсвует модель!");
                // Возвращение активного листа
                ok = swDraw.ActivateSheet(strActiveSheetName);
                return;
            }

            swmodel = swView.ReferencedDocument;
            if (swmodel.GetType() != (int)swDocumentTypes_e.swDocASSEMBLY)
            {
                swapp.SendMsgToUser("Чертеж должен быть сборочным!");
                // Возвращение активного листа
                ok = swDraw.ActivateSheet(strActiveSheetName);
                return;
            }
        }
        #endregion

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAddin_Click(object sender, EventArgs e)
        {

            if (addinform.Visible == false)
            {
                addinform.Visible = true;
            }

           
        }
       
    }
}
