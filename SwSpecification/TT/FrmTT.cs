using Microsoft.VisualBasic;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SwSpecification.TT
{
    public partial class FrmTT : Form
    {
        public FrmTT()
        {
            try
            {
                InitializeComponent();
                CmdOk.GotFocus += new EventHandler(CmdOk_GotFocus);
                CmdCancel.GotFocus += new EventHandler(CmdCancel_GotFocus);
                CmdSet.GotFocus += new EventHandler(CmdSet_GotFocus);
                ChkAlign.GotFocus += new EventHandler(ChkAlign_GotFocus);
                CboTTType.GotFocus += new EventHandler(CboTTType_GotFocus);
                ChkAutoN.GotFocus += new EventHandler(ChkAutoN_GotFocus);
                ChkList.GotFocus += new EventHandler(ChkList_GotFocus);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; \n" + ex.StackTrace);
            }
        }
        #region VARIABLES      
            SldWorks swApp; // Объект - SolidWorks
            ModelDoc2 swModel; // Объект -  открытый документ
            DrawingDoc swDraw;
            Note swNote; // Объект - заметка
            Annotation swAnn; // Объект - аннотация
            Sheet swSheet; // Объект - страница
            SelectionMgr swSelMgr; // Объект - выделенный объект в СВ
            //SolidWorks.Interop.sldworks.View swView; // Объект - вид
            string[,] TT = new string[51, 501]; // Массив - значения ТТ
            int[] TTQuantity; // Переменная - число
            int i; // Счетчик
            int j; // Счетчик
            int i1; // Счетчик
            int j1; // Счетчик
            int i2; // Счетчик
            int j2; // Счетчик
            int i3; // Счетчик
            int i4; // Счетчик
            int k; // Счетчик
            int m1; // Метка обхода процедуры автонумерации
            int m2; // Метка источника данных ТТ
            int m3; // Метка проверка языка для тт
            string strTemp;
            string strActiveSheetName;
            bool ok;
            string[] N; // Массив - номера строк
            int RowQuantity; // Переменная - текущее число строк
            int RowQuantitySet; // Переменная - необходимое число строк
            int RowQuantityMax; // Константа - максимальное число строк
            string NName; // Переменная - Имя свойства
            string TTName; // Переменная - Имя свойства
            string TTNote; // Переменная - текст прочитанной заметки
            string TTFileString; // Переменная - текст строки, прочитанной из файла
            int TTNoteLength; // Переменная - длина прочитанной заметки
            int TTNoteStringLength; // Переменная - длина строки
            string newNoteName; // Константа - имя заметки с ТТ
            string oldNoteName; // Переменная - имя старой заметки с ТТ
            bool boolstatus; // логическая переменная
            long longstatus; // логическая переменная
            string Symbol; // Переменная - один символ строки ТТ
            string TopString; // Пременная - верхняя строка при сортировке
            int LostIndex; // Переменная - индекс потерявшей фокус строки
            object vSheetProps; // Переменная - свойства страницы
            double NoteSetWidth; // Переменная - Координата заметки с ТТ
            double NoteSetHeidth; // Переменная - Координата заметки с ТТ      
            string[] TTNoteString; // Массив переменных - строки прочитанной заметки
            string AnalyserString; // Переменная - строка для анализа
            string TempString; // Переменная - строка для временного хранения данных
            string TempStringNew; // Переменная - строка для временного хранения данных
            int ChkValue; // Переменная - положение переключателя номера строки
            int ListMark; // Метка наличия списка
            int ParaMark; //  Метка наличия форматирования 2005
            object ListPos; //  Метка наличия элемента в списке координата его начала
            object ListSpace; // Координата первого пробела после нужного элемента
            int ListNumber; // Метка  наличия списка
            int ListIndent; // Переменная - отступ
            int ListFindent; // Переменная - отступ первой строки
            int CboNListIndex; // Переменная - временно хранит номер списка
            double Xini; // Коррекция точки вставки
            double Yini; // Коррекция точки вставки
            const int swDocNONE = 0;
            const int swDocPART = 1;
            const int swDocASSEMBLY = 2;
            const int swDocDRAWING = 3;
            const int swSelNOTES = 15;
        #endregion
        #region Controls
            #region ChkN    
            List<CheckBox> ChkN = new List<CheckBox>();
            object sChkN;
            void chkN()
            {
                var chk = new CheckBox();
                chk.Width = 15;
                chk.Click += new EventHandler(ChkN_Click);
                chk.GotFocus += new EventHandler(ChkN_GotFocus);
                flp.Controls.Add(chk);
                ChkN.Add(chk);
            }
            #endregion
            #region CboN
            List<ComboBox> CboN = new List<ComboBox>();
            object sN;
            void cboN()
            {
                try
                {
                    var combo = new ComboBox();
                    combo.Enter += new System.EventHandler(comboN_Enter);

                    int n = flp.Controls.IndexOf((sN as ComboBox)) + 1;
                    var c = new Control[flp.Controls.Count - n];

                    combo.Width = 45;

                    flp.Controls.Add(combo);
                    flp.Controls.AddRange(c);
                    CboN.Add(combo);
                    combo.GotFocus += new System.EventHandler(comboN_GotFocus);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "; " + ex.StackTrace);
                }
            }
            private void comboN_Enter(object sender, EventArgs e)
            {
                sN = sender;
            }
            #endregion
            #region CboTT      
            List<ComboBox> CboTT = new List<ComboBox>();
            object sT;
            void cboTT()
            {
                try
                {
                    var combo = new ComboBox();
                    combo.Enter += new System.EventHandler(this.comboT_Enter);
                    combo.Width = 340;

                    flp.Controls.Add(combo);
                    CboTT.Add(combo);

                    combo.LostFocus += new EventHandler(comboT_LostFocus);
                    combo.GotFocus += new EventHandler(comboT_GotFocus);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "; " + ex.StackTrace);
                }
            }
            private void comboT_Enter(object sender, EventArgs e)
            {
                sT = sender;
            }
            #endregion
        #endregion
        private void FrmTT_Load(object sender, EventArgs e)
        {
            try
            {
                chkN();
                cboN();
                cboTT();
                //Start();

                CboChangeLang.Items.Add("Рус. ТТ");
                CboChangeLang.Items.Add("Укр. ТВ");
                CboChangeLang.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        Array arrStrTT()
        {
            string[] arrStr = null;

            switch (m3)
            {
                case 0:
                    arrStr = new string[] {
                    "$$$Общие ТТ",
                    "*Размеры для справок.",
                    "*Размер для справок.",
                    "Размеры для справок.",
                    "Размер для справок.",
                    "Н14, h14, ±t2 / 2.",
                    "Н12, h12, ±t1 / 2.",
                    "Неуказанные предельные отклонения размеров Н14, h14, ±t2 / 2.",
                    "Неуказанные предельные отклонения размеров Н12, h12, ±t1 / 2.",
                    "Неуказанные радиусы скруглений R",
                    "На поверхности А дефекты не допускаются.",
                    "Размер выполнить по месту.",
                    "Размер выполнить плотно по ответной детале", "Пескоструить",

                    "$$$Сварка, пайка, склеивание",
                    "Сварные швы по ГОСТ.",
                    "Швы вакуумно-плотные.",

                    "$$$Покрытия",
                    "Покрытие: Хим.Окс.прм.",
                    "Покрытие: Ан.Окс.черн." };
                    break;
                case 1:
                    arrStr = new string[] {
                    "$$$Загальні ТВ",
                    "*Розміри для довідок.",
                    "*Розмір для довідок.",
                    "Розміри для довідок.",
                    "Розмір для довідок.",
                    "Н14, h14, ±J14/2.",
                    "Н12, h12, ±J12/2.",
                    "Не вказані граничні відхилення Н14, h14, ±Jt14/2.",
                    "Не вказані граничні відхилення Н12, h12, ±Jt12/2.",
                    "() Розміри в дужках - для гнуття.",
                    "Гострі кромки притупити R0,5.",
                    "«А» - лицьова поверхня.",
                    "Масу уточнити після виготовлення дослідного зразку.",

                    "$$$Зварювання, пайка, склеювання",
                    "Зварні шви по ГОСТ 14771-76.",
                    "Зварні шви по ГОСТ 15878-79.",
                    "Поверхню зварювання зачистити до металевого блиску.",
                    "Зварні шви зачистити з плавним переходом.",
                    "Зварні шви зачистити до основного металу.",
                    "Клей \"Цеанопан\"",
                    "Шви виконати ниткою типу «Анна» ПЕ №40 S/2, ОСТ 17-303-84, колір – білий.",
                    "Клепати по \"И1-01\".",
                    "Кути герметизувати герметиком.",

                    "$$$Покриття",
                    "Поверхню для покриття зачистити від іржавчини.",
                    "Виключити попадання краски на різьбову поверхню.",
                    "Покриття: колір $PRPSHEET:\"RAL\", клас покриття - $PRPSHEET:\"Класс покрытия\", $PRPSHEET:\"Тип покрытия\".",
                    "Покриття: колір $PRPSHEET:\"RAL\".",
                    "Поверхні «Л» виконати фарбування по класу 2, не зазначені поверхні по класу 5.",
                    "Площа покриття $PRPSHEET:\"Площадь покрытия\" м2."};
                    break;
            }
            return arrStr;
        }

        void Start()
        {
            try
            {
                swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
                swModel = swApp.ActiveDoc;

                RowQuantityMax = 25;
                newNoteName = "TT";
                m1 = 0; // автонумерации включена;
                ListNumber = 0;
                ListIndent = 3;
                //ListFindent = -5;
                //ListMark = 0; // Списка нет
                ParaMark = 0; // Форматирования нет

                Xini = 0 / 1000;
                Yini = 0 / 1000;

                // Заполнение массива номеров
                Array.Resize(ref N, RowQuantityMax + 3);
                N[1] = "";
                N[2] = "->";
                for (i = 1; i <= RowQuantityMax; i++)
                {
                    N[i + 2] = Strings.LTrim(i.ToString() + ". ");
                }

                #region Чтение ТТ
                // Чтение ТТ
                CboTTType.Items.Clear();
                i = 0;

                // Заполняем ТТ в зависимости от выбранного языка
                foreach (var str in arrStrTT())
                {
                    TTFileString = str.ToString();
                    if (Strings.Left(TTFileString, 3) == "$$$")  // Проверка, является ли строка заголовком раздела
                    {
                        k = Strings.Len(TTFileString);
                        CboTTType.Items.Add(Strings.Right(TTFileString, k - 3));
                        i = i + 1;
                        j = 1;
                    }
                    else
                    {
                        TT[i, j] = TTFileString;
                        j = j + 1;
                        Array.Resize(ref TTQuantity, i + 1);
                        TTQuantity[i] = j - 1;
                        //Debug.Print(TTQuantity[i].ToString());
                    }
                }

                //// Чтение файла ТТ
                //using (StreamReader SR = new StreamReader(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Source" + @"\TT.txt", Encoding.GetEncoding(1251)))
                //{
                //    while ((line = SR.ReadLine()) != null)
                //    {
                //    }
                //}

                CboTTType.SelectedIndex = 0;
                #endregion

                // Проверка формы
                RowQuantity = CboN.Count;
                while (RowQuantity > 1)
                {
                    CboUnLoad();
                    RowQuantity = CboN.Count;
                }

                // Заполнение первой строки
                CboN[0].Items.Clear();
                for (i = 1; i <= RowQuantityMax + 2; i++)
                {
                    CboN[0].Items.Add(N[i].ToString());
                }

                CboTT[0].Items.Clear();
                for (j = 1; j <= TTQuantity[1]; j++)
                {
                    CboTT[0].Items.Add(TT[1, j].ToString());
                }

                // Чтение заметки ТТ
                TTNote = "";
                m2 = 0; // ТТ не прочитаны
                swSelMgr = swModel.SelectionManager;

                swDraw = (DrawingDoc)swModel;
                swSheet = swDraw.GetCurrentSheet();
                strActiveSheetName = swSheet.GetName();
                strTemp = "TT@" + strActiveSheetName;
                //Debug.Print(strTemp);
                ok = swModel.Extension.SelectByID2(strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);
                if (ok)
                {
                    swNote = swSelMgr.GetSelectedObject2(1);
                    TTNote = swNote.PropertyLinkedText;
                    m2 = 1; // ТТ прочитаны из заметки
                }

                #region Анализ прочитанной заметки
                // Анализ прочитанной заметки
                if (m2 == 1)
                {
                    TTNoteLength = Strings.Len(TTNote);

                    // Разбивка на строки
                    j = 1;
                    Array.Resize(ref TTNoteString, j + 1);
                    TTNoteString[j] = "";

                    for (k = 1; k < TTNoteLength; k++)
                    {
                        Symbol = Strings.Mid(TTNote, k, 1);
                        if (Symbol == Convert.ToString(Strings.Chr(10)))
                        {
                            j = j + 1;
                            Array.Resize(ref TTNoteString, j + 1);
                            TTNoteString[j] = "";
                        }
                        else if (Symbol == Convert.ToString(Strings.Chr(13))) { }
                        else
                        {
                            TTNoteString[j] = TTNoteString[j] + Symbol;
                        }
                    }
                    // Анализ строк
                    for (k = 1; k <= j; k++)
                    {

                        RowQuantity = CboN.Count;
                        AnalyserString = TTNoteString[k];
                        Analyser1();
                        TTNoteString[k] = AnalyserString;
                        TTNoteStringLength = Strings.Len(TTNoteString[k]);

                        if (ParaMark == 0)
                        {
                            if (ListNumber == 1)
                            {
                                CboN[RowQuantity - 1].SelectedIndex = 0; // Пусто
                                CboTT[RowQuantity - 1].Text = TTNoteString[k];
                            }
                            else
                            {
                                CboN[RowQuantity - 1].SelectedIndex = 1; // Табуляция
                                CboTT[RowQuantity - 1].Text = TTNoteString[k];
                            }
                        }
                        else
                        {
                            if (Strings.Left(TTNoteString[k], 1) == " ") // Пробел в начале
                            {
                                if (Strings.Left(TTNoteString[k], 3) == "   ") // Три пробела в начале
                                {
                                    CboN[RowQuantity - 1].SelectedIndex = 1; // Табуляция
                                    CboTT[RowQuantity - 1].Text = Strings.Right(TTNoteString[k], TTNoteStringLength - 3);
                                }
                                else
                                {
                                    CboN[RowQuantity - 1].SelectedIndex = 0; // Пусто
                                    CboTT[RowQuantity - 1].Text = TTNoteString[k];
                                }
                            }
                            else
                            {
                                if (Strings.Mid(TTNoteString[k], 2, 1) == ".") // Есть точка во втором знаке
                                {
                                    AnalyserString = Strings.Left(TTNoteString[k], 2) + " ";
                                    Analyser3();
                                    CboN[RowQuantity - 1].SelectedIndex = CboNListIndex;
                                    if (CboNListIndex == 0)
                                    {
                                        CboTT[RowQuantity - 1].Text = TTNoteString[k];
                                    }
                                    else
                                    {
                                        if (Strings.Mid(TTNoteString[k], 4, 1) == " ") // Есть пробел после точки
                                        {
                                            CboTT[RowQuantity - 1].Text = Strings.Right(TTNoteString[k], TTNoteStringLength - 3);
                                        }
                                        else // Нет пробела после точки
                                        {
                                            CboTT[RowQuantity - 1].Text = Strings.Right(TTNoteString[k], TTNoteStringLength - 2);
                                        }
                                    }
                                }
                                else // Нет точки в третьем знаке
                                {
                                    if (Strings.Mid(TTNoteString[k], 3, 1) == ".") // Есть точка в третьем знаке
                                    {
                                        AnalyserString = Strings.Left(TTNoteString[k], 3) + " ";
                                        Analyser3();
                                        CboN[RowQuantity - 1].SelectedIndex = CboNListIndex;
                                        if (CboNListIndex == 0)
                                        {
                                            CboTT[RowQuantity - 1].Text = TTNoteString[k];
                                        }
                                        else
                                        {
                                            if (Strings.Mid(TTNoteString[k], 4, 1) == " ") // Есть пробел после точки
                                            {
                                                CboTT[RowQuantity - 1].Text = Strings.Right(TTNoteString[k], TTNoteStringLength - 4);
                                            }
                                            else // Нет пробела после точки
                                            {
                                                CboTT[RowQuantity - 1].Text = Strings.Right(TTNoteString[k], TTNoteStringLength - 3);
                                            }
                                        }
                                    }
                                    else // Нет точки в третьем знаке
                                    {
                                        CboN[RowQuantity - 1].SelectedIndex = 0; // Пусто
                                        CboTT[RowQuantity - 1].Text = TTNoteString[k];
                                    }
                                }
                            }
                        }
                        if (j - k != 0) // Проверка необходимости добавить строку
                        {
                            CboLoad();
                        }
                    }
                }
                else // Заметки не существует
                {
                    // Проверка свойства RowQuantity
                    if (swModel.CustomInfo["RowQuantity"] != "")
                    {
                        RowQuantitySet = Convert.ToInt32(swModel.CustomInfo["RowQuantity"]);
                    }
                    else
                    {
                        RowQuantitySet = 0;
                        k = 1;
                        // Определение числа строк, заполненных старой версией
                        while (k == 8)
                        {
                            TTName = "TT" + Strings.LTrim(k.ToString());
                            if (swModel.CustomInfo[TTName] == "")
                            {
                                break;
                            }
                            RowQuantitySet = RowQuantitySet + 1;
                            k = k + 1;
                        }
                    }
                    if (RowQuantitySet != 0)
                    {

                        m2 = 2; // ТТ прочитаны из свойств
                        for (k = 1; k <= RowQuantitySet; k++) // Чтение из свойств
                        {
                            NName = "N" + Strings.LTrim(k.ToString());
                            TTName = "TT" + Strings.LTrim(k.ToString());
                            AnalyserString = swModel.CustomInfo[TTName];
                            Analyser1();
                            TempString = AnalyserString;
                            if (ParaMark == 1)
                            {
                                if (ListNumber == 1)
                                {
                                    CboN[k - 1].SelectedIndex = 0; // Пусто
                                }
                                else
                                {
                                    CboN[k - 1].SelectedIndex = 1; // Табуляция
                                }
                            }
                            else
                            {
                                if (swModel.CustomInfo[NName] == "   ") // Три пробела в начале
                                {
                                    CboN[k - 1].SelectedIndex = 1; // Табуляция
                                }
                                else
                                {
                                    AnalyserString = swModel.CustomInfo[NName];
                                    Analyser3();
                                    CboN[k - 1].SelectedIndex = CboNListIndex;
                                }
                            }

                            CboTT[k - 1].Text = TempString;
                            if (RowQuantitySet - k != 0)
                            {
                                CboLoad();
                            }
                        }
                    }
                }
                #endregion

                CmdAdd.Enabled = false;
                CmdDel.Enabled = false;
                CmdUp.Enabled = false;
                CmdDown.Enabled = false;
                CmdDiam.Enabled = false;
                CmdDeg.Enabled = false;
                CmdPM.Enabled = false;
                ChkAlign.Checked = Convert.ToBoolean(1);

                RowQuantity = CboN.Count;
                ChkList.Checked = ParaMark == 1 ? Convert.ToBoolean(1) : Convert.ToBoolean(0);

                if (m2 == 0) // Включение списка если ТТ не было
                {
                    ChkList.Checked = Convert.ToBoolean(1);
                }

                //TODO: ChkAutoN
                ChkAutoN.Checked = true;
                ChkAutoN_Click(true, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
                Close();
            }
        }
        private void CmdOk_Click(object sender, EventArgs e) // OK
        {
            try
            {
                // Предварительное удаление свойств из файла
                ok = swModel.DeleteCustomInfo2("", RowQuantity.ToString());
                for (i = 1; i <= RowQuantityMax; i++)
                {
                    NName = "N" + Strings.LTrim(Convert.ToString(i));
                    TTName = "TT" + Strings.LTrim(Convert.ToString(i));
                    ok = swModel.DeleteCustomInfo2("", NName);
                    ok = swModel.DeleteCustomInfo2("", TTName);
                }
                RowQuantity = CboN.Count;

                // Заполнение свойств файла и текста заметки
                TTNote = "";

                for (i = 1; i <= RowQuantity; i++)
                {
                    NName = "N" + Strings.LTrim(Convert.ToString(i));
                    TTName = "TT" + Strings.LTrim(Convert.ToString(i));
                    AnalyserString = CboTT[i - 1].Text;
                    Analyser2();
                    TempString = AnalyserString;

                    if (ChkList.Checked == Convert.ToBoolean(1)) // Список включен
                    {
                        if (ChkN[i - 1].Checked == Convert.ToBoolean(1))
                        {
                            if (i - 1 != 0)
                            {
                                if (ChkN[i - 2].Checked != Convert.ToBoolean(1))
                                {
                                    TempStringNew = "<PARA  indent=1 findent=-7 number=on ntype=1 nformat=$$.>" + TempString;
                                }
                                else
                                {
                                    TempStringNew = TempString;
                                }
                            }
                            else
                            {
                                TempStringNew = "<PARA  indent=7 findent=-1 number=on ntype=1 nformat=$$.>" + TempString;
                            }
                        }
                        else
                        {
                            if (i - 1 != 0)
                            {
                                if (ChkN[i - 2].Checked != Convert.ToBoolean(0))
                                {
                                    TempStringNew = "<PARA  indent=1 findent=0 number=off>" + TempString;
                                }
                                else
                                {
                                    TempStringNew = TempString;
                                }
                            }
                            else
                            {
                                TempStringNew = "<PARA  indent=1 findent=0 number=off>" + TempString;
                            }
                        }

                        TTNote = TTNote + TempStringNew;

                        

                    }
                    else // Список выключен
                    {

                        TempStringNew = CboN[i - 1].Text;
                        if (TempStringNew == "->")
                        {
                            TempStringNew = "   ";
                        }
                        TTNote = TTNote + TempStringNew + TempString;
                    }
                    if (RowQuantity - i != 0)
                    {
                        TTNote = TTNote + (char)(10);
                    }
                    
                }

                //  Создание заметки
                if (m2 == 1)
                {
                    swNote.SetText(TTNote);
                    NoteAlign();
                }
                else
                {
                    swModel.ClearSelection2(true);
                    swDraw.EditSheet();
                    swNote = swModel.InsertNote(TTNote);
                    ok = swNote.SetName(newNoteName);
                    ChkAlign.Checked = Convert.ToBoolean(1);
                    NoteAlign();
                }

                swModel.ClearSelection2(true);
                swModel.WindowRedraw();

                Close();
                swApp = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        private void CmdCancel_Click(object sender, EventArgs e)
        {
            Close();
            swApp = null;
        }
        //TODO: CmdSet_Click
        private void CmdSet_Click(object sender, EventArgs e)
        {
            swSelMgr = swModel.SelectionManager;
            if (swSelMgr.GetSelectedObjectCount() == 1) // Проверка выделения
            {
                if (swSelMgr.GetSelectedObjectType(1) == 15)
                {
                    if (m2 == 1)
                    {
                        VBMath.Randomize();
                        oldNoteName = "NN" + Strings.LTrim(Convert.ToString(VBMath.Rnd() * 100));
                        //Debug.Print(oldNoteName);
                        ok = swNote.SetName(oldNoteName);
                    }

                    swNote = swSelMgr.GetSelectedObject2(1);
                    ok = swNote.SetName(newNoteName);
                    Start();
                }
                else
                { MessageBox.Show("Необходимо выделить заметку"); }
            }
            else
            { MessageBox.Show("Выделите одну заметку..."); }
        }
        private void NoteAlign()
        {
            if (ChkAlign.Checked == Convert.ToBoolean(1))
            {
                swNote.Angle = 0;
                boolstatus = swNote.SetBalloon(0, 0);
                swAnn = swNote.GetAnnotation();
                // Определение координат заметки
                swSheet = swDraw.GetCurrentSheet();
                vSheetProps = swSheet.GetProperties()[5];
                NoteSetWidth = Convert.ToDouble(vSheetProps) - 0.185 + Xini;
                NoteSetHeidth = 0.073 + RowQuantity * 0.0055 + Yini;
                longstatus = swAnn.SetLeader2(false, 0, true, true, false, false);
                boolstatus = swAnn.SetPosition(NoteSetWidth, NoteSetHeidth, 0);
            }
            else
            {
            }
        }
        private void ChkAutoN_Click(object sender, EventArgs e) // Включение\выключение автонумерации
        {
            RowQuantity = CboN.Count;
            m1 = 1; // автонумерации выключена
            for (i = 1; i <= RowQuantity; i++) // активизация\погашение переключателей нумерации строк и списков нумерации
            {
                if (ChkAutoN.Checked == Convert.ToBoolean(1))
                {
                    ChkN[i - 1].Enabled = true;
                    CboN[i - 1].Enabled = false;
                    if (CboN[i - 1].Text == "->")
                    { ChkN[i - 1].Checked = Convert.ToBoolean(0); }
                    else
                    { ChkN[i - 1].Checked = Convert.ToBoolean(1); }
                }
                else
                {
                    ChkN[i - 1].Enabled = false;
                    ChkN[i - 1].Checked = Convert.ToBoolean(0);
                    CboN[i - 1].Enabled = true;
                    ChkList.Checked = Convert.ToBoolean(0);
                }
            }
            m1 = 0; // автонумерации включена
            AutoN();
        }
        private void ChkList_Click(object sender, EventArgs e)
        {
            if (ChkList.Checked == true)
            {
                ChkAutoN.Checked = true;
            }
        }
        private void ChkN_Click(object sender, EventArgs e) // Включение\выключение переключателей нумерации строк
        {
            sChkN = sender;
            if (m1 != 1)
            { AutoN(); }
        }
        // Автонумерация
        void AutoN()
        {
            if (ChkAutoN.Checked == Convert.ToBoolean(1))
            {
                RowQuantity = CboN.Count;
                j2 = 0;
                m1 = 1;
                if ((RowQuantity == 1))
                {
                    CboN[0].SelectedIndex = 0;
                    ChkN[0].Checked = Convert.ToBoolean(0);
                    ChkN[0].Enabled = false;
                }
                else
                {
                    for (i2 = 1; (i2 <= RowQuantity); i2++)
                    {
                        if (ChkN[i2 - 1].Checked == Convert.ToBoolean(1))
                        {
                            j2 = (j2 + 1);
                            CboN[i2 - 1].SelectedIndex = j2 + 1;
                        }
                        else
                        {
                            CboN[i2 - 1].SelectedIndex = 1;
                        }
                    }
                }
                m1 = 0;
            }
        }
        void Analyser1()  // Анализатор входящего текста
        {
            AnalyserString = AnalyserString.Replace("<MOD-PM>", Convert.ToString((char)(177)));
            AnalyserString = AnalyserString.Replace("<MOD-DEG>", Convert.ToString((char)(176)));
            AnalyserString = AnalyserString.Replace("<MOD-DIAM>", Convert.ToString((char)(0xd8)));

            // Проверка на наличие форматирования 2005
            if (Strings.Left(AnalyserString, 5) == "<PARA")
            {
                ParaMark = 1; // Обнаружено форматирование
                ListPos = Strings.InStr(AnalyserString, "number=on");
                if ((int)ListPos > 0)
                { // Список включен
                    ListNumber = 1;
                    //ListMark = 1; // Обнаружен список
                }

                ListPos = Strings.InStr(AnalyserString, "number=off");
                if ((int)ListPos > 0)
                { // Список выключен
                    ListNumber = 0;
                }

                ListPos = Strings.InStr(AnalyserString, "number=restart");
                if ((int)ListPos > 0)
                { // Список рестартован
                    ListNumber = 2;
                }

                ListPos = Strings.InStr(AnalyserString, "indent=");
                if ((int)ListPos > 0)
                { // Есть отступ
                    ListNumber = 2;
                    TempString = "";
                    i3 = 1;
                    Symbol = "";

                    while (Symbol != " " & Symbol != ">")
                    {
                        TempString = TempString + Symbol;
                        Symbol = Strings.Mid(AnalyserString, (int)ListPos + 6 + i3, 1);
                        i3 = i3 + 1;
                    }
                    ListIndent = Convert.ToInt32(TempString);
                }

                ListPos = Strings.InStr(AnalyserString, "findent=");
                if ((int)ListPos > 0)
                { // Есть отступ первой строки
                    TempString = "";
                    i3 = 1;
                    Symbol = "";
                    while (Symbol != " " & Symbol != ">")
                    {
                        TempString = TempString + Symbol;
                        Symbol = Strings.Mid(AnalyserString, (int)ListPos + 7 + i3, 1);
                        i3 = i3 + 1;
                    }
                    ListIndent = Convert.ToInt32(TempString);
                }

                ListPos = Strings.InStr(AnalyserString, ">"); // Удаление данных форматирования
                TTNoteStringLength = Strings.Len(AnalyserString);
                AnalyserString = Strings.Right(AnalyserString, TTNoteStringLength - (int)ListPos);
            }
        }
        void Analyser2() // Анализатор исходящего текста
        {
            AnalyserString = AnalyserString.Replace(Convert.ToString((char)(177)), "<MOD-PM>");
            AnalyserString = AnalyserString.Replace(Convert.ToString((char)(176)), "<MOD-DEG>");
            AnalyserString = AnalyserString.Replace(Convert.ToString((char)(0xD8)), "<MOD-DIAM>");
        }
        void Analyser3() // Анализатор номера в начале строки
        {
            CboNListIndex = 0;
            for (i4 = 1; i4 < RowQuantityMax; i4++)
            {
                if (AnalyserString == N[i4 + 2]) { CboNListIndex = i4 + 1; }
            }
        }
        private void CboTTType_DropDownClosed(object sender, EventArgs e)
        {
            RowQuantity = CboN.Count;

            i = CboTTType.SelectedIndex + 1;

            for (k = 1; k <= RowQuantity; k++)
            {
                TempString = CboTT[k - 1].Text;
                CboTT[k - 1].Items.Clear();
                CboTT[k - 1].Text = TempString;
                for (j = 1; j <= TTQuantity[i]; j++)
                {
                    CboTT[k - 1].Items.Add(TT[i, j]);
                }
            }
        }
        void CboLoad() // Процедура загрузки новой строки
        {
            try
            {
                chkN();

                RowQuantity = CboN.Count;
                cboN();
                CboN[RowQuantity].Items.Clear();
                for (i1 = 1; i1 <= RowQuantityMax + 2; i1++)
                {
                    CboN[RowQuantity].Items.Add(N[i1].ToString());
                }

                CboN[RowQuantity].Top = CboN[RowQuantity - 1].Top + CboN[RowQuantity - 1].Height + 120;
                CboN[RowQuantity].Left = CboN[RowQuantity - 1].Left;
                CboN[RowQuantity].Visible = true;

                cboTT();
                CboTT[RowQuantity].Items.Clear();
                i1 = CboTTType.SelectedIndex + 1;
                for (j1 = 1; j1 <= TTQuantity[i1]; j1++)
                {
                    CboTT[RowQuantity].Items.Add(TT[i1, j1].ToString());
                }

                CboTT[RowQuantity].Top = CboN[RowQuantity].Top;
                CboTT[RowQuantity].Left = CboTT[RowQuantity - 1].Left;
                CboTT[RowQuantity].Visible = true;

                ChkN[RowQuantity].Top = CboN[RowQuantity].Top;
                ChkN[RowQuantity].Left = ChkN[RowQuantity - 1].Left;
                ChkN[RowQuantity].Visible = true;
                m1 = 1; // автонумерации выключена

                if (ChkAutoN.Checked == Convert.ToBoolean(1))
                {
                    ChkN[RowQuantity].Checked = Convert.ToBoolean(1);
                    ChkN[RowQuantity].Enabled = true;
                    CboN[RowQuantity].Enabled = false;
                    if (RowQuantity == 1)
                    {
                        ChkN[0].Enabled = true;
                        ChkN[0].Checked = Convert.ToBoolean(1);
                    }
                }
                else
                {
                    ChkN[RowQuantity].Checked = Convert.ToBoolean(0);
                    ChkN[RowQuantity].Enabled = false;
                    CboN[RowQuantity].Enabled = true;
                }

                m1 = 0; // автонумерации включена

                Height = Height + CboN[RowQuantity - 1].Height + 9;
                flp.Height = flp.Height + CboN[RowQuantity - 1].Height + 120;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        void CboUnLoad()  // Процедура удаления  строки
        {
            RowQuantity = CboN.Count;

            CboN[RowQuantity - 1].Dispose();
            CboN.RemoveAt(RowQuantity - 1);
            CboTT[RowQuantity - 1].Dispose();
            CboTT.RemoveAt(RowQuantity - 1);
            ChkN[RowQuantity - 1].Dispose();
            ChkN.RemoveAt(RowQuantity - 1);

            Height = Height - CboN[RowQuantity - 2].Height - 9;
            flp.Height = flp.Height - CboN[RowQuantity - 2].Height - 120;
        }
        #region Focus
        private void comboT_GotFocus(object sender, EventArgs e)
        {
            RowQuantity = CboN.Count;
            CmdUp.Enabled = true;
            CmdDown.Enabled = true;
            CmdAdd.Enabled = true;
            CmdDel.Enabled = true;
            CmdDiam.Enabled = true;
            CmdDeg.Enabled = true;
            CmdPM.Enabled = true;

            if (Index() == 0)
            {
                CmdUp.Enabled = false;
            }
            if (RowQuantity == 1)
            {
                CmdDel.Enabled = false;
            }
            if (Index() == RowQuantity - 1)
            {
                CmdDown.Enabled = false;
            }
            if (RowQuantity == RowQuantityMax)
            {
                CmdAdd.Enabled = false;
            }
        }
        private void comboT_LostFocus(object sender, EventArgs e) // Потеря фокуса строкой ТТ
        {
            LostIndex = Index();
        }
        int StoredIndex;
        int Index()
        {
            foreach (var obj in CboTT)
            {
                if (obj.Focused)
                {
                    StoredIndex = CboTT.IndexOf(obj);
                    break;
                }
            }
            //this.Controls[StoredIndex].Focus();
            return StoredIndex;
        }
        void comboN_GotFocus(object sender, EventArgs e)
        { CmdFalse(); }
        void ChkN_GotFocus(object sender, EventArgs e)
        { CmdFalse(); }
        void CmdOk_GotFocus(object sender, EventArgs e)
        {
            CmdFalse();
        }
        void CmdCancel_GotFocus(object sender, EventArgs e)
        {
            CmdFalse();
        }
        void CmdSet_GotFocus(object sender, EventArgs e)
        {
            CmdFalse();
        }
        void ChkAlign_GotFocus(object sender, EventArgs e)
        {
            CmdFalse();
        }
        void CboTTType_GotFocus(object sender, EventArgs e)
        {
            CmdFalse();
        }
        void ChkAutoN_GotFocus(object sender, EventArgs e)
        {
            CmdFalse();
        }
        void ChkList_GotFocus(object sender, EventArgs e)
        {
            CmdFalse();
        }
        void CmdFalse() // Скрытие кнопок управления строками
        {
            CmdUp.Enabled = false;
            CmdDown.Enabled = false;
            CmdAdd.Enabled = false;
            CmdDel.Enabled = false;
            CmdDiam.Enabled = false;
            CmdDeg.Enabled = false;
            CmdPM.Enabled = false;
        }
        #endregion
        private void CmdUp_Click(object sender, EventArgs e)
        {
            TopString = CboTT[LostIndex - 1].Text;
            CboTT[LostIndex - 1].Text = CboTT[LostIndex].Text;
            CboTT[LostIndex].Text = TopString;
            CboTT[LostIndex - 1].Focus();

            CboNListIndex = CboN[LostIndex - 1].SelectedIndex;
            CboN[LostIndex - 1].SelectedIndex = CboN[LostIndex].SelectedIndex;
            CboN[LostIndex].SelectedIndex = CboNListIndex;

            ChkValue = Convert.ToInt16(ChkN[LostIndex - 1].Checked);
            ChkN[LostIndex - 1].Checked = ChkN[LostIndex].Checked;
            ChkN[LostIndex].Checked = Convert.ToBoolean(ChkValue);
            if (ChkAutoN.Checked == true)
            {
                AutoN();
            }
        }
        private void CmdDown_Click(object sender, EventArgs e)
        {
            TopString = CboTT[LostIndex].Text;
            CboTT[LostIndex].Text = CboTT[LostIndex + 1].Text;
            CboTT[LostIndex + 1].Text = TopString;
            CboTT[LostIndex + 1].Focus();

            CboNListIndex = CboN[LostIndex].SelectedIndex;
            CboN[LostIndex].SelectedIndex = CboN[LostIndex + 1].SelectedIndex;
            CboN[LostIndex + 1].SelectedIndex = CboNListIndex;

            ChkValue = Convert.ToInt16(ChkN[LostIndex].Checked);
            ChkN[LostIndex].Checked = ChkN[LostIndex + 1].Checked;
            ChkN[LostIndex + 1].Checked = Convert.ToBoolean(ChkValue);
            if (ChkAutoN.Checked == Convert.ToBoolean(1))
            {
                AutoN();
            }
        }
        private void CmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                CboLoad();

                RowQuantity = CboN.Count;
                j = RowQuantity - LostIndex - 2;
                k = LostIndex;

                for (i = 1; i <= j; i++)
                {
                    LostIndex = RowQuantity - 1 - i;
                    CmdDown_Click(true, null);
                }

                AutoN();

                CboTT[k + 1].Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        private void CmdDel_Click(object sender, EventArgs e) // Удаление строки
        {
            //CboN.Remove((sN as ComboBox));
            //flp.Controls.Remove((sN as ComboBox));
            //int old = CboN.Count - 1;
            //if (old != -1)
            //{
            //    CboN.ElementAt(old).Select();
            //}
            //else
            //{
            //    //combo.Focus();
            //}

            RowQuantity = CboN.Count;
            j = RowQuantity - LostIndex - 1;
            k = LostIndex;
            for (i = 1; i <= j; i++)
            {
                LostIndex = RowQuantity - j - 1 + i;
                CmdUp_Click(true, null);
            }

            CboUnLoad();
            if (j == 0)
            {
                CboTT[k - 1].Focus();
            }
            else
            {
                CboTT[k].Focus();
            }
            AutoN();


        }
        private void CmdDiam_Click(object sender, EventArgs e)
        {
            CboTT[LostIndex].Text = CboTT[LostIndex].Text + (char)(0xD8);
            CboTT[LostIndex].Focus();
        }
        private void CmdDeg_Click(object sender, EventArgs e)
        {
            CboTT[LostIndex].Text = CboTT[LostIndex].Text + (char)(176);
            CboTT[LostIndex].Focus();
        }
        private void CmdPM_Click(object sender, EventArgs e)
        {
            CboTT[LostIndex].Text = CboTT[LostIndex].Text + (char)(177);
            CboTT[LostIndex].Focus();
        }

        private void CboChangeLang_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (CboChangeLang.SelectedIndex)
            {
                case 0:
                    m3 = 0;
                    break;
                case 1:
                    m3 = 1;
                    break;
            }
            Start();
        }
    }
}