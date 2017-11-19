using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Configuration;

namespace MorningGirl.SpStudyInNagoya8.GoogleToSharePoint
{
    public static class DeleteSharePointTask
    {
        [FunctionName("DeleteSharePointTask")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("SharePoint Task Delete Functions Start.");

            var spManager = new SharePointManager(
                 ConfigurationManager.ConnectionStrings["SharePointConnection"].ConnectionString);

            spManager.DeleteSharePointData();

            return req.CreateResponse(HttpStatusCode.OK, "SharePoint Task Delete Complete ");
        }
    }
}
