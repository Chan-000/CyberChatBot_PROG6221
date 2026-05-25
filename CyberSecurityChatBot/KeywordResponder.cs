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
        private string lastResponse = "";

        // Constructor - Initialises all cybersecurity topics
        public KeywordResponder ()
        {
            responses = new Dictionary<string, List<string>>()
            {
                {
                    "password", new List<string>
                    {
                        "Strong passwords should include uppercase letters, lowercase letters, numbers, and symbols to make them harder to guess.",
                        "Avoid using names, birthdays, or simple words in your passwords because hackers can guess them easily.",
                        "Using the same password for multiple accounts is risky because if one account is hacked, the others may also become vulnerable.",
                        "A password manager can help you create and safely store strong passwords for all your accounts."
                    }

                },

                {
                    "phishing", new List<string>
                    {
                        "Phishing attacks often pretend to come from trusted organisations like banks or delivery to trick people into sharing personal information.",
                        "Be cautious of emails or SMS messages asking you to click links or urgently confirm account details.",
                        "If a message looks suspicious, contact the company directly using their official website or phone number instead of replying to the message.",
                        "Scammers often create fake login pages that look real, so always double-check website addresses before entering passwords."
                    }

                },

                {
                    "privacy", new List<string>
                    {
                        "Protecting your privacy online is very important because personal information shared publicly can sometimes be used by cybercriminals for scams or identity theft.",
                        "Review your social media and account privacy settings regularly so you can control who is able to view your information and online activity.",
                        "Using two-factor authentication together with strong passwords can greatly improve your online privacy and make it harder for attackers to access your accounts.",
                        "Be careful about the personal details you share online, such as your phone number, location, or banking information, especially on public platforms."
                    }

                },

                {
                    "malware", new List<string>
                    {
                        "Ransomware is a type of malware that locks or encrypts your files and demands money before you can access them again.",
                        "Never open suspicious email attachments or download files from unknown websites because ransomware often spreads through fake links and attachments.",
                        "Backing up your important files regularly to cloud storage or an external drive can protect you from losing valuable information during a ransomware attack.",
                        "Keeping your operating system and antivirus software updated helps close security vulnerabilities that ransomware attackers try to exploit."
                    }

                },

                {
                    "scam", new List<string>
                    {
                        "Online scams often try to create panic or urgency so that people make quick decisions without thinking carefully.",
                        "Never share OTPs, passwords, or banking PINs with anyone, even if they claim to be from your bank.",
                        "Scammers sometimes pretend to be trusted companies or government organisations to trick victims into sending money or personal information.",
                        "If an offer sounds too good to be true, it is usually a scam."
                    }

                },

                {
                    "ransomware", new List<string>
                    {
                        "Ransomware is a type of malware that locks or encrypts your files and demands money before you can access them again.",
                        "Never open suspicious email attachments or download files from unknown websites because ransomware often spreads through fake links and attachments.",
                        "Backing up your important files regularly to cloud storage or an external drive can protect you from losing valuable information during a ransomware attack.",
                        "Keeping your operating system and antivirus software updated helps close security vulnerabilities that ransomware attackers try to exploit."
                    }

                },

                {
                    "firewall", new List<string>
                    {
                        "A firewall acts like a protective barrier between your computer and the internet by blocking suspicious or unauthorised traffic.",
                        "Always keep your firewall enabled because it helps prevent hackers and malicious programs from accessing your device without permission.",
                        "Firewalls monitor incoming and outgoing network activity and can stop dangerous connections before they reach your system.",
                        "It is a good idea to check your firewall settings occasionally to ensure your device is properly protected against online threats."
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
                // keyword matching
                if (input.Contains(item.Key)) 
                {
                    List<string> possibleResponses = item.Value;

                    string selectedResponse = "";

                    //prevent imediate repetition
                    do
                    {
                        int index = random.Next(possibleResponses.Count);
                        selectedResponse = possibleResponses[index];

                    } while (selectedResponse == lastResponse && possibleResponses.Count > 1);

                    lastResponse = selectedResponse;

                    return selectedResponse;
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
