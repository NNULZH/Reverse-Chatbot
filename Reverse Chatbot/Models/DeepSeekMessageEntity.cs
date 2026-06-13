using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Reverse_Chatbot.Models
{
    internal class DeepSeekMessageEntity
    {   
        public int Id { get; set; }
        public string Role { get; set; }
        public string Content { get; set; }

    }
}
