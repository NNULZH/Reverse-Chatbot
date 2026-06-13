using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Prism.Commands;
using Prism.Mvvm;
using Reverse_Chatbot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Reverse_Chatbot.ViewModels
{
    internal class ChatViewModel:BindableBase
    {
        IDeepseekApi Deepseek;

        string response;
        public string Response { get => response; set => SetProperty(ref response, value); }
        public string Ask { get; set; }

        public ICommand PostCommand { get; set; }
        public ChatViewModel(IDeepseekApi deepseekApi) 
        {
            Deepseek = deepseekApi;
            PostCommand = new DelegateCommand(async()=> await PostAsync(Ask));
        }

        async Task PostAsync(string request)
        {
            var reply = await Deepseek.PostAsync(request);

            await Application.Current.Dispatcher.
                BeginInvoke(new Action(() => { Response = reply; }));
        }
    }
}
