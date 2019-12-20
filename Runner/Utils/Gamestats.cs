using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Runner.Utils
{
    public class GlobalstatsIO_AccessToken
    {
        public string access_token = null;
        public string token_type = null;
        public string expires_in = null;
        public Int32 created_at = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

        public bool isValid()
        {
            //Check if still valid, allow a 2 minute grace period
            return (created_at + int.Parse(expires_in) - 120) > (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }

    public class GlobalstatsIO_RankResponseData
    {
        public List<GlobalstatsIO_StatisticValues> data = null;
    }

    public class GlobalstatsIO_user_rank
    {
        public string name = null;
        public int value;
        public int rank;
    }

    public class GlobalstatsIO_RankResponse
    {
        public GlobalstatsIO_RankResponseData better_ranks = null;
        public GlobalstatsIO_RankResponseData worse_ranks = null;
        public GlobalstatsIO_user_rank user_rank = null;
    }

    public class GlobalstatsIO_StatisticResponse
    {
        public string name = null;
        public string _id = null;

        public List<GlobalstatsIO_StatisticValues> values = null;
    }

    public class GlobalstatsIO_StatisticValues
    {
        public string key = null;
        public int value = 0;
        public string sorting = null;
        public int rank = 0;
        public string value_change = "0";
        public string rank_change = "0";
        public string name = null;
    }

    public class GlobalStats
    {
        private GlobalstatsIO_AccessToken token = null;

        public void getAccessToken()
        {
            var client = new RestClient("https://api.globalstats.io/oauth/access_token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Length", "157");
            request.AddHeader("Host", "api.globalstats.io");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", "grant_type=client_credentials&scope=endpoint_client&client_id=" + GameStatsStore.api_id + "&client_secret=" + GameStatsStore.api_secret, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            token = JsonConvert.DeserializeObject<GlobalstatsIO_AccessToken>(response.Content);
        }

        public GlobalstatsIO_StatisticResponse PostScore(string username, int score, string id = null)
        {
            if (token == null || token.isValid()) getAccessToken();

            var client = new RestClient("https://api.globalstats.io/v1/statistics" + (id == null ? "" : "/" + id));
            Method methode = id == null ? Method.POST : Method.PUT;
            var request = new RestRequest(methode);
            request.AddHeader("Authorization", "Bearer " + token.access_token);
            request.AddHeader("Content-Type", "application/json");

            request.AddParameter("undefined", "{\"name\":\"" + username + "\",\"values\":{\"Score\":" + score + "}}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<GlobalstatsIO_StatisticResponse>(response.Content);
        }

        public GlobalstatsIO_RankResponse GetRank(string id)
        {
            if (token == null || token.isValid()) getAccessToken();

            var client = new RestClient("https://api.globalstats.io/v1/statistics/" + id + "/section/Score");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token.access_token);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<GlobalstatsIO_RankResponse>(response.Content);
        }

        public GlobalstatsIO_RankResponseData GetTopPlayers()
        {
            if (token == null || token.isValid()) getAccessToken();

            var client = new RestClient("https://api.globalstats.io/v1/gtdleaderboard/Score?limit=100");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + token.access_token);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<GlobalstatsIO_RankResponseData>(response.Content);
        }
    }
}