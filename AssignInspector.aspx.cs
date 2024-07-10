using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Data.SqlClient;

using AppEngine;
using AppEngine.Infrastructure.ApplicationManager;

namespace RRD.GRESAdmin
{
    public partial class AssignInspector : System.Web.UI.Page
    {
        private string connString;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            MySession.Current.ApplicationID = Guid.Parse("931656bf-b7f3-406a-af89-3633512356e3");
            connString = ConnectionStrings.Get()[MySession.Current.ApplicationID].MainDb;
            //}
        }

        protected void OnItemDataBoundHandler(object sender, GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode)
            {
                GridEditableItem item = (GridEditableItem)e.Item;

                if (!(e.Item is IGridInsertItem))
                {
                    RadComboBox combo = (RadComboBox)item.FindControl("RadComboBox1");

                    RadComboBoxItem selectedItem = new RadComboBoxItem();
                    selectedItem.Text = ((DataRowView)e.Item.DataItem)["InspectorNo"].ToString();
                    selectedItem.Value = ((DataRowView)e.Item.DataItem)["AssignedCompanyInspectorID"].ToString();
                    selectedItem.Attributes.Add("InspectorNo", ((DataRowView)e.Item.DataItem)["CompanyName"].ToString());

                    combo.Items.Add(selectedItem);

                    selectedItem.DataBind();

                    Session["AssignedCompanyInspectorID"] = selectedItem.Value;
                }
            }
        }

        protected void RadComboBox1_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string sql = "select CompanyID as [AssignedCompanyInspectorID], InspectorNo, CompanyName, CompanyStreet1, CompanyCity, CompanyState, CompanyZip  from [Company] where InspectorNo LIKE '" + e.Text + "%' and CompanyRelationshipID = 4 order by InspectorNo";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connString);
            //adapter.SelectCommand.Parameters.AddWithValue("@CompanyName", e.Text);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            RadComboBox comboBox = (RadComboBox)sender;
            // Clear the default Item that has been re-created from ViewState at this point.
            comboBox.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["InspectorNo"].ToString();
                item.Value = row["AssignedCompanyInspectorID"].ToString();
                item.Attributes.Add("CompanyName", row["CompanyName"].ToString());
                item.Attributes.Add("CompanyState", row["CompanyState"].ToString());
                item.Attributes.Add("CompanyZip", row["CompanyZip"].ToString());

                comboBox.Items.Add(item);

                item.DataBind();
            }         
        
        }

      
        protected void OnSelectedIndexChangedHandler(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Session["AssignedCompanyInspectorID"] = e.Value;
           
        }


        private void SetMessage(string message)
        {
            Label1.Text = string.Format("<span>{0}</span>", message);
        }


        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == string.Empty)
            {
                RadGrid1.Rebind();
            }
            string[] editedItemIds = e.Argument.Split(':');
            int i;
            for (i = 0; i <= editedItemIds.Length - 2; i++)
            {
                string inspectionOrderID = editedItemIds[i];
                GridDataItem updatedItem = RadGrid1.MasterTableView.FindItemByKeyValue("InspectionOrderID", Guid.Parse(inspectionOrderID));

                UpdateValues(updatedItem);
            }
        }
        protected void UpdateValues(GridDataItem updatedItem)
        {

            RadComboBox ddl = (RadComboBox)updatedItem.FindControl("ddlCompanyID");
            SqlDataSource1.UpdateParameters["AssignedCompanyInspectorID"].DefaultValue = ddl.SelectedValue;


            SqlDataSource1.UpdateParameters["InspectionOrderID"].DefaultValue = updatedItem.GetDataKeyValue("InspectionOrderID").ToString();

            try
            {
                SqlDataSource1.Update();
            }
            catch (Exception ex)
            {
                SetMessage(Server.HtmlEncode("Unable to update InspectionOrder. Reason: " + ex.StackTrace).Replace("'", "&#39;").Replace("\r\n", "<br />"));
            }
            SetMessage("InspectionOrder with ID: " + updatedItem.GetDataKeyValue("InspectionOrderID") + " updated");

        }
    }
}