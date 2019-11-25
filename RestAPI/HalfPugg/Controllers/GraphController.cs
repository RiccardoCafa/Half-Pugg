using HalfPugg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace HalfPugg.Controllers
{
    public class GraphController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();


        [ResponseType(typeof(string))]
        [Route("api/Graph/GetPlayersMatch")]
        [HttpGet]
        public IHttpActionResult GetPlayerMatchs()
        {

            Graph<Player, Match,int> graph = new Graph<Player, Match,int>((Match m)=> {

                return m.Weight;
            });

            foreach(var m in db.Matches)
            {
                
                graph.AddVertice(m.Player1,m.IdPlayer1);
                graph.AddVertice(m.Player2, m.IdPlayer2);
                graph.AddAresta(m.IdPlayer1, m.IdPlayer2, m);
            }

            string res = graph.ToNet();
            return Ok();
        }
    }
}