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
                /*
                 * Reference:OpenAI. (2026). ChatGPT response on using SoundPlayer in C# for audio playback.
                 * Available at: https://chat.openai.com/
                 * Only play audio if it running on windows
                 */
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
                    SoundPlayer player = new SoundPlayer(path);
                    player.PlaySync();
                }

            }
            catch
            {
                //silent fail which doesn't crash the app
               
            }
        }
    }
}
