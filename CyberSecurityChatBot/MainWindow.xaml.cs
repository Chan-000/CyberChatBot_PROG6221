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
    }
}