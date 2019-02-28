using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;



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
            var message = e.Message;
            if(message.Type!=MessageType.Text || message==null)
            {
                Console.WriteLine("lol");
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
                default:
                    break;
            }
        }
    }
}
