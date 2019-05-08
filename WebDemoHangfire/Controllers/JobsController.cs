using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using WebDemoHangfire.Service.Intefaces;

namespace WebDemoHangfire.Controllers
{
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IJobToProcess _jobToProcess;

        public JobsController(IJobToProcess jobToProcess)
        {
            _jobToProcess = jobToProcess;
        }
        public IActionResult Index()
        {
            return Ok("Api Web HangFire runing...");
        }

        /// <summary>
        /// Fire and Forget jobs are executed only once and almost immediatelly after creation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("FireAndForget")]
        public IActionResult FireAndForget()
        {
            Console.WriteLine($"Request: {DateTime.Now}");

            var jobFireForget = BackgroundJob.Enqueue<IJobToProcess>(job => job.InsertUser("FireAndForget"));

            return Ok("Job Criado com Sucesso");
        }

        /// <summary>
        /// Job Delayed are executed only once too, but not immediately. It takes a certain time interval.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("JobDelayed")]
        public IActionResult JobDelayed()
        {
            Console.WriteLine($"Request: {DateTime.Now}");
            var jobDelayed = BackgroundJob.Schedule<IJobToProcess>(job => job.InsertUser($"JobDelayed"), TimeSpan.FromSeconds(30));

            ContinueWith(jobDelayed);

            return Ok("Job Criado com Sucesso");
        }

        /// <summary>
        /// Continuations are executed when its parent Id has been finished
        /// </summary>
        /// <returns></returns>
        private string ContinueWith(string jobId)
        {
            Console.WriteLine($"Request: {DateTime.Now}");
            //jobId é o id do serviço que o método aguardará para começar a ser executado.
            BackgroundJob.ContinueJobWith<IJobToProcess>(jobId, job => job.InsertUser("ContinueWith"));

            return "Job Criado com Sucesso";
        }

        /// <summary>
        /// Recurring jobs are fire many times on the specific CRON schedule
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RecurringJobAddOrUpdate")]
        public IActionResult RecurringJobAddOrUpdate()
        {
            var rnd = new Random();
            RecurringJob.AddOrUpdate<IJobToProcess>(job => job.InsertUser("RecurringJobAddOrUpdate"), Cron.Minutely);

            return Ok("Job Criado com Sucesso");
        }
    }
}