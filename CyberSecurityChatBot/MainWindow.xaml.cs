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

            // display bot response
            AppendBotMessage(botResponse);

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

        //
        private void LoadTasks()
        {
            TaskListBox.ItemsSource = null;
            TaskListBox.ItemsSource = _taskManager.GetAllTasks();

        }
        

        //
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

        //
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

        //
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

    }
}