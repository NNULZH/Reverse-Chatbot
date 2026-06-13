using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverse_Chatbot.Interfaces
{
    internal interface IDeepseekApi
    {
        public Task<string> PostAsync(string request);
    }
}
