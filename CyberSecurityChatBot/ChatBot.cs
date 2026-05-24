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
            return "Hello! Welcome to the Cybersecurity Awareness Chatbot.\n Please tell me your name to begin.";
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

                return "Nice to meet you, " + memoryStore.userName + "! Ask me anything about cybersecurity.";
            }

            // follow-up handling
            if (input.Contains("tell me more") || input.Contains("explain more") || input.Contains("another tip"))
            {
                if (!string.IsNullOrEmpty(lastTopic))
                {
                    string extraResponse = keywordResponder.GetResponse(lastTopic);

                    return "Here is more information about " + lastTopic + ": " + extraResponse; 
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

                        return "Great! l will remember that you are interested in " + keyword + ".";
                    }
                }
            }

            // detect sentiment
            Sentiment detectedSentiment = sentimentDetector.Detect(input);
            string sentimentResponse = sentimentDetector.GetSentimentResponse(detectedSentiment);

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
                string memoryResponse = memoryStore.GetPersonalisedOpener();
                return sentimentResponse + memoryResponse + keywordResponse;
            }

            //General chatbot questions
            if (input.Contains("how are you"))
            {
                return "I'm just code and ready to help you stay safe online";
            }

            if (input.Contains("What can you do"))
            {
                return "l can help you learn about passwords, phishing, scams, malware, privacy, firewalls and online safety";
            }

            if (input.Contains("purpose"))
            {
                return "My purpose is to educate users about cybersecurity awareness";
            }

            // Default fallback response
            return "L am not sure l understand. Can you please try rephrasing your question?";
        }
    }
}
