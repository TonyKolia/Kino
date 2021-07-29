using KinoApp.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace KinoApp.ServiceHelpers
{
    public class ServiceMethods : IServiceMethods
    {
        public T PerformGet<T>(string endpoint, string[] parameters, out OperationResult result)
        {
            result = new OperationResult();
            var response = ServiceHelper.CreateClient().Execute(ServiceHelper.CreateGetRequest(endpoint, parameters));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            else
            {
                result.Success = false;
                result.Message = JsonConvert.DeserializeObject<string>(response.Content);
                return default;
            }
        }

        public void PerformPost(string endpoint, string[] parameters, object data, out OperationResult result)
        {
            var response = ServiceHelper.CreateClient().Execute(ServiceHelper.CreatePostRequest(endpoint, parameters, data));
            result = new OperationResult 
            {
                Success = response.StatusCode == HttpStatusCode.Created,
                Message = response.StatusCode == HttpStatusCode.Created ? "Created Successfully!" : JsonConvert.DeserializeObject<string>(response.Content)
            };
        }

        public void PerformDelete(string endpoint, string[] parameters, out OperationResult result)
        {
            var response = ServiceHelper.CreateClient().Execute(ServiceHelper.CreateDeleteRequest(endpoint, parameters));
            result = new OperationResult
            {
                Success = response.StatusCode == HttpStatusCode.OK,
                Message = response.StatusCode == HttpStatusCode.OK ? "Deleted Successfully!" : JsonConvert.DeserializeObject<string>(response.Content)
            };
        }

        public void PerformUpdate(string endpoint, string[] parameters, object data, out OperationResult result)
        {
            var response = ServiceHelper.CreateClient().Execute(ServiceHelper.CreateUpdateRequest(endpoint, parameters, data));
            result = new OperationResult
            {
                Success = response.StatusCode == HttpStatusCode.OK,
                Message = response.StatusCode == HttpStatusCode.OK ? "Updated Successfully!" : JsonConvert.DeserializeObject<string>(response.Content)
            };
        }
    }
}
