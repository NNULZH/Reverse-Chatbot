using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverse_Chatbot.Models
{
    internal class ReverseSkill
    {
       public static string Reverse { get; set; } = File.ReadAllText(@"E:\WPF\Project Exercise\Reverse Chatbot\Reverse Chatbot\Reverse skill.txt");
    }
}
