using System;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using WA.Pizza.Infrastructure.Abstractions;

namespace WA.Pizza.Api.Controllers;

public class JobTestController:  BaseApiController
{
    private readonly IJobService _jobService;
    private readonly IRecurringJobManager _recurringJobManager;

    public JobTestController(
        IJobService jobService, 
        IRecurringJobManager recurringJobManager)
    {
        _jobService = jobService;
        _recurringJobManager = recurringJobManager;
    }
    
    // [HttpGet("/ReccuringJob")]
    // public ActionResult CreateReccuringJob()
    // {
    //     _recurringJobManager.AddOrUpdate("jobId", () => _jobService.ReccuringJob(), Cron.Minutely);
    //     return Ok();
    // }
}