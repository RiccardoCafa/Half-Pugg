using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace OverwatchAPI
{

    public class role
    {
        public string name;
        public int level;
    }
    public class profile
    {
        public string name;
        public int level;
        public int endorsement;
        public int prestige;
        public int rating;
        public role[] ratings;
    }
    public class careerStats
    {
        public float allDamageDone;
        public float barrierDamageDone;
        public float deaths;
        public float eliminations;
        public float finalBlows;
        public float healingDone;
        public float heroDamageDone;
        public float objectiveKills;
        public string objectiveTime;
        public float soloKills;
        public string timeSpentOnFire;
    }
    public class player
    {
        public profile profile;
        public careerStats quickCareer;
        public careerStats compCareer;
    }


    public enum region
    {
        us, eu, asia
    }

    public static class OwAPI
    {
        private static HttpClient client;

        public const string ENDPOINT_API = "https://ow-api.com/v1/stats/pc/";

        private static string regStr(region reg)
        {
            switch (reg)
            {
                case region.asia:
                    return "asia";
                case region.eu:
                    return "eu";
                case region.us:
                    return "us";
            }
            return "";
        }

        private static profile getProfile(JToken token)
        {
            if (token == null) return null;
            var ratings = token["ratings"];

            JArray rls = new JArray();

            if (ratings.HasValues)
            {
                rls = JArray.Parse(ratings.ToString());
            }

            role[] roles = new role[rls.Count];
            byte i = 0;
            foreach (var r in rls)
            {
                roles[i] = new role
                {
                    name = r["role"].ToString(),
                    level = r["level"].Value<int>()
                };
            }
           
            return new profile
            {
                endorsement = token["endorsement"].HasValues?token["endorsement"].Value<int>():-1,
                name = token["name"].HasValues?token["name"].ToString():"none",
                level = token["level"].HasValues?token["level"].Value<int>():-1,
                prestige = token["prestige"].HasValues?token["prestige"].Value<int>():-1,
                rating = token["rating"].HasValues?token["rating"].Value<int>():-1,
                ratings = roles
            };
        }

        private static careerStats getCareer(JToken token)
        {
            return (token != null) ?
             new careerStats
             {
                 allDamageDone = token["allDamageDoneAvgPer10Min"].HasValues ? token["allDamageDoneAvgPer10Min"].Value<float>() : -1,
                 barrierDamageDone = token["barrierDamageDoneAvgPer10Min"].HasValues ? token["barrierDamageDoneAvgPer10Min"].Value<float>(): - 1,
                 deaths = token["deathsAvgPer10Min"].HasValues ? token["deathsAvgPer10Min"].Value<float>() : -1,
                 eliminations = token["eliminationsAvgPer10Min"].HasValues ? token["eliminationsAvgPer10Min"].Value<float>():-1,
                finalBlows = token["finalBlowsAvgPer10Min"].HasValues?token["finalBlowsAvgPer10Min"].Value<float>():-1,
                healingDone = token["healingDoneAvgPer10Min"].HasValues?token["healingDoneAvgPer10Min"].Value<float>():-1,
                heroDamageDone = token["heroDamageDoneAvgPer10Min"].HasValues?token["heroDamageDoneAvgPer10Min"].Value<float>():-1,
                objectiveKills = token["objectiveKillsAvgPer10Min"].HasValues?token["objectiveKillsAvgPer10Min"].Value<float>():-1,
                objectiveTime = token["objectiveTimeAvgPer10Min"].HasValues?token["objectiveTimeAvgPer10Min"].ToString():"",
                soloKills = token["soloKillsAvgPer10Min"].HasValues?token["soloKillsAvgPer10Min"].Value<float>():-1,
                timeSpentOnFire = token["timeSpentOnFireAvgPer10Min"].HasValues?token["timeSpentOnFireAvgPer10Min"].ToString():""
            }:null;
        }

        static OwAPI()
        {
            client = new HttpClient();
        }

        public static player GetPlayerProfile(string name, region reg)
        {
            string url = ENDPOINT_API + regStr(reg) + $"/{name}/complete";
            JObject obj = JObject.Parse(client.GetAsync(url).Result.Content.ReadAsStringAsync().Result);

            return new player
            {
                profile = getProfile(obj)
            };
        }

        public static player GetPlayer(string name, region reg)
        {
           
            string url = ENDPOINT_API + regStr(reg) + $"/{name}/complete";
            JObject obj = JObject.Parse(client.GetAsync(url).Result.Content.ReadAsStringAsync().Result);
            if (obj.ContainsKey("error")) throw new Exception("Erro ao buscar jogador:" + obj["error"].Value<string>());
            JToken quickCr = obj["quickPlayStats"].HasValues? obj["quickPlayStats"]["careerStats"]["allHeroes"]["average"]:null;
            JToken compCr = obj["competitiveStats"].HasValues? obj["competitiveStats"]["careerStats"]["allHeroes"]["average"]:null;

            return new player
            {
                profile =  getProfile(obj),
                compCareer = getCareer(compCr),
                quickCareer = getCareer(quickCr)
            };
           
        }
        
        public static JObject GetPlayerComplete(string name, region reg)
                => JObject.Parse(client.GetAsync(ENDPOINT_API + regStr(reg) + $"/{name}/complete")
                                                                    .Result.Content.ReadAsStringAsync().Result);

        public static IEnumerable<player> GetPlayerProfile(List<string> name, List<region> reg)
        {
            if (name.Count != reg.Count) throw new Exception("Tamanho das listas não batem");

            for (int i = 0; i < name.Count; i++)
            {
                yield return GetPlayerProfile(name[i], reg[i]);
            }
        }

        public static IEnumerable<JObject> GetPlayerComplete(List<string> name, List<region> reg)
        {
            if (name.Count != reg.Count) throw new Exception("Tamanho das listas não batem");

            for (int i = 0; i < name.Count; i++)
            {
                yield return GetPlayerComplete(name[i], reg[i]);
            }
        }

        public static IEnumerable<player> GetPlayer(List<string> name, List<region> reg)
        {
            if (name.Count != reg.Count) throw new Exception("Tamanho das listas não batem");

            for (int i = 0; i < name.Count; i++)
            {
                yield return GetPlayer(name[i], reg[i]);
            }
        }

    }
}
