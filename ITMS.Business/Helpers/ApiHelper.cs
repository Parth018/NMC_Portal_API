using System.Threading.Tasks;
using ITMS.Business.Common;
using ITMS.Business.Models.General;
using ITMS.Business.Enums.General;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Microsoft.AspNetCore.Http;
using AspNetCore.Http.Extensions;

namespace ITMS.Business.Helpers
{
   public static class ApiHelper
    {
        #region Api Request
        
		public static ResponseDetail SendApiRequest<T>(T data, string url, HttpMethod httpMethod)
        {
            var responseModel = new ResponseDetail();
            try
            {
                var baseUrl = System.IO.Path.Combine(Constant.siteConfiguration.ITMSWebAPIUrl, url);
                if (httpMethod == HttpMethod.Get || httpMethod == HttpMethod.Delete)
                {
                    baseUrl = baseUrl + Convert.ToString(data);
                }
                var client = new HttpClient { BaseAddress = new Uri(baseUrl) };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromMinutes(Constant.TimeoutLimitInMinutes);
                //client.DefaultRequestHeaders.Add(Constant.STRING_CULTURE, Thread.CurrentThread.CurrentUICulture.Name);

                var response = CallToApiResponse(data, httpMethod, client, baseUrl);
                if (response.IsSuccessStatusCode)
                {
                    SuccessStatusCodeTrue<T>(response, responseModel);
                }
                else
                {
                    ResponseDetail responseDetail;
                    if (SuccessStatusCodeFalse<T>(responseModel, response, out responseDetail)) return responseDetail;
                }
            }
            catch (Exception ex)
            {
                responseModel.Success = false;
                responseModel.Message = ex.Message;
                return responseModel;
            }
            return responseModel;
        }
        
		private static HttpResponseMessage CallToApiResponse<T>(T data, HttpMethod httpMethod, HttpClient client, string baseUrl)
        {
            HttpResponseMessage response;
            if (httpMethod == HttpMethod.Get)
            {
                response = client.GetAsync(baseUrl).Result;
            }
            else if (httpMethod == HttpMethod.Post)
            {
                response = client.PostAsJsonAsync(baseUrl, data).Result;
            }
            else if (httpMethod == HttpMethod.Put)
            {
                response = client.PutAsJsonAsync(baseUrl, data).Result;
            }
            else if (httpMethod == HttpMethod.Delete)
            {
                response = client.DeleteAsync(baseUrl).Result;
            }
            else
            {
                response = client.GetAsync(baseUrl).Result;
            }
            return response;
        }
        
		private static void SuccessStatusCodeTrue<T>(HttpResponseMessage response, ResponseDetail responseModel)
        {
            var content = response.Content;
            var result = content.ReadAsStringAsync().Result;
            responseModel.Success = true;
            dynamic returnObj = JObject.Parse(result);
            if (returnObj != null)
            {
                if (returnObj["Data"] != null)
                {
                    responseModel.Data = returnObj["Data"];
                }
                if (returnObj["Success"] != null)
                {
                    responseModel.Success = returnObj["Success"];
                }
                if (returnObj["MessageType"] != null)
                {
                    responseModel.MessageType = returnObj["MessageType"];
                }
                responseModel.Message = returnObj["Message"] != null ? returnObj["Message"].ToString() : string.Empty;
            }
        }
        
		private static bool SuccessStatusCodeFalse<T>(ResponseDetail responseModel, HttpResponseMessage response, out ResponseDetail responseDetail)
        {
            responseModel.Success = false;
            switch (response.StatusCode)
            {
                case HttpStatusCode.NotAcceptable:
                    responseModel.Message = "Wait for few seconds back-end server need to be ready";
                    responseModel.MessageType = MessageType.Error;
                    {
                        responseDetail = responseModel;
                        return true;
                    }
                case HttpStatusCode.ServiceUnavailable:
                    responseModel.Message = "Wait for few seconds back-end server services not avaliable.";
                    responseModel.MessageType = MessageType.Error;
                    {
                        responseDetail = responseModel;
                        return true;
                    }
            }
            var content = response.Content;
            var result = content.ReadAsStringAsync().Result;
            if (!string.IsNullOrEmpty(result))
            {
                dynamic returnObj = JObject.Parse(result);
                responseModel.Message = (returnObj != null && returnObj["Message"] != null) ? returnObj["Message"].ToString() : response.ReasonPhrase;
                if (returnObj != null && returnObj["MessageType"] != null)
                {
                    responseModel.MessageType = returnObj["MessageType"].ToString();
                }
            }
            else
            {
                responseModel.Message = response.ReasonPhrase;
            }
            responseDetail = null;
            return false;
        }
       
		#endregion

        #region Api Request Async
        
