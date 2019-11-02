﻿using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http.Results;

namespace OverwatchAPI
{
    
    public class owFilter
    { //-1 para não informado
        public int role;//1 tank, 2 damage, 4 support , <1 para não informado
        public int[] level;// level+endorsement
        public int[] rating;
        public float[] damage;
        public float[] healing;
        public int[] elimination;
        public TimeSpan[] objTime; //null se nao informado
        public TimeSpan[] onfire; //null se nao informado
        public bool competitive;
    }
    public class profile 
    {
        public string name;
        public int level;
        public int endorsement;
        public int prestige;
        public int rating;
        public int tank_rating;
        public int damage_rating;
        public int support_rating;
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
        public TimeSpan objectiveTime;
        public float soloKills;
        public TimeSpan timeSpentOnFire;
    }
    public class OwPlayer
    {
        public int idHalf;
        public profile profile;
        public careerStats quickCareer;
        public careerStats compCareer;
    }
    public enum region
    {
        us=0, eu=1, asia=2
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
            int[] levels = new int[] { -1, -1, -1 };

            var ratings = token["ratings"];

            JArray rls = new JArray();
            if (ratings.HasValues)
            {
                rls = JArray.Parse(ratings.ToString());
            }

           
            byte i = 0;
            foreach (var r in rls)
            {
                if (r["role"].ToString() == "tank")
                {
                    levels[0] = r["level"].Value<int>();
                }
                else if (r["role"].ToString() == "damage")
                {
                    levels[1] = r["level"].Value<int>();
                }
                else if (r["role"].ToString() == "support")
                {
                    levels[2] = r["level"].Value<int>();
                }
                
                i++;
            }
           
            return new profile
            {
                endorsement = token["endorsement"].Value<int>(),
                name = token["name"].ToString(),
                level = token["level"].Value<int>(),
                prestige = token["prestige"].Value<int>(),
                rating = token["rating"].Value<int>(),
                tank_rating = levels[0],
                damage_rating = levels[1],
                support_rating = levels[2]
            };
        }

        private static careerStats getCareer(JToken token)
        {
            return (token != null) ?
             new careerStats
             {
                 allDamageDone =
                    token["allDamageDoneAvgPer10Min"] != null ? token["allDamageDoneAvgPer10Min"].Value<float>() : 0f,
                 barrierDamageDone =
                    token["barrierDamageDoneAvgPer10Min"] != null ? token["barrierDamageDoneAvgPer10Min"].Value<float>() : 0f,
                 deaths =
                    token["deathsAvgPer10Min"] != null ? token["deathsAvgPer10Min"].Value<float>() : 0f,
                 eliminations =
                    token["eliminationsAvgPer10Min"] != null ? token["eliminationsAvgPer10Min"].Value<float>() : 0f,
                 finalBlows =
                    token["finalBlowsAvgPer10Min"] != null ? token["finalBlowsAvgPer10Min"].Value<float>() : 0f,
                 healingDone =
                    token["healingDoneAvgPer10Min"] != null ? token["healingDoneAvgPer10Min"].Value<float>() : 0f,
                 heroDamageDone =
                    token["heroDamageDoneAvgPer10Min"] != null ? token["heroDamageDoneAvgPer10Min"].Value<float>() : 0f,
                 objectiveKills =
                    token["objectiveKillsAvgPer10Min"] != null ? token["objectiveKillsAvgPer10Min"].Value<float>() : 0f,
                 objectiveTime =
                    token["objectiveTimeAvgPer10Min"] != null ?
                    TimeSpan.ParseExact(token["objectiveTimeAvgPer10Min"].ToString(), @"mm\:ss", new CultureInfo("en-US")) :
                    TimeSpan.Zero,
                 soloKills =
                    token["soloKillsAvgPer10Min"] != null ? token["soloKillsAvgPer10Min"].Value<float>() : 0f,
                 timeSpentOnFire =
                    token["timeSpentOnFireAvgPer10Min"] != null ?
                    TimeSpan.ParseExact(token["timeSpentOnFireAvgPer10Min"].ToString(), @"mm\:ss", new CultureInfo("en-US")) :
                    TimeSpan.Zero
            }:null;
        }

        static OwAPI()
        {
            client = new HttpClient();
        }

        public static OwPlayer GetPlayerProfile(string name, region reg, int id)
        {
            string url = ENDPOINT_API + regStr(reg) + $"/{name}/complete";
            JObject obj = JObject.Parse(client.GetAsync(url).Result.Content.ReadAsStringAsync().Result);
            if (obj.ContainsKey("error")) return null;
                return new OwPlayer
            {
                idHalf = id,
                profile = getProfile(obj)
            };
        }

        public static OwPlayer GetPlayer(string name, region reg, int id)
        {
           
            string url = ENDPOINT_API + regStr(reg) + $"/{name}/complete";
            JObject obj = JObject.Parse(client.GetAsync(url).Result.Content.ReadAsStringAsync().Result);
            if (obj.ContainsKey("error")) return null;// throw new Exception($"[{name}] " + obj["error"].Value<string>());
            JToken quickCr = obj["quickPlayStats"].HasValues? obj["quickPlayStats"]["careerStats"]["allHeroes"]["average"]:null;
            JToken compCr = obj["competitiveStats"].HasValues? obj["competitiveStats"]["careerStats"]["allHeroes"]["average"]:null;

            return new OwPlayer
            {
                idHalf = id,
                profile =  getProfile(obj),
                compCareer = getCareer(compCr),
                quickCareer = getCareer(quickCr)
            };
           
        }
        
        public static JObject GetPlayerComplete(string name, region reg)
                => JObject.Parse(client.GetAsync(ENDPOINT_API + regStr(reg) + $"/{name}/complete")
                                                                    .Result.Content.ReadAsStringAsync().Result);

        public static IEnumerable<OwPlayer> GetPlayerProfile(List<string> name, List<region> reg, List<int> ids)
        {
            if (name.Count != reg.Count) throw new Exception("Tamanho das listas não batem");

            for (int i = 0; i < name.Count; i++)
            {
                yield return GetPlayerProfile(name[i], reg[i],ids[i]);
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

        public static IEnumerable<OwPlayer> GetPlayer(List<string> name, List<region> reg, List<int> ids)
        {
            if (name.Count != reg.Count) throw new Exception("Tamanho das listas não batem");

            for (int i = 0; i < name.Count; i++)
            {
                string url = ENDPOINT_API + regStr(reg[i]) + $"/{name[i]}/complete";
                JObject obj = JObject.Parse(client.GetAsync(url).Result.Content.ReadAsStringAsync().Result);
                if (obj.ContainsKey("error")) {
                    continue;
                }
                JToken quickCr = obj["quickPlayStats"].HasValues ? obj["quickPlayStats"]["careerStats"]["allHeroes"]["average"] : null;
                JToken compCr = obj["competitiveStats"].HasValues ? obj["competitiveStats"]["careerStats"]["allHeroes"]["average"] : null;

                yield return new OwPlayer
                {
                    idHalf = ids[i],
                    profile = getProfile(obj),
                    compCareer = getCareer(compCr),
                    quickCareer = getCareer(quickCr)
                };
                
            }
        }

    }
}
