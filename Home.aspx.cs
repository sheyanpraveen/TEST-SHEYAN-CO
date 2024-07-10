using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using AppEngine.ApplicationManager;
using AppEngine.BLL;
using AppEngine.Common;
using Microsoft.Web.Administration;
using RRD.GRESAdmin.Common;
using Telerik.Web.UI;
using AppEngine.Infrastructure.ApplicationManager;
using AppEngine.Infrastructure.BLL;

namespace RRD.GRESAdmin
{
    public partial class Home : System.Web.UI.Page
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private string connString;

        private static string DefaultWebSiteName = "Default Web Site";

        /* Must be replaced */
        protected static Guid applicationID = new Guid("931656bf-b7f3-406a-af89-3633512356e3");
        protected static Guid userID = new Guid("931656bf-b7f3-406a-af89-3633512356e3");

        /// <summary>
        /// The local admin user name
        /// </summary>
        private string localAdminUserName = ConfigurationManager.AppSettings["LocalAdminUserName"].ToString();

        /// <summary>
        /// The local admin password
        /// </summary>
        private string localAdminPassword = ConfigurationManager.AppSettings["LocalAdminPassword"].ToString();

        /// <summary>
        /// The local admin domain/
        /// </summary>
        private string localAdminDomain = ConfigurationManager.AppSettings["LocalAdminDomain"].ToString();

        /// <summary>
        /// The application pool name
        /// </summary>
        private string appPoolName = string.Empty;

        /// <summary>
        /// The error message no IIS site name
        /// </summary>
        private const string ErrorMessageNoIisSiteName = "IIS site name has not been set up for this application.";

        /// <summary>
        /// The error message invalid IIS site name
        /// </summary>
        private const string ErrorMessageInvalidIisSiteName = "IIS site name is invalid.";

        /// <summary>
        /// The error message exception
        /// </summary>
        private const string ErrorMessageException = "Error occurred.";

        /// <summary>
        /// The error message application pool recycle exception
        /// </summary>
        private const string ErrorMessageAppPoolRecycleException = "Error occurred. Application pool recycling failed. Message: {0}";

        /// <summary>
        /// The success message
        /// </summary>
        private const string SuccessMessage = "Application pool has been successfully recycled.";

        /// <summary>
        /// The confirm message/
        /// </summary>
        private const string ConfirmMessage = "The '{0}' application pool is used by the following application(s).";


        private const string SelectedPermittedApplicationsQuery = @"SELECT SS.ApplicationID, SS.ApplicationName FROM appServerSettings SS
                                                        INNER JOIN UserApplication UA
                                                        ON SS.ApplicationID = UA.ApplicationID
                                                        WHERE UA.UserID = '{0}'";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                string navigateUrl = Request.Url.AbsolutePath.ToString();
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=" + navigateUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            connString = ConfigurationManager.ConnectionStrings["RRDGRESMasterDB"].ConnectionString;

