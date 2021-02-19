namespace EdDSAJwtBearer
{
    public static class EdDSAJwtBearerErrors
    {
        public const string ValidIssuerRequired = "Valid issuer is required when vlidate user is true";
        public const string ValidAudienceRequired = "Valid audience is required when validate audience is true";
        public const string InvalidToken = "(001) Invalid bearer autenthication token";
        public const string InvalidIssuer = "(002) Invalid issuer";
        public const string InvalidAudience = "(003) Invalid audience";
        public const string ExpiredToken = "(004) Token has expired";
    }
}
