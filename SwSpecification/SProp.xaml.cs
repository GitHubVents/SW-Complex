using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SolidWorks.Interop.sldworks;
using VentsMaterials;
using SolidWorks.Interop.swconst;

namespace SwSpecification
{
    /// <summary>
    /// Interaction logic for SProp.xaml
    /// </summary>
    public partial class SProp : Window
    {
        public SProp()
        {
            InitializeComponent();
            BindingComboBoxPartition();
        }

        ConnectSqlString con = new ConnectSqlString();
        public SldWorks SwApp;
        public ModelDoc2 SwModel;
        public string[] ConfigArray;

        int m; // 0 - Деталь, 1 - Сборка

        private void BindingComboBoxPartition()
        {
            try
            {
                SwApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
                SwModel = SwApp.ActiveDoc;

                var itemsArrayPart = new[] { "Стандартные изделия" ,
                    "Прочие изделия", "Материалы", "Комплекты",
                    "ЭМ-Стандартные изделия", "ЭМ-Прочие изделия", "ЭМ-Материалы" };

                var itemsArrayAssembly = new[] { "Стандартные изделия" ,
                    "Прочие изделия", "Комплекты",
                    "ЭМ-Стандартные изделия", "ЭМ-Прочие изделия"};

                string[] itemsArray;

                if ((SwModel.GetType() == (int)swDocumentTypes_e.swDocPART))
                {
                    itemsArray = itemsArrayPart;
                    m = 0;
                }
                else
                {
                    itemsArray = itemsArrayAssembly;
                    m = 1;
                }
                
                CboChangePartition.ItemsSource = itemsArray;

                var activConfiguration = SwModel.ConfigurationManager.ActiveConfiguration.Name;
                var getCustomProp = SwModel.Extension.CustomPropertyManager[activConfiguration];

                var nameProp = getCustomProp.Get("Раздел");

                var activPartition = itemsArray.Select(s => nameProp);

                foreach (var x in from x in itemsArray
                                  let enumerable = (string[])(activPartition as string[] ?? activPartition.ToArray())
                                  where x == enumerable.LastOrDefault()
                                  select x)
                {
                    CboChangePartition.Text = x;
                }

                if (CboChangePartition.Text == "")
                {
                    DgMatProp.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\r\nTargetSite: {ex.TargetSite}\r\nStackTrace: {ex.StackTrace}");
            }
        }

        private void CboChangePartition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = CboChangePartition.SelectedItem as string;

            if (item != "")
            {
                DgMatProp.Visibility = Visibility.Visible;
            }

            //Стандартные изделия IsFastener - IsFastener = 0
            //Прочие изделия IsFastener - IsFastener = 0
            //Материалы
            //Комплекты - IsFastener = 0
            //ЭМ-Стандартные изделия IsFastener - IsFastener = 0
            //ЭМ-Прочие изделия IsFastener - IsFastener = 0
            //ЭМ-Материалы
            if (item != "Материалы" & item != "ЭМ-Материалы")
            {
                DgMatProp.Columns[3].Visibility = Visibility.Hidden;
                DgMatProp.Columns[4].Visibility = Visibility.Hidden;
                DgMatProp.Columns[5].Visibility = Visibility.Hidden;

                DgMatProp.Columns[2].Visibility = Visibility.Visible;

                _sM.CheckType = 0;
            }
            else
            {
                DgMatProp.Columns[3].Visibility = Visibility.Visible;
                DgMatProp.Columns[4].Visibility = Visibility.Visible;
                DgMatProp.Columns[5].Visibility = Visibility.Visible;

                DgMatProp.Columns[2].Visibility = Visibility.Hidden;
                _sM.CheckType = 1;
            }

            DgMatProp.ItemsSource = GetSldWorksListProp();
        }
        private void DgMatProp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //var regex = new Regex(@"[^0-9\.]+");
            //e.Handled = regex.IsMatch(e.Text);
        }
        #region " DATA "

