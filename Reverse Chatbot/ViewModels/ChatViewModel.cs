using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Prism.Commands;
using Prism.Mvvm;
using Reverse_Chatbot.Interfaces;
using Reverse_Chatbot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Reverse_Chatbot.ViewModels
{   
    //先做出简单的记忆力功能
    internal class ChatViewModel:BindableBase
    {
        IDeepseekApi Deepseek;
        IMemoryContextFactory MemoryContextFactory;

        string response;
        string ask;
        public string Response { get => response; set => SetProperty(ref response, value); }
        public string Ask { get => ask; set => SetProperty(ref ask, value); }

        public ICommand PostCommand { get; set; }
        public ChatViewModel(IDeepseekApi deepseekApi,IMemoryContextFactory memoryContextFactory) 
        {
            this.MemoryContextFactory = memoryContextFactory;
            Deepseek = deepseekApi;
            PostCommand = new DelegateCommand(async()=> await PostAsync(Ask));
        }

        //Post时,发送的历史记忆结合最新输入
        async Task PostAsync(string request)
        {   
            using (var context = MemoryContextFactory.CreateContext()) {

                await context.DeepSeekMessageEntities.AddAsync
                    (new Models.DeepSeekMessageEntity { Role="user",Content = request});

                await context.SaveChangesAsync();

                var reply = await Deepseek.PostAsync
                (
                context.DeepSeekMessageEntities.
                Select((s) => new DeepseekMessage { Content = s.Content,Role=s.Role}).ToList());

                Ask = "";

                await Application.Current.Dispatcher.
                    BeginInvoke(new Action(() => { Response = reply; }));

                await context.DeepSeekMessageEntities.AddAsync
                    (new Models.DeepSeekMessageEntity { Role = "assistant", Content = reply });

                await context.SaveChangesAsync();
            }

            
        }
    }
}
