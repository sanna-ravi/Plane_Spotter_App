using System;
using System.Collections.Generic;
using System.Text;

namespace Spotters.Service.ServiceModels
{
    public sealed class BearerTokensOptions
    {
        public string Key { set; get; }
        public string Issuer { set; get; }
        public string Audience { get; set; }
        public int TokenExpirationMinutes { get; set; }
        public string RSAPrivateKey { get; set; }
        public string RSAPublicKey { get; set; }
    }
}
