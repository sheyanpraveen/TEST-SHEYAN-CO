using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace RRD.GRESAdmin
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.UI.MasterPage" />
    public partial class BaseWidgetControl : System.Web.UI.MasterPage
    {
        /// <summary>
        /// The application identifier
        /// </summary>
        private static Guid applicationID = Guid.Empty;
        /// <summary>
        /// The user identifier
        /// </summary>
        private static Guid userID = Guid.Empty;
        /// <summary>
        /// The control identifier
        /// </summary>
        protected Guid controlID = Guid.Empty;
        /// <summary>
        /// The section identifier
        /// </summary>
        protected Guid sectionID = Guid.Empty;

        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
            tab.NavigateUrl = "~/appControls/Edit.aspx?SectionID=" + sectionID + "&ControlID=" + controlID + "&IsParentControlWidget=1";

            tab = RadTabStrip1.FindTabByText("Settings");
            tab.NavigateUrl = "~/UI/BaseWidgetControl.aspx?ControlID=" + controlID + "&SectionID=" + sectionID;
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
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

        /// <summary>
        /// Handles the TabClick event of the RadTabStrip1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RadTabStripEventArgs"/> instance containing the event data.</param>
        protected void RadTabStrip1_TabClick(object sender, RadTabStripEventArgs e)
        {
            if (e.Tab.ID != null)
            {
                Response.Redirect(e.Tab.Value);
            }

        }
    }
}