        // Свойства из SQL
        //public DataTable MaterialsTable(string lvlId)
        public DataTable MaterialsTable()
        {
            var materialsTable = new DataTable();

            try
            {
                //var query = @"select MaterialsName from MaterialsProp where LevelID = '" + lvlId + @"'";
                const string query = @"select MaterialsName from MaterialsProp";

                var sqlConnection = new SqlConnection(con.Con);
                var sqlCommand = new SqlCommand(query, sqlConnection);

                sqlConnection.Open();

                var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(materialsTable);

                sqlConnection.Close();

                sqlDataAdapter.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

            return materialsTable;
        }

        // Свойства из SolidWorks
        public class SldWorksPropClass
        {
            public string Config { get; set; }
            public string MatId { get; set; }
            public string Description { get; set; }
            public string Code1C { get; set; }
            public string Length { get; set; }
            public string Width { get; set; }
            public string Summ { get; set; }
            public string Note { get; set; }
        }

        readonly SetMaterials _sM = new SetMaterials();
        public List<SldWorksPropClass> GetSldWorksListProp()
        {
            // MessageBox.Show(SwModel.GetPathName());
            var columnRow = new List<SldWorksPropClass>();
            try
            {
                string[] ConfigArray = SwModel.GetConfigurationNames();

                for (var i = 0; i <= ConfigArray.GetUpperBound(0); i++)
                {
                    Configuration swConf = SwModel.GetConfigurationByName(ConfigArray[i]);

                    if (swConf.IsDerived()) continue;
                    var customProp = SwModel.Extension.CustomPropertyManager[ConfigArray[i]];

                    string description;
                    string valOut;
                    string note;
                    //string matId;
                    string code1c;
                    string lenght;
                    string width;
                    string summ;

                    //const string propMatId = "MaterialID";
                    const string propDescription = "Наименование";
                    const string propLinght = "Длина";
                    const string propWight = "Ширина";
                    const string propSumm = "Количество";
                    const string propNote = "Примечание";
                    const string propCode1C = "Код материала";

                    //customProp.Get4(propMatId, true, out valOut, out matId);
                    customProp.Get4(propDescription, true, out valOut, out description);
                    customProp.Get4(propLinght, true, out valOut, out lenght);
                    customProp.Get4(propWight, true, out valOut, out width);
                    customProp.Get4(propSumm, true, out valOut, out summ);
                    customProp.Get4(propNote, true, out valOut, out note);
                    customProp.Get4(propCode1C, true, out valOut, out code1c);

                    //if (matId == "")
                    //{
                    //    MessageBox.Show("Примените материал на модель!");
                    //    Close();
                    //    break;
                    //}

                    if (SwModel.GetType() == (int)swDocumentTypes_e.swDocPART)
                    {

                        foreach (var matName in _sM.GetCustomProperty(ConfigArray[i], SwApp))
                        {

                            var sldWorksPropArray = new SldWorksPropClass
                            {
                                Config = ConfigArray[i],
                                Description = _sM.CheckType == 0 ? description : matName.Name,
                                //Description = matName.Name == "" ? "" : matName.Name,
                                //MatId = matId == "" ? "" : matId,
                                Code1C = code1c == "" ? "" : code1c,
                                Length = lenght == "" ? "" : lenght,
                                Width = width == "" ? "" : width,
                                Summ = summ == "" ? "" : summ,
                                Note = note == "" ? "" : note
                            };

                            columnRow.Add(sldWorksPropArray);
                        }
                    }

                    if (SwModel.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
                    {
                        var sldWorksPropArray = new SldWorksPropClass
                        {
                            Config = ConfigArray[i],
                            Description = description,
                            Code1C = code1c == "" ? "" : code1c,
                            Length = lenght == "" ? "" : lenght,
                            Width = width == "" ? "" : width,
                            Summ = summ == "" ? "" : summ,
                            Note = note == "" ? "" : note
                        };

                        columnRow.Add(sldWorksPropArray);
                    }
                    


                

                    Debug.Print("Примечание - {0}, Материал ID - {1}, Длина - {2}, Ширина - {3}, Количество - {4}", note, "matId", lenght, width, summ);
                }
                return columnRow;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\r\nTargetSite: {ex.TargetSite}\r\nStackTrace: {ex.StackTrace}");
            }

            return columnRow;
        }

        #endregion
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CboChangePartition.Text == "")
                {
                    MessageBox.Show("Не выбран раздел!");
                    return;
                }

                SaveCustomProperty();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\r\nTargetSite: {ex.TargetSite}\r\nStackTrace: {ex.StackTrace}");
            }
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SaveCustomProperty()
        {
            var items = DgMatProp.ItemsSource as List<SldWorksPropClass>;

            if (items == null) return;

            DeleteAllProperties();

            foreach (var i in items)
            {
                var config = i.Config;
                var length = i.Length == "" ? null : i.Length;
                var width = i.Width == "" ? null : i.Width;
                var code = i.Code1C == "" ? null : i.Code1C;

                int n;
                if (int.TryParse(config, out n))
                {
                    var customProp = SwModel.Extension.CustomPropertyManager[config];

                    //var propName = new[] { "Обозначение", "Длина", "Ширина", "Количество", "Примечание", "Наименование", "Раздел", "Расчетное количество", "Код материала" };
                    //foreach (var delProp in propName)
                    //{
                    //    customProp.Delete(delProp);
                    //}

                    var itemPartition = CboChangePartition.SelectedItem as string;

                    customProp.Add2("Длина", 3, length);
                    customProp.Add2("Ширина", 3, width);
                    customProp.Add2("Раздел", 30, itemPartition);
                    customProp.Add2("Код материала", 3, code);

                    if (m == 0)
                    {
                        if (width == null & length == null)
                        {
                            customProp.Add2("Наименование", 30, i.Description == "" ? "" : Convert.ToString(i.Description));
                        }
                        else
                        {
                            customProp.Add2("Наименование", 30, i.Description == "" ? "" : Convert.ToString(i.Description + " Заготовка: " + length + "x" + width));
                        }
                    }

                    if (m == 1)
                    {
                        customProp.Add2("Наименование", 30, i.Description == "" ? "" : Convert.ToString(i.Description));
                    }

                    //Стандартные изделия IsFastener - IsFastener = 0
                    //Прочие изделия IsFastener - IsFastener = 0
                    //Материалы
                    //Комплекты - IsFastener = 0
                    //ЭМ-Стандартные изделия IsFastener - IsFastener = 0
                    //ЭМ-Прочие изделия IsFastener - IsFastener = 0
                    //ЭМ-Материалы
                    var customPropIsFastener = SwModel.Extension.CustomPropertyManager[""];
                    var selectedItem = CboChangePartition.Text;

                    if (selectedItem == "Стандартные изделия" | selectedItem == "Прочие изделия" |
                        selectedItem == "Комплекты" | selectedItem == "ЭМ-Стандартные изделия" |
                        selectedItem == "ЭМ-Прочие изделия")
                    {
                        customPropIsFastener.Add2("IsFastener", 30, "0");
                    }
                    else
                    {
                        customPropIsFastener.Delete("IsFastener");
                    }

                    if (i.Summ != "")
                    {
                        // Count_SP
                        SwModel.AddCustomInfo3(config, "UNIT_OF_MEASURE", 30, "");
                        customProp.Add2("Количество", 30, i.Summ == "" ? "" : i.Summ);
                        SwModel.CustomInfo2[config, "UNIT_OF_MEASURE"] = "Количество";
                    }

                    customProp.Add2("Расчетное количество", 3, "");
                    customProp.Add2("Примечание", 30, i.Note == "" ? "" : i.Note);
                }
                else
                {
                    MessageBox.Show(@"Конфигурацию """ + config + @" ""необходимо переименовать в числовое значение!");
                }
            }

            DgMatProp.ItemsSource = GetSldWorksListProp();
        }
        private string[] vConfNameArr;
        private string[] vCustInfoNameArr; // Массив свойств файла
        public void DeleteAllProperties()
        {
            try
            {
                // Удаление свойств из ConfigCpec
                vConfNameArr = SwModel.GetConfigurationNames();

                for (var i = 0; i <= vConfNameArr.GetUpperBound(0); i++)
                {
                    vCustInfoNameArr = SwModel.GetCustomInfoNames2(vConfNameArr[i]);

                    for (var j = 0; j <= vCustInfoNameArr.GetUpperBound(0); j++)
                    {
                        SwModel.DeleteCustomInfo2(vConfNameArr[i], vCustInfoNameArr[j]);
                    }
                }

                // Удаление свойств из CustomProperty
                vCustInfoNameArr = SwModel.GetCustomInfoNames2("");

                for (var j = 0; j <= vCustInfoNameArr.GetUpperBound(0); j++)
                {
                    SwModel.DeleteCustomInfo2("", vCustInfoNameArr[j]);
                }

                SwModel.SummaryInfo[0] = ""; //swSumInfoTitle
                SwModel.SummaryInfo[1] = ""; //swSumInfoSubject
                SwModel.SummaryInfo[2] = ""; //swSumInfoAuthor
                SwModel.SummaryInfo[3] = ""; //swSumInfoKeywords
                SwModel.SummaryInfo[4] = ""; //swSumInfoComment

                //if (m == 0)
                //{
                //    var swPartDoc = (PartDoc)SwModel;
                //    swPartDoc.SetMaterialPropertyName("", "");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\r\nTargetSite: {ex.TargetSite}\r\nStackTrace: {ex.StackTrace}");
            }
        }
    }
}