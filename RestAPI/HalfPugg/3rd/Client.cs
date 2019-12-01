using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;


    public static class Client
    {
        public static HttpClient httpClient;

        static Client(){
            httpClient = new HttpClient();
        }
    }
