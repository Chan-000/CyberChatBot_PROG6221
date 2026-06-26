using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityChatBot
{
    /*
     * The Activity class records important chatbot actions
     * during program execution
     * The Log can display action or the full history
     */
    public class ActivityLogger
    {
        //Stores all activity Log entries for the current session
        private static List<string> _log = new List<string>();

        // Adds a new action to the activity log
        // A timestamp is automatically included
        public static void Log(string action)
        {
            string entry = $"[{DateTime.Now:HH:mm}] {action}";
            _log.Add(entry);
        }

        //Return the most recent log entries
        // By default only 10 actions are actions , if more entries exist
        // a show more message is displayed to the user
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

        //Returns every Log entry recorderd during the current application
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

        //Returns the total number of log entries
        // used to determine The "show more" functionality
        public static int GetCount()
        {
            return _log.Count;
        }

        
    }
}
