using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityChatBot
{
    /*
     * Handles all quiz functionality
     * It stores quiz questions and track user progress
     * calculates scores, provides feedback 
     * manages quiz completion
     */
    public class QuizManager
    {
        //Stores and tracks
        private List<QuizQuestion> _questions;
        private int _currentIndex;
        private int _score;

        //Creates and loads all cybersecurity questions
        public QuizManager()
        {
            _questions = new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    Question = "What should you do if you receive an email asking for your password?",
                    Options = new List<string>
                    {
                        "A) Reply with your password",
                        "B) Delete the email",
                        "C) Report the email as phishing",
                        "D) Ignore it"
                    },
                    CorrectAnswer = "C",
                    Explanation = "Reporting phishing emails helps prevent scams."
                },

                new QuizQuestion
                {
                    Question = "True or False: You should use the same password for all accounts",
                    Options = new List<string>
                    {
                        "True",
                        "False"
                    },
                    CorrectAnswer = "B",
                    Explanation = "Each account should have a unique password, to avoid compromising all accounts",
                    IsTrueFalse = true
                },

                new QuizQuestion
                {
                    Question = "Why is two-factor authentication (2FA) more secure than just a password?",
                    Options = new List<string>
                    {
                        "A) It makes your password longer",
                        "B) It requires something you know + something you have",
                        "C) It hides your password from the website",
                        "D) It only works on mobile phones"
                    },
                    CorrectAnswer = "B",
                    Explanation = "2FA adds an extra layer of security.Even if someone steals your password, they still need your phone or security key to get in"
                },

                new QuizQuestion
                {
                    Question = "True or False: Public Wi-fi is always safe",
                    Options = new List<string>
                    {
                        "True",
                        "False"
                    },
                    CorrectAnswer = "B",
                    Explanation = "Public Wi-Fi often unsecured, so hackers can exploit this weakness to steal personal information",
                    IsTrueFalse = true
                },

                new QuizQuestion
                {
                    Question = "Which of these is a common sign of a phishing attempt?",
                    Options = new List<string>
                    {
                        "A) Perfect spelling and grammar",
                        "B) Email comes from your exact contacts list",
                        "C) Generic greeting like 'Dear Customer' + urgent request",
                        "D) The company logo looks correct"
                    },
                    CorrectAnswer = "C",
                    Explanation = "Phishing of creates urgency, Generic greetings and urgency are red flags."
                },

                new QuizQuestion
                {
                    Question = "True or False:  Antivirus software helps protect against malware.",
                    Options = new List<string>
                    {
                        "True",
                        "False"
                    },
                    CorrectAnswer = "A",
                    Explanation = "Antivirus software helps detect threats.",
                    IsTrueFalse = true
                },

                new QuizQuestion
                {
                    Question = "What should you check before entering personal information online?",
                    Options = new List<string>
                    {
                        "A) Website colour",
                        "B) Advertisement count",
                        "C) Font style",
                        "D) HTTPS connection"
                    },
                    CorrectAnswer = "D",
                    Explanation = "HTTPS indicates a secure connection."
                },

                new QuizQuestion
                {
                    Question = "True or False: Backing up data can help recover from ransomware.",
                    Options = new List<string>
                    {
                        "True",
                        "False"
                    },
                    CorrectAnswer = "A",
                    Explanation = "Backups help restore lost files.",
                    IsTrueFalse = true
                },

                new QuizQuestion
                {
                    Question = "What is social engineering",
                    Options = new List<string>
                    {
                        "A) Building websites",
                        "B) Manipulating people into revealing information",
                        "C) Software testing",
                        "D) Network maintenance"
                    },
                    CorrectAnswer = "B",
                    Explanation = "Social engineering targets human behaviour."
                },

                new QuizQuestion
                {
                    Question = "True or False: Privacy settings should not be reviewed regularly.",
                    Options = new List<string>
                    {
                        "True",
                        "False"
                    },
                    CorrectAnswer = "B",
                    Explanation = "Regular reviews improve security.",
                    IsTrueFalse = true
                },

                new QuizQuestion
                {
                    Question = "What password is the strongest",
                    Options = new List<string>
                    {
                        "A) abc123",
                        "B) P@55w0rd!",
                        "C) password123",
                        "D) 7X#kL9!qT2@z"
                    },
                    CorrectAnswer = "D",
                    Explanation = "Long complex passwords are stronger."
                }
            };

            _currentIndex = 0;
            _score = 0;
        }

        //return the current quiz question to the user
        public QuizQuestion GetCurrentQuestion()
        {
            return _questions[_currentIndex];
        }

        //checks whether the user's and is correct 
        // and increases score if its correct
        public bool SubmitAnswer(string answer)
        {
            bool correct = answer.Trim().Equals(_questions[_currentIndex].CorrectAnswer, System.StringComparison.OrdinalIgnoreCase);
            if (correct)
            {
                _score++;
            }
            _currentIndex++;
            return correct;
        }

        //return the explanation for the previous answered question
        public string GetFeedback()
        {
            return _questions[_currentIndex - 1].Explanation;
        }

        //return the correct answer for the previous question
        public string GetCorrectAnswer()
        {
            QuizQuestion question = _questions[_currentIndex - 1];

            if (question.IsTrueFalse)
            {
                if (question.CorrectAnswer == "A") return "True";
                if (question.CorrectAnswer == "B") return "False";
            }
            return question.CorrectAnswer;
        }

        //return the current question number
        public int GetCurrentQuestionNumber()
        {
            return _currentIndex + 1;
        }

        //Checks whether all quiz questions have been answered
        public bool IsFinished()
        {
            return _currentIndex >= _questions.Count;
        }

        //return the user's current score
        public int GetScore()
        {
            return _score;
        }

        //return total number of questions
        public  int GetTotalQuestions()
        {
            return _questions.Count;
        }

        //give a personalised feedback based on the user's score
        public string GetFinalMessage()
        {
            double percentage = (double)_score / _questions.Count * 100;

            if (percentage >= 80)
                return "Great job! You're a cybersecurity pro! Keep it up.";
            else if (percentage >= 50)
                return "Good effort! Review the topics you missed and keep learning.";
            else
                return "Keep learning! Cybersecurity is important for everyone.";
        }

        // resets the quiz so that the user can start from the beginning
        public void ResetQuiz()
        {
            _currentIndex = 0;
            _score = 0;
        }
    }
}
