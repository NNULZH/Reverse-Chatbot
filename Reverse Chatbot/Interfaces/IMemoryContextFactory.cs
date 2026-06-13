using Reverse_Chatbot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverse_Chatbot.Interfaces
{
    internal interface IMemoryContextFactory
    {
        MemoryContext CreateContext();
    }
}
