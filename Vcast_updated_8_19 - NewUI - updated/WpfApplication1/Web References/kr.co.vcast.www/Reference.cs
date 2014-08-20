﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.18052.
// 
#pragma warning disable 1591

namespace WpfApplication1.kr.co.vcast.www {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="VCastServiceSoap", Namespace="http://tempuri.org/")]
    public partial class VCastService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback LoginOperationCompleted;
        
        private System.Threading.SendOrPostCallback LectureListOperationCompleted;
        
        private System.Threading.SendOrPostCallback LectureModuleAddOperationCompleted;
        
        private System.Threading.SendOrPostCallback LectureModuleFinishOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public VCastService() {
            this.Url = global::WpfApplication1.Properties.Settings.Default.WpfApplication1_kr_co_vcast_www_VCastService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event LoginCompletedEventHandler LoginCompleted;
        
        /// <remarks/>
        public event LectureListCompletedEventHandler LectureListCompleted;
        
        /// <remarks/>
        public event LectureModuleAddCompletedEventHandler LectureModuleAddCompleted;
        
        /// <remarks/>
        public event LectureModuleFinishCompletedEventHandler LectureModuleFinishCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Login", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Login(string MemberID, string Password) {
            object[] results = this.Invoke("Login", new object[] {
                        MemberID,
                        Password});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void LoginAsync(string MemberID, string Password) {
            this.LoginAsync(MemberID, Password, null);
        }
        
        /// <remarks/>
        public void LoginAsync(string MemberID, string Password, object userState) {
            if ((this.LoginOperationCompleted == null)) {
                this.LoginOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLoginOperationCompleted);
            }
            this.InvokeAsync("Login", new object[] {
                        MemberID,
                        Password}, this.LoginOperationCompleted, userState);
        }
        
        private void OnLoginOperationCompleted(object arg) {
            if ((this.LoginCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LoginCompleted(this, new LoginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/LectureList", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string LectureList(int CompanySeq, int MemberSeq) {
            object[] results = this.Invoke("LectureList", new object[] {
                        CompanySeq,
                        MemberSeq});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void LectureListAsync(int CompanySeq, int MemberSeq) {
            this.LectureListAsync(CompanySeq, MemberSeq, null);
        }
        
        /// <remarks/>
        public void LectureListAsync(int CompanySeq, int MemberSeq, object userState) {
            if ((this.LectureListOperationCompleted == null)) {
                this.LectureListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLectureListOperationCompleted);
            }
            this.InvokeAsync("LectureList", new object[] {
                        CompanySeq,
                        MemberSeq}, this.LectureListOperationCompleted, userState);
        }
        
        private void OnLectureListOperationCompleted(object arg) {
            if ((this.LectureListCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LectureListCompleted(this, new LectureListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/LectureModuleAdd", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int LectureModuleAdd(int LectureSeq, int MemberSeq, string ModuleTitle, int PlayTime) {
            object[] results = this.Invoke("LectureModuleAdd", new object[] {
                        LectureSeq,
                        MemberSeq,
                        ModuleTitle,
                        PlayTime});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void LectureModuleAddAsync(int LectureSeq, int MemberSeq, string ModuleTitle, int PlayTime) {
            this.LectureModuleAddAsync(LectureSeq, MemberSeq, ModuleTitle, PlayTime, null);
        }
        
        /// <remarks/>
        public void LectureModuleAddAsync(int LectureSeq, int MemberSeq, string ModuleTitle, int PlayTime, object userState) {
            if ((this.LectureModuleAddOperationCompleted == null)) {
                this.LectureModuleAddOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLectureModuleAddOperationCompleted);
            }
            this.InvokeAsync("LectureModuleAdd", new object[] {
                        LectureSeq,
                        MemberSeq,
                        ModuleTitle,
                        PlayTime}, this.LectureModuleAddOperationCompleted, userState);
        }
        
        private void OnLectureModuleAddOperationCompleted(object arg) {
            if ((this.LectureModuleAddCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LectureModuleAddCompleted(this, new LectureModuleAddCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/LectureModuleFinish", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void LectureModuleFinish(int LectureSeq, int LectureModuleSeq) {
            this.Invoke("LectureModuleFinish", new object[] {
                        LectureSeq,
                        LectureModuleSeq});
        }
        
        /// <remarks/>
        public void LectureModuleFinishAsync(int LectureSeq, int LectureModuleSeq) {
            this.LectureModuleFinishAsync(LectureSeq, LectureModuleSeq, null);
        }
        
        /// <remarks/>
        public void LectureModuleFinishAsync(int LectureSeq, int LectureModuleSeq, object userState) {
            if ((this.LectureModuleFinishOperationCompleted == null)) {
                this.LectureModuleFinishOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLectureModuleFinishOperationCompleted);
            }
            this.InvokeAsync("LectureModuleFinish", new object[] {
                        LectureSeq,
                        LectureModuleSeq}, this.LectureModuleFinishOperationCompleted, userState);
        }
        
        private void OnLectureModuleFinishOperationCompleted(object arg) {
            if ((this.LectureModuleFinishCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LectureModuleFinishCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void LoginCompletedEventHandler(object sender, LoginCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LoginCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LoginCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void LectureListCompletedEventHandler(object sender, LectureListCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LectureListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LectureListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void LectureModuleAddCompletedEventHandler(object sender, LectureModuleAddCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LectureModuleAddCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LectureModuleAddCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void LectureModuleFinishCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591