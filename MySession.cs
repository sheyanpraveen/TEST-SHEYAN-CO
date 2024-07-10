using System;
using System.Web;

namespace RRD.GRESAdmin
{
    public class MySession
    {
        // private constructor     
        private MySession() { }
        // Gets the current session.     
        public static MySession Current
        {
            get
            {

                MySession session = (MySession)HttpContext.Current.Session["__MySession__"]; ;

                if (session == null)
                {
                    session = new MySession();
                    HttpContext.Current.Session["__MySession__"] = session;
                }
                return session;
            }
        }

        public Guid ApplicationID { get; set; }
        public String ApplicationName { get; set; }
        public Guid UserID { get; set; }
        public String NodeID { get; set; }
        public String NodeURL { get; set; }
        public String TreeViewSelectedNodeDetails { get; set; }
        public bool RefreshParent { get; set; }
    }
}