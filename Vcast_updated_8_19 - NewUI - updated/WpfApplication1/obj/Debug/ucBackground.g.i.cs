﻿#pragma checksum "..\..\ucBackground.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "47FCDFF7D20D6CF84016D8D24F61A820"
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
    /// ucBackground
    /// </summary>
    public partial class ucBackground : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\ucBackground.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid_background;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\ucBackground.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img_prev;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\ucBackground.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img_leftboard;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\ucBackground.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img_left;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\ucBackground.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img_selectedboard;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\ucBackground.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img_selected;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\ucBackground.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img_rightboard;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\ucBackground.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img_right;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\ucBackground.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img_next;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\ucBackground.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img_ok;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApplication1;component/ucbackground.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ucBackground.xaml"
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
            this.grid_background = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.img_prev = ((System.Windows.Controls.Image)(target));
            
            #line 12 "..\..\ucBackground.xaml"
            this.img_prev.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.imgNavi_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.img_leftboard = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.img_left = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.img_selectedboard = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            this.img_selected = ((System.Windows.Controls.Image)(target));
            return;
            case 7:
            this.img_rightboard = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            this.img_right = ((System.Windows.Controls.Image)(target));
            return;
            case 9:
            this.img_next = ((System.Windows.Controls.Image)(target));
            
            #line 19 "..\..\ucBackground.xaml"
            this.img_next.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.imgNavi_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 10:
            this.img_ok = ((System.Windows.Controls.Image)(target));
            
            #line 21 "..\..\ucBackground.xaml"
            this.img_ok.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.img_ok_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

