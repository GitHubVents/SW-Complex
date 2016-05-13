using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using System.Reflection;
using System.Windows;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.swconst;
using SolidWorksTools;
using SolidWorksTools.File;
using System.Collections.Generic;
using System.Diagnostics;
using SWPlus;
using VentsMaterials;
using MessageBox = System.Windows.MessageBox;
using View = SolidWorks.Interop.sldworks.View;
using System.Threading;
using System.Xml;

namespace SwSpecification
{
    /// <summary>
    /// Summary description for SwSpecification.
    /// </summary>
    [Guid("869c32b4-4c9f-4310-8bc4-9d7761fe8ea1"), ComVisible(true)]
    [SwAddin(
        Description = "SW-Complex. Приложений для конструктора по оформлению конструкторской документации",
        Title = "SW-Complex",
        LoadAtStartup = true
        )]
    public class SwGUID : ISwAddin
    {
        #region Local Variables
        ISldWorks iSwApp = null;
        ICommandManager iCmdMgr = null;
        int _addinId = 0;
        BitmapHandler iBmp;
        private ModelDoc2 swModel;
        private View swView;
        private Sheet swSheet;
        private DrawingDoc _swDraw;
        private NewDoc NewDocform;
        SettingForm _stgForm = new SettingForm();
        SetMaterials _ventsDll = new SetMaterials();
        CheckModel CheckModelClass = new CheckModel();
        
      
        public const int mainCmdGroupID = 5;
        public const int mainItemID1 = 0;
        public const int mainItemID2 = 1;
        public const int mainItemID3 = 2;
        public const int mainItemID4 = 3;
        public const int mainItemID5 = 4;
        public const int mainItemID6 = 5;
        public const int mainItemID7 = 6;
        public const int mainItemID8 = 7;
        public const int mainItemID9 = 8;
        public const int mainItemID10 = 9;
        public const int mainItemID11 = 10;
        public const int mainItemID12 = 11;
        public const int mainItemID13 = 12;
        public const int mainItemID14 = 13;
        public const int mainItemID15 = 14;
        public const int flyoutGroupID = 91;

        #region Event Handler Variables
        Hashtable _openDocs = new Hashtable();
        SldWorks _swEventPtr = null;
        #endregion

        // Public Properties
        public ISldWorks SwApp
        {
            get { return iSwApp; }
            set { throw new NotImplementedException(); }
        }

        public ICommandManager CmdMgr
        {
            get { return iCmdMgr; }
        }

        public Hashtable OpenDocs
        {
            get { return _openDocs; }
        }

        #endregion
        #region SolidWorks Registration
        [ComRegisterFunctionAttribute]
        public static void RegisterFunction(Type t)
        {
            #region Get Custom Attribute: SwAddinAttribute
            SwAddinAttribute SWattr = null;
            Type type = typeof(SwGUID);

            foreach (System.Attribute attr in type.GetCustomAttributes(false))
            {
                if (attr is SwAddinAttribute)
                {
                    SWattr = attr as SwAddinAttribute;
                    break;
                }
            }

            #endregion

            try
            {
                Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
                Microsoft.Win32.RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

                string keyname = "SOFTWARE\\SolidWorks\\Addins\\{" + t.GUID.ToString() + "}";
                Microsoft.Win32.RegistryKey addinkey = hklm.CreateSubKey(keyname);
                addinkey.SetValue(null, 0);

                addinkey.SetValue("Description", SWattr.Description);
                addinkey.SetValue("Title", SWattr.Title);

                keyname = "Software\\SolidWorks\\AddInsStartup\\{" + t.GUID.ToString() + "}";
                addinkey = hkcu.CreateSubKey(keyname);
                addinkey.SetValue(null, Convert.ToInt32(SWattr.LoadAtStartup), Microsoft.Win32.RegistryValueKind.DWord);
            }
            catch (System.NullReferenceException nl)
            {
                Console.WriteLine("There was a problem registering this dll: SWattr is null. \n\"" + nl.Message + "\"");
                System.Windows.Forms.MessageBox.Show("There was a problem registering this dll: SWattr is null.\n\"" + nl.Message + "\"");
            }

            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);

