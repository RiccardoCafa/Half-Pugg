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
        public int rating;
    }

    /*
     > Overwatch é um jogo que se divide em partida rapida e competitivo (tem mais modalidades mas a API trata apenas essas duas)
     
     > Os dados gerais do jogador, aqueles que não estão diretamente ligados ao desempenho dele na partida (Nome, nivel, prestigio,etc...),
     estão armazenados no perfil (profile)
     
     > O sistema de nivel é separado em duas partes: level e endorsement, endorsement se refere a parte da centena e level fica com a parte
     da dezena e unidade ex: 311 é endorsement 3 e level 11
     
     > Prestige é a indicação que o jogador recebe aos finais de partidas
     
     > rating é a "pontuação" média do jogador, quanto mais vitorias mais pontos o jogador ganha e maior o elo ele recebe, essa pontuação
     existe apenas no modo competitivo, entretanto fica disponivel no perfil do usuario

     > a pontuação tambem é calculada isoladamente para cada classe que o jogador joga (Tank,Dps,Support), a classe é representada aqui pela
     estrutura role, o rating citado anteriormente é a media das pontuações das classes que o jogador joga, sendo que ele pode jogar apenas 
     uma, com duas ou as tres

     > as informações referentes as partidas competitivas e rapidas são iguais e representados pela mesma classe (careeStats), a classe Player
     que representa o jogador em si contém duas referencias para careerStats, uma para competitivo e outra para partida rapida
     */


    public class owFilter
    { //-1 para não informado
        public int role;//-1 none, 0 tank, 1 dps, 2 haler
        public int[] level;// level+endorsement
        public int[] rating;
        public float[] damage;
        public float[] healing;
        public int[] elimination;
        public DateTime[] objTime; //00:00 nao informado
        public DateTime[] onfire; //00:00 nao informado
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
                    rating = r["level"].Value<int>()
                };
                i++;
            }
           
            return new profile
            {
                endorsement = token["endorsement"].Value<int>(),
                name = token["name"].ToString(),
                level = token["level"].Value<int>(),
                prestige = token["prestige"].Value<int>(),
                rating = token["rating"].Value<int>(),
                ratings = roles
            };
        }

        private static careerStats getCareer(JToken token)
        {
            return (token != null) ?
             new careerStats
             {
                  
                 allDamageDone = token["allDamageDoneAvgPer10Min"].Value<float>(),
                 barrierDamageDone = token["barrierDamageDoneAvgPer10Min"].Value<float>(),
                 deaths = token["deathsAvgPer10Min"].Value<float>() ,
                 eliminations = token["eliminationsAvgPer10Min"].Value<float>(),
                finalBlows = token["finalBlowsAvgPer10Min"].Value<float>(),
                healingDone =token["healingDoneAvgPer10Min"].Value<float>(),
                heroDamageDone =token["heroDamageDoneAvgPer10Min"].Value<float>(),
                objectiveKills = token["objectiveKillsAvgPer10Min"].Value<float>(),
                objectiveTime = token["objectiveTimeAvgPer10Min"].ToString(),
                soloKills = token["soloKillsAvgPer10Min"].Value<float>(),
                timeSpentOnFire = token["timeSpentOnFireAvgPer10Min"].ToString()
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
            if (obj.ContainsKey("error")) throw new Exception($"[{name}] " + obj["error"].Value<string>());
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
                string url = ENDPOINT_API + regStr(reg[i]) + $"/{name[i]}/complete";
                JObject obj = JObject.Parse(client.GetAsync(url).Result.Content.ReadAsStringAsync().Result);
                if (obj.ContainsKey("error")) throw new Exception($"[{name[i]}] " + obj["error"].Value<string>());
                JToken quickCr = obj["quickPlayStats"].HasValues ? obj["quickPlayStats"]["careerStats"]["allHeroes"]["average"] : null;
                JToken compCr = obj["competitiveStats"].HasValues ? obj["competitiveStats"]["careerStats"]["allHeroes"]["average"] : null;

                yield return new player
                {
                    profile = getProfile(obj),
                    compCareer = getCareer(compCr),
                    quickCareer = getCareer(quickCr)
                };
                
            }
        }

    }
}
