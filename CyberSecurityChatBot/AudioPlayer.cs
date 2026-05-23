using System;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;


namespace CyberSecurityChatBot
{
    public static class AudioPlayer
    {
        public static void PlayVoiceGreeting()
        {
            try
            {
                // Reference:
                // OpenAI. (2026). ChatGPT response on using SoundPlayer in C# for audio playback.
                // Available at: https://chat.openai.com/
                // (Accessed: April 2026)
                //Only play audio if it running on windows
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
                    SoundPlayer player = new SoundPlayer(path);
                    player.PlaySync();
                }
                else
                {
                    Console.WriteLine("Audio is not supported on this system.");
                }

            }
            catch
            {
                //the system does not crash even if the audio is missing or does not play
                Console.WriteLine("Voice greeting could not be played - file may be missing");
            }
        }
    }
}
