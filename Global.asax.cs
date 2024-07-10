using AppEngine.Infrastructure.ApplicationManager;
using GleamTech.FileUltimate;
using System;
using System.Configuration;
using System.Web.DynamicData;
using System.Web.Routing;
using System.Web.UI;

namespace RRD.GRESAdmin
{
    public class Global : System.Web.HttpApplication
    {
        public static String ApplicationConnString;
        private static MetaModel s_defaultModel = new MetaModel();
        private const string DefaultApplicationID = "B31AB187-62B7-48C2-806F-047F372EBF94";

        public static MetaModel DefaultModel
        {
            get
            {
                return s_defaultModel;
            }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            string initialApplication = string.IsNullOrEmpty(ConfigurationManager.AppSettings["InitialApplicationID"]) ? DefaultApplicationID : ConfigurationManager.AppSettings["InitialApplicationID"];
            // Change applicationId to main db when do a propper release
            ApplicationConnString = ConnectionStrings.Get()[new Guid(initialApplication)].MainDb;

            //                    IMPORTANT: DATA MODEL REGISTRATION 
            // Uncomment this line to register a LINQ to SQL model for ASP.NET Dynamic Data.
            // Set ScaffoldAllTables = true only if you are sure that you want all tables in the
            // data model to support a scaffold (i.e. templates) view. To control scaffolding for
            // individual tables, create a partial class for the table and apply the
            // [ScaffoldTable(true)] attribute to the partial class.
            // Note: Make sure that you change "YourDataContextType" to the name of the data context
            // class in your application.
            DefaultModel.RegisterContext(typeof(RRD.GRESAdmin.Data.AdminDataContext), new ContextConfiguration() { ScaffoldAllTables = true });

            // The following statement supports separate-page mode, where the List, Detail, Insert, and 
            // Update tasks are performed by using separate pages. To enable this mode, uncomment the following 
            // route definition, and comment out the route definitions in the combined-page mode section that follows.
            routes.Add(new DynamicDataRoute("{table}/{action}.aspx")
            {
                Constraints = new RouteValueDictionary(new { action = "List|Details|Edit|Insert" }),
                Model = DefaultModel
            });

            // The following statements support combined-page mode, where the List, Detail, Insert, and
            // Update tasks are performed by using the same page. To enable this mode, uncomment the
            // following routes and comment out the route definition in the separate-page mode section above.
            //routes.Add(new DynamicDataRoute("{table}/ListDetails.aspx")
            //{
            //    Action = PageAction.List,
            //    ViewName = "ListDetails",
            //    Model = DefaultModel
            //});

            //routes.Add(new DynamicDataRoute("{table}/List.aspx")
            //{
            //    Action = PageAction.List,
            //    ViewName = "List",
            //    Model = DefaultModel
            //});

            //routes.Add(new DynamicDataRoute("{table}/Details.aspx")
            //{
            //    Action = PageAction.Details,
            //    ViewName = "List",
            //    Model = DefaultModel
            //});

            //routes.Add(new DynamicDataRoute("{table}/Edit.aspx")
            //{
            //    Action = PageAction.Edit,
            //    ViewName = "Edit",
            //    Model = DefaultModel
            //});
            //routes.Add(new DynamicDataRoute("{table}/Insert.aspx")
            //{
            //    Action = PageAction.Insert,
            //    ViewName = "Insert",
            //    Model = DefaultModel
            //});
            

        }

        void Application_Start(object sender, EventArgs e)
        {
            string initialApplication = string.IsNullOrEmpty(ConfigurationManager.AppSettings["InitialApplicationID"]) ? DefaultApplicationID : ConfigurationManager.AppSettings["InitialApplicationID"];

            // Change applicationId to main db when do a propper release
            //var applicationID = DefaultApplicationID;
            //var applicationID = "D64673B2-DC1E-4EB8-A26E-A44A2E138BD2"; 
            //var applicationID = "585403c2-2c7b-40e0-b7d4-6661ae1fff82";
            var appSettings = AppEngine.Infrastructure.BLL.appSettings.GetByApplicationID(new Guid(initialApplication));

            FileUltimateConfiguration.Current.LicenseKey = appSettings.GleamTechLicenseKey;

            if (DefaultModel.Tables.Count < 1)
            {
                RegisterRoutes(RouteTable.Routes);
            }

            ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
            new ScriptResourceDefinition
            {
                Path = "~/scripts/jquery-1.4.1.min.js",
                DebugPath = "~/scripts/jquery-1.4.1.js",
                CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.4.1.min.js",
                CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.4.1.js"
            });
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Change applicationId to main db when do a propper release
            string initialApplication = string.IsNullOrEmpty(ConfigurationManager.AppSettings["InitialApplicationID"]) ? DefaultApplicationID : ConfigurationManager.AppSettings["InitialApplicationID"];

            Guid applicationID = new Guid(initialApplication);
            ApplicationConnString = ConnectionStrings.Get()[applicationID].MainDb;
            MySession.Current.ApplicationID = applicationID;
            Session["ApplicationConnString"] = ApplicationConnString;

            if (DefaultModel.Tables.Count < 1)
            {
                RegisterRoutes(RouteTable.Routes);
            }
        }

    }
}
