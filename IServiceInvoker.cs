using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


namespace Convergence.TimerServiceEngine
{
    public interface IServiceInvoker
    {
        string ServiceURL { get; set; }
        bool AuthenticateServiceUser(Guid applicationId, byte[] securityKey);
        XmlDocument SendWorkFlowMessage(string wokFlowMessage);
    }
}
