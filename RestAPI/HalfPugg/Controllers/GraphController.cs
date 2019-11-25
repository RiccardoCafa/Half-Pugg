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
     
      
        [System.Web.Http.Route("api/Graph/GetPlayersMatch")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetPlayerMatchs()
        {
         
            Graph<Player, Match,string> graph = new Graph<Player, Match,string>((Match m)=> {

                return m.Weight;
            });
            var matches = db.Matches.ToArray();
            foreach(var m in matches)
            {
                
                graph.AddVertice(m.Player1,m.Player1.Name);
                graph.AddVertice(m.Player2, m.Player2.Name);
                graph.AddAresta(m.Player1.Name, m.Player2.Name, m);
            }
        
            string res = graph.ToNet();

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "value");
            response.Content = new StringContent(res, Encoding.Unicode);

            return response;
        }
    }
}