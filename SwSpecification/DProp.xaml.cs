
using System;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.VisualBasic;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SWPlus
{
    /// <summary>
    /// Interaction logic for DProp.xaml
    /// </summary>
    public partial class DProp : Window
    {
        public DProp()
        {
            InitializeComponent();
        }
        #region Variables
            private SldWorks swApp;
            private ModelDoc2 swModel;
            private Note swNote;
            private SelectionMgr swSelMgr;
            private ModelDocExtension swModExt;
            private DrawingDoc swDraw;
            private Sheet swSheet;
            private View swView;
            private Annotation swAnn;
            private TextFormat swTextFormat;
            private int MChkScale; // Метка флажка Без масштаба: 0 - пользователь изменил флажок; 1 - флажок изменен из программы
            private string strActiveSheetName;
            private string[] vSheetNames;
            private string[] sFormatArray; // Массив значений форматов из чертежа
            private string[,] FormatArr = new string[28, 3];
            private string sSheetsNames1; // Имена листов со старыми версиями
            private string sSheetsNames2; // Имена листов с нечитаемыми форматами
            private string sSheetsNames3; // Имена листов с проблемами форматами
            private string sSheetsNames4; // Имена листов с проблемами форматами
            private string stdDay;
            private string stdMonth;
            private string stdYear;
            private string stdHour;
            private string stdMinute;
            private string stdSecond;
            private string MIniMaster;
            private string Source8;      
            private string[] s = new string[35];    
            private string[] vModelViewNames;
            private bool ok;
            private int MIni;
            private int MView;
            int lRetval;
            private long DeleteOption;
            private string strSheetFormatName;
            private bool vRetval;
            private double Numerator;
            private double Denominator;
            private double DTemp;
            private string stdFontName;
            private double stdFontSize;
            private int stdFontItalic;
            private int stdFontBold;
            private int prpFontSize;
            private int prpFileName;
            private int MIniLRI;
            private string strTemp;
            private string strTemp1;
            private int m1;
            private string sConfigName;
            private int MAdd;
            private string TemplateName;
            private int prpTestVersion;
            private int prpTestFormat;
            private int prpTestName;
            private int prpTestStandard;
            private int k;
            private int k1;
            private int k2;
            private int k3;
            private int m;
            private int j;
            private int i;
            int mEx1;
            int mEx2;
            private int MImpr; // Метка процедуры Исправления
            private int varTemp;
            private int varTemp1;
            private int varTemp2;
            private double[] vSheetProps;
            private int MIniRoug;
            private string strMsg;
        #endregion
        private void DPropForm_Loaded(object sender, RoutedEventArgs e)
        {
            CheckStart();
        }
        public void CheckStart() // Проверка открытого чертежа
        {
            try
            {
                swApp = (SldWorks) Marshal.GetActiveObject("SldWorks.Application");
                swModel = (ModelDoc2) swApp.ActiveDoc;
                if (swModel == null | swModel.GetType() != (int)swDocumentTypes_e.swDocDRAWING)
                {
                    MessageBox.Show("Откройте чертеж");
                    Close();
                    return;
                }
                swDraw = (DrawingDoc)swModel;
                Start();
            }
            catch (COMException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Start()
        {
            try
            {

            //Source = swApp.GetCurrentMacroPathName(); // Get macro path+filename
            //Source8 = Strings.Left(Source, Strings.Len(Source) - 15) + @"SpecEditor\MyStandard.sldstd"; // Путь к файлу стандарт

            var userAppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            Source8 = userAppData + "\\SW-Complex\\MyStandard.sldstd";

            strTemp =  "0";
            MIniRoug = Convert.ToInt32(Strings.Left(strTemp, 1));
            strTemp = "0";
            MIni = Convert.ToInt32(Strings.Left(strTemp, 1));
            strTemp = "1";
            MIniLRI = Convert.ToInt32(Strings.Left(strTemp, 1));

            // Чтение ini файла макроса Мастер
            strTemp = "Arial Narrow";
            strTemp = "1";
            strTemp = "0";
            //MIniMaster = @"\\srvkb\SolidWorks Admin\Templates\Основные надписи\";
            MIniMaster = swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swFileLocationsSheetFormat) + @"\";

            MyProperties();

            if (prpTestStandard == 1)
            {
                MyStandard();
            }

            // Узнаем имя активного листа
            swSheet = swDraw.GetCurrentSheet();
            strActiveSheetName = swSheet.GetName();

            // Активируем первый лист
            vSheetNames = swDraw.GetSheetNames();
            ok = swDraw.ActivateSheet(vSheetNames[0]);
            swSheet = swDraw.GetCurrentSheet();

            #region Получаем параметры модели
            // Получаем параметры модели
            MView = 1;
            swView = swDraw.GetFirstView();
            m = 0;

            if (swSheet.CustomPropertyView == "По умолчанию" | swSheet.CustomPropertyView == "Default")
            {
                swView = swView.GetNextView(); // Получаем первый вид
            }
            else
            {
                while (swView == null)
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
                    MessageBox.Show("Не удалось определить вид из свойств листа. Используется первый вид.");
                }
            }
            if (swView == null)
            {
                MView = 0;
            }
            else
            {
                if (swView.ReferencedDocument == null)
                {
                    MView = 0;
                }
                else
                {
                    sConfigName = swView.ReferencedConfiguration; // Имя конфигурации вида
                    swModel = swView.ReferencedDocument;
                    vModelViewNames = swModel.GetModelViewNames();
                }
            }
            #endregion
            // Считываем масштаб
            vSheetProps = swSheet.GetProperties();
            Numerator = vSheetProps[2];
            Denominator = vSheetProps[3];

            if (Numerator < 1)
            {
                DTemp = 1 / Numerator;
                Numerator = Numerator * DTemp;
                Denominator = Denominator * DTemp;
            }

            if (Denominator < 1)
            {
                DTemp = 1 / Denominator;
                Numerator = Numerator * DTemp;
                Denominator = Denominator * DTemp;
            }
            #region Определяем системный разделитель
                // Определяем системный разделитель
                if (swApp.GetUserPreferenceToggle((int)swUserPreferenceToggle_e.swUseSystemSeparatorForDims))
                {
                    strTemp = swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swSeparatorCharacterForDims);
                }
                else
                {
                    strTemp1 = swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swSeparatorCharacterForDims);
                    swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swUseSystemSeparatorForDims, true);
                    strTemp = swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swSeparatorCharacterForDims);
                    swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swUseSystemSeparatorForDims, false);
                    ok = swApp.SetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swSeparatorCharacterForDims, strTemp1);
                }
                if (strTemp == ".")
                {
                    m = 0;
                }
                else
                {
                    if (strTemp == ",")
                    {
                        m = 1;
                    }
                    else
                    {
                        MessageBox.Show("Устанавите системным разделителем точку или запятую");
                        CmdCancel_Click(true, null);
                        return;
                    }
                }
                #endregion
            #region Заполнение списка "Масштаб"
                // Заполнение списка "Масштаб"
                CboScale.Items.Clear();
                CboScale.Items.Add("1:10");
                CboScale.Items.Add("1:5");
                CboScale.Items.Add("1:4");
                if (m == 0)
                {
                    CboScale.Items.Add("1:2.5");
                }
                else
                {
                    CboScale.Items.Add("1:2,5");
                }

                CboScale.Items.Add("1:2");
                CboScale.Items.Add("1:1");
                CboScale.Items.Add("2:1");

                if (m == 0)
                {
                    CboScale.Items.Add("2.5:1");
                }
                else
                {
                    CboScale.Items.Add("2,5:1");
                }
                CboScale.Items.Add("4:1");
                CboScale.Items.Add("5:1");
                CboScale.Items.Add("10:1");

                m1 = 0;

                for (i = 0; i <= CboScale.Items.Count - 1; i++)
                {
                    if (CboScale.Items[i].ToString() == Convert.ToString(Numerator) + ":" + Convert.ToString(Denominator))
                    {
                        m1 = 1;
                        break;
                    }
                }

                if (m1 == 0)
                {
                    CmdAllScale_Click(true, null);
                }
                else
                {
                    CboScale.SelectedValue = Convert.ToString(Numerator) + ":" + Convert.ToString(Denominator);
                }
                #endregion
            #region Проверяем флажок ChkScale
                // Проверяем флажок ChkScale
                swModel = (ModelDoc2)swDraw;
                swSelMgr = swModel.SelectionManager;
                MChkScale = 1;
                ChkScale.IsChecked = false;
                strSheetFormatName = swSheet.GetSheetFormatName();
                strTemp = "Scale@" + strSheetFormatName;

                ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);

                if (ok)
                {
                    swNote = swSelMgr.GetSelectedObject2(1);

                    if (swNote.GetText() == "-")
                    {
                        ChkScale.IsChecked = true;
                    }
                }
                MChkScale = 0;
                Tests();
                SheetsControl();
            #endregion
            // Возвращение активного листа
            ok = swDraw.ActivateSheet(strActiveSheetName);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CmdCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //swModel.ClearSelection2(true);
                //Hide
                //Unload Me
                //End
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CmdAddSheet_Click(object sender, RoutedEventArgs e) // Добавление нового листа
        {
        //    MAdd = 1;
        //    _swApp.RunMacro("", "Master_run", "main_add");

        //    // Узнаем имя активного листа
        //    swSheet = swDraw.GetCurrentSheet();
        //    strActiveSheetName = swSheet.GetName();

        //    strSheetFormatName = swSheet.GetSheetFormatName();
        //    // Лист
        //    strTemp = "Revision2@" + strSheetFormatName;
        //    _swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
        //    if (Ok)
        //    {
        //        _swNote = swSelMgr.GetSelectedObject2(1);
        //        if (_swNote.GetText() != " ")
        //        {
        //            _swNote.SetText("Нов.");
        //        }
        //    }
        //    Tests();
        //    SheetsControl();

        //    // Возвращение активного листа
        //    swDraw.ActivateSheet(strActiveSheetName);
        //    MAdd = 0;
        }
        private void CmdDelSheet_Click(object sender, RoutedEventArgs e) // Удаление листа
        {
        //    // Узнаем имя активного листа
        //    swSheet = swDraw.GetCurrentSheet();
        //    strActiveSheetName = swSheet.GetName();
        //    DeleteOption = (int) swDeleteSelectionOptions_e.swDelete_Absorbed + (int) swDeleteSelectionOptions_e.swDelete_Children;
        //    _swModel.Extension.SelectByID2(strActiveSheetName, "SHEET", 0, 0, 0, false, 0, null, 0);
        //    _swModel.Extension.DeleteSelection2((int) DeleteOption);

        //    SheetsControl();
        }
        private void CmdChange_Click(object sender, RoutedEventArgs e) // Замена форматки
       {
           SaveData();
           //_swApp.RunMacro("", "Master_run", "main_change");
           RestoreData();

           // Узнаем имя активного листа
           swSheet = swDraw.GetCurrentSheet();
           strActiveSheetName = swSheet.GetName();

           Tests();

           // Возвращение активного листа
           swDraw.ActivateSheet(strActiveSheetName);

        }
        public void CmdRelod()
        {
            try
            {
                //SaveData();

                swSheet = swDraw.GetCurrentSheet();
                vSheetProps = swSheet.GetProperties();
                TemplateName = swSheet.GetTemplateName(); // Запоминаем название шаблона

                varTemp = Strings.InStrRev(TemplateName, @"\");

                if (Strings.Left(swSheet.GetName(), 3) == "DRW" | Strings.Left(swSheet.GetName(), 4) == "Лист" | Strings.Left(swSheet.GetName(), 5) == "Sheet")
                {
                    if (Strings.Left(TemplateName, varTemp) != MIniMaster)
                    {
                        FormatSize();
                    }
                    else if (Strings.Left(swSheet.GetName(), 2) == "SP" | Strings.Left(swSheet.GetName(), 2) == "VP" | Strings.Left(swSheet.GetName(), 3) == "LRI")
                    {
                        //if (Strings.Left(TemplateName, varTemp) != Strings.Left(Source6, Strings.Len(Source6) - 16))
                        //{
                        //    TemplateName = Strings.Left(Source6, Strings.Len(Source6) - 16) + Strings.Right(TemplateName, Strings.Len(TemplateName) - varTemp);
                        //}
                    }
                    else
                    {
                        FormatSize();
                    }
                }
                // Проверяем существование такой форматки
                if (File.Exists(TemplateName) == false)
                {
                    strTemp = "Не удалось перезагрузить основную надпись для листа " + swSheet.GetName() + " !" + Strings.Chr(10) + "Выберите команду Заменить основную надпись.";
                    return;
                }

                ok = swDraw.SetupSheet4(swSheet.GetName(),
                                        (int)vSheetProps[0],
                                        (int)swDwgTemplates_e.swDwgTemplateA4sizeVertical,
                                        vSheetProps[2],
                                        vSheetProps[3],
                                        Convert.ToBoolean(vSheetProps[4]),
                                        null,
                                        Convert.ToDouble(vSheetProps[5]),
                                        Convert.ToDouble(vSheetProps[6]),
                                        swSheet.CustomPropertyView);

                ok = swDraw.SetupSheet4(swSheet.GetName(),
                                        (int)vSheetProps[0],
                                        (int)vSheetProps[1],
                                        vSheetProps[2],
                                        vSheetProps[3],
                                        Convert.ToBoolean(vSheetProps[4]),
                                        TemplateName,
                                        Convert.ToDouble(vSheetProps[5]),
                                        Convert.ToDouble(vSheetProps[6]),
                                        swSheet.CustomPropertyView);

                if (ok == false)
                {
                    strTemp = "Не удалось перезагрузить основную надпись для листа " + swSheet.GetName() + " !" + Strings.Chr(10) + "Выберите команду Заменить основную надпись.";
                    MessageBox.Show(strTemp);

                    return;
                }

                swModel.ForceRebuild3(true);

                //RestoreData();

                // Проверка первого листа и вызов макросов MProp и Roughness, чтобы исправить форматирование
                swSheet = swDraw.GetCurrentSheet();
                strActiveSheetName = swSheet.GetName();
                vSheetNames = swDraw.GetSheetNames();


                if (MImpr != 1) //Нет вызова из Impr
                {
                    // Узнаем имя активного листа
                    swSheet = swDraw.GetCurrentSheet();
                    strActiveSheetName = swSheet.GetName();

                    //Tests();
                    // Возвращение активного листа
                    swDraw.ActivateSheet(strActiveSheetName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CmdReload_Click(object sender, RoutedEventArgs e) // Перезагрузка форматки
        {
            CmdRelod();
        }
        private void CmdRev_Click(object sender, RoutedEventArgs e)
        {
        //    // Узнаем имя активного листа
        //    swSheet = swDraw.GetCurrentSheet();
        //    strActiveSheetName = swSheet.GetName();

        //    // Изм.
        //    if (LblSetRev == "нет")
        //    {
        //        swDraw.AddCustomInfo3("", "Revision", 30, "");
        //        swDraw.CustomInfo2("", "Revision") = "";
        //    }
        //    else
        //    {
        //        swDraw.AddCustomInfo3("", "Revision", 30, LblSetRev.Caption);
        //        swDraw.CustomInfo2("", "Revision") = LblSetRev.Caption;
        //    }

        //    // Определяем дату
        //    var MyDate = new DateTime();
        //    var MyDay = MyDate.Day;
        //    var MyMonth = MyDate.Month;
        //    var MyYear = MyDate.Year;
        //    string strData = MyDay + "." + MyMonth + "." + MyYear;

        //    // Устанавливаем Лист и Дату
        //    vSheetNames = swDraw.GetSheetNames();

        //    For i = 0 To UBound (vSheetNames) swDraw.ActivateSheet(vSheetNames[i]);
        //    swSheet = swDraw.GetCurrentSheet();
        //    strSheetFormatName = swSheet.GetSheetFormatName();
        //    // Лист
        //    strTemp = "Revision2@" + strSheetFormatName;
        //    _swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
        //    if (Ok)
        //    {
        //        //Определяем и устанавливаем Лист
        //        if (LblSetRev.Caption = "нет")
        //        {
        //            strTemp = " ";
        //        }
        //        else
        //        {
        //            strTemp = "Зам."
        //        }
        //        _swNote = swSelMgr.GetSelectedObject2(1);
        //        if (_swNote.GetText() == "Нов." || MAdd == 1)
        //        {

        //        }
        //        else
        //        {
        //            _swNote.SetText(strTemp);
        //        }
        //    }
        //    // Дата
        //    strTemp = "Revision4@" + strSheetFormatName;
        //    _swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
        //    if (Ok)
        //    {
        //        // Определяем и устанавливаем дату}
        //        if (LblSetRev.Caption == "нет")
        //        {
        //            strTemp = " ";
        //        }
        //        else
        //        {
        //            strTemp = strData;
        //        }
        //        _swNote = swSelMgr.GetSelectedObject2(1);

        //        _swNote.SetText(strTemp);
        //    }
        //    // Возвращение активного листа
        //    swDraw.ActivateSheet(strActiveSheetName);

        //    // Перестраиваем чертеж
        //    _swModel.Rebuild((int) swRebuildOptions_e.swRebuildAll);
        //    _swModel.GraphicsRedraw2();
        //    _swModel.ClearSelection2(true);

        //    // Изменение отображения редакции чертежа
        //    strTemp = swDraw.CustomInfo2("", "Revision");
        //    if (strTemp == "")
        //    {
        //        LblGetRev.Caption = "нет";
        //        SpinButton1.Value = 1;
        //    }
        //    else
        //    {
        //        LblGetRev.Caption = strTemp;
        //        SpinButton1.Value = CInt(strTemp) + 1;
        //    }
        }
        private void CmdScale_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Узнаем имя активного листа
                swSheet = swDraw.GetCurrentSheet();
                strActiveSheetName = swSheet.GetName();

                strTemp = CboScale.Text;

                varTemp = Strings.InStr(strTemp, ":"); // Проверяем разделитель
                Numerator = Convert.ToDouble(Strings.Left(strTemp, varTemp - 1));

                Denominator = Convert.ToDouble(Strings.Right(strTemp, Strings.Len(strTemp) - varTemp));
     
                SheetsControl();

                // Возвращение активного листа
                ok = swDraw.ActivateSheet(strActiveSheetName);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CmdAllScale_Click(object sender, RoutedEventArgs e)
        {
            // Заполнение списка "Масштаб"
            CboScale.Items.Clear();
            CboScale.Items.Add("1:1000");
            CboScale.Items.Add("1:800");
            CboScale.Items.Add("1:500");
            CboScale.Items.Add("1:400");
            CboScale.Items.Add("1:200");
            CboScale.Items.Add("1:100");
            CboScale.Items.Add("1:75");
            CboScale.Items.Add("1:50");
            CboScale.Items.Add("1:40");
            CboScale.Items.Add("1:25");
            CboScale.Items.Add("1:20");
            CboScale.Items.Add("1:15");
            CboScale.Items.Add("1:10");
            CboScale.Items.Add("1:5");
            CboScale.Items.Add("1:4");

            if (m == 0)
            {
                CboScale.Items.Add("1:2.5");
            }
            else
            {
                CboScale.Items.Add("1:2,5");
            }

            CboScale.Items.Add("1:2");
            CboScale.Items.Add("1:1");
            CboScale.Items.Add("2:1");

            if (m == 0)
            {
                CboScale.Items.Add("2.5:1");
            }
            else
            {
                CboScale.Items.Add("2,5:1");
            }

            CboScale.Items.Add("4:1");
            CboScale.Items.Add("5:1");
            CboScale.Items.Add("10:1");
            CboScale.Items.Add("20:1");
            CboScale.Items.Add("40:1");
            CboScale.Items.Add("50:1");
            CboScale.Items.Add("100:1");

            m1 = 0;
       
            for (i = 0; i <= CboScale.Items.Count - 1; i++)
            {
                if (CboScale.Items[i].ToString() == Convert.ToString(Numerator) + ":" + Convert.ToString(Denominator))
                {
                    m1 = 1;
                    break;
                }
            }

            if (m1 == 0)
            {
                //swApp.SendMsgToUser2("Установлен нестандартный масштаб!", (int) swMessageBoxIcon_e.swMbWarning, (int) swMessageBoxBtn_e.swMbOk);
                //MessageBox.Show("Установлен нестандартный масштаб!");

                CboScale.Items.Add(Convert.ToString(Numerator) + ":" + Convert.ToString(Denominator));
            }

            CboScale.Text = Convert.ToString(Numerator) + ":" + Convert.ToString(Denominator);
        }
        private void ChkScale_Checked(object sender, RoutedEventArgs e)
        {
            if (MChkScale == 0)
            {
                //Узнаем имя активного листа
                swDraw = (DrawingDoc)swModel;
                swSheet = swDraw.GetCurrentSheet();
                strActiveSheetName = swSheet.GetName();

                //Активируем первый лист

                swDraw.ActivateSheet(vSheetNames[0]);
                swSheet = swDraw.GetCurrentSheet();
                strSheetFormatName = swSheet.GetSheetFormatName();
                strTemp = "Scale@" + strSheetFormatName;
                swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
                if (ok)
                {
                    swNote = swSelMgr.GetSelectedObject2(1);
                    if (ChkScale.IsChecked == true)
                    {
                        swNote.SetText("-");
                    }
                    else
                    {
                        swNote.SetText("$PRP:" + '\u0022' + "SW-Sheet Scale" + '\u0022');
                    }
                    //Возвращение активного листа
                    swDraw.ActivateSheet(strActiveSheetName);
                }
            }
        }
        private void SheetsControl()
        {

            try
            {
                // Переименовываем листы и выставляем масштаб
                vSheetNames = swDraw.GetSheetNames();
                j = 0;
                m = 0;
                k = 0;
                k1 = 0;

                //Debug.Print "UBound(vSheetNames)", UBound(vSheetNames)
        
                for (i = 0; i <= vSheetNames.GetUpperBound(0); i++)
                {
                    //Проверка имени листа
                    if (Strings.Left(vSheetNames[i], 3) == "LRI")
                    {
                        m = 1;
                        k = i;
                        j = j + 1;
                    }

                    if (Strings.Left(vSheetNames[i], 3) == "DRW" | Strings.Left(vSheetNames[i], 4) == "Лист" | Strings.Left(vSheetNames[i], 5) == "Sheet")
                    {
                        j = j + 1;
                        k1 = i;

                        ok = swDraw.ActivateSheet(vSheetNames[i]);
                        swSheet = swDraw.GetCurrentSheet();
                        strTemp = "DRW" + Convert.ToString(j);
                        vSheetProps = swSheet.GetProperties();
                        ok = swDraw.SetupSheet4(strTemp, (int)vSheetProps[0], (int)vSheetProps[1], Numerator, Denominator, 
                            Convert.ToBoolean(vSheetProps[4]), swSheet.GetTemplateName(), vSheetProps[5], vSheetProps[6], swSheet.CustomPropertyView);
                    }

                }

                //Вставляем ЛРИ
                if (MIniLRI == 1 & j > 2 & m == 0)
                {
                    strTemp = "LRI";
                    //vRetval = swDraw.NewSheet3(strTemp, swDwgPapersUserDefined, swDwgTemplateCustom, 1, 1, true, Source10, 0.21, 0.297, "");
                    swSheet = swDraw.GetCurrentSheet();
                    swSheet.SheetFormatVisible = true;

                    if (MView == 1) // Вставляем вид
                    {
                        swView = swDraw.CreateDrawViewFromModelView3(swModel.GetPathName(), vModelViewNames[0], -0.1, 0, 0);
                        swView.ReferencedConfiguration = sConfigName;
                        swDraw.SuppressView();
                        swModel.ForceRebuild3(true);
                    }

                        j = j + 1;

                    // Тасуем листы
                    vSheetNames = swDraw.GetSheetNames();

                    for (i = vSheetNames.GetUpperBound(0); i >= k1 + 2; i += -1)
                    {
                        vSheetNames[i] = vSheetNames[i - 1];
                    }

                    vSheetNames[k1 + 1] = strTemp;
                    ok = swDraw.ReorderSheets(vSheetNames);
                    }
                    else if (MIniLRI == 1 & j < 4 & m == 1)
                    {
                    strMsg = "Удалить ЛРИ?";
                    lRetval = swApp.SendMsgToUser2(strMsg, 0, 0);

                    //swMbHitYes = 6;

                    if (lRetval == 6)
                    {
                        ok = swDraw.ActivateSheet(vSheetNames[k]);
                        swSheet = swDraw.GetCurrentSheet();
                        //DeleteOption = SwConst.swDelete_Absorbed + SwConst.swDelete_Children;
                        ok = swModel.Extension.SelectByID2(swSheet.GetName(), "SHEET", 0, 0, 0, false, 0, null, 0);
                        ok = swModel.Extension.DeleteSelection2(Convert.ToInt32(DeleteOption));
                        j = j - 1;
                    }
                }

                // Заносим количество листов
                vSheetNames = swDraw.GetSheetNames();
                ok = swDraw.ActivateSheet(vSheetNames[0]);
                swSheet = swDraw.GetCurrentSheet();
                strSheetFormatName = swSheet.GetSheetFormatName();
                strTemp = "Sheet1@" + strSheetFormatName;
                ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);

                if (ok)
                {
                    if (j > 1) // Листов больше одного
                    {
                        strTemp = "Аркуш 1";
                    }
                    else
                    {
                        strTemp = "Аркуш";
                    }
                    swNote = swSelMgr.GetSelectedObject2(1);
                    swNote.SetText(strTemp);
                }

                strTemp = "Sheet2@" + strSheetFormatName;
                ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
                if (ok)
                {
                    strTemp = "Аркушів " + j;
                    swNote = swSelMgr.GetSelectedObject2(1);
                    swNote.SetText(strTemp);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Tests()
        {
            try
            {

            prpTestVersion = 1;

            if (prpTestVersion == 1 | prpTestFormat == 1 | prpTestStandard == 1)
            {

  
                //vSheetNames = swDraw.GetSheetNames();
                // ReDim sFormatArray(UBound(vSheetNames))

                sFormatArray = vSheetNames;

                //Считываем и проверяем версии форматов всех листов
                k = 0;
                k1 = 0;
                k2 = 0;
                k3 = 0;

                sSheetsNames1 = "";
                sSheetsNames2 = "";
                sSheetsNames3 = "";
                sSheetsNames4 = "";
                //strMsg = "";

                for (i = 0; i <= vSheetNames.GetUpperBound(0); i++)
                {
                    ok = swDraw.ActivateSheet(vSheetNames[i]);
                    swSheet = swDraw.GetCurrentSheet();
                    strSheetFormatName = swSheet.GetSheetFormatName();

                    // Проверка имени листа
                    #region Проверка имени листа
                    if (Strings.Left(vSheetNames[i], 3) == "DRW" | Strings.Left(vSheetNames[i], 4) == "Лист" |
                    Strings.Left(vSheetNames[i], 5) == "Sheet" | Strings.Left(vSheetNames[i], 2) == "SP")
                    {
                        if (prpTestVersion == 1)
                        {
                            MessageBox.Show("Проверка версии форматкиVersion@" + strSheetFormatName);
                            //Проверка версии форматки
                            strTemp = "Version@" + strSheetFormatName;
                        
                            ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
                            MessageBox.Show("Проверка версии ok " + ok);
                            if (ok)
                            {
                                swNote = swSelMgr.GetSelectedObject2(1);
                                strTemp = swNote.GetText();

                                if (Strings.Right(strTemp, 1) != "4")
                                {
                                    k1 = k1 + 1;
                                    sSheetsNames1 = sSheetsNames1 + vSheetNames[i] + ", ";
                                }
                            }
                        }

                        if (prpTestStandard == 1)
                        {
                            // Проверка оформления
                            mEx1 = 0;
                            mEx2 = 0;

                            swView = swDraw.GetFirstView(); // Проверяем шрифты форматки

                            if (swView != null) 
                            {
                                swNote = swView.GetFirstNote();

                                j = 0;

                                while (j == 0)
                                {
                                    if (swNote != null)
                                    {
                                        swAnn = swNote.GetAnnotation();

                                        if (swAnn.OwnerType != (int)swAnnotationOwner_e.swAnnotationOwner_DrawingTemplate) // Исключаем объекты форматки
                                        {
                                            swNote = swNote.GetNext();
                                        }
                                        else
                                        {
                                            j = 1;
                                        }
                                    }
                                    else
                                    {
                                        j = 1;
                                    }
                                }

                                if (swNote != null) 
                                {
                                    swAnn = swNote.GetAnnotation();
                                    swTextFormat = swAnn.GetTextFormat(0);

                                    if (swTextFormat.TypeFaceName != stdFontName)
                                    {
                                        mEx1 = 1;
                                    }

                                    if (swTextFormat.Italic & stdFontItalic == 0) 
                                    {
                                        mEx1 = 1;
                                    } 
                                    else if (swTextFormat.Italic == false & stdFontItalic == 1) 
                                    {
                                        mEx1 = 1;
                                    }

                                    if (swTextFormat.Bold & stdFontBold == 0) 
                                    {
                                        mEx1 = 1;
                                    } 
                                    else if (swTextFormat.Bold == false & stdFontBold == 1) 
                                    {
                                        mEx1 = 1;
                                    }
                                }
                                else
                                {
                                    mEx1 = 1;
                                }
                            }
                            else
                            {
                                mEx1 = 1;
                            }

                            if (swView != null)
                            {
                                swView = swView.GetNextView(); // Проверяем шрифты чертежа

                                if (swView != null)
                                {
                                    swNote = swView.GetFirstNote();
                                    if (swNote != null)
                                    {
                                        swAnn = swNote.GetAnnotation();
                                        swTextFormat = swAnn.GetTextFormat(0);

                                        if (swTextFormat.TypeFaceName != stdFontName)
                                        {
                                            mEx2 = 1;
                                        }
                                        if (swTextFormat.Italic & stdFontItalic == 0)
                                        {
                                            mEx2 = 1;
                                        }
                                        if (swTextFormat.Italic == false & stdFontItalic == 1)
                                        {
                                            mEx2 = 1;
                                        }
                                        if (swTextFormat.Bold & stdFontBold == 0)
                                        {
                                            mEx2 = 1;
                                        }
                                        if (swTextFormat.Bold == false & stdFontBold == 1)
                                        {
                                            mEx2 = 1;
                                        }
                                    }
                                }
                            }

                            swModExt = swModel.Extension;
                            swTextFormat = swModExt.GetUserPreferenceTextFormat((int)swUserPreferenceTextFormat_e.swDetailingDimensionTextFormat, (int)swUserPreferenceOption_e.swDetailingDimension);

                            if (swTextFormat.TypeFaceName != stdFontName | swTextFormat.CharHeight != stdFontSize / 1000) 
                            {
                                mEx2 = 1;
                            }

                            if (swTextFormat.Italic & stdFontItalic == 0) 
                            {
                                mEx2 = 1;
                            } 
                            else if (swTextFormat.Italic == false & stdFontItalic == 1) 
                            {
                                mEx2 = 1;
                            }

                            if (swTextFormat.Bold & stdFontBold == 0) 
                            {
                                mEx2 = 1;
                            } 

                            else if (swTextFormat.Bold == false & stdFontBold == 1) 
                            {
                                mEx2 = 1;
                            }

                            if (mEx1 == 1) 
                            {
                                k3 = k3 + 1;
                                sSheetsNames4 = sSheetsNames4 + vSheetNames[i] + ", ";
                            }

                            if (mEx2 == 1) 
                            {
                                k2 = k2 + 1;
                                sSheetsNames2 = sSheetsNames2 + vSheetNames[i] + ", ";
                            }
                        }
                    }
                    #endregion
                    if (prpTestFormat == 1)
                    {
                        if (Strings.Left(vSheetNames[i], 3) == "DRW" | 
                            Strings.Left(vSheetNames[i], 4) == "Лист" |
                            Strings.Left(vSheetNames[i], 5) == "Sheet")
                        {
                            // Проверка имени формата
                            strTemp = "Format@" + strSheetFormatName;
                            ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);

                 
                            if (ok)
                            {
                                swNote = swSelMgr.GetSelectedObject2(1);
                                strTemp = swNote.GetText();

                                // Проверка корректности и длины записи
                                if (Strings.Len(Strings.Trim(strTemp)) > 8 & Strings.Left(strTemp, 6) == "Формат")
                                {


                                }
                                else
                                {
                                    k = k + 1;
                                    sSheetsNames3 = sSheetsNames3 + vSheetNames[i] + ", ";
                                }
                            }
                            else
                            {
                                k = k + 1;
                                sSheetsNames3 = sSheetsNames3 + vSheetNames[i] + ", ";
                            }
                        }
                    }
                }
                //MessageBox.Show(k.ToString());
                //MessageBox.Show(k1.ToString());
                //MessageBox.Show(k2.ToString());
                //MessageBox.Show(k3.ToString());
                if (k > 0 || k1 > 0 || k2 > 0 || k3 > 0) // Предупреждение об ошибках
                {
                    if (k1 > 0)
                    {
                        sSheetsNames1 = Strings.Left(sSheetsNames1, Strings.Len(sSheetsNames1) - 2);
                        strMsg = "Лист(ы) " + sSheetsNames1 + " имеют нестандартную или устаревшую основную надпись.";
                    }

                    if (k2 > 0)
                    {
                        sSheetsNames2 = Strings.Left(sSheetsNames2, Strings.Len(sSheetsNames2) - 2);
                        strMsg = strMsg + Strings.Chr(10) + "Лист(ы) " + sSheetsNames2 +
                                 " имеют ошибки оформления чертежа.";
                    }

                    if (k3 > 0)
                    {
                        sSheetsNames4 = Strings.Left(sSheetsNames4, Strings.Len(sSheetsNames4) - 2);
                        strMsg = strMsg + Strings.Chr(10) + "Лист(ы) " + sSheetsNames4 +
                                 " имеют ошибки оформления основной надписи.";
                    }

                    if (k > 0)
                    {
                        sSheetsNames3 = Strings.Left(sSheetsNames3, Strings.Len(sSheetsNames3) - 2);
                        strMsg = strMsg + Strings.Chr(10) + "Лист(ы) " + sSheetsNames3 +
                                 " не позволяют определить формат.";
                    }

                    strMsg = strMsg + Strings.Chr(10) + "Исправить?";

                    MessageBoxResult result = MessageBox.Show(strMsg, "", MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        //MessageBox.Show("1");
                        swModExt = swModel.Extension;
                        MessageBox.Show(Source8);
                        ok = swModExt.LoadDraftingStandard(Source8);

                        //if (MIni == 1)
                        //{
                            //Add(); // Доп. оформление
                        //}

                        Impr(); // Перегружаем все форматки чертежа
                    }
                    else
                    {
                        //MessageBox.Show("2");
                        swModel = (ModelDoc2)swDraw;
                        swModel.ForceRebuild3(false);
                    }

                    //lRetval = swApp.SendMsgToUser2(strMsg, (int) swMessageBoxIcon_e.swMbWarning, (int) swMessageBoxBtn_e.swMbYesNo);

                    //if (lRetval == 6) // Yes
                    //{
                    //    swModExt = swModel.Extension;
                    //    ok = swModExt.LoadDraftingStandard(Source8);

                    //    if (MIni == 1)
                    //    {
                    //        Add(); // Доп. оформление
                    //    }

                    //    Impr(); // Перегружаем все форматки чертежа
                    //}
                    //else
                    //{
                    //    swModel = (ModelDoc2)swDraw;
                    //    swModel.ForceRebuild3(false);
                    //}

                    //if (lRetval == 3) // No
                    //{
                    //    swModel = (ModelDoc2)swDraw;
                    //    swModel.ForceRebuild3(false);
                    //}
                }
            }


            }
                catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void Impr() // Перезагрузка форматок чертежа
        {
            MImpr = 1;
            for (j = 0; j <= vSheetNames.GetUpperBound(0); j++)
            {
                if (Strings.Left(vSheetNames[j], 3) == "DRW" | Strings.Left(vSheetNames[j], 4) == "Лист" | Strings.Left(vSheetNames[j], 5) == "Sheet" |
                    Strings.Left(vSheetNames[j], 2) == "SP" | Strings.Left(vSheetNames[j], 2) == "VP" | Strings.Left(vSheetNames[j], 3) == "LRI")
                {
                    varTemp = Strings.InStr(sSheetsNames1, vSheetNames[j]); // Проверяем имя
                    varTemp1 = Strings.InStr(sSheetsNames3, vSheetNames[j]); // Проверяем имя
                    varTemp2 = Strings.InStr(sSheetsNames4, vSheetNames[j]); // Проверяем имя
                    if (varTemp > 0 | varTemp1 > 0 | varTemp2 > 0) //Значение присутствует
                    {
                        swDraw.ActivateSheet(vSheetNames[j]);
                        swSheet = swDraw.GetCurrentSheet();
                        CmdRelod();
                        varTemp = Strings.InStr(sSheetsNames2, vSheetNames[j]); // Проверяем имя

                        if (varTemp > 0) //Значение присутствует
                        {
                            AnnDef();
                        }
                    }
                    else
                    {
                        varTemp = Strings.InStr(sSheetsNames2, vSheetNames[j]); // Проверяем имя
                        if (varTemp > 0) //Значение присутствует
                        {
                            AnnDef();
                        }
                    }
                }
            }
   
            Tests();
            MImpr = 0;
        }
        public void AnnDef() //Выставляем все по умолчанию
        {
            try
            {
                //Поиск объектов со шрифтом не по умолчанию
                swView = swDraw.GetFirstView();
                while (swView != null)
                {
                    swAnn = swView.GetFirstAnnotation3();
                    while (swAnn != null)
                    {
                        if (swAnn.OwnerType != (int)swAnnotationOwner_e.swAnnotationOwner_DrawingTemplate) //Исключаем объекты форматки
                        {
                            if (swAnn.GetUseDocTextFormat(0) == false) //Шрифт не по умолчанию)
                            {
                                swTextFormat = swAnn.GetTextFormat(0);
                                swAnn.SetTextFormat(0, true, swTextFormat); // Выставляем по умолчанию
                            }



                            //if ()
                            //{
                            //    if ()
                            //    {
                            //    }
                            //}




                        }
                           swAnn = swAnn.GetNext3();
                     }
                     swView = swView.GetNextView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Add()
        {
            swApp.SetUserPreferenceDoubleValue((int)swUserPreferenceDoubleValue_e.swDetailingBreakLineGap, 0.006);
            swApp.SetUserPreferenceDoubleValue((int)swUserPreferenceDoubleValue_e.swDetailingBreakLineExtension, 0);

            // Line Font
            swApp.SetUserPreferenceIntegerValue((int)swUserPreferenceIntegerValue_e.swLineFontVisibleEdgesThickness, (int)swLineWeights_e.swLW_NORMAL);
            swApp.SetUserPreferenceIntegerValue((int)swUserPreferenceIntegerValue_e.swLineFontHiddenEdgesThickness, (int)swLineWeights_e.swLW_THIN);
            swApp.SetUserPreferenceIntegerValue((int)swUserPreferenceIntegerValue_e.swLineFontSketchCurvesThickness, (int)swLineWeights_e.swLW_THIN);
            swApp.SetUserPreferenceIntegerValue((int)swUserPreferenceIntegerValue_e.swLineFontConstructionCurvesThickness, (int)swLineWeights_e.swLW_THIN);
            swApp.SetUserPreferenceIntegerValue((int)swUserPreferenceIntegerValue_e.swLineFontCrosshatchThickness, (int)swLineWeights_e.swLW_THIN);
            swApp.SetUserPreferenceIntegerValue((int)swUserPreferenceIntegerValue_e.swLineFontTangentEdgesThickness, (int)swLineWeights_e.swLW_THIN);
            swApp.SetUserPreferenceIntegerValue((int)swUserPreferenceIntegerValue_e.swLineFontCosmeticThreadThickness, (int)swLineWeights_e.swLW_THIN);
            swApp.SetUserPreferenceIntegerValue((int)swUserPreferenceIntegerValue_e.swLineFontHideTangentEdgeThickness, (int)swLineWeights_e.swLW_THIN);
            swApp.SetUserPreferenceIntegerValue((int)swUserPreferenceIntegerValue_e.swLineFontExplodedLinesThickness, (int)swLineWeights_e.swLW_THIN);
            swApp.SetUserPreferenceIntegerValue((int)swUserPreferenceIntegerValue_e.swLineFontBreakLineThickness, (int)swLineWeights_e.swLW_THIN);

            // Толщина линии
            swApp.SetUserPreferenceDoubleValue((int)swUserPreferenceDoubleValue_e.swPageSetupPrinterThinLineWeight, 0.0002);
            swApp.SetUserPreferenceDoubleValue((int)swUserPreferenceDoubleValue_e.swPageSetupPrinterNormalLineWeight, 0.0006);
            swApp.SetUserPreferenceDoubleValue((int)swUserPreferenceDoubleValue_e.swPageSetupPrinterThickLineWeight, 0.0008);
            swApp.SetUserPreferenceDoubleValue((int)swUserPreferenceDoubleValue_e.swPageSetupPrinterThick2LineWeight, 0.001);
            swApp.SetUserPreferenceDoubleValue((int)swUserPreferenceDoubleValue_e.swPageSetupPrinterThick3LineWeight, 0.0012);
            swApp.SetUserPreferenceDoubleValue((int)swUserPreferenceDoubleValue_e.swPageSetupPrinterThick4LineWeight, 0.0014);
            swApp.SetUserPreferenceDoubleValue((int)swUserPreferenceDoubleValue_e.swPageSetupPrinterThick5LineWeight, 0.0016);
            swApp.SetUserPreferenceDoubleValue((int)swUserPreferenceDoubleValue_e.swPageSetupPrinterThick6LineWeight, 0.0018);
            swApp.SetUserPreferenceDoubleValue((int)swUserPreferenceDoubleValue_e.swPageSetupPrinterThinLineWeight, 0.0002);
            swApp.SetUserPreferenceDoubleValue((int)swUserPreferenceDoubleValue_e.swPageSetupPrinterNormalLineWeight, 0.0006);
            swApp.SetUserPreferenceDoubleValue((int)swUserPreferenceDoubleValue_e.swPageSetupPrinterThickLineWeight, 0.0008);
            swApp.SetUserPreferenceDoubleValue((int)swUserPreferenceDoubleValue_e.swPageSetupPrinterThick2LineWeight, 0.001);
            swApp.SetUserPreferenceDoubleValue((int)swUserPreferenceDoubleValue_e.swPageSetupPrinterThick3LineWeight, 0.0012);
            swApp.SetUserPreferenceDoubleValue((int)swUserPreferenceDoubleValue_e.swPageSetupPrinterThick4LineWeight, 0.0014);
            swApp.SetUserPreferenceDoubleValue((int)swUserPreferenceDoubleValue_e.swPageSetupPrinterThick5LineWeight, 0.0016);
            swApp.SetUserPreferenceDoubleValue((int)swUserPreferenceDoubleValue_e.swPageSetupPrinterThick6LineWeight, 0.0018);
        }
        public void SaveData() // Сохранение данных листа
        {
            try
            {

            for (i = 0; i <= 35 - 1; i++)
            {
                s[i] = " ";
            }
 
            swSheet = swDraw.GetCurrentSheet();
            strSheetFormatName = swSheet.GetSheetFormatName();

            // Лист
            strTemp = "Revision2@" + strSheetFormatName;
            ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
 
            if (ok)
            {
                swNote = swSelMgr.GetSelectedObject2(1);
                s[0] = swNote.GetText();
            }
            // № докум.
            strTemp = "Revision3@" + strSheetFormatName;
            ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);

            if (ok)
            {
                swNote = (Note)swSelMgr.GetSelectedObject2(1);
                s[1] = swNote.GetText();
            }
            // Дата изм.
            strTemp = "Revision4@" + strSheetFormatName;
            ok =  swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
        
            if (ok)
            {
                swNote = swSelMgr.GetSelectedObject2(1);
                s[2] = swNote.GetText();
            }

            // Дата
            strTemp = "Date@" + strSheetFormatName;
            ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
         
            if (ok)
            {
                swNote = swSelMgr.GetSelectedObject2(1);
                s[3] = swNote.GetText();
            }
   
            // Лист
            strTemp = "Sheet1@" + strSheetFormatName;
            ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
   
            if (ok)
            {
                swNote = swSelMgr.GetSelectedObject2(1);
                s[4] = swNote.GetText();
            }
            //' Листов
            strTemp = "Sheet2@" + strSheetFormatName;
            ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);

            if (ok)
            {
                swNote = swSelMgr.GetSelectedObject2(1);
                s[5] = swNote.GetText();
            }


            if (Strings.Left(swSheet.GetName(), 2) == "SP")
            {

                strTemp = "Conf_00@" + strSheetFormatName;
                ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);

                if (ok)
                {


                    for (i = 0; i <= 9; i++)
                    {
                        // Исполнение
                        strTemp = "Conf_0" + i + "@" + strSheetFormatName;
                        ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
                        if (ok)
                        {
                            swNote = swSelMgr.GetSelectedObject2(1);
                            swAnn = swNote.GetAnnotation();
                            if (swAnn.Visible == (int)swAnnotationVisibilityState_e.swAnnotationHidden)
                            {
                                s[i + 6] = "0";
                            }
                            else
                            {
                                s[i + 6] = "1";
                            }
                        }

                        // Код
                        strTemp = "Code0" + i + "@" + strSheetFormatName;

                        ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
                        if (ok)
                        {
                            swNote = swSelMgr.GetSelectedObject2(1);
                            s[i + 16] = swNote.GetText();
                        }
                    
                        // Литера
                        strTemp = "Lit0" + i + "@" + strSheetFormatName;
                        swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
                        if (ok)
                        {
                            swNote = swSelMgr.GetSelectedObject2(1);
                            s[i + 26] = swNote.GetText();
                        }
                    }
   
                }
            }
     
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void RestoreData() // Восстановление данных листа
        {
            //swModel = (ModelDoc2) swDraw;
            swSheet = swDraw.GetCurrentSheet();
   
            strSheetFormatName = swSheet.GetSheetFormatName();

            // Лист
            strTemp = "Revision2@" + strSheetFormatName;

            ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
           
            if (ok)
            {
                swNote = swSelMgr.GetSelectedObject2(1);
                ok = swNote.SetText(s[0]);
            }

            strTemp = "Revision3@" + strSheetFormatName;
            ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
        
            if (ok)
            {
                swNote = swSelMgr.GetSelectedObject2(1);
                ok = swNote.SetText(s[1]);
            }

            strTemp = "Revision4@" + strSheetFormatName;
            ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
 
            if (ok)
            {
                swNote = swSelMgr.GetSelectedObject2(1);
                ok = swNote.SetText(s[2]);
            }

            strTemp = "Date@" + strSheetFormatName;
            ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
       
            if (ok)
            {
                swNote = swSelMgr.GetSelectedObject2(1);
                ok = swNote.SetText(s[3]);
            }

            strTemp = "Sheet1@" + strSheetFormatName;
            ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
            if (ok)
            {
                swNote = swSelMgr.GetSelectedObject2(1);
                ok = swNote.SetText(s[4]);
            }

            strTemp = "Sheet2@" + strSheetFormatName;
            ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
            if (ok)
            {
                swNote = swSelMgr.GetSelectedObject2(1);
                ok = swNote.SetText(s[5]);
            }
          
            if (Strings.Left(swSheet.GetName(), 2) == "SP" | Strings.Left(swSheet.GetName(), 2) == "VP")
            {
                strTemp = "Conf_00@" + strSheetFormatName;
                ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);


                if (ok)
                {
                    for (i = 0; i <= 9; i++)
                    {
                        strTemp = "Conf_0" + i + "@" + strSheetFormatName;
                        ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
                        if (ok)
                        {

                            swNote = swSelMgr.GetSelectedObject2(1);
                            swAnn = swNote.GetAnnotation();
                            if (s[i + 6] == "0")
                            {
                                swAnn.Visible = (int)swAnnotationVisibilityState_e.swAnnotationHidden;
                            }
                            else
                            {
                                swAnn.Visible = (int)swAnnotationVisibilityState_e.swAnnotationVisible;
                            }
                        }
                        strTemp = "Code0" + i + "@" + strSheetFormatName;

                        ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
                        if (ok)
                        {
                            swNote = swSelMgr.GetSelectedObject2(1);
                            ok = swNote.SetText(s[i + 16]);
                        }

                        strTemp = "Lit0" + i + "@" + strSheetFormatName;
                        ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
                        if (ok)
                        {
                            swNote = swSelMgr.GetSelectedObject2(1);
                            ok = swNote.SetText(s[i + 26]);
                        }


                    }

                   

                }
            }

      
            swModel.ClearSelection2(true);
        }
        public void Temp()
        {
            for (int i = 0; i <= swAnn.GetTextFormatCount() - 1; i++)
            {
                swTextFormat = swAnn.GetTextFormat(i);
                swTextFormat.Italic = true;
                swTextFormat.TypeFaceName = "Arial Narrow";
                if (swAnn.GetType() == 6) // swNote
                {
                    swTextFormat.CharHeightInPts = 30;
                    swNote = swAnn.GetSpecificAnnotation();
                    if (swNote.IsBomBalloon())
                    {
                        swTextFormat.CharHeightInPts = 5;
                    }
                    if (swNote.ReadOnly)
                    {
                        swTextFormat.CharHeightInPts = 5;
                    }
                    else
                    {
                        swTextFormat.CharHeightInPts = 16;
                    }
                }
                swAnn.SetTextFormat(i, false, swTextFormat);
            }
        }
        public void CmdStandardMethod()
        {

            try
            {
                swModExt = swModel.Extension;
                swModExt.LoadDraftingStandard(Source8);

                if (MIni == 1)
                {
                    Add(); // Доп. оформление
                }
                else
                {
                   
                    //AnnDef();

                    //Узнаем имя активного листа
                    swSheet = swDraw.GetCurrentSheet();
                    strActiveSheetName = swSheet.GetName();

                    //Tests();

                    //Возвращение активного листа
                    swDraw.ActivateSheet(strActiveSheetName);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        private void CmdStandard_Click(object sender, RoutedEventArgs e) // Исправить оформление чертежа
        {
            CmdStandardMethod();
        }
        public void MyStandard()
        {
            // Свойства ini файла
            strTemp = "Arial Narrow"; // Шрифт
            stdFontName = strTemp;

            strTemp = "3,5"; // Размер шрифта
            strTemp = Strings.Replace(strTemp, ".", ",");
            stdFontSize = Convert.ToDouble(strTemp);

            strTemp = "1"; // Наклонный
            if (strTemp == "1")
            {
                stdFontItalic = 1;
            }
            else
            {
                stdFontItalic = 0;
            }

            strTemp = "0"; // Жирный
            if (strTemp == "1")
            {
                stdFontBold = 1;
            }
            else
            {
                stdFontBold = 0;
            }

            strTemp = "29";
            stdDay = strTemp;

            strTemp = "4";
            stdMonth = strTemp;

            strTemp = "2010";
            stdYear = strTemp;

            strTemp = "15";
            stdHour = strTemp;

            strTemp = "6";
            stdMinute = strTemp;

            strTemp = "40";
            stdSecond = strTemp;

        }
        public void MyProperties()
        {
            strTemp = "1";

            if (strTemp == "1") // Проверка формата
            {
                prpTestFormat = 1;
            }
            else
            {
                prpTestFormat = 0;
            }

            if (strTemp == "1") // Проверка имен
            {
                prpTestName = 1;
            }
            else
            {
                prpTestName = 0;
            }

            // Проверка оформления
            if (strTemp == "1")
            {
                prpTestStandard = 1;
            }
            else
            {
                prpTestStandard = 0;
            }

            // Управление шрифтом
            if (strTemp == "1")
            {
                prpFontSize = 1;
            }
            else
            {
                prpFontSize = 0;
            }
        }
        private void FormatSize()
        {
            try
            {

            FormatArr[0, 0] = "A4-P";
            FormatArr[0, 1] = "210";
            FormatArr[0, 2] = "297";

            FormatArr[1, 0] = "A3-P";
            FormatArr[1, 1] = "297";
            FormatArr[1, 2] = "420";

            FormatArr[2, 0] = "A3-A";
            FormatArr[2, 1] = "420";
            FormatArr[2, 2] = "297";

            FormatArr[3, 0] = "A2-P";
            FormatArr[3, 1] = "420";
            FormatArr[3, 2] = "594";

            FormatArr[4, 0] = "A2-A";
            FormatArr[4, 1] = "594";
            FormatArr[4, 2] = "420";

            FormatArr[5, 0] = "A1-P";
            FormatArr[5, 1] = "594";
            FormatArr[5, 2] = "841";

            FormatArr[6, 0] = "A1-A";
            FormatArr[6, 1] = "841";
            FormatArr[6, 2] = "594";

            FormatArr[7, 0] = "A0-P";
            FormatArr[7, 1] = "841";
            FormatArr[7, 2] = "1189";

            FormatArr[8, 0] = "A0-A";
            FormatArr[8, 1] = "1189";
            FormatArr[8, 2] = "841";

            FormatArr[9, 0] = "A4x3-A";
            FormatArr[9, 1] = "630";
            FormatArr[9, 2] = "297";

            FormatArr[10, 0] = "A4x4-A";
            FormatArr[10, 1] = "841";
            FormatArr[10, 2] = "297";

            FormatArr[11, 0] = "A4x5-A";
            FormatArr[11, 1] = "1051";
            FormatArr[11, 2] = "297";

            FormatArr[12, 0] = "A4x6-A";
            FormatArr[12, 1] = "1261";
            FormatArr[12, 2] = "297";

            FormatArr[13, 0] = "A4x7-A";
            FormatArr[13, 1] = "1471";
            FormatArr[13, 2] = "297";

            FormatArr[14, 0] = "A4x8-A";
            FormatArr[14, 1] = "1682";
            FormatArr[14, 2] = "297";

            FormatArr[15, 0] = "A4x9-A";
            FormatArr[15, 1] = "1892";
            FormatArr[15, 2] = "297";

            FormatArr[16, 0] = "A3x3-A";
            FormatArr[16, 1] = "891";
            FormatArr[16, 2] = "420";

            FormatArr[17, 0] = "A3x4-A";
            FormatArr[17, 1] = "1189";
            FormatArr[17, 2] = "420";

            FormatArr[18, 0] = "A3x5-A";
            FormatArr[18, 1] = "1486";
            FormatArr[18, 2] = "420";

            FormatArr[19, 0] = "A3x6-A";
            FormatArr[19, 1] = "1783";
            FormatArr[19, 2] = "420";

            FormatArr[20, 0] = "A3x7-A";
            FormatArr[20, 1] = "2080";
            FormatArr[20, 2] = "420";

            FormatArr[21, 0] = "A2x3-A";
            FormatArr[21, 1] = "1261";
            FormatArr[21, 2] = "594";

            FormatArr[22, 0] = "A2x4-A";
            FormatArr[22, 1] = "1682";
            FormatArr[22, 2] = "594";

            FormatArr[23, 0] = "A2x5-A";
            FormatArr[23, 1] = "2102";
            FormatArr[23, 2] = "594";

            FormatArr[24, 0] = "A1x3-A";
            FormatArr[24, 1] = "1783";
            FormatArr[24, 2] = "841";

            FormatArr[25, 0] = "A1x4-A";
            FormatArr[25, 1] = "2378";
            FormatArr[25, 2] = "841";

            FormatArr[26, 0] = "A0x2-A";
            FormatArr[26, 1] = "1682";
            FormatArr[26, 2] = "1189";

            FormatArr[27, 0] = "A0x3-A";
            FormatArr[27, 1] = "2523";
            FormatArr[27, 2] = "1189";


            if (swSheet.GetName() == vSheetNames[0])
            {
                TemplateName = MIniMaster + FormatArr[0, 0] + "-1.slddrt";
            }
            else
            {
                TemplateName = MIniMaster + FormatArr[0, 0] + "-2.slddrt";
            }

            for (i = 0; i <= 28; i++)
            {
                if (Convert.ToString(vSheetProps[5] * 1000) == FormatArr[i, 1] & Convert.ToString(vSheetProps[6] * 1000) == FormatArr[i, 2])
                {
                    if (swSheet.GetName() == vSheetNames[0])
                    {
                        TemplateName = MIniMaster + FormatArr[i, 0] + "-1.slddrt";
                    }
                    else
                    {
                        TemplateName = MIniMaster + FormatArr[i, 0] + "-2.slddrt";
                    }
                    break;
                }
            }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}