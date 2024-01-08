namespace KintellaLocalizationREST.Helpers
{
    public class AppSettings
    {
        public string? Secret { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? ConnectionString { get; set; }
        public AppSettings()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            Secret = configuration.GetValue<string>("AppSettings:Secret");
            Issuer = configuration.GetValue<string>("AppSettings:Issuer");
            Audience = configuration.GetValue<string>("AppSettings:Audience");
            ConnectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
        }
       
    }
}