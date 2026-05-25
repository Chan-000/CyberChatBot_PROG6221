using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityChatBot
{
    /*
     * Memory class
     * Responsible for storing and recalling user information
     */
   public class MemoryStore
   {
        //Stores the user's name
        public string userName { get; set; } = "";

        //Stores favourite cybersecurity topic
        public string FavouriteTopic { get; set; } = "";
        
        // Dictionary used for flexible memory storage
        private Dictionary<string, string> memory = new Dictionary<string, string>();

        // Store information using a key and a value
        // Example key = "topic" value = "phishing"
        public void Store (string key, string value)
        {
            memory[key] = value;
        }

        // Recalls stored information and return emptu string if nothing exists
        public string Recall(string key)
        {
            if (memory.ContainsKey(key))
            {
                return memory[key];
            }

            return "";
        }

        // creates a personalised sentence
        public string GetPersonalisedOpener(string currentInput)
        {
            currentInput = currentInput.ToLower();

           // Only use memory sometimes instead of every response
           if (!string.IsNullOrEmpty(FavouriteTopic))
            {
                // only trigger when topic is related
                if (currentInput.Contains(FavouriteTopic))
                {
                    return "Since you are interested in " + FavouriteTopic + ", " + userName + ", here is some advice: ";
                }

            }
            return "";
        }
   }
}
