﻿#pragma checksum "..\..\..\VentsBom\BomControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E54FAB53AEABA0BBF6EF133434C3B427"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace VentsBom {
    
    
    /// <summary>
    /// BomControl
    /// </summary>
    public partial class BomControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 38 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl MyTab;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid BomTable1All;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContextMenu ContextMenu;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem ChangeMenuItem;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem DeleteMenuItem;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn NameColumn;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn CountColumn;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn NotesColumn;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox Temp;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton FullBom;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton DivededBom;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Sheets;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid EditRows;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteRow;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddRow;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button GenerateBom;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CountOfRowsLabel;
        
        #line default
        #line hidden
        
        
        #line 193 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem SettingsTab;
        
        #line default
        #line hidden
        
        
        #line 194 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Settings;
        
        #line default
        #line hidden
        
        
        #line 212 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Sp1Textbox;
        
        #line default
        #line hidden
        
        
        #line 220 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Sp2Textbox;
        
        #line default
        #line hidden
        
        
        #line 228 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Sp1TemplateTextbox;
        
        #line default
        #line hidden
        
        
        #line 236 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Sp2TemplateTextbox;
        
        #line default
        #line hidden
        
        
        #line 249 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Gsp1Textbox;
        
        #line default
        #line hidden
        
        
        #line 257 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Gsp2Textbox;
        
        #line default
        #line hidden
        
        
        #line 265 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Gsp1TemplateTextbox;
        
        #line default
        #line hidden
        
        
        #line 273 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Gsp2TemplateTextbox;
        
        #line default
        #line hidden
        
        
        #line 277 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox SettingsOther;
        
        #line default
        #line hidden
        
        
        #line 284 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox АвтопроставлениеПозиций;
        
        #line default
        #line hidden
        
        
        #line 290 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox FontsGroupBox;
        
        #line default
        #line hidden
        
        
        #line 306 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox Italic;
        
        #line default
        #line hidden
        
        
        #line 310 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CharSpasingFactor;
        
        #line default
        #line hidden
        
        
        #line 311 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FontHeight;
        
        #line default
        #line hidden
        
        
        #line 324 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Load;
        
        #line default
        #line hidden
        
        
        #line 326 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddToDocument;
        
        #line default
        #line hidden
        
        
        #line 329 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseButton;
        
        #line default
        #line hidden
        
        
        #line 332 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressBar1;
        
        #line default
        #line hidden
        
        
        #line 333 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CouButton;
        
        #line default
        #line hidden
        
        
        #line 335 "..\..\..\VentsBom\BomControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button GetSpec;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SwSpecification;component/ventsbom/bomcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\VentsBom\BomControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MyTab = ((System.Windows.Controls.TabControl)(target));
            
            #line 39 "..\..\..\VentsBom\BomControl.xaml"
            this.MyTab.Loaded += new System.Windows.RoutedEventHandler(this.TabControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BomTable1All = ((System.Windows.Controls.DataGrid)(target));
            
            #line 52 "..\..\..\VentsBom\BomControl.xaml"
            this.BomTable1All.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.BomTable1All_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 53 "..\..\..\VentsBom\BomControl.xaml"
            this.BomTable1All.LayoutUpdated += new System.EventHandler(this.BomTable1All_LayoutUpdated);
            
            #line default
            #line hidden
            
            #line 53 "..\..\..\VentsBom\BomControl.xaml"
            this.BomTable1All.CurrentCellChanged += new System.EventHandler<System.EventArgs>(this.BomTable1All_CurrentCellChanged);
            
            #line default
            #line hidden
            
            #line 54 "..\..\..\VentsBom\BomControl.xaml"
            this.BomTable1All.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.BomTable1All_PreviewKeyDown);
            
            #line default
            #line hidden
            
            #line 54 "..\..\..\VentsBom\BomControl.xaml"
            this.BomTable1All.ManipulationDelta += new System.EventHandler<System.Windows.Input.ManipulationDeltaEventArgs>(this.BomTable1All_ManipulationDelta);
            
            #line default
            #line hidden
            
            #line 54 "..\..\..\VentsBom\BomControl.xaml"
            this.BomTable1All.ManipulationCompleted += new System.EventHandler<System.Windows.Input.ManipulationCompletedEventArgs>(this.BomTable1All_ManipulationCompleted);
            
            #line default
            #line hidden
            
            #line 54 "..\..\..\VentsBom\BomControl.xaml"
            this.BomTable1All.ManipulationBoundaryFeedback += new System.EventHandler<System.Windows.Input.ManipulationBoundaryFeedbackEventArgs>(this.BomTable1All_ManipulationBoundaryFeedback);
            
            #line default
            #line hidden
            
            #line 54 "..\..\..\VentsBom\BomControl.xaml"
            this.BomTable1All.IsHitTestVisibleChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.BomTable1All_IsHitTestVisibleChanged);
            
            #line default
            #line hidden
            
            #line 54 "..\..\..\VentsBom\BomControl.xaml"
            this.BomTable1All.DragOver += new System.Windows.DragEventHandler(this.BomTable1All_DragOver);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ContextMenu = ((System.Windows.Controls.ContextMenu)(target));
            return;
            case 4:
            
            #line 57 "..\..\..\VentsBom\BomControl.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddNewRow);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 58 "..\..\..\VentsBom\BomControl.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddRow_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ChangeMenuItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 59 "..\..\..\VentsBom\BomControl.xaml"
            this.ChangeMenuItem.Click += new System.Windows.RoutedEventHandler(this.ChangeItem);
            
            #line default
            #line hidden
            return;
            case 7:
            this.DeleteMenuItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 60 "..\..\..\VentsBom\BomControl.xaml"
            this.DeleteMenuItem.Click += new System.Windows.RoutedEventHandler(this.DeleteNewItem);
            
            #line default
            #line hidden
            return;
            case 8:
            this.NameColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 9:
            this.CountColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 10:
            this.NotesColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 11:
            this.Temp = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 12:
            this.FullBom = ((System.Windows.Controls.RadioButton)(target));
            
            #line 116 "..\..\..\VentsBom\BomControl.xaml"
            this.FullBom.Checked += new System.Windows.RoutedEventHandler(this.FullBom_Checked);
            
            #line default
            #line hidden
            
            #line 116 "..\..\..\VentsBom\BomControl.xaml"
            this.FullBom.Unchecked += new System.Windows.RoutedEventHandler(this.FullBom_Unchecked);
            
            #line default
            #line hidden
            return;
            case 13:
            this.DivededBom = ((System.Windows.Controls.RadioButton)(target));
            
            #line 118 "..\..\..\VentsBom\BomControl.xaml"
            this.DivededBom.Checked += new System.Windows.RoutedEventHandler(this.DivededBom_Checked);
            
            #line default
            #line hidden
            
            #line 118 "..\..\..\VentsBom\BomControl.xaml"
            this.DivededBom.Unchecked += new System.Windows.RoutedEventHandler(this.DivededBom_Unchecked);
            
            #line default
            #line hidden
            return;
            case 14:
            this.Sheets = ((System.Windows.Controls.ComboBox)(target));
            
            #line 119 "..\..\..\VentsBom\BomControl.xaml"
            this.Sheets.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Sheets_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 15:
            this.EditRows = ((System.Windows.Controls.Grid)(target));
            return;
            case 16:
            this.DeleteRow = ((System.Windows.Controls.Button)(target));
            
            #line 127 "..\..\..\VentsBom\BomControl.xaml"
            this.DeleteRow.Click += new System.Windows.RoutedEventHandler(this.DeleteRow_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            this.AddRow = ((System.Windows.Controls.Button)(target));
            
            #line 128 "..\..\..\VentsBom\BomControl.xaml"
            this.AddRow.Click += new System.Windows.RoutedEventHandler(this.AddRow_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            this.GenerateBom = ((System.Windows.Controls.Button)(target));
            
            #line 129 "..\..\..\VentsBom\BomControl.xaml"
            this.GenerateBom.Click += new System.Windows.RoutedEventHandler(this.GenerateBom_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            this.CountOfRowsLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 20:
            this.SettingsTab = ((System.Windows.Controls.TabItem)(target));
            return;
            case 21:
            this.Settings = ((System.Windows.Controls.Grid)(target));
            
            #line 194 "..\..\..\VentsBom\BomControl.xaml"
            this.Settings.Loaded += new System.Windows.RoutedEventHandler(this.Settings_Loaded);
            
            #line default
            #line hidden
            return;
            case 22:
            this.Sp1Textbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 212 "..\..\..\VentsBom\BomControl.xaml"
            this.Sp1Textbox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Sp1Textbox_TextChanged);
            
            #line default
            #line hidden
            
            #line 212 "..\..\..\VentsBom\BomControl.xaml"
            this.Sp1Textbox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.Sp1Textbox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 23:
            this.Sp2Textbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 220 "..\..\..\VentsBom\BomControl.xaml"
            this.Sp2Textbox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.Sp1Textbox_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 220 "..\..\..\VentsBom\BomControl.xaml"
            this.Sp2Textbox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Sp2Textbox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 24:
            this.Sp1TemplateTextbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 228 "..\..\..\VentsBom\BomControl.xaml"
            this.Sp1TemplateTextbox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Sp1TemplateTextbox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 25:
            this.Sp2TemplateTextbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 236 "..\..\..\VentsBom\BomControl.xaml"
            this.Sp2TemplateTextbox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Sp2TemplateTextbox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 26:
            this.Gsp1Textbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 249 "..\..\..\VentsBom\BomControl.xaml"
            this.Gsp1Textbox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Sp1Textbox_TextChanged);
            
            #line default
            #line hidden
            
            #line 249 "..\..\..\VentsBom\BomControl.xaml"
            this.Gsp1Textbox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.Sp1Textbox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 27:
            this.Gsp2Textbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 257 "..\..\..\VentsBom\BomControl.xaml"
            this.Gsp2Textbox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.Sp1Textbox_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 257 "..\..\..\VentsBom\BomControl.xaml"
            this.Gsp2Textbox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Sp2Textbox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 28:
            this.Gsp1TemplateTextbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 265 "..\..\..\VentsBom\BomControl.xaml"
            this.Gsp1TemplateTextbox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Sp1TemplateTextbox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 29:
            this.Gsp2TemplateTextbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 273 "..\..\..\VentsBom\BomControl.xaml"
            this.Gsp2TemplateTextbox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Sp2TemplateTextbox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 30:
            this.SettingsOther = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 31:
            this.АвтопроставлениеПозиций = ((System.Windows.Controls.CheckBox)(target));
            
            #line 286 "..\..\..\VentsBom\BomControl.xaml"
            this.АвтопроставлениеПозиций.Checked += new System.Windows.RoutedEventHandler(this.АвтопроставлениеПозиций_Checked);
            
            #line default
            #line hidden
            
            #line 286 "..\..\..\VentsBom\BomControl.xaml"
            this.АвтопроставлениеПозиций.Unchecked += new System.Windows.RoutedEventHandler(this.АвтопроставлениеПозиций_Unchecked);
            
            #line default
            #line hidden
            return;
            case 32:
            this.FontsGroupBox = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 33:
            this.Italic = ((System.Windows.Controls.CheckBox)(target));
            
            #line 308 "..\..\..\VentsBom\BomControl.xaml"
            this.Italic.Checked += new System.Windows.RoutedEventHandler(this.АвтопроставлениеПозиций_Checked);
            
            #line default
            #line hidden
            
            #line 308 "..\..\..\VentsBom\BomControl.xaml"
            this.Italic.Unchecked += new System.Windows.RoutedEventHandler(this.АвтопроставлениеПозиций_Unchecked);
            
            #line default
            #line hidden
            return;
            case 34:
            this.CharSpasingFactor = ((System.Windows.Controls.TextBox)(target));
            return;
            case 35:
            this.FontHeight = ((System.Windows.Controls.TextBox)(target));
            return;
            case 36:
            this.Load = ((System.Windows.Controls.Button)(target));
            
            #line 325 "..\..\..\VentsBom\BomControl.xaml"
            this.Load.Click += new System.Windows.RoutedEventHandler(this.Load_Click);
            
            #line default
            #line hidden
            return;
            case 37:
            this.AddToDocument = ((System.Windows.Controls.Button)(target));
            
            #line 327 "..\..\..\VentsBom\BomControl.xaml"
            this.AddToDocument.Click += new System.Windows.RoutedEventHandler(this.AddToDocument_Click);
            
            #line default
            #line hidden
            return;
            case 38:
            this.CloseButton = ((System.Windows.Controls.Button)(target));
            
            #line 331 "..\..\..\VentsBom\BomControl.xaml"
            this.CloseButton.Click += new System.Windows.RoutedEventHandler(this.CloseButton_Click);
            
            #line default
            #line hidden
            return;
            case 39:
            this.ProgressBar1 = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 40:
            this.CouButton = ((System.Windows.Controls.Button)(target));
            
            #line 333 "..\..\..\VentsBom\BomControl.xaml"
            this.CouButton.Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 41:
            this.GetSpec = ((System.Windows.Controls.Button)(target));
            
            #line 335 "..\..\..\VentsBom\BomControl.xaml"
            this.GetSpec.Click += new System.Windows.RoutedEventHandler(this.GetSpec_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

