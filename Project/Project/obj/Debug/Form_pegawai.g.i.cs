﻿#pragma checksum "..\..\Form_pegawai.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "161ABF0317571668C3808FC5B82CD054DCA8563A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Project;
using RootLibrary.WPF.Localization;
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
    /// Form_pegawai
    /// </summary>
    public partial class Form_pegawai : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 156 "..\..\Form_pegawai.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas canvas;
        
        #line default
        #line hidden
        
        
        #line 158 "..\..\Form_pegawai.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbNamaPeg;
        
        #line default
        #line hidden
        
        
        #line 160 "..\..\Form_pegawai.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbPemesan;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\Form_pegawai.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbMenu;
        
        #line default
        #line hidden
        
        
        #line 162 "..\..\Form_pegawai.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbPendaftaran;
        
        #line default
        #line hidden
        
        
        #line 163 "..\..\Form_pegawai.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbPendaftaran_Copy;
        
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
            System.Uri resourceLocater = new System.Uri("/Project;component/form_pegawai.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Form_pegawai.xaml"
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
            this.canvas = ((System.Windows.Controls.Canvas)(target));
            return;
            case 2:
            this.tbNamaPeg = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            
            #line 159 "..\..\Form_pegawai.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TextBlock_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tbPemesan = ((System.Windows.Controls.TextBlock)(target));
            
            #line 160 "..\..\Form_pegawai.xaml"
            this.tbPemesan.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.tbPemesan_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbMenu = ((System.Windows.Controls.TextBlock)(target));
            
            #line 161 "..\..\Form_pegawai.xaml"
            this.tbMenu.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TbMenu_MouseDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tbPendaftaran = ((System.Windows.Controls.TextBlock)(target));
            
            #line 162 "..\..\Form_pegawai.xaml"
            this.tbPendaftaran.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TbPendaftaran_MouseDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.tbPendaftaran_Copy = ((System.Windows.Controls.TextBlock)(target));
            
            #line 163 "..\..\Form_pegawai.xaml"
            this.tbPendaftaran_Copy.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Struk_mouseDown);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 165 "..\..\Form_pegawai.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