		public static async Task<ResponseDetail> SendApiRequestAsync<T>(T data, string url, HttpMethod httpMethod)
        {
            var responseModel = new ResponseDetail();
            try
            {
                var baseUrl = System.IO.Path.Combine(Constant.siteConfiguration.ITMSWebAPIUrl, url);
                if (httpMethod == HttpMethod.Get || httpMethod == HttpMethod.Delete)
                {
                    baseUrl = baseUrl + Convert.ToString(data);
                }
                var client = new HttpClient { BaseAddress = new Uri(baseUrl) };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromMinutes(Constant.TimeoutLimitInMinutes);
                //client.DefaultRequestHeaders.Add("culture", Thread.CurrentThread.CurrentUICulture.Name);

                var response = await CallToApiResponseAsync(data, httpMethod, client, baseUrl);
                if (response.IsSuccessStatusCode)
                {
                    await SuccessStatusCodeTrueAsync<T>(response, responseModel);
                }
                else
                {
                    await SuccessStatusCodeFalseAsync<T>(responseModel, response);
                }
            }
            catch (Exception ex)
            {
                responseModel.Success = false;
                responseModel.Message = ex.Message;
                return responseModel;
            }
            return responseModel;
        }
        
		private static async Task<HttpResponseMessage> CallToApiResponseAsync<T>(T data, HttpMethod httpMethod, HttpClient client, string baseUrl)
        {
            HttpResponseMessage response;
            if (httpMethod == HttpMethod.Get)
            {
                response = await client.GetAsync(baseUrl).ConfigureAwait(false);
            }
            else if (httpMethod == HttpMethod.Post)
            {
                response = await client.PostAsJsonAsync(baseUrl, data).ConfigureAwait(false);
            }
            else if (httpMethod == HttpMethod.Put)
            {
                response = await client.PutAsJsonAsync(baseUrl, data).ConfigureAwait(false);
            }
            else if (httpMethod == HttpMethod.Delete)
            {
                response = await client.DeleteAsync(baseUrl).ConfigureAwait(false);
            }
            else
            {
                response = await client.GetAsync(baseUrl).ConfigureAwait(false);
            }
            return response;
        }
        
		private static async Task SuccessStatusCodeTrueAsync<T>(HttpResponseMessage response, ResponseDetail responseModel)
        {
            var content = response.Content;
            var result = await content.ReadAsStringAsync().ConfigureAwait(false);
            responseModel.Success = true;
            dynamic returnObj = JObject.Parse(result);
            if (returnObj != null)
            {
                if (returnObj["Data"] != null)
                {
                    responseModel.Data = returnObj["Data"];
                }
                if (returnObj["Success"] != null)
                {
                    responseModel.Success = returnObj["Success"];
                }
                if (returnObj["MessageType"] != null)
                {
                    responseModel.MessageType = returnObj["MessageType"];
                }
                responseModel.Message = returnObj["Message"] != null ? returnObj["Message"].ToString() : string.Empty;
            }
        }
        private static async Task SuccessStatusCodeFalseAsync<T>(ResponseDetail responseModel, HttpResponseMessage response)
        {
            responseModel.Success = false;
            var content = response.Content;
            var result = await content.ReadAsStringAsync().ConfigureAwait(false);
            if (!string.IsNullOrEmpty(result))
            {
                dynamic returnObj = JObject.Parse(result);
                responseModel.Message = (returnObj != null && returnObj["Message"] != null)
                    ? returnObj["Message"].ToString()
                    : response.ReasonPhrase;
                if (returnObj != null && returnObj["MessageType"] != null)
                {
                    responseModel.MessageType = returnObj["MessageType"].ToString();
                }
            }
            else
            {
                responseModel.Message = response.ReasonPhrase;
            }
        }
        #endregion

        public static async Task<ResponseDetail> SendApiWithFileRequestAsync(IFormFile file, string url, HttpMethod httpMethod)
        {
            var responseModel = new ResponseDetail();
            try
            {
                var baseUrl = Constant.siteConfiguration.ITMSWebAPIUrl + url;

                var client = new HttpClient { BaseAddress = new Uri(baseUrl) };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var multipartContent = new MultipartFormDataContent();
                var t = new StreamContent(file.OpenReadStream());
                t.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
                multipartContent.Add(t, "File", file.FileName);
                var response = await client.PostAsync(baseUrl, multipartContent).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    await SuccessStatusCodeTrueAsync<object>(response, responseModel);
                }
                else
                {
                    await SuccessStatusCodeFalseAsync<object>(responseModel, response);
                }
            }
            catch (Exception ex)
            {
                responseModel.Success = false;
                responseModel.Message = ex.Message;
                return responseModel;
            }
            return responseModel;
        }
    }
}
