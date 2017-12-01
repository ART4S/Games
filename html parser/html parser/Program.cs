using System;
using System.Net;
using System.Linq;
using System.Text.RegularExpressions;

namespace html_parser
{
    class Program
    {
        static void Main()
        {
            const string personId = "168207";
            const string language = "cs";

            const string taskIdPattern = "(?<=id_task=)\\d+";
            const string acceptedTasksPattern = "((?<=\\()\\d+(?=\\):</b>))";
            const string placePattern = "(?<=<td>)\\d+(?=</td>(([^>]*>){3})[^<]*<[^0-9]*" + personId + ")";

            WebClient webClient = new WebClient();
            string htmlPage = webClient.DownloadString("http://acmp.ru/index.asp?main=user&id=" + personId + "&lang=" + language);

            int acceptedTasksCount = int.Parse(Regex.Match(htmlPage, acceptedTasksPattern).Value);

            string[] acceptedTasks = Regex.Matches(htmlPage, taskIdPattern)
                .OfType<Match>()
                .Take(acceptedTasksCount)
                .Select(match =>
                {
                    string taskLeaderboard = webClient.DownloadString("http://acmp.ru/index.asp?main=bstatus&id_t=" + match.Value + "&lang=" + language);

                    int number = int.Parse(match.Value);
                    int place = match.Success
                        ? int.Parse(Regex.Match(taskLeaderboard, placePattern).Value)
                        : 99;

                    return (number, place);
                })
                .OrderByDescending(x => x.place)
                .ThenBy(x => x.number)
                .Select(x => x.number + " " + x.place)
                .ToArray();

            string results = string.Join("\n", acceptedTasks);

            Console.WriteLine(results);
        }
    }
}
