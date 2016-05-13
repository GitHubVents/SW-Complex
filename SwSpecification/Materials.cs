using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using VentsMaterials;
using System.Collections.Generic;

namespace SwSpecification
{
    public partial class Materials : Form
    {
        public Materials()
        {
            InitializeComponent();
        }

        #region VARIABLES

        private SldWorks _swapp;
        private ModelDoc2 _swmodel;
        private readonly SetMaterials _matDll = new SetMaterials();
        private readonly ToSQL _sqlDll = new ToSQL();
        private readonly CheckModel _checkModelClass = new CheckModel();
        private readonly ConnectSqlString _con = new ConnectSqlString();

        #endregion
        private void Materials_Load(object sender, EventArgs e)
        {
            try
            {
                ToSQL.Conn = _con.Con;
                //PictureBox2.ImageLocation = @"D:\Program Files\SolidWorks 2013\SolidWorks\data\graphics\Images\preview\light grey low gloss plastic.jpg";

                ButtonOK.Enabled = false;
                BtnOkAndClose.Enabled = false;

                AddSelectAllCheckBox(DataGridConfig);

                CboColor.DisplayMember = "RAL";
                CboColor.ValueMember = "HEX";
                CboColor.DataSource = _sqlDll.RalTable();

                _sqlDll.PopulateTreeView("1", "Материалы", null, MatTree);

                CboType.ValueMember = "Code";
                CboType.DisplayMember = "Name";

                CboType.DataSource = _matDll.CoatingTypeDt().DefaultView;

                CboClass.DataSource = _matDll.CoatingListClass();

                LoadDataGrid();
                txtBoxSearch.Text = "Поиск...";
                txtBoxSearch.ForeColor = Color.Gray;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadDataGrid()
        {
            try
            {
                _swapp = (SldWorks) Marshal.GetActiveObject("SldWorks.Application");
                _swmodel = (ModelDoc2) _swapp.ActiveDoc;
                DataGridConfig.Rows.Clear();

                var confarray = _matDll.GetConfigurationNames();

                foreach (var confname in confarray)
                {
                    if (confname == "По умолчанию")
                    {
                        var configuration = (IConfiguration) _swmodel.GetConfigurationByName("По умолчанию");

                        _swapp.SendMsgToUser2("Конфигурация \"По умолчанию\" будет переименована на \"00\"",
                            (int) swMessageBoxIcon_e.swMbInformation, (int) swMessageBoxBtn_e.swMbOk);
                        configuration.Name = "00";
                    }
                }

                var confarray2 = _matDll.GetConfigurationNames();

                foreach (var confname in confarray2)
                {
                    int n;
                    if (int.TryParse(confname, out n))
                    {
                        var matname = _matDll.GetCustomProperty(confname, null);

                        foreach (var customProperty in matname)
                        {
                            if (customProperty.ColorSQL == "")
                            {
                                DataGridConfig.Rows.Add(ColChB.Selected, confname, customProperty.Name, "Без покрытия");
                            }
                            else
                            {
                                DataGridConfig.Rows.Add(ColChB.Selected, confname, customProperty.Name,
                                    customProperty.ColorSQL, customProperty.CoatingType, customProperty.CoatingClass);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(@"Конфигурацию """ + confname +
                                        @" ""необходимо переименовать в числовое значение!");

                        Close();
                        return;
                    }
                }

                var activeConfName = _matDll.GetActiveConfigurationName();
                DataGridConfig.Rows[Convert.ToInt32(activeConfName)].Cells[0].Value = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
        #region ADD ALL CHECKBOX DATAGRIDVIEW HEADER

        private bool _isSelectAllChecked;

        private void AddSelectAllCheckBox(DataGridView theDataGridView)
        {
            try
            {
                CheckBox cbx = new CheckBox();
                cbx.Name = "SelectAll";
                cbx.Size = new Size(14, 14);
                Rectangle rect;
                rect = theDataGridView.GetCellDisplayRectangle(0, -1, true);
                cbx.Location = new System.Drawing.Point(rect.Location.X + ((rect.Width - cbx.Width)/2),
                    rect.Location.Y + ((rect.Height - cbx.Height)/2));
                cbx.BackColor = Color.White;
                theDataGridView.Controls.Add(cbx);
                cbx.Click += HeaderCheckBox_Click;
                theDataGridView.CellValueChanged += DataGridView_CellChecked;
                theDataGridView.CurrentCellDirtyStateChanged += DataGridView_CurrentCellDirtyStateChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void HeaderCheckBox_Click(object sender, EventArgs e)
        {
            try
            {
                var activeConfName = _matDll.GetActiveConfigurationName();

                this._isSelectAllChecked = true;
                var cbx = (CheckBox) sender;
                var theDataGridView = (DataGridView) cbx.Parent;
                foreach (DataGridViewRow row in theDataGridView.Rows)
                {
                    row.Cells[0].Value = cbx.Checked;
                    ButtonOK.Enabled = true;
                    BtnOkAndClose.Enabled = true;
                    if (Convert.ToBoolean(row.Cells[0].Value) == false)
                    {
                        ButtonOK.Enabled = false;
                        BtnOkAndClose.Enabled = false;
                    }
                }
                if (Convert.ToBoolean(DataGridConfig.Rows[Convert.ToInt32(activeConfName)].Cells[0].Value) == false)
                {
                    MessageBox.Show(@"Не удалось отменить выбор активной конфигурации.", @"SolidWorks",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DataGridConfig.Rows[Convert.ToInt32(activeConfName)].Cells[0].Value = true;
                    ButtonOK.Enabled = true;
                    BtnOkAndClose.Enabled = true;
                }
                theDataGridView.EndEdit();
                this._isSelectAllChecked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DataGridView_CellChecked(System.Object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                var activeConfName = _matDll.GetActiveConfigurationName();
                ;

                var dataGridView = (DataGridView) sender;
                var isChecked =
                    Convert.ToBoolean(DataGridConfig.Rows[Convert.ToInt32(activeConfName)].Cells[e.ColumnIndex].Value);
                if (!this._isSelectAllChecked)
                {
                    if (
                        Convert.ToBoolean(dataGridView.Rows[Convert.ToInt32(activeConfName)].Cells[e.ColumnIndex].Value) ==
                        false)
                    {
                        var isAllUnChecked =
                            dataGridView.Rows.Cast<DataGridViewRow>().Any(row => Convert.ToBoolean(row.Cells[0].Value));
                        //((CheckBox)dataGridView.Controls.Item("SelectAll")).Checked = false;
                        ((CheckBox) dataGridView.Controls["SelectAll"]).Checked = false;

                        //dataGridView.Controls[]

                        ButtonOK.Enabled = isAllUnChecked;
                        BtnOkAndClose.Enabled = isAllUnChecked;
                        if (Convert.ToBoolean(DataGridConfig.Rows[Convert.ToInt32(activeConfName)].Cells[0].Value) ==
                            false)
                        {
                            MessageBox.Show(@"Не удалось отменить выбор активной конфигурации.", @"SolidWorks",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            DataGridConfig.Rows[Convert.ToInt32(activeConfName)].Cells[0].Value = true;
                        }
                    }
                    else
                    {
                        var isAllChecked =
                            dataGridView.Rows.Cast<DataGridViewRow>().All(row => Convert.ToBoolean(row.Cells[0].Value));
                        //((CheckBox)dataGridView.Controls.Item("SelectAll")).Checked = isAllChecked;
                        ((CheckBox) dataGridView.Controls["SelectAll"]).Checked = isAllChecked;

                        ButtonOK.Enabled = isAllChecked;
                        BtnOkAndClose.Enabled = isAllChecked;
                        if (isChecked == true)
                        {
                            ButtonOK.Enabled = true;
                            BtnOkAndClose.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataGridView_CurrentCellDirtyStateChanged(System.Object sender, System.EventArgs e)
        {
            try
            {
                DataGridView dataGridView = (DataGridView) sender;
                if (dataGridView.CurrentCell is DataGridViewCheckBoxCell)
                {
                    dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        private string _count;
        public string Number
        {
            get { return _count; }
            set { _count = value; }
        }
        private void MatTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                var id = MatTree.SelectedNode.Name;
                var dt = _sqlDll.MaterialsTable();

                PropertyBox.Visible = false;
                label2.Visible = true;

                MaterialName.Text = "";
                KodERP.Text = "";
                Plotnost.Text = "";
                SwProp.Text = "";
                Description.Text = "";
                KodMateriala.Text = "";

                foreach (DataRow LevelID in dt.Rows)
                {
                    if (LevelID["LevelID"].ToString() == id)
                    {
                        MaterialName.Text = LevelID["MaterialsName"].ToString();

                        if (MaterialName.Text == LevelID["MaterialsName"].ToString())
                        {
                            PropertyBox.Visible = true;
                            //PropertyTab.Enabled = true;
                            label2.Visible = false;

                            KodERP.Text = LevelID["ERP"].ToString();
                            Plotnost.Text = LevelID["Density"].ToString();
                            SwProp.Text = LevelID["SwProperty"].ToString();

                            //var matnamecncgetvalue = dt.Rows[0][MatNameCNC].ToString(); LevelID["MaterialsName"].ToString()
                            //Number = matnamecncgetvalue;

                            Description.Text = LevelID["Description"].ToString();
                            KodMateriala.Text = LevelID["CodeMaterial"].ToString();

                            //TxtObrazec.Text = dt.Rows[0][hatch].ToString();
                            //TxtBoxAngel.Text = dt.Rows[0][angle].ToString();
                            //TxtDens.Text = dt.Rows[0][scale].ToString();
                            //TxtPwShader.Text = dt.Rows[0][pwshader].ToString();
                            //TxtPath.Text = dt.Rows[0][path].ToString();
                            //TxtBoxColorCod.Text = dt.Rows[0][RGB].ToString();
                        }
                        else
                        {
                            PropertyBox.Visible = false;
                            label2.Visible = true;

                            MaterialName.Text = "";
                            KodERP.Text = "";
                            Plotnost.Text = "";
                            SwProp.Text = "";
                            Description.Text = "";
                            KodMateriala.Text = "";

                            //TxtObrazec.Text = "";
                            //TxtBoxAngel.Text = "";
                            //TxtDens.Text = "";
                            //TxtPwShader.Text = "";
                            //TxtPath.Text = "";
                            //TxtBoxColorCod.Text = "";
                            //MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void ButtonOK_Click(object sender, EventArgs e)
        {
            try
            {
                SetMaterial();
                LoadDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnOkAndClose_Click(object sender, EventArgs e)
        {
            SetMaterial();
            Close();
        }
        public void SetMaterial()
        {
            try
            {
                ToSQL.Conn = _con.Con;

                if (MatTree.SelectedNode == null)
                {
                    MessageBox.Show(@"Выберите материал для применения!");
                    return;
                }

                var id = MatTree.SelectedNode.Name;

                dynamic checkedRows = (from rows in DataGridConfig.Rows.Cast<DataGridViewRow>()
                    where Convert.ToBoolean(rows.Cells[0].Value.ToString())
                    select rows).ToList();

                foreach (DataGridViewRow row in checkedRows)
                {
                    var confname = row.Cells[1].Value.ToString();

                    _checkModelClass.SplitConfigurations(confname);

                    if (_checkModelClass.CheckSheetMetal())
                    {
                        string sheetMetalMessage;
                        _matDll.CheckSheetMetalProperty(confname, null, out sheetMetalMessage);
                        if (!(string.IsNullOrEmpty(sheetMetalMessage)))
                        {
                            MessageBox.Show(sheetMetalMessage);
                        }
                    }
                    _matDll.ApplyMaterial("", confname, Convert.ToInt32(id), null);
                }

                foreach (DataGridViewRow row in checkedRows)
                {
                    var confname = row.Cells[1].Value.ToString();

                    _matDll.SetColor(confname, Convert.ToString(CboColor.SelectedValue),
                        Convert.ToString(CboType.SelectedValue), Convert.ToString(CboClass.SelectedValue), null);
                }

                foreach (var confname in _matDll.GetConfigurationNames())
                {
                    foreach (var matname in _matDll.GetCustomProperty(confname, null))
                    {
                        //TODO: входная строка, MaterialsDLL
                        _sqlDll.AddCustomProperty(confname, matname.Name, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "; " + ex.StackTrace);
            }
        }
        //функция поиска в дереве.Если ничего не найдено - возвращает null
        private TreeNode SearchNode(string SearchText, TreeNode StartNode)
        {
            TreeNode node = null;
            while (StartNode != null)
            {
                if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
                {
                    node = StartNode; //что-то нашли, выходим
                    break;
                };
                if (StartNode.Nodes.Count != 0) //у узла есть дочерние элементы
                {
                    node = SearchNode(SearchText, StartNode.Nodes[0]);//ищем рекурсивно в дочерних
                    if (node != null)
                    {
                        break;//чето нашли
                    };
                };
                StartNode = StartNode.NextNode;
            };
            return node;//вернули результат поиска
        }
        private void txtBoxSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchText = this.txtBoxSearch.Text;
                if (SearchText == "")
                {
                    MatTree.Nodes.Clear();
                    _sqlDll.PopulateTreeView("1", "Материалы", null, MatTree);
                    return;
                };
                TreeNode SelectedNode = SearchNode(SearchText, MatTree.Nodes[0]);//пытаемся найти в поле Text
                if (SelectedNode != null)
                {
                    //нашли, выделяем...
                    this.MatTree.SelectedNode = SelectedNode;
                    this.MatTree.SelectedNode.Expand();
                    MatTree.SelectedNode.BackColor = Color.Yellow;
                    //this.MatTree.Select();
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ";\n" + ex.StackTrace);
            }
        }
        string searchStr(TextBox txtBox, string textBoxText)
        {
            var result = "";
            if (textBoxText == "")
            {
                result = "Поиск...";
                txtBox.ForeColor = Color.Gray;
            }
            else
            {
                result = txtBox.Text;
            }
            if (textBoxText == "Поиск...")
            {
                result = "";
                txtBox.ForeColor = Color.Black;
            }
            return result;
        }
        private void txtBoxSearch_Enter(object sender, EventArgs e)
        {
            txtBoxSearch.Text = searchStr(txtBoxSearch, txtBoxSearch.Text);
        }
        private void txtBoxSearch_Leave(object sender, EventArgs e)
        {
            txtBoxSearch.Text = searchStr(txtBoxSearch, txtBoxSearch.Text);
        }
    }
}