using HalfPugg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace HalfPugg.Controllers
{
    public class GraphController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();


        public interface IVert
        {
            string GetName();
        }

        public class VertGame : IVert
        {
            public Game data;

            public string GetName()=>data.Name;
            
        }
        public class VertPlayer : IVert
        {
            public Player data;
            public string GetName() => data.Name;

        }



        [System.Web.Http.Route("api/Graph/GetPlayersMatch")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetPlayerMatchs()
        {
         
            Graph<Player, Match,int> graph = new Graph<Player, Match,int>((Match m)=> {

                return m.Weight;
            });
            var matches = db.Matches.ToArray();
            foreach(var m in matches)
            {
                
                graph.AddVertice(m.Player1,m.IdPlayer1);
                graph.AddVertice(m.Player2, m.IdPlayer2);
                graph.AddAresta(m.IdPlayer1, m.IdPlayer2, m);
            }
        
            string res = graph.ToNet((Graph<Player, Match, int>.Vertice v) => v.info.Name);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "value");
            response.Content = new StringContent(res, Encoding.Unicode);

            return response;
        }

        [System.Web.Http.Route("api/Graph/GetPlayersGame")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetPlayerGame()
        {
            Graph<IVert, object, string> graph = new Graph<IVert, object, string>();
           
            foreach(var a in db.PlayerGames.ToArray())
            {
                graph.AddVertice(new VertGame { data = a.Game }, "g_"+a.Game.Name);
               
                graph.AddVertice(new VertPlayer { data = a.Gamer }, "p_" + a.Gamer.Nickname);
                graph.AddAresta("g_" + a.Game.Name, "p_" + a.Gamer.Nickname, null);
          
            }

            string res = graph.ToNet((Graph<IVert, object, string>.Vertice v)=> v.info.GetName());
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "value");
            response.Content = new StringContent(res, Encoding.Unicode);
            return response;
        }

        [System.Web.Http.Route("api/Graph/GetTagsGame")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetTagsGame()
        {
            Graph<string,object,string> graph = new Graph<string, object, string>();
            
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