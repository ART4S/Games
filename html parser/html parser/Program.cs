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
            const string personID = "168207";
            const string language = "cs";

            WebClient webClient = new WebClient();
            string htmlPage = webClient.DownloadString("http://acmp.ru/index.asp?main=user&id=" + personID + "&lang=" + language);
            MatchCollection tasksID = Regex.Matches(htmlPage, "(?<=id_task=)\\d+");
            int acceptedTasksCount = int.Parse(Regex.Match(htmlPage, "((?<=\\()\\d+(?=\\):</b>))").Value);

            string searchPlacePattern = "(?<=<td>)\\d+(?=</td>(([^>]*>){3})[^<]*<[^0-9]*" + personID + ")";

            Dictionary<int, int> acceptedTasks = new Dictionary<int, int>();

            for (int i = 0; i < acceptedTasksCount; i++)
            {
                string taskLeaderboard = webClient.DownloadString("http://acmp.ru/index.asp?main=bstatus&id_t=" + tasksID[i].Value + "&lang=" + language);

                acceptedTasks.Add(int.Parse(tasksID[i].Value),
                    Regex.IsMatch(taskLeaderboard, searchPlacePattern)
                        ? int.Parse(Regex.Match(taskLeaderboard, searchPlacePattern).Value)
                        : 99);
            }

            List<KeyValuePair<int, int>> sortedAcceptedTasks = acceptedTasks
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .ToList();

            foreach (KeyValuePair<int, int> task in sortedAcceptedTasks)
                Console.WriteLine(task.Key + " " + task.Value);
        }
    }
}
