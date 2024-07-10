using System;
using Telerik.Web.UI;
using System.Web.UI.WebControls;


namespace RRD.GRESAdmin
{
    public partial class BaseGridControl : System.Web.UI.MasterPage
    {
        private static Guid applicationID = Guid.Empty;
        private static Guid userID = Guid.Empty;
        protected Guid controlID = Guid.Empty;
        protected Guid sectionID = Guid.Empty;

        protected void Page_Init(object sender, EventArgs e)
        {
           
            bool result = false;

            applicationID = (MySession.Current.ApplicationID != null) ? applicationID = new Guid(MySession.Current.ApplicationID.ToString()) : Guid.Empty;
            userID = (MySession.Current.UserID != null) ? userID = new Guid(MySession.Current.UserID.ToString()) : Guid.Empty;

            if (Request.QueryString["ControlID"] != null)
            {
                result = Guid.TryParse(Request.QueryString["ControlID"], out controlID);
            }
            if (Request.QueryString["SectionID"] != null)
            {
                result = Guid.TryParse(Request.QueryString["SectionID"], out sectionID);

            }
            RadTab tab = RadTabStrip1.FindTabByText("Details");
            tab.NavigateUrl = "~/appControls/Edit.aspx?SectionID=" + sectionID + "&ControlID=" + controlID + "&IsParentControl=1";

            tab = RadTabStrip1.FindTabByText("Controls");
            tab.NavigateUrl = "~/appControls/List.aspx?SectionID=" + sectionID + "&ControlID=" + controlID;

            tab = RadTabStrip1.FindTabByText("Settings");
            tab.NavigateUrl = "~/UI/BaseGridControl.aspx?ControlID=" + controlID + "&SectionID=" + sectionID;

        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // ApplyAppPathModifier will add the session ID if we're using Cookieless session.
            string urlWithSessionID = Response.ApplyAppPathModifier(Request.Url.PathAndQuery);
            RadTab tab = RadTabStrip1.FindTabByUrl(urlWithSessionID);
            if (tab != null)
            {
                tab.SelectParents();
                tab.Selected = true;
            }
          

        }

        protected void RadTabStrip1_TabClick(object sender, RadTabStripEventArgs e)
        {
            if (e.Tab.ID != null)
            {

                Response.Redirect(e.Tab.Value);
            }

        }

    }
}