/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers
 * for more information concerning the license and the contributors participating to this project.
 */

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Strava.Activities;
using Strava.Athletes;
using Strava.Authentication;
using Strava.Clients;
using Microsoft.AspNetCore.Authorization;

namespace Mvc.Client.Controllers
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        public ActivitiesController()
        {            
        }

        [HttpGet("~/activities"), HttpPost("~/activities")]
        public ActionResult Activities()
        {
            var model=new string[]{"ABCD","EFGH"};
            Console.WriteLine($"HttpContext.User.Identity.Name={HttpContext.User.Identity.Name}");
            // Console.WriteLine($"HttpContext.Session={HttpContext.Session.}");
            // StravaClient client = new StravaClient(User);
            return View(model);
        }
        public static async Task GetActivityListAsync(WebAuthentication wa)
        {

            StravaClient client = new StravaClient(wa);
            // Receive the currently authenticated athlete
            List<ActivitySummary> activities = await client.Activities.GetActivitiesAsync(20, 20);
            // IAuthenticationService authentication  = DependencyResolver.Current.GetService<IAuthenticationService>()

            foreach (ActivitySummary Activity in activities)
            {
                Console.WriteLine($"Email:{Activity.Name} {Activity.Type}");
            }
        }
    }
}
