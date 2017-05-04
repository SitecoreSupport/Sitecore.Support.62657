using Sitecore.Pipelines;
using Sitecore.Social.Client.Mvc.Areas.Social;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sitecore.Support.Social.Client.Mvc.Pipelines.Initialize
{
  public class RegisterSocialArea
  {
    public virtual void Process(PipelineArgs args)
    {
      List<RouteBase> source = RouteTable.Routes.Where<RouteBase>(delegate (RouteBase route) {
        Route route2 = route as Route;
        return ((((route2 != null) && (route2.Defaults != null)) && route2.Defaults.ContainsKey("area")) && (route2.Defaults["area"].ToString() == "Social"));
      }).ToList<RouteBase>();
      if (!source.Any<RouteBase>())
      {
        new SocialAreaRegistration().RegisterArea(new AreaRegistrationContext("Social", RouteTable.Routes));
      }
      else
      {
        foreach (Route route in source)
        {
          if (route != null)
          {
            route.DataTokens["UseNamespaceFallback"] = true;
          }
        }
      }
    }
  }
}
