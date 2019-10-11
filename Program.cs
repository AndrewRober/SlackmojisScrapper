using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace SlackmojisScrapper
{
    class Program
    {
        static void Main(string[] args) =>
            new Regex("src=[\\'\"]?([^\\'\" >]+)", RegexOptions.Compiled).Matches(new WebClient().DownloadString(@"https://slackmojis.com/"))
                .Where(link => link.Value.Contains("images")).Select(link => link.Value.Replace("src=\"", string.Empty)).ToList()
                .ForEach(link =>
                {
                    using (WebClient subclient = new WebClient())
                    {
                        var fileName = link.Substring(link.LastIndexOf("/") + 1,
                            link.LastIndexOf("?") - link.LastIndexOf("/") - 1);
                        subclient.DownloadFile(new Uri(link), $@"imgs\{fileName}");
                    }
                });
    }
}
