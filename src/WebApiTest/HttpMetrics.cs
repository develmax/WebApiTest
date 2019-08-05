using Prometheus;

namespace WebApiTest
{
    public static class HttpMetrics
    {
        public static Counter HttpRequestsTotal = Metrics.CreateCounter(
        "http_requests_total",
            "The total count of http requests", "method", "handler", "code");

        public static Histogram HttpRequestDurationSeconds = Metrics.CreateHistogram(
            "http_request_duration_seconds",
            "Total duration of http request", new HistogramConfiguration
            {
                Buckets = new[] { 0, .005, .01, .025, .05, .075, .1, .25, .5, .75, 1, 2.5, 5, 7.5, 10, 30, 60, 120, 180, 240, 300 },
                // Histogram.LinearBuckets(start: 1, width: 1, count: 64),
                LabelNames = new[] { "method", "handler", "code" }
            });

        static HttpMetrics()
        {
            
            /*HttpRequestsTotal = Metrics.Counter()
                .Name("http_requests_total")
                .Help("The total count of http requests")
                .LabelNames("method", "handler", "code")
                .Register();

            HttpRequestDurationSeconds = Metrics.Histogram()
                .Name("http_request_duration_seconds")
                .Buckets(0, .005, .01, .025, .05, .075, .1, .25, .5, .75, 1, 2.5, 5, 7.5, 10, 30, 60, 120, 180, 240, 300)
                .Help("Total duration of http request")
                .LabelNames("method", "handler", "code")
                .Register();*/
        }
    }
}