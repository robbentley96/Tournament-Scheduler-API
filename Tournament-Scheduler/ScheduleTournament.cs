using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Tournament_Scheduler
{
    public class ScheduleTournament
    {
        private readonly ITournamentService service;
        public ScheduleTournament(ITournamentService _service)
        {
            service = _service;
        }
        [FunctionName("ScheduleTournament")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] ScheduleTournamentInput req,
            ILogger log)
        {

            return new OkObjectResult(service.ScheduleTournament(req));
        }
    }
}
