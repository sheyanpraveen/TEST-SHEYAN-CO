using System;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using AppEngine.Domain.DomainObject.BLL;

namespace RRD.GRESAdmin
{
    public partial class PageForm : System.Web.UI.MasterPage
    {
        private static Guid applicationID = Guid.Empty;
        private static Guid userID = Guid.Empty;
        protected Guid formID = Guid.Empty;
        protected Guid pageID = Guid.Empty;

        protected void Page_Init(object sender, EventArgs e)
        {

            bool result = false;

            applicationID = (MySession.Current.ApplicationID != null) ? applicationID = new Guid(MySession.Current.ApplicationID.ToString()) : Guid.Empty;
            userID = (MySession.Current.UserID != null) ? userID = new Guid(MySession.Current.UserID.ToString()) : Guid.Empty;

            if (Request.QueryString["FormID"] != null)
            {
                result = Guid.TryParse(Request.QueryString["FormID"], out formID);
            }
            if (Request.QueryString["PageID"] != null)
            {
                result = Guid.TryParse(Request.QueryString["PageID"], out pageID);

            }
            RadTab tab = RadTabStrip1.FindTabByText("Page");
            tab.NavigateUrl = "~/appPages/Edit.aspx?PageID=" + pageID + "&FormID=" + formID;

            tab = RadTabStrip1.FindTabByText("Form");
            tab.NavigateUrl = "~/appForms/Edit.aspx?FormID=" + formID + "&PageID=" + pageID;


            RadTab maskTab = RadTabStrip1.FindTabByText("Mask Controls");
            RadTab buttonSettingsTab = RadTabStrip1.FindTabByText("Button Settings");

            if (GetFormById(applicationID, formID).IsDivBasedRenderEnabled)
            {
                maskTab.NavigateUrl = "~/UI/MaskControls.aspx?FormID=" + formID + "&PageID=" + pageID;
                maskTab.Visible = true;

                buttonSettingsTab.NavigateUrl = "~/UI/ButtonSettings.aspx?FormID=" + formID + "&PageID=" + pageID;
                buttonSettingsTab.Visible = true;
            }
            else
            {
                maskTab.Visible = false;
                buttonSettingsTab.Visible = false;
            }
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

        private appForm GetFormById(Guid applicationId, Guid formId)
        {
            appForm result = new appForm();
            result = appForm.GetByFormID(applicationId, null, formId);

            return result;
        }
    }
}