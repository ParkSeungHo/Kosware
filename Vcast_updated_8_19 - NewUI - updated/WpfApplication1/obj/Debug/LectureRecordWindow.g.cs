﻿#pragma checksum "..\..\LectureRecordWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "49765DDDC99DB0223553A6664AE00EC8"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.34014
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
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
using System.Windows.Forms;
using System.Windows.Forms.Integration;
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


namespace WpfApplication1 {
    
    
    /// <summary>
    /// LectureRecordWindow
    /// </summary>
    public partial class LectureRecordWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\LectureRecordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ImageBrush imgBrushBg;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\LectureRecordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid_avatar;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\LectureRecordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Forms.Integration.WindowsFormsHost windowsFormsHost1;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\LectureRecordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid_ppt;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\LectureRecordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Forms.Integration.WindowsFormsHost windowsFormsHost2;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\LectureRecordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgClose;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApplication1;component/lecturerecordwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\LectureRecordWindow.xaml"
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
            
            #line 5 "..\..\LectureRecordWindow.xaml"
            ((WpfApplication1.LectureRecordWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 5 "..\..\LectureRecordWindow.xaml"
            ((WpfApplication1.LectureRecordWindow)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Window_KeyDown);
            
            #line default
            #line hidden
            
            #line 5 "..\..\LectureRecordWindow.xaml"
            ((WpfApplication1.LectureRecordWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.imgBrushBg = ((System.Windows.Media.ImageBrush)(target));
            return;
            case 3:
            this.grid_avatar = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.windowsFormsHost1 = ((System.Windows.Forms.Integration.WindowsFormsHost)(target));
            return;
            case 5:
            this.grid_ppt = ((System.Windows.Controls.Grid)(target));
            
            #line 14 "..\..\LectureRecordWindow.xaml"
            this.grid_ppt.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.grid_ppt_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.windowsFormsHost2 = ((System.Windows.Forms.Integration.WindowsFormsHost)(target));
            return;
            case 7:
            this.imgClose = ((System.Windows.Controls.Image)(target));
            
            #line 17 "..\..\LectureRecordWindow.xaml"
            this.imgClose.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.imgClose_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

