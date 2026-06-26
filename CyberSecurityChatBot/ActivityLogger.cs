using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityChatBot
{
    public class ActivityLogger
    {
        private static List<string> _log = new List<string>();

        // log a new action
        public static void Log(string action)
        {
            string entry = $"[{DateTime.Now:HH:mm}] {action}";
            _log.Add(entry);
        }

        //get recent log entries
        public static string GetRecentLog(int count = 10)
        {
            if (_log.Count == 0)
            {
                return "No activity recorded yet.";
            }

            var recentEntries = _log.TakeLast(count).ToList();

            string result ="Here's a summary of recent actions:\n\n";

            for (int i = 0; i < recentEntries.Count; i++)
            {
                result += $"{i + 1}. {recentEntries[i]}\n";
            }

            if (_log.Count > count)
            {
                result += "\nType 'show more' to see the full activity history.";
            }

            return result;
        }

        //Get all entries (for "show more")
        public static string GetFullLog()
        {
            if (_log.Count == 0)
            {
                return "No activity recorded yet.";
            }

            string result = "Full Activity Log:\n\n";

            for (int i = 0; i < _log.Count; i++)
            {
                result += $"{i + 1}. {_log[i]}\n";
            }
            return result;
        }

        //Get total number of log entries
        public static int GetCount()
        {
            return _log.Count;
        }

        
    }
}
