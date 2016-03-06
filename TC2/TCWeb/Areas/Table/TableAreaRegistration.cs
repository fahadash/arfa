using System.Web.Mvc;

namespace TCWeb.Areas.Table
{
    public class TableAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Table";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Table_default",
                "Table/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );


            context.MapRoute(
                "Table_1",
                "Table/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
