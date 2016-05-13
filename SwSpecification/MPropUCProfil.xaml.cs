using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace SwSpecification
{
    public partial class MPropUcProfil : Window
    {
        public MPropUcProfil()
        {
            InitializeComponent();
        }
        //private const string XmlPathName = @"C:\Program Files\SW-Complex\Profile.xml";
        string XmlPathName = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\SW-Complex\\Profile.xml";
        readonly string[] _colArr = { "DrawnBy", "Chkd", "TSuperv", "ExaminedBy" };
        private string _razrab;
        private string _prov;
        private string _tKontrol;
        private string _ytv;
        public static int CboIndex;
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void CreateXml()
        {
            try
            {
                _razrab = TxtBoxRazrab.Text;
                _prov = TxtBoxProv.Text;
                _tKontrol = TxtBoxTKontrol.Text;
                _ytv = TxtBoxYtv.Text;

                if (_razrab == "" && _prov == "" && _tKontrol == "" && _ytv == "") return;

                //System.IO.MemoryStream myMemoryStream;
                var myXml = new XmlTextWriter(XmlPathName, Encoding.UTF8);

                //создаем XML
                myXml.WriteStartDocument();

                // устанавливаем параметры форматирования xml-документа
                myXml.Formatting = Formatting.Indented;

                // длина отступа
                myXml.Indentation = 2;

                // создаем элементы
                // имя тэга
                myXml.WriteStartElement("xml");

                var txtBoxArr = new[] { _razrab, _prov, _tKontrol, _ytv };

                myXml.WriteStartElement("Lang"); // Begin
                myXml.WriteAttributeString("value", "Русский");
                if (CboLang.SelectedIndex == 0)
                {
                    for (var i = 0; i <= txtBoxArr.GetUpperBound(0); i++)
                    {
                        myXml.WriteStartElement(_colArr[i]);
                        myXml.WriteString(txtBoxArr[i]);
                        myXml.WriteEndElement();
                    }
                }

                myXml.WriteEndElement(); // End


                myXml.WriteStartElement("Lang"); // Begin
                myXml.WriteAttributeString("value", "Английский");
                if (CboLang.SelectedIndex == 0)
                {
                    for (var i = 0; i <= txtBoxArr.GetUpperBound(0); i++)
                    {
                        myXml.WriteStartElement(_colArr[i]);
                        myXml.WriteString(txtBoxArr[i]);
                        myXml.WriteEndElement();
                    }
                }

                myXml.WriteEndElement(); // End
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
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                if (File.Exists(XmlPathName) == false)
                {
                    CreateXml();
                }
                else
                {
                    var doc = new XmlDocument();
                    doc.Load(XmlPathName);

                    _razrab = TxtBoxRazrab.Text;
                    _prov = TxtBoxProv.Text;
                    _tKontrol = TxtBoxTKontrol.Text;
                    _ytv = TxtBoxYtv.Text;

                    var txtBoxArr = new[] { _razrab, _prov, _tKontrol, _ytv };

                    var nodeDrawnBy = doc.SelectSingleNode("//Lang[@value = '" + CboLang.Text + "']/DrawnBy");
                    var nodeChkd = doc.SelectSingleNode("//Lang[@value = '" + CboLang.Text + "']/Chkd");
                    var nodeTSuperv = doc.SelectSingleNode("//Lang[@value = '" + CboLang.Text + "']/TSuperv");
                    var nodeExaminedBy = doc.SelectSingleNode("//Lang[@value = '" + CboLang.Text + "']/ExaminedBy");

                    if (nodeDrawnBy != null) nodeDrawnBy.InnerText = txtBoxArr[0];
                    if (nodeChkd != null) nodeChkd.InnerText = txtBoxArr[1];
                    if (nodeTSuperv != null) nodeTSuperv.InnerText = txtBoxArr[2];
                    if (nodeExaminedBy != null) nodeExaminedBy.InnerText = txtBoxArr[3];

                    doc.Save(XmlPathName);
                }

                CboIndex = CboLang.SelectedIndex;

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static string NodeDrawnByName;
        public static string NodeChkdName;
        public static string NodeTSupervName;
        public static string NodeExaminedByName;
        public static string NodeDrawnByNameEng;
        public static string NodeChkdNameEng;
        public static string NodeExaminedByNameEng;
        public void GridMPropUc_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                CboLang.Items.Add("Русский");
                CboLang.Items.Add("Английский");
                CboLang.SelectedIndex = 0;

                TxtBoxTKontrol.IsEnabled = true;
                if (!File.Exists(XmlPathName)) return;

                var doc = new XmlDocument();
                doc.Load(XmlPathName);

                _razrab = TxtBoxRazrab.Text;
                _prov = TxtBoxProv.Text;
                _tKontrol = TxtBoxTKontrol.Text;
                _ytv = TxtBoxYtv.Text;

                //rus
                var nodeDrawnBy = doc.SelectSingleNode("//Lang[@value = 'Русский']/DrawnBy");
                if (nodeDrawnBy != null) NodeDrawnByName = nodeDrawnBy.InnerText;
                var nodeChkd = doc.SelectSingleNode("//Lang[@value = 'Русский']/Chkd");
                if (nodeChkd != null) NodeChkdName = nodeChkd.InnerText;
                var nodeTSuperv = doc.SelectSingleNode("//Lang[@value = 'Русский']/TSuperv");
                if (nodeTSuperv != null) NodeTSupervName = nodeTSuperv.InnerText;
                var nodeExaminedBy = doc.SelectSingleNode("//Lang[@value = 'Русский']/ExaminedBy");
                if (nodeExaminedBy != null) NodeExaminedByName = nodeExaminedBy.InnerText;

                //eng
                var nodeDrawnByEng = doc.SelectSingleNode("//Lang[@value = 'Английский']/DrawnBy");
                if (nodeDrawnByEng != null) NodeDrawnByNameEng = nodeDrawnByEng.InnerText;
                var nodeChkdEng = doc.SelectSingleNode("//Lang[@value = 'Английский']/Chkd");
                if (nodeChkdEng != null) NodeChkdNameEng = nodeChkdEng.InnerText;
                var nodeExaminedByEng = doc.SelectSingleNode("//Lang[@value = 'Английский']/ExaminedBy");
                if (nodeExaminedByEng != null) NodeExaminedByNameEng = nodeExaminedByEng.InnerText;

                TxtBoxRazrab.Text = NodeDrawnByName;
                TxtBoxProv.Text = NodeChkdName;
                TxtBoxTKontrol.Text = NodeTSupervName;
                TxtBoxYtv.Text = NodeExaminedByName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CboLang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!File.Exists(XmlPathName)) return;

            if (CboLang.Text == "Английский")
            {
                TxtBoxTKontrol.IsEnabled = true;
                var doc = new XmlDocument();
                doc.Load(XmlPathName);
                var nodeTSuperv = doc.SelectSingleNode("//Lang[@value = 'Русский']/TSuperv");
                var nodeTSupervName = nodeTSuperv.InnerText;
                TxtBoxTKontrol.Text = nodeTSupervName;
            }
            else
            {
                TxtBoxTKontrol.IsEnabled = false;
                TxtBoxTKontrol.Text = "";
            }
        }
    }
}