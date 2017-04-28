using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebAppLongPaths
{
    public class MvcApplication : System.Web.HttpApplication
    {
		protected void Application_Start()
        {
			System.AppContext.SetSwitch( "Switch.System.IO.BlockLongPaths", false );

			System.AppContext.SetSwitch( "Switch.System.IO.UseLegacyPathHandling", false );

			bool stillUsingLegacyPaths;
			AppContext.TryGetSwitch( "Switch.System.IO.UseLegacyPathHandling", out stillUsingLegacyPaths );

			bool stillBlockLongPaths;
			AppContext.TryGetSwitch( "Switch.System.IO.UseLegacyPathHandling", out stillBlockLongPaths );

			if( stillUsingLegacyPaths || stillBlockLongPaths ) {

				throw new Exception( "Testing will fail if we are using legacy path handling." );

			};


			AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
