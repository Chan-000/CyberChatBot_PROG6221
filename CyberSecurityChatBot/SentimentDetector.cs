using System;
using System.Collections.Generic;

namespace CyberSecurityChatBot
{

    // Enum is used to represent different sentiments
    public enum Sentiment
    {
        Neutral,
        Worried,
        Curious,
        Frustrated, 
        Happy
    }

    // Detects emotional tone in user message 
   public class SentimentDetector
    {
        private Dictionary<Sentiment, List<string>> sentimentKeywords;


        // Constructor 
        //Initialises sentiment keywords
        public SentimentDetector()
        {
            sentimentKeywords = new Dictionary<Sentiment, List<string>>()
            {
                {
                    Sentiment.Worried, new List<string>
                    {
                        "worried",
                        "scared",
                        "nervous",
                        "unsafe",
                        "afraid",
                        "anxious"
                    }
                },

                {
                    Sentiment.Curious, new List<string>
                    {
                        "curious",
                        "interested",
                        "wondering",
                        "how does",
                        "want to know"
                    }

                },

                {
                    Sentiment.Frustrated, new List<string>
                    {
                        "frustrated",
                        "confused",
                        "annoyed",
                        "angry",
                        "don't understand"
                    }

                },

                {
                    Sentiment.Happy, new List<string>
                    {
                        "happy",
                        "great",
                        "thanks",
                        "awesome",
                        "helpful",
                        "love it"
                    }

                }

            };
        }

        // Detect sentiment from user input
        public Sentiment Detect (string input)
        {
            input = input.ToLower();

            foreach (var sentiment in sentimentKeywords)
            {
                foreach (string keyword in sentiment.Value)
                {
                    if (input.Contains(keyword))
                    {
                        return sentiment.Key;
                    }
                }
            }

            return Sentiment.Neutral;
        }


        // Return empathetic chatbot response
        public string GetSentimentResponse(Sentiment sentiment, string userName)
        {
            switch (sentiment)
            {
                case Sentiment.Worried:
                    return "It's understandable to feel worried, " + userName + ". Cybersecurity threats can be scary, but there are ways to stay safe.\n\n";

                case Sentiment.Curious:
                    return "I am glad you are curious about cybersecurity, " + userName + ". Learning more is one of the best ways to protect yourself online.\n\n";

                case Sentiment.Frustrated:
                    return "Cybersecurity can feel overwhelming but don't worry " + userName + ". I'm here to help make it easier to understand.\n\n";

                case Sentiment.Happy:
                    return "That's great to hear, " + userName + "! Staying positive while learning about online safety is important.\n\n";

                default: 
                    return "";
            }
        }
    }
}
