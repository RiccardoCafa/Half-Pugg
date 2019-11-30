using HalfPugg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;

namespace HalfPugg.Controllers
{

    #region Class

    public interface IVert
    {
        string GetName();
    }

    public class VertGame : IVert
    {
        public Game data;

        public string GetName() => data.Name;

    }
    public class VertPlayer : IVert
    {
        public Player data;
        public string GetName() => data.Name;

    }


    public class MatchDate
    {
        public string PlayerName;
        public DateTime dataMatch;
    }

    public class Analytics
    {
        public List<Player> TopTenPlayers = new List<Player>(10);
        public List<MatchDate> MatchesDate = new List<MatchDate>();
    }

    #endregion

    public class AnalyticsController : ApiController
    {
        HalfPuggContext db = new HalfPuggContext();

       
        [HttpGet]
        [Route("api/GetAnalytics")]
        public IHttpActionResult GetAnalytics(int userId)
        {
            Analytics analytics = new Analytics();

            List<Match> matches = db.Matches.Where(x => x.IdPlayer1 == userId || x.IdPlayer2 == userId).OrderByDescending(x=>x.Weight).ThenBy(x=>x.CreateAt).ToList();
          
            foreach(Match m in matches)
            {
                analytics.MatchesDate.Add(new MatchDate()
                {
                    dataMatch = m.CreateAt,
                    PlayerName = userId == m.IdPlayer1 ? m.Player2.Nickname : m.Player1.Nickname,
                });

                analytics.TopTenPlayers.Add(userId == m.IdPlayer1 ? m.Player2 : m.Player1);
            }

            analytics.MatchesDate.Sort((a, b) => a.dataMatch.CompareTo(b.dataMatch));
            analytics.TopTenPlayers = analytics.TopTenPlayers.Take(10).ToList();

            return Ok(analytics);
        }



        [Route("api/Analytics/GetPlayersMatch")]
        [HttpGet]
        public HttpResponseMessage GetPlayerMatchs()
        {

            Graph<Player, Match, int> graph = new Graph<Player, Match, int>((Match m) => {

                return m.Weight;
            });
            var matches = db.Matches.ToArray();
            foreach (var m in matches)
            {

                graph.AddVertice(m.Player1, m.IdPlayer1);
                graph.AddVertice(m.Player2, m.IdPlayer2);
                graph.AddAresta(m.IdPlayer1, m.IdPlayer2, m);
            }

            string res = graph.ToNet((Graph<Player, Match, int>.Vertice v) => v.info.Name);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "value");
            response.Content = new StringContent(res, Encoding.Unicode);

            return response;
        }

        [Route("api/Analytics/GetPlayersGame")]
        [HttpGet]
        public HttpResponseMessage GetPlayerGame()
        {
            Graph<IVert, object, string> graph = new Graph<IVert, object, string>();

            foreach (var a in db.PlayerGames.ToArray())
            {
                graph.AddVertice(new VertGame { data = a.Game }, "g_" + a.Game.Name);

                graph.AddVertice(new VertPlayer { data = a.Gamer }, "p_" + a.Gamer.Nickname);
                graph.AddAresta("g_" + a.Game.Name, "p_" + a.Gamer.Nickname, null);

            }

            string res = graph.ToNet((Graph<IVert, object, string>.Vertice v) => v.info.GetName());
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "value");
            response.Content = new StringContent(res, Encoding.Unicode);
            return response;
        }

        [Route("api/Analytics/GetTagsGame")]
        [HttpGet]
        public HttpResponseMessage GetTagsGame()
        {
            Graph<string, object, string> graph = new Graph<string, object, string>();

            foreach (var item in db.HashTags.ToArray())
            {
                graph.AddVertice(item.Hashtag, "h_" + item.ID_Matter);
                foreach (var con in item.Games)
                {
                    graph.AddVertice(con.Name, "g_" + con.ID_Game);
                    graph.AddAresta("h_" + item.ID_Matter, "g_" + con.ID_Game);
                }
            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "value");
            response.Content = new StringContent(graph.ToNet(), Encoding.Unicode);

            return response;
        }
    }
}
