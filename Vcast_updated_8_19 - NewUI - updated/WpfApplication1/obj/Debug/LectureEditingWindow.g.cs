﻿#pragma checksum "..\..\LectureEditingWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B0442FBD7E622C74D9E9DF486CC40E01"
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
    /// LectureEditingWindow
    /// </summary>
    public partial class LectureEditingWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\LectureEditingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid_title;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\LectureEditingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement showMedia;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\LectureEditingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider slider_seek;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\LectureEditingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image_PlayPause;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\LectureEditingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label DurationLabel;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\LectureEditingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lectureSaveList;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\LectureEditingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Directory_Text;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApplication1;component/lectureeditingwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\LectureEditingWindow.xaml"
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
            
            #line 4 "..\..\LectureEditingWindow.xaml"
            ((WpfApplication1.LectureEditingWindow)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Window_KeyDown);
            
            #line default
            #line hidden
            
            #line 4 "..\..\LectureEditingWindow.xaml"
            ((WpfApplication1.LectureEditingWindow)(target)).Closed += new System.EventHandler(this.Window_Closed_1);
            
            #line default
            #line hidden
            
            #line 4 "..\..\LectureEditingWindow.xaml"
            ((WpfApplication1.LectureEditingWindow)(target)).ContentRendered += new System.EventHandler(this.Window_ContentRendered_1);
            
            #line default
            #line hidden
            return;
            case 2:
            this.grid_title = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.showMedia = ((System.Windows.Controls.MediaElement)(target));
            
            #line 18 "..\..\LectureEditingWindow.xaml"
            this.showMedia.MediaOpened += new System.Windows.RoutedEventHandler(this.showMedia_MediaOpened);
            
            #line default
            #line hidden
            return;
            case 4:
            this.slider_seek = ((System.Windows.Controls.Slider)(target));
            return;
            case 5:
            
            #line 24 "..\..\LectureEditingWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseLeftButtonUp_9);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 25 "..\..\LectureEditingWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseLeftButtonUp_4);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 26 "..\..\LectureEditingWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseLeftButtonUp_5);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 27 "..\..\LectureEditingWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseLeftButtonUp_6);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 28 "..\..\LectureEditingWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseLeftButtonUp_7);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 30 "..\..\LectureEditingWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseLeftButtonUp_8);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 31 "..\..\LectureEditingWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseLeftButtonUp_11);
            
            #line default
            #line hidden
            return;
            case 12:
            this.image_PlayPause = ((System.Windows.Controls.Image)(target));
            
            #line 33 "..\..\LectureEditingWindow.xaml"
            this.image_PlayPause.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseLeftButtonUp_2);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 34 "..\..\LectureEditingWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseLeftButtonUp_3);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 35 "..\..\LectureEditingWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseLeftButtonUp_10);
            
            #line default
            #line hidden
            return;
            case 15:
            this.DurationLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 16:
            this.lectureSaveList = ((System.Windows.Controls.ListView)(target));
            
            #line 42 "..\..\LectureEditingWindow.xaml"
            this.lectureSaveList.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.lectureSaveList_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 17:
            this.Directory_Text = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 18:
            
            #line 153 "..\..\LectureEditingWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseLeftButtonUp_1);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 154 "..\..\LectureEditingWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseLeftButtonUp_12);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
