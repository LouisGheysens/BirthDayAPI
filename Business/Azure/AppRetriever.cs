namespace Business.Azure
{
    public static class AppRetriever
    {
        public static string GetConnectionstring()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration config = builder.Build();
            return config.GetValue<string>("ConnectionStrings:BirthDatabaseContext");

        }


        public static string GetAzureKey()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration config = builder.Build();
            return config.GetValue<string>("ConnectionStrings:AzureBlobStorage");
        }
    }
}
