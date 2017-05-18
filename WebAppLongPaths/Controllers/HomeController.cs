using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppLongPaths.Controllers {
	public class HomeController : Controller {
		public ActionResult Index() {
			return View();
		}

		public ActionResult About() {
			ViewBag.Message = "Your application description page.";

			return View();
		}


		public ActionResult Contact() {
			ViewBag.Message = "Your contact page.";

			string dir = "";
			foreach( var num in new[] {1,2,3,4,5,6,7,8,9,0} ) {
				dir = dir + "/" + new String( num.ToString()[0], 30 );
			}

			/*System.AppContext.SetSwitch( "Switch.System.IO.UseLegacyPathHandling", false );
			System.AppContext.SetSwitch( "Switch.System.IO.BlockLongPaths", false );*/
			AssemblyInit();

			var fullDir = Server.MapPath( $"~/Temp{dir}/" );

			Directory.CreateDirectory( fullDir );
			
			using( StreamWriter _testData = new StreamWriter( Server.MapPath( $"~/Temp{dir}/data.txt" ), true ) ) {
				_testData.WriteLine( "Hello" ); // Write the file.
			}

			return View();
		}

		// http://stackoverflow.com/questions/40722086/uselegacypathhandling-is-not-loaded-properly-from-app-config-runtime-element
		public static void AssemblyInit() {
			// Check to see if we're using legacy paths
			bool stillUsingLegacyPaths;
			if( AppContext.TryGetSwitch( "Switch.System.IO.UseLegacyPathHandling", out stillUsingLegacyPaths ) && stillUsingLegacyPaths ) {
				// Here's where we trash the private cached field to get this to ACTUALLY work.
				var switchType = Type.GetType( "System.AppContextSwitches" ); // <- internal class, bad idea.
				if( switchType != null ) {
					AppContext.SetSwitch( "Switch.System.IO.UseLegacyPathHandling", false );   // <- Should disable legacy path handling
					AppContext.SetSwitch( "Switch.System.IO.BlockLongPaths", false );   // <- Should disable legacy path handling

					// Get the private field that is used for caching the path handling value (bad idea).
					var legacyField = switchType.GetField( "_useLegacyPathHandling", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic );
					legacyField?.SetValue( null, (Int32)0 ); // <- caching uses 0 to indicate no value, -1 for false, 1 for true.

					legacyField = switchType.GetField( "_blockLongPaths", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic );
					legacyField?.SetValue( null, (Int32)0 ); // <- caching uses 0 to indicate no value, -1 for false, 1 for true.
				}
			}
		}
	}
}