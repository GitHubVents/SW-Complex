using System;
using System.Collections.Generic;
using System.Data;
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
using SolidWorks.Interop.swconst;
using SWPlus;
using SwSpecification;
using VentsMaterials;

namespace SwSpecification
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EditPropeprty : Window
    {
        public EditPropeprty()
        {
            InitializeComponent();

        }

        private SldWorks _swApp;
        public ModelDoc2 _swModel { get; set; }
        private DrawingDoc _swDraw;
        private Sheet _swSheet;
        private SelectionMgr _swSelMgr;
        SolidWorks.Interop.sldworks.View _swView;
        public string StrActiveSheetName { get; set; } // Имя листа, который был активен при открытии чертежа
        string[] _vSheetNames;

        private void GridHome_Loaded(object sender, RoutedEventArgs e)
        {
            StartLoad();
        }

        public void StartLoad()
        {
            try
            {
                LoadDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadDataGrid()
        {

            DataGridTable.ItemsSource = ClassPropertySldWorks.ListColumnToEditProp(_swModel);

        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            EditTable();
            LoadDataGrid();
        }

        private void BtnOkAndClose_Click(object sender, RoutedEventArgs e)
        {
            EditTable();
            Close();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        public void EditTable()
        {
            try
            {

            _swSelMgr = _swModel.SelectionManager;

            var list = (List<ClassPropertySldWorks.ColumnNameEditPropTable>)DataGridTable.ItemsSource;

                foreach (var items in list)
                {
                    var columnConfig = items.ColumnConfig;

                    _swModel.DeleteCustomInfo2(columnConfig, "Код_ФБ");
                    _swModel.DeleteCustomInfo2(columnConfig, "Наименование");

                    var customPropMan = _swModel.Extension.CustomPropertyManager[columnConfig];

                    var columnDescription = items.ColumnDescription;
                    var colFb = items.CodFb;

                    customPropMan.Add2("Наименование", 30, columnDescription);
                    customPropMan.Add2("Код_ФБ", 30, colFb);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

  
    }
}
