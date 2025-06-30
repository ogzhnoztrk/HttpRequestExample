using System.Net.Http.Headers;
using System.Text;

namespace HttpRequestExample.ExampleLibrary
{
    public static class HttpService
    {
        public static string ApiLink { get; internal set; }

        //Api linkini endpoint kısmına kadar oluşturur
        public static string _apiLinkGenerate(string ip, int port, bool useSsl, string startpath = "api"/*, string endpoint = ""*/)
        {
            var link = string.Format("{0}://{1}:{2}/{3}", (object)(useSsl ? "https" : "http"), (object)ip, (object)port, (object)startpath/*, (object)endpoint*/);
            return link;
        }

        public static Result Request(string? token, HttpType type, string endpoint)
        {
            return Request<object>(token, type, endpoint, null);
        }

        public static Result Request<TRequest>(string? token, HttpType type, string endpoint, TRequest? body)
        {
            Result response;

            try
            {
                var requestLink = string.Format("{0}/{1}", ApiLink, endpoint);

                var client = new HttpClient
                {
                    BaseAddress = new Uri(requestLink),
                    MaxResponseContentBufferSize = Int32.MaxValue
                };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                }

                string bodystr = JsonConvert.SerializeObject(body);

                Task<HttpResponseMessage> task;
                switch (type)
                {

                    case HttpType.Post:

                        //utf8 ve media type eklendi
                        task = client.PostAsync(requestLink, content: new StringContent(bodystr, Encoding.UTF8, "application/json"));
                        break;
                    case HttpType.Put:
                        //utf8 ve media type eklendi
                        task = client.PutAsync(requestLink, content: new StringContent(bodystr, Encoding.UTF8, "application/json"));
                        break;
                    case HttpType.Delete:
                        task = client.DeleteAsync(requestLink);
                        break;
                    default:
                        task = client.GetAsync(requestLink);
                        break;
                }

                task.Wait();

                using (HttpResponseMessage _response = task.Result)
                {
                    Task<string> taskResponse = _response.Content.ReadAsStringAsync();
                    taskResponse.Wait();
                    try
                    {
                        response = JsonConvert.DeserializeObject<Result>(taskResponse.Result);
                        if (!(response is Result))
                        {
                            response = new Result { code = (int)_response.StatusCode, message = taskResponse.Result, data = null };
                        }
                    }
                    catch
                    {
                        response = new Result { code = (int)_response.StatusCode, message = taskResponse.Result, data = null };
                    }
                }
            }
            catch (Exception ex)
            {
                //response = new Result<TResponse>(400, ex.Message);
                response = new Result(417, ex.Message);
            }

            return response;
        }

    }
}

/// <summary>
/// Http istek tipi
/// </summary>
/// <example>Get, Post, Put, Delete</example>
public enum HttpType
{
    Get,
    Post,
    Put,
    Delete
}