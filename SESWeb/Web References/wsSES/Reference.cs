﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace SESWeb.wsSES {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4161.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ServiceSheetFileDetailsFromWEBPORTAL_OutBinding", Namespace="urn:TARGET:WEBPORTAL:ServiceSheetFileDetails")]
    public partial class ServiceSheetFileDetailsFromWEBPORTAL_OutService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ServiceSheetFileDetailsFromWEBPORTAL_OutOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ServiceSheetFileDetailsFromWEBPORTAL_OutService() {
            this.Url = global::SESWeb.Properties.Settings.Default.SESWeb_wsSES_ServiceSheetFileDetailsFromWEBPORTAL_OutService;
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
        public event ServiceSheetFileDetailsFromWEBPORTAL_OutCompletedEventHandler ServiceSheetFileDetailsFromWEBPORTAL_OutCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("ServiceSheetFileDetailsFromWEBPORTAL_Response", Namespace="urn:TARGET:WEBPORTAL:ServiceSheetFileDetails")]
        public ServiceSheetFileDetailsFromWEBPORTAL_Response ServiceSheetFileDetailsFromWEBPORTAL_Out([System.Xml.Serialization.XmlElementAttribute(Namespace="urn:TARGET:WEBPORTAL:ServiceSheetFileDetails")] ServiceSheetFileDetailsFromWEBPORTAL_Request ServiceSheetFileDetailsFromWEBPORTAL_Request) {
            object[] results = this.Invoke("ServiceSheetFileDetailsFromWEBPORTAL_Out", new object[] {
                        ServiceSheetFileDetailsFromWEBPORTAL_Request});
            return ((ServiceSheetFileDetailsFromWEBPORTAL_Response)(results[0]));
        }
        
        /// <remarks/>
        public void ServiceSheetFileDetailsFromWEBPORTAL_OutAsync(ServiceSheetFileDetailsFromWEBPORTAL_Request ServiceSheetFileDetailsFromWEBPORTAL_Request) {
            this.ServiceSheetFileDetailsFromWEBPORTAL_OutAsync(ServiceSheetFileDetailsFromWEBPORTAL_Request, null);
        }
        
        /// <remarks/>
        public void ServiceSheetFileDetailsFromWEBPORTAL_OutAsync(ServiceSheetFileDetailsFromWEBPORTAL_Request ServiceSheetFileDetailsFromWEBPORTAL_Request, object userState) {
            if ((this.ServiceSheetFileDetailsFromWEBPORTAL_OutOperationCompleted == null)) {
                this.ServiceSheetFileDetailsFromWEBPORTAL_OutOperationCompleted = new System.Threading.SendOrPostCallback(this.OnServiceSheetFileDetailsFromWEBPORTAL_OutOperationCompleted);
            }
            this.InvokeAsync("ServiceSheetFileDetailsFromWEBPORTAL_Out", new object[] {
                        ServiceSheetFileDetailsFromWEBPORTAL_Request}, this.ServiceSheetFileDetailsFromWEBPORTAL_OutOperationCompleted, userState);
        }
        
        private void OnServiceSheetFileDetailsFromWEBPORTAL_OutOperationCompleted(object arg) {
            if ((this.ServiceSheetFileDetailsFromWEBPORTAL_OutCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ServiceSheetFileDetailsFromWEBPORTAL_OutCompleted(this, new ServiceSheetFileDetailsFromWEBPORTAL_OutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4161.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:TARGET:WEBPORTAL:ServiceSheetFileDetails")]
    public partial class ServiceSheetFileDetailsFromWEBPORTAL_Request {
        
        private ServiceSheetFileDetailsFromWEBPORTAL_RequestRow[] requestField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Row", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ServiceSheetFileDetailsFromWEBPORTAL_RequestRow[] Request {
            get {
                return this.requestField;
            }
            set {
                this.requestField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4161.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:TARGET:WEBPORTAL:ServiceSheetFileDetails")]
    public partial class ServiceSheetFileDetailsFromWEBPORTAL_RequestRow {
        
        private string lBLNIField;
        
        private string lWERTField;
        
        private string bLDATField;
        
        private string iNVOICE_RECEIPT_DATEField;
        
        private string fILENAMEField;
        
        private byte[] fILEDATAField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LBLNI {
            get {
                return this.lBLNIField;
            }
            set {
                this.lBLNIField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LWERT {
            get {
                return this.lWERTField;
            }
            set {
                this.lWERTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BLDAT {
            get {
                return this.bLDATField;
            }
            set {
                this.bLDATField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string INVOICE_RECEIPT_DATE {
            get {
                return this.iNVOICE_RECEIPT_DATEField;
            }
            set {
                this.iNVOICE_RECEIPT_DATEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FILENAME {
            get {
                return this.fILENAMEField;
            }
            set {
                this.fILENAMEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary")]
        public byte[] FILEDATA {
            get {
                return this.fILEDATAField;
            }
            set {
                this.fILEDATAField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4161.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:TARGET:WEBPORTAL:ServiceSheetFileDetails")]
    public partial class ServiceSheetFileDetailsFromWEBPORTAL_Response {
        
        private ServiceSheetFileDetailsFromWEBPORTAL_ResponseRow[] responseField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Row", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ServiceSheetFileDetailsFromWEBPORTAL_ResponseRow[] Response {
            get {
                return this.responseField;
            }
            set {
                this.responseField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4161.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:TARGET:WEBPORTAL:ServiceSheetFileDetails")]
    public partial class ServiceSheetFileDetailsFromWEBPORTAL_ResponseRow {
        
        private string lBLNIField;
        
        private string lWERTField;
        
        private string bLDATField;
        
        private string iNVOICE_RECEIPT_DATEField;
        
        private string fILENAMEField;
        
        private string sAPINDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LBLNI {
            get {
                return this.lBLNIField;
            }
            set {
                this.lBLNIField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LWERT {
            get {
                return this.lWERTField;
            }
            set {
                this.lWERTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BLDAT {
            get {
                return this.bLDATField;
            }
            set {
                this.bLDATField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string INVOICE_RECEIPT_DATE {
            get {
                return this.iNVOICE_RECEIPT_DATEField;
            }
            set {
                this.iNVOICE_RECEIPT_DATEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FILENAME {
            get {
                return this.fILENAMEField;
            }
            set {
                this.fILENAMEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAPIND {
            get {
                return this.sAPINDField;
            }
            set {
                this.sAPINDField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4161.0")]
    public delegate void ServiceSheetFileDetailsFromWEBPORTAL_OutCompletedEventHandler(object sender, ServiceSheetFileDetailsFromWEBPORTAL_OutCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4161.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ServiceSheetFileDetailsFromWEBPORTAL_OutCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ServiceSheetFileDetailsFromWEBPORTAL_OutCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ServiceSheetFileDetailsFromWEBPORTAL_Response Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ServiceSheetFileDetailsFromWEBPORTAL_Response)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591