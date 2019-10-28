using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace HalfPugg.Token
{
    public class SigningConfigurations
    {
        public SecurityKey key { get; }
        public SigningCredentials SigingCredential { get; }

        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigingCredential = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}