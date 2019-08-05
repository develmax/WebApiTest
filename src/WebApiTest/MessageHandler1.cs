using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Routing;
using System.Web.Http.WebHost;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApiTest
{
    public class MessageHandler1 : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //Debug.WriteLine("Process request");
            // Call the inner handler.
            var sw = Stopwatch.StartNew();
            var response = await base.SendAsync(request, cancellationToken);
            //Debug.WriteLine("Process response");

            var method = request.Method.Method;

            var routeTemplate =
                request.GetConfiguration().Routes.GetVirtualPath(request, null, request.GetRouteData().Values)
                    .VirtualPath;
                /*((IHttpRouteData[])request.GetConfiguration().Routes.GetRouteData(request).Values["MS_SubRoutes"])
                .First().Route.RouteTemplate;*/

            var handler = routeTemplate;
                //GetController(request, (HttpContextBase)request.Properties["MS_HttpContext"]);
                //request.GetRouteData().Route.GetVirtualPath(request, request.GetRouteData().Values).VirtualPath;
            //GetHttpMetricPath(httpContext);

            var statusCode = response.StatusCode.ToString();

            sw.Stop();

            HttpMetrics.HttpRequestDurationSeconds
                .Labels(method, handler, statusCode)
                .Observe(sw.Elapsed.TotalSeconds);

            HttpMetrics.HttpRequestsTotal
                .Labels(method, handler, statusCode).Inc();

            return response;
        }

        private string GetController(HttpRequestMessage request, HttpContextBase httpContext)
        {
            var routeData = request.GetRequestContext().RouteData;
            var handler = ((HttpControllerHandler) httpContext.Handler);
            //var routeData = ((HttpControllerHandler)httpContext.Handler).RequestContext.RouteData;

            var routeValues = (RouteValueDictionary)routeData.Values;
            var matchedRouteBase = routeData.Route;
            var matchedRoute = matchedRouteBase as Route;

            if (matchedRoute != null)
            {
                var Route = matchedRoute.Url ?? string.Empty;
            }

            var virtualPathData = getVirtualPathData(httpContext, routeValues);
            //AssignRouteValues(httpContext, routeValues);
            return virtualPathData.VirtualPath;
        }

        protected virtual VirtualPathData getVirtualPathData(HttpContextBase httpContext, RouteValueDictionary routeValues)
        {
            return RouteTable.Routes.GetVirtualPath(((MvcHandler)httpContext.Handler).RequestContext, routeValues);
        }

        private void AssignRouteValues(HttpContextBase httpContext, RouteValueDictionary routeValues)
        {
            /*var virtualPathData = getVirtualPathData(httpContext, routeValues);

            if (virtualPathData != null)
            {
                var vpdRoute = virtualPathData.Route as Route;
                if (vpdRoute != null)
                {
                    RouteDefaults = vpdRoute.Defaults;
                    RouteConstraints = vpdRoute.Constraints;
                    RouteDataTokens = virtualPathData.DataTokens;
                    RouteValues = routeValues;
                }
            }*/
        }




    }
}