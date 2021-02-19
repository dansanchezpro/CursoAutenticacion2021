using System;
using System.Collections.Generic;
using System.Security.Claims;
namespace EdDSAJwtBearer
{
    public class EdDSAJwtBearServer
    {
        public EdDSAJwtBearerServerOptions EdDSAJwtBearServerOptions { get; set; }
        public EdDSAJwtBearServer()
        {
        }
        public EdDSAJwtBearServer(EdDSAJwtBearerServerOptions options)
        {
            EdDSAJwtBearServerOptions = options;
        }
        public string CreateToken(IEnumerable<Claim> claims, string[] roles, DateTime expires)
            => EdDSATokenHandler.CreateToken(EdDSAJwtBearServerOptions.PrivateSigningKey,
                EdDSAJwtBearServerOptions.Issuer,
                EdDSAJwtBearServerOptions.Audience, claims, roles, expires);
    }
}
