using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Utils
{
    internal class SoundManager
    {
        static private string[] Paths =
        {
            "jump",
            "die",
            "gameover"
        };

        static private List<SoundEffect> Sounds = new List<SoundEffect>();

        public int Song
        {
            get => default;
            set
            {
            }
        }

        static public void Load(ContentManager content)
        {
            foreach (string path in Paths)
            {
                Sounds.Add(content.Load<SoundEffect>("Graphics/sounds/" + path));
            }
        }

        static public void Play(string sound)
        {
            SoundEffectInstance instance = Sounds[Array.FindIndex(Paths, x => x == sound)].CreateInstance();
            instance.Play();
        }

        public void SetVolume()
        {
            throw new System.NotImplementedException();
        }
    }
}