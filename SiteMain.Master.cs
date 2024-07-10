using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppEngine.Common;

namespace RRD.GRESAdmin
{

    public partial class SiteMain : System.Web.UI.MasterPage
    {
        /* Must be replaced */
        protected static Guid applicationID = new Guid("931656bf-b7f3-406a-af89-3633512356e3");
        protected static Guid userID = new Guid("931656bf-b7f3-406a-af89-3633512356e3");

        protected void Page_Load(object sender, EventArgs e)
        {
            ApplicationName.Text = " (" + MySession.Current.ApplicationName + ")";
        }

        public static void SetSession()
        {
            MySession.Current.ApplicationID = applicationID;
            //MySession.Current.UserID = userID;
        }
    }
}