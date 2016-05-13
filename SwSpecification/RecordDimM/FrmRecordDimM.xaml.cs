using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
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
using System.Diagnostics;
using SolidWorks.Interop.swconst;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Controls.Primitives;

namespace RecordDimM
{
    public partial class FrmRecordDimM : Window
    {
        public FrmRecordDimM()
        {
            try
            {
                InitializeComponent();
                UserForm_Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        #region Поля

        ClassRecordDimM ClassRecord;

        DisplayDimension DimProp;
        double DPValue;
        double DPValuePr;
        Dimension DP;
        int i;
        string StartStr;
        string StartStr2;
        string EndStr;
        string BottomStr;
        string StartStrPr;
        string StartStr2Pr;
        string ENDstrPr;
        string BottomStrPr;
        string Suffix;
        string Prefix;
        string CalloutBelow;
        int Col;
        double StepValue;
        object varTemp;
        object varTemp1;
        object varTemp2;
        string strTemp;
        string AStr;
        string Numb;
        string sLeft;
        string sStep;
        string sDop;
        public long DimPrec;
        long BrokenLeaderRet;
        object Offset;
        int m3; // Метка действия m3 = 0 - действия программы, m3 = 1 - действия пользователя
        int m4; // Метка полей ввода
        int m5; // Метка формата
        string sDeg;
        int m_OptDim;
        int m_ColEl;
        int m_fask;
        int m_Otv;
        int m_RezOvt;
        int m_Rad;
        int m_OptPaz;
        int m_OptExt;
        int m_OptPlace;
        int m_Massiv;
        bool cop;
        bool ok;
        string str1, str2;
        #endregion   
        private void TogButFormat_Click(object sender, RoutedEventArgs e)
        {

            if (TogButFormat.IsChecked == true) // Нажата
            {
                m5 = 1;

                CommandButton1_Click(true, null);
                CommandButton1.IsEnabled = false;
                CmdClose.IsEnabled = false;
                CmdCancel.IsEnabled = false;
            }
            else // Отжата
            {
                m5 = 0;
                CommandButton1.IsEnabled = true;
                CmdClose.IsEnabled = true;
                CmdCancel.IsEnabled = true;
            }
        }
        public void UserForm_Activate()
        {
            try
            {

           
            ClassRecord = new ClassRecordDimM { FrmRecord = this };

            ClassRecord.MonitorSolidWorks();

            Debug.Print("Новая активация");
            m3 = 0;
            m4 = 0;

            // Проверка ошибки
            if (ClassRecord.m2 == 1) // Ошибка
            {
                // Гасим лишнее
                TogButFormat.IsEnabled = false;
                TogButFormat.IsChecked = false;
                ColEl.IsEnabled = false;
                Otv.IsEnabled = false;
                RezOvt.IsEnabled = false;
                Fask.IsEnabled = false;
                Rad.IsEnabled = false;
                OptPaz.IsEnabled = false;
                Massiv.IsEnabled = false;
                OptPlace.IsEnabled = false;
                OptDim.IsEnabled = false;
                OptExt.IsEnabled = false;
                ChkDouble.IsEnabled = false;
                ChkStar.IsEnabled = false;
                CboStar.IsEnabled = false;
                ChkDis.IsEnabled = false;
                ChkHor.IsEnabled = false;
                ChkChamfer.IsEnabled = false;
                Faska.IsEnabled = false;
                ChkDop.IsEnabled = false;
                CboDop.IsEnabled = false;
                ChkStep.IsEnabled = false;
                CboStep.IsEnabled = false;
                ChkLeft.IsEnabled = false;
                CommandButton1.IsEnabled = false;
                CmdClose.IsEnabled = false;
                TxtUser.Visibility = Visibility.Hidden;
                TxtPref.Visibility = Visibility.Hidden;
                TxtSuf.Visibility = Visibility.Hidden;
                return;
            }
            else // Все ок
            {
                Debug.Print("m5=", m5);
                DimProp = ClassRecord.SelMgr.GetSelectedObject5(1); // Получаем размер с фичами
                if (m5 == 1)
                {
                    CommandButton1_Click(true, null);
                    m3 = 1;
                    return;
                }
                else // Кнопка формат отжата
                {
                    TogButFormat.IsEnabled = true;
                    ColEl.IsEnabled = true;
                    Otv.IsEnabled = true;
                    RezOvt.IsEnabled = true;
                    Fask.IsEnabled = true;
                    Rad.IsEnabled = true;
                    OptPaz.IsEnabled = true;
                    Massiv.IsEnabled = true;
                    OptPlace.IsEnabled = true;
                    OptDim.IsEnabled = true;
                    OptExt.IsEnabled = true;
                    ChkDouble.IsEnabled = true;
                    ChkStar.IsEnabled = true;
                    CboStar.IsEnabled = true;
                    ChkDis.IsEnabled = true;
                    ChkHor.IsEnabled = true;
                    ChkChamfer.IsEnabled = true;
                    Faska.IsEnabled = true;
                    ChkDop.IsEnabled = true;
                    CboDop.IsEnabled = true;
                    ChkStep.IsEnabled = true;
                    CboStep.IsEnabled = true;
                    ChkLeft.IsEnabled = true;
                    CommandButton1.IsEnabled = true;
                    CmdClose.IsEnabled = true;
                    TxtUser.Visibility = Visibility.Visible;
                    TxtPref.Visibility = Visibility.Visible;
                    TxtSuf.Visibility = Visibility.Visible;
                }
            }

            Prefix = DimProp.GetText((int)swDimensionTextParts_e.swDimensionTextPrefix); // Префикс
            Suffix = DimProp.GetText((int)swDimensionTextParts_e.swDimensionTextSuffix); // Суффикс
            CalloutBelow = DimProp.GetText((int)swDimensionTextParts_e.swDimensionTextCalloutBelow); // Текст под размером
            DP = DimProp.GetDimension(); // Получаем только размер
            DPValue = DP.GetValue2(""); // Получаем величину размера
            DimPrec = DimProp.GetPrimaryPrecision2(); // Получаем точность размера
            DPValuePr = DPValue;

            DPValuePr = MyRound(DPValuePr, DimPrec); // Округляем размер

            Debug.Print("Размер=", DPValuePr);
            Debug.Print("Prefix=", DimProp.GetText((int)swDimensionTextParts_e.swDimensionTextPrefix));
            Debug.Print("Suffix=", DimProp.GetText((int)swDimensionTextParts_e.swDimensionTextSuffix));
            Debug.Print("CalloutAbove=", DimProp.GetText((int)swDimensionTextParts_e.swDimensionTextCalloutAbove));
            Debug.Print("CalloutBelow=", DimProp.GetText((int)swDimensionTextParts_e.swDimensionTextCalloutBelow));
            Debug.Print("TextAll=", DimProp.GetText((int)swDimensionTextParts_e.swDimensionTextAll));

            ColEl.Items.Clear(); // Список количества
            for (i = 1; i <= 50; i++)
            {
                ColEl.Items.Add(i);
            }
           
            Faska.Items.Clear(); // Список фасок
            Faska.Items.Add("0,3");
            Faska.Items.Add("0,5");
            Faska.Items.Add("1");
            Faska.Items.Add("1,6");
            Faska.Items.Add("2");
            Faska.SelectedIndex = 2;

            CboDop.Items.Clear(); // Список допусков резьбы
            CboDop.Items.Add("6g");
            CboDop.Items.Add("8g");
            CboDop.Items.Add("6H");
            CboDop.Items.Add("7H");
            CboDop.SelectedIndex = 2;

            CboStep.Items.Clear(); // Список мелких шагов
            CboStep.Items.Add("0,25");
            CboStep.Items.Add("0,35");
            CboStep.Items.Add("0,5");
            CboStep.Items.Add("0,75");
            CboStep.Items.Add("1");
            CboStep.Items.Add("1,5");
            CboStep.Items.Add("2");
            CboStep.SelectedIndex = 2;

            CboStar.Items.Clear(); // Список звездочек
            CboStar.Items.Add("*");
            CboStar.Items.Add("**");
            CboStar.Items.Add("***");
            CboStar.SelectedIndex = 0;

            StartStr = ""; // Префикс для однострочного отображения
            StartStr2 = ""; // Префикс для двустрочного отображения
            EndStr = ""; // Суффикс
            BottomStr = ""; // Вторая строка

            // ********* Анализатор **********
            // Заменяем русские символы на латинские
            Prefix = Prefix.Replace("М", "M");
            Suffix = Suffix.Replace("х", "x");
            Suffix = Suffix.Replace("-", "-");

            // Заменяем поля на символы
            Prefix = Prefix.Replace("<MOD-PM>", Convert.ToString((char)(177)));
            Prefix = Prefix.Replace("<MOD-DEG>", Convert.ToString((char)(176)));
            Prefix = Prefix.Replace("<MOD-DIAM>", Convert.ToString((char)(0xd8)));
            Prefix = Prefix.Replace("<MOD-BOX>", Convert.ToString((char)(0x25a1)));
     
            Suffix = Suffix.Replace("<MOD-PM>", Convert.ToString((char)(177)));
            Suffix = Suffix.Replace("<MOD-DEG>", Convert.ToString((char)(176)));
            Suffix = Suffix.Replace("<MOD-DIAM>", Convert.ToString((char)(0xd8)));
            Suffix = Suffix.Replace("<MOD-BOX>", Convert.ToString((char)(0x25a1)));

            CalloutBelow = CalloutBelow.Replace("<MOD-PM>", Convert.ToString((char)(177)));
            CalloutBelow = CalloutBelow.Replace("<MOD-DEG>", Convert.ToString((char)(176)));
            CalloutBelow = CalloutBelow.Replace("<MOD-DIAM>", Convert.ToString((char)(0xd8)));
            CalloutBelow = CalloutBelow.Replace("<MOD-BOX>", Convert.ToString((char)(0x25a1)));

            // Проверка количества строк
            varTemp = Strings.InStr(CalloutBelow, "stack size");

            if ((int)varTemp > 0) // Две строки
            {
                ChkDouble.IsChecked = true;
                varTemp1 = Strings.InStr(CalloutBelow, "FONT size");

                if ((int)varTemp1 > 0)
                {
                    AStr = Strings.Right(CalloutBelow, Strings.Len(CalloutBelow) - 43);
                }
                else
                {
                    varTemp2 = Strings.InStr(CalloutBelow, "stack size=0.");
                    if ((int)varTemp2 > 0)
                    {
                        AStr = Strings.Right(CalloutBelow, Strings.Len(CalloutBelow) - 31);
                    }
                    else
                    {
                        AStr = Strings.Right(CalloutBelow, Strings.Len(CalloutBelow) - 14);
                    }
                }
            }
            else // Одна строка
            {
                ChkDouble.IsChecked = false;
                AStr = Prefix;
                Debug.Print("Одна строка");
            }

            ChkDouble_Click(true, null);

            if (Strings.InStr(Prefix, "M") > 0) // Резьбовое отверстие
            {
                RezOvt.IsChecked = true;
                if (Strings.InStr(Suffix, "/") > 0) // Нашли фаску
                {
                    Debug.Print("Нашли фаску");
                    ChkChamfer.IsChecked = true;
                    Faska.Text = Strings.Mid(Suffix, Strings.InStr(Suffix, "/") + 1,
                        Strings.InStr(Strings.InStr(Suffix, "/"), Suffix, "x") - Strings.InStr(Suffix, "/") - 1);
                }
                else // Фаски нет
                {
                    Debug.Print("Фаски нет");
                    ChkChamfer.IsChecked = false;
                }

                if (Strings.InStr(Suffix, "LH") > 0) // Найдена левая резьба
                {
                    ChkLeft.IsChecked = true;
                    if (Strings.InStr(Strings.InStr(Suffix, "LH"), Suffix, "-") > 0)
                    {
                        sDop = Strings.Mid(Suffix, Strings.InStr(Strings.InStr(Suffix, "LH"), Suffix, "-") + 1, 2);
                    }
                    else { sDop = ""; }
                }
                else // Левой резьбы нет
                {
                    ChkLeft.IsChecked = false;
                    if (Strings.InStr(Suffix, "-") > 0)
                    {
                        sDop = Strings.Mid(Suffix, Strings.InStr(Suffix, "-") + 1, 2);
                    }
                    else
                    {
                        sDop = "";
                    }
                }

                if (sDop == "") // Нет допуска
                { ChkDop.IsChecked = false; }
                else // Допуск найден
                {
                    CboDop.Text = sDop;
                    ChkDop.IsChecked = true;
                }

                if (Strings.InStr(Suffix, "x") == 1) // Найден мелкий шаг
                {
                    ChkStep.IsChecked = true;
                    i = 2;
                    Numb = "";

                    while (Convert.ToBoolean(int.Parse(Strings.Mid(Suffix, i, 1))) == true |
                        Strings.InStr((Strings.Mid(Suffix, i, 1)), ",") > 0 |
                        Strings.InStr((Strings.Mid(Suffix, i, 1)), ".") > 0)
                    {
                        Numb = Numb + Strings.Mid(Suffix, i, 1);
                        i = i + 1;
                    }

                    CboStep.Text = Numb;
                }
                else // Мелкого шага нет
                {
                    ChkStep.IsChecked = false;
                }

                RezOvt_Click(true, null);

            }
            else if (Strings.InStr(Prefix, "R") > 0) // Радиус
            {
                Rad.IsChecked = true;
                Rad_Click(true, null);
            }
            else if (Strings.InStr(Suffix, Strings.Chr(176).ToString()) > 0 & Strings.InStr(Suffix, "/") == 0) // Фаска
            {
                Fask.IsChecked = true;
                Fask_Click(true, null);
            }
            else if (Strings.InStr(AStr, "паз") > 0) // Паз
            {
                OptPaz.IsChecked = true;
                OptPaz_Click(true, null);
            }
            else if (Strings.InStr(AStr, "выступ") > 0) // Выступ
            {
                OptExt.IsChecked = true;
                OptExt_Click(true, null);
            }
            else if (Strings.InStr(Prefix, "=") > 0) // Массив
            {
                Massiv.IsChecked = true;
                Massiv_Click(true, null);
            }
            else if (Strings.InStr(Prefix, "мест") > 0) // Место
            {
                OptPlace.IsChecked = true;
                OptPlace_Click(true, null);
            }
            else if (Strings.InStr(Prefix, "мест") > 0) // Обычное отверстие
            {
                Otv.IsChecked = true;
                if (Strings.InStr(Suffix, "/") > 0) // С фаской
                {
                    ChkChamfer.IsChecked = true;
                    Faska.Text = Strings.Mid(Suffix, Strings.InStr(Suffix, "/") + 1,
                        Strings.InStr(Suffix, "x") - Strings.InStr(Suffix, "/") - 1);
                }
                else
                {
                    ChkChamfer.IsChecked = false;
                }

                Otv_Click(true, null);
            }
            else // Простой размер
            {
                OptDim.IsChecked = true;
                OptDim_Click(true, null);
            }

            if (OptDim.IsChecked == true) // Простой размер
            {
                ColEl.Text = "1";
            }
            else
            {
                // Проверка количества элементов
                i = 1;
                Numb = "";

                while (IsNumeric(Strings.Mid(AStr, i, 1)) == true)
                {
                    Numb = Numb + int.Parse(Strings.Mid(AStr, i, 1));
                    i = i + 1;
                }

                if (Numb == "") // Элемент один
                {
                    ColEl.Text = "1";
                }
                else // Элементов больше одного
                {
                    ColEl.Text = Numb;
                }

                Debug.Print("*******", Numb, AStr);

                // Проверка
                if (Strings.InStr(Suffix, "*") > 0) // Найдена *
                {
                    ChkStar.IsChecked = true;
                    CboStar.Text = Strings.Right(Suffix, Strings.Len(Suffix) - Strings.InStr(Suffix, "*") + 1);
                }
                else
                {
                    ChkStar.IsChecked = false;
                }
            }

            ColEl_SelectionChanged(true, null);
            ChkStar_Click(true, null);

            // Проверка отображения размера

            Offset = DimProp.OffsetText;
            BrokenLeaderRet = DimProp.GetBrokenLeader2();
            if (Convert.ToBoolean(Offset) == true) // Текст смещен
            {
                ChkDis.IsChecked = true;
            }
            else
            {
                ChkDis.IsChecked = false;

                if (BrokenLeaderRet == 2) // Текст горизонтально
                {
                    ChkHor.IsChecked = true;
                }
                else
                {
                    ChkHor.IsChecked = false;
                }
            }

            m3 = 1;
            Debug.Print("Обновление");
            Change();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        // IsNumeric
        public bool IsNumeric(string s)
        {
            float output;
            return float.TryParse(s, out output);
        }
        void Change()
        {
            StartStr = ""; // Префикс для однострочного отображения
            StartStr2 = ""; // Префикс для двустрочного отображения
            EndStr = ""; // Суффикс
            BottomStr = ""; // Вторая строка
            Col = ColEl.SelectedIndex; // Количество

            var ColElitem = Convert.ToInt16(ColEl.SelectedValue);
            var colElText = ColElitem.ToString();
            var faskaItem = Faska.SelectedValue;
            var faskaItemText = faskaItem.ToString();
            var cboDopItemText = CboDop.SelectedValue;
            var cboStepItemText = CboStep.SelectedValue;

            Debug.Print(cboDopItemText.ToString());

            #region Отверстие
            if (Otv.IsChecked == true) // Отверстие
            {
                if (ChkDouble.IsChecked == true)
                {
                    StartStr2 = "<MOD-DIAM>";
                    BottomStr = colElText + " отв.";
                }
                else
                {
                    if (Col == 0)
                    {
                        StartStr = "<MOD-DIAM>";
                    }
                    else
                    {
                        StartStr = colElText + " отв.<MOD-DIAM>";
                    }
                }

                if (ChkChamfer.IsChecked == false || ChkChamfer.IsEnabled == false)
                { }
                else
                {
                    EndStr = "/" + faskaItemText + "x45<MOD-DEG>";
                }
            }
            #endregion
            #region Резьбовое отверстие
            if (RezOvt.IsChecked == true) // Резьбовое отверстие
            {
                if (ChkDouble.IsChecked == true)
                {
                    StartStr2 = "M";
                    BottomStr = colElText + " отв.";
                }
                else
                {
                    if (Col == 0)
                    {
                        StartStr = "M";
                    }
                    else
                    {
                        StartStr = colElText + " отв.М";
                    }
                }

                if (ChkLeft.IsChecked == true)
                {
                    sLeft = "-LH";
                }
                else
                {
                    sLeft = "";
                }

                if (ChkStep.IsChecked == true)
                {
                    sStep = "x" + cboStepItemText;
                }
                else
                {
                    sStep = "";
                }

                if (ChkDop.IsChecked == false)
                {
                    sDop = "-" + cboDopItemText;
                }
                else
                {
                    sDop = "";
                }

                if (ChkChamfer.IsChecked == false || ChkChamfer.IsEnabled == false)
                {
                    EndStr = sStep + sLeft + sDop;
                }
                else
                {
                    EndStr = sStep + sLeft + sDop + "/" + faskaItemText + "x45<MOD-DEG>";
                }


            }
            #endregion
            #region Радиусы
            if (Rad.IsChecked == true) // Радиусы
            {
                if (ChkDouble.IsChecked == true)
                {
                    StartStr2 = "R";
                    if (Convert.ToInt32(Strings.Right(colElText, 2)) <= 20 &&
                        Convert.ToInt32(Strings.Right(colElText, 2)) != 0)
                    {
                        if (colElText == "1")
                        { } // Пусто
                        else
                        {
                            if (Convert.ToInt32(Strings.Right(colElText, 2)) < 5)
                            {
                                if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                                {
                                    BottomStr = colElText + " радиус";
                                }
                                else
                                {
                                    BottomStr = colElText + " радиуса";
                                }
                            }
                            else
                            {
                                BottomStr = colElText + " радиусов";
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                        {
                            BottomStr = colElText + " радиус";
                        }
                        else if (Convert.ToInt32(Strings.Right(colElText, 1)) > 1 &&
                            Convert.ToInt32(Strings.Right(colElText, 1)) < 5)
                        {
                            BottomStr = colElText + " радиуса";
                        }
                        else
                        {
                            BottomStr = colElText + " радиусов";
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(Strings.Right(colElText, 2)) <= 20 &&
                        Convert.ToInt32(Strings.Right(colElText, 2)) != 0)
                    {
                        if (colElText == "1")
                        {
                            StartStr = "R";
                        }
                        else
                        {
                            if (Convert.ToInt32(Strings.Right(colElText, 2)) < 5)
                            {
                                if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                                {
                                    StartStr = colElText + " радиус R";
                                }
                                else
                                {
                                    StartStr = colElText + " радиуса R";
                                }
                            }
                            else
                            {
                                StartStr = colElText + " радиусов R";
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                        {
                            StartStr = colElText + " радиус R";
                        }
                        else if (Convert.ToInt32(Strings.Right(colElText, 1)) > 1 &&
                            Convert.ToInt32(Strings.Right(colElText, 1)) < 5)
                        {
                            StartStr = colElText + " радиуса R";
                        }
                        else
                        {
                            StartStr = colElText + " радиусов R";
                        }
                    }
                }

            }
            #endregion
            #region Фаски
            if (Fask.IsChecked == true) // Фаски
            {
                if (ChkDouble.IsChecked == true)
                {
                    if (Convert.ToInt32(Strings.Right(colElText, 2)) <= 20 &&
                        Convert.ToInt32(Strings.Right(colElText, 2)) != 0)
                    {
                        if (Convert.ToInt32(Strings.Right(colElText, 2)) < 5)
                        {
                            if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                            {
                                BottomStr = colElText + " фаска";
                            }
                            else
                            {
                                BottomStr = colElText + " фаски";
                            }
                        }
                        else
                        {
                            BottomStr = colElText + " фасок";
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                        {
                            BottomStr = colElText + " фаска";
                        }
                        else if (Convert.ToInt32(Strings.Right(colElText, 1)) > 1 &&
                            Convert.ToInt32(Strings.Right(colElText, 1)) < 5)
                        {
                            BottomStr = colElText + " фаски";
                        }
                        else
                        {
                            BottomStr = colElText + " фасок";
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(Strings.Right(colElText, 2)) <= 20 &&
                        Convert.ToInt32(Strings.Right(colElText, 2)) != 0)
                    {
                        if (colElText == "1")
                        {
                        }
                        else
                        {
                            if (Convert.ToInt32(Strings.Right(colElText, 2)) < 5)
                            {
                                if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                                {
                                    StartStr = colElText + " фаска ";
                                }
                                else
                                {
                                    StartStr = colElText + " фаски ";
                                }
                            }
                            else
                            {
                                StartStr = colElText + " фасок ";
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                        {
                            StartStr = colElText + " фаска ";
                        }
                        else if (Convert.ToInt32(Strings.Right(colElText, 1)) > 1 &&
                            Convert.ToInt32(Strings.Right(colElText, 1)) < 5)
                        {
                            StartStr = colElText + " фаски ";
                        }
                        else
                        {
                            StartStr = colElText + " фасок ";
                        }
                    }
                }
                EndStr = "x45<MOD-DEG>";
            }
            #endregion
            #region Пазы
            if (OptPaz.IsChecked == false) // Пазы
            {
                if (ColEl.Items.Count < 2)
                {
                    MessageBox.Show("Значение количества для пазов должно быть 2 или более");
                    colElText = "2";
                }
            }
            else
            {
                if (ChkDouble.IsChecked == true)
                {
                    if (Convert.ToInt32(Strings.Right(colElText, 2)) <= 20 &&
                        Convert.ToInt32(Strings.Right(colElText, 2)) != 0)
                    {
                        if (Convert.ToInt32(Strings.Right(colElText, 2)) < 5)
                        {
                            if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                            {
                                BottomStr = colElText + " паз";
                            }
                            else
                            {
                                BottomStr = colElText + " паза";
                            }
                        }
                        else
                        {
                            BottomStr = colElText + " пазов";
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                        {
                            BottomStr = colElText + " паз";
                        }
                        else if (Convert.ToInt32(Strings.Right(colElText, 1)) > 1 &&
                            Convert.ToInt32(Strings.Right(colElText, 1)) < 5)
                        {
                            BottomStr = colElText + " паза";
                        }
                        else
                        {
                            BottomStr = colElText + " пазов";
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(Strings.Right(colElText, 2)) <= 20 &&
                        Convert.ToInt32(Strings.Right(colElText, 2)) != 0)
                    {
                        if (colElText == "1")
                        {
                        }
                        else
                        {
                            if (Convert.ToInt32(Strings.Right(colElText, 2)) < 5)
                            {
                                if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                                {
                                    StartStr = colElText + " паз ";
                                }
                                else
                                {
                                    StartStr = colElText + " паза ";
                                }
                            }
                            else
                            {
                                StartStr = colElText + " пазов ";
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                        {
                            StartStr = colElText + " паз ";
                        }
                        else if (Convert.ToInt32(Strings.Right(colElText, 1)) > 1 &&
                            Convert.ToInt32(Strings.Right(colElText, 1)) < 5)
                        {
                            StartStr = colElText + " паза ";
                        }
                        else
                        {
                            StartStr = colElText + " пазов ";
                        }
                    }
                }
            }
            #endregion
            #region Выступы
            if (OptExt.IsChecked == true) // Выступы
            {
                if (Convert.ToInt32(colElText) < 2)
                {
                    MessageBox.Show("Значение количества для выступов должно быть 2 или более");
                    colElText = "2";
                }
                else
                {
                    if (ChkDouble.IsChecked == true)
                    {
                        if (Convert.ToInt32(Strings.Right(colElText, 2)) <= 20 &&
                            Convert.ToInt32(Strings.Right(colElText, 2)) != 0)
                        {
                            if (Convert.ToInt32(Strings.Right(colElText, 2)) < 5)
                            {
                                if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                                {
                                    BottomStr = colElText + " выступ";
                                }
                                else
                                {
                                    BottomStr = colElText + " выступа";
                                }
                            }
                            else
                            {
                                BottomStr = colElText + " выступов";
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                            {
                                BottomStr = colElText + " выступ";
                            }
                            else if (Convert.ToInt32(Strings.Right(colElText, 1)) > 1 &&
                                Convert.ToInt32(Strings.Right(colElText, 1)) < 5)
                            {
                                BottomStr = colElText + " выступа";
                            }
                            else
                            {
                                BottomStr = colElText + " выступов";
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(Strings.Right(colElText, 2)) <= 20 &&
                            Convert.ToInt32(Strings.Right(colElText, 2)) != 0)
                        {
                            if (colElText == "1")
                            {
                            }
                            else
                            {
                                if (Convert.ToInt32(Strings.Right(colElText, 2)) < 5)
                                {
                                    if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                                    {
                                        StartStr = colElText + " выступ ";
                                    }
                                    else
                                    {
                                        StartStr = colElText + " выступа ";
                                    }
                                }
                                else
                                {
                                    StartStr = colElText + " выступов ";
                                }
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                            {
                                StartStr = colElText + " выступ ";
                            }
                            else if (Convert.ToInt32(Strings.Right(colElText, 1)) > 1 &&
                                Convert.ToInt32(Strings.Right(colElText, 1)) < 5)
                            {
                                StartStr = colElText + " выступа ";
                            }
                            else
                            {
                                StartStr = colElText + " выступов ";
                            }
                        }
                    }
                }
            }
            #endregion
            #region Массив
            if (Massiv.IsChecked == true) // Массив
            {
                if (Convert.ToInt32(colElText) < 3 || colElText == "")
                {
                    MessageBox.Show("Значение количества для массива должно быть 3 или более");
                    colElText = "3";
                }
                else
                {
                    StepValue = DPValue / Double.Parse(colElText);
                    StepValue = MyRound(StepValue, DimPrec + 1);
                    StartStr = colElText + "x" + StepValue.ToString() + "=";
                    EndStr = "";
                }
            }
            #endregion
            #region Места
            if (OptPlace.IsChecked == true) // Места
            {
                if (Convert.ToInt32(colElText) < 2)
                {
                    MessageBox.Show("Значение количества для мест должно быть 2 или более");
                    colElText = "2";
                }
                else
                {
                    if (ChkDouble.IsChecked == true)
                    {
                        if (Convert.ToInt32(Strings.Right(colElText, 2)) <= 20 &&
                            Convert.ToInt32(Strings.Right(colElText, 2)) != 0)
                        {
                            if (Convert.ToInt32(Strings.Right(colElText, 2)) < 5)
                            {
                                if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                                {
                                    BottomStr = colElText + " место";
                                }
                                else
                                {
                                    BottomStr = colElText + " места";
                                }
                            }
                            else
                            {
                                BottomStr = colElText + " мест";
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                            {
                                BottomStr = colElText + " место";
                            }
                            else if (Convert.ToInt32(Strings.Right(colElText, 1)) > 1 &&
                                Convert.ToInt32(Strings.Right(colElText, 1)) < 5)
                            {
                                BottomStr = colElText + " места";
                            }
                            else
                            {
                                BottomStr = colElText + " мест";
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(Strings.Right(colElText, 2)) <= 20 &&
                            Convert.ToInt32(Strings.Right(colElText, 2)) != 0)
                        {
                            if (colElText == "1")
                            {
                            }
                            else
                            {
                                if (Convert.ToInt32(Strings.Right(colElText, 2)) < 5)
                                {
                                    if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                                    {
                                        StartStr = colElText + " место ";
                                    }
                                    else
                                    {
                                        StartStr = colElText + " места ";
                                    }
                                }
                                else
                                {
                                    StartStr = colElText + " мест ";
                                }
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(Strings.Right(colElText, 1)) == 1)
                            {
                                StartStr = colElText + " место ";
                            }
                            else if (Convert.ToInt32(Strings.Right(colElText, 1)) > 1 &&
                                Convert.ToInt32(Strings.Right(colElText, 1)) < 5)
                            {
                                StartStr = colElText + " места ";
                            }
                            else
                            {
                                StartStr = colElText + " мест ";
                            }
                        }
                    }
                }
            }
            #endregion
            #region Простой размер

            if (OptDim.IsChecked == true) // Простой размер
            {
                StartStr = Prefix;
                BottomStr = CalloutBelow;
                EndStr = Suffix;
            }

            // ******* Отображение *******
            // Проверка углового размера
            if (DimProp.GetType() == 3)
            {
                sDeg = Strings.Chr(176).ToString();
            }
            else
            {
                sDeg = "";
            }

            if (OptDim.IsChecked == true) // Простой размер
            {
                StartStrPr = StartStr;
                ENDstrPr = EndStr;
                LblFirst.Content = DPValuePr + sDeg;
                //TODO: LblFirst.AutoSize = true
                //LblFirst.AutoSize = true;
                //LblFirst.left = 318 - LblFirst.Width / 2;
                TxtPref.Text = StartStrPr;
                //TxtPref.Width = 150 - LblFirst.Width / 2 + 5;
                //TxtPref.Left = 168;
                TxtSuf.Text = ENDstrPr;
                TxtSuf.Width = TxtPref.Width + 5;
                //TxtSuf.Left = 318 + LblFirst.Width / 2 - 10;

                if (ChkDouble.IsChecked == true) // В две строки
                {
                    if (Convert.ToInt32(varTemp) > 0)
                    {
                        if (Convert.ToInt32(varTemp1) > 0)
                        {
                            BottomStrPr = Strings.Right(BottomStr, Strings.Len(BottomStr) - 43);
                        }
                        else
                        {
                            if (Convert.ToInt32(varTemp2) > 0)
                            {
                                BottomStrPr = Strings.Right(BottomStr, Strings.Len(BottomStr) - 31);
                            }
                            else
                            {
                                BottomStrPr = Strings.Right(BottomStr, Strings.Len(BottomStr) - 14);
                            }
                        }
                    }
                    else
                    {
                        BottomStrPr = BottomStr;
                    }
                    TxtUser.Text = BottomStrPr;
                }
                else // В одну строку
                {
                    TxtUser.Text = "";
                    LblSecond.Content = "";
                }
            }
            else // Другие типы размеров
            {
                // Добавление звездочки
                if (ChkStar.IsChecked == true)
                {
                    EndStr = EndStr + CboStar.Text;
                }
                //char.ConvertFromUtf32((int)e.KeyCode)
                // Замена полей на символы
                StartStrPr = StartStr.Replace("<MOD-DIAM>", char.ConvertFromUtf32(0xd8));
                StartStr2Pr = StartStr2.Replace("<MOD-DIAM>", char.ConvertFromUtf32(0xd8));
                ENDstrPr = EndStr.Replace("<MOD-DEG>", char.ConvertFromUtf32(176));

                if (ChkDouble.IsChecked == true) // В две строки
                {
                    BottomStrPr = BottomStr;
                    LblFirst.Content = StartStr2Pr + DPValuePr + sDeg + ENDstrPr;
                    LblSecond.Content = BottomStrPr;
                }
                else // В одну строку
                {
                    LblFirst.Content = StartStrPr + DPValuePr + ENDstrPr;
                    LblSecond.Content = "";
                }

                //LblFirst.AutoSize = false;
                LblFirst.Width = 300;
                //LblFirst.Left = 168;
            }
            #endregion
        }
        #region Buttons
        // Применить
        private void CommandButton1_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            Debug.Print("CommandButton1_Click");
            if (OptDim.IsChecked == true) // Простой размер
            {
                StartStr = TxtPref.Text;
                EndStr = TxtSuf.Text;
                BottomStr = TxtUser.Text;

                // Обратно заменяем символы на поля
                StartStr = StartStr.Replace(Convert.ToString((char)(177)), "<MOD-PM>");
                StartStr = StartStr.Replace(Convert.ToString((char)(176)), "<MOD-DEG>");
                StartStr = StartStr.Replace(Convert.ToString((char)(0xD8)), "<MOD-DIAM>");
                StartStr = StartStr.Replace(Convert.ToString((char)(0x25A1)), "<MOD-BOX>");

                EndStr = EndStr.Replace(Convert.ToString((char)(177)), "<MOD-PM>");
                EndStr = EndStr.Replace(Convert.ToString((char)(176)), "<MOD-DEG>");
                EndStr = EndStr.Replace(Convert.ToString((char)(0xD8)), "<MOD-DIAM>");
                EndStr = EndStr.Replace(Convert.ToString((char)(0x25A1)), "<MOD-BOX>");

                BottomStr = BottomStr.Replace(Convert.ToString((char)(177)), "<MOD-PM>");
                BottomStr = BottomStr.Replace(Convert.ToString((char)(176)), "<MOD-DEG>");
                BottomStr = BottomStr.Replace(Convert.ToString((char)(0xD8)), "<MOD-DIAM>");
                BottomStr = BottomStr.Replace(Convert.ToString((char)(0x25A1)), "<MOD-BOX>");

                DimProp.SetText((int)swDimensionTextParts_e.swDimensionTextSuffix, EndStr);

                if (ChkDouble.IsChecked == true)
                {
                    DimProp.SetText((int)swDimensionTextParts_e.swDimensionTextPrefix, " " + StartStr);
                    DimProp.SetText((int)swDimensionTextParts_e.swDimensionTextCalloutBelow, "<stack size=0.2> <stack size=5>" + BottomStr);
                }
                else
                {
                    DimProp.SetText((int)swDimensionTextParts_e.swDimensionTextPrefix, StartStr);
                    DimProp.SetText((int)swDimensionTextParts_e.swDimensionTextCalloutBelow, "");
                }
            }
            else // Другие типы размеров
            {
                DimProp.SetText((int)swDimensionTextParts_e.swDimensionTextSuffix, EndStr);
                if (ChkDouble.IsChecked == true) // Две строки
                {
                    DimProp.SetText((int)swDimensionTextParts_e.swDimensionTextPrefix, " " + StartStr2);
                    DimProp.SetText((int)swDimensionTextParts_e.swDimensionTextCalloutBelow,
                        "<stack size=0.2> <stack size=5>" + BottomStr);
                }
                else
                {
                    DimProp.SetText((int)swDimensionTextParts_e.swDimensionTextPrefix, StartStr);
                    DimProp.SetText((int)swDimensionTextParts_e.swDimensionTextCalloutBelow, "");
                }
            }

            if (ChkDis.IsChecked == true)
            {
                Offset = true;
            }
            else
            {
                Offset = false;
                if (ChkHor.IsChecked == true)
                {
                    ok = Convert.ToBoolean(DimProp.SetBrokenLeader2(false,
                        (int)swDisplayDimensionLeaderText_e.swBrokenLeaderHorizontalText));
                }
                else
                {
                    ok = Convert.ToBoolean(DimProp.SetBrokenLeader2(true,
                        (int)swDisplayDimensionLeaderText_e.swBrokenLeaderHorizontalText));
                }
            }

            DimProp.OffsetText = Convert.ToBoolean(Offset);
            ClassRecord.swModel.GraphicsRedraw2();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        // Применить и закрыть
        private void CmdClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommandButton1_Click(true, null);
                Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        // Закрыть
        private void CommandButton2_Click(object sender, RoutedEventArgs e)
        {
            Close();
            return;
        }
        // Отмена
        private void CmdCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserForm_Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        #endregion
        // Фаски
        private void Fask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            if (m3 == 0) // Программа
            {
                m_fask = 0;
            }
            else
            {
                m_fask = 1; // Пользователь
            }

            m3 = 0;

            TxtPref.Visibility = Visibility.Hidden;
            TxtSuf.Visibility = Visibility.Hidden;
            ChkChamfer.IsEnabled = false;
            Faska.IsEnabled = false;
            ChkDop.IsEnabled = false;
            CboDop.IsEnabled = false;
            CboStep.IsEnabled = false;
            ChkStep.IsEnabled = false;
            ChkLeft.IsEnabled = false;
            ChkStar.IsEnabled = true;
            ChkStar_Click(true, null);
            ChkDouble.IsEnabled = true;
            ChkDouble_Click(true, null);
            ColEl.IsEnabled = true;
            ColEl_SelectionChanged(true, null);

            OptDim.IsChecked = false;

            if (m_fask == 1)
            {
                m3 = 1;
                Change();
            }

            Debug.Print("Fask_Click end", m3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        // Отверстия
        private void Otv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (m3 == 0) // Программа
                {
                    m_Otv = 0;
                }
                else
                {
                    m_Otv = 1; // Пользователь
                }
                m3 = 0;

                TxtPref.Visibility = Visibility.Hidden;
                TxtSuf.Visibility = Visibility.Hidden;
                ChkChamfer.IsEnabled = true;
                ChkChamfer_Click(true, null);
                ChkDop.IsEnabled = false;
                CboDop.IsEnabled = false;
                CboStep.IsEnabled = false;
                ChkStep.IsEnabled = false;
                ChkLeft.IsEnabled = false;
                ChkStar.IsEnabled = true;
                ChkStar_Click(true, null);
                ChkDouble.IsEnabled = true;
                ChkDouble_Click(true, null);
                ColEl.IsEnabled = true;
                ColEl_SelectionChanged(true, null);

                OptDim.IsChecked = false;

                if (m_Otv == 1)
                {
                    m3 = 1;
                    Change();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        // Радиусы
        private void Rad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (m3 == 0) // Программа
                {
                    m_Rad = 0;
                }
                else
                {
                    m_Rad = 1; // Пользователь
                }

                m3 = 0;

                TxtPref.Visibility = Visibility.Hidden;
                TxtSuf.Visibility = Visibility.Hidden;
                ChkChamfer.IsEnabled = false;
                Faska.IsEnabled = false;
                ChkDop.IsEnabled = false;
                CboDop.IsEnabled = false;
                CboStep.IsEnabled = false;
                ChkStep.IsEnabled = false;
                ChkLeft.IsEnabled = false;
                ChkStar.IsEnabled = true;
                ChkStar_Click(true, null);
                ChkDouble.IsEnabled = true;
                ChkDouble_Click(true, null);
                ColEl.IsEnabled = true;
                ColEl_SelectionChanged(true, null);

                OptDim.IsChecked = false;

                if (m_Rad == 1)
                {
                    m3 = 1;
                    Change();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        // Пазы
        private void OptPaz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (m3 == 0) // Программа
                {
                    m_OptPaz = 0;
                }
                else
                {
                    m_OptPaz = 1; // Пользователь
                }
                m3 = 0;

                TxtPref.Visibility = Visibility.Hidden;
                TxtSuf.Visibility = Visibility.Hidden;

                if (Convert.ToInt32(ColEl.Text) < 2)
                {
                    ColEl.Text = "2";
                }

                ChkChamfer.IsEnabled = false;
                Faska.IsEnabled = false;
                ChkDop.IsEnabled = false;
                CboDop.IsEnabled = false;
                CboStep.IsEnabled = false;
                ChkStep.IsEnabled = false;
                ChkLeft.IsEnabled = false;
                ChkStar.IsEnabled = true;
                ChkStar_Click(true, null);
                ChkDouble.IsEnabled = true;
                ChkDouble_Click(true, null);
                ColEl.IsEnabled = true;
                ColEl_SelectionChanged(true, null);

                OptDim.IsChecked = false;

                if (m_OptPaz == 1)
                {
                    m3 = 1;
                    Change();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        // Выступы
        private void OptExt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (m3 == 0)
                {
                    m_OptExt = 0;
                }
                else
                {
                    m_OptExt = 1;
                }
                m3 = 0;

                TxtPref.Visibility = Visibility.Hidden;
                TxtSuf.Visibility = Visibility.Hidden;
                if (Convert.ToInt32(ColEl.Text) < 2)
                {
                    ColEl.Text = "2";
                }
                ChkChamfer.IsEnabled = false;
                Faska.IsEnabled = false;
                ChkDop.IsEnabled = false;
                CboDop.IsEnabled = false;
                CboStep.IsEnabled = false;
                ChkStep.IsEnabled = false;
                ChkLeft.IsEnabled = false;
                ChkStar.IsEnabled = true;
                ChkStar_Click(true, null);
                ChkDouble.IsEnabled = true;
                ChkDouble_Click(true, null);
                ColEl.IsEnabled = true;
                ColEl_SelectionChanged(true, null);

                OptDim.IsChecked = false;

                if (m_OptExt == 1)
                {
                    m3 = 1;
                    Change();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        // Резьбовые отверстия
        private void RezOvt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (m3 == 0) // Программа
                {
                    m_RezOvt = 0;
                }
                else
                {
                    m_RezOvt = 1; // Пользователь
                }

                m3 = 0;

                TxtPref.Visibility = Visibility.Hidden;
                TxtSuf.Visibility = Visibility.Hidden;
                ChkChamfer.IsEnabled = true;
                ChkChamfer_Click(true, null);
                ChkDop.IsEnabled = true;
                ChkDop_Click(true, null);
                ChkStep.IsEnabled = true;
                ChkStep_Click(true, null);
                ChkLeft.IsEnabled = true;
                ChkLeft_Click(true, null);
                ChkStar.IsEnabled = true;
                ChkStar_Click(true, null);
                ChkDouble.IsEnabled = true;
                ChkDouble_Click(true, null);
                ColEl.IsEnabled = true;
                ColEl_SelectionChanged(true, null);

                OptDim.IsChecked = false;

                if (m_RezOvt == 1)
                {
                    m3 = 1;
                    Change();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        // Массив
        private void Massiv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (m3 == 0) // Программа
                {
                    m_Massiv = 0;
                }
                else
                {
                    m_Massiv = 1; // Пользователь
                }
                m3 = 0;

                TxtPref.Visibility = Visibility.Hidden;
                TxtSuf.Visibility = Visibility.Hidden;

                var numArr = Convert.ToInt32(ColEl.SelectedValue);

                if (numArr < 3)
                {
                    ColEl.Text = "3";
                }

                ChkChamfer.IsEnabled = false;
                Faska.IsEnabled = false;
                ChkDop.IsEnabled = false;
                CboDop.IsEnabled = false;
                CboStep.IsEnabled = false;
                ChkStep.IsEnabled = false;
                ChkLeft.IsEnabled = false;
                ChkStar.IsEnabled = true;
                ChkStar_Click(true, null);
                ColEl.IsEnabled = true;
                ColEl_SelectionChanged(true, null);
                ChkDouble.IsChecked = false;
                ChkDouble.IsEnabled = false;

                OptDim.IsChecked = false;

                if (m_Massiv == 1)
                {
                    m3 = 1;
                    Change();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        // Места
        private void OptPlace_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (m3 == 0) // Программа
                {
                    m_OptPlace = 0;
                }
                else
                {
                    m_OptPlace = 1; // Пользователь
                }
                m3 = 0;
                TxtPref.Visibility = Visibility.Hidden;
                TxtSuf.Visibility = Visibility.Hidden;
                if (Convert.ToInt32(ColEl.Text) < 2)
                {
                    ColEl.Text = "2";
                }
                ChkChamfer.IsEnabled = false;
                Faska.IsEnabled = false;
                ChkDop.IsEnabled = false;
                CboDop.IsEnabled = false;
                CboStep.IsEnabled = false;
                ChkStep.IsEnabled = false;
                ChkLeft.IsEnabled = false;
                ChkStar.IsEnabled = true;
                ChkStar_Click(true, null);
                ChkDouble.IsEnabled = true;
                ChkDouble_Click(true, null);
                ColEl.IsEnabled = true;
                ColEl_SelectionChanged(true, null);

                OptDim.IsChecked = false;

                if (m_OptPlace == 1)
                {
                    m3 = 1;
                    Change();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        // Простой размер
        private void OptDim_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (m3 == 0)
                {
                    m_OptDim = 0;
                }
                else
                {
                    m_OptDim = 1;
                }
                m3 = 0;

                TxtPref.Visibility = Visibility.Visible;
                TxtSuf.Visibility = Visibility.Visible;
                ChkChamfer.IsEnabled = false;
                Faska.IsEnabled = false;
                ChkDop.IsEnabled = false;
                CboDop.IsEnabled = false;
                CboStep.IsEnabled = false;
                ChkStep.IsEnabled = false;
                ChkLeft.IsEnabled = false;
                ChkStar.IsEnabled = false;
                CboStar.IsEnabled = false;
                ColEl.SelectedIndex = 0;
                ColEl.IsEnabled = false;
                ChkDouble.IsEnabled = true;

                Fask.IsChecked = false;
                Rad.IsChecked = false;
                OptPaz.IsChecked = false;
                OptExt.IsChecked = false;
                OptPlace.IsChecked = false;
                Massiv.IsChecked = false;

                if (Convert.ToInt32(varTemp) > 0)
                {
                    ChkDouble.IsChecked = true;
                }
                else
                {
                    ChkDouble.IsChecked = false;
                }
                if (m_OptDim == 1)
                {
                    m3 = 1;
                    Change();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        // Количество
        private void ColEl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (m3 == 0) // Программа
                {
                    m_ColEl = 0;
                }
                else
                {
                    m_ColEl = 1; // Пользователь
                }
                m3 = 0;
                if (OptDim.IsChecked == true)
                {
                    ChkDouble.IsEnabled = true;
                }
                else
                {
                    if (ColEl.SelectedIndex == 0 | Massiv.IsChecked == true)
                    {
                        ChkDouble.IsChecked = false;
                        ChkDouble.IsEnabled = false;
                    }
                    else
                    {
                        ChkDouble.IsEnabled = true;
                    }
                }
                ChkDouble_Click(true, null);

                if (m_ColEl == 1)
                {
                    m3 = 1;
                    Change();
                }

                Debug.Print("ColEl_SelectionChanged value: " + ColEl.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - " + ex.StackTrace);
            }


        }
        // В две строки
        private void ChkDouble_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ChkDouble.IsChecked == true & OptDim.IsChecked == true)
                {
                    TxtUser.Visibility = Visibility.Visible;
                    LblSecond.Visibility = Visibility.Hidden;
                }
                else
                {
                    TxtUser.Visibility = Visibility.Hidden;
                    LblSecond.Visibility = Visibility.Visible;
                }

                if (m3 == 1)
                {
                    Change();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - " + ex.StackTrace);
            }
        }
        // Фаска отверстий
        private void ChkChamfer_Click(object sender, RoutedEventArgs e)
        {
            if (ChkChamfer.IsChecked == false)
            {
                Faska.IsEnabled = false;
            }
            else
            {
                Faska.IsEnabled = true;
            }
            if (m3 == 1)
            {
                Change();
            }
        }
        // Фаска отверстий
        private void Faska_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ChkChamfer.IsChecked == false)
                {
                    Faska.IsEnabled = false;
                }
                else
                {
                    Faska.IsEnabled = true;
                }

                if (m3 == 1)
                {
                    Change();
                }
                Debug.Print("Faska_SelectionChanged value: " + Faska.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        // Список допусков резьбы
        private void CboDop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (m3 == 1)
                {
                    Change();
                }
                Debug.Print("CboDop_SelectionChanged value: " + CboDop.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        // Допуск резьбы
        private void ChkDop_Click(object sender, RoutedEventArgs e)
        {
            if (ChkDop.IsChecked == false)
            {
                CboDop.IsEnabled = false;
            }
            else
            {
                CboDop.IsEnabled = true;
            }
            if (m3 == 1)
            {
                Change();
            }
        }
        // Левая резьба
        private void ChkLeft_Click(object sender, RoutedEventArgs e)
        {
            if (m3 == 1)
            {
                Change();
            }
        }
        // Мелкий шаг
        private void ChkStep_Click(object sender, RoutedEventArgs e)
        {
            if (ChkStep.IsChecked == false)
            {
                CboStep.IsEnabled = false;
            }
            else
            {
                CboStep.IsEnabled = true;
            }
            if (m3 == 1)
            {
                Change();
            }
        }
        // Список мелких шагов
        private void CboStep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (m3 == 1)
                {
                    Change();
                }
                Debug.Print("CboStep_SelectionChanged value: " + CboStep.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        // Сместить размер
        private void ChkDis_Click(object sender, RoutedEventArgs e)
        {
            if (ChkDis.IsChecked == true)
            {
                ChkHor.IsChecked = false;
            }
        }
        // Горизонтально
        private void ChkHor_Click(object sender, RoutedEventArgs e)
        {
            if (ChkHor.IsChecked == true)
            {
                ChkDis.IsChecked = false;
            }
        }
        // Список *
        private void CboStar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (m3 == 1)
                {
                    Change();
                }
                Debug.Print("CboStep_SelectionChanged value: " + CboStep.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        // *
        private void ChkStar_Click(object sender, RoutedEventArgs e)
        {
            if (ChkStar.IsChecked == true)
            {
                CboStar.IsEnabled = true;
            }
            else
            {
                CboStar.IsEnabled = false;
            }

            if (m3 == 1)
            {
                Change();
            }
        }
        private void TxtPref_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            m4 = 1;
        }
        private void TxtSuf_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            m4 = 2;
        }
        private void TxtUser_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            m4 = 3;
        }
        void Cmd(char var)
        {
            try
            {
                if (m4 == 1)
                {
                    str1 = Strings.Left(TxtPref.Text, TxtPref.SelectionStart);
                    str2 = Strings.Right(TxtPref.Text, Strings.Len(TxtPref) - TxtPref.SelectionStart);
                    TxtPref.Text = str1 + (char)var + str2;
                    TxtPref.Focus();
                }
                else if (m4 == 2)
                {
                    str1 = Strings.Left(TxtSuf.Text, TxtSuf.SelectionStart);
                    str2 = Strings.Right(TxtSuf.Text, Strings.Len(TxtSuf.Text) - TxtSuf.SelectionStart);
                    TxtSuf.Text = str1 + (char)var + str2;
                    TxtSuf.Focus();
                }
                else if (m4 == 3)
                {
                    str1 = Strings.Left(TxtUser.Text, TxtUser.SelectionStart);
                    str2 = Strings.Right(TxtUser.Text, Strings.Len(TxtUser) - TxtUser.SelectionStart);
                    TxtUser.Text = str1 + (char)var + str2;
                    TxtUser.Focus();
                }
                else
                {
                    MessageBox.Show("Укажите место вставки символа");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - " + ex.StackTrace);
            }
        }
        private void CmdDeg_Click(object sender, RoutedEventArgs e)
        {
            Cmd((char)176);
        }
        private void CmdDiam_Click(object sender, RoutedEventArgs e)
        {
            Cmd((char)0xd8);         
        }
        private void CmdPM_Click(object sender, RoutedEventArgs e)
        {
            Cmd((char)177);
        }
        private void CmdSq_Click(object sender, RoutedEventArgs e)
        {
            Cmd((char)0x25a1);
        }
        // Округление
        private double MyRound(double RoundValue, long PrecValue)
        {
            try
            {
                int PrecValueInt;
                double Delta;
                int j;
                PrecValue = Math.Abs(PrecValue);
                PrecValueInt = (int)PrecValue;
                j = 1;



                for (i = 1; i <= PrecValueInt; i++)
                {
                    RoundValue = RoundValue * 10;
                    j = j * 10;
                }

                Delta = RoundValue - (int)(RoundValue);

                if (Delta >= 0.5)
                {
                    RoundValue = (int)(RoundValue);
                    RoundValue = RoundValue + 1;
                }
                else
                {
                    RoundValue = (int)(RoundValue);
                }

                RoundValue = RoundValue / j;

             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - " + ex.StackTrace);
            }
            return RoundValue;
        }
        private void TogButFormat_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TogButFormat.IsChecked == true) // Нажата
                {
                    m5 = 1;
                    CommandButton1_Click(true, null);
                    CommandButton1.IsEnabled = false;
                    CmdClose.IsEnabled = false;
                    CmdCancel.IsEnabled = false;
                }
                else // Отжата
                {
                    m5 = 0;
                    CommandButton1.IsEnabled = true;
                    CmdClose.IsEnabled = true;
                    CmdCancel.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        private void FormRecordDimM_Activated(object sender, EventArgs e)
        {
            UserForm_Activate();
        }
        private void FormRecordDimM_Deactivated(object sender, EventArgs e)
        {
            try
            {
                //if (PresentationSource.FromVisual(this) == null) return;
                //var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
                //var mouse = transform.Transform(GetMousePosition());
                //LeftMouseClick((int)Left + 100, (int)Top + 100);
                //LeftMouseClick((int)Left + 100, (int)Top + 100);
                FormRecordDimM_Activated(sender, null);
                //SetCursorPos((int)mouse.X, (int)mouse.Y);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        #region Mouse event
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            static extern bool SetCursorPos(int x, int y);
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
            public const int MOUSEEVENTF_LEFTDOWN = 0x02;
            public const int MOUSEEVENTF_LEFTUP = 0x04;
            //This simulates a left mouse click
            public static void LeftMouseClick(int xpos, int ypos)
            {
                SetCursorPos(xpos, ypos);
                mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
            }
            // get current mouse coordination
            public Point GetMousePosition()
            {
                var point = System.Windows.Forms.Control.MousePosition;
                return new Point(point.X, point.Y);
            }
        #endregion
    }
}