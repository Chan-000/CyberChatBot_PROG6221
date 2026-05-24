using System;
using System.Collections.Generic;

namespace CyberSecurityChatBot
{

    /*
     * Handles cybersecurity keyword recognition
     * and random chatbot responses
     */
    public class KeywordResponder
    {
        //Dictionary storing keywords and responses
        private Dictionary<string, List<string>> responses;

        // Random object for random responses
        private Random random = new Random();

        // Constructor - Initialises all cybersecurity topics
        public KeywordResponder ()
        {
            responses = new Dictionary<string, List<string>>()
            {
                {
                    "password", new List<string>
                    {
                        "Use strong passwords with uppercase, lowercase, numbers and symbols.",
                        "Avoid using personal information in your passwords.",
                        "Use a different password for every important account."
                    }

                },

                {
                    "phishing", new List<string>
                    {
                        "Never click suspicious email links pretending to from banks.",
                        "Be careful of fake SMS messages asking for your login details.",
                        "Always verify suspicious messages before responding."
                    }

                },

                {
                    "privacy", new List<string>
                    {
                        "Review your social media privacy settings regularly.",
                        "Avoid sharing sensitive personal information online.",
                        "Use two-factor authentication to protect your accounts."
                    }

                },

                {
                    "malware", new List<string>
                    {
                        "Install trusted antivirus software to protect your device.",
                        "Avoid downloading files from unknown websites.",
                        "Keep your software updated to reduce malware risks."
                    }

                },

                {
                    "scam", new List<string>
                    {
                        "Scammers often pretend to be trusted organisations.",
                        "Never share OTPs and PINs with anyone.",
                        "If anyone sounds to good to be true, it probably is."
                    }

                },

                {
                    "ransomware", new List<string>
                    {
                        "Ransomware locks your files and demands payment to restore the, so never open email attachments from unknown senders..",
                        "Always back up important data to an external drive or cloud storage.",
                        "Keep your operating system updated to patch vulnerabilities ransomware exploits."
                    }

                },

                {
                    "firewall", new List<string>
                    {
                        "Enable your device firewall to block unathorised network traffic.",
                        "A firewall acts as your first line of defence against external attacks.",
                        "Regularly review your firewall rules to ensure they are up to date."
                    }

                }
            };
        }

       //Return a random response based on user input
       public string GetResponse(string input)
       {
            input = input.ToLower().Trim();

            foreach (var item in responses)
            {
                if (input.Contains(item.Key)) 
                {
                    List<string> possibleResponses = item.Value;

                    int index = random.Next(possibleResponses.Count);

                    return possibleResponses[index];
                }
                
            }

            return "";// if no keyword is matched
       }

       // Return all available keywords
       public List<string> GetAllKeywords()
        {
            return new List<string>(responses.Keys);
        }
    }
}
