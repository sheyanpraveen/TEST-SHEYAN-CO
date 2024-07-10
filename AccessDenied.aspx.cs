using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RRD.GRESAdmin.Account
{
    public partial class AccessDenied : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {                
                var owinContext = Request.GetOwinContext();
                var authenticationManager = owinContext.Authentication;
                
                // sign out
                authenticationManager.SignOut();
                Session.Abandon();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
    }
}