using System;
using Hangfire;
using Hangfire.States;
using Microsoft.AspNetCore.Mvc;
using WebDemoHangfire.Service.Intefaces;

namespace WebDemoHangfire.Controllers
{
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IJobToProcess _jobToProcess;
        private readonly IBackgroundJobClient _backgroundJobs;
        
        public JobsController(IJobToProcess jobToProcess,
                              IBackgroundJobClient backgroundJobs)
        {
            _jobToProcess = jobToProcess;
            _backgroundJobs = backgroundJobs;
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


            //Inserindo em uma fila específica
            _backgroundJobs.Create<IJobToProcess>(job => job.InsertUser("FireAndForget"), new EnqueuedState("fila1"));

            //var jobFireForget = _backgroundJobs.Enqueue<IJobToProcess>(job => job.InsertUser("FireAndForget"));
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
            var jobDelayed = _backgroundJobs.Schedule<IJobToProcess>(job => 
                                                    job.InsertUser($"JobDelayed"), 
                                                    TimeSpan.FromSeconds(30));

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
            _backgroundJobs.ContinueJobWith<IJobToProcess>(jobId, job => job.InsertUser("ContinueWith"));

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
            RecurringJob.AddOrUpdate<IJobToProcess>(
                                                methodCall: job => job.InsertUser("RecurringJobAddOrUpdate"),
                                                cronExpression: Cron.Minutely,
                                                queue: "fila2");

            return Ok("Job Criado com Sucesso");
        }
    }
}