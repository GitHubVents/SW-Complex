using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace RecordDimM
{
    public class ClassRecordDimM
    {
        SldWorks swApp;
        public ModelDoc2 swModel;
        DrawingDoc MyDrw;
        PartDoc MyPart;
        AssemblyDoc MyAssem;
        public SelectionMgr SelMgr;

        int m; // Метка открытой формы
        int m1; // Метка обновления
        public int m2; // Метка ошибка
        public FrmRecordDimM FrmRecord { get; set; }

        public void MonitorSolidWorks()
        {
            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            Test();
        }
        private void MyDrw_NewSelectionNotify()
        {
            m1 = 1;
            MonitorSolidWorks();
        }
        public void Test() // Проверка открытого документа
        {
            try
            {
                swModel = swApp.ActiveDoc;
                m2 = 0;

                if (swModel == null)
                {
                    m2 = 1;
                    FrmRecord.LblFirst.Content = "Откройте документ";
                    FrmRecord.LblSecond.Content = "";
                }
                else if (swModel.GetType() == (int)swDocumentTypes_e.swDocDRAWING)
                {
                    MyDrw = swApp.ActiveDoc;
                }
                else if (swModel.GetType() == (int)swDocumentTypes_e.swDocPART)
                {
                    MyPart = swApp.ActiveDoc;
                }
                else if (swModel.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
                {
                    MyAssem = swApp.ActiveDoc;
                }
                else
                {
                    m2 = 1;
                    FrmRecord.LblFirst.Content = "Откройте нормальный документ";
                    FrmRecord.LblSecond.Content = "";
                }

                if (m2 == 0)
                {
                    SelMgr = swModel.SelectionManager;


                    if (SelMgr.GetSelectedObjectCount() == 1)
                    {
                        if ((int)SelMgr.GetSelectedObjectType(1) != (int)swSelectType_e.swSelDIMENSIONS) // Выберите размер swSelDIMENSIONS
                        {
                            FrmRecord.LblFirst.Content = "Выберите размер";
                            FrmRecord.LblSecond.Content = "";
                            m2 = 1;
                        }
                    }
                    else
                    {
                        FrmRecord.LblFirst.Content = "Выберите один размер";
                        FrmRecord.LblSecond.Content = "";
                        m2 = 1;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Source - {0},\r\n" +
                                            "TargetSite - {1},\r\n" +
                                            "Message - {2},\r\n" +
                                            "StackTrace - {3}\r\n",
                                            ex.Source, ex.TargetSite, ex.Message, ex.StackTrace));
            }
        }
    }
}
