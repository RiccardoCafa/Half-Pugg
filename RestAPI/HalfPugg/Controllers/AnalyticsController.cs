using HalfPugg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HalfPugg.Controllers
{
    public class MatchDate
    {
        public string PlayerName;
        public DateTime dataMatch;
    }

    public class Analytics
    {
        public List<Player> TopTenPlayers;
        public List<MatchDate> MatchesDate;

        public Analytics()
        {
            TopTenPlayers = new List<Player>();
            MatchesDate = new List<MatchDate>();
        }
    }

    public class AnalyticsController : ApiController
    {
        HalfPuggContext db;

        public AnalyticsController()
        {
            db = new HalfPuggContext();
        }

        [HttpGet]
        [Route("api/GetAnalytics")]
        public IHttpActionResult GetAnalytics(int userId)
        {
            Analytics analytics = new Analytics();

            Player me = db.Gamers.Find(userId);
            List<Match> matches = db.Matches.Where(x => x.IdPlayer1 == userId || x.IdPlayer2 == userId).AsEnumerable().ToList();
            List<Player> players = db.Gamers.Where(x => x.ID != userId).AsEnumerable().ToList();

            foreach(Player player in players)
            {
                if(matches.Find(x => x.IdPlayer1 == player.ID || x.IdPlayer2 == player.ID) == null)
                {
                    analytics.TopTenPlayers.Add(player);
                }
                if(analytics.TopTenPlayers.Count == 10)
                {
                    break;
                }
            }

            foreach(Match m in matches)
            {
                analytics.MatchesDate.Add(new MatchDate()
                {
                    dataMatch = m.CreateAt,
                    PlayerName = userId == m.IdPlayer1 ? m.Player2.Nickname : m.Player1.Nickname,
                });
            }

            analytics.MatchesDate.Sort((a, b) => a.dataMatch.CompareTo(b.dataMatch));

            return Ok(analytics);
        }

    }
}
