using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AndroidFeatures.Models
{
    internal class ApiModel
    {
        public HttpClient getClient()
        {
            HttpClient client = new();
            client.BaseAddress = new Uri("http://10.130.66.50:5229");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
