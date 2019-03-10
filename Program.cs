using System;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

//=====================
//Bot name @r2_Test_Bot
//=====================

namespace TelegramBotTest
{
    class Program
    {
        static TelegramBotClient Bot;

        static void Main(string[] args)
        {
            Bot = new TelegramBotClient("757952797:AAFg-__DCHl7d8p-3Mo6HmrHiPSGCg7gPcI");

            Bot.OnMessage += Bot_OnMessage;

            var me = Bot.GetMeAsync().Result;

            Console.WriteLine(me.FirstName);

            Bot.StartReceiving();
            Console.ReadKey();
            Bot.StopReceiving();
        }

        private static async void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            //e - user message
            var message = e.Message;
            if(message.Type!=MessageType.Text || message==null)
            {
                return;
            }
            Console.WriteLine(message.Text);

            switch(message.Text)
            {
                case "/start":
                    string Text = Function.StartCommand();                       
                    await Bot.SendTextMessageAsync(message.From.Id, Text);
                    break;

                case "/help":
                    string HelpText = Function.HelpCommand();                     
                    await Bot.SendTextMessageAsync(message.From.Id, HelpText);
                    break;                              
                case "/p":
                    string PText = Function.PMounthly();
                    await Bot.SendTextMessageAsync(message.From.Id, PText);
                    break;
                case "/r":
                    string RText = Function.RandomUrl();
                    await Bot.SendTextMessageAsync(message.From.Id, RText);
                    break;          
                   
                //case string k when k.Contains("/id"):
                //    string id = k.Replace("/id", "");
                    
                //    string MTesxt = Function.Member(id);
                //    await Bot.SendTextMessageAsync(message.From.Id, MTesxt);
                //    break;

                case string r when r.Contains("/r"):
                    r = r.Replace("/r", "");
                    int Rtimes = int.Parse(r);
                    while(Rtimes!=0)
                    {
                        string RTText = Function.RandomUrl();
                        await Bot.SendTextMessageAsync(message.From.Id, RTText);
                        Rtimes--;
                    }
                    break;
                case string p when p.Contains("/p"):
                    p = p.Replace("/p", "");
                    int Ptimes = int.Parse(p);
                    while (Ptimes != 0)
                    {
                        string PTText = Function.PMounthly();
                        await Bot.SendTextMessageAsync(message.From.Id, PTText);
                        Ptimes--;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
