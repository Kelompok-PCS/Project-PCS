﻿#pragma checksum "..\..\..\Master Promo\Menu_Promo.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "39B0D9BF48C9E38C8D8F4202E456061EBCA319D5EDFB7591C130066D90586D66"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Project.Master_menu;
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


namespace Project {
    
    
    /// <summary>
    /// Menu_Promo
    /// </summary>
    public partial class Menu_Promo : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\Master Promo\Menu_Promo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFilter;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Master Promo\Menu_Promo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbFilter;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Master Promo\Menu_Promo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnInsert;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Master Promo\Menu_Promo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid gridMenu;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Master Promo\Menu_Promo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid gridPurgatory;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Master Promo\Menu_Promo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbKode;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Master Promo\Menu_Promo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbNama;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Master Promo\Menu_Promo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEdit;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Master Promo\Menu_Promo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rdDelete;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Master Promo\Menu_Promo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rdUpdate;
        
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
            System.Uri resourceLocater = new System.Uri("/Project;component/master%20promo/menu_promo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Master Promo\Menu_Promo.xaml"
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
            
            #line 9 "..\..\..\Master Promo\Menu_Promo.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.Grid_MouseWheel);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnFilter = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.tbFilter = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.btnInsert = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\Master Promo\Menu_Promo.xaml"
            this.btnInsert.Click += new System.Windows.RoutedEventHandler(this.btnInsert_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.gridMenu = ((System.Windows.Controls.DataGrid)(target));
            
            #line 16 "..\..\..\Master Promo\Menu_Promo.xaml"
            this.gridMenu.Loaded += new System.Windows.RoutedEventHandler(this.gridMenu_Loaded);
            
            #line default
            #line hidden
            
            #line 16 "..\..\..\Master Promo\Menu_Promo.xaml"
            this.gridMenu.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.gridMenu_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.gridPurgatory = ((System.Windows.Controls.DataGrid)(target));
            
            #line 17 "..\..\..\Master Promo\Menu_Promo.xaml"
            this.gridPurgatory.Loaded += new System.Windows.RoutedEventHandler(this.gridPurgatory_Loaded);
            
            #line default
            #line hidden
            return;
            case 7:
            this.lbKode = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.tbNama = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.btnEdit = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\Master Promo\Menu_Promo.xaml"
            this.btnEdit.Click += new System.Windows.RoutedEventHandler(this.btnEdit_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.rdDelete = ((System.Windows.Controls.RadioButton)(target));
            
            #line 22 "..\..\..\Master Promo\Menu_Promo.xaml"
            this.rdDelete.Checked += new System.Windows.RoutedEventHandler(this.rdDelete_Checked);
            
            #line default
            #line hidden
            return;
            case 11:
            this.rdUpdate = ((System.Windows.Controls.RadioButton)(target));
            
            #line 23 "..\..\..\Master Promo\Menu_Promo.xaml"
            this.rdUpdate.Checked += new System.Windows.RoutedEventHandler(this.rdUpdate_Checked);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

