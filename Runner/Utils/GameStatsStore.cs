using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Utils
{
    internal class GameStatsStore
    {
        public static string api_id = "";
        public static string api_secret = "";

        [JsonProperty]
        internal GlobalStats GameStatsToken = new GlobalStats();

        [JsonProperty]
        internal GlobalstatsIO_StatisticResponse GameStatsPlayer = new GlobalstatsIO_StatisticResponse();

        public void Save()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(@".\score.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, this);
            }
        }

        static public GameSettings Load()
        {
            try
            {
                string text = File.ReadAllText(@".\score.json");
                return JsonConvert.DeserializeObject<GameSettings>(text);
            }
            catch
            {
                return new GameSettings();
            }
        }
    }
}