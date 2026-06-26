using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Markup;


namespace CyberSecurityChatBot
{

    /*
    *The ChatBot class controls the main logic of the application
    *Connects all chatbot feaures together
    */
    public class ChatBot
    {
        //Helper classes
        private KeywordResponder keywordResponder;
        private SentimentDetector sentimentDetector;
        private MemoryStore memoryStore;
        private TaskManager taskManager;
        private ActivityLogger activityLogger;

        // tracks whether the bot is waiting for name
        private bool awaitingName = true;

        //Stores last discussed topic
        private string lastTopic = "";

        //
        private string lastTrackCreated = "";

        //Constructor: Initialises all helper classes
        public ChatBot()
        {
            keywordResponder = new KeywordResponder();
            sentimentDetector = new SentimentDetector();
            memoryStore = new MemoryStore();
            taskManager = new TaskManager();
            activityLogger = new ActivityLogger();
        }

        // Initial greeting shown at startup
        public string GetGreeting()
        {
            return "Hello and Welcome to CyberBot! I'm your cybersecurity awareness assistant, here to help you stay safe online.\n" +
                   "Before we begin, what is your name.";
        }

        // Main chatbot processing method
        public string ProcessInput(string input)
        {

            // prevent null errors
            if (string.IsNullOrWhiteSpace(input))
            {
                return "Please enter a message.";
            }

            input = input.ToLower().Trim();

            //========== NLP INTENT DETECTION ============

            //Reminder intent
            if (input.Contains("remind me") ||
                input.Contains("reminder") ||
                input.Contains("set a reminder") ||
                input.Contains("remind me to") ||
                input.Contains("don't forget"))
            {
                string reminderText = input
                    .Replace("remind me", "")
                    .Replace("reminder", "")
                    .Replace("set a reminder", "")
                    .Replace("remind me to", "")
                    .Replace("don't forget", "")
                    .Trim();

                ActivityLogger.Log($"Reminder set for '{reminderText}'");
                return $"Got it! I'll remind you '{reminderText}'.";
            }

            // Add task intent
            if (input.Contains("add task") ||
            input.Contains("add a task") ||
            input.Contains("create task") ||
            input.Contains("l need to") ||
            input.Contains("enable"))

            {
                string title = input;
                title = title.Replace("add task", "");
                title = title.Replace("add a task", "");
                title = title.Replace("create task", "");
                title = title.Replace("l need to", "");
                title = title.Replace("enable", "Enable");

                title = title.Trim();

                if (string.IsNullOrWhiteSpace(title))
                {
                    return "Please provide a task title.";
                }

                taskManager.AddTask(title, "Added through chatbot", "No reminder");
                lastTrackCreated = title;

                return $"Task added: '{title}'. Would you like to set a reminder for this task";
            }

            // quiz intent
            if (input.Contains("start quiz") ||
                input.Contains("take quiz") ||
                input.Contains("quiz me") ||
                input.Contains("test my knowledge") ||
                input.Contains("play the game"))
            {
                ActivityLogger.Log("Quiz activated");
                return "QUIZ_REQUEST";
            }

            //view tasks
            if (input.Contains("show tasks") ||
                input.Contains("my tasks") ||
                input.Contains("view tasks"))
            {
                List<CyberTask> tasks = taskManager.GetAllTasks();

                if (tasks.Count == 0)
                {
                    return "You currently have no tasks.";
                }
                string result = "Your Tasks:\n\n";

                foreach (CyberTask task in tasks)
                {
                    string status = task.IsComplete ? "Completed" : "Pending";
                    result += $"ID: {task.Id}\n";
                    result += $"Title: {task.Title}\n";
                    result += $"Status: {status}\n\n";
                }
                return result;
            }

            //Activity log commands
            if (input.Contains("show activity log") ||
                input.Contains("what have you done") ||
                input.Contains("what did you do") ||
                input.Contains("show log") ||
                input.Contains("recent actions") ||
                input.Contains("recent activity"))
            {
                return ActivityLogger.GetRecentLog();
            }

            
            //Capture user name 
            if (awaitingName)
            {
                memoryStore.userName = input;
                awaitingName = false;

                return "Nice to meet you, " + memoryStore.userName +
                    "!\n\nYou can ask me about:\n\n"+
                    "• Password safety\n" +
                    "• Phishing\n" +
                    "• Privacy\n" +
                    "• Malware\n" +
                    "• Online scams\n" +
                    "• Firewall\n" +
                    "• Ransomware\n\n"+
                    "How can l help you today?";
            }

            // follow-up handling
            if (input.Contains("tell me more") || input.Contains("explain more") || input.Contains("another tip"))
            {
                if (!string.IsNullOrEmpty(lastTopic))
                {
                    string extraResponse = keywordResponder.GetResponse(lastTopic);

                    return "Of course, " + memoryStore.userName + ". Here is another tip or more information about " + lastTopic + ": " + extraResponse; 
                }

                return "Please choose a cybersecurity topic first.";
            }

            // Detect favourite topic
            if (input.Contains("interested in"))
            {
                foreach (string keyword in keywordResponder.GetAllKeywords())
                {
                    if (input.Contains(keyword))
                    {
                        memoryStore.FavouriteTopic = keyword;

                        return "Great! l will remember that you are interested in " + keyword + ".\n\n" + keywordResponder.GetResponse(keyword);
                    }
                }
            }

            // detect sentiment
            Sentiment detectedSentiment = sentimentDetector.Detect(input);
            string sentimentResponse = sentimentDetector.GetSentimentResponse(detectedSentiment, memoryStore.userName);

            // detect cybersecurity keywords
            string keywordResponse = keywordResponder.GetResponse(input);

            if (!string.IsNullOrEmpty(keywordResponse))
            {
                // save last topic
                foreach (string keyword in keywordResponder.GetAllKeywords())
                {
                    if (input.Contains(keyword))
                    {
                        lastTopic = keyword;
                        break;
                    }
                }

                // personalised opener
                string memoryResponse = memoryStore.GetPersonalisedOpener(input);
                return sentimentResponse + memoryResponse + keywordResponse;
            }

            //General chatbot questions
            if (input.Contains("how are you"))
            {
                return "I'm just code and ready to help you stay safe online";
            }

            if (input.Contains("what can you do"))
            {
                return "I can help you learn about cybersecurity topics like passwords, phishing scams, malware, online privacy, ransomware, firewalls and online safety";
            }

            if (input.Contains("purpose"))
            {
                return "My purpose is to educate users about cybersecurity awareness";
            }

            // Default fallback response
            return "I am not sure l understand. Can you please try rephrasing your question?";
        }

    }
}
