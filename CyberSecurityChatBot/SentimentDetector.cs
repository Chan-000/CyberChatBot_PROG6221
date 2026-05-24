using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        "great",
                        "thanks",
                        "awesome",
                        "helpful",
                        "love it"
                    }

                },

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
        public string GetSentimentResponse(Sentiment sentiment)
        {
            switch (sentiment)
            {
                case Sentiment.Worried:
                    return "It's understandable to feel worried about cybersecurity threats.";

                case Sentiment.Curious:
                    return "I am glad you want to learn more about staying safe online.";

                case Sentiment.Frustrated:
                    return "Cybersecurity can feel confusing sometimes, but l will help you understand it better.";

                case Sentiment.Happy:
                    return "I am happy that you are enjoying learning about cybersecurity.";

                default: 
                    return "";
            }
        }
    }
}
