using System.Threading.Tasks;
using System.Net.Http;

namespace Accelerator.Functions
{
    class SimRequest
    {
        public Task<HttpResponseMessage> SimPost(string url)
        {
            return Task.Run(() =>
            {
                HttpClient request = new HttpClient();
                {
                    request.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
                    request.DefaultRequestHeaders.Add("UserAgent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:56.0) Gecko/20100101 Firefox/56.0");
                    request.DefaultRequestHeaders.Add("Accept-Encoding", "zh-CN,zh;q=0.8,en-US;q=0.5,en;q=0.3");
                    request.DefaultRequestHeaders.Add("Accept-Language", "gzip, deflate");
                }
                try
                {
                    HttpResponseMessage response = request.GetAsync(url).Result;
                    return response;
                }
                catch
                {
                    return null;
                }
            });
        }
    }
}
