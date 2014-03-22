using System.Web.Mvc;

namespace Protal.Areas.Words
{
    public class WordsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Words";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Words_default",
                "Words/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
