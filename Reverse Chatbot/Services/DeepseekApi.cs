using Microsoft.EntityFrameworkCore.Metadata;
using Reverse_Chatbot.Interfaces;
using Reverse_Chatbot.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reverse_Chatbot.Services
{
    internal class DeepseekApi:IDeepseekApi
    {
        static readonly HttpClient httpClient = new HttpClient();

        string ApiKey;
        static readonly string EndPoint = "https://api.deepseek.com/chat/completions";

        public DeepseekApi()
        {
            this.ApiKey = File.ReadAllText
                (@"C:\Users\Lenovo\Desktop\哪些东西？\密码\DeepSeekApi\apikey.txt").Trim(); 
        }

        //传进来一个json不好么?或者直接传入对应格式数组
        public async Task<string> PostAsync(List<DeepseekMessage> history)
        {
            var RequestData = new DeepseekRequest();//嗯,发送就发送,还是不把存储耦合在这里了
            RequestData.Messages.AddRange(history);

            string jsonBody = JsonSerializer.Serialize(RequestData);            
            var content = new StringContent(jsonBody,Encoding.UTF8, "application/json");
            
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, EndPoint))
            {
                //开始附加Api请求头
                requestMessage.Content = content;
                requestMessage.Headers.Add("Authorization", $"Bearer {ApiKey}");

                try
                {
                    var response = await httpClient.SendAsync(requestMessage);
                    // 如果状态码不是 2xx，会抛出异常
                    response.EnsureSuccessStatusCode();

                    string responsejson = await response.Content.ReadAsStringAsync();

                    var responseobject = JsonSerializer.Deserialize<DeepseekResponse>(responsejson);

                    if (responseobject != null && responseobject.Choices.Count > 0)
                    {
                        //返回首个message  这是因为回复其实也就一个并且是首个!
                        return responseobject.Choices[0].Message.Content;
                    }
                }
                catch (HttpRequestException ex)
                {
                    // 这里可以替换成你自己的日志记录或者异常处理逻辑
                    return $"网络请求错误: {ex.Message}";
                }
                catch (JsonException ex)
                {
                    return $"JSON 解析错误: {ex.Message}";
                }

            }
            return "异常错误";
        }
    }
}
