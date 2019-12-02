using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace HalfPugg._3rd
{

    public class DotaFilter
    {
        public int[] kills,
            deaths,
            assists,
            xp_per_min,
            gold_per_min,
            hero_damage,
            tower_damage,
            hero_healing,
            last_hits,
            mmr_estimate,
            competitive_rank,
            rank_tier;
    }

    public class PlayerStats
    {
        public int kills,
            deaths,
            assists,
            xp_per_min,
            gold_per_min,
            hero_damage,
            tower_damage,
            hero_healing,
            last_hits;
    }

    public class MmrEstimate
    {
        public int estimate { get; set; }
    }

    public class Profile
    {
        public int account_id { get; set; }
        public string personaname { get; set; }
        public string name { get; set; }
        public string steamid { get; set; }
        public string avatar { get; set; }
        public string last_login { get; set; }
        public string loccountrycode { get; set; }
    }

    public class DotaPlayer
    {
        public int idHalf { get; set; }
        public string tracked_until { get; set; }
        public int solo_competitive_rank { get; set; }
        public int competitive_rank { get; set; }
        public int rank_tier { get; set; }
        public int leaderboard_rank { get; set; }
        public MmrEstimate mmr_estimate { get; set; }
        public Profile profile { get; set; }
        public PlayerStats stats { get; set; }
    }

    public static class DotaAPI
    {
        private static HttpClient client;

        public const string ENDPOINT_API = "https://api.opendota.com/api/";

        static JsonSerializerSettings setings = new JsonSerializerSettings();

        static DotaAPI()
        {
            client = Client.httpClient;
            setings.NullValueHandling = NullValueHandling.Ignore;
        }

        public static async Task<DotaPlayer> GetPlayer(string PlayerID, int id)
        {
            string res = await client.GetAsync(ENDPOINT_API + $"/players/{PlayerID}").Result.Content.ReadAsStringAsync();
            DotaPlayer player = JsonConvert.DeserializeObject<DotaPlayer>(res, setings);
            if (player == null) return null;
            string stt = await client.GetAsync(ENDPOINT_API + $"/players/{PlayerID}/recentMatches").Result.Content.ReadAsStringAsync();
            PlayerStats[] stats = JsonConvert.DeserializeObject<PlayerStats[]>(stt, setings);
            int sttC = stats.Length;
            PlayerStats pStats = new PlayerStats();

            if(stats!=null && sttC != 0)
            {
                foreach (var s in stats)
                {
                    pStats.assists += s.assists;
                    pStats.deaths += s.deaths;
                    pStats.gold_per_min += s.gold_per_min;
                    pStats.hero_damage += s.hero_damage;
                    pStats.hero_healing += s.hero_healing;
                    pStats.kills += s.kills;
                    pStats.last_hits += s.last_hits;
                    pStats.tower_damage += s.tower_damage;
                    pStats.xp_per_min += s.xp_per_min;
                }
                pStats.assists /= sttC;
                pStats.deaths /= sttC;
                pStats.gold_per_min /= sttC;
                pStats.hero_damage /= sttC;
                pStats.hero_healing /= sttC;
                pStats.kills /= sttC;
                pStats.last_hits /= sttC;
                pStats.tower_damage /= sttC;
                pStats.xp_per_min /= sttC;
            }

            player.stats = pStats;
            player.idHalf = id;

            return player;
        }

        public static IEnumerable<DotaPlayer> GetPlayers(ICollection<string> PlayerID, ICollection<int> ids)
        {
            int count = 0;
            foreach(string id in PlayerID)
            {
                yield return GetPlayer(id, ids.ToArray()[count]).Result;
                count++;
            }
        }

        
    }
}