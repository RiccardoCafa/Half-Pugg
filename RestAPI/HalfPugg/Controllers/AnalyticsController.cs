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

    public class PlayerMatch
    {
        public Player player;
        public float weight;
    }

    public class Analytics
    {
        public List<PlayerMatch> TopTenPlayers = new List<PlayerMatch>(10);
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
                if (m.Weight == 0) continue;
                analytics.TopTenPlayers.Add(new PlayerMatch { player = userId == m.IdPlayer1 ? m.Player2 : m.Player1, weight = m.Weight });
            }

            analytics.MatchesDate.Sort((a, b) => a.dataMatch.CompareTo(b.dataMatch));
            analytics.TopTenPlayers = analytics.TopTenPlayers.Take(10).ToList();

            return Ok(analytics);
        }

        [HttpGet]
        [Route("api/Analytics/GetPlayersConnections")]
        public IHttpActionResult GetNetworkPlayer(string nickname)
        {
            Player player = db.Gamers.FirstOrDefault(w => w.Nickname == nickname);
            if (player == null) return NotFound();
            Graph<dynamic, float, int> graph = new Graph<dynamic, float, int>();
            var matches = db.Matches.Where(x=>x.IdPlayer1 == player.ID || x.IdPlayer2 == player.ID).AsEnumerable().ToArray();
            foreach(var m in matches)
            {
                graph.AddVertice(new { m.Player1.Nickname }, m.IdPlayer1);
                graph.AddVertice(new { m.Player2.Nickname }, m.IdPlayer2);
                graph.AddAresta(m.IdPlayer1, m.IdPlayer2, m.Weight);
                if(m.IdPlayer1 != player.ID)
                {
                    var matches2 = db.Matches.Where(x => x.IdPlayer1 == m.IdPlayer1 || x.IdPlayer2 == m.IdPlayer2).AsEnumerable().ToArray();
                    foreach(var m2 in matches2)
                    {

                    }
                }
            }
            return Ok(graph.ToNet());
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

            string res = graph.ToNet((Graph<Player, Match, int>.Vertice v) => v.info.Nickname);
            int count = 0;
            bool readingVertices = true;
            List<dynamic> playerPair = new List<dynamic>();
            List<dynamic> edgesPair = new List<dynamic>();
            foreach (var line in res.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                count++;
                if(count == 1)
                {
                    int vertices = int.Parse(line.Split(' ')[1]);
                    continue;
                }
                if (line.Contains("*edges"))
                {
                    readingVertices = false;
                    continue;
                }
                if (readingVertices)
                {
                    string[] vertice = line.Split(' ');
                    int ID = int.Parse(vertice[0]);
                    string Nickname = vertice[1].Trim('\"');
                    playerPair.Add(new { ID, Nickname });
                }
                else
                {
                    string[] edge = line.Split(' ');
                    string PlayerDe = playerPair.Where(x => x.ID == int.Parse(edge[0])).First().Nickname;
                    string PlayerPara = playerPair.Where(x => x.ID == int.Parse(edge[1])).First().Nickname;
                    int Weight = int.Parse(edge[2]);
                    edgesPair.Add(new { PlayerDe, PlayerPara, Weight });
                }
            } 

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new { edgesPair, playerPair });
            //response.Content = new ObjectContent//new StringContent(res, Encoding.Unicode);

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
