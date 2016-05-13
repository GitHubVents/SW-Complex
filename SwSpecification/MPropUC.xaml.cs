using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SwSpecification;
using Microsoft.VisualBasic;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SwSpecification.DataTableClass;
using VentsMaterials;
using System.IO;

namespace SWPlus
{
    /// <summary>
    /// Interaction logic for MPropUC.xaml
    /// </summary>
    public partial class MProp : Window
    {
        public MProp()
        {
            InitializeComponent();
            Test();
        }
        #region " VARIABLES "

        private SetMaterials matDll = new SetMaterials();
        private DProp _dPropClass = new DProp();
        private CheckModel CheckModelClass = new CheckModel();

        private SldWorks swApp;
        public ModelDoc2 swModel;
        private DrawingDoc swDraw;
        private Sheet swSheet;
        private View swView;
        private Configuration swConfig;
        private SelectionMgr swSelMgr;
        private Note swNote;
        private Annotation swAnn;
        private TextFormat swTextFormat;
        private ModelDocExtension swModExt;
        private PartDoc swPartDoc;

        //private int MDoc;

        public int MDoc { get; set; }

        private int m;
        private int MStart;
        private int MSection;
        private int MAssem; // Метка флажка Сборка: 0 - можно менять флажок; 1 - нельзя менять флажок
        private int ConfNumb;
        private int MConf;
        private int MChkBlank; // Метка флажка "Заготовка": 0 - можно менять флажок; 1 - нельзя менять флажок 
        private int varTemp;
        private int varTemp1;
        private int prpFontSize;
        private int prpFileName;
        private int MDrw; // Метка соответствия чертежа и модели: 0 - чертеж не соответствует; 1 - чертеж нормальный

        private int MMass;
        // Метка флашка задать "Задать единицы и точность": 0 - можно менять флажок; 1 - нльзя менять флажок 

        private int intTemp;
        private int mRun;
        private int MDel; // Метка кнопки ""Очистить все свойства": 0 - кнопка нажата пользователем; 1 - программно
        private int MClose; // Метка закрытия макроса: 1 - макрос закрывается
        private int MTests; // Метка процедуры Tests: 0 - первычный вызов; 1 - вызов из ChkFormat_Click

        private int MForm;
            // Метка флажка Задать формат: 0 - пользователь изменил флажок; 1 - флажок изменен из программы

        private int m1; // Метка
        private int MIni1;
        private int MIni2;
        private string MIni3;
        private int MIni4;
        private int MIni5;
        private int MIni6;
        private int intDRWSheet; // Счетчик листов чертежа
        private int n; // Носитель
        private int k; // Носитель
        private int k1; // Носитель
        private int k2; // Носитель
        private int MChkMaterialSW; // Метка флажка "Материал SW": 0 - можно менять флажок; 1 - нельзя менять флажок
        private int MChk3; // Метка флажка "Сортамент": 0 - можно менять флажок; 1 - нельзя менять флажок
        private int prpTestVersion;
        private int prpTestFormat;
        private int prpTestName;
        private int prpTestStandard;
        private int LevelNumber; // Номер уровня для конфигурации

        private bool ok;
        private double MIni7;
        private double mv; // Масса
        private double vv; // Объем
        private double dv; // Плотность
        private double mvTemp;

        private Single singlTemp;

        private string sNumberReal; // Имя файла через свойство SW
        private string sModelName;
        public string sNumber; // При записи свойства - часть Обозначения
        private string sConfigName;
        private string sNumberTitle;
        private string strTemp;
        private string strTemp1;
        private string strTempEng;
        private string prpSection;
        private string Source4;
        private string strDescription; // Наименование в одну строку
        private string prpNameSep;

        //private string prpMass;
        //private string prpMassTable;

        private string prpDescriptionMulti;
        private string sSheetsNames1; // Имена листов со старыми версиями
        private string sSheetsNames2; // Имена листов с нечитаемыми форматами
        private string sSheetsNames3; // Имена листов с проблемами оформленкия
        private string strSheetFormatName; // Имя формата листа (SolidWorks)

        private string stdFontName;
        private double stdFontSize;
        private int stdFontItalic;
        private int stdFontBold;

        private string stdDay;
        private string stdMonth;
        private string stdYear;
        private string stdHour;
        private string stdMinute;
        private string stdSecond;
        private string strMsg; // Строка сообщения пользователя
        private int swMbWarning;
        private int swMbOk;
        private int iResult; // Метка

        private string prpFirstApplySP;
        private string prpDraftFirstApplySP;
        private string prpInformNumberSP;
        private string prpLitSP;
        private string prpInformNumberVP;
        private string prpQuantity;

        private string[] sDocData;
        private string[] vSheetNames;
        private double[] vMassProp;
        private string[] vConfNameArr;
        private string[] vConfNameArr2;
        private string[] vCustInfoNameArr; // Массив свойств файла
        private string[] sFormatArray; // Массив значений форматов из чертежа

        private int[] NumChildren; // Массив для количества извлеченных конфигураций
        private int[] NumLevel; // Массив для количества уровней вложенности конфигураций

        private long longTemp;
        private long lRetval;
        private int longstatus;
        private int longwarnings;

        public string MaterialTxt;

        private MPropUcProfil _profilForm;

        public string strActiveSheetName { get; set; } // Имя листа, который был активен при открытии чертежа

