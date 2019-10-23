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

        static OwAPI()
        {
            client = new HttpClient();
        }

        public static player GetPlayerProfile(string name, region reg)
        {
            string url = ENDPOINT_API + regStr(reg) + $"/{name}/complete";
            JObject obj = JObject.Parse(client.GetAsync(url).Result.Content.ReadAsStringAsync().Result);

            JArray rls = JArray.Parse(obj["ratings"].ToString());
            role[] roles = new role[rls.Count];
            byte i = 0;
            foreach (var r in rls)
            {
                roles[i] = new role
                {
                    name = r["role"].ToString(),
                    level = int.Parse(r["level"].ToString())
                };
            }

            return new player
            {
                profile = new profile
                {
                    endorsement = int.Parse(obj["endorsement"].ToString()),
                    name = obj["name"].ToString(),
                    level = int.Parse(obj["level"].ToString()),
                    prestige = int.Parse(obj["prestige"].ToString()),
                    rating = int.Parse(obj["rating"].ToString()),
                    ratings = roles
                },
            };
        }

        public static IEnumerable<player> GetPlayerProfile(List<string> name, List<region> reg)
        {
            if (name.Count != reg.Count) throw new Exception("Tamanho das listas não batem");

            for (int i = 0; i < name.Count; i++)
            {
                yield return GetPlayerProfile(name[i], reg[i]);
            }
        }

        public static player GetPlayer(string name, region reg)
        {
            string url = ENDPOINT_API + regStr(reg) + $"/{name}/complete";
            JObject obj = JObject.Parse(client.GetAsync(url).Result.Content.ReadAsStringAsync().Result);
            JToken quickCr = obj["quickPlayStats"]["careerStats"]["allHeroes"]["average"];
            JToken compCr = obj["competitiveStats"]["careerStats"]["allHeroes"]["average"];
            JArray rls = JArray.Parse(obj["ratings"].ToString());
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
            return new player
            {
                profile = new profile
                {
                    endorsement = obj["endorsement"].Value<int>(),
                    name = obj["name"].ToString(),
                    level = obj["level"].Value<int>(),
                    prestige = obj["prestige"].Value<int>(),
                    rating = obj["rating"].Value<int>(),
                    ratings = roles
                },
                quickCareer = new careerStats
                {
                    allDamageDone = quickCr["allDamageDoneAvgPer10Min"].Value<float>(),
                    barrierDamageDone = quickCr["barrierDamageDoneAvgPer10Min"].Value<float>(),
                    deaths = quickCr["deathsAvgPer10Min"].Value<float>(),
                    eliminations = quickCr["eliminationsAvgPer10Min"].Value<float>(),
                    finalBlows = quickCr["finalBlowsAvgPer10Min"].Value<float>(),
                    healingDone = quickCr["healingDoneAvgPer10Min"].Value<float>(),
                    heroDamageDone = quickCr["heroDamageDoneAvgPer10Min"].Value<float>(),
                    objectiveKills = quickCr["objectiveKillsAvgPer10Min"].Value<float>(),
                    objectiveTime = quickCr["objectiveTimeAvgPer10Min"].ToString(),
                    soloKills = quickCr["soloKillsAvgPer10Min"].Value<float>(),
                    timeSpentOnFire = quickCr["timeSpentOnFireAvgPer10Min"].ToString()
                },
                compCareer = new careerStats
                {
                    allDamageDone = compCr["allDamageDoneAvgPer10Min"].Value<float>(),
                    barrierDamageDone = compCr["barrierDamageDoneAvgPer10Min"].Value<float>(),
                    deaths = compCr["deathsAvgPer10Min"].Value<float>(),
                    eliminations = compCr["eliminationsAvgPer10Min"].Value<float>(),
                    finalBlows = compCr["finalBlowsAvgPer10Min"].Value<float>(),
                    healingDone = compCr["healingDoneAvgPer10Min"].Value<float>(),
                    heroDamageDone = compCr["heroDamageDoneAvgPer10Min"].Value<float>(),
                    objectiveKills = compCr["objectiveKillsAvgPer10Min"].Value<float>(),
                    objectiveTime = compCr["objectiveTimeAvgPer10Min"].ToString(),
                    soloKills = compCr["soloKillsAvgPer10Min"].Value<float>(),
                    timeSpentOnFire = compCr["timeSpentOnFireAvgPer10Min"].ToString()
                }
            };
        }

        public static IEnumerable<player> GetPlayer(List<string> name, List<region> reg)
        {
            if (name.Count != reg.Count) throw new Exception("Tamanho das listas não batem");

            for (int i = 0; i < name.Count; i++)
            {
                yield return GetPlayer(name[i], reg[i]);
            }
        }

        public static JObject GetPlayerComplete(string name, region reg)
                => JObject.Parse(client.GetAsync(ENDPOINT_API + regStr(reg) + $"/{name}/complete")
                                                                    .Result.Content.ReadAsStringAsync().Result);

        public static IEnumerable<JObject> GetPlayerComplete(List<string> name, List<region> reg)
        {
            if (name.Count != reg.Count) throw new Exception("Tamanho das listas não batem");

            for (int i = 0; i < name.Count; i++)
            {
                yield return GetPlayerComplete(name[i], reg[i]);
            }
        }
    }
}
