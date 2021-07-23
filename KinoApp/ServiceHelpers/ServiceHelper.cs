using Kino.API.Helpers;
using KinoApp.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KinoApp.ServiceHelpers
{
    public static class ServiceHelper
    {
        public static RestClient CreateClient()
        {
            return new RestClient(Helpers.ConfigurationHelper.GetConfiguration().GetSection("BaseAPIUrl").Value);
        }

        public static RestRequest CreateGetRequest(string endpoint, string[] parameters)
        {
            var completeEndPoint = parameters != null ? string.Format(endpoint, parameters) : endpoint;
            return new RestRequest(completeEndPoint, Method.GET);
        }

        public static RestRequest CreatePostRequest(string endpoint, string[] parameters, object data)
        {
            var completeEndPoint = parameters != null ? string.Format(endpoint, parameters) : endpoint;
            var request = new RestRequest(completeEndPoint, Method.POST);
            request.AddJsonBody(data);
            return request;
        }

        public static RestRequest CreateDeleteRequest(string endpoint, string[] parameters)
        {
            var completeEndPoint = parameters != null ? string.Format(endpoint, parameters) : endpoint;
            return new RestRequest(completeEndPoint, Method.DELETE);
        }

        public static RestRequest CreateUpdateRequest(string endpoint, string[] parameters, object data)
        {
            var completeEndPoint = parameters != null ? string.Format(endpoint, parameters) : endpoint;
            var request = new RestRequest(completeEndPoint, Method.PUT);
            request.AddJsonBody(data);
            return request;
        }
    }
}
