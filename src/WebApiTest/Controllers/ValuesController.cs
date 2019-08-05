using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Prometheus;

namespace WebApiTest.Controllers
{
    /*[RoutePrefix("api")]
    [Route("values")]*/
    public class ValuesController : ApiController
    {
        private static readonly Counter ProcessedJobCount = Metrics
                .CreateCounter("myapp_jobs_processed_total", "Number of processed jobs.");


        private static readonly Gauge UsersLoggedIn = Metrics
            .CreateGauge("myapp_users_logged_in", "Number of active user sessions",
                new GaugeConfiguration
                {
                    SuppressInitialValue = true
                });

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            ProcessedJobCount.Inc();
            UsersLoggedIn.Set(DateTime.Now.Second);

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
