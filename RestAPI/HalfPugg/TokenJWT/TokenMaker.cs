using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalfPugg.TokenJWT
{

    public class TokenMaker
    {
        public static readonly string secret = "This is my shared, not secret, secret!";

        private IJwtAlgorithm algorithm;
        private IJsonSerializer serializer;
        private IBase64UrlEncoder urlEncoder;
        private IJwtEncoder encoder;

        public TokenMaker()
        {
            algorithm = new HMACSHA256Algorithm();
            serializer = new JsonNetSerializer();
            urlEncoder = new JwtBase64UrlEncoder();
            encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
        }

        /// <summary>
        /// Decode a token with the secret and payload values. 
        /// A expiration time can be setted for the token.
        /// </summary>
        /// <param name="payload">The payload values, stored in a dictionary</param>
        /// <param name="expTime">Time to expirate in seconds</param>
        /// <returns>Encoded token</returns>
        public string MakeToken(Dictionary<string, object> payload, int expTime = 0)
        {
            if(expTime != 0)
            {
                IDateTimeProvider timeDaNet = new UtcDateTimeProvider();
                var agora = timeDaNet.GetNow();
                var segundos = UnixEpoch.GetSecondsSince(agora) + expTime;

                payload.Add("exp", segundos);
            }
            return encoder.Encode(payload, secret);
        }
    }

    public class TokenValidation
    {
        private string secret;

        private IJsonSerializer serializer;
        private IDateTimeProvider timeNet;
        private IJwtValidator validator;
        private IBase64UrlEncoder urlEncoder;
        private IJwtDecoder decoder;

        public TokenValidation()
        {
            secret = TokenMaker.secret;

            serializer = new JsonNetSerializer();
            timeNet = new UtcDateTimeProvider();
            validator = new JwtValidator(serializer, timeNet);
            urlEncoder = new JwtBase64UrlEncoder();
            decoder = new JwtDecoder(serializer, validator, urlEncoder);
        }

        public string ValidateToken(string token)
        {
            try
            {
                var data = decoder.Decode(token, secret, verify: true);
                return data;
            } catch(TokenExpiredException)
            {
                Console.WriteLine("Token expirado");
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token inválido");
            }
            return null;
        }
    }
}