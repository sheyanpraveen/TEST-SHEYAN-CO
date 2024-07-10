﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://rrd.com/CSI/TimerService", ConfigurationName="WorkFlowMessageSenderCSI.SendWorkflowMessageSoap")]
    public interface SendWorkflowMessageSoap {
        
        // CODEGEN: Generating message contract since message AuthenticateUserRequest has headers
        [System.ServiceModel.OperationContractAttribute(Action="http://rrd.com/CSI/TimerService/AuthenticateUser", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.AuthenticateUserResponse AuthenticateUser(Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.AuthenticateUserRequest request);
        
        // CODEGEN: Generating message contract since message SendWorkFlowMessageRequest has headers
        [System.ServiceModel.OperationContractAttribute(Action="http://rrd.com/CSI/TimerService/SendWorkFlowMessage", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SendWorkFlowMessageResponse SendWorkFlowMessage(Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SendWorkFlowMessageRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9037.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rrd.com/CSI/TimerService")]
    public partial class SecuredWebServiceHeader : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string usernameField;
        
        private string passwordField;
        
        private System.Guid applicationIDField;
        
        private string authenticatedTokenField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Username {
            get {
                return this.usernameField;
            }
            set {
                this.usernameField = value;
                this.RaisePropertyChanged("Username");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
                this.RaisePropertyChanged("Password");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public System.Guid ApplicationID {
            get {
                return this.applicationIDField;
            }
            set {
                this.applicationIDField = value;
                this.RaisePropertyChanged("ApplicationID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string AuthenticatedToken {
            get {
                return this.authenticatedTokenField;
            }
            set {
                this.authenticatedTokenField = value;
                this.RaisePropertyChanged("AuthenticatedToken");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
                this.RaisePropertyChanged("AnyAttr");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="AuthenticateUser", WrapperNamespace="http://rrd.com/CSI/TimerService", IsWrapped=true)]
    public partial class AuthenticateUserRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://rrd.com/CSI/TimerService")]
        public Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SecuredWebServiceHeader SecuredWebServiceHeader;
        
        public AuthenticateUserRequest() {
        }
        
        public AuthenticateUserRequest(Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SecuredWebServiceHeader SecuredWebServiceHeader) {
            this.SecuredWebServiceHeader = SecuredWebServiceHeader;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="AuthenticateUserResponse", WrapperNamespace="http://rrd.com/CSI/TimerService", IsWrapped=true)]
    public partial class AuthenticateUserResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rrd.com/CSI/TimerService", Order=0)]
        public string AuthenticateUserResult;
        
        public AuthenticateUserResponse() {
        }
        
        public AuthenticateUserResponse(string AuthenticateUserResult) {
            this.AuthenticateUserResult = AuthenticateUserResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SendWorkFlowMessage", WrapperNamespace="http://rrd.com/CSI/TimerService", IsWrapped=true)]
    public partial class SendWorkFlowMessageRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://rrd.com/CSI/TimerService")]
        public Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SecuredWebServiceHeader SecuredWebServiceHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rrd.com/CSI/TimerService", Order=0)]
        public string workFlowMessage;
        
        public SendWorkFlowMessageRequest() {
        }
        
        public SendWorkFlowMessageRequest(Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SecuredWebServiceHeader SecuredWebServiceHeader, string workFlowMessage) {
            this.SecuredWebServiceHeader = SecuredWebServiceHeader;
            this.workFlowMessage = workFlowMessage;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SendWorkFlowMessageResponse", WrapperNamespace="http://rrd.com/CSI/TimerService", IsWrapped=true)]
    public partial class SendWorkFlowMessageResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rrd.com/CSI/TimerService", Order=0)]
        public string SendWorkFlowMessageResult;
        
        public SendWorkFlowMessageResponse() {
        }
        
        public SendWorkFlowMessageResponse(string SendWorkFlowMessageResult) {
            this.SendWorkFlowMessageResult = SendWorkFlowMessageResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SendWorkflowMessageSoapChannel : Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SendWorkflowMessageSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SendWorkflowMessageSoapClient : System.ServiceModel.ClientBase<Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SendWorkflowMessageSoap>, Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SendWorkflowMessageSoap {
        
        public SendWorkflowMessageSoapClient() {
        }
        
        public SendWorkflowMessageSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SendWorkflowMessageSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SendWorkflowMessageSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SendWorkflowMessageSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.AuthenticateUserResponse Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SendWorkflowMessageSoap.AuthenticateUser(Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.AuthenticateUserRequest request) {
            return base.Channel.AuthenticateUser(request);
        }
        
        public string AuthenticateUser(Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SecuredWebServiceHeader SecuredWebServiceHeader) {
            Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.AuthenticateUserRequest inValue = new Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.AuthenticateUserRequest();
            inValue.SecuredWebServiceHeader = SecuredWebServiceHeader;
            Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.AuthenticateUserResponse retVal = ((Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SendWorkflowMessageSoap)(this)).AuthenticateUser(inValue);
            return retVal.AuthenticateUserResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SendWorkFlowMessageResponse Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SendWorkflowMessageSoap.SendWorkFlowMessage(Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SendWorkFlowMessageRequest request) {
            return base.Channel.SendWorkFlowMessage(request);
        }
        
        public string SendWorkFlowMessage(Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SecuredWebServiceHeader SecuredWebServiceHeader, string workFlowMessage) {
            Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SendWorkFlowMessageRequest inValue = new Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SendWorkFlowMessageRequest();
            inValue.SecuredWebServiceHeader = SecuredWebServiceHeader;
            inValue.workFlowMessage = workFlowMessage;
            Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SendWorkFlowMessageResponse retVal = ((Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI.SendWorkflowMessageSoap)(this)).SendWorkFlowMessage(inValue);
            return retVal.SendWorkFlowMessageResult;
        }
    }
}
