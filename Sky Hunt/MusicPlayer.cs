using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading.Tasks;

namespace Sky_Hunt
{
    public static class MusicPlayer
    {
        static List<SoundPlayer> music = new List<SoundPlayer>() {
        new SoundPlayer(Path.Combine(Directory.GetParent(Directory.
                GetCurrentDirectory()).Parent.FullName, "Resources\\track1.wav")),
        new SoundPlayer(Path.Combine(Directory.GetParent(Directory.
                GetCurrentDirectory()).Parent.FullName, "Resources\\track2.wav")),
        new SoundPlayer(Path.Combine(Directory.GetParent(Directory.
                GetCurrentDirectory()).Parent.FullName, "Resources\\track3.wav"))};
        public static async void Play(int startIndex)
        {
            for (int i = startIndex; i < music.Count; i++)
            {
                await Task.Run(() => music[i].PlaySync());
                if (i == music.Count - 1)
                    i = -1;
            }
        }
    }
}