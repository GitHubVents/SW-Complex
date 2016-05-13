using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml;
using SolidWorks.Interop.sldworks;


namespace SwSpecification
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class UcTableToDrwDoc
    {
        public UcTableToDrwDoc()
        {
            InitializeComponent();
        }

        private SldWorks _swApp;
        public ModelDoc2 _swModel;
        private SelectionMgr swSelMgr;
        private View _swView;
        public DrawingDoc _swDraw;
        private Sheet _swSheet;
        public string StrActiveSheetName { get; set; } // Имя листа, который был активен при открытии чертежа
        string[] _vSheetNames;
        private string[] vConfNameArr;
        private string[] vCustInfoNameArr; // Массив свойств файла
        private bool _ok;
        string[] vCustNames;
        string vCustTypes = "";
        string vCustVals = "";
        //public const string XmlPathName = @"C:\Program Files\SW-Complex\DrawTable.xml";
        public string XmlPathName = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\SW-Complex\\DrawTable.xml";

        private NewTableForm _newTable;
        readonly ClassPropertySldWorks _classPropSld = new ClassPropertySldWorks();
        private void GridHome_Loaded(object sender, RoutedEventArgs e)
        {
            CheckDrawingDoc();
            LoadComboBox();
        }
        private void CboTableName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadDataGrid();
        }
        public void LoadDataGrid()
        {
            try
            {
                
                DgLoadTable.Columns.Clear();

                var columnNameXml = GetXmlColName();

                for (var i = 0; i <= columnNameXml.Count - 1; i++)
                {

                    var columnName = _classPropSld.ListColumnBinding().Where(x => x.PropertiesName == columnNameXml[i]).Select(y => y.Binding);

                    var colName = new DataGridTextColumn
                    {
                        Header = columnNameXml[i],
                        Binding = new Binding(columnName.Single()),
                        Width = DataGridLength.Auto
                    };

                    DgLoadTable.Columns.Add(colName);

                }

                DgLoadTable.ItemsSource = ClassPropertySldWorks.ListColumn(_swModel);

                //((DataGridTextColumn)DgLoadTable.Columns[0]).Binding = new Binding(".");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        static public string TableName { get; set; }
        public List<string> GetXmlColName()
        {
            var strList = new List<string>();

            var textReader = new XmlTextReader(XmlPathName);

            while (textReader.Read())
            {
                if (textReader.NodeType == XmlNodeType.Element)
                {
                    if (textReader.Name == "TableName")
                    {
                        TableName = textReader.GetAttribute("value");
                    }

                    var sItemAdType = ((string)CboTableName.SelectedItem);

                    if (sItemAdType == TableName)
                    {
                        if (textReader.Name == "Col")
                        {
                            var colName = textReader.GetAttribute("value");

                            strList.Add(colName);
                        }
                    }
                }
            }

            textReader.Close();

            return strList;
        }
        public void LoadComboBox()
        {
            try
            {

                if (File.Exists(XmlPathName) == false) return;

                var textReader = new XmlTextReader(XmlPathName);

                while (textReader.Read())
                {
                    if (textReader.NodeType == XmlNodeType.Element)
                    {
                        if (textReader.Name == "TableName")
                        {
                            var tableName = textReader.GetAttribute("value");
                            CboTableName.Items.Add(tableName);
                            //CboTableName.SelectedIndex = 0;
                        }
                    }
                }

                textReader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnAddTable_Click(object sender, RoutedEventArgs e)
        {
            //GetAllPropertiesName();
            if (CboTableName.Text != "")
            {
                Rebuild();
                InsertTable();
                Close();
            }
            else
            {
                MessageBox.Show("Выберете таблицу!");
            }
        }
        ModelDocExtension _swModelDocExtComp;
        CustomPropertyManager _swCustPropMgrComp;
        public void GetAllPropertiesName()
        {
            _swModelDocExtComp = _swModel.Extension;

            var propNames = default(object);
            var propTypes = default(object);
            var propValues = default(object);

            _swCustPropMgrComp = _swModelDocExtComp.CustomPropertyManager[String.Empty];

            _swCustPropMgrComp.GetAll(ref propNames, ref propTypes, ref propValues);

            var asCustPropName = (string[])propNames;
            //var asCustPropValues = (string[])propValues;

            for (var i = 0; i < asCustPropName.GetUpperBound(0) - 1; i++)
            {
                MessageBox.Show(asCustPropName[i]);
            }  
        }
        public void CheckDrawingDoc()
        {
            try
            {
                _swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
                _swModel = _swApp.ActiveDoc;

                _swDraw = (DrawingDoc)_swModel;

                // Получение первого листа
                _swSheet = _swDraw.GetCurrentSheet();
                StrActiveSheetName = _swSheet.GetName();

                // Узнаем имя активного листа
                _vSheetNames = _swDraw.GetSheetNames();
                _swDraw.ActivateSheet(_vSheetNames[0]);
                _swSheet = _swDraw.GetCurrentSheet();

                _swView = _swDraw.GetFirstView();

                if (_swSheet.CustomPropertyView == "По умолчанию" | _swSheet.CustomPropertyView == "Default")
                {
                    _swView = _swView.GetNextView();
                }

                _swModel = _swView.ReferencedDocument;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Rebuild()
        {
            //PbLoadAddTable.Minimum = 0;

            //var confArray = ClassPropertySldWorks.ListColumn(_swModel);
            //PbLoadAddTable.Maximum = confArray.Count;
            //PbLoadAddTable.Value = 0;

            //double value = 0;

            // to the ProgressBar's SetValue method.
            //UpdateProgressBarDelegate updatePbDelegate = PbLoadAddTable.SetValue;

            string[] arrayConfig = _swModel.GetConfigurationNames();

            foreach (var confName in from confName in arrayConfig
                                     let swConf = _swModel.GetConfigurationByName(confName)
                                     where ((Configuration) swConf).IsDerived() == false select confName)
            {
                _swModel.ShowConfiguration2(confName);

                //value += 1;

                _swModel.ForceRebuild3(false);

                //Dispatcher.Invoke(updatePbDelegate, System.Windows.Threading.DispatcherPriority.Background, new object[] { ProgressBar.ValueProperty, value });
            }

            //PbLoadAddTable.Value = 0;
        }
        #region "Add or Delete table"
        public void DeleteTable()
        {
            try
            {

                _swModel.Extension.SelectByID2("", "ANNOTATIONTABLES", 0, 0, 0, false, 0, null, 0);
                _swModel.EditDelete();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private delegate void UpdateProgressBarDelegate(DependencyProperty dp, Object value);
        public void InsertTable()
        {
            try
            {
                //DeleteTable();

                var sldPropName = _classPropSld.ListColumnBinding();

                var columnNameXml = GetXmlColName();

                var myTable = _swDraw.InsertTableAnnotation(-0, 0, 1, ClassPropertySldWorks.ListColumn(_swModel).Count() + 1, GetXmlColName().Count);

                if ((myTable == null)) return;

                myTable.BorderLineWeight = 2;
                myTable.GridLineWeight = 1;

                for (var i = 0; i <= columnNameXml.Count - 1; i++)
                {
                    var column = sldPropName.Where(x => x.PropertiesName == columnNameXml[i]).Select(y => y.PropertiesName);

                    myTable.Text[0, i] = column.Single();

                    myTable.SetColumnWidth(i, 0.05, 0);

                    for (var row = 0; row < DgLoadTable.Items.Count; row++)
                    {
                        var b = DgLoadTable.Columns[i].GetCellContent(DgLoadTable.Items[row]) as TextBlock;

                        myTable.Text[row + 1, i] = b.Text;

                        //if (row > 1)
                        //{
                        //    myTable.set_CellTextHorizontalJustification(row, 0, (int)swTextJustification_e.swTextJustificationRight);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        private void AddNewTable_Click(object sender, RoutedEventArgs e)
        {
            _newTable = new NewTableForm(_swModel) {MainForm = this};
            //_newTable.ShowDialog();
            _newTable.ShowDialog();
        }
        private void BtnDeleteTable_Click(object sender, RoutedEventArgs e)
        {
            if (CboTableName.Text != "")
            {
                DeleteXmlTable();
            }
            else
            {
                MessageBox.Show("Выберете таблицу для удаления!");
            }
        }
        public void DeleteXmlTable()
        {
            try
            {
            // Удалить xml тэг
  
            //var doc = new XmlDocument();
            //doc.Load(@"C:\test.xml");
            //var root = doc.DocumentElement;

            //MessageBox.Show(CboTableName.SelectedItem.ToString());

            //var node = root.SelectSingleNode(String.Format("TableName[value='{0}']", CboTableName.SelectedItem));
            //var outer = node.ParentNode;
            //outer.RemoveChild(node);
            //doc.Save(@"C:\test.xml");

            // Удалить xml атрибут

            var doc = new XmlDocument();
            doc.Load(XmlPathName);

            if (doc.DocumentElement != null)
            {
                var cl = doc.DocumentElement.ChildNodes;
                foreach (var n in from XmlNode n in cl where n.Attributes != null && 
                                        n.Attributes["value"].Value == CboTableName.SelectedItem.ToString() select n)
                    doc.DocumentElement.RemoveChild(n);
            }

            using (var writer = new StreamWriter(XmlPathName))
            {
                doc.Save(writer);
            }

            //doc.Save(XmlPathName);
  
            CboTableName.Items.Clear();

            LoadComboBox();

            DgLoadTable.ItemsSource = null;
                     
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }  
    }
}