using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using SolidWorks.Interop.sldworks;
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;

namespace SwSpecification
{
    /// <summary>
    /// Interaction logic for NotificationOfChanges.xaml
    /// </summary>
    public partial class NotificationOfChanges : Window
    {
        public NotificationOfChanges()
        {
            InitializeComponent();
        }

        private SldWorks _swApp;
        private ModelDoc2 _swModel;
        private Sheet _swSheet;
        private DrawingDoc _swDraw;
        private Note _swNote;
        private SelectionMgr _swSelMgr;

        private string _strActiveSheetName;
        private string _strSheetFormatName;
        private string _strTemp;

        private string[] _vSheetNames;

        private bool ok;

        #region " Версия чертежа "

        private void FormLoad_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
                _swModel = (ModelDoc2)_swApp.ActiveDoc;
                _swDraw = (DrawingDoc)_swModel;
                _swSheet = (Sheet)_swDraw.GetCurrentSheet();
                _strActiveSheetName = _swSheet.GetName();
                _vSheetNames = _swDraw.GetSheetNames();

                CmdRevMethod(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }

        public string StrTempNumber { get; set; }
        public string StrTempData { get; set; }
        public void CmdRevMethod(bool m)
        {
            try
            {

            if (m & string.IsNullOrEmpty(TxtBox.Text))
            {
                MessageBox.Show("Введите номер извещения.");
                return;
            }

            for (var i = 0; i <= _vSheetNames.GetUpperBound(0); i++)
            {
                //TODO: _swNote

                //ok = _swDraw.ActivateSheet(_vSheetNames[i]);
                _swDraw.ActivateSheet(_vSheetNames[i]);
                _swSheet = _swDraw.GetCurrentSheet();
                _strSheetFormatName = _swSheet.GetSheetFormatName();

                _swModel = (ModelDoc2)_swDraw;

                _swSelMgr = _swModel.SelectionManager;

                //MessageBox.Show(_swSelMgr.ToString());
                // Дата
                StrTempData = "Revision4@" + _strSheetFormatName;
                ok = _swModel.Extension.SelectByID2(StrTempData, "NOTE", 0, 0, 0, false, 0, null, 0);

                if (ok)
                {
                    _swNote = _swSelMgr.GetSelectedObject2(1);

                    //MessageBox.Show(_swNote.ToString());
                    if (m)
                    {
                        var date = (DateTime)StrData.SelectedDate;
                        StrTempData = Convert.ToString(date.ToString("dd-MM-yy"));
                        _swNote.SetText(StrTempData);
                    }
                    else
                    {
                        //MessageBox.Show(StrTempData);
                        StrTempData = _swNote.GetText();

                        if (StrTempData == "" | StrTempData == " ")
                        {
                            StrData.SelectedDate = (DateTime.Today);
                        }
                        else
                        {
                            StrData.SelectedDate = Convert.ToDateTime(StrTempData);
                        }
                    }
                }

                // Номер
                StrTempNumber = "Revision3@" + _strSheetFormatName;
                _swModel.Extension.SelectByID2(StrTempNumber, "NOTE", 0, 0, 0, false, 0, null, 0);

                if (ok)
                {
                    _swNote = _swSelMgr.GetSelectedObject2(1);

                    if (m)
                    {
                        StrTempNumber = "ИКД-" + TxtBox.Text;
                        _swNote.SetText(StrTempNumber);
                    }
                    else
                    {
                        StrTempNumber = _swNote.GetText();
                        if (StrTempNumber != " ")
                        {
                            TxtBox.Text = StrTempNumber.Substring(4);
                        }
                    }
                }

                // Зам.
                _strTemp = "Revision2@" + _strSheetFormatName;
                _swModel.Extension.SelectByID2(_strTemp, "NOTE", 0, 0, 0, false, 0, null, 0);


                if (ok)
                {

                    _swNote = _swSelMgr.GetSelectedObject2(1);


                    if (m)
                    {

   
                        if (Rb1.IsChecked == true)
                        {
                            _swNote.SetText("Зам.");
                            _swModel.DeleteCustomInfo2("", "Revision");

                        }

                        if (Rb3.IsChecked == true)
                        {
                            _swNote.SetText("Нов.");
                            _swModel.DeleteCustomInfo2("", "Revision");
                        }

                        if (Rb2.IsChecked == true)
                        {
                            _swNote.SetText("Змін.");
                            _swModel.DeleteCustomInfo2("", "Revision");
                            _swModel.AddCustomInfo3("", "Revision", 30, "");
                            _swModel.CustomInfo2["", "Revision"] = Convert.ToString(SpinButton.Content);
                        }
                    }
                    else
                    {
                        _strTemp = _swNote.GetText();

                        if (_strTemp == "Зам.")
                        {
                            Rb1.IsChecked = true;
                            Rb3.IsChecked = false;
                            Rb2.IsChecked = false;
                            SpinButton.IsEnabled = false;

                        }

                        if (_strTemp == "Нов.")
                        {
                            Rb1.IsChecked = false;
                            Rb3.IsChecked = true;
                            Rb2.IsChecked = false;
                            SpinButton.IsEnabled = false;
                        }

                        if (_strTemp == "Змін.")
                        {
                            Rb1.IsChecked = false;
                            Rb3.IsChecked = false;
                            Rb2.IsChecked = true;
                            SpinButton.IsEnabled = true;
                        }

                        int n;
                        bool isNumeric = int.TryParse(_swModel.CustomInfo2["", "Revision"], out n);

                        if (isNumeric)
                        {
                            SpinButton.Content = _swModel.CustomInfo2["", "Revision"];
                        }
                        else
                        {
                            _swModel.DeleteCustomInfo2("", "Revision");
                        }

                    }
                }

            }

            // Возвращение активного листа
            _swDraw.ActivateSheet(_strActiveSheetName);

            // Перестраиваем чертеж
            _swModel.ForceRebuild3(false);

            _swModel.GraphicsRedraw2();
            _swModel.ClearSelection2(true);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }

        private void CmdRev_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CmdRevMethod(true);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void SpinButton_Spin(object sender, SpinEventArgs e)
        {
            try
            {

         
            int value = String.IsNullOrEmpty(Convert.ToString(SpinButton.Content)) ? 0 : Convert.ToInt32(Convert.ToString(SpinButton.Content));

            if (e.Direction == SpinDirection.Increase)
            {
                value++;
                SpinButton.Content = value.ToString();
            }
            else
            {
                if (value <= 1)
                {
                    SpinButton.Content = 1;
                }
                else
                {
                    value--;
                    SpinButton.Content = value.ToString();
                }

            }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }

        }

        // Only numbers in TextBox
        private void TxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            int iValue = -1;

            if (Int32.TryParse(textBox.Text, out iValue) == false)
            {
                var textChange = e.Changes.ElementAt<TextChange>(0);
                int iAddedLength = textChange.AddedLength;
                int iOffset = textChange.Offset;

                textBox.Text = textBox.Text.Remove(iOffset, iAddedLength);
            }
        }

        private void RB1_Click(object sender, RoutedEventArgs e)
        {
            if (Rb1.IsChecked == true)
            {
                SpinButton.IsEnabled = false;
                SpinButton.Content = "";
            }
        }

        private void RB2_Click(object sender, RoutedEventArgs e)
        {
            if (Rb2.IsChecked == true)
            {
                SpinButton.IsEnabled = true;
                int n;
                bool isNumeric = int.TryParse(_swModel.CustomInfo2["", "Revision"], out n);

                if (isNumeric)
                {

                    SpinButton.Content = _swModel.CustomInfo2["", "Revision"];

                }
                else
                {
                    _swModel.DeleteCustomInfo2("", "Revision");
                }
            }
        }

        #endregion
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Rb3_Click(object sender, RoutedEventArgs e)
        {
            if (Rb3.IsChecked == true)
            {
                SpinButton.IsEnabled = false;
                SpinButton.Content = "";
            }
        }
    }
}