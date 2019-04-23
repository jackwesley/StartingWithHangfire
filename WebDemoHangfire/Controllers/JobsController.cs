using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace WebDemoHangfire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet]
        public string Get()
        {
            Console.WriteLine($"Request: {DateTime.Now}");
            var jobFireForget = BackgroundJob.Enqueue(() => Console.WriteLine($"Fire and forget: {DateTime.Now}"));
            var jobDelayed = BackgroundJob.Schedule(() => Console.WriteLine($"Delayed: {DateTime.Now}"), TimeSpan.FromSeconds(30));
            BackgroundJob.ContinueJobWith(jobDelayed, () => Console.WriteLine($"Continuation: {DateTime.Now}"));
            RecurringJob.AddOrUpdate(() => Console.WriteLine($"Recurring: {DateTime.Now}"), Cron.Minutely);
            return "Jobs Criados com Sucesso";
        }
    }
}