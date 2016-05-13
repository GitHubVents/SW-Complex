using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using SolidWorks.Interop.sldworks;

namespace SwSpecification
{
    /// <summary>
    /// Interaction logic for NewTableForm.xaml
    /// </summary>
    public partial class NewTableForm : Window
    {
        public SwSpecification.UcTableToDrwDoc MainForm { get; set; }   
        public ModelDoc2 SwModel;
        public NewTableForm(ModelDoc2 swModel)
        {
            InitializeComponent();
            SwModel = swModel;
        }
        readonly ClassPropertySldWorks _classPropSld = new ClassPropertySldWorks();
        private void GridNewTable_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var itemsListViewUsers = _classPropSld.ListColumnBinding().Select(items => new CheckBoxListItem(false, items.PropertiesName)).ToList();
                LvNewTableForm.ItemsSource = itemsListViewUsers;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public class CheckBoxListItem
        {
            public bool Checked { get; set; }
            public string PropertiesName { get; set; }
            public CheckBoxListItem(bool ch, string propertiesName)
            {
                Checked = ch;
                PropertiesName = propertiesName;
            }
        }
        public List<CheckBoxListItem> ReturnCheckedItems()
        {
            return LvNewTableForm.SelectedItems.Cast<CheckBoxListItem>().ToList();
        }
        // Свойства с модели
        public class PropertiesClass
        {
            public string PropertiesName { get; set; }
        }
        ModelDocExtension _swModelDocExtComp;
        CustomPropertyManager _swCustPropMgrComp;
        public List<PropertiesClass> ListProperties()
        {
            var listString = new List<PropertiesClass>();

            _swModelDocExtComp = SwModel.Extension;

            var propNames = default(object);
            var propTypes = default(object);
            var propValues = default(object);

            _swCustPropMgrComp = _swModelDocExtComp.CustomPropertyManager["00"];

            _swCustPropMgrComp.GetAll(ref propNames, ref propTypes, ref propValues);

            var asCustPropName = (string[])propNames;
            //var asCustPropValues = (string[])propValues;

            for (var i = 0; i < asCustPropName.GetUpperBound(0) - 1; i++)
            {
                var columnValue = new PropertiesClass
                {
                    PropertiesName = asCustPropName[i]
                };

                listString.Add(columnValue);
              }
            return listString;
        }
        public event EventHandler ButtonClicked;
        private void BtnSaveNewTable_Click(object sender, RoutedEventArgs e)
        {
            NewTable();

            //foreach (var columnName in ReturnCheckedItems().Select(propName => propName.PropertiesName))
            //{
            //    MessageBox.Show(columnName == "" ? "1" : columnName);
            //}
        }
        public void NewTable()
        {
            try
            {
                if (TxtNameNewTable.Text != "")
                {
                    //TODO: CONST XMLPATH

                    //var filePath = File.Exists(SwSpecification.UcTableToDrwDoc.XmlPathName);

                    var filePath = File.Exists(MainForm.XmlPathName);
                    if (filePath == false)
                    {
                        GenerateXml();
                    }
                    else
                    {
                        var doc = new XmlDocument();
                        //doc.Load(SwSpecification.UcTableToDrwDoc.XmlPathName);

                        doc.Load(MainForm.XmlPathName);

                        var tableName = doc.CreateElement("TableName");
                        tableName.SetAttribute("value", TxtNameNewTable.Text);

                        foreach (var columnName in ReturnCheckedItems().Select(propName => propName.PropertiesName))
                        {
                            var newTable = doc.CreateElement("Col");
                            newTable.SetAttribute("value", columnName);
                            tableName.AppendChild(newTable);
                        }

                        if (doc.DocumentElement != null) doc.DocumentElement.AppendChild(tableName);
                        //doc.Save(SwSpecification.UcTableToDrwDoc.XmlPathName);

                        doc.Save(MainForm.XmlPathName);
                    }

                    MainForm.CboTableName.Items.Clear();
                    MainForm.LoadComboBox();
                    MainForm.CboTableName.Text = TxtNameNewTable.Text;

                    Close();
                }
                else
                {
                    MessageBox.Show("Введите имя таблицы!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void GenerateXml()
        {
            try
            {
                //System.IO.MemoryStream myMemoryStream;
                //var myXml = new XmlTextWriter(SwSpecification.UcTableToDrwDoc.XmlPathName, Encoding.UTF8);

                var myXml = new XmlTextWriter(MainForm.XmlPathName, Encoding.UTF8);

                //создаем XML
                myXml.WriteStartDocument();

                // устанавливаем параметры форматирования xml-документа
                myXml.Formatting = Formatting.Indented;

                // длина отступа
                myXml.Indentation = 2;

                // создаем элементы
                // имя тэга
                myXml.WriteStartElement("xml");
                    myXml.WriteStartElement("TableName");

                    // имя новой таблицы
                    myXml.WriteAttributeString("value", TxtNameNewTable.Text);

                        foreach (var columnName in ReturnCheckedItems().Select(propName => propName.PropertiesName))
                        {
                            // Имя свойств
                            myXml.WriteStartElement("Col");
                            myXml.WriteAttributeString("value", columnName);
                            myXml.WriteEndElement();
                        }

                    myXml.WriteEndElement(); // имя новой таблицы
                    myXml.WriteEndElement(); // xml

                    // заносим данные в myMemoryStream 
                    myXml.Flush();
                    myXml.Close();
               
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            LvNewTableForm.SelectAll();
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            LvNewTableForm.UnselectAll();
        }
    }
}