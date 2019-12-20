using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Runner.Utils
{
    public class GameSettings
    {
        [JsonProperty]
        internal int BGSpeed = 200;

        [JsonProperty]
        internal bool Fullscreen;

        [JsonProperty]
        internal int ScreenWidth = GameData.ScreenSizes[4, 0];

        [JsonProperty]
        internal float MenuScale = 1;

        [JsonProperty]
        internal int DefaultTeam;

        [JsonProperty]
        internal string PlayerName = "noob";

        [JsonProperty]
        internal bool Cheats = false;

        [JsonProperty]
        internal bool Debug = false;

        [JsonProperty]
        internal bool UseSlowGeoSketch = false;

        public void Save()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(@".\save.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, this);
            }
        }

        static public GameSettings Load()
        {
            try
            {
                string text = File.ReadAllText(@".\save.json");
                return JsonConvert.DeserializeObject<GameSettings>(text);
            }
            catch
            {
                return new GameSettings();
            }
        }

        static public GameSettings Reset()
        {
            if (File.Exists(@".\save.json"))
            {
                File.Delete(@".\save.json");
            }

            return new GameSettings();
        }
    }
}