            if (!IsPostBack)
            {
                var queryStringSelectedPermittedApplications = string.Format(SelectedPermittedApplicationsQuery, MySession.Current.UserID);
                SqlDataAdapter adapter = new SqlDataAdapter(queryStringSelectedPermittedApplications, connString);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if(dt.Rows.Count > 0)
                {
                    cboApplication.DataSource = dt;
                }
                else
                {
                    cboApplication.DataSource = Cached.appServerSettings();
                }

                cboApplication.DataTextField = "ApplicationName";
                cboApplication.DataValueField = "ApplicationID";
                cboApplication.DataBind();

                if (MySession.Current.ApplicationID != Guid.Empty)
                {
                    applicationID = MySession.Current.ApplicationID;
                    var key = StringEnumHelper.GetStringValue(Enums.SessionKey.ApplicationID);
                    int index = cboApplication.FindItemIndexByValue(MySession.Current.ApplicationID.ToString());
                    cboApplication.SelectedIndex = index;
                    MySession.Current.ApplicationName = cboApplication.SelectedItem.Text;
                }

            }

        }

        protected void cboApplication_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            applicationID = new Guid(cboApplication.SelectedValue);

            Global.ApplicationConnString = ConnectionStrings.Get()[applicationID].MainDb;
            Session["ApplicationConnString"] = ConnectionStrings.Get()[applicationID].MainDb;

            MySession.Current.ApplicationID = applicationID;
            MySession.Current.ApplicationName = cboApplication.SelectedItem.Text;
            //MySession.Current.UserID = userID;

            Response.Redirect(Request.RawUrl);
        }

        /// <summary>
        /// Handles the Click event of the RecycleAppPool control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void RecycleAppPool_Click(object sender, EventArgs e)
        {
            applicationID = new Guid(cboApplication.SelectedValue);
            LbErrorMessage.Text = string.Empty;
            LbsuccessMessage.Text = string.Empty;
            HfAppPoolName.Value = string.Empty;

            try
            {
                appServerSettings appSettings = Cached.appServerSettings(applicationID);
                string iisSiteName = appSettings.IISSiteName;
                if (!string.IsNullOrEmpty(iisSiteName))
                {
                    string[] siteNames = GetIisSitesWithSameAppPool(iisSiteName);
                    if (siteNames == null || siteNames.Count() == 0)
                    {
                        LbErrorMessage.Text = ErrorMessageInvalidIisSiteName;
                    }
                    else
                    {
                        string script = "function f(){$find(\"" + RadWindowAppPoolRecycle.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        LtConfirmMessage.Text = string.Format(ConfirmMessage, appPoolName);
                        BlIisSiteNames.DataSource = siteNames;
                        BlIisSiteNames.DataBind();
                        HfAppPoolName.Value = appPoolName;
                    }
                }
                else
                {
                    LbErrorMessage.Text = ErrorMessageNoIisSiteName;
                }
            }
            catch (Exception ex)
            {
                LbErrorMessage.Text = ex.ToString();
            }
        }

        /// <summary>
        /// Gets the IIS sites with same application pool.
        /// </summary>
        /// <param name="iisSiteName">Name of the IIS site.</param>
        /// <returns></returns>
        private string[] GetIisSitesWithSameAppPool(string iisSiteName)
        {
            string[] siteNames = null;

            using (new Impersonator(localAdminUserName, localAdminDomain, localAdminPassword))
            {
                ServerManager iisManager = new ServerManager();
                SiteCollection sites = iisManager.Sites;

                // get the default web site
                Microsoft.Web.Administration.Site defaultSite = iisManager.Sites[Home.DefaultWebSiteName];

                //get iis site by name
                Microsoft.Web.Administration.Site iisSite = sites.FirstOrDefault(x => x.Name.ToLower() == iisSiteName.ToLower());

                if (iisSite != null)
                {
                    appPoolName = iisSite.Applications["/"].ApplicationPoolName;

                    //get applications with same app pool
                    List<Microsoft.Web.Administration.Site> appPoolsites = iisManager.Sites.Where(y => y.Applications["/"].ApplicationPoolName == appPoolName).ToList();
                    siteNames = appPoolsites.Select(x => x.Name).ToArray();

                    if (defaultSite != null)
                    {
                        // also check under default web site which uses same app pool
                        List<Application> httpApplications = defaultSite.Applications.Where(x => x.ApplicationPoolName == appPoolName).ToList();
                        siteNames = siteNames.Concat(httpApplications.Where(x => x.Path != "/").Select(y => defaultSite.Name + y.Path).ToArray()).ToArray();
                    }
                }
                else
                {
                    if (defaultSite != null)
                    {
                        // check for the site under the default web site                    
                        Application httpApplication = defaultSite.Applications.Where(x => x.Path.ToLower() == string.Concat("/", iisSiteName.ToLower())).FirstOrDefault();
                        appPoolName = httpApplication.ApplicationPoolName;

                        if (httpApplication != null)
                        {
                            //get applications with same app pool
                            List<Microsoft.Web.Administration.Site> appPoolsites = iisManager.Sites.Where(y => y.Applications["/"].ApplicationPoolName == appPoolName).ToList();
                            siteNames = appPoolsites.Select(x => x.Name).ToArray();

                            if (defaultSite != null)
                            {
                                // also check under default web site which uses same app pool
                                List<Application> httpApplications = defaultSite.Applications.Where(x => x.ApplicationPoolName == appPoolName).ToList();
                                siteNames = siteNames.Concat(httpApplications.Where(x => x.Path != "/").Select(y => defaultSite.Name + y.Path).ToArray()).ToArray();
                            }
                        }
                    }
                }
            }

            return siteNames;
        }

        /// <summary>
        /// Handles the Click event of the BtnYes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void BtnYes_Click(object sender, EventArgs e)
        {
            appPoolName = HfAppPoolName.Value;
            try
            {
                using (new Impersonator(localAdminUserName, localAdminDomain, localAdminPassword))
                {
                    string systemDirectory = Environment.GetFolderPath(Environment.SpecialFolder.System);
                    string command = string.Format(@"{0}\inetsrv\appcmd recycle apppool {1}", systemDirectory, appPoolName);

                    string result = ExecuteCommandSync(command);

                    LbsuccessMessage.Text = result;
                }
            }
            catch (Exception ex)
            {
                LbErrorMessage.Text = string.Format(ErrorMessageAppPoolRecycleException, ex.Message);
            }
            finally
            {
                HfAppPoolName.Value = string.Empty;
            }
        }


        public string ExecuteCommandSync(object command)
        {
            // create the ProcessStartInfo using "cmd" as the program to be run,
            // and "/c " as the parameters.
            // Incidentally, /c tells cmd that we want it to execute the command that follows,
            // and then exit.
            System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

            // The following commands are needed to redirect the standard output.

            // This means that it will be redirected to the Process.StandardOutput StreamReader.
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.Verb = "runas";

            // Do not create the black window.
            processStartInfo.CreateNoWindow = true;

            // Now we create a process, assign its ProcessStartInfo and start it
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo = processStartInfo;
            process.Start();
            process.WaitForExit();

            // Get the output into a string
            string result = process.StandardOutput.ReadToEnd();

            // Display the command output.
            Console.WriteLine(result);

            return result;
        }

    }
}