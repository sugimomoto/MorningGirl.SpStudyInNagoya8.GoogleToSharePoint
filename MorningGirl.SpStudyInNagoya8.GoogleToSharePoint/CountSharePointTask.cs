using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Configuration;

using Newtonsoft.Json;

namespace MorningGirl.SpStudyInNagoya8.GoogleToSharePoint
{
    public static class CountSharePointTask
    {
        [FunctionName("CountSharePointTask")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            
            // Body�̏����擾
            var jsondata = JsonConvert.DeserializeObject<GoogleDialogRequestBody>(await req.Content.ReadAsStringAsync());

            log.Info(req.Content.ReadAsStringAsync().ToString());

            log.Info($"Target Datetime {jsondata.result.parameters.datetime.ToString()}");



            var spManager = new SharePointManager(
                 ConfigurationManager.ConnectionStrings["SharePointConnection"].ConnectionString);
            
            // SharePoint�̃^�X�N�̌������m�F
            var count = spManager.CountSharePointData(jsondata.result.parameters.datetime);
            
            log.Info(count.ToString());

            // ���X�|���X�pBody��Json�N���X�̍쐬
            var response = new GoogleDialogResponseBody()
            {
                speech = $"{jsondata.result.parameters.datetime.ToShortDateString()}��SharePoint�̃^�X�N��{count.ToString()}���ł�",
                displayText = $"{jsondata.result.parameters.datetime.ToShortDateString()}��SharePoint�̃^�X�N��{count.ToString()}���ł�"
            };
            
            // Response�̍쐬
            var respo = req.CreateResponse(HttpStatusCode.OK, response);            
            respo.Headers.Add("ContentType", "application/json");

            return respo;
        }
    }
}
