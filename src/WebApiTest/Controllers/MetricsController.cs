using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Prometheus;

namespace WebApiTest.Controllers
{
    /*[RoutePrefix("api")]
    [Route("values")]*/
    public class MetricsController : ApiController
    {
        /*private static readonly Counter ProcessedJobCount = Metrics
            .CreateCounter("myapp_jobs_processed_total", "Number of processed jobs.");


        private static readonly Gauge UsersLoggedIn = Metrics
            .CreateGauge("myapp_users_logged_in", "Number of active user sessions",
                new GaugeConfiguration
                {
                    SuppressInitialValue = true
                });*/

        // GET api/values
        //[HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            /*ProcessedJobCount.Inc();
            UsersLoggedIn.Set(10);*/

            //var response = HttpContext.Current.Response;
            //response.StatusCode = 200;
            /*using (var outputStream = response.OutputStream)
            {
                await Metrics.DefaultRegistry.CollectAndExportAsTextAsync(outputStream);
            }*/
            /*var stream = new MemoryStream();
            await Metrics.DefaultRegistry.CollectAndExportAsTextAsync(stream);

            stream.CopyTo(response.OutputStream);*/

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new PushStreamContent(
                    async (stream, httpContent, arg3) =>
                    {
                        //await outputStream.WriteAsync(content, 0, content.Length);
                        await Metrics.DefaultRegistry.CollectAndExportAsTextAsync(stream);
                        stream.Close();
                    })
                { 
                    Headers =
                    {
                        ContentType = MediaTypeHeaderValue.Parse("text/plain")
                    }
                }

                //return new string[] { "value1", "value2" };

                /*response.StatusCode = 200;
                using (var outputStream = response.Body)
                {
                    return await ScrapeHandler.ProcessAsync(Metrics.DefaultCollectorRegistry, outputStream);
                }*/
            };
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
