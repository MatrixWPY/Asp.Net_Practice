﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.42000
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.DBServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="QueryBaseModel", Namespace="http://schemas.datacontract.org/2004/07/WcfService.Model")]
    [System.SerializableAttribute()]
    public partial class QueryBaseModel : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WebAPI.DBServiceReference.QueryBaseModel.DataTableBase DataTableParamField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<string, object> QueryParamField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WebAPI.DBServiceReference.QueryBaseModel.DataTableBase DataTableParam {
            get {
                return this.DataTableParamField;
            }
            set {
                if ((object.ReferenceEquals(this.DataTableParamField, value) != true)) {
                    this.DataTableParamField = value;
                    this.RaisePropertyChanged("DataTableParam");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<string, object> QueryParam {
            get {
                return this.QueryParamField;
            }
            set {
                if ((object.ReferenceEquals(this.QueryParamField, value) != true)) {
                    this.QueryParamField = value;
                    this.RaisePropertyChanged("QueryParam");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
        [System.Runtime.Serialization.DataContractAttribute(Name="QueryBaseModel.DataTableBase", Namespace="http://schemas.datacontract.org/2004/07/WcfService.Model")]
        [System.SerializableAttribute()]
        public partial class DataTableBase : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
            
            [System.NonSerializedAttribute()]
            private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
            
            [System.Runtime.Serialization.OptionalFieldAttribute()]
            private string OrderColumnField;
            
            [System.Runtime.Serialization.OptionalFieldAttribute()]
            private string OrderDirField;
            
            [System.Runtime.Serialization.OptionalFieldAttribute()]
            private System.Nullable<int> PageRowCntField;
            
            [System.Runtime.Serialization.OptionalFieldAttribute()]
            private System.Nullable<int> PageStartRowField;
            
            public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
                get {
                    return this.extensionDataField;
                }
                set {
                    this.extensionDataField = value;
                }
            }
            
            [System.Runtime.Serialization.DataMemberAttribute()]
            public string OrderColumn {
                get {
                    return this.OrderColumnField;
                }
                set {
                    if ((object.ReferenceEquals(this.OrderColumnField, value) != true)) {
                        this.OrderColumnField = value;
                        this.RaisePropertyChanged("OrderColumn");
                    }
                }
            }
            
            [System.Runtime.Serialization.DataMemberAttribute()]
            public string OrderDir {
                get {
                    return this.OrderDirField;
                }
                set {
                    if ((object.ReferenceEquals(this.OrderDirField, value) != true)) {
                        this.OrderDirField = value;
                        this.RaisePropertyChanged("OrderDir");
                    }
                }
            }
            
            [System.Runtime.Serialization.DataMemberAttribute()]
            public System.Nullable<int> PageRowCnt {
                get {
                    return this.PageRowCntField;
                }
                set {
                    if ((this.PageRowCntField.Equals(value) != true)) {
                        this.PageRowCntField = value;
                        this.RaisePropertyChanged("PageRowCnt");
                    }
                }
            }
            
            [System.Runtime.Serialization.DataMemberAttribute()]
            public System.Nullable<int> PageStartRow {
                get {
                    return this.PageStartRowField;
                }
                set {
                    if ((this.PageStartRowField.Equals(value) != true)) {
                        this.PageStartRowField = value;
                        this.RaisePropertyChanged("PageStartRow");
                    }
                }
            }
            
            public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
            
            protected void RaisePropertyChanged(string propertyName) {
                System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
                if ((propertyChanged != null)) {
                    propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ContactInfoExModel", Namespace="http://schemas.datacontract.org/2004/07/WcfService.Model")]
    [System.SerializableAttribute()]
    public partial class ContactInfoExModel : WebAPI.DBServiceReference.ContactInfoModel {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TotalCountField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TotalCount {
            get {
                return this.TotalCountField;
            }
            set {
                if ((this.TotalCountField.Equals(value) != true)) {
                    this.TotalCountField = value;
                    this.RaisePropertyChanged("TotalCount");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ContactInfoModel", Namespace="http://schemas.datacontract.org/2004/07/WcfService.Model")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(WebAPI.DBServiceReference.ContactInfoExModel))]
    public partial class ContactInfoModel : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> AgeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long ContactInfoIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<WebAPI.DBServiceReference.ContactInfoModel.EnumGender> GenderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsEnableField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NicknameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhoneNoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> UpdateTimeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address {
            get {
                return this.AddressField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressField, value) != true)) {
                    this.AddressField = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Age {
            get {
                return this.AgeField;
            }
            set {
                if ((this.AgeField.Equals(value) != true)) {
                    this.AgeField = value;
                    this.RaisePropertyChanged("Age");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long ContactInfoID {
            get {
                return this.ContactInfoIDField;
            }
            set {
                if ((this.ContactInfoIDField.Equals(value) != true)) {
                    this.ContactInfoIDField = value;
                    this.RaisePropertyChanged("ContactInfoID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreateTime {
            get {
                return this.CreateTimeField;
            }
            set {
                if ((this.CreateTimeField.Equals(value) != true)) {
                    this.CreateTimeField = value;
                    this.RaisePropertyChanged("CreateTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<WebAPI.DBServiceReference.ContactInfoModel.EnumGender> Gender {
            get {
                return this.GenderField;
            }
            set {
                if ((this.GenderField.Equals(value) != true)) {
                    this.GenderField = value;
                    this.RaisePropertyChanged("Gender");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsEnable {
            get {
                return this.IsEnableField;
            }
            set {
                if ((this.IsEnableField.Equals(value) != true)) {
                    this.IsEnableField = value;
                    this.RaisePropertyChanged("IsEnable");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Nickname {
            get {
                return this.NicknameField;
            }
            set {
                if ((object.ReferenceEquals(this.NicknameField, value) != true)) {
                    this.NicknameField = value;
                    this.RaisePropertyChanged("Nickname");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhoneNo {
            get {
                return this.PhoneNoField;
            }
            set {
                if ((object.ReferenceEquals(this.PhoneNoField, value) != true)) {
                    this.PhoneNoField = value;
                    this.RaisePropertyChanged("PhoneNo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> UpdateTime {
            get {
                return this.UpdateTimeField;
            }
            set {
                if ((this.UpdateTimeField.Equals(value) != true)) {
                    this.UpdateTimeField = value;
                    this.RaisePropertyChanged("UpdateTime");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
        [System.Runtime.Serialization.DataContractAttribute(Name="ContactInfoModel.EnumGender", Namespace="http://schemas.datacontract.org/2004/07/WcfService.Model")]
        public enum EnumGender : int {
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            Female = 0,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            Male = 1,
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DBServiceReference.IDBService")]
    public interface IDBService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/GetContactInfoByCondition", ReplyAction="http://tempuri.org/IDBService/GetContactInfoByConditionResponse")]
        WebAPI.DBServiceReference.ContactInfoExModel[] GetContactInfoByCondition(WebAPI.DBServiceReference.QueryBaseModel objQueryBaseModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/GetContactInfoByCondition", ReplyAction="http://tempuri.org/IDBService/GetContactInfoByConditionResponse")]
        System.Threading.Tasks.Task<WebAPI.DBServiceReference.ContactInfoExModel[]> GetContactInfoByConditionAsync(WebAPI.DBServiceReference.QueryBaseModel objQueryBaseModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/GetContactInfo", ReplyAction="http://tempuri.org/IDBService/GetContactInfoResponse")]
        WebAPI.DBServiceReference.ContactInfoModel GetContactInfo(long lContactInfoID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/GetContactInfo", ReplyAction="http://tempuri.org/IDBService/GetContactInfoResponse")]
        System.Threading.Tasks.Task<WebAPI.DBServiceReference.ContactInfoModel> GetContactInfoAsync(long lContactInfoID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/AddContactInfo", ReplyAction="http://tempuri.org/IDBService/AddContactInfoResponse")]
        long AddContactInfo(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/AddContactInfo", ReplyAction="http://tempuri.org/IDBService/AddContactInfoResponse")]
        System.Threading.Tasks.Task<long> AddContactInfoAsync(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/UpdateContactInfo", ReplyAction="http://tempuri.org/IDBService/UpdateContactInfoResponse")]
        bool UpdateContactInfo(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/UpdateContactInfo", ReplyAction="http://tempuri.org/IDBService/UpdateContactInfoResponse")]
        System.Threading.Tasks.Task<bool> UpdateContactInfoAsync(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/DeleteContactInfo", ReplyAction="http://tempuri.org/IDBService/DeleteContactInfoResponse")]
        bool DeleteContactInfo(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/DeleteContactInfo", ReplyAction="http://tempuri.org/IDBService/DeleteContactInfoResponse")]
        System.Threading.Tasks.Task<bool> DeleteContactInfoAsync(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/AddUpdateContactInfo", ReplyAction="http://tempuri.org/IDBService/AddUpdateContactInfoResponse")]
        long AddUpdateContactInfo(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/AddUpdateContactInfo", ReplyAction="http://tempuri.org/IDBService/AddUpdateContactInfoResponse")]
        System.Threading.Tasks.Task<long> AddUpdateContactInfoAsync(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/GetContactInfoRESTful", ReplyAction="http://tempuri.org/IDBService/GetContactInfoRESTfulResponse")]
        WebAPI.DBServiceReference.ContactInfoModel GetContactInfoRESTful(long lContactInfoID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/GetContactInfoRESTful", ReplyAction="http://tempuri.org/IDBService/GetContactInfoRESTfulResponse")]
        System.Threading.Tasks.Task<WebAPI.DBServiceReference.ContactInfoModel> GetContactInfoRESTfulAsync(long lContactInfoID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/InsertContactInfoRESTful", ReplyAction="http://tempuri.org/IDBService/InsertContactInfoRESTfulResponse")]
        long InsertContactInfoRESTful(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/InsertContactInfoRESTful", ReplyAction="http://tempuri.org/IDBService/InsertContactInfoRESTfulResponse")]
        System.Threading.Tasks.Task<long> InsertContactInfoRESTfulAsync(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/UpdateContactInfoRESTful", ReplyAction="http://tempuri.org/IDBService/UpdateContactInfoRESTfulResponse")]
        bool UpdateContactInfoRESTful(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/UpdateContactInfoRESTful", ReplyAction="http://tempuri.org/IDBService/UpdateContactInfoRESTfulResponse")]
        System.Threading.Tasks.Task<bool> UpdateContactInfoRESTfulAsync(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/DeleteContactInfoRESTful", ReplyAction="http://tempuri.org/IDBService/DeleteContactInfoRESTfulResponse")]
        bool DeleteContactInfoRESTful(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBService/DeleteContactInfoRESTful", ReplyAction="http://tempuri.org/IDBService/DeleteContactInfoRESTfulResponse")]
        System.Threading.Tasks.Task<bool> DeleteContactInfoRESTfulAsync(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDBServiceChannel : WebAPI.DBServiceReference.IDBService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DBServiceClient : System.ServiceModel.ClientBase<WebAPI.DBServiceReference.IDBService>, WebAPI.DBServiceReference.IDBService {
        
        public DBServiceClient() {
        }
        
        public DBServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DBServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DBServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DBServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public WebAPI.DBServiceReference.ContactInfoExModel[] GetContactInfoByCondition(WebAPI.DBServiceReference.QueryBaseModel objQueryBaseModel) {
            return base.Channel.GetContactInfoByCondition(objQueryBaseModel);
        }
        
        public System.Threading.Tasks.Task<WebAPI.DBServiceReference.ContactInfoExModel[]> GetContactInfoByConditionAsync(WebAPI.DBServiceReference.QueryBaseModel objQueryBaseModel) {
            return base.Channel.GetContactInfoByConditionAsync(objQueryBaseModel);
        }
        
        public WebAPI.DBServiceReference.ContactInfoModel GetContactInfo(long lContactInfoID) {
            return base.Channel.GetContactInfo(lContactInfoID);
        }
        
        public System.Threading.Tasks.Task<WebAPI.DBServiceReference.ContactInfoModel> GetContactInfoAsync(long lContactInfoID) {
            return base.Channel.GetContactInfoAsync(lContactInfoID);
        }
        
        public long AddContactInfo(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel) {
            return base.Channel.AddContactInfo(objContactInfoModel);
        }
        
        public System.Threading.Tasks.Task<long> AddContactInfoAsync(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel) {
            return base.Channel.AddContactInfoAsync(objContactInfoModel);
        }
        
        public bool UpdateContactInfo(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel) {
            return base.Channel.UpdateContactInfo(objContactInfoModel);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateContactInfoAsync(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel) {
            return base.Channel.UpdateContactInfoAsync(objContactInfoModel);
        }
        
        public bool DeleteContactInfo(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel) {
            return base.Channel.DeleteContactInfo(objContactInfoModel);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteContactInfoAsync(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel) {
            return base.Channel.DeleteContactInfoAsync(objContactInfoModel);
        }
        
        public long AddUpdateContactInfo(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel) {
            return base.Channel.AddUpdateContactInfo(objContactInfoModel);
        }
        
        public System.Threading.Tasks.Task<long> AddUpdateContactInfoAsync(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel) {
            return base.Channel.AddUpdateContactInfoAsync(objContactInfoModel);
        }
        
        public WebAPI.DBServiceReference.ContactInfoModel GetContactInfoRESTful(long lContactInfoID) {
            return base.Channel.GetContactInfoRESTful(lContactInfoID);
        }
        
        public System.Threading.Tasks.Task<WebAPI.DBServiceReference.ContactInfoModel> GetContactInfoRESTfulAsync(long lContactInfoID) {
            return base.Channel.GetContactInfoRESTfulAsync(lContactInfoID);
        }
        
        public long InsertContactInfoRESTful(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel) {
            return base.Channel.InsertContactInfoRESTful(objContactInfoModel);
        }
        
        public System.Threading.Tasks.Task<long> InsertContactInfoRESTfulAsync(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel) {
            return base.Channel.InsertContactInfoRESTfulAsync(objContactInfoModel);
        }
        
        public bool UpdateContactInfoRESTful(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel) {
            return base.Channel.UpdateContactInfoRESTful(objContactInfoModel);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateContactInfoRESTfulAsync(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel) {
            return base.Channel.UpdateContactInfoRESTfulAsync(objContactInfoModel);
        }
        
        public bool DeleteContactInfoRESTful(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel) {
            return base.Channel.DeleteContactInfoRESTful(objContactInfoModel);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteContactInfoRESTfulAsync(WebAPI.DBServiceReference.ContactInfoModel objContactInfoModel) {
            return base.Channel.DeleteContactInfoRESTfulAsync(objContactInfoModel);
        }
    }
}
