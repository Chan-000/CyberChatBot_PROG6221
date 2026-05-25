using System;
using System.Collections.Generic;


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

        // tracks whether the bot is waiting for name
        private bool awaitingName = true;

        //Stores last discussed topic
        private string lastTopic = "";

        //Constructor: Initialises all helper classes
        public ChatBot()
        {
            keywordResponder = new KeywordResponder();
            sentimentDetector = new SentimentDetector();
            memoryStore = new MemoryStore();
        }

        // Initial greeting shown at startup
        public string GetGreeting()
        {
            return "Hello and Welcome to CyberBot! I'm your cybersecurity awareness assistant, here to help you stay safe online.\n" +
                   "Before we begin, what is your name.";
        }

        // Main chatbot processing method
        public string ProcessInput (string input)
        {
            
            // prevent null errors
            if (string.IsNullOrWhiteSpace(input))
            {
                return "Please enter a message.";
            }

            input = input.ToLower().Trim();

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
