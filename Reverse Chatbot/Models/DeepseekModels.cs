using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Reverse_Chatbot.Models
{
    //最基本的消息格式
    public class DeepseekMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
    //包装成为符合API要求的请求体
    public class DeepseekRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = "deepseek-v4-pro";
        [JsonPropertyName("messages")]
        public List<DeepseekMessage> Messages { get; set; } 
            = new List<DeepseekMessage>() 
            { new DeepseekMessage() 
                { Role = "system", 
                  Content = ReverseSkill.Reverse
                } 
            };
        [JsonPropertyName("temperature")]
        public double Temperature { get; set; } = 0.7;

        [JsonPropertyName ("stream")]
        public bool Stream = false;
    }
    //看起来下面这两个配合我不是很会用啊
    public class DeepseekResponse
    {
        [JsonPropertyName("choices")]
        public List<DeepseekChoice> Choices { get; set; }
    }

    public class DeepseekChoice
    {
        [JsonPropertyName("message")]
        public DeepseekMessage Message { get; set; }
    }
}
