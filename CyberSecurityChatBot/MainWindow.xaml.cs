using System.Windows;
using System.Windows.Input;

namespace CyberSecurityChatBot
{
    /*
     * Interaction logic for MainWindow.xaml
     */
    public partial class MainWindow : Window
    {
        // Main chatbot object
        private ChatBot chatBot;

        //taskmanager object
        private TaskManager _taskManager;

        //QuizManager object
        private QuizManager _quizManager;

        // Constructor
        public MainWindow()
        {
            InitializeComponent();

            // creates chatbot object
            chatBot = new ChatBot();

            // Play voice greeting
            AudioPlayer.PlayVoiceGreeting();

            // Load ASCII art
            LoadAsciiArt();

            // Display initial greeting
            AppendBotMessage(chatBot.GetGreeting());

            // creates TaskManager object
            _taskManager = new TaskManager();

            LoadTasks();

            // 
            _quizManager = new QuizManager();

        }

        // loads ASCII art into the GUI
        private void LoadAsciiArt()
        {
            AsciiArtText.Text = @"
                    _________         ___.                  __________          __    
\_   ___ \ ___.__.\_ |__    ____ _______\______   \  ____ _/  |_  
/    \  \/<   |  | | __ \ _/ __ \\_  __ \|    |  _/ /  _ \\   __\ 
\     \____\___  | | \_\ \\  ___/ |  | \/|    |   \(  <_> )|  |   
 \______  // ____| |___  / \___  >|__|   |______  / \____/ |__|   
        \/ \/          \/      \/               \/                                                         
                                                                                                                                                                                                                                                                                                                                                                                                       
           ";
        }

        // send button click event
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        // Enter key event
        private void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }

        // handles sending messages
        private void SendMessage()
        {
            // Get user input
            string userMessage = UserInputTextBox.Text.Trim();

            // prevent empty messages
            if (string.IsNullOrWhiteSpace(userMessage))
            {
                return;
            }

            // display user message
            AppendUserMessage(userMessage);

            // process chatbot response
            string botResponse = chatBot.ProcessInput(userMessage);

            // display bot response and NLP integration  
            if (botResponse == "QUIZ_REQUEST")
            {
                ShowQuizPanel();

                QuestionTextBlock.Text = "Press Start Quiz to begin";
                FeedbackTextBlock.Text = "";
                ScoreTextBlock.Text = "Score: 0";

                StartQuizButton.Visibility = Visibility.Visible;

                OptionA.Visibility = Visibility.Collapsed;
                OptionB.Visibility = Visibility.Collapsed;
                OptionC.Visibility = Visibility.Collapsed;
                OptionD.Visibility = Visibility.Collapsed;

                SubmitAnswerButton.Visibility = Visibility.Collapsed;
                NextQuestionButton.Visibility = Visibility.Collapsed;

                AppendBotMessage("Opening Cybersecurity Quiz... You can click START QUIZ on the left panel");
            }
            else
            {
                AppendBotMessage(botResponse);
            }
           
            //refresh task list
            LoadTasks();

            //  clear input box
            UserInputTextBox.Clear();

            // keep focus on textbox
            UserInputTextBox.Focus();

            // scroll automatically
            ChatScrollViewer.ScrollToEnd();
        }

        // Display user message
        private void AppendUserMessage(string message)
        {
            ChatDisplay.Text += "\nYou: " + message + "\n";
        }

        //Displays chatbot messages
        private void AppendBotMessage(string message)
        {
            ChatDisplay.Text += "\nCyberBot: " + message + "\n";
        }

        //Reloads task from task.json and displays them
        private void LoadTasks()
        {
            TaskListBox.ItemsSource = null;
            TaskListBox.ItemsSource = _taskManager.GetAllTasks();

        }
        
        // Hides task Asistant panel and displays quiz panel
        private void ShowQuizPanel()
        {
            TaskPanel.Visibility = Visibility.Collapsed;
            QuizPanel.Visibility = Visibility.Visible;
        }

        // hides quiz panel and brings back the task assistant panel
        private void HideQuizPanel()
        {
            QuizPanel.Visibility = Visibility.Collapsed;
            TaskPanel.Visibility = Visibility.Visible;
        }

