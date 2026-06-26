# CyberSecurity Awareness Chatbot - CyberBot (part2)

## Project Description
A WPF-based Cybersecurity Awareness Chatbot that helps users lean about online safety, scams, phishing, malware , privacy and other cybersecurity topics through interactive conversation.

## Student Information
- Name: Chantel Tafadzwa Chiraswa
- Student Number: ST10497253

## Part 1
- Personalised user interaction by using the user name
- Voice greeting using a WAV file
- ASCII art logo display
- Structured and colourful console UI
- Typing effect for a realistic conversation
- Menu system + text input
- Cybersecurity awareness topics

## Part 2
- WPF graphical user interface
- Keyword recognition (password, phishing, malware, and other cybersecurity topics)
- Randomised chatbot responses
- Follow-up responses such as "tell me more" "another tip"
- Memory and Recall (remembers user's  name and favourite topic)
- Sentiment Detection (worried, happy, curious , frustrated)
- Empathetic repsones based on user mood
- Clean error handling and fallback responses

## Part 3
- WPF graphical user interface
- Task Assistant with JSON storage
- Cybersecurity Quiz
- NLP intent recognition
- Activity Logger with timestamps
- Full integration of all chatbot features

## How to Clone and Run the Project
1. Clone or download the repository
2. Open the solution file 'CyberSecurityChatbot.slnx' in Visual Studio
3. In visio studio Click Build and select Build Solution
4. Click Start or continue to launch the chatbot

## Prerequisites
- Visual Studio 2026
- GitHub account
- .Net 8.0 SDK
- Windows Presentation Foundation (WPF)

## Installing Newtonsoft.Json
1. Open the project in Visual Studio 2022.
2. Right-click the project in Solution Explorer.
3. Select: Manage NuGet Packages
4. Search for: Newtonsoft.Json
5. Click Install
6. Build solution

## Task storage
No setup is required for task storage.
The file:   tasks.json is automatically created when the first task is added through the application.

## Audio File Setup
greeting.wav must be placed inside the main project folder 'CyberSecurityChatBot/'
The file properties should be Build Action = Content and Copy to Output Directory

## ChatBot GUI
<img width="889" height="647" alt="Chatbot GUI" src="https://github.com/user-attachments/assets/5946112d-d205-4806-8ce3-62b5981fa5fe" />

## GitHub Actions CI
<img width="650" height="198" alt="CI Actions" src="https://github.com/user-attachments/assets/5220ed7c-9864-4bd7-873c-053ccac50e2f" />

## YouTube video link Part 3
https://youtu.be/huc7DtpOgII

### Releases

## v3.0 - Task Assistant and JSON storage
- Task creation
- Task deletion
- Task completion
- JSON persistence
- Activity logging

## v3.1 - Quiz and Activity Log
- Cybersecurity quiz
- Question feedback
- Score tracking
- Activity logger
- Show More functionality

## v3.2 - Final Integrated Version
- NLP intent recognition
- Reminder handling
- Full GUI integration
- Personalized chatbot interaction
- Complete POE functionality

## task json is here
CyberSecurityChatBot
 └── bin
      └── Debug
           └── net8.0-windows
                └── tasks.json
