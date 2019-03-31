using System;
using System.Linq;
using HtmlAgilityPack;

namespace TelegramBotTest
{
    class Function
    {
        public static string HelpCommand()
        {
            return @"
/p - mounthly pictures
/r - random picture";
        }

        public static string StartCommand()
        {
           return @"
hi there !
enter /help to see more command";
        }
               
        public static string PMounthly()
        {
            Random rand = new Random();
            int num = rand.Next(0, 7);
            string[] url = new string[]
            {
                "https://www.pixiv.net/ranking.php?mode=original",
                "https://www.pixiv.net/ranking.php?mode=daily",
                "https://www.pixiv.net/ranking.php?mode=weekly",
                "https://www.pixiv.net/ranking.php?mode=monthly",
                "https://www.pixiv.net/ranking.php?mode=daily&content=illust",
                "https://www.pixiv.net/ranking.php?mode=weekly&content=illust",
                "https://www.pixiv.net/ranking.php?mode=monthly&content=illust",
                "https://www.pixiv.net/ranking.php?mode=male"
            };
            return Parse(url[num]);
        }
        public static string RandomUrl()
        {
            Random rand = new Random();
            int num = rand.Next(0, 7);
            string RandomUrl;
            string[] url = new string[]
            {
                "https://www.pixiv.net/ranking.php?mode=original&date=20",
                "https://www.pixiv.net/ranking.php?mode=daily&date=20",
                "https://www.pixiv.net/ranking.php?mode=weekly&date=20",
                "https://www.pixiv.net/ranking.php?mode=monthly&date=20",
                "https://www.pixiv.net/ranking.php?mode=daily&content=illust&date=20",
                "https://www.pixiv.net/ranking.php?mode=weekly&content=illust&date=20",
                "https://www.pixiv.net/ranking.php?mode=monthly&content=illust&date=20",
                "https://www.pixiv.net/ranking.php?mode=male&date=20"
            };

            string Mounthfirst = null;
            string Dayfirts = null;

            //set random date
            int year = rand.Next(13, 19);
            int mounth = rand.Next(1, 12);
            int day = rand.Next(1, 28);

            if(year==19)
            {
                mounth = rand.Next(1, 2);
                day = rand.Next(1, 26);
            }
            if(mounth< 9)
            {
               Mounthfirst="0"+mounth.ToString();
                
            }
            if (day < 9)
            {
                Dayfirts = "0" + day.ToString();
            }

            RandomUrl = url[num];
            RandomUrl += year.ToString();

            if (Mounthfirst == null)
            {
                RandomUrl += mounth.ToString();
            }
            else
            {
                RandomUrl += Mounthfirst;
            }

            if (Dayfirts == null)
            {
                RandomUrl += day.ToString();
            }
            else
            {
                RandomUrl += Dayfirts;
            }

            return Parse(RandomUrl);
        }
        public static string Member(string id)
        {
            string Result = null;
            Random rand = new Random();

            string url = "https://www.pixiv.net/member.php?id=" + id;

            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);
            HtmlNode[] nodes = null;

            nodes = document.DocumentNode.SelectNodes("//div/div[2]/div/div[1]/div[2]/ul[1]/li/a").ToArray();

            int num = rand.Next(0, nodes.Length);
            Result = nodes[num].InnerHtml.ToString();

            Result = Result.Remove(0, 10);
            Result = Result.Remove(95);
            //get image url

            //get full resulution image
            Result = Result.Replace("c/600x1200_90/", "");
                
            return Result;
        }

        public static string Parse(string url)
        {
            string Result = null;
            Random rand = new Random();

            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);
            HtmlNode[] nodes = null;

            try
            {
                nodes = document.DocumentNode.SelectNodes("//div[2]/a/div").ToArray();                       
            }
            catch (Exception)
            {
                if (nodes == null)
                {
                    document = web.Load("https://www.pixiv.net/ranking.php?mode=monthly");
                    nodes = document.DocumentNode.SelectNodes("//div[2]/a/div").ToArray();
                }
            }

            int num = rand.Next(0, nodes.Length);
            Result = nodes[num].InnerHtml.ToString();

            while (Result.Length < 102)
            {
                num = rand.Next(6, nodes.Length);
                Result = nodes[num].InnerHtml.ToString();
            }

            Result = Result.Replace("<img src=\"https://s.pximg.net/www/images/common/transparent.gif\" alt=\"\" class=\"_thumbnail ui-scroll-view\" data-filter=\"thumbnail-filter lazy-image\" data-src=\"", "");

            //get image url
            Result = Result.Remove(91);

            //get full resulution image
            Result = Result.Replace("c/240x480/", "");
        
            return Result;
        }
    }
}
