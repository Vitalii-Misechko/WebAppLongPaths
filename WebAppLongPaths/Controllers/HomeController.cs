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
			
			using( StreamWriter _testData = new StreamWriter( Server.MapPath( $"~/Temp{dir}/data.txt" ), true ) ) {
				_testData.WriteLine( "Hello" ); // Write the file.
			}

			return View();
		}
	}
}