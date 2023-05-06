using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Model;
using Services;
using Repositories;

namespace PhotoSharingApp.Func
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        private readonly DDTableService _tableService;
        private PhotoRepository _photoRepository;

        public Function1(ILogger<Function1> logger, DDTableService tableService)
        {
            _logger = logger;
            _tableService = tableService;
        }

        [Function("Function1")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            //   var photos = _photoService.GetPhotos();
            var entities = _tableService.GetAllEntities();
            _photoRepository = new PhotoRepository(entities);
            var photos = _photoRepository.GetAllPhotos();
            foreach (var photo in photos)
            {
                response.WriteString(photo + "\n");
            }

            return response;
        }
    }
}