        //Opens the Cybersecurity Quiz panel and prepares the quiz panel
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleTextBox.Text.Trim();
            string description = TaskDescriptionTextBox.Text.Trim();
            string reminder = TaskReminderTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Please enter a task title.");
                return;
            }

            string message = _taskManager.AddTask(title, description, reminder);
            MessageBox.Show(message);
            
            LoadTasks();

            TaskTitleTextBox.Clear();
            TaskDescriptionTextBox.Clear();
            TaskReminderTextBox.Clear();

        }

        //Completes a task from Task panel
        private void CompleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            CyberTask? selectedTask = TaskListBox.SelectedItem as CyberTask;

            if (selectedTask == null)
            {
                MessageBox.Show("Please select a task!");
                return;
            }

            _taskManager.MarkAsComplete(selectedTask.Id);
            MessageBox.Show("Task marked as complete!");
            LoadTasks();
        }

        //deletes a task from Task panel
        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            CyberTask? selectedTask = TaskListBox.SelectedItem as CyberTask;

            if (selectedTask == null)
            {
                MessageBox.Show("Please select a task!");
                return;
            }

            _taskManager.DeleteTask(selectedTask.Id);

            MessageBox.Show("Task deleted!");

            LoadTasks();
        }

        // Open the Cybersecurity Quiz panel and prepares the quiz start screen
        private void OpenQuizButton_Click(object sender, RoutedEventArgs e)
        {
            ShowQuizPanel();
            _quizManager.ResetQuiz();

            StartQuizButton.Visibility = Visibility.Visible;

            QuestionTextBlock.Text = "Press Start Quiz to begin";
            FeedbackTextBlock.Text = "";
            ScoreTextBlock.Text = "Score: 0";

            OptionA.Visibility = Visibility.Collapsed;
            OptionB.Visibility = Visibility.Collapsed;
            OptionC.Visibility = Visibility.Collapsed;
            OptionD.Visibility = Visibility.Collapsed;

            SubmitAnswerButton.Visibility = Visibility.Collapsed;
            NextQuestionButton.Visibility = Visibility.Collapsed;
        }

        // Starts a new quiz session and logs the action
        private void StartQuizButton_Click(object sender, RoutedEventArgs e)
        {
            ActivityLogger.Log("Quiz started");
            _quizManager.ResetQuiz();
            SubmitAnswerButton.IsEnabled = true;
            SubmitAnswerButton.Visibility = Visibility.Visible;
            LoadQuestion();
            StartQuizButton.Visibility = Visibility.Collapsed;
            FeedbackTextBlock.Text = "";
        }

        //Validates the selecetd answer and displays feedback + explanaytion
        private void SubmitAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            string answer = "";

            if (OptionA.IsChecked == true) answer = "A";
            else if (OptionB.IsChecked == true) answer = "B";
            else if (OptionC.IsChecked == true) answer = "C";
            else if (OptionD.IsChecked == true) answer = "D";

            if (string.IsNullOrEmpty(answer))
            {
                MessageBox.Show("Please select an answer");
                return;
            }

            bool correct = _quizManager.SubmitAnswer(answer);
            string feedback = _quizManager.GetFeedback();

            if (correct)
            {
                FeedbackTextBlock.Text = "Correct!\n\n" + "Explanation: " + feedback;
            }
            else
            {
                FeedbackTextBlock.Text =
                    "Incorrect.\n\n" + 
                    "Correct Answer: " +
                    _quizManager.GetCorrectAnswer() +
                    "\n\nExplanation: " +
                    feedback;
            }

            NextQuestionButton.Visibility = Visibility.Visible;
            SubmitAnswerButton.IsEnabled = false; 
        }

        //Loads the next question or completes the quiz
        private void NextQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (_quizManager.IsFinished())
            {
                ActivityLogger.Log($"Quiz completed - score: {_quizManager.GetScore()} out of {_quizManager.GetTotalQuestions()}");
                MessageBox.Show(
                    $"Quiz Complete!\n\n" +
                    $"Score: {_quizManager.GetScore()}/" +
                    $"{_quizManager.GetTotalQuestions()}\n\n" +
                    _quizManager.GetFinalMessage());

                HideQuizPanel();
                StartQuizButton.Visibility = Visibility.Collapsed;//visible here
                return;
            }
            SubmitAnswerButton.IsEnabled = true;
            LoadQuestion();
        }


        //Loads the current quiz question and displays available answer question
        private void LoadQuestion()
        {
            QuizQuestion question = _quizManager.GetCurrentQuestion();

            QuestionTextBlock.Text = question.Question;

            ScoreTextBlock.Text = $"Score: {_quizManager.GetScore()}/{_quizManager.GetTotalQuestions()}";

            OptionA.Content = "";
            OptionB.Content = "";
            OptionC.Content = "";
            OptionD.Content = "";

            OptionA.Visibility = Visibility.Collapsed;
            OptionB.Visibility = Visibility.Collapsed;
            OptionC.Visibility = Visibility.Collapsed;
            OptionD.Visibility = Visibility.Collapsed;

            if (question.Options.Count > 0)
            {
                OptionA.Content = question.Options[0];
                OptionA.Visibility = Visibility.Visible;
            }


            if (question.Options.Count > 1)
            {
                OptionB.Content = question.Options[1];
                OptionB.Visibility = Visibility.Visible;
            }


            if (question.Options.Count > 2)
            {
                OptionC.Content = question.Options[2];
                OptionC.Visibility = Visibility.Visible;
            }


            if (question.Options.Count > 3)
            {
                OptionD.Content = question.Options[3];
                OptionD.Visibility = Visibility.Visible;
            }

            OptionA.IsChecked = false;
            OptionB.IsChecked = false;
            OptionC.IsChecked = false;
            OptionD.IsChecked = false;

            FeedbackTextBlock.Text = "";
            NextQuestionButton.Visibility = Visibility.Collapsed;

            if (_quizManager.GetCurrentQuestionNumber() == _quizManager.GetTotalQuestions())
            {
                NextQuestionButton.Content = "Finish Quiz";
            }
            else
            {
                NextQuestionButton.Content = "Next Question";
            }
        }
    }
}