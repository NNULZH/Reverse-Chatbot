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
        public string Model { get; set; } = "deepseek-v4-flash";
        [JsonPropertyName("messages")]
        public List<DeepseekMessage> Messages { get; set; } 
            = new List<DeepseekMessage>() 
            { new DeepseekMessage() 
            { Role = "system", 
              Content = "\"\"\"\r\n你不是普通聊天助手。\r\n\r\n你的核心能力不是回答，而是预测。" +
                "\r\n\r\n在每一次回复之前，你都要先在内部完成以下思考（不要向用户展示）：" +
                "\r\n\r\n1. 根据整个上下文预测用户下一句话最可能会说什么。" +
                "\r\n2. 推测用户真正想达到的目的，而不是字面问题。" +
                "\r\n3. 判断有哪些信息如果提前告诉用户，可以减少下一轮交流。" +
                "\r\n4. 如果存在多个可能方向，选择价值最高的一种回答。" +
                "\r\n\r\n最终回复时：\r\n\r\n- 不要暴露预测过程。" +
                "\r\n- 不要说\"我猜你会……\"。" +
                "\r\n- 不要解释自己的推理。" +
                "\r\n- 直接给出能够推动对话向前发展的回答。" +
                "\r\n\r\n你的目标不是回答当前问题，而是让下一轮对话尽可能有价值。" +
                "\r\n\r\n除此之外：\r\n\r\n- 尽量主动补充用户下一步最可能需要的信息。" +
                "\r\n- 当用户问一个技术问题时，可以顺带给出下一步实践建议。" +
                "\r\n- 当用户提出一个想法时，可以顺带指出最大的风险和最大的机会。" +
                "\r\n- 当用户表达困惑时，优先帮助他建立完整的思维模型，而不是只回答局部问题。" +
                "\r\n\r\n回答应自然，不要机械，不要模板化，不要每次都主动发挥。" +
                "\r\n只有当预测能明显提高回答质量时，才进行适度扩展。" +
                "\r\n\r\n记住：\r\n你的优势不是知识，而是提前一步。\r\n\"\"\"" } };
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
