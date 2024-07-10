using System;


namespace RRD.GRESAdmin
{
    public partial class Redirect : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (MySession.Current.RefreshParent == true)
                {
                    MySession.Current.RefreshParent = false;
                    Page.RegisterStartupScript("RefreshParent", "<script language='javascript'>RefreshParent()</script>");
                }
            }
        }
    }
}