        #endregion
        #region " FORM LOAD "
        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            LoadComboBox();
            main();
        }
        public void main() // Перезагрузка листа DProp
        {
            mRun = 1;
            Test();
        }
        public void main_reload() // Перезагрузка листа DProp
        {
            mRun = 1;
            Test();
        }
        #endregion
        #region " LoadComboBox "
        private readonly DataTableSql _dtSql = new DataTableSql();
        public void LoadComboBox()
        {
            try
            {
                CboDrawingDoc.SelectedValuePath = "Name";
                CboDrawingDoc.DisplayMemberPath = "Code";
                CboDrawingDoc.ItemsSource = _dtSql.DocTypeDt().DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CboDrawingDoc_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                m = 0;

                var dt = new DataTableSql();

                var dtDrawDoc = dt.DocTypeDt();

                foreach (DataRow r in dtDrawDoc.Rows)
                {
                    var docData = r["Code"] + " - " + r["Name"];

                    strTemp = docData;

                    strTemp1 = Strings.Left(strTemp, Strings.InStr(strTemp, "-") - 1);
                    strTemp1 = Strings.Trim(strTemp1);

                    var cboDrawingDoText = CboDrawingDoc.Text;

                    if (cboDrawingDoText == "")
                    {
                        return;
                    }

                    if (cboDrawingDoText == strTemp1)
                    {
                        strTemp1 = Strings.Right(strTemp, Strings.Len(strTemp) - Strings.InStr(strTemp, "-"));
                        strTemp1 = Strings.Trim(strTemp1);
                        //TxtAssem2.Text = strTemp1;
                        m = 1;
                        return;
                    }
                }

                if (m == 1)
                {
                    //TxtAssem2.IsEnabled = false;
                }
                else
                {
                    //TxtAssem2.IsEnabled = true;
                }

                if (prpTestFormat == 1)
                {
                    var b = default(RoutedEventArgs);
                    ChkFormat_Click(true, b);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        private void ChkFormat_Click(object sender, RoutedEventArgs e)
        {
            if (MForm == 0) // Изменения разрешены
            {
                Tests(1);
            }
        }
        public void Tests(int mTests)
        {
            try
            {
                if (mRun == 0)
                {
                    prpTestVersion = 1;
                    //prpTestFormat = 1;
                    //prpTestStandard = 1;


                    if (prpTestVersion == 1 | prpTestFormat == 1 | prpTestStandard == 1)
                    {
                        // Проверка соответствия чертежа модели
                        if (MDrw == 1) // Чертеж нормальный для этой конфигурации
                        {
                            // ReDim sFormatArray(UBound(vSheetNames))
                            //Array.Resize(ref sFormatArray, vSheetNames.GetUpperBound(0));

                            sFormatArray = vSheetNames;

                            // Считываем проверяем форматы всех листов
                            intDRWSheet = 0;

                            k = 0;
                            k1 = 0;
                            k2 = 0;

                            sSheetsNames1 = "";
                            sSheetsNames2 = "";
                            sSheetsNames3 = "";

                            for (var i = 0; i <= vSheetNames.GetUpperBound(0); i++)
                            {
                                ok = swDraw.ActivateSheet(vSheetNames[i]);
                                swSheet = swDraw.GetCurrentSheet();
                                strSheetFormatName = swSheet.GetSheetFormatName();

                                // Проверка версии и оформления
                                if (Strings.Left(vSheetNames[i], 3) == "DRW" | Strings.Left(vSheetNames[i], 4) == "Лист" |
                                    Strings.Left(vSheetNames[i], 5) == "Sheet" | Strings.Left(vSheetNames[i], 2) == "SP")
                                {
                                    if (prpTestVersion == 1 & MTests == 0)
                                    {
                                        // Проверка версии форматки
                                        strTemp = "Version@" + strSheetFormatName;

                                        swModel = (ModelDoc2) swDraw;

                                        ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);

                                        if (ok)
                                        {
                                            swSelMgr = swModel.SelectionManager;

                                            swNote = swSelMgr.GetSelectedObject2(1);
                                            strTemp = swNote.GetText();

                                            // Проверка версии
                                            if (Strings.Right(strTemp, 1) != "4")
                                            {
                                                k1 = k1 + 1;
                                                sSheetsNames1 = sSheetsNames1 + vSheetNames[i] + ", ";
                                            }
                                        }
                                        else
                                        {
                                            k1 = k1 + 1;

                                            sSheetsNames1 = sSheetsNames1 + vSheetNames[i] + ", ";
                                        }
                                    }

                                    //if (prpTestStandard == 1 & MTests == 0)
                                    //{
                                    //    // Проверка оформления
                                    //    m = 0;
                                    //    swView = swDraw.GetFirstView();

                                    //    if (swView != null)
                                    //    {
                                    //        swNote = swView.GetFirstNote();

                                    //        var j = 0;

                                    //        while (j == 0)
                                    //        {
                                    //            if (swNote != null)
                                    //            {
                                    //                swAnn = swNote.GetAnnotation();

                                    //                if (swAnn.OwnerType != (int) swAnnotationOwner_e.swAnnotationOwner_DrawingTemplate) // Исключаем объекты форматки
                                    //                {

                                    //                    swNote = swNote.GetNext();
                                    //                }
                                    //                else
                                    //                {
                                    //                    j = 1;
                                    //                }
                                    //            }
                                    //        }

                                    //        if (swNote != null)
                                    //        {
                                    //            swAnn = swNote.GetAnnotation();
                                    //            swTextFormat = swAnn.GetTextFormat(0);

                                    //            if (swTextFormat.TypeFaceName != stdFontName)
                                    //            {
                                    //                m = 1;
                                    //            }

                                    //            if (swTextFormat.Italic & stdFontItalic == 0)
                                    //            {
                                    //                m = 1;
                                    //            }
                                    //            else if (swTextFormat.Italic == false & stdFontItalic == 1)
                                    //            {
                                    //                m = 1;
                                    //            }

                                    //            if (swTextFormat.Bold & stdFontBold == 0)
                                    //            {
                                    //                m = 1;
                                    //            }
                                    //            else if (swTextFormat.Bold == false & stdFontBold == 1)
                                    //            {
                                    //                m = 1;
                                    //            }
                                    //        }
                                    //        else
                                    //        {
                                    //            m = 1;
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        m = 1;
                                    //    }

                                    //    swView = swView.GetNextView();

                                    //    if (swView != null)
                                    //    {
                                    //        swNote = swView.GetFirstNote();

                                    //        if (swNote != null)
                                    //        {
                                    //            swAnn = swNote.GetAnnotation();
                                    //            swTextFormat = swAnn.GetTextFormat(0);

                                    //            if (swTextFormat.TypeFaceName != stdFontName)
                                    //            {
                                    //                m = 1;
                                    //            }
                                    //            if (swTextFormat.Italic & stdFontItalic == 0)
                                    //            {
                                    //                m = 1;
                                    //            }
                                    //            else if (swTextFormat.Italic == false & stdFontItalic == 1)
                                    //            {
                                    //                m = 1;
                                    //            }
                                    //            if (swTextFormat.Bold & stdFontBold == 0)
                                    //            {
                                    //                m = 1;
                                    //            }
                                    //            else if (swTextFormat.Bold == false & stdFontBold == 1)
                                    //            {
                                    //                m = 1;
                                    //            }
                                    //        }

                                    //    }

                                    //    swModExt = swModel.Extension;
                                    //    swTextFormat = swModExt.GetUserPreferenceTextFormat((int) swUserPreferenceTextFormat_e.swDetailingDimensionTextFormat,
                                    //            (int) swUserPreferenceOption_e.swDetailingDimension);

                                    //    if (swTextFormat.TypeFaceName != stdFontName | swTextFormat.CharHeight != stdFontSize / 1000)
                                    //    {
                                    //        m = 1;
                                    //    }

                                    //    if (swTextFormat.Italic & stdFontItalic == 0)
                                    //    {
                                    //        m = 1;
                                    //    }
                                    //    else if (swTextFormat.Italic == false & stdFontItalic == 1)
                                    //    {
                                    //        m = 1;
                                    //    }
                                    //    if (swTextFormat.Bold & stdFontBold == 0)
                                    //    {
                                    //        m = 1;
                                    //    }
                                    //    else if (swTextFormat.Bold == false & stdFontBold == 1)
                                    //    {
                                    //        m = 1;
                                    //    }


                                    //    if (m == 1)
                                    //    {
                                    //        k2 = k2 + 1;
                                    //        sSheetsNames2 = sSheetsNames2 + vSheetNames[i] + ", ";

                                    //    }


                                    //}
                                }


                                if (prpTestFormat == 1 & ChkFormat.IsChecked == false) // Формат читается из чертежа
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
                                            if (Strings.Len(Strings.Trim(strTemp)) > 8 &
                                                Strings.Left(strTemp, 6) == "Формат")
                                            {
                                                sFormatArray[intDRWSheet] = Strings.Right(strTemp,
                                                    Strings.Len(strTemp) - 7);
                                                intDRWSheet = intDRWSheet + 1;
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
                                } // 2 if
                            } // for i

                            //TODO: " имеют настандартную или устаревшую основную надпись. "

                            if (k1 > 0)
                            {
                                sSheetsNames1 = Strings.Left(sSheetsNames1, Strings.Len(sSheetsNames1) - 2);

                                //MessageBox.Show("Лист(ы) " + sSheetsNames1 + " имеют настандартную или устаревшую основную надпись. " +
                                //                                        "Текст основной надписи может отображаться неверно.");
                                //lRetval = swapp.SendMsgToUser2(strMsg, swMbWarning, swMbOk);
                            }

                            //if (k2 > 0)
                            //{
                            //    sSheetsNames2 = Strings.Left(sSheetsNames2, Strings.Len(sSheetsNames2) - 2);
                            //    MessageBox.Show("Лист(ы) " + sSheetsNames1 + " имеют ошибки оформления." + Strings.Chr(10) +
                            //                                            "Для исправления используйте макрос DProp");
                            //    //lRetval = swapp.SendMsgToUser2(strMsg, swMbWarning, swMbOk);
                            //}

                            #region Есть проверка формата

                            if (prpTestFormat == 1) // Есть проверка формата
                            {
                                if (CboDrawingDoc.Text == "СК")
                                {
                                    MForm = 1;
                                    ChkFormat.IsChecked = false;
                                    MForm = 0;
                                    ChkFormat.IsEnabled = false;

                                    CboFormat.IsEnabled = false;
                                    CboFormat.Text = "A4";
                                }
                                else if (CboDrawingDoc.Text == "ВП")
                                {
                                    MForm = 1;
                                    ChkFormat.IsChecked = false;
                                    MForm = 0;
                                    ChkFormat.IsEnabled = false;

                                    CboFormat.IsEnabled = false;
                                    CboFormat.Text = "A3";
                                }
                                else
                                {
                                    ChkFormat.IsEnabled = true;

                                    if (ChkFormat.IsChecked == false) // Формат читается из чертежа
                                    {
                                        CboFormat.IsEnabled = false;

                                        if (k == 0 & intDRWSheet > 0) // Все прочитанные форматы определены 
                                        {
                                            // Определяем число листов
                                            if (intDRWSheet > 1) // Листов больше одного
                                            {
                                                m = 0;
                                                // Сортируем форматы
                                                for (int i = 0; i < intDRWSheet - 1; i++)
                                                {
                                                    strTemp = sFormatArray[i];
                                                    n = i;
                                                    for (int j = 0; j < intDRWSheet - 2; j++)
                                                    {
                                                        // Длина равна, левые части нет
                                                        if (Strings.Len(strTemp) == Strings.Len(sFormatArray[j + 1]) &
                                                            Strings.Left(strTemp, 2) !=
                                                            Strings.Left(sFormatArray[j + 1], 2))
                                                        {
                                                            iResult =
                                                                Strings.StrComp(Strings.Left(sFormatArray[j + 1], 2),
                                                                    Strings.Left(strTemp, 2));
                                                            switch (iResult)
                                                            {
                                                                case -1: // меньше
                                                                    m = 1;
                                                                    break;
                                                                case 0: // равняется
                                                                    break;
                                                                case 1: // больше
                                                                    strTemp = sFormatArray[j + 1];
                                                                    n = j + 1;
                                                                    m = 1;
                                                                    break;
                                                            }
                                                        }
                                                        // Длина не равна, левые части равны
                                                        else if (Strings.Len(strTemp) != Strings.Len(sFormatArray[j + 1]) &
                                                                 Strings.Left(strTemp, 2) ==
                                                                 Strings.Left(sFormatArray[j + 1], 2))
                                                        {
                                                            iResult = Strings.StrComp(sFormatArray[j + 1], strTemp);
                                                            switch (iResult)
                                                            {
                                                                case -1:
                                                                    strTemp = sFormatArray[j + 1];
                                                                    n = j + 1;
                                                                    m = 1;
                                                                    break;
                                                                case 0:
                                                                    break;
                                                                case 1:
                                                                    m = 1;
                                                                    break;
                                                            }
                                                        }
                                                        // Длина равна, левые части тоже
                                                        else if (Strings.Len(strTemp) ==
                                                                 Strings.Len(sFormatArray[j + 1]) &
                                                                 Strings.Left(strTemp, 2) ==
                                                                 Strings.Left(sFormatArray[j + 1], 2))
                                                        {
                                                            iResult =
                                                                Strings.StrComp(
                                                                    Strings.Right(sFormatArray[j + 1], 1),
                                                                    Strings.Right(strTemp, 1));
                                                            switch (iResult)
                                                            {
                                                                case -1: // меньше
                                                                    strTemp = sFormatArray[j + 1];
                                                                    n = j + 1;
                                                                    m = 1;
                                                                    break;
                                                                case 0: // равняется
                                                                    break;
                                                                case 1: // больше
                                                                    m = 1;
                                                                    break;
                                                            }
                                                        }
                                                        // Длина равна, левые части нет
                                                        else
                                                        {
                                                            iResult =
                                                                Strings.StrComp(
                                                                    Strings.Left(sFormatArray[j + 1], 2),
                                                                    Strings.Left(strTemp, 2));
                                                            switch (iResult)
                                                            {
                                                                case -1: // меньше
                                                                    m = 1;
                                                                    break;
                                                                case 0: // равняется
                                                                    break;
                                                                case 1: // больше
                                                                    strTemp = sFormatArray[j + 1];
                                                                    n = j + 1;
                                                                    m = 1;
                                                                    break;
                                                            }
                                                        }
                                                    } // for 2 j

                                                    sFormatArray[n] = sFormatArray[i];
                                                    sFormatArray[i] = strTemp;
                                                } // for 1
                                            }

                                            // Заносим формат

                                            if (m == 0 | intDRWSheet == 1)
                                                // Все форматы одинаковые или формат всего один
                                            {
                                                if (Strings.Len(sFormatArray[0]) > 2) // Формат кратный
                                                {
                                                    CboFormat.SelectedIndex = 7;
                                                    note.Text = "*) " + sFormatArray[0];
                                                }
                                                else // Формат обычный
                                                {
                                                    CboFormat.Text = sFormatArray[0];
                                                }
                                            }
                                            else // Форматов много и они разные
                                            // Формируем запись форматов в примечание
                                            {
                                                strTemp = "";
                                                for (int i = 0; i < intDRWSheet - 1; i++)
                                                {
                                                    if (i != intDRWSheet - 1)
                                                    {
                                                        if (sFormatArray[i] != sFormatArray[i + 1])
                                                        {
                                                            strTemp = strTemp + " " + sFormatArray[i] + ",";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        strTemp = strTemp + " " + sFormatArray[i] + ",";
                                                    }
                                                }
                                                CboFormat.SelectedIndex = 7;
                                                note.Text = "*)" + Strings.Left(strTemp, Strings.Len(strTemp) - 1);
                                            }

                                            if (CboFormat.SelectedIndex == 7)
                                            {
                                                note.IsEnabled = false;
                                            }
                                            else
                                            {
                                                note.IsEnabled = true;
                                            }
                                        } // IF Если формат читается из чертежа
                                        else if (k == 0 & intDRWSheet == 0) // Нет подходящих листов чертежа
                                        {
                                            MessageBox.Show("Формат не определен.");
                                            CboFormat.Text = "";
                                            note.IsEnabled = true;
                                        }
                                        else // Один или более форматов не определены
                                        {
                                            sSheetsNames3 = Strings.Left(sSheetsNames3, Strings.Len(sSheetsNames3) - 2);
                                            //MessageBox.Show("Лист(ы) " + sSheetsNames3 + " не позволяют определить формат." + Strings.Chr(10) + "Для исправления используйте макрос DProp");
                                            // NotKeepFormOnTop
                                            //lRetval = swapp.SendMsgToUser2(strMsg, swMbWarning, swMbOk);
                                            // KeepFormOnTop
                                            CboFormat.Text = "";
                                            note.IsEnabled = true;
                                        }
                                    }
                                    else // Формат устанавливается пользователем
                                    {
                                        CboFormat.IsEnabled = true;
                                        note.IsEnabled = true;
                                    }

                                    if (MDoc == 1) // Открыт чертеж
                                    {
                                    }
                                    else // Открыта модель
                                    {
                                        swModel = swApp.ActivateDoc(Source4);
                                    }
                                }
                            }

                            #endregion
                        } // 1 if
                        else // С чертежем проблемы
                        {
                            if (prpTestFormat == 1) // Есть проверка формата
                            {
                                if (ChkFormat.IsChecked == false) // Формат читается из чертежа
                                {
                                    CboFormat.Text = "";
                                    CboFormat.IsEnabled = false;
                                    note.IsEnabled = true;
                                }
                                else // Формат устанавливается пользователем
                                {
                                    CboFormat.IsEnabled = true;
                                    note.IsEnabled = true;
                                }
                            }
                        }

                        if (prpTestFormat == 1) // Есть проверка формата
                        {
                            // Очистка примечания от *)
                            strTemp = note.Text;

                            if (CboFormat.Text != "*)" & Strings.Left(strTemp, 2) == "*)")
                            {
                                note.Text = "";
                            }
                        }
                        var e = default(SelectionChangedEventArgs);
                        CboFormat_LayoutUpdated(true, e);
                    }
                }

                if (swModel.GetType() == (int) swDocumentTypes_e.swDocDRAWING)
                {
                    swModel = swView.ReferencedDocument;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CboFormat_LayoutUpdated(object sender, EventArgs e)
        {
            if (CboFormat.Text == "БЧ")
            {
                note.IsEnabled = false;
                note.Text = "Масса";
            }
            else
            {
                if (note.Text == "Масса")
                {
                    note.Text = "";
                }

                if (CboFormat.SelectedIndex == 7)
                {
                    note.IsEnabled = false;
                }
                else
                {
                    note.IsEnabled = true;
                }
            }
        }
        public void ChkSplitConfiguration()
        {
            var confarray = matDll.GetConfigurationNames();
            {
                foreach (var confname in confarray)
                {
                    CheckModelClass.SplitConfigurations(confname);
                }
            }
        }
        // Загрузка формы
        public void MProp_Activate()
        {
            try
            {
                _profilForm = new MPropUcProfil();

                // Проверка типа открытого документа
                if (MDoc == 1) // Открыт чертеж
                {
                    //wModel = (ModelDoc2)swDraw;
                    sModelName = swView.GetReferencedModelName();
                    sConfigName = swView.ReferencedConfiguration;
                    swModel = swView.ReferencedDocument;

                    //if (SwSpecification.Properties.Settings.Default.ChkInsPrp & SwSpecification.Properties.Settings.Default.ChkStandard)
                    //{
                    //    DPropClass.CheckStart();
                    //    DPropClass.CmdRelod();
                    //    DPropClass.CmdStandardMethod();
                    //}
                    //else if (SwSpecification.Properties.Settings.Default.ChkInsPrp & SwSpecification.Properties.Settings.Default.ChkStandard == false)
                    //{
                    //    DPropClass.CheckStart();
                    //    DPropClass.CmdRelod();
                    //}
                    //else if (SwSpecification.Properties.Settings.Default.ChkStandard & SwSpecification.Properties.Settings.Default.ChkInsPrp == false)
                    //{
                    //    DPropClass.CheckStart();
                    //    DPropClass.CmdStandardMethod();
                    //}
                }
                else // Открытая модель
                {
                    vConfNameArr = swModel.GetConfigurationNames();

                    foreach (var confname in vConfNameArr)
                    {
                        if (confname == "По умолчанию")
                        {
                            var configuration = (IConfiguration) swModel.GetConfigurationByName("По умолчанию");
                            MessageBox.Show("Конфигурация \"По умолчанию\" будет переименована на \"00\"");
                            configuration.Name = "00";
                        }
                    }

                    // Add ConfigName
                    AddCustomProperty(vConfNameArr);
                    ChkSplitConfiguration();

                    if (MStart == 0) // Исходная загрузка
                    {
                        swConfig = swModel.GetActiveConfiguration();
                        sConfigName = swConfig.Name;
                    }
                    else
                    {
                        swModel.ForceRebuild3(true);
                    }
                }
                var con = new ConnectSqlString();
                ToSQL.Conn = con.Con;

                strTemp = "0";
                MIni1 = strTemp == "1" ? 1 : 0;
                strTemp = "1";
                MIni4 = strTemp == "1" ? 1 : 0;
                strTemp = "0";
                MIni5 = strTemp == "1" ? 1 : 0;
                strTemp = "0";
                MIni6 = strTemp == "1" ? 1 : 0;
                strTemp = "0";
                MIni2 = strTemp == "1" ? 1 : 0;

                TxtNumber.Width = Convert.ToDouble(ChkManual.IsChecked == true ? "270" : "330");

                MyProperties();

                if (prpTestStandard == 1)
                {
                    MyStandard();
                }

                m = 1;

                //TxtAssem2.Text = "";

                Source4 = swModel.GetPathName();

                // Обозначение
                // Получение имени модели для Обозначения
                sNumberTitle = swModel.GetTitle();

                // Проверка на наличие расширения в имени файла
                if (sNumberTitle.Length > 7)
                {
                    strTemp = Strings.Mid(sNumberTitle, Strings.Len(sNumberTitle) - 6, 4);

                    if (strTemp == ".SLD" || strTemp == ".sld")
                    {
                        sNumberTitle = Strings.Left(sNumberTitle, Strings.Len(sNumberTitle) - 7);
                    }
                }

                // Проверяем наличие ручного ввода Обозначения
                m = 0; // Метка заданного Наименования (=1)

                strTemp = "$PRP:" + Strings.Chr(34) + "SW-File Name" + Strings.Chr(34);

                if (prpFileName == 0) // Разделитель не используется
                {
                    if (swModel.CustomInfo["Обозначение"] == strTemp | swModel.CustomInfo["Обозначение"] == "")
                    {
                        ChkManual.IsChecked = false;
                        TxtNumber.Text = sNumberTitle;
                    }
                    else
                    {
                        ChkManual.IsChecked = true;
                        TxtNumber.Text = swModel.CustomInfo["Обозначение"];
                    }
                }
                else // Разделитель используем
                {
                    varTemp = Strings.InStr(sNumberTitle, prpNameSep);

                    if (varTemp > 0) // Разделитель найден
                    {
                        strTemp1 = swModel.CustomInfo["Обозначение"] + prpNameSep + swModel.CustomInfo["Наименование"];

                        if (sNumberTitle == strTemp1 | swModel.CustomInfo["Обозначение"] == "" |
                            swModel.CustomInfo["Обозначение"] == strTemp)
                        {
                            ChkManual.IsChecked = false;
                            TxtNumber.Text = Strings.Left(sNumberTitle, varTemp);

                            DescriptionTxtBox.Text = Strings.Right(sNumberTitle,
                                Strings.Len(sNumberTitle) - varTemp - Strings.Len(prpNameSep) + 1);
                            m = 1;
                        }
                        else
                        {
                            ChkManual.IsChecked = true;
                            TxtNumber.Text = swModel.CustomInfo["Обозначение"];
                        }
                    }
                    else // Нет разделителя
                    {
                        if (swModel.CustomInfo["Обозначение"] == strTemp | swModel.CustomInfo["Обозначение"] == "")
                        {
                            ChkManual.IsChecked = false;
                            TxtNumber.Text = sNumberTitle;
                        }
                        else
                        {
                            ChkManual.IsChecked = true;
                            TxtNumber.Text = swModel.CustomInfo["Обозначение"];
                        }
                    }
                }

                var b = default(RoutedEventArgs);
                ChkManual_Click(true, b);

                // Проверка свойств Заголовок и Наименование_ФБ
                if (m == 0)
                {
                    varTemp = Strings.InStr(swModel.CustomInfo2["", "Наименование"], Strings.Chr(10).ToString());
                    strTemp = swModel.CustomInfo2["", "Наименование_ФБ"];

                    ChkFont.IsChecked = Strings.InStrRev(strTemp, "<FONT size=3.5>") > 0;

                    if (Strings.Left(strTemp, 5) == "<FONT")
                    {
                        if (Strings.InStr(strTemp, "PTS") > 0)
                        {
                            varTemp = Strings.InStrRev(strTemp, "S>"); //<FONT size=14PTS> <FONT size=20PTS>

                            strTemp = Strings.Len(strTemp) > varTemp + 1
                                ? Strings.Right(strTemp, Strings.Len(strTemp) - varTemp - 1)
                                : "";
                        }
                        else
                        {
                            varTemp = Strings.InStrRev(strTemp, "5>");

                            strTemp = Strings.Len(strTemp) > varTemp + 1
                                ? Strings.Right(strTemp, Strings.Len(strTemp) - varTemp - 1)
                                : "";
                        }
                    }

                    //TODO: DescriptionEng Наименование_ФБ strTemp
                    strTempEng = swModel.CustomInfo2["", "DescriptionEng"];

                    TxtEngDescription.Text = strTempEng;

                    if (strTemp == "") // Если строчка пустая
                    {
                        strTemp = swModel.CustomInfo2["", "Наименование"];
                        DescriptionTxtBox.Text = strTemp == "" ? swModel.SummaryInfo[0] : strTemp;
                    }
                    else
                    {
                        DescriptionTxtBox.Text = strTemp;
                    }
                }
                else
                {
                    ChkFont.IsChecked = false;
                    ChkFont_Click(true, b);
                }
                // Управление шрифтом
                ChkFont.IsEnabled = prpFontSize != 0;
                #region Проверка свойства Исполнение
                // Проверка свойства Исполнение
                if (swModel.CustomInfo2[sConfigName, "Исполнение"] == "1" |
                    swModel.CustomInfo2[sConfigName, "Исполнение"] == "-1" |
                    swModel.CustomInfo2[sConfigName, "Исполнение"] == "True")
                {
                    Исполнение.IsChecked = true;
                    //ChkConf.Value = true;
                    //strTemp = CboConfig.Text;
                }
                else if (swModel.CustomInfo2[sConfigName, "Исполнение"] == "2")
                {
                    Исполнение.IsChecked = true;

                    //ChkConf.Value = activeconfname;

                    strTemp = swModel.CustomInfo2[sConfigName, "Обозначение"];
                    varTemp = Strings.InStrRev(strTemp, "-");

                    if (varTemp > 0)
                    {
                        strTemp = Strings.Right(strTemp, Strings.Len(strTemp) - varTemp);
                    }
                    else
                    {
                        strTemp = "";
                    }

                    TxtConf.Text = strTemp;
                }
                else
                {
                    Исполнение.IsChecked = false;
                    TxtConf.Text = "";
                }
                #endregion
                var e = default(RoutedEventArgs);
                Исполнение_Click(true, e);
                #region Заполнение списков Раздел и проверка свойства Раздел
                // Заполнение списков Раздел и проверка свойства Раздел
                MSection = 1;
                ComboBoxSection.Items.Clear();

                var dtSect = _dtSql.SectionDt();

                foreach (DataRow r in dtSect.Rows)
                {
                    strTemp = r["Name"].ToString();

                    ComboBoxSection.Items.Add(strTemp);
                }

                strTemp = swModel.CustomInfo2[sConfigName, "Раздел"];

                if (strTemp == "")
                {
                    strTemp = swModel.CustomInfo["Раздел"];
                }

                m = 0;

                for (var i = 0; i <= ComboBoxSection.Items.Count - 1; i++)
                {
                    if (strTemp == (string) ComboBoxSection.Items[i])
                    {
                        m = 1;
                    }
                }

                if (m == 1)
                {
                    ComboBoxSection.Text = strTemp;
                }
                else
                {
                    // Выставляем раздел
                    if (swModel.GetType() == (int) swDocumentTypes_e.swDocPART)
                    {
                        ComboBoxSection.Text = "Детали";
                        if (swModel.GetType() == (int) swDocumentTypes_e.swDocDRAWING)
                        {
                            ComboBoxSection.Text = "Детали";
                        }
                    }
                    else if (swModel.GetType() == (int) swDocumentTypes_e.swDocASSEMBLY)
                    {
                        ComboBoxSection.Text = "Сборочные единицы";
                        if (swModel.GetType() == (int) swDocumentTypes_e.swDocDRAWING)
                        {
                            ComboBoxSection.Text = "Сборочные единицы";
                        }
                    }
                }
                #endregion
                MSection = 0;
                var sCe = default(EventArgs);
                ComboBoxSection_DropDownClosed(true, sCe);
                #region Литера
                ////////////
                //
                //  Литера
                //
                ////////////
                var literaarray = new string[10];
                literaarray[0] = "";
                literaarray[1] = "П";
                literaarray[2] = "Э";
                literaarray[3] = "Т";
                literaarray[4] = "И";
                literaarray[5] = "О";
                literaarray[6] = "О1";
                literaarray[7] = "О2";
                literaarray[8] = "А";
                literaarray[9] = "Б";

                CboLit.ItemsSource = literaarray;

                strTemp = swModel.CustomInfo2[sConfigName, "Литера_Таблица"];

                if (strTemp == "") // Если строчка пустая
                {
                    strTemp = swModel.CustomInfo2[sConfigName, "Литера_ФБ"];
                }
                if (Strings.Left(strTemp, 5) == "<FONT")
                {
                    varTemp = Strings.InStrRev(strTemp, "5>");
                    if (Strings.Len(strTemp) > varTemp + 1)
                    {
                        strTemp = Strings.Right(strTemp, Strings.Len(strTemp) - varTemp - 1); // Удаляем пустуюу строку
                    }
                    else
                    {
                        strTemp = " ";
                    }
                }
                #endregion
                #region Проверка цифрового индекса
                // Проверка цифрового индекса
                if (Strings.Left(strTemp, 1) == "O" & Strings.Right(strTemp, 1) == "1") // O<FONT size=1.8>1
                {
                    strTemp = "O1";
                }
                else if (Strings.Left(strTemp, 1) == "O" & Strings.Right(strTemp, 1) == "2") // O<FONT size=1.8>2
                {
                    strTemp = "O2";
                }
                CboLit.Text = strTemp;
                #endregion
                #region Проверка свойств
                // Проверка свойства "Сборка"
                MAssem = 1; // Метка флажка Сборка: 0 - можно менять флажок; 1 - нельзя менять флажок
                if (swModel.GetType() == (int) swDocumentTypes_e.swDocPART)
                {
                    ChkAssem.Content = "Сборка";
                    if (swModel.CustomInfo2["", "Сборка"] == "-1" | swModel.CustomInfo2["", "Сборка"] == "True")
                    {
                        ChkAssem.IsChecked = true;
                    }
                    else
                    {
                        ChkAssem.IsChecked = false;
                    }
                }
                else
                {
                    ChkAssem.Content = "Деталь";
                    if (swModel.CustomInfo2["", "Сборка"] == "0" | swModel.CustomInfo2["", "Сборка"] == "False")
                    {
                        ChkAssem.IsChecked = true;
                    }
                    else
                    {
                        ChkAssem.IsChecked = false;
                    }
                }
                MAssem = 0; // Метка флажка Сборка: 0 - можно менять флажок; 1 - нельзя менять флажок

                MChkBlank = 1; // Метка флажка "Заготовка": 0 - можно менять флажок; 1 - нельзя менять флажок 

                // Проверка свойства Заготовка
                strTemp = swModel.CustomInfo2[sConfigName, "Заготовка"];
                if (strTemp != "")
                {
                    ChkBlank.IsChecked = true;
                }
                else
                {
                    ChkBlank.IsChecked = false;
                }

                MChkBlank = 0;
                ChkBlank_Click(true, e);

                // Проверка свойства Сборка2_ФБ
                strTemp = swModel.CustomInfo2[sConfigName, "Сборка2_ФБ"];
                if (strTemp == "")
                {
                    strTemp = swModel.CustomInfo2["", "Сборка2_ФБ"];
                }
                if (Strings.Left(strTemp, 5) == "<FONT")
                {
                    varTemp = Strings.InStrRev(strTemp, "5>");
                    if (Strings.Len(strTemp) > varTemp + 1)
                    {
                        strTemp = Strings.Right(strTemp, Strings.Len(strTemp) - varTemp - 1);
                    }
                    else
                    {
                        strTemp = " ";
                    }
                }
                #endregion
                //TxtAssem2.Text = strTemp;
                // Проверка свойства Сборка1_ФБ
                n = 0;
                MForm = 1; // Метка флажка задать формат: 0 - пользователь изменил флажок; 1 - флажок изменен из программы

                CboDrawingDoc.Text = swModel.CustomInfo2[sConfigName, "Сборка1_ФБ"];

                if (CboDrawingDoc.Text == "")
                {
                    CboDrawingDoc.Text = swModel.CustomInfo2["", "Сборка1_ФБ"];
                }

                MForm = 0; // Метка флажка задать формат: 0 - пользователь изменил флажок; 1 - флажок изменен из программы

                // Заполнение списка конфигураций
                // Проверка числа конфигураций
                ConfNumb = swModel.GetConfigurationCount(); // Общее число конфигураций
                vConfNameArr = swModel.GetConfigurationNames(); // Имена всех конфигураций для проверки "По умолчанию"

                //ChkAll.Value = 0;
                MConf = 1; // Метка списка конфигураций: 0 - пользователь изменил список; 1 - список изменен из программы
                #region Заполнение списка
                    // Заполнение списка
                    if (ConfNumb > 1 & MDoc == 0) // Конфигураций много и открыта модель
                    {
                        CboConfig.IsEnabled = true;
                        CboConfig.Items.Clear();

                        vConfNameArr2 = swModel.GetConfigurationNames(); // Имена всех конфигураций для загрузки CboConfig

                        foreach (var confname in vConfNameArr2)
                        {
                            Configuration swConf = swModel.GetConfigurationByName(confname);
                            if (swConf.IsDerived() == false)
                            {
                                if (int.TryParse(confname, out n))
                                {
                                    CboConfig.Items.Add(confname);
                                }
                                else
                                {
                                    MessageBox.Show("Конфигурацию \"" + confname +
                                                    " \"необходимо переименовать в числовое значение!");
                                    return;
                                }
                            }
                        }

                        CboConfig.SelectedValue = sConfigName;
                    }
                    else // Конфигурация одна или открыт чертеж
                    {
                        CboConfig.Items.Add(sConfigName);
                        CboConfig.Text = sConfigName;
                        CboConfig.IsEnabled = false;
                    }
                #endregion
                m = 0;
                #region Проверка соответствия чертежа и модели
                // Проверка соответствия чертежа и модели
                // Определение имени чертежа
                if (prpTestName == 1) // Есть проверка имени
                {
                    strTemp = Strings.Left(Source4, Strings.Len(Source4) - 7); // Путь к файлу модели
                    varTemp1 = Strings.InStrRev(strTemp, @"\");
                    varTemp = Strings.InStr(varTemp1, strTemp, prpNameSep);

                    if (Исполнение.IsChecked == true)
                    {
                        if (prpFileName == 1 & varTemp > 0)
                        {
                            if (CboDrawingDoc.Text == "" || CboDrawingDoc.Text == "СК")
                            {
                                
                                _sDrawName = Strings.Left(strTemp, varTemp - 1) + "-" + Strings.LTrim(TxtConf.Text) +
                                             Strings.Right(strTemp, Strings.Len(strTemp) - varTemp + 1) + ".SLDDRW";

                                if (File.Exists(_sDrawName) == false)
                                {
                                    _sDrawName = Strings.Left(strTemp, varTemp - 1) +
                                                 Strings.Right(strTemp, Strings.Len(strTemp) - varTemp + 1) + ".SLDDRW";

                                    m = 1;
                                }
                            }
                            else
                            {
                                _sDrawName = Strings.Left(strTemp, varTemp - 1) + "-" + Strings.LTrim(TxtConf.Text) +
                                             CboDrawingDoc.Text +
                                             Strings.Right(strTemp, Strings.Len(strTemp) - varTemp + 1) + ".SLDDRW";


                                if (File.Exists(_sDrawName) == false)
                                {
                                    _sDrawName = Strings.Left(strTemp, varTemp - 1) + CboDrawingDoc.Text +
                                                 Strings.Right(strTemp, Strings.Len(strTemp) - varTemp + 1) + ".SLDDRW";


                                    m = 1;
                                }
                            }
                        }
                        else
                        {
                            if (CboDrawingDoc.Text == "" || CboDrawingDoc.Text == "СК")
                            {
                                _sDrawName = Strings.Left(Source4, Strings.Len(Source4) - 7) + "-" +
                                             Strings.LTrim(TxtConf.Text) + ".SLDDRW";

                                if (File.Exists(_sDrawName) == false)
                                {
                                    _sDrawName = Strings.Left(Source4, Strings.Len(Source4) - 7) + ".SLDDRW";

                                    m = 1;
                                }
                            }
                            else
                            {
                                _sDrawName = Strings.Left(Source4, Strings.Len(Source4) - 7) + "-" +
                                             Strings.LTrim(TxtConf.Text) + CboDrawingDoc.Text + ".SLDDRW";

                                if (File.Exists(_sDrawName) == false)
                                {
                                    
                                    _sDrawName = Strings.Left(Source4, Strings.Len(Source4) - 7) + CboDrawingDoc.Text +
                                                 ".SLDDRW";


                                    m = 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (prpFileName == 1 & varTemp > 0)
                        {
                            if (CboDrawingDoc.Text == "" || CboDrawingDoc.Text == "СК")
                            {
                                _sDrawName = Strings.Left(Source4, Strings.Len(Source4) - 3) + "DRW";
                            }
                            else
                            {
                                _sDrawName = Strings.Left(strTemp, varTemp - 1) + CboDrawingDoc.Text +
                                             Strings.Right(strTemp, Strings.Len(strTemp) - varTemp + 1) + ".SLDDRW";
                            }
                        }
                        else
                        {
                            if (CboDrawingDoc.Text == "" || CboDrawingDoc.Text == "СК")
                            {
                                _sDrawName = Strings.Left(Source4, Strings.Len(Source4) - 3) + "DRW";
                            }
                            else
                            {
                                //_sDrawName = Strings.Left(Source4, Strings.Len(Source4) - 7) + CboDrawingDoc.Text +
                                //             ".SLDDRW";

                                _sDrawName = Strings.Left(Source4, Strings.Len(Source4) - 7) + ".SLDDRW";
                            }
                        }
                    }
                }
                else
                {
                    _sDrawName = Strings.Left(Source4, Strings.Len(Source4) - 3) + "DRW";
                }
                #endregion
                longstatus = 0;
                longwarnings = 0;
                // Проверка соответствия
                MDrw = 0;
                #region " Проверка соответствия "

                if (MDoc == 1) // Открыт чертеж
                {
                    TextBlockLabel.Text = " Модель: " + sModelName;
                    TextBlockLabel.Foreground = new SolidColorBrush(Colors.Black);
                    MDrw = 1;

                    swModel = (ModelDoc2) swDraw;
                    swModel.ForceRebuild3(false);
                }
                else // Открыта модель
                {
                    // Проверка существования чертежа
                    if (File.Exists(_sDrawName)) // Чертеж существует
                    {
                        // Проверка соответствия чертежа текущей конфигурации
                        swDraw =
                            (DrawingDoc)
                                swApp.OpenDoc6(_sDrawName, (int) swDocumentTypes_e.swDocDRAWING,
                                    (int) swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref longstatus,
                                    ref longwarnings);

                        swSheet = swDraw.GetCurrentSheet();
                        strActiveSheetName = swSheet.GetName(); // Узнаем имя активного листа 
                        vSheetNames = swDraw.GetSheetNames();
                        swDraw.ActivateSheet(vSheetNames[0]);
                        swSheet = swDraw.GetCurrentSheet();
                        swView = swDraw.GetFirstView();

                        m1 = 0;

                        if (swSheet.CustomPropertyView == "По умолчанию" || swSheet.CustomPropertyView == "Default")
                        {
                            swView = swView.GetNextView(); // Получаем первый вид
                        }
                        else
                        {
                            while (swView != null)
                            {
                                if (swView.GetName2() == swSheet.CustomPropertyView)
                                {
                                    m1 = 1;
                                    break;
                                }
                                swView = swView.GetNextView();
                            }
                            if (m1 == 0)
                            {
                                swView = swDraw.GetFirstView();
                                swView = swView.GetNextView();
                                swApp.SendMsgToUser(
                                    "Не удалось определить вид из свойств листа. Используйтся первый вид.");
                            }
                        }

                        if (swView == null)
                        {
                            TextBlockLabel.Text = " Чертеж с именем " + _sDrawName + " не содержит видов";

                            TextBlockLabel.Foreground = new SolidColorBrush(Colors.Red);
                        }
                        else
                        {
                            sModelName = swView.GetReferencedModelName();

                            if (sModelName == Source4) // Модель соответствует
                            {
                                strTemp = swView.ReferencedConfiguration;

                                if (strTemp == sConfigName) // Конфигурация соответствует
                                {
                                    if (m == 1)
                                    {
                                        TextBlockLabel.Text = "Считан груповой чертеж " + _sDrawName +
                                                              ". Конфигурация совпадает";
                                        TextBlockLabel.Foreground = new SolidColorBrush(Colors.Blue);
                                        MDrw = 1;
                                            // Метка соответствия чертежа и модели: 0 - чертеж не соответствует; 1 - чертеж нормальный
                                    }
                                    else
                                    {
                                        TextBlockLabel.Text = " Чертеж: " + _sDrawName;
                                        TextBlockLabel.Foreground = new SolidColorBrush(Colors.Black);
                                        MDrw = 1;
                                            // Метка соответствия чертежа и модели: 0 - чертеж не соответствует; 1 - чертеж нормальный
                                    }
                                }
                                else // Конфигурация другая
                                {
                                    if (m == 1)
                                    {
                                        TextBlockLabel.Text = "Считан груповой чертеж " + _sDrawName +
                                                              ". В чертеже использована конфигурация " + strTemp;
                                        TextBlockLabel.Foreground = new SolidColorBrush(Colors.Blue);


                                        MDrw = 1;
                                            // Метка соответствия чертежа и модели: 0 - чертеж не соответствует; 1 - чертеж нормальный
                                    }
                                    else
                                    {
                                        swConfig = swModel.GetConfigurationByName(strTemp);
                                            // Проверяем конфигурацию для чертежа
                                        if (swConfig.IsDerived()) // Производная конфигурация
                                        {
                                            if (Strings.InStr(strTemp, "Для чертежа") > 0 ||
                                                Strings.InStr(strTemp, "для чертежа") > 0)
                                            {
                                                m = 1;
                                            }
                                        }

                                        if (m == 1)
                                        {
                                            TextBlockLabel.Text = "В чертеже с именем " + _sDrawName +
                                                                  ". использована конфигурация " + strTemp;
                                            TextBlockLabel.Foreground = new SolidColorBrush(Colors.Green);

                                            MDrw = 1;
                                                // Метка соответствия чертежа и модели: 0 - чертеж не соответствует; 1 - чертеж нормальный
                                        }
                                        else
                                        {
                                            TextBlockLabel.Text = "В чертеже с именем " + _sDrawName +
                                                                  ". использована конфигурация " + strTemp;
                                            TextBlockLabel.Foreground = new SolidColorBrush(Colors.Red);
                                        }
                                    }
                                }
                            }
                            else if (sModelName == "")
                            {
                                TextBlockLabel.Text = "В чертеже с именем " + _sDrawName +
                                                      ". не содержит вида с моделью";
                                TextBlockLabel.Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else // Модель другая
                            {
                                TextBlockLabel.Text = "В чертеже с именем " + _sDrawName + ". ссылается на модель" +
                                                      sModelName;
                                TextBlockLabel.Foreground = new SolidColorBrush(Colors.Red);
                            }
                        }
                    }
                    else // Файла чертежа нет
                    {
                        TextBlockLabel.Text = "В чертеже с именем " + _sDrawName + ". не существует";
                        TextBlockLabel.Foreground = new SolidColorBrush(Colors.Red);
                    }

                    swModel = swApp.ActivateDoc(Source4);
                }

                #endregion
                // Проверка свойств "Формат", "Примечание" и "Формат_ФБ"
                // Проверка свойств "Примечание"
                note.Text = swModel.CustomInfo2[sConfigName, "Примечание"];
                if (note.Text == ""){note.Text = swModel.CustomInfo2["", "Примечание"];}
                #region Заполнение свойств
                    #region Заполнение списка формат и проверка свойства Формат
                        CboFormat.Items.Clear();
                        CboFormat.Items.Add("");
                        CboFormat.Items.Add("A4");
                        CboFormat.Items.Add("A3");
                        CboFormat.Items.Add("A2");
                        CboFormat.Items.Add("A1");
                        CboFormat.Items.Add("A0");
                        CboFormat.Items.Add("БЧ");
                        CboFormat.Items.Add("*)");
                        CboFormat.Items.Add("-");

                        CboFormat.Text = swModel.CustomInfo2[sConfigName, "Формат"];

                        //CboFormat.IsEnabled = false;

                        if (CboFormat.Text == "")
                        {
                            CboFormat.Text = swModel.CustomInfo2["", "Формат"];
                        }

                        // Проферка свойства "Формат_ФБ"
                        if (prpTestFormat == 1) // Есть проверка
                        {
                            MForm = 1;
                            // Метка флажка Задать формат: 0 - пользователь изменил флажок; 1 - флажок изменен из программы
                            ChkFormat.IsEnabled = true;

                            if (swModel.CustomInfo2[sConfigName, "Формат_ФБ"] == "-1" |
                                swModel.CustomInfo2[sConfigName, "Формат_ФБ"] == "True" |
                                swModel.CustomInfo2[sConfigName, "Формат_ФБ"] == "")
                            {
                                ChkFormat.IsChecked = false; // В спецификации наоборот
                            }
                            else
                            {
                                ChkFormat.IsChecked = true;
                            }

                            MForm = 0;
                        }
                        else // Нет проверки
                        {
                            MForm = 1;
                            ChkFormat.IsEnabled = false;
                            ChkFormat.IsChecked = true;
                            CboFormat.IsEnabled = true;
                            note.IsEnabled = true;
                            MForm = 0;
                        }
                    #endregion
                    Tests(0);
                    // Заполнение списка Формат для Заготовки
                    var formatmatarray = new string[9];
                    formatmatarray[0] = "";
                    formatmatarray[1] = "A4";
                    formatmatarray[2] = "A3";
                    formatmatarray[3] = "A2";
                    formatmatarray[4] = "A1";
                    formatmatarray[5] = "A0";
                    formatmatarray[6] = "БЧ";
                    formatmatarray[7] = "*)";
                    formatmatarray[8] = "-";

                    CboBlankFormat.ItemsSource = formatmatarray;

                    #region Заполнение списка масс
                    // Заполнение списка масс
                    CboMass.Items.Clear();
                    CboMass.Items.Add("Миллиграммы");
                    CboMass.Items.Add("Граммы");
                    CboMass.Items.Add("Килограммы");
                    CboMass.Items.Add("Фунты");

                    // Заполнение списка точностей
                    CboTol.Items.Clear();
                    for (var i = 0; i <= 8; i++)
                    {
                        CboTol.Items.Add(i);
                    }

                    vMassProp = swApp.GetMassProperties2(Source4, sConfigName, 1);

                    if (vMassProp != null) // Деталь имеет массу
                    {
                        mv = vMassProp[5];
                        vv = vMassProp[3];

                        MMass = 1; // Метка флашка задать "Задать единицы и точность": 0 - можно менять флажок; 1 - нльзя менять флажок
                        if (swModel.CustomInfo2[sConfigName, "Единицы"] == "0" ||
                            swModel.CustomInfo2[sConfigName, "Единицы"] == "False") // Пользовательские настройки массы
                        {
                            swModel.SetUserPreferenceIntegerValue((int) swUserPreferenceIntegerValue_e.swUnitSystem,
                                (int) swUnitSystem_e.swUnitSystem_Custom);
                            ChkDefMass.IsChecked = true;
                            longTemp =
                                swModel.GetUserPreferenceIntegerValue(
                                    (int) swUserPreferenceIntegerValue_e.swUnitsMassPropMass);

                            switch (longTemp)
                            {
                                case (int) swUnitsMassPropMass_e.swUnitsMassPropMass_Milligrams:
                                    CboMass.SelectedIndex = 0;
                                    break;
                                case (int) swUnitsMassPropMass_e.swUnitsMassPropMass_Grams:
                                    CboMass.SelectedIndex = 1;
                                    break;
                                case (int) swUnitsMassPropMass_e.swUnitsMassPropMass_Kilograms:
                                    CboMass.SelectedIndex = 2;
                                    break;
                                case (int) swUnitsMassPropMass_e.swUnitsMassPropMass_Pounds:
                                    CboMass.SelectedIndex = 3;
                                    break;
                            }

                            longTemp =
                                swModel.GetUserPreferenceIntegerValue(
                                    (int) swUserPreferenceIntegerValue_e.swUnitsMassPropDecimalPlaces);
                            CboTol.SelectedIndex = Convert.ToInt32(longTemp);
                        }
                        else if (swModel.CustomInfo2[sConfigName, "Единицы"] == "-1" ||
                                 swModel.CustomInfo2[sConfigName, "Единицы"] == "True") // Масса по умолчанию
                        {
                            ChkDefMass.IsChecked = false;
                        }
                        else // Без настроек
                        {
                            ChkDefMass.IsChecked = false;
                        }

                        MMass = 0;
                            // Метка флажка "Задать единицы и точность": 0 - можно менять флажок; 1 - нельяза менять флажок
                        ChkDefMass_Click();
                    }
                    else // Деталь не имеет массы
                    {
                        ChkDefMass.IsEnabled = false;
                        CboMass.IsEnabled = false;
                        CboTol.IsEnabled = false;
                        TxtMass.Text = "0";
                        TxtMass.IsEnabled = false;
                        LblMass.Content = "";
                    }
                    #endregion
                    foreach (DataRow r in dtSect.Rows)
                    {
                        strTemp = r["Name"].ToString();
                        CboBlankSection.Items.Add(strTemp);
                    }
                    #region Заполнение списка точностей
                        // Заполнение списка точностей
                        CboTol.Items.Clear();
                        var performance = swModel.CustomInfo2[sConfigName, "Исполнение"];
                        if (performance == "1")
                        {
                            Исполнение.IsEnabled = true;
                            Исполнение.IsChecked = true;
                        }
                        CboBlankGroup.IsEnabled = false;
                    #endregion
                #endregion
                #region Проверка: указывается ли литера в чертеже
                // Проверка: указывается ли литера в чертеже
                strTemp = swModel.CustomInfo2[sConfigName, "Литера_ФБ"];
                varTemp = Strings.InStr(strTemp, "табл"); // Проверяем значение
                // true если значение присутствует
                ChkLitTable.IsChecked = varTemp > 0 ? true : false;
                for (var i = 0; i <= 8; i++)
                {
                    CboTol.Items.Add(i);
                }
                #endregion
                #region Проверка: указывается ли масса в чертеже
                // Проверка: указывается ли масса в чертеже
                strTemp = swModel.CustomInfo2[sConfigName, "Масса_ФБ"];
                    if (Strings.Left(strTemp, 5) == "<FONT")
                    {
                        varTemp = Strings.InStrRev(strTemp, "5>");
                        strTemp = Strings.Len(strTemp) > varTemp + 1 ? Strings.Right(strTemp, Strings.Len(strTemp) - varTemp - 1) : " ";
                    }
                    if (strTemp == "-")
                    {
                        ChkNoMass.IsChecked = true;
                    }
                    else if (Strings.Left(strTemp, 3) == "Див.")
                    {
                        ChkMassTable.IsChecked = true;
                    }
                    else
                    {
                        ChkNoMass.IsChecked = false;
                        ChkMassTable.IsChecked = false;
                    }
                    if (mRun == 1)
                    {
                        //var e = default(RoutedEventArgs);
                        ApplyAndClose_Click(true, e);
                    }
                #endregion
            }
            catch (COMException ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }
        private void CboMass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mass();
        }
        private void CboTol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mass();
        }
        private void ChkDefMass_Click()
        {
            if (ChkDefMass.IsChecked == false)
            {
                CboMass.IsEnabled = false;
                CboTol.IsEnabled = false;
            }
            else
            {
                CboMass.IsEnabled = true;
                CboTol.IsEnabled = true;
            }
            mass();
        }
        private void mass()
        {
            // Зависят CboMass, CboTol
            if (ChkDefMass.IsChecked == true) // Пользовательские настройки массы
            {
                switch (CboMass.SelectedIndex)
                {
                    case 0:
                        singlTemp = 1000000;
                        strTemp = "мг";
                        break;
                    case 1:
                        singlTemp = 1000;
                        strTemp = "г";
                        break;
                    case 2:
                        singlTemp = 1;
                        strTemp = "кг";
                        break;
                    case 3:
                        singlTemp = Convert.ToSingle(0.4536);
                        strTemp = "ф";
                        break;
                }

                intTemp = CboTol.SelectedIndex;
                mvTemp = mv*singlTemp;

                var myRoundString = MyRound(mvTemp, Convert.ToInt32(intTemp));

                TxtMass.Text = myRoundString.ToString();

                LblMass.Content = strTemp;
            }
            else // Масса по умолчанию
            {
                mvTemp = mv;

                if (mv > 0.1) // Масса больше 100 грамм
                {
                    var myRoundString = MyRound(mvTemp, 2);

                    TxtMass.Text = myRoundString.ToString();

                    LblMass.Content = "кг";

                    CboMass.SelectedIndex = 2;
                    CboTol.SelectedIndex = 3;
                }
                else // Масса меньше 100 грамм
                {
                    mvTemp = mvTemp*1000;

                    var myRoundString = MyRound(mvTemp, 1);
                    ;

                    TxtMass.Text = myRoundString.ToString();
                    LblMass.Content = "г";

                    CboMass.SelectedIndex = 1;
                    CboTol.SelectedIndex = 1;
                }
            }
        }
        // Округление
        private double MyRound(double RoundValue, int PrecValue)
        {
            //int PrecValueInt;
            //double Delta;

            long j;

            PrecValue = Math.Abs(PrecValue);

            var PrecValueInt = (int) (PrecValue);

            j = 1;

            for (var i = 1; i <= PrecValueInt; i++)
            {
                RoundValue = RoundValue*10;
                j = j*10;
            }

            var Delta = RoundValue - Math.Truncate(RoundValue);

            if (Delta >= 0.5)
            {
                RoundValue = Math.Truncate(RoundValue);
                RoundValue = RoundValue + 1;
            }
            else
            {
                RoundValue = Math.Truncate(RoundValue);
            }

            RoundValue = RoundValue/j;


            return RoundValue;
        }
        void AddCustomProperty(object[] confArr)
        {
            foreach (var item in confArr)
            {
                var swExtModel = swModel.Extension;
                var custProperty = swExtModel.CustomPropertyManager[item.ToString()];
                custProperty.Add2("ConfigName", (int)swCustomInfoType_e.swCustomInfoText, item.ToString());
            }
        }
        public void GetCustomProperty(string confname)
        {
            var swExtModel = swModel.Extension;

            var custProperty = swExtModel.CustomPropertyManager[confname];
            string val = null;
            string valout = null;

            custProperty.Get4("Масса_Таблица", true, out val, out valout);

            TxtMass.Text = valout;
        }
        #region " Проверка "
        public void Test()
        {
            swApp = (SldWorks) Marshal.GetActiveObject("SldWorks.Application");
            swModel = swApp.ActiveDoc;

            mRun = 0;
            m = 0;

            // Проверка открытого документа
            if (swModel == null)
            {
                MessageBox.Show("Откройте модель, сборку или чертеж!");
                Close();
                return;
            }

            if (string.IsNullOrEmpty(swModel.GetPathName()))
            {
                MessageBox.Show("Сохраните файл!");
                Close();
                return;
            }

            // Определение типа документа
            if (swModel.GetType() == (int) swDocumentTypes_e.swDocDRAWING)
            {
                MDoc = 1;

                swDraw = (DrawingDoc) swModel;

                // Получение первого листа
                swSheet = swDraw.GetCurrentSheet();
                strActiveSheetName = swSheet.GetName();

                // Узнаем имя активного листа
                vSheetNames = swDraw.GetSheetNames();
                swDraw.ActivateSheet(vSheetNames[0]);
                swSheet = swDraw.GetCurrentSheet();
                swView = swDraw.GetFirstView();

                m = 0;

                if (swSheet.CustomPropertyView == "По умолчанию" | swSheet.CustomPropertyView == "Default")
                {
                    swView = swView.GetNextView();
                    // Получаем первый вид
                }
                else
                {
                    while (swView != null)
                    {
                        if (swView.GetName2() == swSheet.CustomPropertyView)
                        {
                            m = 1;
                        }
                        swView = swView.GetNextView();
                    }
                    if (m == 0)
                    {
                        swView = swDraw.GetFirstView();
                        swView = swView.GetNextView();
                        MessageBox.Show("Не удалось определить вид из свойств листа. Ипользуется первый вид.");
                    }
                }

                if (swView == null)
                {
                    MessageBox.Show("Отсутсвует модель!");
                    // Возвращение активного листа
                    swDraw.ActivateSheet(strActiveSheetName);
                    Close();
                    return;
                }

                if (swView.ReferencedDocument == null)
                {
                    MessageBox.Show("Отсутсвует модель!");
                    // Возвращение активного листа
                    swDraw.ActivateSheet(strActiveSheetName);
                    Close();
                    return;
                }
            }
            else
            {
                MDoc = 0; // Метка документа: 0 - модель; 1 - чертеж
            }


            MProp_Activate();
        }
        #endregion
        #region " CHECKBOXES "

        #region " ChkBox Формат "

        private void ChkFormat_Checked(object sender, RoutedEventArgs e)
        {
            CboFormat.IsEnabled = true;
        }

        private void ChkFormat_Unchecked(object sender, RoutedEventArgs e)
        {
            CboFormat.IsEnabled = false;
        }
        #endregion
        #region " ChkBox Исполнение "
        // ChkBox Исполнение
         private void Исполнение_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Исполнение.IsChecked == true)
                {
                    TxtConf.Visibility = Visibility.Visible;
                    LblClass3.Visibility = Visibility.Visible;
                    TxtNumber.Width = Convert.ToDouble("267");

                    TxtConf.IsEnabled = false;

                    varTemp = Strings.InStr(sConfigName, " ");

                    if (varTemp > 0)
                    {
                        strTemp = Strings.Left(sConfigName, varTemp - 1);
                    }
                    else
                    {
                        strTemp = sConfigName;
                    }

                    TxtConf.Text = strTemp;
                }
                else
                {
                    TxtConf.Visibility = Visibility.Hidden;
                    LblClass3.Visibility = Visibility.Hidden;
                    TxtNumber.Width = Convert.ToDouble("332");

                    TxtConf.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region " ChkBox Ввести Обознач. "
        // ChkBox Ввести Обознач.
        private void ChkManual_Click(object sender, RoutedEventArgs e)
        {
            if (ChkManual.IsChecked == true)
            {
                TxtNumber.IsEnabled = true;
                DescriptionTxtBox.IsEnabled = true;
            }
            else
            {
                var swNumber = swModel.GetTitle();
                var swNumberLenght = swNumber.Length;

                TxtNumber.IsEnabled = false;
                TxtNumber.Text = Strings.Left(sNumberTitle, swNumberLenght);


                //if (prpFileName == 0)
                //{
                //    TxtNumber.Text = sNumberTitle;
                //}
                //else
                //{
                //    varTemp = Strings.InStr(sNumberTitle, prpNameSep);
                //    if (varTemp > 0)
                //    {
                //        DescriptionTxtBox.IsEnabled = false;
                //        TxtNumber.Text = Strings.Left(sNumberTitle, varTemp);
                //        DescriptionTxtBox.Text = Strings.Right(sNumberTitle, Strings.Len(sNumberTitle) - varTemp - Strings.Len(prpNameSep) + 1);
                //        ChkFont.IsChecked = false;
                //        ChkFont_Click(true, e);
                //    }
                //    else
                //    {
                //        TxtNumber.Text = sNumberTitle;
                //    }
                //}
            }
        }
        #endregion
        #region " ChkBox Загатовка "
        private void ChkBlank_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MChkBlank == 0) // Изменения разрешены
                {
                    if (ChkBlank.IsChecked == true)
                    {
                        TxtBlankNumber.IsEnabled = true;
                        TxtBlankDescription.IsEnabled = true;
                        CboBlankFormat.IsEnabled = true;
                        TxtBlankRemark.IsEnabled = true;
                        CboBlankSection.IsEnabled = true;

                        strTemp = swModel.CustomInfo2[sConfigName, "Заготовка"];
                        varTemp = Strings.InStr(strTemp, "$");
                        if (varTemp > 0)
                        {
                            CboBlankFormat.SelectedValue = varTemp > 1 ? Strings.Left(strTemp, varTemp - 1) : "";

                            strTemp = Strings.Right(strTemp, Strings.Len(strTemp) - varTemp);
                            varTemp = Strings.InStr(strTemp, "$");


                            if (varTemp > 0)
                            {
                                TxtBlankNumber.Text = varTemp > 1 ? Strings.Left(strTemp, varTemp - 1) : "";

                                strTemp = Strings.Right(strTemp, Strings.Len(strTemp) - varTemp);
                                varTemp = Strings.InStr(strTemp, "$");

                                if (varTemp > 0)
                                {
                                    if (varTemp > 1)
                                    {
                                        TxtBlankDescription.Text = Strings.Left(strTemp, varTemp - 1);
                                    }
                                    else
                                    {
                                        TxtBlankDescription.Text = "";
                                    }
                                    strTemp = Strings.Right(strTemp, Strings.Len(strTemp) - varTemp);
                                    varTemp = Strings.InStr(strTemp, "$");

                                    if (varTemp > 0)
                                    {
                                        if (varTemp > 1)
                                        {
                                            TxtBlankRemark.Text = Strings.Left(strTemp, varTemp - 1);
                                        }
                                        else
                                        {
                                            TxtBlankRemark.Text = "";
                                        }


                                        strTemp = Strings.Right(strTemp, Strings.Len(strTemp) - varTemp);
                                        varTemp = Strings.InStr(strTemp, "$");

                                        if (varTemp > 0)
                                        {
                                            if (varTemp > 1)
                                            {
                                                // было CboBlankSection.SelectedValue
                                                CboBlankSection.Text = Strings.Left(strTemp, varTemp - 1);
                                            }
                                            else
                                            {
                                                CboBlankSection.Text = "";
                                            }

                                            strTemp = Strings.Right(strTemp, Strings.Len(strTemp) - varTemp);
                                            varTemp = Strings.InStr(strTemp, "$");

                                            if (varTemp > 0)
                                            {
                                                if (varTemp > 1)
                                                {
                                                    CboBlankGroup.Text = Strings.Left(strTemp, varTemp - 1);
                                                }
                                                else
                                                {
                                                    CboBlankGroup.Text = "";
                                                }

                                                strTemp = Strings.Right(strTemp, Strings.Len(strTemp) - varTemp);
                                                varTemp = Strings.InStr(strTemp, "$");

                                                if (varTemp > 0)
                                                {
                                                    if (varTemp > 1)
                                                    {
                                                        //TxtBlankDocNumber.Text = Strings.Left(strTemp, varTemp - 1);
                                                    }
                                                    else
                                                    {
                                                        //TxtBlankDocNumber.Text = "";
                                                    }

                                                    strTemp = Strings.Right(strTemp, Strings.Len(strTemp) - varTemp);
                                                    varTemp = Strings.InStr(strTemp, "$");

                                                    if (varTemp > 0)
                                                    {
                                                        if (varTemp > 1)
                                                        {
                                                            //TxtRemarkVP.Text = Strings.Left(strTemp, varTemp - 1);
                                                        }
                                                        else
                                                        {
                                                            //TxtRemarkVP.Text = "";
                                                        }

                                                        strTemp = Strings.Right(strTemp, Strings.Len(strTemp) - varTemp);
                                                        varTemp = Strings.InStr(strTemp, "$");

                                                        if (varTemp > 0)
                                                        {
                                                            if (varTemp > 1)
                                                            {
                                                                //TxtVendorVP.Text = Strings.Left(strTemp, varTemp - 1);
                                                            }
                                                            else
                                                            {
                                                                //TxtVendorVP.Text = "";
                                                            }

                                                            //TxtCodeVP.Text = Right$(strTemp, Len(strTemp) - varTemp)
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        TxtBlankNumber.IsEnabled = false;
                        TxtBlankDescription.IsEnabled = false;
                        CboBlankFormat.IsEnabled = false;
                        TxtBlankRemark.IsEnabled = false;
                        CboBlankSection.IsEnabled = false;

                        //MaterialTxtBox.Text = "";
                        TxtBlankNumber.Text = "";
                        TxtBlankDescription.Text = "";
                        CboBlankFormat.Text = "";
                        TxtBlankRemark.Text = "";
                        CboBlankSection.Text = "";
                        //CboBlankGroup.Text = "";
                        //CboBlankSection_Change;
                    }


                    var a = default(RoutedEventArgs);
                    ChkAssem_Click(false, a);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region " ChkBox Умен. шрифт "
        // ChkBox Умен. шрифт
        private void ChkFont_Click(object sender, RoutedEventArgs e)
        {
            if (ChkFont.IsChecked == true)
            {
                DescriptionTxtBox.FontSize = 14;
            }
            else
            {
                DescriptionTxtBox.FontSize = 18;
            }
        }
        #endregion
        private void ChkAssem_Click(object sender, RoutedEventArgs e)
        {
            // Зависят ChkMaterialSW, Chk3
            if (MAssem == 0) // Изменения разрешены
            {
                MChkMaterialSW = 1; // Метка флажка "Материал SW": 0 - можно менять флажок; 1 - нельзя менять флажок
                MChk3 = 1; // Метка флажка "Сортамент": 0 - можно менять флажок; 1 - нельзя менять флажок

                if (ChkAssem.IsChecked == true & swModel.GetType() == (int) swDocumentTypes_e.swDocPART |
                    ChkAssem.IsChecked == false & swModel.GetType() == (int) swDocumentTypes_e.swDocASSEMBLY)
                    // Выбрана сборка гасим все
                {
                    //MaterialTxtBox.Text = "";
                    //MaterialTxtBox.IsEnabled = false;

                    TxtDens.IsEnabled = false;
                    CboHatch.IsEnabled = false;
                    MChkMaterialSW = 0;
                    MChk3 = 0;
                }
                else // Выбрана деталь
                {
                    if (swModel.GetType() == (int) swDocumentTypes_e.swDocPART) // Если открыта деталь
                    {
                        // Плотность и штриховка
                        TxtDens.Text =
                            Convert.ToString(
                                swModel.GetUserPreferenceDoubleValue(
                                    (int) swUserPreferenceDoubleValue_e.swMaterialPropertyDensity));
                        strTemp =
                            swModel.GetUserPreferenceStringValue(
                                (int) swUserPreferenceStringValue_e.swMaterialPropertyCrosshatchPattern);

                        m = 0;

                        for (int i = 0; i < CboHatch.Items.Count; i++)
                        {
                            if (CboHatch.Items[i] == strTemp)
                            {
                                m = 1;
                                break;
                            }
                        }

                        if (m == 0)
                        {
                            CboHatch.Items.Add(strTemp);
                        }

                        CboHatch.Text =
                            swModel.GetUserPreferenceStringValue(
                                (int) swUserPreferenceStringValue_e.swMaterialPropertyCrosshatchPattern);
                    }
                    else // Если открыта сборка
                    {
                        TxtDens.IsEnabled = false;
                        CboHatch.IsEnabled = false;
                    }

                    // Проверка: указывается ли материал в чертеже
                    // prpMaterial - Материал_ФБ
                    strTemp = swModel.CustomInfo2[sConfigName, "Материал_ФБ"]; // Считываем материал из модели

                    if (strTemp == "") // Если строчка пустая
                    {
                        strTemp = swModel.CustomInfo2["", "Материал_ФБ"]; // Считываем материал из модели
                    }
                }

                // Выставляем раздел
                if (ChkAssem.IsChecked == true)
                {
                    if (swModel.GetType() == (int) swDocumentTypes_e.swDocPART)
                    {
                        ComboBoxSection.Text = "Сборочные единицы";
                    }
                    else
                    {
                        ComboBoxSection.Text = "Детали";
                    }
                }
                else
                {
                    if (swModel.GetType() == (int) swDocumentTypes_e.swDocPART &
                        ComboBoxSection.Text == "Сборочные единицы")
                    {
                        ComboBoxSection.Text = "";
                    }
                    else if (swModel.GetType() == (int) swDocumentTypes_e.swDocASSEMBLY &
                             ComboBoxSection.Text == "Детали")
                    {
                        ComboBoxSection.Text = "";
                    }
                }
            }
        }
        private void ChkNoMass_Click(object sender, RoutedEventArgs e)
        {
            if (ChkNoMass.IsChecked == true)
            {
                ChkMassTable.IsChecked = false;
            }
        }
        private void ChkMassTable_Click(object sender, RoutedEventArgs e)
        {
            if (ChkMassTable.IsChecked == true)
            {
                ChkNoMass.IsChecked = false;
            }
        }
        private void ChkDefMass_Click(object sender, RoutedEventArgs e)
        {
            // Зависит mass
            if (ChkDefMass.IsChecked == false) //Масса по умолчанию
            {
                CboMass.IsEnabled = false;
                CboTol.IsEnabled = false;
            }
            else // Пользовательские настройки массы
            {
                CboMass.IsEnabled = true;
                CboTol.IsEnabled = true;
            }

            mass();
        }
        #endregion
        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            ApplyMProp();
        }
        private void ApplyAndClose_Click(object sender, RoutedEventArgs e)
        {
            MClose = 1;

            //applyMProp();

            Apply_Click(true, e);

            Close();

            Close_Click(true, e);
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (longwarnings != (int) swFileLoadWarning_e.swFileLoadWarning_AlreadyOpen & MDoc != 1)
                    // Если чертеж не был открыт и редактируется чертеж
                {
                    swApp.QuitDoc(_sDrawName); //  то закрываем его
                    swModel = swApp.ActivateDoc(Source4);
                }
                else
                {
                    ok = swDraw.ActivateSheet(strActiveSheetName); // Возвращаем активность листу
                }

                Close();

                if (mRun == 0)
                {
                    //System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ApplyMProp()
        {
            try
            {
                #region " Очистка свойств "

                // Очистка свойств
                if (MIni1 == 1)
                {
                    MDel = 1;
                    var e = default(RoutedEventArgs);
                    //DeleteAllProperties_Click(true, e);
                    DeleteProperties_Click(true, e);
                }

                #endregion
                #region " Назначение массовых характеристик "

                // Назначение массовых характеристик
                swModel.SetUserPreferenceIntegerValue((int) swUserPreferenceIntegerValue_e.swUnitSystem,
                    (int) swUnitSystem_e.swUnitSystem_Custom);
                switch (CboMass.SelectedIndex)
                {
                    case 0:
                        swModel.SetUserPreferenceIntegerValue(
                            (int) swUserPreferenceIntegerValue_e.swUnitsMassPropLength, (int) swLengthUnit_e.swMETER);
                        swModel.SetUserPreferenceIntegerValue((int) swUserPreferenceIntegerValue_e.swUnitsMassPropMass,
                            (int) swUnitsMassPropMass_e.swUnitsMassPropMass_Milligrams);
                        swModel.SetUserPreferenceIntegerValue(
                            (int) swUserPreferenceIntegerValue_e.swUnitsMassPropVolume,
                            (int) swUnitsMassPropVolume_e.swUnitsMassPropVolume_Meters3);
                        break;
                    case 1:

                        swModel.SetUserPreferenceIntegerValue(
                            (int) swUserPreferenceIntegerValue_e.swUnitsMassPropLength, (int) swLengthUnit_e.swMETER);
                        swModel.SetUserPreferenceIntegerValue((int) swUserPreferenceIntegerValue_e.swUnitsMassPropMass,
                            (int) swUnitsMassPropMass_e.swUnitsMassPropMass_Grams);
                        swModel.SetUserPreferenceIntegerValue(
                            (int) swUserPreferenceIntegerValue_e.swUnitsMassPropVolume,
                            (int) swUnitsMassPropVolume_e.swUnitsMassPropVolume_Meters3);
                        break;
                    case 2:

                        swModel.SetUserPreferenceIntegerValue(
                            (int) swUserPreferenceIntegerValue_e.swUnitsMassPropLength, (int) swLengthUnit_e.swMETER);
                        swModel.SetUserPreferenceIntegerValue((int) swUserPreferenceIntegerValue_e.swUnitsMassPropMass,
                            (int) swUnitsMassPropMass_e.swUnitsMassPropMass_Kilograms);
                        swModel.SetUserPreferenceIntegerValue(
                            (int) swUserPreferenceIntegerValue_e.swUnitsMassPropVolume,
                            (int) swUnitsMassPropVolume_e.swUnitsMassPropVolume_Meters3);
                        break;
                    case 3:

                        swModel.SetUserPreferenceIntegerValue(
                            (int) swUserPreferenceIntegerValue_e.swUnitsMassPropLength, (int) swLengthUnit_e.swFEET);
                        swModel.SetUserPreferenceIntegerValue((int) swUserPreferenceIntegerValue_e.swUnitsMassPropMass,
                            (int) swUnitsMassPropMass_e.swUnitsMassPropMass_Pounds);
                        swModel.SetUserPreferenceIntegerValue(
                            (int) swUserPreferenceIntegerValue_e.swUnitsMassPropVolume,
                            (int) swUnitsMassPropVolume_e.swUnitsMassPropVolume_Feet3);
                        break;
                }
                swModel.SetUserPreferenceIntegerValue(
                    (int) swUserPreferenceIntegerValue_e.swUnitsMassPropDecimalPlaces, CboMass.SelectedIndex);

                #endregion
                #region " Обозначение вводится вручную "

                // Обозначение вводится вручную
                if (ChkManual.IsChecked == true)
                {
                    sNumber = TxtNumber.Text;
                }
                else
                {
                    if (prpFileName == 0) // Не используем разделитель
                    {
                        sNumber = "$PRP:" + Strings.Chr(34) + "SW-File Name" + Strings.Chr(34);
                    }
                    else
                    {
                        varTemp = Strings.InStr(sNumberTitle, prpNameSep);
                        if (varTemp > 0) // Разделитель найден
                        {
                            sNumber = TxtNumber.Text;
                        }
                        else
                        {
                            sNumber = "$PRP:" + Strings.Chr(34) + "SW-File Name" + Strings.Chr(34);
                        }
                    }
                }

                sNumberReal = "$PRP:" + Strings.Chr(34) + "SW-File Name" + Strings.Chr(34);
                    // sNumberReal - для записи свойства масса, плотность, материал;

                ok = swModel.AddCustomInfo2("Number", 30, sNumber);
                swModel.CustomInfo2["", "Number"] = sNumber;

                if (TxtNumber.Text == "")
                {
                    swModel.SummaryInfo[4] = "";
                }
                else
                {
                    swModel.SummaryInfo[4] = TxtNumber.Text;
                }


                swModel.AddCustomInfo2("Обозначение", 30, "");
                swModel.CustomInfo2["", "Обозначение"] = sNumber;

                #endregion
                #region " Ипользуется исполнение "

                // Ипользуется исполнение
                if (Исполнение.IsChecked == true & Strings.LTrim(TxtConf.Text) != "")
                {
                    ok = swModel.AddCustomInfo3(sConfigName, "Обозначение", 30, "");
                    swModel.CustomInfo2[sConfigName, "Обозначение"] = sNumber + "-" + Strings.LTrim(TxtConf.Text);
                }
                else
                {
                    ok = swModel.AddCustomInfo3(sConfigName, "Обозначение", 30, "");
                    swModel.CustomInfo2[sConfigName, "Обозначение"] = sNumber;
                }

                // ReName (Ввести Обозначение, служебное свойство)
                ok = swModel.AddCustomInfo3("", "RenameSWP", 30, "");
                if (ChkManual.IsChecked == true)
                {
                    swModel.CustomInfo2["", "RenameSWP"] = "1";
                }
                else
                {
                    swModel.CustomInfo2["", "RenameSWP"] = "0";
                }

                // Исполнение
                ok = swModel.AddCustomInfo3(sConfigName, "Исполнение", 30, "");

                if (Исполнение.IsChecked == true)
                {
                    swModel.CustomInfo2[sConfigName, "Исполнение"] = "1";
                }
                else
                {
                    swModel.CustomInfo2[sConfigName, "Исполнение"] = "0";
                }

                #endregion

                // Наименование, Наименование_ФБ и Description
                // Преобразование Наименования к однострочному виду
                if (Strings.InStr(DescriptionTxtBox.Text, Strings.Chr(13).ToString()) > 0) // Проверка количества строк
                {
                    strDescription = Strings.Replace(DescriptionTxtBox.Text,
                        Strings.Chr(13) + Strings.Chr(10).ToString(), " ");
                }
                else
                {
                    strDescription = DescriptionTxtBox.Text;
                }

                #region " Прописывание свойств "

                // Прописывание свойств
                if (strDescription == "")
                {
                    swModel.SummaryInfo[0] = "";
                }
                else
                {
                    swModel.SummaryInfo[0] = strDescription;
                }

                ok = swModel.AddCustomInfo2("Наименование", 30, "");

                swModel.AddCustomInfo2("Наименование", 30, "");
                swModel.CustomInfo2["", "Наименование"] = strDescription;

                swModel.AddCustomInfo2("Description", 30, strDescription);
                swModel.CustomInfo2["", "Description"] = strDescription;

                swModel.AddCustomInfo2("DescriptionEng", 30, TxtEngDescription.Text);
                swModel.CustomInfo2["", "DescriptionEng"] = TxtEngDescription.Text;

                if (prpFontSize == 1) // Проверка количества строк
                {
                    varTemp = Strings.InStr(DescriptionTxtBox.Text, Strings.Chr(10).ToString());

                    if (varTemp > 0)
                    {
                        if (Strings.InStr(varTemp + 1, DescriptionTxtBox.Text, Strings.Chr(10).ToString()) > 0)
                        {
                            strTemp = DescriptionTxtBox.Text;
                        }
                        else // Две строки
                        {
                            if (ChkFont.IsChecked == true)
                            {
                                strTemp = "<FONT size=2> " + Strings.Chr(13) + Strings.Chr(10) + "<FONT size=3.5>" +
                                          DescriptionTxtBox.Text;
                            }
                            else
                            {
                                strTemp = "<FONT size=2> " + Strings.Chr(13) + Strings.Chr(10) + "<FONT size=5>" +
                                          DescriptionTxtBox.Text;
                            }
                        }
                    }
                    else // Одна строка
                    {
                        if (ChkFont.IsChecked == true)
                        {
                            strTemp = "<FONT size=4> " + Strings.Chr(10) + "<FONT size=3.5>" + DescriptionTxtBox.Text;
                        }
                        else
                        {
                            strTemp = "<FONT size=4> " + Strings.Chr(10) + "<FONT size=5>" + DescriptionTxtBox.Text;
                        }
                    }
                }
                else
                {
                    strTemp = DescriptionTxtBox.Text;
                }

                swModel.AddCustomInfo2("Наименование_ФБ", 30, "");
                swModel.CustomInfo2["", "Наименование_ФБ"] = strTemp;

                #endregion

                //// Используется исполнение
                //if (Исполнение.IsChecked == true & Strings.LTrim(TxtConf.Text) != "")
                //{
                //    swModel.AddCustomInfo3(sConfigName, "Наименование", 30, "");
                //    swModel.AddCustomInfo3(sConfigName, "Наименование_ФБ", 30, "");
                //}
                //else
                //{
                //    swModel.DeleteCustomInfo2(sConfigName, "Наименование");
                //    swModel.DeleteCustomInfo2(sConfigName, "Наименование_ФБ");
                //}

                // Обновляем постоянные части Обозначения и Наименования для всех конфигураций
                for (var i = 0; i < ConfNumb - 1; i++)
                {
                    if (swModel.CustomInfo2[vConfNameArr[i], "Исполнение"] == "1" |
                        swModel.CustomInfo2[vConfNameArr[i], "Исполнение"] == "2")
                    {
                        strTemp1 = swModel.CustomInfo2[vConfNameArr[i], "Обозначение"];

                        varTemp = Strings.InStrRev(strTemp1, "-");
                        if (varTemp > 0)
                        {
                            strTemp1 = Strings.Right(strTemp1, Strings.Len(strTemp1) - varTemp);
                        }
                        else
                        {
                            strTemp1 = "";
                        }

                        swModel.AddCustomInfo3(vConfNameArr[i], "Обозначение", 30, "");
                        swModel.CustomInfo2[vConfNameArr[i], "Обозначение"] = sNumber + "-" + strTemp1;
                        //swModel.AddCustomInfo3(vConfNameArr[i], "Наименование", 30, "");


                        //swModel.AddCustomInfo3(vConfNameArr[i], "Наименование_ФБ", 30, "");
                        //swModel.CustomInfo2[vConfNameArr[i], "Наименование_ФБ"] = strTemp + " " + swModel.CustomInfo2[vConfNameArr[i], "Код_ФБ"];
                    }
                    else
                    {
                        ok = swModel.AddCustomInfo3(vConfNameArr[i], "Обозначение", 30, "");
                        swModel.CustomInfo2[vConfNameArr[i], "Обозначение"] = sNumber;
                        //ok = swModel.DeleteCustomInfo2(vConfNameArr[i], "Наименование");
                        //ok = swModel.DeleteCustomInfo2(vConfNameArr[i], "Наименование_ФБ");
                    }
                }

                #region " Раздел "

                // Раздел
                swModel.AddCustomInfo3(sConfigName, "Раздел", 30, "");
                swModel.CustomInfo2[sConfigName, "Раздел"] = ComboBoxSection.Text;

                swModel.AddCustomInfo3("", "Сборка", 30, "");
                if (swModel.GetType() == (int) swDocumentTypes_e.swDocPART)
                {
                    if (ChkAssem.IsChecked == true)
                    {
                        swModel.CustomInfo2["", "Сборка"] = "True";
                    }
                    else
                    {
                        swModel.CustomInfo2["", "Сборка"] = "False";
                    }
                }
                else
                {
                    if (ChkAssem.IsChecked == true)
                    {
                        swModel.CustomInfo2["", "Сборка"] = "False";
                    }
                    else
                    {
                        swModel.CustomInfo2["", "Сборка"] = "True";
                    }
                }

                #endregion

                // Группа
                if (ComboBoxSection.Text == "Стандартные изделия" |
                    ComboBoxSection.Text == "Прочие изделия" |
                    ComboBoxSection.Text == "Материалы")
                {
                    ok = swModel.AddCustomInfo3("", "Группа", 30, "");
                    //swModel.CustomInfo2["", "Группа"] = CboGroup.Text;
                }
                else
                {
                    ok = swModel.DeleteCustomInfo2("", "Группа");
                }


                ok = swModel.AddCustomInfo3(sConfigName, "Формат_ФБ", 30, "");

                if (ChkFormat.IsChecked == true)
                {
                    swModel.CustomInfo2[sConfigName, "Формат_ФБ"] = "False";
                }
                else
                {
                    swModel.CustomInfo2[sConfigName, "Формат_ФБ"] = "True";
                }


                swModel.AddCustomInfo3(sConfigName, "Единицы", 30, "");
                if (ChkDefMass.IsChecked == true)
                {
                    swModel.CustomInfo2[sConfigName, "Единицы"] = "False";
                }
                else
                {
                    swModel.CustomInfo2[sConfigName, "Единицы"] = "True";
                }

                #region " Литера "

                // Литера
                swModel.AddCustomInfo3(sConfigName, "Литера_ФБ", 30, "");
                swModel.AddCustomInfo3(sConfigName, "Литера_Таблица", 30, "");
                if (prpFontSize == 1)
                {
                    switch (CboLit.Text)
                    {
                        case "O1":
                            strTemp = "O<FONT size=1.8>1";
                            break;
                        case "O2":
                            strTemp = "O<FONT size=1.8>2";
                            break;
                        default:
                            strTemp = CboLit.Text;
                            break;
                    }
                }
                else
                {
                    strTemp = CboLit.Text;
                }

                swModel.CustomInfo2[sConfigName, "Литера_Таблица"] = strTemp;

                if (ChkLitTable.IsChecked == true) // Литера в таблице
                {
                    if (prpFontSize == 1)
                    {
                        swModel.CustomInfo2[sConfigName, "Литера_ФБ"] = "<FONT size=2.5>" + "Див." + Strings.Chr(10) +
                                                                        "таблицю";
                    }
                    else
                    {
                        swModel.CustomInfo2[sConfigName, "Литера_ФБ"] = "Див.таблицю";
                    }
                }
                else
                {
                    if (prpFontSize == 1)
                    {
                        swModel.CustomInfo2[sConfigName, "Литера_ФБ"] = "<FONT size=1> " + Strings.Chr(10) +
                                                                        "<FONT size=3.5>" + strTemp;
                    }
                    else
                    {
                        swModel.CustomInfo2[sConfigName, "Литера_ФБ"] = strTemp;
                    }
                }

                if (swModel.CustomInfo2[sConfigName, "Литера_SP"] != "-")
                {
                    swModel.CustomInfo2[sConfigName, "Литера_SP"] = swModel.CustomInfo2[sConfigName, "Литера_Таблица"];
                }

                #endregion

                //ok = swModel.AddCustomInfo3(sConfigName, "Контора", 30, CboFirm.Text);
                //swModel.CustomInfo2[sConfigName, "Контора"] = CboFirm.Text;

                #region " Плотность и масса "

                // Плотность и масса
                if (swModel.GetType() == (int) swDocumentTypes_e.swDocPART) // Деталь
                {
                    swModel.DeleteCustomInfo2("", "Плотность_ФБ");
                    swModel.AddCustomInfo3(sConfigName, "Плотность_ФБ", 30,
                        Strings.Chr(34) + "SW-Density@@" + sConfigName + "@" + sNumberReal + ".SLDPRT" + Strings.Chr(34));
                    swModel.CustomInfo2[sConfigName, "Плотность_ФБ"] = Strings.Chr(34) + "SW-Density@@" + sConfigName +
                                                                       "@" + sNumberReal + ".SLDPRT" + Strings.Chr(34);

                    swModel.AddCustomInfo3(sConfigName, "Масса_ФБ", 30,
                        Strings.Chr(34) + "SW-Mass@@" + sConfigName + "@" + sNumberReal + ".SLDPRT" + Strings.Chr(34));
                    swModel.AddCustomInfo3(sConfigName, "Weight", 30,
                        Strings.Chr(34) + "SW-Mass@@" + sConfigName + "@" + sNumberReal + ".SLDPRT" + Strings.Chr(34));

                    swModel.AddCustomInfo3(sConfigName, "Масса_Таблица", 30,
                        Strings.Chr(34) + "SW-Mass@@" + sConfigName + "@" + sNumberReal + ".SLDPRT" + Strings.Chr(34));

                    swModel.DeleteCustomInfo2("", "Масса_ФБ");

                    if (ChkNoMass.IsChecked == true) // Без массы
                    {
                        if (prpFontSize == 1)
                        {
                            swModel.CustomInfo2[sConfigName, "Масса_ФБ"] = "<FONT size=1> " + Strings.Chr(10) +
                                                                           "<FONT size=3.5>" + "-";
                        }
                        else
                        {
                            swModel.CustomInfo2[sConfigName, "Масса_ФБ"] = "-";
                        }
                    }
                    else if (ChkMassTable.IsChecked == true) // Масса в таблице
                    {
                        if (prpFontSize == 1)
                        {
                            swModel.CustomInfo2[sConfigName, "Масса_ФБ"] = "<FONT size=2.5>" + "Див." + Strings.Chr(10) +
                                                                           "таблицю";
                        }
                        else
                        {
                            swModel.CustomInfo2[sConfigName, "Масса_ФБ"] = "Див.таблицю";
                        }
                    }
                    else // С массой
                    {
                        if (CboMass.SelectedIndex == 1)
                        {
                            if (prpFontSize == 1)
                            {
                                swModel.CustomInfo2[sConfigName, "Масса_ФБ"] = "<FONT size=1> " + Strings.Chr(10) +
                                                                               "<FONT size=3.5>" + Strings.Chr(34) +
                                                                               "SW-Mass@@" + sConfigName + "@" +
                                                                               sNumberReal + ".SLDPRT" + Strings.Chr(34) +
                                                                               " г";
                            }
                            else
                            {
                                swModel.CustomInfo2[sConfigName, "Масса_ФБ"] = Strings.Chr(34) + "SW-Mass@@" +
                                                                               sConfigName +
                                                                               "@" + sNumberReal + ".SLDPRT" +
                                                                               Strings.Chr(34) + " г";
                            }
                        }
                        else
                        {
                            if (prpFontSize == 1)
                            {
                                swModel.CustomInfo2[sConfigName, "Масса_ФБ"] = "<FONT size=1> " + Strings.Chr(10) +
                                                                               "<FONT size=3.5>" + Strings.Chr(34) +
                                                                               "SW-Mass@@" + sConfigName + "@" +
                                                                               sNumberReal + ".SLDPRT" + Strings.Chr(34);
                            }
                            else
                            {
                                swModel.CustomInfo2[sConfigName, "Масса_ФБ"] = Strings.Chr(34) + "SW-Mass@@" +
                                                                               sConfigName +
                                                                               "@" + sNumberReal + ".SLDPRT" +
                                                                               Strings.Chr(34);
                            }
                        }
                    }

                    swModel.CustomInfo2[sConfigName, "Масса_Таблица"] = Strings.Chr(34) + "SW-Mass@@" + sConfigName +
                                                                        "@" + sNumberReal + ".SLDPRT" + Strings.Chr(34);
                }
                else // Сборка
                {
                    swModel.AddCustomInfo3(sConfigName, "Масса_ФБ", 30,
                        Strings.Chr(34) + "SW-Mass@@" + sConfigName + "@" + sNumberReal + ".SLDASM" + Strings.Chr(34));
                    swModel.AddCustomInfo3(sConfigName, "Weight", 30,
                        Strings.Chr(34) + "SW-Mass@@" + sConfigName + "@" + sNumberReal + ".SLDASM" + Strings.Chr(34));


                    swModel.AddCustomInfo3(sConfigName, "Масса_Таблица", 30,
                        Strings.Chr(34) + "SW-Mass@@" + sConfigName + "@" + sNumberReal + ".SLDASM" + Strings.Chr(34));
                    swModel.DeleteCustomInfo2("", "Масса_ФБ");

                    if (ChkNoMass.IsChecked == true) // Без массы
                    {
                        if (prpFontSize == 1)
                        {
                            swModel.CustomInfo2[sConfigName, "Масса_ФБ"] = "<FONT size=1> " + Strings.Chr(10) +
                                                                           "<FONT size=3.5>" + "-";
                        }
                        else
                        {
                            swModel.CustomInfo2[sConfigName, "Масса_ФБ"] = "-";
                        }
                    }
                    else if (ChkMassTable.IsChecked == true) // Масса в таблице
                    {
                        if (prpFontSize == 1)
                        {
                            swModel.CustomInfo2[sConfigName, "Масса_ФБ"] = "<FONT size=2.5>" + "Див." + Strings.Chr(10) +
                                                                           "таблицю";
                        }
                        else
                        {
                            swModel.CustomInfo2[sConfigName, "Масса_ФБ"] = "Див.таблицю";
                        }
                    }
                    else // С массой
                    {
                        if (CboMass.SelectedIndex == 1)
                        {
                            if (prpFontSize == 1)
                            {
                                swModel.CustomInfo2[sConfigName, "Масса_ФБ"] = "<FONT size=1> " + Strings.Chr(10) +
                                                                               "<FONT size=3.5>" + Strings.Chr(34) +
                                                                               "SW-Mass@@" + sConfigName + "@" +
                                                                               sNumberReal + ".SLDASM" + Strings.Chr(34) +
                                                                               " г";
                            }
                            else
                            {
                                swModel.CustomInfo2[sConfigName, "Масса_ФБ"] = Strings.Chr(34) + "SW-Mass@@" +
                                                                               sConfigName +
                                                                               "@" + sNumberReal + ".SLDASM" +
                                                                               Strings.Chr(34) + " г";
                            }
                        }
                        else
                        {
                            if (prpFontSize == 1)
                            {
                                swModel.CustomInfo2[sConfigName, "Масса_ФБ"] = "<FONT size=1> " + Strings.Chr(10) +
                                                                               "<FONT size=3.5>" + Strings.Chr(34) +
                                                                               "SW-Mass@@" + sConfigName + "@" +
                                                                               sNumberReal + ".SLDASM" + Strings.Chr(34);
                            }
                            else
                            {
                                swModel.CustomInfo2[sConfigName, "Масса_ФБ"] = Strings.Chr(34) + "SW-Mass@@" +
                                                                               sConfigName +
                                                                               "@" + sNumberReal + ".SLDASM" +
                                                                               Strings.Chr(34);
                            }
                        }
                    }
                    swModel.CustomInfo2[sConfigName, "Масса_Таблица"] = Strings.Chr(34) + "SW-Mass@@" + sConfigName +
                                                                        "@" + sNumberReal + ".SLDASM" + Strings.Chr(34);
                }

                #endregion

                // Плотность и штриховка
                if (TxtDens.IsEnabled)
                {
                    ok =
                        swModel.SetUserPreferenceStringValue(
                            (int) swUserPreferenceStringValue_e.swMaterialPropertyCrosshatchPattern, CboHatch.Text);
                }

                #region " Заготовка и свет "

                // Заготовка и материал
                swModel.AddCustomInfo3(sConfigName, "Заготовка", 30, "");

                if (ChkBlank.IsChecked == true)
                {
                    strTemp = CboBlankFormat.Text + "$" + TxtBlankNumber.Text + "$" + TxtBlankDescription.Text + "$" +
                              TxtBlankRemark.Text + "$" + CboBlankSection.Text + "$" + CboBlankGroup.Text;
                    swModel.CustomInfo2[sConfigName, "Заготовка"] = strTemp;

                    #region " Материал "

                    // Материал
                    swModel.DeleteCustomInfo2("", "Материал_ФБ");
                    swModel.AddCustomInfo3(sConfigName, "Материал_Таблица", 30, "");
                    swModel.AddCustomInfo3(sConfigName, "Материал_ФБ", 30, "");

                    //if (Strings.InStr(MaterialTxtBox.Text, Strings.Chr(10).ToString()) > 0) // Проверка количества строк
                    if (Strings.InStr(MaterialTxt, Strings.Chr(10).ToString()) > 0) // Проверка количества строк

                    {
                        swModel.CustomInfo2[sConfigName, "Материал_ФБ"] = "<FONT size=5.0> " + "<FONT size=3.5>" +
                                                                          MaterialTxt;
                    }
                    else
                    {
                        swModel.CustomInfo2[sConfigName, "Материал_ФБ"] = "<FONT size=1.8> " + Strings.Chr(10) +
                                                                          "<FONT size=3.5>" + MaterialTxt;
                    }

                    swModel.CustomInfo2[sConfigName, "Материал_Таблица"] = MaterialTxt;

                    #endregion
                }
                else
                {
                    swModel.CustomInfo2[sConfigName, "Заготовка"] = "";
                }

                //Свет
                // Удаляем источники света
                swModExt = swModel.Extension;
                swModExt.DeleteScene();

                var longTemp = swModel.GetLightSourceCount();

                for (var i = longTemp - 1; i >= 0; i += -1)
                {
                    swModel.DeleteLightSource(i);
                }

                strTemp = swModel.GetLightSourceName(0);

                // Добавляем источники света
                // Light type:
                // 1 = Ambient light
                // 2 = Spot light
                // 3 = Point light
                // 4 = Directional light

                swModel.AddLightSource("LD1", 4, "Направленный 1");
                swModel.AddLightSource("LD2", 4, "Направленный 2");

                // Выставляем параметры источников
                swModel.SetAmbientLightProperties(strTemp, 0.3, 0.0, 0.0, 16777215, true, true);

                var doubleTemp = Math.Tan(80*3.1416/180);
                var doubleTemp1 = Math.Sqrt(Math.Pow(doubleTemp, 2) + 1)*Math.Tan(50*3.1416/180);

                swModel.SetDirectionLightProperties("LD1", 0.5, 0.1, 0.2, 16777215, true, true, doubleTemp, doubleTemp1,
                    1);
                doubleTemp = Math.Tan(-40*3.1416/180);
                doubleTemp1 = Math.Sqrt(Math.Pow(doubleTemp, 2) + 1)*Math.Tan(20*3.1416/180);
                swModel.SetDirectionLightProperties("LD2", 0.0, 0.1, 0.2, 16777215, true, true, doubleTemp, doubleTemp1,
                    1);

                #endregion

                // Примечание и формат
                ok = swModel.DeleteCustomInfo2("", "Примечание");
                ok = swModel.DeleteCustomInfo2("", "Формат");

                ok = swModel.DeleteCustomInfo2(sConfigName, "Примечание");
                ok = swModel.DeleteCustomInfo2(sConfigName, "Формат");

                strTemp = note.Text; // БЧ
                if (note.Text == "Масса")
                {
                    if (swModel.GetType() == (int) swDocumentTypes_e.swDocPART) // Деталь
                    {
                        strTemp = Strings.Chr(34) + "SW-Mass@@" + sConfigName + "@" + sNumberReal + ".SLDPRT" +
                                  Strings.Chr(34) + " " + LblMass.Content;
                    }
                    else
                    {
                        strTemp = Strings.Chr(34) + "SW-Mass@@" + sConfigName + "@" + sNumberReal + ".SLDASM" +
                                  Strings.Chr(34) + " " + LblMass.Content;
                    }
                }

                if (ConfNumb > 1)
                {
                    strTemp = note.Text;
                    ok = swModel.AddCustomInfo3(sConfigName, "Примечание", 30, strTemp);
                    swModel.CustomInfo2[sConfigName, "Примечание"] = strTemp;
                    ok = swModel.AddCustomInfo3(sConfigName, "Формат", 30, CboFormat.Text);
                    swModel.CustomInfo2[sConfigName, "Формат"] = CboFormat.Text;
                }
                else
                {
                    ok = swModel.AddCustomInfo3("", "Примечание", 30, strTemp);
                    swModel.CustomInfo2["", "Примечание"] = strTemp;
                    ok = swModel.AddCustomInfo3("", "Формат", 30, CboFormat.Text);
                    swModel.CustomInfo2["", "Формат"] = CboFormat.Text;

                    ok = swModel.AddCustomInfo3(sConfigName, "Примечание", 30, strTemp);
                    swModel.CustomInfo2[sConfigName, "Примечание"] = strTemp;
                    ok = swModel.AddCustomInfo3(sConfigName, "Формат", 30, CboFormat.Text);
                    swModel.CustomInfo2[sConfigName, "Формат"] = CboFormat.Text;
                }

                // Добавления свойств в сборку
                ok = swModel.DeleteCustomInfo2("", "Сборка1_ФБ");
                ok = swModel.AddCustomInfo3(sConfigName, "Сборка1_ФБ", 30, CboDrawingDoc.Text);
                swModel.CustomInfo2[sConfigName, "Сборка1_ФБ"] = CboDrawingDoc.Text;

                ok = swModel.DeleteCustomInfo2("", "Сборка2_ФБ");
                ok = swModel.AddCustomInfo3(sConfigName, "Сборка2_ФБ", 30, "");

                //if (Strings.InStr(TxtAssem2.Text, Strings.Chr(10).ToString()) > 0) // Проверка количества строк
                //{
                //    swModel.CustomInfo2[sConfigName, "Сборка2_ФБ"] = TxtAssem2.Text;
                //}
                //else
                //{
                //    if (prpFontSize == 1)
                //    {
                //        swModel.CustomInfo2[sConfigName, "Сборка2_ФБ"] = "<FONT size=1> " + Strings.Chr(10) + "<FONT size=2.5>" + TxtAssem2.Text;
                //    }
                //    else
                //    {
                //        swModel.CustomInfo2[sConfigName, "Сборка2_ФБ"] = TxtAssem2.Text;
                //    }
                //}


                // Нужно заносить во все конфигурации
                n = 1;

                try
                {
                    _profilForm.GridMPropUc_Loaded(true, null);

                    //if (MPropUcProfil.CboIndex == 0)
                    //{
                    // Разработал
                    if (MPropUcProfil.NodeDrawnByName == "")
                    {
                        swModel.SummaryInfo[2] = "";
                    }
                    else
                    {
                        swModel.SummaryInfo[2] = MPropUcProfil.NodeDrawnByName;
                    }

                    swModel.AddCustomInfo2("Конструктор", 30, MPropUcProfil.NodeDrawnByName);
                    swModel.CustomInfo2["", "Конструктор"] = MPropUcProfil.NodeDrawnByName;

                    swModel.AddCustomInfo3(sConfigName, "Проверил", 30, MPropUcProfil.NodeChkdName);
                    swModel.DeleteCustomInfo2("", "Проверил");
                    swModel.CustomInfo2[sConfigName, "Проверил"] = MPropUcProfil.NodeChkdName;

                    swModel.AddCustomInfo3(sConfigName, "Техконтроль", 30, MPropUcProfil.NodeTSupervName);
                    swModel.DeleteCustomInfo2("", "Техконтроль");
                    swModel.CustomInfo2[sConfigName, "Техконтроль"] = MPropUcProfil.NodeTSupervName;

                    swModel.AddCustomInfo3(sConfigName, "Утвердил", 30, MPropUcProfil.NodeExaminedByName);
                    swModel.DeleteCustomInfo2("", "Утвердил");
                    swModel.CustomInfo2[sConfigName, "Утвердил"] = MPropUcProfil.NodeExaminedByName;
                    //}

                    ////Eng
                    //if (MPropUcProfil.CboIndex == 1)
                    //{
                    // Разработал
                    if (MPropUcProfil.NodeDrawnByNameEng == "")
                    {
                        swModel.SummaryInfo[2] = "";
                    }
                    else
                    {
                        swModel.SummaryInfo[2] = MPropUcProfil.NodeDrawnByNameEng;
                    }

                    swModel.AddCustomInfo2("DrawnBy", 30, MPropUcProfil.NodeDrawnByNameEng);
                    swModel.CustomInfo2["", "DrawnBy"] = MPropUcProfil.NodeDrawnByNameEng;

                    swModel.AddCustomInfo3(sConfigName, "CheckedBy", 30, MPropUcProfil.NodeChkdNameEng);
                    swModel.DeleteCustomInfo2("", "CheckedBy");
                    swModel.CustomInfo2[sConfigName, "CheckedBy"] = MPropUcProfil.NodeChkdNameEng;

                    //const int swCustomInfoText = 30;
                    //ok = swModel.AddCustomInfo3(sConfigName, "TSuperv", swCustomInfoText, _profilForm.TxtBoxTKontrol.Text);
                    //ok = swModel.DeleteCustomInfo2("", "TSuperv");
                    //swModel.CustomInfo2[sConfigName, "TSuperv"] = _profilForm.TxtBoxTKontrol.Text;

                    swModel.AddCustomInfo3(sConfigName, "EngineeringApproval", 30, MPropUcProfil.NodeExaminedByNameEng);
                    swModel.DeleteCustomInfo2("", "EngineeringApproval");
                    swModel.CustomInfo2[sConfigName, "EngineeringApproval"] = MPropUcProfil.NodeExaminedByNameEng;
                    //}
                }
                catch (Exception)
                {
                    MessageBox.Show("Необходимо заполнить профиль пользователя!");
                }

                // Запись свойств в чертеж
                //MDrw = 1;
                if (MDrw == 1)
                {
                    swModel = (ModelDoc2) swDraw;

                    if (strDescription == "")
                    {
                        swModel.SummaryInfo[0] = "";
                    }
                    else
                    {
                        swModel.SummaryInfo[0] = strDescription;
                    }

                    //var sNumber = swmodel.CustomInfo2["", "Number"];
                    swModel.AddCustomInfo2("Description", 30, strDescription);
                    swModel.CustomInfo2["", "Description"] = strDescription;

                    swModel.AddCustomInfo2("DescriptionEng", 30, TxtEngDescription.Text);
                    swModel.CustomInfo2["", "DescriptionEng"] = TxtEngDescription.Text;

                    swModel.AddCustomInfo2("Number", 30, sNumber);
                    swModel.CustomInfo2["", "Number"] = sNumber;

                    swModel.ForceRebuild3(false);
                }

                if (MClose != 1)
                {
                    MStart = 1;
                    Test();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //private void DeleteAllProperties_Click(object sender, RoutedEventArgs e) // Удаление всех свойств из файла
        //{
        //    try
        //    {
        //        for (var i = 0; i <= vConfNameArr.GetUpperBound(0); i++)
        //        {
        //            vCustInfoNameArr = swModel.GetCustomInfoNames2(vConfNameArr[i]);

        //            for (var j = 0; j <= vCustInfoNameArr.GetUpperBound(0); j++)
        //            {
        //                ok = swModel.DeleteCustomInfo2(vConfNameArr[i], vCustInfoNameArr[j]);
        //            }
        //        }

        //        // Удаление свойств из чертежа
        //        vCustInfoNameArr = swModel.GetCustomInfoNames2("");

        //        for (var j = 0; j <= vCustInfoNameArr.GetUpperBound(0); j++)
        //        {
        //            ok = swModel.DeleteCustomInfo2("", vCustInfoNameArr[j]);
        //        }

        //        const int swSumInfoTitle = 0;
        //        const int swSumInfoSubject = 1;
        //        const int swSumInfoAuthor = 2;
        //        const int swSumInfoKeywords = 3;
        //        const int swSumInfoComment = 4;

        //        swModel.SummaryInfo[swSumInfoTitle] = "";
        //        swModel.SummaryInfo[swSumInfoSubject] = "";
        //        swModel.SummaryInfo[swSumInfoAuthor] = "";
        //        swModel.SummaryInfo[swSumInfoKeywords] = "";
        //        swModel.SummaryInfo[swSumInfoComment] = "";

        //        if (MDel != 1)
        //        {
        //            MStart = 1;

        //            main();
        //        }

        //        MaterialTxt = "";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        private string _sDrawName;
        private void CboConfig_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                MConf = 0;

                if (MConf == 0) // Если пользователь изменил конфигурацию
                {
                    if (longwarnings != (int) swFileLoadWarning_e.swFileLoadWarning_AlreadyOpen)
                        // Если чертеж не был открыт
                    {
                        swApp.QuitDoc(_sDrawName); // То закрываем его
                        swModel = swApp.ActivateDoc(Source4);
                    }
                    else
                    {
                        swDraw.ActivateSheet(strActiveSheetName); // Возвращаем активность листу      
                    }

                    sConfigName = Strings.LTrim(Convert.ToString(CboConfig.SelectedValue));


                    MStart = 1;
                    Test();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void TxtBlankNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ChkAssem.IsChecked == true & swModel.GetType() == (int) swDocumentTypes_e.swDocASSEMBLY |
                ChkAssem.IsChecked == false & swModel.GetType() == (int) swDocumentTypes_e.swDocPART) // Выбрана деталь
            {
                if (TxtBlankNumber.Text != "")
                {
                    MaterialTxt = "Изделие-заготовка:" + Convert.ToChar(10) + TxtBlankNumber.Text;
                }
                else
                {
                    MaterialTxt = "Изделие-заготовка:" + Convert.ToChar(10) + TxtBlankDescription.Text;
                }
            }
        }
        private void TxtBlankDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ChkAssem.IsChecked == true & swModel.GetType() == (int) swDocumentTypes_e.swDocASSEMBLY |
                ChkAssem.IsChecked == false & swModel.GetType() == (int) swDocumentTypes_e.swDocPART) // Выбрана деталь
            {
                if (TxtBlankNumber.Text == "")
                {
                    MaterialTxt = "Изделие-заготовка:" + Convert.ToChar(10) + TxtBlankDescription.Text;
                }
            }
        }
        private void ComboBoxSection_DropDownClosed(object sender, EventArgs e)
        {
            if (MSection == 0)
            {
                if (ComboBoxSection.Text == "Стандартные изделия" | ComboBoxSection.Text == "Прочие изделия")
                {
                    //TODO: ComboBoxSection_SelectionChanged

                    //CboGroup.IsEnabled = true;
                    //// Чтение файла и заполнение списка CboGroup
                    //CboGroup.Items.Clear();

                    //string[] grouparray = {"Подшипники", 
                    //      "Крепежные изделия",
                    //      "Пневматическое оборудование",
                    //      "Электротехнические изделия", 
                    //      "Фотоприемные устройства",
                    //        "Разное"};

                    //foreach (var VARIABLE in grouparray)
                    //{
                    //    CboGroup.Items.Add(VARIABLE);
                    //}

                    strTemp = swModel.CustomInfo2["", "Группа"];

                    //for (var i = 0; i <= CboGroup.Items.Count - 1; i++)
                    //{
                    //    if (strTemp == CboGroup.Items[i])
                    //    {
                    //        CboGroup.Text = strTemp;
                    //    }
                    //}
                }
                else if (ComboBoxSection.Text == "Материалы")
                {
                    //CboGroup.IsEnabled = true;
                    // Чтение файла и заполнение списка CboGroup
                    //CboGroup.Items.Clear();

                    string[] grouparray =
                    {
                        "Металлы черные",
                        "Металлы магнитоэлектрические и ферромагнитные",
                        "Металлы цветны, благородные и редкие",
                        "Кабели, провода и шнуры"
                    };

                    strTemp = swModel.CustomInfo2["", "Группа"];

                    //for (var i = 0; i <= CboGroup.Items.Count - 1; i++)
                    //{
                    //    if (strTemp == CboGroup.Items[i])
                    //    {
                    //        CboGroup.Text = strTemp;
                    //    }
                    //}
                }
                else
                {
                    //CboGroup.IsEnabled = false;
                    //CboGroup.Items.Clear();
                }
            }
        }
        public void MyProperties()
        {
            strTemp = "1";

            prpTestFormat = strTemp == "1" ? 1 : 0;

            prpTestName = strTemp == "1" ? 1 : 0;

            // Проверка оформления
            prpTestStandard = strTemp == "1" ? 1 : 0;

            // Управление шрифтом
            prpFontSize = strTemp == "1" ? 1 : 0;
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
            stdFontItalic = strTemp == "1" ? 1 : 0;

            strTemp = "0"; // Жирный
            stdFontBold = strTemp == "1" ? 1 : 0;

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
        private void EditProp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var editPropForm = new EditPropeprty {_swModel = swModel};
                editPropForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Profil_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _profilForm = new MPropUcProfil();
                _profilForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DeleteProperties_Click(object sender, RoutedEventArgs e)
        {
            DeleteAllProperties();
        }
        public void DeleteAllProperties()
        {
            try
            {
                for (var i = 0; i <= vConfNameArr.GetUpperBound(0); i++)
                {
                    vCustInfoNameArr = swModel.GetCustomInfoNames2(vConfNameArr[i]);

                    for (var j = 0; j <= vCustInfoNameArr.GetUpperBound(0); j++)
                    {
                        ok = swModel.DeleteCustomInfo2(vConfNameArr[i], vCustInfoNameArr[j]);
                    }
                }

                // Удаление свойств из чертежа
                vCustInfoNameArr = swModel.GetCustomInfoNames2("");

                for (var j = 0; j <= vCustInfoNameArr.GetUpperBound(0); j++)
                {
                    ok = swModel.DeleteCustomInfo2("", vCustInfoNameArr[j]);
                }

                const int swSumInfoTitle = 0;
                const int swSumInfoSubject = 1;
                const int swSumInfoAuthor = 2;
                const int swSumInfoKeywords = 3;
                const int swSumInfoComment = 4;

                swModel.SummaryInfo[swSumInfoTitle] = "";
                swModel.SummaryInfo[swSumInfoSubject] = "";
                swModel.SummaryInfo[swSumInfoAuthor] = "";
                swModel.SummaryInfo[swSumInfoKeywords] = "";
                swModel.SummaryInfo[swSumInfoComment] = "";

                //if (MDel != 1)
                //{
                MStart = 1;
                main();
                //}
                if (swModel.GetType() == (int)swDocumentTypes_e.swDocPART)
                {
                    swPartDoc = (PartDoc)swModel;
                    swPartDoc.SetMaterialPropertyName("", "");
                }

                MaterialTxt = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}