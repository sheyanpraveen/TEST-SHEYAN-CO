using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace RRD.GRESAdmin
{
    public partial class SiteMaster : MasterPage
    {
        private readonly string AntiXsrfTokenKey = ConfigurationManager.AppSettings["AntiXsrfTokenKey"].ToString();
		private readonly string AntiXsrfUserNameKey = ConfigurationManager.AppSettings["AntiXsrfUserNameKey"].ToString();
		private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //RegisterHyperLink.NavigateUrl = "Register";
            // Enable this once you have account confirmation enabled for password reset functionality
            //ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            //OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            //var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            //if (!String.IsNullOrEmpty(returnUrl))
            //{
            //    RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            //}

            
            //Title.Text = settings.ProductName;
            ////ProductName.Text = settings.ApplicationName;
            //CP_ProductName.Text = ProductName.Text;

            ////if ((CompanyName != "") && (CompanyName != null))
            generateStyle("Project Portal", "Covius");
            ////else
            ////    generateStyle(settings.ApplicationNickName, settings.CompanyName);

            //var urlScheme = Page.Request.Url.Scheme.ToString().Trim().ToLower();

            //lit_FontRaleway.Text = "<link href='" + urlScheme + "://fonts.googleapis.com/css?family=Raleway' rel='stylesheet' type='text/css'>";
            //lit_FontOpenSans.Text = "<link href='" + urlScheme + "://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css'>";
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        protected void generateStyle(String applicationnickname, String companyname)
        {
            if (this.Request.UserAgent != null && !Regex.IsMatch(this.Request.UserAgent, @"Mozilla/4.0.*Trident"))
            {
                //vsErrorMessage.HeaderText = "";

                #region "Normal Styles"               
                lit_SectionHeader.Text = @"
                <style>

                section#intro .order-form input,
                section#intro .order-form textarea {
	                width: 100%;
	                --webkit-box-sizing: border-box;
	                -moz-box-sizing: border-box;
	                box-sizing: border-box;
	                font-family: 'open sans';
                }




	                .button{
		                background: #eee; /* Old browsers */
		                background: #eee -moz-linear-gradient(top, rgba(255,255,255,.2) 0%, rgba(0,0,0,.2) 100%); /* FF3.6+ */
		                background: #eee -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(255,255,255,.2)), color-stop(100%,rgba(0,0,0,.2))); /* Chrome,Safari4+ */
		                background: #eee -webkit-linear-gradient(top, rgba(255,255,255,.2) 0%,rgba(0,0,0,.2) 100%); /* Chrome10+,Safari5.1+ */
		                background: #eee -o-linear-gradient(top, rgba(255,255,255,.2) 0%,rgba(0,0,0,.2) 100%); /* Opera11.10+ */
		                background: #eee -ms-linear-gradient(top, rgba(255,255,255,.2) 0%,rgba(0,0,0,.2) 100%); /* IE10+ */
		                background: #eee linear-gradient(top, rgba(255,255,255,.2) 0%,rgba(0,0,0,.2) 100%); /* W3C */
	                  border: 1px solid #aaa;
	                  border-top: 1px solid #ccc;
	                  border-left: 1px solid #ccc;
	                  -moz-border-radius: 3px;
	                  -webkit-border-radius: 3px;
	                  border-radius: 3px;
	                  color: black;
	                  display: inline-block;
	                  font-size: 11px;
	                  font-weight: bold;
	                  text-decoration: none;
	                  text-shadow: 0 1px rgba(255, 255, 255, .75);
	                  cursor: pointer;
	                  margin-bottom: 20px;
	                  line-height: normal;
	                  padding: 8px 10px;
	                  font-family: 'HelveticaNeue', 'Helvetica Neue', Helvetica, Arial, sans-serif; }

	                .button:hover{
		                color: #222;
		                background: #ddd; /* Old browsers */
		                background: #ddd -moz-linear-gradient(top, rgba(255,255,255,.3) 0%, rgba(0,0,0,.3) 100%); /* FF3.6+ */
		                background: #ddd -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(255,255,255,.3)), color-stop(100%,rgba(0,0,0,.3))); /* Chrome,Safari4+ */
		                background: #ddd -webkit-linear-gradient(top, rgba(255,255,255,.3) 0%,rgba(0,0,0,.3) 100%); /* Chrome10+,Safari5.1+ */
		                background: #ddd -o-linear-gradient(top, rgba(255,255,255,.3) 0%,rgba(0,0,0,.3) 100%); /* Opera11.10+ */
		                background: #ddd -ms-linear-gradient(top, rgba(255,255,255,.3) 0%,rgba(0,0,0,.3) 100%); /* IE10+ */
		                background: #ddd linear-gradient(top, rgba(255,255,255,.3) 0%,rgba(0,0,0,.3) 100%); /* W3C */
	                  border: 1px solid #888;
	                  border-top: 1px solid #aaa;
	                  border-left: 1px solid #aaa; }

	                .button:active {
		                border: 1px solid #666;
		                background: #ccc; /* Old browsers */
		                background: #ccc -moz-linear-gradient(top, rgba(255,255,255,.35) 0%, rgba(10,10,10,.4) 100%); /* FF3.6+ */
		                background: #ccc -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(255,255,255,.35)), color-stop(100%,rgba(10,10,10,.4))); /* Chrome,Safari4+ */
		                background: #ccc -webkit-linear-gradient(top, rgba(255,255,255,.35) 0%,rgba(10,10,10,.4) 100%); /* Chrome10+,Safari5.1+ */
		                background: #ccc -o-linear-gradient(top, rgba(255,255,255,.35) 0%,rgba(10,10,10,.4) 100%); /* Opera11.10+ */
		                background: #ccc -ms-linear-gradient(top, rgba(255,255,255,.35) 0%,rgba(10,10,10,.4) 100%); /* IE10+ */
		                background: #ccc linear-gradient(top, rgba(255,255,255,.35) 0%,rgba(10,10,10,.4) 100%); /* W3C */ }

	                .button.full-width,
	                button.full-width,
	                input[type='submit'].full-width,
	                input[type='reset'].full-width,
	                input[type='button'].full-width {
		                width: 100%;
		                padding-left: 0 !important;
		                padding-right: 0 !important;
		                text-align: center; }

	                /* Fix for odd Mozilla border & padding issues */
	                button::-moz-focus-inner,
	                input::-moz-focus-inner {
                    border: 0;
                    padding: 0;
	                }

                .landing11 .login-bg  {
	                background-image: url(images/11csi.jpg);
	                -webkit-background-size: cover;
	                background-size: cover;
	                background-position: center;
                }
                .landing12 .login-bg {
	                background-image: url(images/branding/" + applicationnickname.Replace(" ", "%20") + @"/" + companyname.Replace(" ", "%20") + @"/loginbackground.jpg);
                    -webkit-background-size: cover;
	                background-size: cover;
	                background-position: center;*/
                }
                .landing13 .login-bg {
	                background-image: url(images/13cdm.jpg);
	                -webkit-background-size: cover;
	                background-size: cover;
	                background-position: center;
                }
                .landing14 .login-bg {
	                background-image: url(images/14.jpg);
	                -webkit-background-size: cover;
	                background-size: cover;
	                background-position: center;
                }
                .landing15 .login-bg {
	                background-image: url(images/15cam.jpg);
	                -webkit-background-size: cover;
	                background-size: cover;
	                background-position: center;
                }
                .landing16 .login-bg {
	                background-image: url(images/16resource.jpg);
	                -webkit-background-size: cover;
	                background-size: cover;
	                background-position: center;
                }
                .landing17 .login-bg {
	                background-image: url(images/17rsadminplus20.jpg);
	                -webkit-background-size: cover;
	                background-size: cover;
	                background-position: center;
                }
                .landing18 .login-bg {
	                background-image: url(images/18rsadminsurv.jpg);
	                -webkit-background-size: cover;
	                background-size: cover;
	                background-position: center;
                }
                .landing19 .login-bg {
	                background-image: url(images/19rsadmin2020.jpg);
	                -webkit-background-size: cover;
	                background-size: cover;
	                background-position: center;
                }
                </style>
                <section class='landing12' id='intro'>";
                //CP_lit_SectionHeader.Text = lit_SectionHeader.Text;
                //lit_FormDiv.Text = "<div style='align-content:center; text-align:center;'>";
                //CP_lit_FormDiv.Text = lit_FormDiv.Text;
                #endregion
            }
            else
            {

                #region "IE Trident Styles"
                lit_SectionHeader.Text = @"
                <style>

                .passwordRecoveryUserNameInput {
                    height: 30px; 
                    width: 100px; 
                    border: 1px solid #ccc; 
                    border-radius: 2px; 
                    font: 'HelveticaNeue', 'Helvetica Neue', Helvetica, Arial, sans-serif;
                    color: #777;
                    background: #fff; 
                    margin: 0;
                    margin-bottom: 20px;
                    display: block;
                }

                .textInputStyle {
                    border: 1px solid #ccc;
	                padding: 6px 4px;
	                outline: none;
	                -moz-border-radius: 2px;
	                -webkit-border-radius: 2px;
	                border-radius: 2px;
	                font: 13px 'HelveticaNeue', 'Helvetica Neue', Helvetica, Arial, sans-serif;
	                color: #777;
	                margin: 0;
                    display: block;
	                margin-bottom: 20px;
	                background: #fff; 
                }


                /* #Forms
                ================================================== */

                /*
	                form {
		                margin-bottom: 20px; }
	                fieldset {
		                margin-bottom: 20px; }
	
	                textarea {
		                min-height: 60px; }
	                label,
	                legend {
		                display: block;
		                font-weight: bold;
		                font-size: 13px;  }
	                select {
		                width: 220px; }
	                input[type='checkbox'] {
		                display: inline; }
	                label span,
	                legend span {
		                font-weight: normal;
		                font-size: 13px;
		                color: #444; }

                input[type='text'],
	                input[type='password'],
	                input[type='email'],
	                textarea,
	                select {
		                border: 1px solid #ccc;
		                padding: 6px 4px;
		                outline: none;
		                -moz-border-radius: 2px;
		                -webkit-border-radius: 2px;
		                border-radius: 2px;
		                font: 13px 'HelveticaNeue', 'Helvetica Neue', Helvetica, Arial, sans-serif;
		                color: #777;
		                margin: 0;
		                width: 100px;
                        height: 20px;
		                max-width: 100%;
		                display: block;
		                margin-bottom: 20px;
		                background: #fff; 
                    }
	                select {
		                padding: 0; 
                    }
	                input[type='text']:focus,
	                input[type='password']:focus,
	                input[type='email']:focus,
	                textarea:focus {
		                border: 1px solid #aaa;
 		                color: #444;
 		                -moz-box-shadow: 0 0 3px rgba(0,0,0,.2);
		                -webkit-box-shadow: 0 0 3px rgba(0,0,0,.2);
		                box-shadow:  0 0 3px rgba(0,0,0,.2); 
                    }
                */

                .UserNameLabel {
                    vertical-align: left;
                    font-size: 13px;
                    font-weight: bold;
                    font-family: 'HelveticaNeue', 'Helvetica Neue', Helvetica, Arial, sans-serif; }
                    text-decoration: none;
                    line-height: normal;
                    display: inline-block;
                }

                /* #Buttons
                ================================================== */

	                .button,
	                button,
	                input[type='submit'],
	                input[type='reset'],
	                input[type='button'],
	                select {
		                background: #eee; /* Old browsers */
		                background: #eee -moz-linear-gradient(top, rgba(255,255,255,.2) 0%, rgba(0,0,0,.2) 100%); /* FF3.6+ */
		                background: #eee -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(255,255,255,.2)), color-stop(100%,rgba(0,0,0,.2))); /* Chrome,Safari4+ */
		                background: #eee -webkit-linear-gradient(top, rgba(255,255,255,.2) 0%,rgba(0,0,0,.2) 100%); /* Chrome10+,Safari5.1+ */
		                background: #eee -o-linear-gradient(top, rgba(255,255,255,.2) 0%,rgba(0,0,0,.2) 100%); /* Opera11.10+ */
		                background: #eee -ms-linear-gradient(top, rgba(255,255,255,.2) 0%,rgba(0,0,0,.2) 100%); /* IE10+ */
		                background: #eee linear-gradient(top, rgba(255,255,255,.2) 0%,rgba(0,0,0,.2) 100%); /* W3C */
	                  border: 1px solid #aaa;
	                  border-top: 1px solid #ccc;
	                  border-left: 1px solid #ccc;
	                  -moz-border-radius: 3px;
	                  -webkit-border-radius: 3px;
	                  border-radius: 3px;
	                  color: black;
	                  display: inline-block;
	                  font-size: 11px;
	                  font-weight: bold;
	                  text-decoration: none;
	                  text-shadow: 0 1px rgba(255, 255, 255, .75);
	                  cursor: pointer;
	                  margin-bottom: 20px;
	                  line-height: normal;
	                  padding: 8px 10px;
	                  font-family: 'HelveticaNeue', 'Helvetica Neue', Helvetica, Arial, sans-serif; }

	                .button:hover,
	                button:hover,
	                input[type='submit']:hover,
	                input[type='reset']:hover,
	                input[type='button']:hover {
		                color: #222;
		                background: #ddd; /* Old browsers */
		                background: #ddd -moz-linear-gradient(top, rgba(255,255,255,.3) 0%, rgba(0,0,0,.3) 100%); /* FF3.6+ */
		                background: #ddd -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(255,255,255,.3)), color-stop(100%,rgba(0,0,0,.3))); /* Chrome,Safari4+ */
		                background: #ddd -webkit-linear-gradient(top, rgba(255,255,255,.3) 0%,rgba(0,0,0,.3) 100%); /* Chrome10+,Safari5.1+ */
		                background: #ddd -o-linear-gradient(top, rgba(255,255,255,.3) 0%,rgba(0,0,0,.3) 100%); /* Opera11.10+ */
		                background: #ddd -ms-linear-gradient(top, rgba(255,255,255,.3) 0%,rgba(0,0,0,.3) 100%); /* IE10+ */
		                background: #ddd linear-gradient(top, rgba(255,255,255,.3) 0%,rgba(0,0,0,.3) 100%); /* W3C */
	                  border: 1px solid #888;
	                  border-top: 1px solid #aaa;
	                  border-left: 1px solid #aaa; }

	                .button:active,
	                button:active,
	                input[type='submit']:active,
	                input[type='reset']:active,
	                input[type='button']:active {
		                border: 1px solid #666;
		                background: #ccc; /* Old browsers */
		                background: #ccc -moz-linear-gradient(top, rgba(255,255,255,.35) 0%, rgba(10,10,10,.4) 100%); /* FF3.6+ */
		                background: #ccc -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(255,255,255,.35)), color-stop(100%,rgba(10,10,10,.4))); /* Chrome,Safari4+ */
		                background: #ccc -webkit-linear-gradient(top, rgba(255,255,255,.35) 0%,rgba(10,10,10,.4) 100%); /* Chrome10+,Safari5.1+ */
		                background: #ccc -o-linear-gradient(top, rgba(255,255,255,.35) 0%,rgba(10,10,10,.4) 100%); /* Opera11.10+ */
		                background: #ccc -ms-linear-gradient(top, rgba(255,255,255,.35) 0%,rgba(10,10,10,.4) 100%); /* IE10+ */
		                background: #ccc linear-gradient(top, rgba(255,255,255,.35) 0%,rgba(10,10,10,.4) 100%); /* W3C */ }

	                .button.full-width,
	                button.full-width,
	                input[type='submit'].full-width,
	                input[type='reset'].full-width,
	                input[type='button'].full-width {
		                width: 100%;
		                padding-left: 0 !important;
		                padding-right: 0 !important;
		                text-align: center; }

	                /* Fix for odd Mozilla border & padding issues */
	                button::-moz-focus-inner,
	                input::-moz-focus-inner {
                    border: 0;
                    padding: 0;
	                }

                .landing11  {
	                background-image: url(images/11csi.jpg);
	                background-repeat: no-repeat;
                }
                .landing12 {
	                background-image: url(Account/images/branding/" + applicationnickname + "/" + companyname + @"/loginbackground.jpg);
	                background-repeat: no-repeat;
                }
                .landing13 {
	                background-image: url(images/13cdm.jpg);
	                background-repeat: no-repeat;
                }
                .landing14 {
	                background-image: url(Account/images/branding/" + applicationnickname + "/" + companyname + @"/loginbackground.jpg);
	                background-repeat: no-repeat;
                }
                .landing15 {
	                background-image: url(images/15cam.jpg);
	                background-repeat: no-repeat;
                }
                .landing16 {
	                background-image: url(images/16resource.jpg);
	                background-repeat: no-repeat;
                }
                .landing17 {
	                background-image: url(images/17rsadminplus20.jpg);
	                background-repeat: no-repeat;
                }
                .landing18 {
	                background-image: url(images/18rsadminsurv.jpg);
	                background-repeat: no-repeat;
                }
                .landing19 {
	                background-image: url(images/19rsadmin2020.jpg);
	                background-repeat: no-repeat;
                }
                </style><section class='landing12' id='intro' style='background-color:transparent;'>";
                //CP_lit_SectionHeader.Text = lit_SectionHeader.Text;
                //lit_FormDiv.Text = "<div style='align-content:center; text-align:center; border:2px solid black'>";
                //CP_lit_FormDiv.Text = lit_FormDiv.Text;
                #endregion
            }
        }
    }

}