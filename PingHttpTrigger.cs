using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using System.Net;
using oca.model;

namespace OYProject
{
    public static class PingHttpTrigger
    {
        // private readonly IMyService _service;
        // public PingHttpTrigger(IMyService service){
        //     this._service = service ?? throw new ArgumentNullException(nameof(service));
        // }
        [FunctionName(nameof(PingHttpTrigger))]
        [OpenApiOperation(operationId : "sabangnet", tags : new[] {"product"})]
        [OpenApiSecurity(schemeName:"function_key", schemeType: SecuritySchemeType.ApiKey , Name =  "x-functions-key", In = OpenApiSecurityLocationType.Header)]
        [OpenApiParameter(name:"name", In =ParameterLocation.Query, Required =true, Description ="Name of Person")]
        [OpenApiParameter(name:"gdsCd", In =ParameterLocation.Query, Required =true, Description ="Code of Product")]
        //[OpenApiRequestBody("application/json", typeof(SabangnetProduct),  Description = "JSON request body containing { product }")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK , contentType:"application/json", bodyType: typeof(ResponseMessage))]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "sabangnet")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

        
            // var res = new ResponseMessage(){ Message = responseMessage}; 

            var service = new MyService(); 
            var result = service.GetMessage(name); 
            var res = new ResponseMessage(){Message = result}; 
            return new OkObjectResult(res);
        }
    }



    public interface IMyService {
        string GetMessage(string name);
    }

    public class MyService : IMyService {
        public string GetMessage(string name){
            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";
            
            return responseMessage; 

        } 
    }
}
