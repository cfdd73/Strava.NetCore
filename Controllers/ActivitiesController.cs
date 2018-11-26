/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers
 * for more information concerning the license and the contributors participating to this project.
 */

using Microsoft.AspNetCore.Mvc;

namespace Mvc.Client.Controllers
{
    public class ActivitiesController : Controller
    {
        [HttpGet("~/activities0")]
        public ActionResult Activities0() => View();

        [HttpGet("~/activities"), HttpPost("~/activities")]
        public ActionResult Activities()
        {
            var model=new string[]{"ABCD","EFGH"};
            return View(model);
        }
    }
}
