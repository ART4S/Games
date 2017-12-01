using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace html_parser
{
    class Program
    {
        static void Main()
        {
            const string personId = "168207";
            const string language = "cs";

            WebClient webClient = new WebClient();
            string htmlPage = webClient.DownloadString("http://acmp.ru/index.asp?main=user&id=" + personId + "&lang=" + language);
            int acceptedTasksCount = int.Parse(Regex.Match(htmlPage, "((?<=\\()\\d+(?=\\):</b>))").Value);

            List<Match> tasksId = Regex.Matches(htmlPage, "(?<=id_task=)\\d+")
                .OfType<Match>()
                .Take(acceptedTasksCount)
                .ToList();

            string searchPlacePattern = "(?<=<td>)\\d+(?=</td>(([^>]*>){3})[^<]*<[^0-9]*" + personId + ")";

            var acceptedTasks = new Dictionary<int, int>();

            foreach (var task in tasksId)
            {
                string taskLeaderboard = webClient.DownloadString("http://acmp.ru/index.asp?main=bstatus&id_t=" + task.Value + "&lang=" + language);

                acceptedTasks.Add(int.Parse(task.Value),
                    task.Success
                    ? int.Parse(Regex.Match(taskLeaderboard, searchPlacePattern).Value)
                    : 99);
            }

            acceptedTasks
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .ToList()
                .ForEach(x => Console.WriteLine(x.Key + " " + x.Value));
        }
    }
}
