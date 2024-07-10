using System;
using System.Xml;
using Convergence.TimerServiceEngine.WorkFlowMessageSenderCSI;


namespace Convergence.TimerServiceEngine
{
    public class CSIServiceInvoker : IServiceInvoker
    {
        private SecuredWebServiceHeader soapHeader;
        public string ServiceURL { get; set; }


        public CSIServiceInvoker() { }
        public CSIServiceInvoker(string serviceURL)
        {
            this.ServiceURL = serviceURL;
        }

        public bool AuthenticateServiceUser(Guid applicationId, byte[] securityKey)
        {
            bool authenticated = false;

            soapHeader = new SecuredWebServiceHeader();
            soapHeader.ApplicationID = applicationId;
            soapHeader.Username = "TimerServiceAccount";
            soapHeader.Password = "p@$$w0rd1";

            SendWorkflowMessageSoapClient serviceClient = new SendWorkflowMessageSoapClient();
            serviceClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(this.ServiceURL);

            string xmlResponse = serviceClient.AuthenticateUser(soapHeader);
            XmlDocument xDocResponse = new XmlDocument();
            xDocResponse.LoadXml(xmlResponse);

            XmlNode xNodeSuscess = xDocResponse.SelectSingleNode(@"Response/Success");
            if (String.Compare(xNodeSuscess.InnerText, "1", true) == 0)
            {
                XmlNode xNodeToken = xDocResponse.SelectSingleNode(@"Response/Token");
                string tokenEecrypted = xNodeToken.InnerText;

                soapHeader.AuthenticatedToken = AppEngine.Security.SecurityEncryption.Decrypt(tokenEecrypted, securityKey);
                authenticated = true;
            }
            return authenticated;
        }

        public XmlDocument SendWorkFlowMessage(string wokFlowMessage)
        {
            XmlDocument xDocResponse = null;
            SendWorkflowMessageSoapClient serviceClient = new SendWorkflowMessageSoapClient();
            serviceClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(this.ServiceURL);

            string responseXML = serviceClient.SendWorkFlowMessage(this.soapHeader, wokFlowMessage);

            xDocResponse = new XmlDocument();
            xDocResponse.LoadXml(responseXML);
            return xDocResponse;
        }
    }
}