                System.Windows.Forms.MessageBox.Show("There was a problem registering the function: \n\"" + e.Message + "\"");
            }
        }

        [ComUnregisterFunctionAttribute]
        public static void UnregisterFunction(Type t)
        {
            try
            {
                Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
                Microsoft.Win32.RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

                string keyname = "SOFTWARE\\SolidWorks\\Addins\\{" + t.GUID.ToString() + "}";
                hklm.DeleteSubKey(keyname);

                keyname = "Software\\SolidWorks\\AddInsStartup\\{" + t.GUID.ToString() + "}";
                hkcu.DeleteSubKey(keyname);
            }
            catch (System.NullReferenceException nl)
            {
                Console.WriteLine("There was a problem unregistering this dll: " + nl.Message);
                System.Windows.Forms.MessageBox.Show("There was a problem unregistering this dll: \n\"" + nl.Message + "\"");
            }
            catch (System.Exception e)
            {
                Console.WriteLine("There was a problem unregistering this dll: " + e.Message);
                System.Windows.Forms.MessageBox.Show("There was a problem unregistering this dll: \n\"" + e.Message + "\"");
            }
        }

        #endregion
        #region ISwAddin Implementation
        public bool ConnectToSW(object ThisSW, int cookie)
        {
            iSwApp = (ISldWorks)ThisSW;
            _addinId = cookie;

            //Setup callbacks
            iSwApp.SetAddinCallbackInfo(0, this, _addinId);

            #region Setup the Command Manager
            iCmdMgr = iSwApp.GetCommandManager(cookie);
            AddCommandMgr();
            #endregion

            #region Setup the Event Handlers
            _swEventPtr = (SldWorks)iSwApp;
            _openDocs = new Hashtable();
       
            #endregion

            #region Setup Sample Property Manager

            #endregion

            return true;
        }
        public bool DisconnectFromSW()
        {
            RemoveCommandMgr();

            Marshal.ReleaseComObject(iCmdMgr);
            iCmdMgr = null;
            Marshal.ReleaseComObject(iSwApp);
            iSwApp = null;
            //The addin _must_ call GC.Collect() here in order to retrieve all managed code pointers 
            GC.Collect();
            GC.WaitForPendingFinalizers();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return true;
        }
        #endregion
        #region UI Methods
            public void AddCommandMgr()
            {
                try
                {
                    ICommandGroup cmdGroup;
                    if (iBmp == null)
                        iBmp = new BitmapHandler();

                    Assembly thisAssembly;
                    //int cmdIndex1, cmdIndex2, cmdIndex3, cmdIndex4, cmdIndex5, cmdIndex6, cmdIndex7, cmdIndex8, cmdIndex9;
                    string Title = "SW-Complex", ToolTip = "SW-Complex";

                    int[] docTypes = new int[]{(int)swDocumentTypes_e.swDocASSEMBLY,
                                               (int)swDocumentTypes_e.swDocDRAWING,
                                               (int)swDocumentTypes_e.swDocPART};

                    thisAssembly = System.Reflection.Assembly.GetAssembly(this.GetType());

                    int cmdGroupErr = 0;
                    bool ignorePrevious = false;

                    object registryIDs;
                    //get the ID information stored in the registry
                    bool getDataResult = iCmdMgr.GetGroupDataFromRegistry(mainCmdGroupID, out registryIDs);

                    int[] knownIDs = new int[3] { mainItemID1, mainItemID2, mainItemID3 };

                    if (getDataResult)
                    {
                        if (!CompareIDs((int[])registryIDs, knownIDs)) //if the IDs don't match, reset the commandGroup
                        {
                            ignorePrevious = true;
                        }
                    }

                    cmdGroup = iCmdMgr.CreateCommandGroup2(mainCmdGroupID, Title, ToolTip, "", -1, ignorePrevious, ref cmdGroupErr);

                    cmdGroup.LargeIconList = iBmp.CreateFileFromResourceBitmap("SwSpecification.AddLogo.bmp", thisAssembly); //Создать отчет
                    cmdGroup.SmallIconList = iBmp.CreateFileFromResourceBitmap("SwSpecification.AddLogo.bmp", thisAssembly);
                    //cmdGroup.LargeMainIcon = iBmp.CreateFileFromResourceBitmap("SwSpecification.AddLogo.bmp", thisAssembly);
                    //cmdGroup.SmallMainIcon = iBmp.CreateFileFromResourceBitmap("SwSpecification.AddLogo.bmp", thisAssembly);

                    GetCurrentVersion();

                    SetSettings();

                    //int menuToolbarOption = (int)(swCommandItemType_e.swMenuItem | swCommandItemType_e.swToolbarItem);
                    const int menuToolbarOption = (int)(swCommandItemType_e.swToolbarItem);

                    // Создать отчет c чертежа...
                    //cmdGroup.AddCommandItem2("", 0, "", "Создать отчет c чертежа", 0, "Load", "LoadEnable", mainItemID1, menuToolbarOption);
                    //Создать отчет cо сборки...
                    //cmdGroup.AddCommandItem2("", 1, "", "Создать отчет cо сборки", 1, "LoadAssembly", "", mainItemID2, menuToolbarOption);
                    // Материалы...
                    cmdGroup.AddCommandItem2("", 0, "Справочник материалов", "Материалы", 2, "LoadMaterials", "LoadMaterialsEnable", mainItemID1, menuToolbarOption);
                    // Извещение об изменение
                    cmdGroup.AddCommandItem2("", 1, "Редактор извещений", "Извещение", 10, "NotificationOfChanges", "NotificationOfChangesEnable", mainItemID2, menuToolbarOption);
                    // Переименование конфигураций
                    cmdGroup.AddCommandItem2("", 2, "Переименование конфигураций", "Переименование конфигураций", 7, "ReNameConfiguration", "LoadSwModelEnable", mainItemID3, menuToolbarOption);
                    // MProp
                    cmdGroup.AddCommandItem2("", 3, "Редактор свойств", "MProp", 6, "LoadMProp", "LoadMPropEnable", mainItemID4, menuToolbarOption);
                    // DProp
                    cmdGroup.AddCommandItem2("", 4, "Редактор чертежа", "DProp", 8, "LoadDProp", "LoadDPropEnable", mainItemID5, menuToolbarOption);
                    // SProp
                    cmdGroup.AddCommandItem2("", 5, "Редактор свойств", "SProp", 9, "LoadSProp", "LoadSPropEnable", mainItemID6, menuToolbarOption);
                    // add table maretial
                    cmdGroup.AddCommandItem2("", 6, "Редактор таблиц", "Вставка таблицы", 0, "LoadTableMat", "CheckDrawingDoc", mainItemID13, menuToolbarOption);
                    // TT
                    cmdGroup.AddCommandItem2("", 7, "Технические требования", "TT", 11, "LoadTT", "CheckDrawingDoc", mainItemID14, menuToolbarOption);
                    // RecorDimM
                    cmdGroup.AddCommandItem2("", 8, "RecordDimM", "RecordDimM", 12, "LoadRecordDimM", "CheckDrawingDoc", mainItemID15, menuToolbarOption);
                    // Параметры...
                    //cmdGroup.AddCommandItem2("", 9, "", "Параметры", 3, "UsersSettings", "", mainItemID9, menuToolbarOption);
                    // Настройки подключения...
                    //cmdGroup.AddCommandItem2("", 10, "", "Настройки подключения", 4, "LoadMyForm", "", mainItemID10, menuToolbarOption);
                    // Справка
                    //cmdGroup.AddCommandItem2("", 11, "", "Справка", 5, "LoadHelp", "", mainItemID11, menuToolbarOption);
                    // О программе
                    //cmdGroup.AddCommandItem2("", 12, "", "О SW-Complex", 0, "AboutSWCom", "", mainItemID12, menuToolbarOption);

                    cmdGroup.HasToolbar = true;
                    cmdGroup.HasMenu = false;

                    //active the command group
                    cmdGroup.Activate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "; " + ex.StackTrace);
                }
            }
            public void RemoveCommandMgr()
            {
                iBmp.Dispose();

                iCmdMgr.RemoveCommandGroup(mainCmdGroupID);
                iCmdMgr.RemoveFlyoutGroup(flyoutGroupID);
            }
            public bool CompareIDs(int[] storedIDs, int[] addinIDs)
            {
                var storedList = new List<int>(storedIDs);
                var addinList = new List<int>(addinIDs);
                
                addinList.Sort();
                storedList.Sort();
                
                if (addinList.Count != storedList.Count)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < addinList.Count; i++)
                    {
                        if (addinList[i] != storedList[i])
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        #endregion
        #region UI Callbacks - Функции и процедуры
        #region " Set settings "
            public void SetSettings()
            {
                CheckModelClass.SetSolidWorksSettings();
            }
        #endregion
        #region " Autoupdate SW-Complex "
            private void GetCurrentVersion()
            {
                try
                {
                    // Ver. XML
                    var XmlPathName = @"\\" + "192.168.14.11" + @"\SolidWorks Admin\Bat\SW-Complex\Version SW-Complex.xml";
                    //var doc = new XmlDocument();
                    //doc.Load(XmlPathName);
                    //var xmlVer = new Version(doc.GetElementsByTagName("version")[0].InnerText);
                    string xmlVer = "";
                    using (var xmlReader = new XmlTextReader(XmlPathName))
                    {
                        while (xmlReader.Read())
                        {
                            switch (xmlReader.NodeType)
                            {
                                case XmlNodeType.Text:
                                    xmlVer = xmlReader.Value;
                                    break;
                            }
                        }
                    }
                    // Ver. App
                    //var assembly = Assembly.GetExecutingAssembly();
                    //var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                    //var appVer = new Version(fvi.FileVersion);
                    var userAppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                    FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(userAppData + "\\SW-Complex\\SwSpecification.dll");
                    //if (myFileVersionInfo.FileVersion < xmlVer.ToString)
                    // Версия 1.0.0.6
                    if (int.Parse(xmlVer.ToString().Replace(".", "")) > int.Parse(myFileVersionInfo.FileVersion.Replace(".", "")))
                    {
                        deleteSwPlus();
                        CopyUpDate();
                        RunSwUpdate();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "; " + ex.StackTrace);
                }
            }
            void CopyUpDate()
            {
                try
                {
                    var userAppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                    Directory.CreateDirectory(userAppData + "\\SW-Complex\\");
                    // Копируем файлы SW-Complex с сервера
                    File.Copy(@"\\" + "192.168.14.11" + @"\SolidWorks Admin\Bat\SW-Complex\Update SW-Complex.exe", userAppData + "\\SW-Complex\\Update SW-Complex.exe", true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "; " + ex.StackTrace);
                }
            }
            void RunSwUpdate()
            {
                try
                {
                    var userAppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                    var stringPath = userAppData + "\\SW-Complex\\Update SW-Complex.exe";
                    Process.Start(stringPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "; " + ex.StackTrace);
                }
            }
            void deleteSwPlus()
            {
            }
        #endregion
        #region " NotificationOfChanges "
        public void NotificationOfChanges()
        {
            try
            {
                var notifOfChanges = new NotificationOfChanges();
                notifOfChanges.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        public int NotificationOfChangesEnable()
        {
            var functionReturnValue = 0;
            try
            {
                swModel = SwApp.ActiveDoc;
                try
                {
                    if (swModel.GetType() != (int)swDocumentTypes_e.swDocDRAWING)
                    {
                        functionReturnValue = 0;
                    }
                    else
                    {
                        functionReturnValue = 1;
                    }
                }
                catch
                {
                    functionReturnValue = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
            return functionReturnValue;
        }
        #endregion
        #region " MProp "
            public void LoadMProp()
            {
                var mPropForm = new MProp();
                mPropForm.ShowDialog();
            }
            public int LoadMPropEnable()
            {
            var functionReturnValue = 0;
            try
            {
                swModel = SwApp.ActiveDoc;
                try
                {
                    if ((swModel == null))
                    {
                        functionReturnValue = 0;
                    }
                    else
                    {
                        functionReturnValue = 1;
                    }
                }
                catch
                {
                    functionReturnValue = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return functionReturnValue;
        }
        #endregion
        #region " DProp "
            public void LoadDProp()
        {
            var dPropForm = new DProp();
            dPropForm.ShowDialog();
        }
            public int LoadDPropEnable()
            {
            var functionReturnValue = 0;
            try
            {
                swModel = SwApp.ActiveDoc;
                try
                {
                    if ((swModel.GetType() != (int)swDocumentTypes_e.swDocDRAWING))
                    {
                        functionReturnValue = 0;
                    }
                    else
                    {
                        functionReturnValue = 1;
                    }
                }
                catch
                {
                    functionReturnValue = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return functionReturnValue;
        }
        #endregion   
        #region " SProp "
            public void LoadSProp()
            {
                var sPropForm = new SProp();
                sPropForm.ShowDialog();
            }   
            public int LoadSPropEnable()
            {
                var functionReturnValue = 0;
                try
                {
                    swModel = SwApp.ActiveDoc;
                    try
                    {
                        if (swModel.GetType() == (int)swDocumentTypes_e.swDocDRAWING)
                        {
                            functionReturnValue = 0;
                        }
                        else
                        {
                            functionReturnValue = 1;
                        }
                    }
                    catch
                    {
                        functionReturnValue = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                return functionReturnValue;
            }
        #endregion   
        #region " Проверка на открытую модель "
            //<~~~~~~ При открытом чертеже кнопка погашена
            public int LoadSwModelEnable()
        {
            var functionReturnValue = 0;
            try
            {
                //SwApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
                swModel = SwApp.ActiveDoc;
                try
                {
                    if ((swModel == null))
                    {
                        functionReturnValue = 0;
                    }
                    else
                    {
                        functionReturnValue = 1;
                    }
                }
                catch
                {
                    functionReturnValue = 0;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                //throw;
            }
            return functionReturnValue;
        }
        #endregion
        #region " ReNameConfiguration "
            public void ReNameConfiguration()
            {
                CheckModelClass.ReNameConfiguration();
            }
        #endregion
        #region " ОТКРЫВАЕМ ТОЛЬКО СБОРОЧНЫЙ ЧЕРТЕЖ "
            public int LoadEnable()
        {
            try
            {
            int functionReturnValue = 0;
            swModel = SwApp.ActiveDoc;
                try
                {
                    if (swModel.GetType() != (int)swDocumentTypes_e.swDocDRAWING)
                    {
                        functionReturnValue = 0;
                    }
                    else
                    {
                        _swDraw = (DrawingDoc)swModel;
                        swSheet = _swDraw.GetCurrentSheet();
                        swView = _swDraw.GetFirstView();
                        functionReturnValue = 1;
                    }

                    if (swSheet.CustomPropertyView == "По умолчанию" | swSheet.CustomPropertyView == "Default")
                    {
                        swView = swView.GetNextView();
                        // Получаем первый вид
                    }

                    swModel = swView.ReferencedDocument;
                    if (swModel.GetType() != (int)swDocumentTypes_e.swDocASSEMBLY)
                    {
                        functionReturnValue = 0;
                    }
                }
                catch
                {
                    functionReturnValue = 0;
                }
                return functionReturnValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
        #endregion
        #region " ФОРМА НАСТРОЕК - SETTINGS "
            public void LoadMyForm()
        {
            //Try
            //If StgForm Is Nothing Then
            _stgForm = new SettingForm();
            //End If
            _stgForm.ShowDialog();
            //Catch
            //StgForm = Nothing
            //End Try

            //<~~~~~~~~~~~~~~~~~~~~~ Сохраняем настройки

            string kye = "ENTERPRISE /s \"srvprog:1641\\Yurchenko_new\" /c \"Constructor\"";
            string logn = Properties.Settings.Default.LoginUser;
            string pas = Properties.Settings.Default.PasswordUser;

            ProcessStartInfo p = new ProcessStartInfo();

            //Путь к программе 1С Предприятие
            p.FileName = "C:\\Program Files (x86)\\1cv82\\8.2.18.61\\bin\\1cv8.exe";

            // Use these arguments for the process
            p.Arguments = kye + "/N\"" + logn + "\"" + "/P" + "\"" + pas + "\"";

            _stgForm.Path1c.Text = p.FileName;
            _stgForm.PathKey.Text = kye;

            //If My.Settings.LoginUser = "*" Then
            _stgForm.Login.Text = Properties.Settings.Default.LoginUser;
            _stgForm.pass.Text = Properties.Settings.Default.PasswordUser;
            _stgForm.Path1c.Text = Properties.Settings.Default.Path1c;
            _stgForm.PathKey.Text = Properties.Settings.Default.Key1c;

            //End If

        }
        #endregion
        #region " USERS SETTINGS "
            public void UsersSettings()
        {
            var formUsersSettings = new FrmUsersSettings();
            formUsersSettings.ShowDialog();
            formUsersSettings.ComboBoxIP.Text = Properties.Settings.Default.ComboBoxIP;
            formUsersSettings.ChkStandard.Checked = Properties.Settings.Default.ChkStandard;
            formUsersSettings.ChkIns.Checked = Properties.Settings.Default.ChkInsPrp;
        }
        #endregion
        #region " СОЗДАНИЕ СПЕЦИФИКАЦИИ "
        // Кнопка "Создать спецификацию"
        public void Load()
        {
            //// Запуск формы Get Config
            //NewDocform = new NewDoc();
            //NewDocform.ShowDialog();
            try
            {
                if (MessageBox.Show("Создать спецификацию?", "SW-Complex", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                }
                else
                {
                 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region " СОЗДАНИЕ ОТЧЕТА И ЗАПУСК 1С СО СБОРКИ"
            public void LoadAssembly()
            {
                // Запуск формы Get Config
                // NewDocform = New NewDoc
                // NewDocform.ShowDialog()
            }
        #endregion
        #region " БАЗА МАТЕРИАЛОВ "
            public void LoadMaterials()
            {
                var matForm = new Materials();
                matForm.ShowDialog();
            }
            //<~~~~~~ При открытом чертеже кнопка погашена
            public int LoadMaterialsEnable()
            {
                var functionReturnValue = 0;
                try
                {
                    //SwApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
                    swModel = SwApp.ActiveDoc;
                    try
                    {
                        if ((swModel.GetType() != (int)swDocumentTypes_e.swDocPART))
                        {
                            functionReturnValue = 0;
                        }
                        else
                        {
                            functionReturnValue = 1;
                        }
                    }
                    catch
                    {
                        functionReturnValue = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return functionReturnValue;
            }
        #endregion
        #region " ОТКРЫВАЕМ СПРАВКУ "
            //TODO: SW-Complex help
            public void loadhelp()
            {
                //ProcessStartInfo c = new ProcessStartInfo();
                //c.FileName = "http://srvkb/faq/";
                //c.FileName = "http://" + "192.168.11.14" + "/faq/";
                //Process.Start(c);
            }
        #endregion
        #region " ABOUT SW-COMPLEX "
            public void AboutSwCom()
            {
                var frmAboutSw = new AboutSWComplex();
                frmAboutSw.ShowDialog();
            }
        #endregion
        #region " Properties Table "
            public void LoadTableMat()
        {
            var tableToDrwDoc = new UcTableToDrwDoc();

            tableToDrwDoc.ShowDialog();
            
        }
        #endregion
        #region TT
            public void LoadTT()
            {
                try
                {
                    var frmT = new TT.FrmTT();
                    frmT.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
            }
        #endregion
        #region RecordDimM
            public void LoadRecordDimM()
            {
                var frmRecordDim = new RecordDimM.FrmRecordDimM();
                frmRecordDim.Show();
            }
        #endregion
        public int CheckDrawingDoc()
        {
            var functionReturnValue = 0;
            try
            {
                swModel = SwApp.ActiveDoc;
                try
                {
                    if ((swModel.GetType() != (int)swDocumentTypes_e.swDocDRAWING))
                    {
                        functionReturnValue = 0;
                    }
                    else
                    {
                        functionReturnValue = 1;
                    }
                }
                catch
                {
                    functionReturnValue = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return functionReturnValue;
        }
        public void FlyoutCallback()
        {
            FlyoutGroup flyGroup = iCmdMgr.GetFlyoutGroup(flyoutGroupID);
            flyGroup.RemoveAllCommandItems();

            flyGroup.AddCommandItem(System.DateTime.Now.ToLongTimeString(), "test", 0, "FlyoutCommandItem1", "FlyoutEnableCommandItem1");
        }
        public int FlyoutEnable()
        {
            return 1;
        }
        public void FlyoutCommandItem1()
        {
            iSwApp.SendMsgToUser("Flyout command 1");
        }
        public int FlyoutEnableCommandItem1()
        {
            return 1;
        }
        #endregion
    }
}